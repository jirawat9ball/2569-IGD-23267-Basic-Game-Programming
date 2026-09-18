using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week05
{
    public class Assignment_Student_Week05 : MonoBehaviour
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
            // เมื่อสร้าง Method ในข้อ 1 เสร็จแล้ว สามารถเปิดคอมเมนต์ด้านล่างเพื่อทดสอบการทำงานได้:
            // UserNameIdentification();
            // UserNameIdentification("boy");
            // UserNameIdentification("big", 18);
            // UserCountry();
        }

        #region ข้อ 1: Method แบบ void และ Parameter (Overloading)

        // Guideline ข้อ 1.1:
        // สร้าง Method ชื่อ UserNameIdentification
        // - ไม่รับ Parameter ใดๆ (วงเล็บว่าง) และไม่ส่งค่ากลับ (void)
        // - พิมพ์ข้อความ "user name is UntitleUser" ออกมาผ่าน Debug.Log

        // Guideline ข้อ 1.2:
        // สร้าง Method ชื่อ UserNameIdentification
        // - เมธอดชื่อเดิม แต่รับพารามิเตอร์ string name เพิ่มเข้ามา (เรียกว่า Overloading)
        // - พิมพ์ "user name is " ต่อด้วยค่า name ออกมาผ่าน Debug.Log

        // Guideline ข้อ 1.3:
        // สร้าง Method ชื่อ UserNameIdentification
        // - เมธอดชื่อเดิม แต่รับ 2 พารามิเตอร์คือ string name และ int age
        // - พิมพ์ "user name is " + name + " age is " + age ออกมาผ่าน Debug.Log

        // Guideline ข้อ 1.4:
        // สร้าง Method ชื่อ UserCountry
        // - รับพารามิเตอร์ string country และกำหนดค่าเริ่มต้น (default value) เป็น "Thailand"
        //   เช่น (string country = "Thailand")
        // - พิมพ์ค่าของ country ออกมาผ่าน Debug.Log

        #endregion

        #region ข้อ 2: Method แบบมีค่าส่งกลับ (Return Type)

        // Guideline ข้อ 2.1:
        // สร้าง Method ชื่อ Add
        // - รับพารามิเตอร์ int a, int b และมี Return Type เป็น int
        // - บวก a กับ b แล้ว return ผลลัพธ์กลับไป (เมธอดนี้ไม่ต้องพิมพ์อะไรออก console)

        // Guideline ข้อ 2.2:
        // สร้าง Method ชื่อ GetStringLength
        // - รับพารามิเตอร์ string text และมี Return Type เป็น int
        // - หาจำนวนตัวอักษรของ text ด้วย text.Length แล้ว return ค่านั้นกลับไป

        // Guideline ข้อ 2.3:
        // สร้าง Method ชื่อ ConvertInttoBool
        // - รับพารามิเตอร์ int sex และมี Return Type เป็น bool
        // - ถ้า sex เท่ากับ 1 ให้ return true นอกนั้น return false

        #endregion

        #region ข้อ 3: แยกโค้ดสร้างแผนที่ออกเป็น Method (Refactoring)

        // Guideline ข้อ 3.1:
        // สร้าง Method ชื่อ GenerateFloor แบบ void ไม่รับพารามิเตอร์
        // - ใช้ Nested Loop วน y ตั้งแต่ 0 ถึง rows-1 และ x ตั้งแต่ 0 ถึง columns-1
        // - แต่ละช่องสุ่มพื้นจาก floorTiles ด้วย Random.Range
        // - Instantiate พื้นที่ตำแหน่ง (x, y) ด้วย Quaternion.identity

        // Guideline ข้อ 3.2:
        // สร้าง Method ชื่อ GenerateWalls แบบ void ไม่รับพารามิเตอร์
        // - วน x ตั้งแต่ -1 ถึง columns และ y ตั้งแต่ -1 ถึง rows (ขยายออกไปด้านละ 1 ช่องเพื่อทำขอบ)
        // - สร้างกำแพงเฉพาะช่องที่อยู่ขอบนอกเท่านั้น (เงื่อนไขขอบคือ x == -1 || x == columns || y == -1 || y == rows)
        // - สุ่มกำแพงจาก wallTiles แล้ว Instantiate ที่ตำแหน่งนั้น

        // Guideline ข้อ 3.3:
        // สร้าง Method ชื่อ GenerateFoods แบบ void ไม่รับพารามิเตอร์
        // - วนลูปจำนวน foodCount รอบ
        // - แต่ละรอบสุ่มอาหารจาก foodTiles และสุ่มตำแหน่ง x (0 ถึง columns-1), y (0 ถึง rows-1)
        // - Instantiate อาหารที่ตำแหน่งที่สุ่มได้

        // Guideline ข้อ 3.4:
        // สร้าง Method ชื่อ PlacePlayer แบบ void ไม่รับพารามิเตอร์
        // - Instantiate ตัวละคร player ไว้ที่มุมซ้ายล่างของแผนที่ คือตำแหน่ง (0, 0)

        // Guideline ข้อ 3.5:
        // สร้าง Method ชื่อ PlaceExit แบบ void ไม่รับพารามิเตอร์
        // - Instantiate ทางออก exitTile ไว้ที่มุมขวาบนของแผนที่ คือตำแหน่ง (columns-1, rows-1)

        #endregion

        #region ข้อ 4-7: Method ของตัวละคร

        // Guideline ข้อ 4:
        // สร้าง Method ชื่อ Move แบบ void รับพารามิเตอร์ Vector2 direction
        // - บวกทิศทางที่รับมาเข้ากับ transform.position ของตัวละคร
        //   (แปลง Vector2 เป็น Vector3 ก่อน เช่น new Vector3(direction.x, direction.y, 0))
        // - ทุกครั้งที่เดิน ให้ลด energy ลง 1

        // Guideline ข้อ 5:
        // สร้าง Method ชื่อ TakeDamage แบบ void รับพารามิเตอร์ int Damage
        // - ลด energy ลงตามค่า Damage ที่รับเข้ามา
        // - ห้ามให้ energy ติดลบ ถ้าน้อยกว่า 0 ให้ตั้งเป็น 0
        // - พิมพ์ "Current Energy : " ตามด้วยค่า energy ปัจจุบัน ผ่าน Debug.Log
        // - เรียก CheckDead() เพื่อตรวจว่าตัวละครตายหรือยัง

        // Guideline ข้อ 6:
        // สร้าง Method ชื่อ CheckDead แบบ private void ไม่รับพารามิเตอร์
        // - Method นี้เป็น private แปลว่าเรียกใช้ได้เฉพาะภายในคลาสนี้
        // - ถ้า energy น้อยกว่าหรือเท่ากับ 0 ให้พิมพ์ "You Lose" ผ่าน Debug.Log

        // Guideline ข้อ 7:
        // สร้าง Method ชื่อ Heal แบบ void รับพารามิเตอร์ int healPoint
        // - เพิ่มค่า energy ขึ้นตามค่า healPoint ที่รับเข้ามา

        #endregion
    }
}
