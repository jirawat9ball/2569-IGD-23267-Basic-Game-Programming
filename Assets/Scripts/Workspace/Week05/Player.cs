using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week05
{
    public class Player : MonoBehaviour
    {
        [Header("ตัวแปรของตัวละคร")]
        public int energy = 20;

        [Header("ขอบเขตแผนที่")]
        public int columns = 8;
        public int rows = 8;

        private void Start()
        {
            // เมื่อสร้าง Method เสร็จแล้ว สามารถเปิดคอมเมนต์ด้านล่างเพื่อทดสอบการทำงานได้:
            // ข้อ 1: ทดสอบเรียก Method แบบ void, Parameter, Overloading และ Default Parameter
            // Move(Vector2.right);
            // Move(0f, 1f);
            // TakeDamage(5);
            // TakeDamage(3, "Slime");
            // Heal();
            // Heal(5);

            // ข้อ 2: ทดสอบเรียก Method แบบมีค่าส่งกลับ (Return Type)
            // Debug.Log("GetEnergy() = " + GetEnergy());
            // Debug.Log("GetStatus() = " + GetStatus());
            // Debug.Log("CanMove(Vector2.right) = " + CanMove(Vector2.right));
        }

        private void Update()
        {
            
        }

        #region ข้อ 1: Method แบบ void, Parameter, Overloading และ Default Parameter

        // Guideline ข้อ 3: (Method แบบ void + Parameter)
        // สร้าง Method ชื่อ Move แบบ public void รับพารามิเตอร์ Vector2 direction
        // - เรียกใช้ CanMove(direction) เพื่อตรวจว่าสามารถเดินได้หรือไม่ ถ้าเดินไม่ได้ (false) ให้ return ออกไปทันที
        // - ถ้าเดินได้ ให้บวกทิศทาง direction เข้ากับ transform.position ของตัวละคร
        //   (แปลง Vector2 เป็น Vector3 ก่อน เช่น new Vector3(direction.x, direction.y, 0))
        // - ลด energy ลง 1

        // Guideline ข้อ 3.1: (Method Overloading)
        // สร้าง Method ชื่อ Move แบบ public void รับพารามิเตอร์ float x, float y
        // - เรียกใช้ Move(new Vector2(x, y)) เพื่อขยับตัวละคร

        // Guideline ข้อ 4: (Method แบบ void + Parameter)
        // สร้าง Method ชื่อ TakeDamage แบบ public void รับพารามิเตอร์ int Damage
        // - ลด energy ลงตามค่า Damage ที่รับเข้ามา
        // - ห้ามให้ energy ติดลบ ถ้าน้อยกว่า 0 ให้ตั้งเป็น 0
        // - พิมพ์ "Current Energy : " ตามด้วยค่า energy ปัจจุบัน ผ่าน Debug.Log
        // - เรียก CheckDead() เพื่อตรวจว่าตัวละครตายหรือยัง

        // Guideline ข้อ 4.1: (Method Overloading)
        // สร้าง Method ชื่อ TakeDamage แบบ public void รับพารามิเตอร์ int Damage, string attacker
        // - พิมพ์ "Attacked by " ต่อด้วยชื่อ attacker ผ่าน Debug.Log
        // - เรียกใช้ TakeDamage(Damage) เพื่อลด energy

        // Guideline ข้อ 5: (Method Scope แบบ private void ไม่รับพารามิเตอร์)
        // สร้าง Method ชื่อ CheckDead แบบ private void ไม่รับพารามิเตอร์
        // - Method นี้เป็น private แปลว่าเรียกใช้ได้เฉพาะภายในคลาสนี้
        // - ถ้า energy น้อยกว่าหรือเท่ากับ 0 ให้พิมพ์ "You Lose" ผ่าน Debug.Log

        // Guideline ข้อ 6: (Method แบบ Default Parameter)
        // สร้าง Method ชื่อ Heal แบบ public void รับพารามิเตอร์ int healPoint ที่มีค่าเริ่มต้นเป็น 10 (int healPoint = 10)
        // - เพิ่มค่า energy ขึ้นตามค่า healPoint ที่รับเข้ามา

        #endregion

        #region ข้อ 2: Method แบบมีค่าส่งกลับ (Return Type)

        // Guideline ข้อ 7: (Method แบบมีค่าส่งกลับ Return Type bool)
        // สร้าง Method ชื่อ CanMove แบบ public bool รับพารามิเตอร์ Vector2 direction
        // - คำนวณตำแหน่งถัดไปโดยนำ transform.position บวกกับ direction
        // - ตรวจสอบว่าตำแหน่งถัดไปอยู่ในขอบเขตแผนที่หรือไม่:
        //   - แกน x ต้องมีค่าตั้งแต่ 0 ถึง columns - 1 (targetPos.x >= 0 && targetPos.x < columns)
        //   - แกน y ต้องมีค่าตั้งแต่ 0 ถึง rows - 1 (targetPos.y >= 0 && targetPos.y < rows)
        // - ถ้าอยู่ในแผนที่ให้ return true นอกนั้น return false

        // Guideline ข้อ 8: (Method แบบมีค่าส่งกลับ Return Type int)
        // สร้าง Method ชื่อ GetEnergy แบบ public int ไม่รับพารามิเตอร์
        // - return ค่า energy ของตัวละครกลับไป

        // Guideline ข้อ 9: (Method แบบมีค่าส่งกลับ Return Type string)
        // สร้าง Method ชื่อ GetStatus แบบ public string ไม่รับพารามิเตอร์
        // - return ข้อความ "Player Energy: " ต่อด้วยค่า energy กลับไป (เช่น "Player Energy: " + energy)

        #endregion
    }
}
