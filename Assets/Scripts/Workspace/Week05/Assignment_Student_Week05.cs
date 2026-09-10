using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week05
{
    public class Assignment_Student_Week05 : MonoBehaviour, IAssignment
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

        void Start()
        {
            UserNameIdentification();
            UserNameIdentification("boy");
            UserNameIdentification("big", 18);
            UserCountry();
        }

        #region ข้อ 1: Method แบบ void และ Parameter (Overloading)

        public void UserNameIdentification()
        {
            // Guideline:
            // 1. Method นี้ไม่รับพารามิเตอร์
            // 2. พิมพ์ข้อความ "user name is UntitleUser" ออกมา
            Debug.Log("user name is UntitleUser");
        }

        public void UserNameIdentification(string name)
        {
            // Guideline:
            // 1. Method ชื่อเดิม แต่รับพารามิเตอร์ string name เพิ่มเข้ามา (เรียกว่า Overloading)
            // 2. พิมพ์ "user name is " ต่อด้วยค่า name
            Debug.Log("user name is " + name);
        }

        public void UserNameIdentification(string name, int age)
        {
            // Guideline:
            // 1. Method ชื่อเดิม แต่รับ 2 พารามิเตอร์คือ name และ age
            // 2. พิมพ์ "user name is " + name + " age is " + age
            Debug.Log("user name is " + name + " age is " + age);
        }

        public void UserCountry(string country = "Thailand")
        {
            // Guideline:
            // 1. กำหนดค่าเริ่มต้น (default value) ให้พารามิเตอร์เป็น "Thailand"
            //    ถ้าเรียกใช้โดยไม่ใส่ค่า จะได้ "Thailand" อัตโนมัติ
            // 2. พิมพ์ค่าของ country ออกมา
            Debug.Log(country);
        }

        #endregion

        #region ข้อ 2: Method แบบมีค่าส่งกลับ (Return Type)

        public int Add(int a, int b)
        {
            // Guideline:
            // 1. Return Type เป็น int แปลว่าต้อง return ค่าตัวเลขกลับไป
            // 2. บวก a กับ b แล้ว return ผลลัพธ์ (Method นี้ไม่ต้องพิมพ์อะไร)
            int c = a + b;
            return c;
        }

        public int GetStringLength(string text)
        {
            // Guideline:
            // 1. หาจำนวนตัวอักษรของ text ด้วย text.Length
            // 2. return ค่านั้นกลับไป
            return text.Length;
        }

        public bool ConvertInttoBool(int sex)
        {
            // Guideline:
            // 1. Return Type เป็น bool (จริง/เท็จ)
            // 2. ถ้า sex เท่ากับ 1 ให้ return true นอกนั้น return false
            return sex == 1;
        }

        #endregion

        #region ข้อ 3: แยกโค้ดสร้างแผนที่ออกเป็น Method (Refactoring)

        public void GenerateFloor()
        {
            // Guideline:
            // 1. ใช้ Nested Loop วน y ตั้งแต่ 0 ถึง rows-1 และ x ตั้งแต่ 0 ถึง columns-1
            // 2. แต่ละช่องสุ่มพื้นจาก floorTiles ด้วย Random.Range
            // 3. Instantiate พื้นที่ตำแหน่ง (x, y) ด้วย Quaternion.identity
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
            // Guideline:
            // 1. วน x ตั้งแต่ -1 ถึง columns และ y ตั้งแต่ -1 ถึง rows (ขยายออกไปด้านละ 1 ช่องเพื่อทำขอบ)
            // 2. สร้างกำแพงเฉพาะช่องที่อยู่ขอบนอกเท่านั้น
            //    เงื่อนไขขอบคือ x == -1 || x == columns || y == -1 || y == rows
            // 3. สุ่มกำแพงจาก wallTiles แล้ว Instantiate ที่ตำแหน่งนั้น
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
            // Guideline:
            // 1. วนลูปจำนวน foodCount รอบ
            // 2. แต่ละรอบสุ่มอาหารจาก foodTiles และสุ่มตำแหน่ง x (0 ถึง columns-1), y (0 ถึง rows-1)
            // 3. Instantiate อาหารที่ตำแหน่งที่สุ่มได้
            for (int i = 0; i < foodCount; i++)
            {
                GameObject toInstantiate = foodTiles[Random.Range(0, foodTiles.Length)];
                Vector2 position = new Vector2(Random.Range(0, columns), Random.Range(0, rows));
                Instantiate(toInstantiate, position, Quaternion.identity);
            }
        }

        public void PlacePlayer()
        {
            // Guideline:
            // Instantiate ตัวละคร player ไว้ที่มุมซ้ายล่างของแผนที่ คือตำแหน่ง (0, 0)
            Instantiate(player, new Vector2(0, 0), Quaternion.identity);
        }

        public void PlaceExit()
        {
            // Guideline:
            // Instantiate ทางออก exitTile ไว้ที่มุมขวาบนของแผนที่ คือตำแหน่ง (columns-1, rows-1)
            Instantiate(exitTile, new Vector2(columns - 1, rows - 1), Quaternion.identity);
        }

        #endregion

        #region ข้อ 4-7: Method ของตัวละคร

        public void Move(Vector2 direction)
        {
            // Guideline:
            // 1. บวกทิศทางที่รับมาเข้ากับ transform.position ของตัวละคร
            //    (แปลง Vector2 เป็น Vector3 ก่อน เช่น new Vector3(direction.x, direction.y, 0))
            // 2. ทุกครั้งที่เดิน ให้ลด energy ลง 1
            transform.position += new Vector3(direction.x, direction.y, 0f);
            energy -= 1;
        }

        public void TakeDamage(int Damage)
        {
            // Guideline:
            // 1. ลด energy ลงตามค่า Damage ที่รับเข้ามา
            // 2. ห้ามให้ energy ติดลบ ถ้าน้อยกว่า 0 ให้ตั้งเป็น 0
            // 3. พิมพ์ "Current Energy : " ตามด้วยค่า energy ปัจจุบัน
            // 4. เรียก CheckDead() เพื่อตรวจว่าตัวละครตายหรือยัง
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
            // Guideline:
            // 1. Method นี้เป็น private แปลว่าเรียกใช้ได้เฉพาะภายในคลาสนี้
            // 2. ถ้า energy น้อยกว่าหรือเท่ากับ 0 ให้พิมพ์ "You Lose"
            if (energy <= 0)
            {
                Debug.Log("You Lose");
            }
        }

        public void Heal(int healPoint)
        {
            // Guideline:
            // เพิ่มค่า energy ขึ้นตามค่า healPoint ที่รับเข้ามา
            energy += healPoint;
        }

        #endregion
    }
}
