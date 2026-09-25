using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week05
{
    public class Assignment_Teacher_Week05 : MonoBehaviour, IAssignment
    {
        #region Lecture Variables

        [Header("ข้อ 1: ตัวแปรสำหรับทดสอบ Method (Void & Parameters)")]
        public string testName = "boy";
        public int testAge = 18;
        public string testCountry = "Thailand";

        [Header("ข้อ 2: ตัวแปรสำหรับทดสอบ Method แบบ Return Type")]
        public int numberA = 10;
        public int numberB = 20;
        public string sampleText = "Hello Unity";
        public int sex = 1;

        #endregion

        #region Homework Variables

        [Header("Ex01: ตัวแปรสำหรับสร้างแผนที่ (Refactoring)")]
        public int columns = 3;
        public int rows = 4;
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;
        public int foodCount = 3;
        public GameObject player;
        public GameObject exitTile;

        [Header("Lv01 Variables")]
        public int baseDamage = 50;
        public float damageMultiplier = 1.5f;

        [Header("Lv02 Variables")]
        public int currentMana = 40;
        public int spellManaCost = 25;

        [Header("Lv03 & Lv04 Variables")]
        public int[] testScores = new int[] { 15, 42, 88, 64, 99, 23 };

        [Header("Lv05 Variables")]
        public int currentExp = 120;
        public int requiredExp = 100;

        [Header("Lv06 Variables")]
        public int testHealth = 120;
        public int minHealth = 0;
        public int maxHealth = 100;

        #endregion

        private const string LineSeparator = "============================";

        void Start()
        {
            // =========================================================================
            // Lecture (ข้อ 1 - 2)
            // =========================================================================

            // ข้อ 1: ทดสอบเรียก Method แบบ void และ Parameter (Overloading)
            UserNameIdentification();
            UserNameIdentification(testName);
            UserNameIdentification(testName, testAge);
            UserCountry(testCountry);

            Debug.Log(LineSeparator);

            // ข้อ 2: ทดสอบเรียก Method แบบมีค่าส่งกลับ (Return Type)
            Debug.Log("Add(1, 9) = " + Add(1, 9));
            Debug.Log("GetStringLength(\"hello\") = " + GetStringLength("hello"));
            Debug.Log("ConvertInttoBool(1) = " + ConvertInttoBool(1));

            // =========================================================================
            // Homework (Level 1: Simple & ข้อ 3)
            // =========================================================================

            Debug.Log(LineSeparator);
            Debug.Log("Lv01_CalculateDamage: " + Lv01_CalculateDamage(baseDamage, damageMultiplier));
            Debug.Log("Lv02_CanCastSpell: " + Lv02_CanCastSpell(currentMana, spellManaCost));
            Debug.Log("Lv03_FindHighestScore: " + Lv03_FindHighestScore(testScores));
            Debug.Log("Lv04_CalculateTotalScore: " + Lv04_CalculateTotalScore(testScores));
            Debug.Log("Lv05_CheckLevelUp: " + Lv05_CheckLevelUp(currentExp, requiredExp));
            Debug.Log("Lv06_ClampHealth: " + Lv06_ClampHealth(testHealth, minHealth, maxHealth));

            // สามารถเปิดใช้งานเพื่อสร้างแผนที่เมื่อกำหนด Prefabs ใน Inspector แล้ว:
            // GenerateFloor();
            // GenerateWalls();
            // GenerateFoods();
            // PlacePlayer();
            // PlaceExit();
        }

        void Update()
        {
        }

        #region Lecture

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

        #endregion // End Lecture

        #region Homework

        #region Ex01: แยกโค้ดสร้างแผนที่ออกเป็น Method (Refactoring)

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

        #region Level 1: Simple

        public int Lv01_CalculateDamage(int baseDamage, float multiplier)
        {
            return (int)(baseDamage * multiplier);
        }

        public bool Lv02_CanCastSpell(int currentMana, int manaCost)
        {
            return currentMana >= manaCost;
        }

        public int Lv03_FindHighestScore(int[] scores)
        {
            if (scores == null || scores.Length == 0) return 0;
            int max = scores[0];
            for (int i = 1; i < scores.Length; i++)
            {
                if (scores[i] > max)
                {
                    max = scores[i];
                }
            }
            return max;
        }

        public int Lv04_CalculateTotalScore(int[] scores)
        {
            if (scores == null || scores.Length == 0) return 0;
            int total = 0;
            for (int i = 0; i < scores.Length; i++)
            {
                total += scores[i];
            }
            return total;
        }

        public bool Lv05_CheckLevelUp(int currentExp, int requiredExp)
        {
            return currentExp >= requiredExp;
        }

        public int Lv06_ClampHealth(int currentHealth, int minHealth, int maxHealth)
        {
            if (currentHealth < minHealth) return minHealth;
            if (currentHealth > maxHealth) return maxHealth;
            return currentHealth;
        }

        #endregion

        #endregion // End Homework
    }
}
