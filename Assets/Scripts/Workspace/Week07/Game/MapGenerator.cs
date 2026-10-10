using System.Collections.Generic;
using UnityEngine;

namespace Week07.Game
{
    /// <summary>
    /// แผนที่ของเกม จัดการการสร้างพื้น, กำแพง, ผู้เล่น, สิ่งกีดขวาง, ไอเทม และทางออก
    /// </summary>
    public class MapGenerator : MonoBehaviour
    {
        [Header("Set MapGenerator")]
        public int Row = 10;
        public int Col = 10;

        [Header("Set Player")]
        public Player player;
        public Vector2Int playerStartPos;

        [Header("Set Exit")]
        public Exit Exit;

        [Header("Set Prefab")]
        public GameObject[] floorsPrefab;
        public GameObject[] wallsPrefab;
        public GameObject[] enemyPrefab;
        public GameObject[] itemsPrefab;

        [Header("Set Transform")]
        public Transform floorParent;
        public Transform wallParent;
        public Transform itemPotionParent;

        [Header("Set object Count")]
        public int obsatcleCount = 3;
        public int itemPotionCount = 3;

        // Block types
        [HideInInspector]
        public int empty = 0;
        [HideInInspector]
        public int demonWall = 1;
        [HideInInspector]
        public int enemy = 2;
        [HideInInspector]
        public int sword = 3;
        [HideInInspector]
        public int potion = 4;
        [HideInInspector]
        public int chest = 5;
        [HideInInspector]
        public int exit = 6;
        [HideInInspector]
        public string playerOnMap = "player";

        // Map Grid Data (OOP Typed)
        [System.NonSerialized] public int[,] mapData;
        [System.NonSerialized] public Wall[,] walls;
        [System.NonSerialized] public ItemPotion[,] potions;
        [System.NonSerialized] public ItemSword[,] swords;
        [System.NonSerialized] public Enemy[,] enemies;
        [System.NonSerialized] public Chest[,] chests;

        public const int MapSize = 10;
        public Exit exitObject => Exit;
        public Exit exitPrefab => Exit;
        public Player playerPrefab => player;
        public int obstacleCount
        {
            get => obsatcleCount;
            set => obsatcleCount = value;
        }

        private readonly List<GameObject> spawned = new List<GameObject>();

        #region Unity Lifecycle

        void Start()
        {
            // ป้องกัน IndexOutOfRangeException กรณี Row/Col ใน Inspector เป็น 0
            if (Row <= 0) Row = 10;
            if (Col <= 0) Col = 10;

            // If mapData already exists (built by test or lecture demo), do not duplicate
            if (mapData != null) return;

            GenerateFloorAndOuterWalls();
            PlacePlayer();
            GenerateObstacles();
            GenerateItems();
            PlaceExit();
        }

        #endregion

        #region Map Generation Methods

        /// <summary>สร้างพื้นภายในแผนที่และกำแพงล้อมรอบ</summary>
        public void GenerateFloorAndOuterWalls()
        {
            mapData = new int[Row, Col];
            if (walls == null) walls = new Wall[Row, Col];
            if (potions == null) potions = new ItemPotion[Row, Col];
            if (swords == null) swords = new ItemSword[Row, Col];
            if (enemies == null) enemies = new Enemy[Row, Col];
            if (chests == null) chests = new Chest[Row, Col];

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

        /// <summary>สร้างและวางตัวผู้เล่นลงในตำแหน่งเริ่มต้น</summary>
        public void PlacePlayer()
        {
            if (playerStartPos.x < 0 || playerStartPos.x >= Row || playerStartPos.y < 0 || playerStartPos.y >= Col)
            {
                playerStartPos = Vector2Int.zero;
            }

            if (player != null)
            {
                Player spawnedPlayer = Instantiate(player, new Vector3(playerStartPos.x, playerStartPos.y, -0.1f), Quaternion.identity);
                spawnedPlayer.name = "Player";
                spawnedPlayer.Name = "Player";
                spawnedPlayer.mapGenerator = this;
                spawnedPlayer.positionX = playerStartPos.x;
                spawnedPlayer.positionY = playerStartPos.y;
                spawnedPlayer.energy = 20;
                spawnedPlayer.attackPoint = 10;

                player = spawnedPlayer;
                spawned.Add(spawnedPlayer.gameObject);
            }
            else
            {
                player = Spawn<Player>("Player", playerStartPos.x, playerStartPos.y);
                player.energy = 20;
                player.attackPoint = 10;
            }

            if (mapData != null && playerStartPos.x < Row && playerStartPos.y < Col)
            {
                mapData[playerStartPos.x, playerStartPos.y] = empty;
            }
        }

        public void PlacePlayerVisual() => PlacePlayer();

        /// <summary>สุ่มวางสิ่งกีดขวาง (DemonWall/Enemy) ตามจำนวน obsatcleCount</summary>
        public void GenerateObstacles()
        {
            if (walls == null) walls = new Wall[Row, Col];
            if (enemies == null) enemies = new Enemy[Row, Col];
            if (obsatcleCount <= 0) return;

            int count = 0;
            int preventInfiniteLoop = 100;

            while (count < obsatcleCount && --preventInfiniteLoop >= 0)
            {
                int x = Random.Range(0, Row);
                int y = Random.Range(0, Col);

                if (mapData != null && mapData[x, y] == empty && !(x == playerStartPos.x && y == playerStartPos.y))
                {
                    PlaceDemonWall(x, y);
                    count++;
                }
            }
        }

        public void GenerateObstaclesVisual() => GenerateObstacles();

        /// <summary>สุ่มวางไอเทม (Potion/Sword/Chest) ตามจำนวน itemPotionCount</summary>
        public void GenerateItems()
        {
            if (potions == null) potions = new ItemPotion[Row, Col];
            if (swords == null) swords = new ItemSword[Row, Col];
            if (chests == null) chests = new Chest[Row, Col];
            if (itemPotionCount <= 0) return;

            int count = 0;
            int preventInfiniteLoop = 100;

            while (count < itemPotionCount && --preventInfiniteLoop >= 0)
            {
                int x = Random.Range(0, Row);
                int y = Random.Range(0, Col);

                if (mapData != null && mapData[x, y] == empty && !(x == playerStartPos.x && y == playerStartPos.y))
                {
                    PlaceItem(x, y, count);
                    count++;
                }
            }
        }

        public void GenerateItemsVisual() => GenerateItems();

        /// <summary>สร้างและวางทางออกที่มุมขวาบนของแผนที่</summary>
        public void PlaceExit()
        {
            if (Row <= 0 || Col <= 0) return;

            int exitX = Row - 1;
            int exitY = Col - 1;

            if (Exit != null)
            {
                Exit spawnedExit;
                if (!Exit.gameObject.scene.IsValid())
                {
                    spawnedExit = Instantiate(Exit, new Vector3(exitX, exitY, 0), Quaternion.identity);
                    spawned.Add(spawnedExit.gameObject);
                }
                else
                {
                    spawnedExit = Exit;
                    spawnedExit.transform.position = new Vector3(exitX, exitY, 0);
                }
                spawnedExit.name = "Exit";
                spawnedExit.Name = "Exit";
                spawnedExit.positionX = exitX;
                spawnedExit.positionY = exitY;
                spawnedExit.mapGenerator = this;
                Exit = spawnedExit;
            }
            else
            {
                Exit = Spawn<Exit>("Exit", exitX, exitY);
            }

            if (mapData != null) mapData[exitX, exitY] = exit;
        }

        public void PlaceExitVisual() => PlaceExit();

        public void GenerateVisualMap()
        {
            GenerateFloorAndOuterWalls();
            PlacePlayer();
            GenerateObstacles();
            GenerateItems();
            PlaceExit();
        }

        #endregion

        #region Spawn Helpers

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
            spawned.Add(obj);
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
                    spawned.Add(obj);
                }
            }

            if (mapData != null && x >= 0 && x < Row && y >= 0 && y < Col)
            {
                mapData[x, y] = empty;
            }
        }

        public void PlaceItem(int x, int y, int index = 0)
        {
            if (itemsPrefab == null || itemsPrefab.Length == 0) return;
            if (x < 0 || x >= Row || y < 0 || y >= Col) return;

            int r = Random.Range(0, itemsPrefab.Length);
            if (itemsPrefab[r] == null) return;

            GameObject obj = Instantiate(itemsPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            if (itemPotionParent != null) obj.transform.parent = itemPotionParent;

            if (obj.TryGetComponent<ItemPotion>(out var itemPotion))
            {
                if (mapData != null) mapData[x, y] = potion;
                if (potions == null) potions = new ItemPotion[Row, Col];
                potions[x, y] = itemPotion;
                itemPotion.positionX = x;
                itemPotion.positionY = y;
                itemPotion.mapGenerator = this;
                if (string.IsNullOrEmpty(itemPotion.Name)) itemPotion.Name = "Potion";
                obj.name = $"Item_Potion {x}, {y}";
            }
            else if (obj.TryGetComponent<ItemSword>(out var itemSword))
            {
                if (mapData != null) mapData[x, y] = sword;
                if (swords == null) swords = new ItemSword[Row, Col];
                swords[x, y] = itemSword;
                itemSword.positionX = x;
                itemSword.positionY = y;
                itemSword.mapGenerator = this;
                if (string.IsNullOrEmpty(itemSword.Name)) itemSword.Name = "Sword";
                obj.name = $"Item_Sword {x}, {y}";
            }
            else if (obj.TryGetComponent<Chest>(out var chestComp))
            {
                if (mapData != null) mapData[x, y] = chest;
                if (chests == null) chests = new Chest[Row, Col];
                chests[x, y] = chestComp;
                chestComp.positionX = x;
                chestComp.positionY = y;
                chestComp.mapGenerator = this;
                if (string.IsNullOrEmpty(chestComp.Name)) chestComp.Name = "Chest";
                obj.name = $"Chest {x}, {y}";
            }
            else
            {
                if (index % 2 == 0)
                {
                    if (mapData != null) mapData[x, y] = potion;
                    var potionComp = obj.AddComponent<ItemPotion>();
                    if (potions == null) potions = new ItemPotion[Row, Col];
                    potions[x, y] = potionComp;
                    potionComp.positionX = x;
                    potionComp.positionY = y;
                    potionComp.mapGenerator = this;
                    if (string.IsNullOrEmpty(potionComp.Name)) potionComp.Name = "Potion";
                    obj.name = $"Item_Potion {x}, {y}";
                }
                else
                {
                    if (mapData != null) mapData[x, y] = sword;
                    var swordComp = obj.AddComponent<ItemSword>();
                    if (swords == null) swords = new ItemSword[Row, Col];
                    swords[x, y] = swordComp;
                    swordComp.positionX = x;
                    swordComp.positionY = y;
                    swordComp.mapGenerator = this;
                    if (string.IsNullOrEmpty(swordComp.Name)) swordComp.Name = "Sword";
                    obj.name = $"Item_Sword {x}, {y}";
                }
            }
            spawned.Add(obj);
        }

        public void PlaceDemonWall(int x, int y)
        {
            if (x < 0 || x >= Row || y < 0 || y >= Col) return;

            GameObject[] prefabs = (enemyPrefab != null && enemyPrefab.Length > 0)
                ? enemyPrefab
                : (wallsPrefab != null && wallsPrefab.Length > 0 ? wallsPrefab : null);
            if (prefabs == null || prefabs.Length == 0) return;

            int r = Random.Range(0, prefabs.Length);
            if (prefabs[r] == null) return;

            GameObject obj = Instantiate(prefabs[r], new Vector3(x, y, 0), Quaternion.identity);
            if (wallParent != null) obj.transform.parent = wallParent;

            if (obj.TryGetComponent<Enemy>(out var enemyComp))
            {
                if (mapData != null) mapData[x, y] = enemy;
                if (enemies == null) enemies = new Enemy[Row, Col];
                enemies[x, y] = enemyComp;
                enemyComp.positionX = x;
                enemyComp.positionY = y;
                enemyComp.mapGenerator = this;
                if (string.IsNullOrEmpty(enemyComp.Name)) enemyComp.Name = "Enemy";
                obj.name = $"Enemy {x}, {y}";
            }
            else if (obj.TryGetComponent<Wall>(out var wallComp))
            {
                if (mapData != null) mapData[x, y] = demonWall;
                if (walls == null) walls = new Wall[Row, Col];
                walls[x, y] = wallComp;
                wallComp.positionX = x;
                wallComp.positionY = y;
                wallComp.mapGenerator = this;
                if (string.IsNullOrEmpty(wallComp.Name)) wallComp.Name = "Wall";
                obj.name = $"DemonWall_Wall {x}, {y}";
            }
            else
            {
                if (mapData != null) mapData[x, y] = demonWall;
                var wall = obj.AddComponent<Wall>();
                if (walls == null) walls = new Wall[Row, Col];
                walls[x, y] = wall;
                wall.positionX = x;
                wall.positionY = y;
                wall.mapGenerator = this;
                if (string.IsNullOrEmpty(wall.Name)) wall.Name = "Wall";
                obj.name = $"DemonWall_Wall {x}, {y}";
            }
            spawned.Add(obj);
        }

        public void PlaceEnemy(int x, int y)
        {
            if (x < 0 || x >= Row || y < 0 || y >= Col) return;

            GameObject prefab = null;
            if (enemyPrefab != null)
            {
                foreach (var p in enemyPrefab)
                {
                    if (p != null && p.GetComponent<Enemy>() != null)
                    {
                        prefab = p;
                        break;
                    }
                }
            }

            GameObject obj = prefab != null
                ? Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity)
                : new GameObject("Enemy");
            if (prefab == null) obj.transform.position = new Vector3(x, y, 0);

            if (wallParent != null) obj.transform.parent = wallParent;
            if (mapData != null) mapData[x, y] = enemy;

            var enemyComp = obj.GetComponent<Enemy>();
            if (enemyComp == null) enemyComp = obj.AddComponent<Enemy>();

            enemyComp.positionX = x;
            enemyComp.positionY = y;
            enemyComp.mapGenerator = this;
            if (string.IsNullOrEmpty(enemyComp.Name)) enemyComp.Name = "Enemy";
            if (enemies == null) enemies = new Enemy[Row, Col];
            enemies[x, y] = enemyComp;
            obj.name = $"Enemy {x}, {y}";
            spawned.Add(obj);
        }

        public void PlacePotion(int x, int y)
        {
            if (x < 0 || x >= Row || y < 0 || y >= Col) return;

            GameObject prefab = null;
            if (itemsPrefab != null)
            {
                foreach (var p in itemsPrefab)
                {
                    if (p != null && p.GetComponent<ItemPotion>() != null)
                    {
                        prefab = p;
                        break;
                    }
                }
            }

            GameObject obj = prefab != null
                ? Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity)
                : new GameObject("Potion");
            if (prefab == null) obj.transform.position = new Vector3(x, y, 0);

            if (itemPotionParent != null) obj.transform.parent = itemPotionParent;
            if (mapData != null) mapData[x, y] = potion;

            var itemPotion = obj.GetComponent<ItemPotion>();
            if (itemPotion == null) itemPotion = obj.AddComponent<ItemPotion>();

            itemPotion.positionX = x;
            itemPotion.positionY = y;
            itemPotion.mapGenerator = this;
            if (string.IsNullOrEmpty(itemPotion.Name)) itemPotion.Name = "Potion";
            if (potions == null) potions = new ItemPotion[Row, Col];
            potions[x, y] = itemPotion;
            obj.name = $"Item_Potion {x}, {y}";
            spawned.Add(obj);
        }

        public void PlaceSword(int x, int y)
        {
            if (x < 0 || x >= Row || y < 0 || y >= Col) return;

            GameObject prefab = null;
            if (itemsPrefab != null)
            {
                foreach (var p in itemsPrefab)
                {
                    if (p != null && p.GetComponent<ItemSword>() != null)
                    {
                        prefab = p;
                        break;
                    }
                }
            }

            GameObject obj = prefab != null
                ? Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity)
                : new GameObject("Sword");
            if (prefab == null) obj.transform.position = new Vector3(x, y, 0);

            if (itemPotionParent != null) obj.transform.parent = itemPotionParent;
            if (mapData != null) mapData[x, y] = sword;

            var itemSword = obj.GetComponent<ItemSword>();
            if (itemSword == null) itemSword = obj.AddComponent<ItemSword>();

            itemSword.positionX = x;
            itemSword.positionY = y;
            itemSword.mapGenerator = this;
            if (string.IsNullOrEmpty(itemSword.Name)) itemSword.Name = "Sword";
            if (swords == null) swords = new ItemSword[Row, Col];
            swords[x, y] = itemSword;
            obj.name = $"Item_Sword {x}, {y}";
            spawned.Add(obj);
        }

        public void PlaceChest(int x, int y)
        {
            if (x < 0 || x >= Row || y < 0 || y >= Col) return;

            GameObject prefab = null;
            if (itemsPrefab != null)
            {
                foreach (var p in itemsPrefab)
                {
                    if (p != null && p.GetComponent<Chest>() != null)
                    {
                        prefab = p;
                        break;
                    }
                }
            }

            GameObject obj = prefab != null
                ? Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity)
                : new GameObject("Chest");
            if (prefab == null) obj.transform.position = new Vector3(x, y, 0);

            if (itemPotionParent != null) obj.transform.parent = itemPotionParent;
            if (mapData != null) mapData[x, y] = chest;

            var chestComp = obj.GetComponent<Chest>();
            if (chestComp == null) chestComp = obj.AddComponent<Chest>();

            chestComp.positionX = x;
            chestComp.positionY = y;
            chestComp.mapGenerator = this;
            if (string.IsNullOrEmpty(chestComp.Name)) chestComp.Name = "Chest";
            if (chests == null) chests = new Chest[Row, Col];
            chests[x, y] = chestComp;
            obj.name = $"Chest {x}, {y}";
            spawned.Add(obj);
        }

        public T Spawn<T>(string objectName, int x, int y, GameObject prefab = null) where T : Component
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

            T comp = go.GetComponent<T>();
            if (comp == null)
            {
                comp = go.AddComponent<T>();
            }

            comp.name = objectName;
            if (comp is Identity identity)
            {
                identity.Name = objectName;
                identity.positionX = x;
                identity.positionY = y;
                identity.mapGenerator = this;
            }

            spawned.Add(go);
            return comp;
        }

        public void TrackSpawned(GameObject go)
        {
            if (go != null) spawned.Add(go);
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

        #region Query Methods

        public int GetMapData(int x, int y)
        {
            if (x < 0 || x >= Row || y < 0 || y >= Col || mapData == null)
            {
                return demonWall;
            }
            return mapData[x, y];
        }

        public int GetMapData(float x, float y)
        {
            return GetMapData((int)x, (int)y);
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
            chests = new Chest[MapSize, MapSize];

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

            var theWall = Spawn<Wall>("Wall1", 1, 3);
            theWall.durability = 3;
            walls[1, 3] = theWall;
            mapData[1, 3] = demonWall;

            var theChest = Spawn<Chest>("Chest1", 1, 1);
            chests[1, 1] = theChest;
            mapData[1, 1] = chest;
        }

        #endregion
    }
}
