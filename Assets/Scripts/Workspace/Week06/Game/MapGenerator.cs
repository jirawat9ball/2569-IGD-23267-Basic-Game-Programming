using System.Collections.Generic;
using UnityEngine;

namespace Week06.Game
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

        public string[,] mapdata;

        public Wall[,] walls;
        public ItemPotion[,] potions;

        // Block types
        [HideInInspector]
        public string empty = "";
        [HideInInspector]
        public string demonWall = "demonWall";
        [HideInInspector]
        public string potion = "potion";
        [HideInInspector]
        public string bonuesPotion = "bonuesPotion";
        [HideInInspector]
        public string exit = "exit";
        [HideInInspector]
        public string playerOnMap = "player";

        #region Unity Lifecycle

        void Start()
        {
            // ป้องกัน IndexOutOfRangeException กรณี Row/Col ใน Inspector เป็น 0
            if (Row <= 0) Row = 10;
            if (Col <= 0) Col = 10;

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
            mapdata = new string[Row, Col];

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
            if (player == null) return;

            // ตรวจสอบพิกัดเริ่มต้นให้อยู่ในขอบเขตแผนที่
            if (playerStartPos.x < 0 || playerStartPos.x >= Row || playerStartPos.y < 0 || playerStartPos.y >= Col)
            {
                playerStartPos = Vector2Int.zero;
            }

            // สร้างตัวผู้เล่นขึ้นมาในฉาก
            Player spawnedPlayer = Instantiate(player, new Vector3(playerStartPos.x, playerStartPos.y, -0.1f), Quaternion.identity);
            spawnedPlayer.name = "Player";
            spawnedPlayer.Name = "Player";
            spawnedPlayer.mapGenerator = this;
            spawnedPlayer.positionX = playerStartPos.x;
            spawnedPlayer.positionY = playerStartPos.y;

            // อ้างอิงตัวผู้เล่นที่สร้างขึ้นมาในฉาก
            player = spawnedPlayer;

            if (mapdata != null && playerStartPos.x < Row && playerStartPos.y < Col)
            {
                mapdata[playerStartPos.x, playerStartPos.y] = playerOnMap;
            }
        }

        /// <summary>สุ่มวางสิ่งกีดขวาง (DemonWall) ตามจำนวน obsatcleCount</summary>
        public void GenerateObstacles()
        {
            walls = new Wall[Row, Col];

            if (enemyPrefab == null || enemyPrefab.Length == 0 || obsatcleCount <= 0) return;

            int count = 0;
            int preventInfiniteLoop = 100;

            while (count < obsatcleCount && --preventInfiniteLoop >= 0)
            {
                int x = Random.Range(0, Row);
                int y = Random.Range(0, Col);

                if (mapdata != null && mapdata[x, y] == empty)
                {
                    PlaceDemonWall(x, y);
                    count++;
                }
            }
        }

        /// <summary>สุ่มวางไอเทมฟื้นฟูเลือดตามจำนวน itemPotionCount</summary>
        public void GenerateItems()
        {
            potions = new ItemPotion[Row, Col];

            if (itemsPrefab == null || itemsPrefab.Length == 0 || itemPotionCount <= 0) return;

            int count = 0;
            int preventInfiniteLoop = 100;

            while (count < itemPotionCount && --preventInfiniteLoop >= 0)
            {
                int x = Random.Range(0, Row);
                int y = Random.Range(0, Col);

                if (mapdata != null && mapdata[x, y] == empty)
                {
                    PlaceItem(x, y);
                    count++;
                }
            }
        }

        /// <summary>สร้างและวางทางออก (Instantiate) ที่มุมขวาบนของแผนที่</summary>
        public void PlaceExit()
        {
            if (Row <= 0 || Col <= 0 || mapdata == null || Exit == null) return;

            int exitX = Row - 1;
            int exitY = Col - 1;
            mapdata[exitX, exitY] = exit;

            Exit spawnedExit = Instantiate(Exit, new Vector3(exitX, exitY, 0), Quaternion.identity);
            spawnedExit.name = "Exit";
            spawnedExit.positionX = exitX;
            spawnedExit.positionY = exitY;
            spawnedExit.mapGenerator = this;
            Exit = spawnedExit;
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

            if (mapdata != null && x >= 0 && x < Row && y >= 0 && y < Col)
            {
                mapdata[x, y] = empty;
            }
        }

        public void PlaceItem(int x, int y)
        {
            if (itemsPrefab == null || itemsPrefab.Length == 0) return;
            if (x < 0 || x >= Row || y < 0 || y >= Col) return;

            int r = Random.Range(0, itemsPrefab.Length);
            if (itemsPrefab[r] == null) return;

            GameObject obj = Instantiate(itemsPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            if (itemPotionParent != null) obj.transform.parent = itemPotionParent;
            if (mapdata != null) mapdata[x, y] = potion;

            var itemPotion = obj.GetComponent<ItemPotion>();
            if (itemPotion != null)
            {
                itemPotion.positionX = x;
                itemPotion.positionY = y;
                itemPotion.mapGenerator = this;
                if (potions != null) potions[x, y] = itemPotion;
                obj.name = $"Item_{itemPotion.Name} {x}, {y}";
            }
        }

        public void PlaceDemonWall(int x, int y)
        {
            if (enemyPrefab == null || enemyPrefab.Length == 0) return;
            if (x < 0 || x >= Row || y < 0 || y >= Col) return;

            int r = Random.Range(0, enemyPrefab.Length);
            if (enemyPrefab[r] == null) return;

            GameObject obj = Instantiate(enemyPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            if (wallParent != null) obj.transform.parent = wallParent;
            if (mapdata != null) mapdata[x, y] = demonWall;

            var wall = obj.GetComponent<Wall>();
            if (wall != null)
            {
                wall.positionX = x;
                wall.positionY = y;
                wall.mapGenerator = this;
                if (walls != null) walls[x, y] = wall;
                obj.name = $"DemonWall_{wall.Name} {x}, {y}";
            }
        }

        #endregion

        #region Query Methods

        public string GetMapData(float x, float y)
        {
            if (mapdata == null || x >= Row || x < 0 || y >= Col || y < 0) return "invalid";
            return mapdata[(int)x, (int)y];
        }

        #endregion
    }
}
