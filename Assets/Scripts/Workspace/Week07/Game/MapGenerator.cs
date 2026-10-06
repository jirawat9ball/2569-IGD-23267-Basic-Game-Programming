using System.Collections.Generic;
using UnityEngine;

namespace Week07.Game
{
    /// <summary>
    /// แผนที่ของเกม จัดการการสร้างพื้น, กำแพง, ผู้เล่น, ศัตรู, ไอเทม และทางออก
    /// รองรับทั้งโหมดสร้างฉากตัวอย่างสำหรับรัน Testcase (Build)
    /// และโหมดสร้างฉาก 2D Grid Game แบบเต็มรูปแบบจาก Prefab (GenerateVisualMap)
    /// โดยใช้ระบบการสืบทอด (Identity / Character) ในการจัดการวัตถุทั้งหมด
    /// </summary>
    public class MapGenerator : MonoBehaviour
    {
        public const int MapSize = 10;

        [Header("Grid Size")]
        public int Row = 10;
        public int Col = 10;

        [Header("Block Types")]
        public int empty = 0;
        public int demonWall = 1;
        public int enemy = 2;
        public int sword = 3;
        public int potion = 4;
        public int exit = 5;

        [Header("Map Grid Data (OOP Typed)")]
        [System.NonSerialized] public int[,] mapData;
        [System.NonSerialized] public Enemy[,] enemies;
        [System.NonSerialized] public ItemPotion[,] potions;
        [System.NonSerialized] public ItemSword[,] swords;
        [System.NonSerialized] public Wall[,] walls;
        public Exit exitObject;
        public Character player;

        [Header("Set Prefabs (Optional for Visual Map)")]
        public Player playerPrefab;
        public Exit exitPrefab;
        public GameObject[] floorsPrefab;
        public GameObject[] wallsPrefab;
        public GameObject[] enemyPrefab;
        public GameObject[] itemsPrefab;

        [Header("Set Transform Parents")]
        public Transform floorParent;
        public Transform wallParent;
        public Transform itemPotionParent;

        [Header("Spawn Counts")]
        public int obstacleCount = 3;
        public int itemPotionCount = 3;
        public Vector2Int playerStartPos;

        private readonly List<GameObject> spawned = new List<GameObject>();

        #region Unity Lifecycle

        private void Start()
        {
            if (Row <= 0) Row = 10;
            if (Col <= 0) Col = 10;

            // If mapData already exists (built by test or lecture demo), do not duplicate
            if (mapData != null) return;

            // If visual prefabs are provided (e.g. in full Unity Scene), generate visual dungeon
            if ((floorsPrefab != null && floorsPrefab.Length > 0) || (wallsPrefab != null && wallsPrefab.Length > 0))
            {
                GenerateVisualMap();
            }
            else
            {
                // Default build demo map for testing and lecture
                Build();
            }
        }

        #endregion

        #region Query Methods

        public int GetMapData(int x, int y)
        {
            if (x < 0 || x >= Row || y < 0 || y >= Col || mapData == null)
            {
                return demonWall;
            }
            return mapData[x, y];
        }

        #endregion

        #region Demo Map Generation (Testcase & Lecture Scenario)

        /// <summary>
        /// สร้างแผนที่ 10x10 ตามฉากตัวอย่างในโจทย์
        /// Player (0,0) energy 100 attack 10 · Potion (2,2) heal 20 · Sword (3,2) bonus 10 · Enemy (3,3) energy 60 attack 5
        /// </summary>
        public static MapGenerator CreateDemoMap()
        {
            var go = new GameObject("MapGenerator");
            var map = go.AddComponent<MapGenerator>();
            map.Build();
            return map;
        }

        public void Build()
        {
            Row = MapSize;
            Col = MapSize;
            mapData = new int[MapSize, MapSize];
            enemies = new Enemy[MapSize, MapSize];
            potions = new ItemPotion[MapSize, MapSize];
            swords = new ItemSword[MapSize, MapSize];
            walls = new Wall[MapSize, MapSize];

            player = Spawn<Player>("Player", 0, 0);
            player.energy = 100;
            player.attackPoint = 10;

            var thePotion = Spawn<ItemPotion>("Potion1", 2, 2);
            thePotion.healPoint = 20;
            potions[2, 2] = thePotion;
            mapData[2, 2] = potion;

            var theSword = Spawn<ItemSword>("Sword1", 3, 2);
            theSword.attackBonus = 10;
            swords[3, 2] = theSword;
            mapData[3, 2] = sword;

            var theEnemy = Spawn<Enemy>("Enemy1", 3, 3);
            theEnemy.energy = 60;
            theEnemy.attackPoint = 5;
            enemies[3, 3] = theEnemy;
            mapData[3, 3] = enemy;
        }

        #endregion

        #region Visual Map Generation (Full 2D Grid Game)

        /// <summary>
        /// สร้างแผนที่เกม 2D พร้อมพื้น, กำแพงล้อมรอบ, ผู้เล่น, ศัตรู, ไอเทม และทางออก
        /// โดยใช้วัตถุที่สืบทอดจาก Identity ทั้งหมด
        /// </summary>
        public void GenerateVisualMap()
        {
            mapData = new int[Row, Col];
            enemies = new Enemy[Row, Col];
            potions = new ItemPotion[Row, Col];
            swords = new ItemSword[Row, Col];
            walls = new Wall[Row, Col];

            GenerateFloorAndOuterWalls();
            PlacePlayerVisual();
            GenerateObstaclesVisual();
            GenerateItemsVisual();
            PlaceExitVisual();
        }

        private void GenerateFloorAndOuterWalls()
        {
            for (int x = -1; x < Row + 1; x++)
            {
                for (int y = -1; y < Col + 1; y++)
                {
                    if (IsBorder(x, y))
                    {
                        CreateOuterWall(x, y);
                    }
                    else
                    {
                        CreateFloor(x, y);
                    }
                }
            }
        }

        private bool IsBorder(int x, int y)
        {
            return x == -1 || x == Row || y == -1 || y == Col;
        }

        private void CreateOuterWall(int x, int y)
        {
            if (wallsPrefab == null || wallsPrefab.Length == 0) return;
            int r = Random.Range(0, wallsPrefab.Length);
            if (wallsPrefab[r] == null) return;

            GameObject obj = Instantiate(wallsPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            if (wallParent != null) obj.transform.parent = wallParent;
            obj.name = $"Wall_{x}, {y}";
        }

        private void CreateFloor(int x, int y)
        {
            if (floorsPrefab != null && floorsPrefab.Length > 0)
            {
                int r = Random.Range(0, floorsPrefab.Length);
                if (floorsPrefab[r] != null)
                {
                    GameObject obj = Instantiate(floorsPrefab[r], new Vector3(x, y, 1), Quaternion.identity);
                    if (floorParent != null) obj.transform.parent = floorParent;
                    obj.name = $"floor_{x}, {y}";
                }
            }

            if (mapData != null && x >= 0 && x < Row && y >= 0 && y < Col)
            {
                mapData[x, y] = empty;
            }
        }

        private void PlacePlayerVisual()
        {
            if (playerStartPos.x < 0 || playerStartPos.x >= Row || playerStartPos.y < 0 || playerStartPos.y >= Col)
            {
                playerStartPos = Vector2Int.zero;
            }

            GameObject prefabToUse = playerPrefab != null ? playerPrefab.gameObject : (player != null ? player.gameObject : null);
            player = Spawn<Player>("Player", playerStartPos.x, playerStartPos.y, prefabToUse);
            player.energy = 20;
            player.attackPoint = 10;
        }

        private void GenerateObstaclesVisual()
        {
            if (obstacleCount <= 0) return;

            int count = 0;
            int preventInfiniteLoop = 100;

            while (count < obstacleCount && --preventInfiniteLoop >= 0)
            {
                int x = Random.Range(0, Row);
                int y = Random.Range(0, Col);

                if (mapData != null && mapData[x, y] == empty && !(x == playerStartPos.x && y == playerStartPos.y))
                {
                    GameObject prefab = (enemyPrefab != null && enemyPrefab.Length > 0) ? enemyPrefab[Random.Range(0, enemyPrefab.Length)] : null;
                    var wall = Spawn<Wall>($"Wall_{x}_{y}", x, y, prefab);
                    walls[x, y] = wall;
                    mapData[x, y] = demonWall;
                    if (wallParent != null) wall.transform.parent = wallParent;
                    count++;
                }
            }
        }

        private void GenerateItemsVisual()
        {
            if (itemPotionCount <= 0) return;

            int count = 0;
            int preventInfiniteLoop = 100;

            while (count < itemPotionCount && --preventInfiniteLoop >= 0)
            {
                int x = Random.Range(0, Row);
                int y = Random.Range(0, Col);

                if (mapData != null && mapData[x, y] == empty && !(x == playerStartPos.x && y == playerStartPos.y))
                {
                    GameObject prefab = (itemsPrefab != null && itemsPrefab.Length > 0) ? itemsPrefab[Random.Range(0, itemsPrefab.Length)] : null;
                    // Alternating Potion and Sword
                    if (count % 2 == 0)
                    {
                        var pot = Spawn<ItemPotion>($"Potion_{x}_{y}", x, y, prefab);
                        potions[x, y] = pot;
                        mapData[x, y] = potion;
                        if (itemPotionParent != null) pot.transform.parent = itemPotionParent;
                    }
                    else
                    {
                        var swd = Spawn<ItemSword>($"Sword_{x}_{y}", x, y, prefab);
                        swords[x, y] = swd;
                        mapData[x, y] = sword;
                        if (itemPotionParent != null) swd.transform.parent = itemPotionParent;
                    }
                    count++;
                }
            }
        }

        private void PlaceExitVisual()
        {
            int exitX = Row - 1;
            int exitY = Col - 1;

            GameObject prefab = exitPrefab != null ? exitPrefab.gameObject : null;
            exitObject = Spawn<Exit>("Exit", exitX, exitY, prefab);
            mapData[exitX, exitY] = exit;
        }

        #endregion

        #region Spawn Helpers

        public T Spawn<T>(string objectName, int x, int y, GameObject prefab = null) where T : Identity
        {
            GameObject go;
            if (prefab != null)
            {
                go = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
            }
            else
            {
                go = new GameObject(objectName);
                go.transform.position = new Vector3(x, y, 0);
            }

            T identity = go.GetComponent<T>();
            if (identity == null)
            {
                identity = go.AddComponent<T>();
            }

            identity.name = objectName;
            identity.Name = objectName;
            identity.positionX = x;
            identity.positionY = y;
            identity.mapGenerator = this;

            spawned.Add(go);
            return identity;
        }

        /// <summary>เก็บกวาดวัตถุทั้งหมดที่สร้างไว้ (ใช้ตอนจบชุดทดสอบ)</summary>
        public void ClearMap()
        {
            foreach (var go in spawned)
            {
                if (go == null) continue;
                if (Application.isPlaying) Destroy(go);
                else DestroyImmediate(go);
            }
            spawned.Clear();

            if (Application.isPlaying) Destroy(gameObject);
            else DestroyImmediate(gameObject);
        }

        #endregion
    }
}
