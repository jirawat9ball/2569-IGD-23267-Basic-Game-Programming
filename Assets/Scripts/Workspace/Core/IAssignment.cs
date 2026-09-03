using UnityEngine;

namespace Workspace.Core
{
    public interface IAssignment
    {
        #region Example 

        /// <summary>
        /// Demonstrates string comparison using if statements.
        /// </summary>

        void As01_SyntaxIf(bool isSixoClock);

        /// <summary>
        /// Demonstrates string comparison using if statements.
        /// </summary>
        void As02_StringComparisonExample(string password);

        /// <summary>
        /// <summary>
        /// Demonstrates number comparison using if statements.
        /// </summary>
        void As03_NumberComparisonExample(int number);

        /// <summary>
        /// Demonstrates the use of AND and OR operators in if statements.
        /// </summary>
        void As04_AndOrOperatorExample(int number);

        /// <summary>
        /// Demonstrates a simple number guessing game, using if-else statements.
        /// </summary>
        void As05_GuessingNumberExample(int guessingNumber, int randomNumber);

        /// <summary>
        /// Demonstrates more complicated number guessing game, using if-else-if statements.
        /// </summary>
        void As06_GuessingNumberMoreOrLessExample(int guessingNumber, int randomNumber);

        /// <summary>
        /// Demonstrates a simple purchasing system using if-else nested if statements.
        /// </summary>
        void Ex01_PurchasingSystemExample(int quantity, int price, int payment);

        /// <summary>
        /// Demonstrates a simple identity verification system using nested nested if
        /// </summary>
        void As07_VerifyIdentityExample(string username, string password, int age, bool isPaid);

        /// <summary>
        /// Demonstrates Rock-Paper-Scissors game logic using if-else statements.
        /// </summary>
        void Ex02_RockPaperScissorsExample(int userChoice, int computerChoice);

        #endregion

        #region Level 1: Simple (7 problems) - Basic if-else and switch-case

        /// <summary>
        /// Determines if a number is positive, negative, or zero.
        /// </summary>
        /// <param name="number">An integer to check.</param>
        /// <returns>"Positive" if number > 0, "Negative" if number < 0, "Zero" if number == 0.</returns>
        /// <example>CheckNumberSign(5) => "Positive"</example>
        /// <example>CheckNumberSign(-3) => "Negative"</example>
        /// <example>CheckNumberSign(0) => "Zero"</example>
        /// <remarks>Output must be written using AssignmentDebugConsole.Log</remarks>
        void Lv01_CheckNumberSign(int number);

        /// <summary>
        /// Returns the day name for a given integer (1-7).
        /// </summary>
        /// <param name="day">Day number (1-7).</param>
        /// <returns>Day name ("Monday" to "Sunday") or "Invalid day" for out-of-range values.</returns>
        /// <example>GetDayName(1) => "Monday"</example>
        /// <example>GetDayName(2) => "Tuesday"</example>
        /// <example>GetDayName(3) => "Wednesday"</example>
        /// <example>GetDayName(4) => "Thursday"</example>
        /// <example>GetDayName(5) => "Friday"</example>
        /// <example>GetDayName(6) => "Saturday"</example>
        /// <example>GetDayName(7) => "Sunday"</example>
        /// <example>GetDayName(0) => "Invalid day"</example>
        /// <example>GetDayName(-1) => "Invalid day"</example>
        /// <remarks>Output must be written using AssignmentDebugConsole.Log</remarks>
        void Lv02_GetDayName(int day);

        /// <summary>
        /// Validates password input.
        /// </summary>
        /// <param name="inputPassword">Password entered by user.</param>
        /// <param name="correctPassword">Correct password to validate against.</param>
        /// <returns>True if passwords match, false otherwise.</returns>
        /// <example>ValidatePassword("secret123", "secret123") => true</example>
        /// <example>ValidatePassword("wrongpass", "secret123") => false</example>
        /// <remarks>Output must be written using AssignmentDebugConsole.Log</remarks>
        void Lv03_ValidatePassword(string inputPassword, string correctPassword);

        /// <summary>
        /// Returns grade for a given score.
        /// </summary>
        /// <param name="score">Score (0-100).</param>
        /// <returns>"A" for >=80, "B" for >=70, "C" for >=60, "D" for >=50, "F" otherwise.</returns>
        /// <example>GetGrade(85) => "A"</example>
        /// <example>GetGrade(80) => "A"</example>
        /// <example>GetGrade(70) => "B"</example>
        /// <example>GetGrade(60) => "C"</example>
        /// <example>GetGrade(50) => "D"</example>
        /// <example>GetGrade(49) => "F"</example>
        /// <remarks>Output must be written using AssignmentDebugConsole.Log</remarks>
        void Lv04_GetGrade(int score);


        /// <summary>
        /// Checks if a year is a leap year (ปีอธิกสุรทิน).
        /// กฎของปีอธิกสุรทิน:
        /// ปีที่หารด้วย 4 ลงตัว ถือว่าเป็นปีอธิกสุรทิน. 
        /// แต่ถ้าปีนั้นหารด้วย 100 ลงตัว จะไม่ถือว่าเป็นปีอธิกสุรทิน ยกเว้นว่า
        /// ปีนั้นจะหารด้วย 400 ลงตัว จึงจะเป็นปีอธิกสุรทิน. 
        /// ตัวอย่าง:
        /// ปี ค.ศ. 2020 เป็นปีอธิกสุรทิน เพราะหารด้วย 4 ลงตัว. 
        /// ปี ค.ศ. 2000 เป็นปีอธิกสุรทิน เพราะหารด้วย 400 ลงตัว. 
        /// ปี ค.ศ. 1900 ไม่เป็นปีอธิกสุรทิน เพราะหารด้วย 100 ลงตัว แต่หารด้วย 400 ไม่ลงตัว.

        /// </summary>
        /// <param name="year">Year to check.</param>
        /// <returns>True if leap year, false otherwise.</returns>
        /// <example>IsLeapYear(2020) => true</example>
        /// <example>IsLeapYear(2000) => true</example>
        /// <example>IsLeapYear(1900) => false</example>
        /// <remarks>Output must be written using AssignmentDebugConsole.Log</remarks>
        void Lv05_IsLeapYear(int year);

        /// <summary>
        /// Performs arithmetic operation based on operator and operands.
        /// </summary>
        /// <param name="num1">First operand.</param>
        /// <param name="op">Operator (+, -, *, /).</param>
        /// <param name="num2">Second operand.</param>
        /// <returns>Result string or error message for invalid operator/division by zero.</returns>
        /// <example>Calculate(5.0, '+', 3.0) => "Result: 8"</example>
        /// <example>Calculate(6.0, '/', 0.0) => "Error: Cannot divide by zero."</example>
        /// <remarks>Output must be written using AssignmentDebugConsole.Log</remarks>
        void Lv06_Calculate(double num1, char op, double num2);

        /// <summary>
        /// Returns season for a given month.
        /// Month December (12) - February (2): Winter
        /// Month March (3) - May (5): Spring
        /// Month June (6) - August (8): Summer
        /// Month September (9) - November (11): Fall
        /// </summary>
        /// <param name="month">Month number (1-12).</param>
        /// <returns>Season string or error message for invalid month.</returns>
        /// <example>GetSeason(1) => "It's Winter."</example>
        /// <example>GetSeason(13) => "Invalid month number. Please enter a number between 1 and 12."</example>
        /// <remarks>Output must be written using AssignmentDebugConsole.Log</remarks>
        void Lv07_GetSeason(int month);

        #endregion

        #region Level 2: Moderate (2 problems) - Combined if-else and switch-case

        /// <summary>
        /// Classifies weapon type and calculates damage bonus
        /// Each Weapon damage modifier is based on its type.
        /// "sword" => 1.3
        /// "axe" => 1.4
        /// "bow" => 1.2
        /// "staff" => 1.5
        /// "dagger" => 1.1
        /// other type => 1.0
        /// </summary>
        /// <param name="weaponType">Type of weapon</param>
        /// <param name="baseDamage">Base damage value</param>
        /// <returns>Returns total damage including type bonus</returns>
        /// <example>Input: "sword", 50, Output: 65</example>
        /// <remarks>Output must be written using AssignmentDebugConsole.Log</remarks>
        void Ex03_CalculateWeaponDamage(string weaponType, int baseDamage);

        /// <summary>
        /// Determines player rank based on score and assigns rewards
        /// 
        /// Rank table:
        /// Gold: 8000+ points
        /// Silver: 6000-7999 points
        /// Bronze: 4000-5999 points
        /// Participation: < 4000 points
        /// 
        /// Rewards are based on rank and completion time.
        /// 
        /// Reward for Rank
        /// Gold: 100 coins
        /// Silver: 75 coins
        /// Bronze: 50 coins
        /// Participation: 25 coins
        /// 
        /// completion time 0-30 minutes: +25 coins
        /// completion time 31-60 minutes: +10 coins
        /// completion time 61+ minutes: +0 coins
        /// </summary>
        /// <param name="score">Player's final score</param>
        /// <param name="completionTime">Time taken to complete (in minutes)</param>
        /// <returns>Returns rank with reward information</returns>
        /// <example>Input: 8500, 45, Output: "Gold Rank - 100 coins earned!"</example>
        /// <remarks>Output must be written using AssignmentDebugConsole.Log</remarks>
        void Ex04_DeterminePlayerRank(int score, int completionTime);

        #endregion

        // TODO: add more assignment
        //
        // [1]
        // void ComparisonString() {
        //     if (password != "Moon") //inputField.text
        //     { 
        //         Debug.Log("wrong password");
        //     }
        //     if (password == "Moon") //inputField.text
        //     {
        //         Debug.Log("password is correct");
        //     }
        //     Debug.Log("!= :" + (password != "Moon"));
        //     Debug.Log("== :" + (password == "Moon"));
        // }
        //
        // [2]
        // void ComparisonNumber()
        // {
        //     if (Number > 10)//int.Parse(inputField.text);
        //     {
        //         Debug.Log("My Number > 10");
        //     }
        //     if (Number < 10)
        //     {
        //         Debug.Log("My Number < 10");
        //     }
        //     if (Number == 10)
        //     {
        //         Debug.Log("My Number == 10");
        //     }
        //     if (Number >= 10)
        //     {
        //         Debug.Log("My Number >= 10");
        //     }
        //     if (Number <= 10)
        //     {
        //         Debug.Log("My Number <= 10");
        //     }
        //     if (Number != 10)
        //     {
        //         Debug.Log("My Number != 10");
        //     }
        // }
        //
        // [3]
        // void AndOr()
        // {
        //     if (Number > 8 && Number < 12)////int.Parse(inputField.text);
        //     {
        //         Debug.Log("My Number  8 > < 12");
        //     }
        //     if (Number > 8 || Number < 12)
        //     {
        //         Debug.Log("My Number  or 8 || 12");
        //     }
        // }
        //
        // [4]
        // void GuessingNumber()
        // {
        //     int guessNumber = int.Parse(inputField.text);
        //     int randomNumber = Random.Range(0, 100);
        //     Debug.Log($"Guessing number {randomNumber}");
        //     if (guessNumber == randomNumber)
        //     {
        //         Debug.Log("Congratulations! We are same mind.");
        //         Answer.text = "�;ǡ��Ҫ�ҧ㨵ç�ѹ";
        //     }
        //     else
        //     {
        //         Debug.Log("I guess we can just agree to disagree.");
        //         Answer.text = "�ѹ�����Ҥ�è�ŧ��·�����դ����Դ������ç�ѹ��";
        //     }
        // }
        // 
        // [5]
        // void GuessingNumber_MoreorLess()
        // {
        //     int guessNumber = int.Parse(inputField.text);
        //     int randomNumber = Random.Range(0, 100);
        //     Debug.Log($"Guessing number {randomNumber}");
        //     if (guessNumber < randomNumber)
        //     {
        //         Debug.Log("Too low! Try again.");
        //     }
        //     else if (guessNumber > randomNumber)
        //     {
        //         Debug.Log("Too high! Try again.");
        //     }
        //     else if (guessNumber == randomNumber)
        //     {
        //         Debug.Log("Congratulations! We are same mind.");
        //         Answer.text = "�;ǡ��Ҫ�ҧ㨵ç�ѹ";
        //     }
        // }
        //
        // [6]
        // void PurchasingSystem() {
        //     int quantity = 0;
        //     int purchasePrice = 10;
        //     int payment = 0;
        //     int change = 0;

        //     if (quantity > 0) {
        //         if (payment > purchasePrice)
        //         {
        //             quantity -= 1;
        //             change = payment - purchasePrice;
        //             Debug.Log("�س����ö�����Թ�����");
        //         }
        //         else {
        //             Debug.Log("�س����͹����");
        //         }
        //     }
        //     else {
        //         Debug.Log("�Թ������");
        //     }
        // }
        //
        // [6]
        // void VerifyIdentity() {
        //     string username ="";
        //     string password = "";
        //     int age = 0;
        //     bool isPaid = false;
        //     // ���������ա�õ�Ǩ�ͺ username ��� password ����
        //     if (username == "user" && password == "user123")
        //     {
        //         Debug.Log("You have user access.");
        //         if (isPaid == true)
        //         {
        //             Debug.Log("welcome vip member");
        //             if (age >= 18)
        //             {
        //                 Debug.Log("You have access to exclusive content");

        //             }
        //         }
        //         else {
        //             Debug.Log("welcome free member");
        //             Debug.Log("You can pay to be VIP");

        //         }
        //     }
        //     else
        //     {
        //         Debug.Log("You have guest access.");
        //     }
        // }
        //
        // [7]
        // void RockPaperScissors()
        // {
        //         int userChoice = 0;
        //         // ��˹���ҵ�����͡
        //         //int rock = 0;
        //         //int paper = 1;
        //         //int Scissors = 2;
        //         // ����������͡�ͧ����������
        //         int computerChoice = UnityEngine.Random.Range(0, 2); ;
        //         // ��Ǩ�ͺ��ҷ�������͹
        //         if (userChoice >= 0 && userChoice <= 2)
        //         {
        //             // ���º��º��
        //             if (userChoice == computerChoice)
        //             {
        //                 Debug.Log("����");
        //             }
        //             else if ((userChoice == 0 && computerChoice == 2) ||
        //                     (userChoice == 1 && computerChoice == 0) ||
        //                     (userChoice == 2 && computerChoice == 1))
        //             {
        //                 Debug.Log("�س���!");
        //             }
        //             else
        //             {
        //                 Debug.Log("���������쪹�!");
        //             }
        //         }
        //         else {
        //             Debug.Log("����դӵͺ㹵�����͡");
        //         }
        //     }
        // }
    }
}
