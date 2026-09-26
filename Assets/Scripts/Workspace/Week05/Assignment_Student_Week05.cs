using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week05
{
    public class Assignment_Student_Week05 : MonoBehaviour
    {
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

        void Start()
        {
            // เมื่อสร้าง Method ในข้อ 1 เสร็จแล้ว สามารถเปิดคอมเมนต์ด้านล่างเพื่อทดสอบการทำงานได้:
            // UserNameIdentification();
            // UserNameIdentification("boy");
            // UserNameIdentification("big", 18);
            // UserCountry();

            // เมื่อสร้าง Method ในการบ้านเสร็จแล้ว สามารถเปิดคอมเมนต์ด้านล่างเพื่อทดสอบได้:
            // Debug.Log("Lv01_CalculateDamage: " + Lv01_CalculateDamage(baseDamage, damageMultiplier));
            // Debug.Log("Lv02_CanCastSpell: " + Lv02_CanCastSpell(currentMana, spellManaCost));
            // Debug.Log("Lv03_FindHighestScore: " + Lv03_FindHighestScore(testScores));
            // Debug.Log("Lv04_CalculateTotalScore: " + Lv04_CalculateTotalScore(testScores));
            // Debug.Log("Lv05_CheckLevelUp: " + Lv05_CheckLevelUp(currentExp, requiredExp));
            // Debug.Log("Lv06_ClampHealth: " + Lv06_ClampHealth(testHealth, minHealth, maxHealth));

            // หมายเหตุ: โค้ดสร้างแผนที่สำหรับ Ex01 ย้ายไปอยู่ในไฟล์ "MapGenerator.cs" ให้นักเรียนเปิดทำในไฟล์นั้น
        }

        #region Lecture

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
        // สร้าง Method ชื่อ GetGreeting
        // - รับพารามิเตอร์ string name และมี Return Type เป็น string
        // - นำคำว่า "Hello, " ไปต่อกับ name แล้ว return ค่านั้นกลับไป (เช่น "Hello, " + name)

        // Guideline ข้อ 2.3:
        // สร้าง Method ชื่อ ConvertInttoBool
        // - รับพารามิเตอร์ int sex และมี Return Type เป็น bool
        // - ถ้า sex เท่ากับ 1 ให้ return true นอกนั้น return false

        #endregion

        #endregion // End Lecture

        #region Homework

        // =========================================================================================
        // 🏠 Ex01: แยกโค้ดสร้างแผนที่ออกเป็น Method (Refactoring)
        // ให้นักเรียนเปิดอ่านโจทย์และเขียนโค้ดทำในไฟล์ "MapGenerator.cs"
        // =========================================================================================

        #region Level 1: Simple (โจทย์การบ้าน Method พื้นฐาน & ระบบเกม)

        // Guideline Lv01:
        // สร้าง Method ชื่อ Lv01_CalculateDamage
        // - รับพารามิเตอร์ int baseDamage, float multiplier และมี Return Type เป็น int
        // - คำนวณพลังโจมตีโดยเอา baseDamage คูณกับ multiplier แล้วแปลงเป็น int (เช่น (int)(baseDamage * multiplier)) จากนั้น return ค่านั้นกลับไป

        // Guideline Lv02:
        // สร้าง Method ชื่อ Lv02_CanCastSpell
        // - รับพารามิเตอร์ int currentMana, int manaCost และมี Return Type เป็น bool
        // - เช็คว่า currentMana มากกว่าหรือเท่ากับ manaCost หรือไม่ ถ้าใช่ return true ไม่ใช่ return false

        // Guideline Lv03:
        // สร้าง Method ชื่อ Lv03_FindHighestScore
        // - รับพารามิเตอร์ int[] scores และมี Return Type เป็น int
        // - ค้นหาคะแนนที่สูงที่สุดใน Array scores แล้ว return ค่านั้นกลับไป (หาก Array ว่างหรือเป็น null ให้ return 0)

        // Guideline Lv04:
        // สร้าง Method ชื่อ Lv04_CalculateTotalScore
        // - รับพารามิเตอร์ int[] scores และมี Return Type เป็น int
        // - วนลูปหาผลรวมของคะแนนทั้งหมดใน Array scores แล้ว return ผลรวมนั้นกลับไป (หาก Array ว่างหรือเป็น null ให้ return 0)

        // Guideline Lv05:
        // สร้าง Method ชื่อ Lv05_CheckLevelUp
        // - รับพารามิเตอร์ int currentExp, int requiredExp และมี Return Type เป็น bool
        // - เช็คว่า currentExp มากกว่าหรือเท่ากับ requiredExp หรือไม่ ถ้าใช่ return true ไม่ใช่ return false

        // Guideline Lv06:
        // สร้าง Method ชื่อ Lv06_ClampHealth
        // - รับพารามิเตอร์ int currentHealth, int minHealth, int maxHealth และมี Return Type เป็น int
        // - จำกัดค่าพลังชีวิตไม่ให้ต่ำกว่า minHealth และไม่ให้เกิน maxHealth แล้ว return ค่านั้นกลับไป
        //   (เช่น ถ้า currentHealth < minHealth ให้ return minHealth, ถ้า currentHealth > maxHealth ให้ return maxHealth, นอกนั้น return currentHealth)

        #endregion

        #endregion // End Homework

    }
}
