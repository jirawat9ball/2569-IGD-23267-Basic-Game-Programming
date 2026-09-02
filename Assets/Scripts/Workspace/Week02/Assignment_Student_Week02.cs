using UnityEngine;

namespace Week02
{
    public class Assignment_Student_Week02 : MonoBehaviour, IAssignment
    {
        [Header("As01 Variables")]
        public bool isSixoClock;

        [Header("As02 Variables")]
        public string stringPassword;

        [Header("As03 & As04 Variables")]
        public int comparisonNumber;

        [Header("As05 & As06 Variables")]
        public int guessingNumber;
        public int randomNumber;

        [Header("As07 Variables")]
        public string username;
        public string identityPassword;
        public int age;
        public bool isPaid;

        void Start()
        {
            // สามารถเปิด-ปิด คอมเมนต์เพื่อทดสอบทีละข้อได้
            As01_SyntaxIf(isSixoClock);
            As02_StringComparisonExample(stringPassword);
            As03_NumberComparisonExample(comparisonNumber);
            As04_AndOrOperatorExample(comparisonNumber);
        }

        void Update()
        {
            // กดปุ่ม Enter (Return) เพื่อรันข้อ 5-7
            if (Input.GetKeyDown(KeyCode.Return))
            {
                As05_GuessingNumberExample(guessingNumber, randomNumber);
                As06_GuessingNumberMoreOrLessExample(guessingNumber, randomNumber);
                As07_VerifyIdentityExample(username, identityPassword, age, isPaid);
            }
        }

        #region Examples

        public void As01_SyntaxIf(bool isSixoClock)
        { 
            // Guideline: ทดสอบการเขียน if เบื้องต้น เช็คเงื่อนไข isSixoClock ว่าเป็นจริงหรือไม่
            // ถ้าเป็นจริงให้พิมพ์ "You can get in" และให้มีพิมพ์ "Crack Crack!!!!" ออกมาเสมอ
        }

        public void As02_StringComparisonExample(string password)
        {
            // Guideline: ทดสอบการเปรียบเทียบ String โดยใช้ if ตรวจสอบ password
            // ถ้าไม่เท่ากับ "Moon" ให้พิมพ์ "wrong password"
            // ถ้าเท่ากับ "Moon" ให้พิมพ์ "password is correct"
        }

        public void As03_NumberComparisonExample(int number)
        {
            // Guideline: ทดสอบการเปรียบเทียบตัวเลขด้วย if แบบต่างๆ (>, <, ==, >=, <=, !=)
            // ให้ตรวจสอบ number เทียบกับ 10 และพิมพ์ข้อความตามเงื่อนไขที่ตรง
        }

        public void As04_AndOrOperatorExample(int number)
        {
            // Guideline: ทดสอบการใช้ตัวดำเนินการ AND (&&) และ OR (||) 
            // - เช็คว่า number มากกว่า 8 AND น้อยกว่า 12
            // - เช็คว่า number มากกว่า 8 OR น้อยกว่า 12
        }

        public void As05_GuessingNumberExample(int guessingNumber, int randomNumber)
        {
            // Guideline: สร้างระบบทายตัวเลขง่ายๆ ด้วย if-else
            // เปรียบเทียบ guessingNumber กับ randomNumber ถงตรงกันให้พิมพ์แสดงความยินดี
            // ถ้าไม่ตรงให้พิมพ์ข้อความเสียใจ (เช่น "I guess we can just agree to disagree.")
        }

        public void As06_GuessingNumberMoreOrLessExample(int guessingNumber, int randomNumber)
        {
            // Guideline: ระบบทายตัวเลขแบบมีเงื่อนไขเพิ่มเติม (if-else-if)
            // เช็คว่า guessingNumber น้อยกว่า, มากกว่า, หรือเท่ากับ randomNumber
            // แล้วแสดงคำใบ้ "Too low!", "Too high!" หรือแสดงความยินดี
        }

        public void As07_VerifyIdentityExample(string username, string password, int age, bool isPaid)
        {
            // Guideline: ระบบตรวจสอบสิทธิด้วย Nested if (if ซ้อน if)
            // 1. เช็ค username และ password
            // 2. ถ้าผ่าน ให้เช็คว่าเป็นสมาชิกแบบจ่ายเงิน (isPaid) หรือไม่
            // 3. ถ้าจ่ายเงินแล้ว ให้เช็คอายุ (age) เพิ่มเติมว่ามากกว่า 18 เพื่อเข้าถึงเนื้อหาพิเศษ
        }

        #endregion

        #region Level 1: Simple

        public void Lv01_CheckNumberSign(int number)
        {
            // Guideline: ตรวจสอบว่า number เป็นบวก (Positive), ลบ (Negative) หรือ ศูนย์ (Zero)
        }

        public void Lv02_GetDayName(int day)
        {
            // Guideline: รับค่า day (1-7) แล้วแปลงเป็นชื่อวัน (Monday - Sunday)
            // ถ้าไม่อยู่ในช่วง 1-7 ให้แสดงข้อความ "Invalid day" (แนะนำให้ใช้ switch-case)
        }

        public void Lv03_ValidatePassword(string inputPassword, string correctPassword)
        {
            // Guideline: ตรวจสอบรหัสผ่านว่า inputPassword ตรงกับ correctPassword หรือไม่
            // ถ้าตรงพิมพ์ "True" ถ้าไม่ตรงพิมพ์ "False"
        }

        public void Lv04_GetGrade(int score)
        {
            // Guideline: คำนวณเกรดจากคะแนน (0-100)
            // >= 80: A, >= 70: B, >= 60: C, >= 50: D, ต่ำกว่านี้: F
        }

        public void Lv05_IsLeapYear(int year)
        {
            // Guideline: ตรวจสอบว่าเป็นปีอธิกสุรทิน (Leap Year) หรือไม่
            // กฎ: หาร 4 ลงตัว เป็น leap year, แต่ถ้าหาร 100 ลงตัว ไม่เป็น, ยกเว้นหาร 400 ลงตัว จะเป็น
        }

        public void Lv06_Calculate(double num1, char op, double num2)
        {
            // Guideline: สร้างเครื่องคิดเลขเบื้องต้น คำนวณ num1 กับ num2 ตามเครื่องหมาย op (+, -, *, /)
            // อย่าลืมเช็คกรณีหารด้วย 0
        }

        public void Lv07_GetSeason(int month)
        {
            // Guideline: เช็คฤดูกาลจากเลขเดือน (1-12)
            // 12,1,2 = Winter | 3,4,5 = Spring | 6,7,8 = Summer | 9,10,11 = Fall
        }

        #endregion

        #region Level 2: Moderate

        public void Ex01_PurchasingSystemExample(int quantity, int price, int payment)
        {
            // Guideline: ระบบซื้อสินค้า
            // 1. เช็ค quantity ว่ามีสินค้าหรือไม่
            // 2. ถ้ามี เช็ค payment ว่าพอจ่าย price หรือไม่
            // 3. คำนวณเงินทอนและแสดงข้อความ
        }

        public void Ex02_RockPaperScissorsExample(int userChoice, int computerChoice)
        {
            // Guideline: เกมเป่ายิ้งฉุบ (0=ค้อน, 1=กระดาษ, 2=กรรไกร)
            // เปรียบเทียบ userChoice กับ computerChoice และแสดงผล เสมอ, ชนะ, หรือ แพ้
        }

        public void Ex03_CalculateWeaponDamage(string weaponType, int baseDamage)
        {
            // Guideline: คำนวณดาเมจอาวุธ
            // เช็ค weaponType (sword=1.3, axe=1.4, bow=1.2, staff=1.5, dagger=1.1, อื่นๆ=1.0)
            // เอา baseDamage คูณกับตัวคูณและพิมพ์ค่าที่ได้
        }

        public void Ex04_DeterminePlayerRank(int score, int completionTime)
        {
            // Guideline: คำนวณแรงค์และของรางวัล
            // เช็คคะแนน (Gold=8000+, Silver=6000+, Bronze=4000+, อื่นๆ=Participation)
            // บวกโบนัสตามเวลาที่ใช้ (<=30นาที: +25, <=60นาที: +10, เกิน60นาที: +0)
        }

        #endregion

    }
}
