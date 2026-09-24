using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week05
{
    public class MapGenerator_Teacher_Week05 : MonoBehaviour
    {
        [Header("ข้อ 3: ตัวแปรสำหรับสร้างแผนที่")]
        public int columns = 3;
        public int rows = 4;
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;
        public int foodCount = 3;
        public GameObject player;
        public GameObject exitTile;

        void Start()
        {
            GenerateMap();
        }

        public void GenerateMap()
        {
            GenerateFloor();
            GenerateWalls();
            GenerateFoods();
            PlacePlayer();
            PlaceExit();
        }

        #region ข้อ 3: แยกโค้ดสร้างแผนที่ออกเป็น Method (Refactoring)

        public void GenerateFloor()
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
                }
            }
        }

        public void GenerateWalls()
        {
            for (int y = -1; y <= rows; y++)
            {
                for (int x = -1; x <= columns; x++)
                {
                    if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        GameObject toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
                        Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
                    }
                }
            }
        }

        public void GenerateFoods()
        {
            for (int i = 0; i < foodCount; i++)
            {
                GameObject toInstantiate = foodTiles[Random.Range(0, foodTiles.Length)];
                Vector2 position = new Vector2(Random.Range(0, columns), Random.Range(0, rows));
                Instantiate(toInstantiate, position, Quaternion.identity);
            }
        }

        public void PlacePlayer()
        {
            Instantiate(player, new Vector2(0, 0), Quaternion.identity);
        }

        public void PlaceExit()
        {
            Instantiate(exitTile, new Vector2(columns - 1, rows - 1), Quaternion.identity);
        }

        #endregion
    }
}
