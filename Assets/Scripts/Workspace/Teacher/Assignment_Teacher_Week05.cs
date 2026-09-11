using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week05
{
    public class Assignment_Teacher_Week05 : MonoBehaviour, IAssignment
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

        [Header("ข้อ 4-7: ตัวแปรของตัวละคร")]
        public int energy = 20;

        #region ข้อ 1: Method แบบ void และ Parameter (Overloading)

        public void UserNameIdentification()
        {
            Debug.Log("user name is UntitleUser");
        }

        public void UserNameIdentification(string name)
        {
            Debug.Log("user name is " + name);
        }

        public void UserNameIdentification(string name, int age)
        {
            Debug.Log("user name is " + name + " age is " + age);
        }

        public void UserCountry(string country = "Thailand")
        {
            Debug.Log(country);
        }

        #endregion

        #region ข้อ 2: Method แบบมีค่าส่งกลับ (Return Type)

        public int Add(int a, int b)
        {
            int c = a + b;
            return c;
        }

        public int GetStringLength(string text)
        {
            return text.Length;
        }

        public bool ConvertInttoBool(int sex)
        {
            return sex == 1;
        }

        #endregion

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

        #region ข้อ 4-7: Method ของตัวละคร

        public void Move(Vector2 direction)
        {
            transform.position += new Vector3(direction.x, direction.y, 0f);
            energy -= 1;
        }

        public void TakeDamage(int Damage)
        {
            energy -= Damage;
            if (energy < 0)
            {
                energy = 0;
            }

            Debug.Log("Current Energy : " + energy);
            CheckDead();
        }

        private void CheckDead()
        {
            if (energy <= 0)
            {
                Debug.Log("You Lose");
            }
        }

        public void Heal(int healPoint)
        {
            energy += healPoint;
        }

        #endregion
    }
}
