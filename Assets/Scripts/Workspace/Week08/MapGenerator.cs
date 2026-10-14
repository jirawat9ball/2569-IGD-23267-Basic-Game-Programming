using UnityEngine;

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
        public GameObject stickPrefab;

        [Header("Parent Transform สำหรับจัดระเบียบ Hierarchy")]
        public Transform floorParent;
        public Transform wallParent;
        public Transform itemParent;

        [Header("จำนวนสิ่งกีดขวาง/ไอเทม")]
        public int threatCount = 2;
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

        [ContextMenu("Generate Map")]
        public void GenerateMap()
        {
            ClearMap();

            mapGrid = new int[rows, columns];

            // สร้าง Parent Transforms หากยังไม่ได้กำหนด
            if (floorParent == null) floorParent = CreateChildFolder("Floors");
            if (wallParent == null) wallParent = CreateChildFolder("Walls");
            if (itemParent == null) itemParent = CreateChildFolder("Items");

            // 1. สร้างพื้น 5x5
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

            // 2. สร้างกำแพงล้อมรอบ 5x5
            if (wallPrefabs != null && wallPrefabs.Length > 0)
            {
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

            // 3. กำหนดตำแหน่งผู้เล่นที่ตำแหน่ง playerStartPos (รหัส 1)
            if (playerStartPos.x >= 0 && playerStartPos.x < rows && playerStartPos.y >= 0 && playerStartPos.y < columns)
            {
                mapGrid[playerStartPos.x, playerStartPos.y] = 1; // Player code 1
            }

            // 4. กำหนดทางออกที่ตำแหน่ง exitPos (รหัส 9)
            if (exitPos.x >= 0 && exitPos.x < rows && exitPos.y >= 0 && exitPos.y < columns)
            {
                mapGrid[exitPos.x, exitPos.y] = 9;
                if (exitPrefab != null)
                {
                    var exitObj = Instantiate(exitPrefab, new Vector3(exitPos.y, exitPos.x, 0), Quaternion.identity);
                    exitObj.name = "Exit";
                }
            }

            // 4. สุ่มวาง Threat (รหัส 2) ในตาราง
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

            // 5. วางไม้สติ๊ก (Wooden Stick)
            if (stickPrefab != null && itemParent != null)
            {
                for (int i = 0; i < stickCount; i++)
                {
                    int r = Random.Range(0, rows);
                    int c = Random.Range(0, columns);
                    if (mapGrid[r, c] == 0)
                    {
                        mapGrid[r, c] = 3; // Item code 3 (Wooden Stick)
                        var stick = Instantiate(stickPrefab, new Vector3(c, r, 0), Quaternion.identity, itemParent);
                        stick.name = $"Wooden Stick_{i + 1}";
                    }
                }
            }
        }

        private Transform CreateChildFolder(string name)
        {
            var child = transform.Find(name);
            if (child != null) return child;
            var go = new GameObject(name);
            go.transform.SetParent(transform);
            return go.transform;
        }
    }
}
