using UnityEngine;
using Workspace.Core;
using Debug = Workspace.Core.SimpleDebugConsole;

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

        [Header("Level 1 Variables")]
        public int lv01Number;
        public int lv02Day;
        public string lv03InputPassword;
        public string lv03CorrectPassword;
        public int lv04Score;
        public int lv05Year;
        public double lv06Num1;
        public string lv06Op = "+";
        public double lv06Num2;
        public int lv07Month;

        [Header("Level 2 Variables")]
        public int ex01Quantity;
        public int ex01Price;
        public int ex01Payment;
        public int ex02UserChoice;
        public int ex02ComputerChoice;
        public string ex03WeaponType;
        public int ex03BaseDamage;
        public int ex04Score;
        public int ex04CompletionTime;

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

            // กดปุ่ม Space เพื่อรันโจทย์ Level 1 และ Level 2
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Lv01_CheckNumberSign(lv01Number);
                Lv02_GetDayName(lv02Day);
                Lv03_ValidatePassword(lv03InputPassword, lv03CorrectPassword);
                Lv04_GetGrade(lv04Score);
                Lv05_IsLeapYear(lv05Year);
                char op = string.IsNullOrEmpty(lv06Op) ? '+' : lv06Op[0];
                Lv06_Calculate(lv06Num1, op, lv06Num2);
                Lv07_GetSeason(lv07Month);

                Ex01_PurchasingSystemExample(ex01Quantity, ex01Price, ex01Payment);
                Ex02_RockPaperScissorsExample(ex02UserChoice, ex02ComputerChoice);
                Ex03_CalculateWeaponDamage(ex03WeaponType, ex03BaseDamage);
                Ex04_DeterminePlayerRank(ex04Score, ex04CompletionTime);
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
            // (เช่น "My Number > 10", "My Number < 10", "My Number == 10", "My Number >= 10", "My Number <= 10", "My Number != 10")
        }

        public void As04_AndOrOperatorExample(int number)
        {
            // Guideline: ทดสอบการใช้ตัวดำเนินการ AND (&&) และ OR (||) 
            // - เช็คว่า number มากกว่า 8 AND น้อยกว่า 12 (พิมพ์ "My Number 8 > < 12")
            // - เช็คว่า number น้อยกว่า 8 OR มากกว่า 12 (พิมพ์ "My Number 8 || 12")
        }

        public void As05_GuessingNumberExample(int guessingNumber, int randomNumber)
        {
            // Guideline: สร้างระบบทายตัวเลขง่ายๆ ด้วย if-else
            // เปรียบเทียบ guessingNumber กับ randomNumber ถงตรงกันให้พิมพ์แสดงความยินดี (พิมพ์ "Correct!")
            // ถ้าไม่ตรงให้พิมพ์ข้อความเสียใจ (เช่น "Incorrect!")
        }

        public void As06_GuessingNumberMoreOrLessExample(int guessingNumber, int randomNumber)
        {
            // Guideline: ระบบทายตัวเลขแบบมีเงื่อนไขเพิ่มเติม (if-else-if)
            // เช็คว่า guessingNumber น้อยกว่า, มากกว่า, หรือเท่ากับ randomNumber
            // แล้วแสดงคำใบ้ "Too low!", "Too high!" หรือแสดงความยินดี "Correct!"
        }

        public void As07_VerifyIdentityExample(string username, string password, int age, bool isPaid)
        {
            // Guideline: ระบบตรวจสอบสิทธิด้วย Nested if (if ซ้อน if)
            // 1. เช็ค username และ password (ถูกต้องพิมพ์ "User access", ผิดพิมพ์ "Guest access")
            // 2. ถ้าผ่าน ให้เช็คว่าเป็นสมาชิกแบบจ่ายเงิน (isPaid) หรือไม่ (ใช่พิมพ์ "VIP member", ไม่ใช่พิมพ์ "Free member")
            // 3. ถ้าจ่ายเงินแล้ว ให้เช็คอายุ (age) เพิ่มเติมว่ามากกว่า 18 เพื่อเข้าถึงเนื้อหาพิเศษ (พิมพ์ "Exclusive content")
        }

        #endregion

        #region Level 1: Simple

        public void Lv01_CheckNumberSign(int number)
        {
            // Guideline: ตรวจสอบว่า number เป็นบวก (พิมพ์ "Positive"), ลบ (พิมพ์ "Negative") หรือ ศูนย์ (พิมพ์ "Zero")
        }

        public void Lv02_GetDayName(int day)
        {
            // Guideline: รับค่า day (1-7) แล้วแปลงเป็นชื่อวัน (พิมพ์ "Monday" ถึง "Sunday")
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
            // >= 80: พิมพ์ "A", >= 70: พิมพ์ "B", >= 60: พิมพ์ "C", >= 50: พิมพ์ "D", ต่ำกว่านี้: พิมพ์ "F"
        }

        public void Lv05_IsLeapYear(int year)
        {
            // Guideline: ตรวจสอบว่าเป็นปีอธิกสุรทิน (Leap Year) หรือไม่
            // กฎ: หาร 4 ลงตัว เป็น leap year, แต่ถ้าหาร 100 ลงตัว ไม่เป็น, ยกเว้นหาร 400 ลงตัว จะเป็น
            // (ถ้าเป็น leap year พิมพ์ "True" ถ้าไม่เป็นพิมพ์ "False")
        }

        public void Lv06_Calculate(double num1, char op, double num2)
        {
            // Guideline: สร้างเครื่องคิดเลขเบื้องต้น คำนวณ num1 กับ num2 ตามเครื่องหมาย op (+, -, *, /)
            // แสดงผลในรูปแบบ "Result: {ผลลัพธ์}" 
            // กรณีหารด้วย 0 พิมพ์ "Error: Cannot divide by zero." 
            // และเครื่องหมายผิดพิมพ์ "Invalid operator. Please use +, -, *, or /."
        }

        public void Lv07_GetSeason(int month)
        {
            // Guideline: เช็คฤดูกาลจากเลขเดือน (1-12)
            // 12,1,2 = Winter (พิมพ์ "It's Winter.") | 3,4,5 = Spring (พิมพ์ "It's Spring.") | 6,7,8 = Summer (พิมพ์ "It's Summer.") | 9,10,11 = Fall (พิมพ์ "It's Fall.")
            // นอกเหนือจากนี้พิมพ์ "Invalid month number. Please enter a number between 1 and 12."
        }

        #endregion

        #region Level 2: Moderate

        public void Ex01_PurchasingSystemExample(int quantity, int price, int payment)
        {
            // Guideline: ระบบซื้อสินค้า
            // 1. เช็ค quantity ว่ามีสินค้าหรือไม่ (ถ้าน้อยกว่าหรือเท่ากับ 0 ให้พิมพ์ "Out of stock")
            // 2. ถ้ามี เช็ค payment ว่าพอจ่าย price หรือไม่
            // 3. ถ้าพอ คำนวณเงินทอนและแสดงข้อความ "Item purchased successfully" และถ้ามีเงินทอน ให้แสดงข้อความ "Your change is {change} baht"
            // 4. ถ้าไม่พอ ให้พิมพ์ "Not enough money"
        }

        public void Ex02_RockPaperScissorsExample(int userChoice, int computerChoice)
        {
            // Guideline: เกมเป่ายิ้งฉุบ (0=ค้อน, 1=กระดาษ, 2=กรรไกร)
            // เช็คผู้ชนะ และพิมพ์ข้อความ:
            // "Draw" สำหรับเสมอ, "You Win!" สำหรับชนะ, "You Lose!" สำหรับแพ้
            // (อย่าลืมจัดการกรณี userChoice ไม่อยู่ใน 0-2 ให้พิมพ์ "Please select a valid number")
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
            // แสดงผลในรูปแบบ "{rank} Rank - {totalCoins} coins earned!" 
            // (หรือ "Invalid score or time" ถ้าคะแนนหรือเวลาติดลบ)
        }

        #endregion

    }
}

