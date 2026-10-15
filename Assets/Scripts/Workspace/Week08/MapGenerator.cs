using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08
{
    /// <summary>
    /// MapGenerator ขนาด 5 x 5 ที่โจทย์จัดเตรียมไว้ให้สำหรับข้อสอบปฏิบัติ Week 08
    /// จัดการการสร้างพื้น, กำแพง, ทางออก, สิ่งกีดขวาง และไอเทมไม้สติ๊ก
    /// </summary>
    public class MapGenerator : MonoBehaviour
    {
        [Header("ขนาดแผนที่ (5 x 5)")]
        public int rows = 5;
        public int columns = 5;

        [Header("ตำแหน่งเริ่มต้น")]
        public Vector2Int playerStartPos = new Vector2Int(0, 0);
        public Vector2Int exitPos = new Vector2Int(4, 4);

        [Header("Prefab ที่ใช้สร้างฉาก")]
        public GameObject[] floorPrefabs;
        public GameObject[] wallPrefabs;
        public GameObject playerPrefab;
        public GameObject[] itemPotions;
        public GameObject exitPrefab;
        public GameObject armorPrefab;

        [Header("Parent Transform สำหรับจัดระเบียบ Hierarchy")]
        public Transform floorParent;
        public Transform wallParent;
        public Transform itemParent;

        [Header("จำนวนสิ่งกีดขวาง/ไอเทม")]
        public int threatCount = 2;
        public int armorCount = 2;

        /// <summary>
        /// ตารางข้อมูลแผนที่ 2 มิติ (5 x 5) ที่สอดคล้องกับโจทย์ Level 3
        /// 0 = ช่องว่าง (Empty)
        /// 1 = ผู้เล่น (Player)
        /// 2 = ภัยคุกคาม/ศัตรู (Threat / Enemy)
        /// 3 = ไอเทม/เกราะ (Item / Armor)
        /// 9 = ทางออก (Exit)
        /// </summary>
        [System.NonSerialized]
        public int[,] mapGrid = new int[5, 5];

        void Start()
        {
            if (rows <= 0) rows = 5;
            if (columns <= 0) columns = 5;

            GenerateMap();
        }

        [ContextMenu("Clear Map")]
        public void ClearMap()
        {
            if (floorParent != null)
            {
                for (int i = floorParent.childCount - 1; i >= 0; i--)
                    SafeDestroy(floorParent.GetChild(i).gameObject);
            }
            if (wallParent != null)
            {
                for (int i = wallParent.childCount - 1; i >= 0; i--)
                    SafeDestroy(wallParent.GetChild(i).gameObject);
            }
            if (itemParent != null)
            {
                for (int i = itemParent.childCount - 1; i >= 0; i--)
                    SafeDestroy(itemParent.GetChild(i).gameObject);
            }

            var exits = GameObject.FindGameObjectsWithTag("Finish");
            foreach (var e in exits) SafeDestroy(e);
            var exitObj = GameObject.Find("Exit");
            if (exitObj != null) SafeDestroy(exitObj);
        }

        private static void SafeDestroy(GameObject obj)
        {
            if (obj == null) return;
            if (Application.isPlaying)
                Destroy(obj);
            else
                DestroyImmediate(obj);
        }

        private Transform CreateChildFolder(string name)
        {
            var child = transform.Find(name);
            if (child != null) return child;
            var go = new GameObject(name);
            go.transform.SetParent(transform);
            return go.transform;
        }

        [ContextMenu("Generate Map")]
        public void GenerateMap()
        {
            ClearMap();

            mapGrid = new int[rows, columns];

            // สร้าง Parent Transforms สำหรับจัดระเบียบ Hierarchy ในฉาก
            if (floorParent == null) floorParent = CreateChildFolder("Floors");
            if (wallParent == null) wallParent = CreateChildFolder("Walls");
            if (itemParent == null) itemParent = CreateChildFolder("Items");

            // 1. สร้างพื้น (Floors) ขนาด rows x columns
            GenerateFloors();

            // 2. สร้างกำแพงล้อมรอบ (Outer Walls)
            GenerateWalls();

            // 3. สร้างกำแพงรูปตัว L (L-shaped Wall) ภายในดันเจี้ยน (Level 3 - ข้อ 3)
            GenerateLWall();

            // 4. กำหนดตำแหน่งทางออก (Exit) ที่ exitPos (รหัส 9) (Level 3 - ข้อ 1)
            PlaceExit();

            // 5. สุ่มวางภัยคุกคาม (Threats) ตาม threatCount (รหัส 2)
            PlaceThreats();

            // 6. เสกสร้างไอเทมเกราะและยา (Level 3 - ข้อ 2)
            SpawnItems();
        }

        // =========================================================================
        // ฟังก์ชันสร้างแผนที่ย่อยแยกตามขั้นตอน (Steps 1 - 6)
        // =========================================================================

        // ขั้นตอนที่ 1: สร้างพื้น (Floors)
        private void GenerateFloors()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (floorPrefabs != null && floorPrefabs.Length > 0)
                    {
                        var prefab = floorPrefabs[Random.Range(0, floorPrefabs.Length)];
                        if (prefab != null)
                        {
                            var tile = Instantiate(prefab, new Vector3(c, r, 0), Quaternion.identity, floorParent);
                            tile.name = $"Floor_{r}_{c}";
                        }
                    }
                    mapGrid[r, c] = 0; // Empty
                }
            }
        }

        // ขั้นตอนที่ 2: สร้างกำแพงล้อมรอบ (Outer Walls)
        private void GenerateWalls()
        {
            if (wallPrefabs == null || wallPrefabs.Length == 0) return;

            for (int r = -1; r <= rows; r++)
            {
                for (int c = -1; c <= columns; c++)
                {
                    if (r == -1 || r == rows || c == -1 || c == columns)
                    {
                        var prefab = wallPrefabs[Random.Range(0, wallPrefabs.Length)];
                        if (prefab != null)
                        {
                            var wall = Instantiate(prefab, new Vector3(c, r, 0), Quaternion.identity, wallParent);
                            wall.name = $"OuterWall_{r}_{c}";
                        }
                    }
                }
            }
        }
        public void PlacePlayer()
        {
            // เมธอดสนับสนุนสำหรับกำหนดตำแหน่งผู้เล่น
            playerPrefab.transform.position = new Vector3(playerStartPos.y, playerStartPos.x, 0);   
        }

        // ขั้นตอนที่ 3 - 4: PlacePlayer() และ PlaceExit() ถูกประกาศไว้เป็น virtual method ในพื้นที่ของนักศึกษาด้านล่าง


        /// <summary>
        /// ค้นหาตำแหน่งช่องว่างแบบสุ่มบนแผนที่ ที่ไม่ทับกับ Player และ Exit
        /// </summary>
        private Vector2Int GetRandomEmptyCell()
        {
            var emptyCells = new System.Collections.Generic.List<Vector2Int>();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (mapGrid[r, c] == 0 && !(r == playerStartPos.x && c == playerStartPos.y) && !(r == exitPos.x && c == exitPos.y))
                    {
                        emptyCells.Add(new Vector2Int(r, c));
                    }
                }
            }
            if (emptyCells.Count == 0) return new Vector2Int(-1, -1);
            return emptyCells[Random.Range(0, emptyCells.Count)];
        }

        // ขั้นตอนที่ 5: สุ่มวางภัยคุกคาม (Threats)
        private void PlaceThreats()
        {
            for (int i = 0; i < threatCount; i++)
            {
                Vector2Int pos = GetRandomEmptyCell();
                if (pos.x != -1)
                {
                    mapGrid[pos.x, pos.y] = 2; // Threat code 2
                }
            }
        }

        // ขั้นตอนที่ 6: เสกสร้างไอเทม (Armor Items และ ItemPotions)
        [ContextMenu("Spawn All Items")]
        public void SpawnItems()
        {
            SpawnArmorItems();
            SpawnItemPotions();
        }

        /// <summary>
        /// เสกสร้างเกราะ (Armor Items) ผ่านเมธอด SpawnArmor ของ Level 2
        /// </summary>
        [ContextMenu("Spawn Armor Items")]
        public void SpawnArmorItems()
        {
            if (itemParent == null) itemParent = CreateChildFolder("Items");

            var prefab = armorPrefab;
            #if UNITY_EDITOR
            if (prefab == null)
            {
                prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Exam/Iron Armor.prefab");
            }
            #endif

            if (prefab == null) return;

            for (int i = 0; i < armorCount; i++)
            {
                Vector2Int pos = GetRandomEmptyCell();
                if (pos.x != -1)
                {
                    mapGrid[pos.x, pos.y] = 3; // Item code 3
                    Vector3 spawnPos = new Vector3(pos.y, pos.x, 0);
                    GameObject spawned = Instantiate(prefab, spawnPos, Quaternion.identity);
                    if (spawned != null)
                    {
                        spawned.transform.SetParent(itemParent);
                        spawned.name = $"{prefab.name}_{i + 1}";
                    }
                }
            }
        }

        /// <summary>
        /// เสกสร้างยา (ItemPotions) ลงในฉาก
        /// </summary>
        [ContextMenu("Spawn Item Potions")]
        public void SpawnItemPotions()
        {
            if (itemParent == null) itemParent = CreateChildFolder("Items");

            if (itemPotions != null)
            {
                foreach (var potionPrefab in itemPotions)
                {
                    if (potionPrefab == null) continue;
                    Vector2Int pos = GetRandomEmptyCell();
                    if (pos.x != -1)
                    {
                        mapGrid[pos.x, pos.y] = 3; // Item code 3
                        var potionObj = Instantiate(potionPrefab, new Vector3(pos.y, pos.x, 0), Quaternion.identity, itemParent);
                        potionObj.name = potionPrefab.name;
                    }
                }
            }
        }

        // =========================================================================
        // เมธอดสนับสนุนและฟังก์ชันเรียกใช้งานส่วนกลาง (Helper & Overload Wrappers)
        // =========================================================================
        public int ScanThreats(Vector2Int playerPos, int threatCode = 2) => ScanThreats(this.mapGrid, playerPos, threatCode);
        public Vector2Int FindPlayerPosition() => playerStartPos;

        /// <summary>
        /// ฟังก์ชันผู้ช่วย: ตรวจสอบว่าพิกัด (r, c) อยู่ภายในขอบเขตของตาราง grid หรือไม่
        /// </summary>
        public bool IsInBounds(int[,] grid, int r, int c)
        {
            if (grid == null) return false;
            return r >= 0 && r < grid.GetLength(0) && c >= 0 && c < grid.GetLength(1);
        }

        // =========================================================================
        // 🎯 พื้นที่สำหรับนักศึกษาเขียนโค้ดข้อสอบ (Student Exam Implementation)
        // รวมทุกเมธอดที่นักศึกษาต้องเขียนไว้ที่ด้านล่างนี้
        // =========================================================================

        // [Level 3 - ข้อ 1] กำหนดตำแหน่งทางออก (Exit) และเสกสร้าง Exit Prefab
        public virtual void PlaceExit()
        {
            // Student code starts HERE ...
            if (exitPrefab != null)
            {
                var exitObj = Instantiate(exitPrefab, new Vector3(exitPos.y, exitPos.x, 0), Quaternion.identity);
                exitObj.name = "Exit";
            }
            // Student code ends HERE ...
        }

        // [Level 3 - ข้อ 2] วางไอเทมเกราะลงในฉาก
        [ContextMenu("Place All Armors")]
        public virtual void PlaceArmor()
        {
            // Student code starts HERE ...
            if (armorPrefab == null) return;

            for (int i = 0; i < armorCount; i++)
            {
                int X = Random.Range(0, rows);
                int Y = Random.Range(0, columns);
                var armorObj = Instantiate(armorPrefab, new Vector3(Y, X, 0), Quaternion.identity, itemParent);
                if (armorObj != null)
                {
                    armorObj.name = $"Armor_{X}_{Y}";
                }
            }
            // Student code ends HERE ...
        }

        // [Level 3 - ข้อ 3] สร้างกำแพงรูปตัว L (L-shaped Wall) สูงครึ่งหนึ่งของแถว (rows) และกว้างครึ่งหนึ่งของคอลัมน์ (columns)
        public virtual void GenerateLWall()
        {
            // Student code starts HERE ...
            int wallHeight = rows / 2;
            int wallWidth = columns / 2;
            var wallPrefab = (wallPrefabs != null && wallPrefabs.Length > 1) ? wallPrefabs[1] : (wallPrefabs != null && wallPrefabs.Length > 0 ? wallPrefabs[0] : null);

            // 1. สร้างกำแพงแนวดิ่งตามแนวแถว r จาก 0 ถึง wallHeight (ที่ c = 0)
            for (int r = 0; r <= wallHeight; r++)
            {
                if (wallPrefab != null)
                {
                    Instantiate(wallPrefab, new Vector3(0, r, 0), Quaternion.identity, wallParent);
                }
            }

            // 2. สร้างกำแพงแนวนอนตามแนวคอลัมน์ c จาก 0 ถึง wallWidth (ที่ r = 0)
            for (int c = 0; c <= wallWidth; c++)
            {
                if (wallPrefab != null)
                {
                    Instantiate(wallPrefab, new Vector3(c, 0, 0), Quaternion.identity, wallParent);
                }
            }
            // Student code ends HERE ...
        }

        // [Level 3 - ข้อ 4] สแกนหาภัยคุกคามรอบตัวผู้เล่นในรัศมี 1 ช่อง (8 ทิศทาง)
        public virtual int ScanThreats(int[,] grid, Vector2Int playerPos, int threatCode = 2)
        {
            if (grid == null) return 0;

            int count = 0;

            // วนลูปสำรวจพื้นที่ 3x3 รอบตัวผู้เล่น
            for (int r = playerPos.x - 1; r <= playerPos.x + 1; r++)
            {
                for (int c = playerPos.y - 1; c <= playerPos.y + 1; c++)
                {
                    // ข้ามตำแหน่งที่ตัวผู้เล่นยืนอยู่
                    if (r == playerPos.x && c == playerPos.y) continue;

                    // ตรวจสอบขอบเขตตารางด้วย IsInBounds เพื่อป้องกัน Out of Bounds
                    if (IsInBounds(grid, r, c))
                    {
                        // Student code starts HERE ...
                        // ตรวจสอบว่าช่อง grid[r, c] ตรงกับ threatCode หรือไม่ ถ้าใช่ให้เพิ่มค่า count
                        if (grid[r, c] == threatCode)
                        {
                            count++;
                        }
                        // Student code ends HERE ...
                    }
                }
            }

            Debug.Log($"Found {count} threats around player at {playerPos}");
            return count;
        }
    }
}

