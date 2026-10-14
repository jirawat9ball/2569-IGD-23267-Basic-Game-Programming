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
        public GameObject exitPrefab;
        public GameObject armorPrefab;
        public GameObject stickPrefab;

        [Header("Parent Transform สำหรับจัดระเบียบ Hierarchy")]
        public Transform floorParent;
        public Transform wallParent;
        public Transform itemParent;

        [Header("จำนวนสิ่งกีดขวาง/ไอเทม")]
        public int threatCount = 2;
        public int armorCount = 2;
        public int stickCount = 2;

        /// <summary>
        /// ตารางข้อมูลแผนที่ 2 มิติ (5 x 5) ที่สอดคล้องกับโจทย์ Level 2
        /// 0 = ช่องว่าง (Empty)
        /// 1 = ผู้เล่น (Player)
        /// 2 = ภัยคุกคาม/ศัตรู (Threat / Enemy)
        /// 3 = ไอเทม/ไม้สติ๊ก (Item / Wooden Stick)
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

            // 3. กำหนดตำแหน่งผู้เล่น (Player) ที่ playerStartPos (รหัส 1)
            PlacePlayer();

            // 4. กำหนดตำแหน่งทางออก (Exit) ที่ exitPos (รหัส 9)
            PlaceExit();

            // 5. สุ่มวางภัยคุกคาม (Threats) ตาม threatCount (รหัส 2)
            PlaceThreats();

            // 6. เสกสร้างไอเทมเกราะ (Armor Items) ผ่าน ArmorSpawner ของ AS03 (รหัส 3)
            SpawnArmor();
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

        // ขั้นตอนที่ 3: กำหนดตำแหน่งผู้เล่น (Player)
        private void PlacePlayer()
        {
            if (playerStartPos.x >= 0 && playerStartPos.x < rows && playerStartPos.y >= 0 && playerStartPos.y < columns)
            {
                mapGrid[playerStartPos.x, playerStartPos.y] = 1; // Player code 1
            }
        }

        // ขั้นตอนที่ 4: กำหนดตำแหน่งทางออก (Exit)
        private void PlaceExit()
        {
            if (exitPos.x >= 0 && exitPos.x < rows && exitPos.y >= 0 && exitPos.y < columns)
            {
                mapGrid[exitPos.x, exitPos.y] = 9; // Exit code 9
                if (exitPrefab != null)
                {
                    var exitObj = Instantiate(exitPrefab, new Vector3(exitPos.y, exitPos.x, 0), Quaternion.identity);
                    exitObj.name = "Exit";
                }
            }
        }

        // ขั้นตอนที่ 5: สุ่มวางภัยคุกคาม (Threats)
        private void PlaceThreats()
        {
            int placedThreats = 0;
            int maxAttempts = 50;
            while (placedThreats < threatCount && maxAttempts-- > 0)
            {
                int r = Random.Range(0, rows);
                int c = Random.Range(0, columns);
                if (mapGrid[r, c] == 0 && !(r == playerStartPos.x && c == playerStartPos.y))
                {
                    mapGrid[r, c] = 2; // Threat code 2
                    placedThreats++;
                }
            }
        }

        // ขั้นตอนที่ 6: เสกสร้างไอเทมเกราะ (Armor Items) ผ่าน ArmorSpawner ของ Level 3 (AS03)
        [ContextMenu("Spawn Armor")]
        public void SpawnArmor()
        {
            SpawnItems();
        }

        [ContextMenu("Spawn Items")]
        public void SpawnItems()
        {
            var prefabToSpawn = armorPrefab != null ? armorPrefab : stickPrefab;
            if (prefabToSpawn == null)
            {
                #if UNITY_EDITOR
                prefabToSpawn = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Exam/Iron Armor.prefab");
                #endif
            }

            if (itemParent == null) itemParent = CreateChildFolder("Items");

            // ดึงหรือแนบคอมโพเนนต์ ArmorSpawner จาก AS03 (รองรับทั้งนักเรียนและอาจารย์แบบ Dynamic)
            Component spawnerComp = null;
            var spawnerType = System.Type.GetType("Week08.Ex03.ArmorSpawner, Workspace")
                ?? System.Type.GetType("Week08.ArmorSpawner, Workspace")
                ?? System.Type.GetType("ArmorSpawner, Workspace")
                ?? System.Type.GetType("Week08.Teacher.Ex03.ArmorSpawner, Workspace")
                ?? System.Type.GetType("Week08.Ex03.ArmorSpawner, Assembly-CSharp")
                ?? System.Type.GetType("Week08.ArmorSpawner, Assembly-CSharp")
                ?? System.Type.GetType("ArmorSpawner, Assembly-CSharp")
                ?? System.Type.GetType("Week08.Teacher.Ex03.ArmorSpawner, Assembly-CSharp");

            if (spawnerType != null)
            {
                spawnerComp = GetComponent(spawnerType) ?? gameObject.AddComponent(spawnerType);
            }

            int count = armorCount > 0 ? armorCount : stickCount;
            for (int i = 0; i < count; i++)
            {
                int r = Random.Range(0, rows);
                int c = Random.Range(0, columns);
                int attempts = 50;
                while (mapGrid[r, c] != 0 && attempts-- > 0)
                {
                    r = Random.Range(0, rows);
                    c = Random.Range(0, columns);
                }

                if (mapGrid[r, c] == 0)
                {
                    mapGrid[r, c] = 3; // Item code 3 (Armor / Item)
                    Vector3 spawnPos = new Vector3(c, r, 0);

                    GameObject spawned = null;
                    if (spawnerComp != null)
                    {
                        var method = spawnerComp.GetType().GetMethod("SpawnArmor") 
                            ?? spawnerComp.GetType().GetMethod("SpawnWeapon");
                        if (method != null)
                        {
                            spawned = method.Invoke(spawnerComp, new object[] { prefabToSpawn, spawnPos }) as GameObject;
                        }
                    }

                    if (spawned == null && prefabToSpawn != null)
                    {
                        spawned = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
                    }

                    if (spawned != null)
                    {
                        if (itemParent != null) spawned.transform.SetParent(itemParent);
                        spawned.name = $"Iron Armor_{i + 1}";
                    }
                }
            }
        }

        // =========================================================================
        // ระดับ 2: การค้นหาและเรดาร์สำรวจดันเจี้ยนบนตาราง (Dungeon Grid Radar)
        // =========================================================================

        // 1. ค้นหาพิกัดไอเทมช่องแรกบนตาราง
        public Vector2Int FindItemPosition(int itemCode)
        {
            return FindItemPosition(this.mapGrid, itemCode);
        }

        public virtual Vector2Int FindItemPosition(int[,] grid, int itemCode)
        {
            // Student code starts HERE ...
            return new Vector2Int(-1, -1);
            // Student code ends HERE ...
        }

        // 2. นับจำนวนไอเทมบนตาราง
        public int CountItems(int itemCode)
        {
            return CountItems(this.mapGrid, itemCode);
        }

        public virtual int CountItems(int[,] grid, int itemCode)
        {
            // Student code starts HERE ...
            return 0;
            // Student code ends HERE ...
        }

        // 3. สแกนหาภัยคุกคามรอบตัวผู้เล่นในรัศมี 1 ช่อง (8 ทิศทาง)
        public int ScanThreats(Vector2Int playerPos, int threatCode = 2)
        {
            return ScanThreats(this.mapGrid, playerPos, threatCode);
        }

        public virtual int ScanThreats(int[,] grid, Vector2Int playerPos, int threatCode = 2)
        {
            // Student code starts HERE ...
            return 0;
            // Student code ends HERE ...
        }

        // 4. หาพิกัดตำแหน่งผู้เล่น
        public Vector2Int FindPlayerPosition()
        {
            return playerStartPos;
        }

        public virtual Vector2Int FindPlayerPosition(MapGenerator mapGen)
        {
            // Student code starts HERE ...
            if (mapGen == null) return new Vector2Int(-1, -1);
            return mapGen.playerStartPos;
            // Student code ends HERE ...
        }

        public Vector2Int FindPlayerPosition(int playerCode)
        {
            return FindPlayerPosition(this.mapGrid, playerCode);
        }

        public virtual Vector2Int FindPlayerPosition(int[,] grid, int playerCode)
        {
            // Student code starts HERE ...
            return new Vector2Int(-1, -1);
            // Student code ends HERE ...
        }
    }
}
