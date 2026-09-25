using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week05
{
    public class Player : MonoBehaviour
    {
        [Header("ข้อ 3-7: ตัวแปรของตัวละคร")]
        public int energy = 20;

        private void Update()
        {
            
        }

        #region ข้อ 3-7: Method ของตัวละคร

        // Guideline ข้อ 3: (Method แบบ void + Parameter)
        // สร้าง Method ชื่อ Move แบบ public void รับพารามิเตอร์ Vector2 direction
        // - บวกทิศทางที่รับมาเข้ากับ transform.position ของตัวละคร
        //   (แปลง Vector2 เป็น Vector3 ก่อน เช่น new Vector3(direction.x, direction.y, 0))
        // - ทุกครั้งที่เดิน ให้ลด energy ลง 1

        // Guideline ข้อ 4: (Method แบบ void + Parameter)
        // สร้าง Method ชื่อ TakeDamage แบบ public void รับพารามิเตอร์ int Damage
        // - ลด energy ลงตามค่า Damage ที่รับเข้ามา
        // - ห้ามให้ energy ติดลบ ถ้าน้อยกว่า 0 ให้ตั้งเป็น 0
        // - พิมพ์ "Current Energy : " ตามด้วยค่า energy ปัจจุบัน ผ่าน Debug.Log
        // - เรียก CheckDead() เพื่อตรวจว่าตัวละครตายหรือยัง

        // Guideline ข้อ 5: (Method Scope แบบ private void ไม่รับพารามิเตอร์)
        // สร้าง Method ชื่อ CheckDead แบบ private void ไม่รับพารามิเตอร์
        // - Method นี้เป็น private แปลว่าเรียกใช้ได้เฉพาะภายในคลาสนี้
        // - ถ้า energy น้อยกว่าหรือเท่ากับ 0 ให้พิมพ์ "You Lose" ผ่าน Debug.Log

        // Guideline ข้อ 6: (Method แบบ Default Parameter)
        // สร้าง Method ชื่อ Heal แบบ public void รับพารามิเตอร์ int healPoint ที่มีค่าเริ่มต้นเป็น 10 (int healPoint = 10)
        // - เพิ่มค่า energy ขึ้นตามค่า healPoint ที่รับเข้ามา

        // Guideline ข้อ 7: (Method แบบมีค่าส่งกลับ Return Type bool)
        // สร้าง Method ชื่อ CanMove แบบ public bool ไม่รับพารามิเตอร์
        // - ถ้า energy มากกว่า 0 ให้ return true นอกนั้นให้ return false

        #endregion
    }
}
