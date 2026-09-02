using System.Diagnostics;
using AssignmentSystem.Services;
using NUnit.Framework;

using Week02;

namespace Week02_If
{
    public class TestBase
    {
        protected IAssignment assignment;
        protected UnityEngine.GameObject testGo;

        [SetUp]
        public void Setup()
        {
            testGo = new UnityEngine.GameObject();
            assignment = testGo.AddComponent<Assignment_Student_Week02>();
            AssignmentDebugConsole.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            if (testGo != null)
            {
                UnityEngine.Object.DestroyImmediate(testGo);
            }
        }
    }

    public class Lecture : TestBase
    {
        [TestCase(true, "You can get in\nCrack Crack!!!!", TestName = "CheckSyntax_true", Description = "Checks if is six o clock")]
        [TestCase(false, "Crack Crack!!!!", TestName = "CheckSyntax_false", Description = "Checks if is not six o clock")]
        public void AS01_Syntax_AllCases(bool istrue, string expected)
        {
            assignment.As01_SyntaxIf(istrue);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase("Moon", TestName = "StringComparisonExample_WithMoonPassword", Description = "Tests StringComparisonExample with 'Moon' (correct) password")]
        [TestCase("Sun", TestName = "StringComparisonExample_WithSunPassword", Description = "Tests StringComparisonExample with 'Sun' (incorrect) password")]
        public void As02_StringComparisonExample(string password)
        {
            assignment.As02_StringComparisonExample(password);
            var output = AssignmentDebugConsole.GetOutput();
            string expected = password == "Moon" ? "password is correct" : "wrong password";
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase(11, "My Number > 10\nMy Number >= 10\nMy Number != 10", TestName = "NumberComparisonExample_GreaterThan10")]
        [TestCase(10, "My Number == 10\nMy Number >= 10\nMy Number <= 10", TestName = "NumberComparisonExample_Equal10")]
        [TestCase(9, "My Number < 10\nMy Number <= 10\nMy Number != 10", TestName = "NumberComparisonExample_LessThan10")]
        public void As03_NumberComparisonExample(int number, string expected)
        {
            assignment.As03_NumberComparisonExample(number);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase(10, "My Number 8 > < 12\nMy Number or 8 || 12", TestName = "AndOrOperatorExample_10")]
        [TestCase(7, "My Number or 8 || 12", TestName = "AndOrOperatorExample_7")]
        [TestCase(13, "My Number or 8 || 12", TestName = "AndOrOperatorExample_13")]
        public void As04_AndOrOperatorExample(int number, string expected)
        {
            assignment.As04_AndOrOperatorExample(number);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase(5, 5, "Guessing number 5\nCongratulations! You guessed the correct number.", TestName = "GuessingNumberExample_Correct")]
        [TestCase(3, 5, "Guessing number 5\nI guess we can just agree to disagree.", TestName = "GuessingNumberExample_Incorrect")]
        public void As05_GuessingNumberExample(int guessingNumber, int randomNumber, string expected)
        {
            assignment.As05_GuessingNumberExample(guessingNumber, randomNumber);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase(3, 5, "Guessing number 5\nToo low! Try again.", TestName = "GuessingNumberMoreOrLessExample_TooLow")]
        [TestCase(7, 5, "Guessing number 5\nToo high! Try again.", TestName = "GuessingNumberMoreOrLessExample_TooHigh")]
        [TestCase(5, 5, "Guessing number 5\nCongratulations! We are same mind.", TestName = "GuessingNumberMoreOrLessExample_Correct")]
        public void As06_GuessingNumberMoreOrLessExample(int guessingNumber, int randomNumber, string expected)
        {
            assignment.As06_GuessingNumberMoreOrLessExample(guessingNumber, randomNumber);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase("user", "user123", 20, true, "You have user access.\nwelcome vip member.\nYou have access to exclusive content.", TestName = "VerifyIdentityExample_VIPAdult")]
        [TestCase("user", "user123", 15, true, "You have user access.\nwelcome vip member.", TestName = "VerifyIdentityExample_VIPUnderage")]
        [TestCase("user", "user123", 20, false, "You have user access.\nwelcome free member.", TestName = "VerifyIdentityExample_FreeMember")]
        [TestCase("guest", "pass", 20, false, "You have guest access.", TestName = "VerifyIdentityExample_Guest")]
        public void As07_VerifyIdentityExample(string username, string password, int age, bool isPaid, string expected)
        {
            assignment.As07_VerifyIdentityExample(username, password, age, isPaid);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }
    }

    public class Homework : TestBase
    {
        [TestCase(5, "Positive", TestName = "CheckNumberSign_Positive", Description = "Checks if the number is positive")]
        [TestCase(-3, "Negative", TestName = "CheckNumberSign_Negative", Description = "Checks if the number is negative")]
        [TestCase(0, "Zero", TestName = "CheckNumberSign_Zero", Description = "Checks if the number is zero")]
        [TestCase(int.MaxValue, "Positive", TestName = "CheckNumberSign_MaxValue", Description = "Checks if int.MaxValue is positive")]
        [TestCase(int.MinValue, "Negative", TestName = "CheckNumberSign_MinValue", Description = "Checks if int.MinValue is negative")]
        public void Lv01_CheckNumberSign_AllCases(int number, string expected)
        {
            assignment.Lv01_CheckNumberSign(number);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase(1, "Monday", TestName = "GetDayName_Monday", Description = "Returns Monday for input 1")]
        [TestCase(7, "Sunday", TestName = "GetDayName_Sunday", Description = "Returns Sunday for input 7")]
        [TestCase(0, "Invalid day", TestName = "GetDayName_Invalid0", Description = "Returns Invalid day for input 0")]
        [TestCase(2, "Tuesday", TestName = "GetDayName_Tuesday", Description = "Returns Tuesday for input 2")]
        [TestCase(3, "Wednesday", TestName = "GetDayName_Wednesday", Description = "Returns Wednesday for input 3")]
        [TestCase(4, "Thursday", TestName = "GetDayName_Thursday", Description = "Returns Thursday for input 4")]
        [TestCase(5, "Friday", TestName = "GetDayName_Friday", Description = "Returns Friday for input 5")]
        [TestCase(6, "Saturday", TestName = "GetDayName_Saturday", Description = "Returns Saturday for input 6")]
        [TestCase(8, "Invalid day", TestName = "GetDayName_Invalid8", Description = "Returns Invalid day for input 8")]
        [TestCase(-5, "Invalid day", TestName = "GetDayName_InvalidNegative", Description = "Returns Invalid day for negative input")]
        public void LV02_GetDayName_AllCases(int day, string expected)
        {
            assignment.Lv02_GetDayName(day);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase("secret123", "secret123", true, TestName = "ValidatePassword_Correct", Description = "Returns true for correct password")]
        [TestCase("wrongpass", "secret123", false, TestName = "ValidatePassword_Incorrect", Description = "Returns false for incorrect password")]
        [TestCase("", "", true, TestName = "ValidatePassword_Empty", Description = "Returns true for both passwords empty")]
        [TestCase("secret123", "Secret123", false, TestName = "ValidatePassword_CaseSensitive", Description = "Returns false for case difference")]
        [TestCase(" secret123 ", "secret123", false, TestName = "ValidatePassword_Whitespace", Description = "Returns false for whitespace difference")]
        [TestCase("secret123", "", false, TestName = "ValidatePassword_EmptyCorrect", Description = "Returns false for empty correct password")]
        [TestCase("", "secret123", false, TestName = "ValidatePassword_EmptyInput", Description = "Returns false for empty input password")]
        public void Lv03_ValidatePassword_AllCases(string input, string correct, bool expected)
        {
            assignment.Lv03_ValidatePassword(input, correct);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected ? "True" : "False", output);
        }

        [TestCase(95, "A", TestName = "GetGrade_A95", Description = "Returns A for score 95")]
        [TestCase(85, "A", TestName = "GetGrade_A85", Description = "Returns A for score 85")]
        [TestCase(75, "B", TestName = "GetGrade_B75", Description = "Returns B for score 75")]
        [TestCase(65, "C", TestName = "GetGrade_C65", Description = "Returns C for score 65")]
        [TestCase(55, "D", TestName = "GetGrade_D55", Description = "Returns D for score 55")]
        [TestCase(100, "A", TestName = "GetGrade_A100", Description = "Returns A for score 100")]
        [TestCase(0, "F", TestName = "GetGrade_F0", Description = "Returns F for score 0")]
        [TestCase(80, "A", TestName = "GetGrade_A80", Description = "Returns A for score 80")]
        [TestCase(70, "B", TestName = "GetGrade_B70", Description = "Returns B for score 70")]
        [TestCase(60, "C", TestName = "GetGrade_C60", Description = "Returns C for score 60")]
        [TestCase(50, "D", TestName = "GetGrade_D50", Description = "Returns D for score 50")]
        [TestCase(49, "F", TestName = "GetGrade_F49", Description = "Returns F for score 49")]
        [TestCase(-1, "F", TestName = "GetGrade_FNegative", Description = "Returns F for negative score")]
        [TestCase(101, "A", TestName = "GetGrade_A101", Description = "Returns A for score above 100")]
        public void Lv04_GetGrade_AllCases(int score, string expected)
        {
            assignment.Lv04_GetGrade(score);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase(2000, true, TestName = "IsLeapYear_2000", Description = "Returns true for leap year 2000 (divisible by 400)")]
        [TestCase(2020, true, TestName = "IsLeapYear_2020", Description = "Returns true for leap year 2020 (divisible by 4)")]
        [TestCase(1900, false, TestName = "IsLeapYear_1900", Description = "Returns false for 1900 (divisible by 100, not 400)")]
        [TestCase(2004, true, TestName = "IsLeapYear_2004", Description = "Returns true for leap year 2004 (divisible by 4)")]
        [TestCase(2100, false, TestName = "IsLeapYear_2100", Description = "Returns false for 2100 (divisible by 100, not 400)")]
        [TestCase(2400, true, TestName = "IsLeapYear_2400", Description = "Returns true for 2400 (divisible by 400)")]
        [TestCase(1999, false, TestName = "IsLeapYear_1999", Description = "Returns false for 1999 (not leap year)")]
        [TestCase(-400, true, TestName = "IsLeapYear_Neg400", Description = "Returns true for -400 (divisible by 400)")]
        [TestCase(0, true, TestName = "IsLeapYear_0", Description = "Returns true for year 0 (divisible by 400)")]
        public void Lv05_IsLeapYear_AllCases(int year, bool expected)
        {
            assignment.Lv05_IsLeapYear(year);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected ? "True" : "False", output);
        }

        [TestCase(5.0, '+', 3.0, "Result: 8", TestName = "Calculate_Add", Description = "Returns 8 for 5+3")]
        [TestCase(5.0, '-', 3.0, "Result: 2", TestName = "Calculate_Subtract", Description = "Returns 2 for 5-3")]
        [TestCase(5.0, '*', 3.0, "Result: 15", TestName = "Calculate_Multiply", Description = "Returns 15 for 5*3")]
        [TestCase(6.0, '/', 2.0, "Result: 3", TestName = "Calculate_Divide", Description = "Returns 3 for 6/2")]
        [TestCase(6.0, '/', 0.0, "Error: Cannot divide by zero.", TestName = "Calculate_DivideByZero", Description = "Returns error for division by zero")]
        [TestCase(6.0, 'x', 2.0, "Invalid operator. Please use +, -, *, or /.", TestName = "Calculate_InvalidOperator_x", Description = "Returns error for invalid operator 'x'")]
        [TestCase(-5.0, '+', -3.0, "Result: -8", TestName = "Calculate_AddNegatives", Description = "Returns -8 for -5+(-3)")]
        [TestCase(0.0, '+', 0.0, "Result: 0", TestName = "Calculate_AddZeros", Description = "Returns 0 for 0+0")]
        [TestCase(1e10, '+', 1e10, "Result: 20000000000", TestName = "Calculate_AddLarge", Description = "Returns 2e10 for 1e10+1e10")]
        [TestCase(0.1, '+', 0.2, "Result: 0.3", TestName = "Calculate_AddFloats", Description = "Returns 0.3 for 0.1+0.2")]
        [TestCase(5.0, ' ', 3.0, "Invalid operator. Please use +, -, *, or /.", TestName = "Calculate_InvalidOperator_Space", Description = "Returns error for invalid operator (space)")]
        [TestCase(5.0, 'X', 3.0, "Invalid operator. Please use +, -, *, or /.", TestName = "Calculate_InvalidOperator_X", Description = "Returns error for invalid operator 'X'")]
        public void Lv06_Calculate_AllCases(double num1, char op, double num2, string expected)
        {
            assignment.Lv06_Calculate(num1, op, num2);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase(1, "It's Winter.", TestName = "GetSeason_Winter1", Description = "Returns Winter for month 1")]
        [TestCase(4, "It's Spring.", TestName = "GetSeason_Spring4", Description = "Returns Spring for month 4")]
        [TestCase(7, "It's Summer.", TestName = "GetSeason_Summer7", Description = "Returns Summer for month 7")]
        [TestCase(10, "It's Fall.", TestName = "GetSeason_Fall10", Description = "Returns Fall for month 10")]
        [TestCase(13, "Invalid month number. Please enter a number between 1 and 12.", TestName = "GetSeason_Invalid13", Description = "Returns Invalid for month 13")]
        [TestCase(2, "It's Winter.", TestName = "GetSeason_Winter2", Description = "Returns Winter for month 2")]
        [TestCase(3, "It's Spring.", TestName = "GetSeason_Spring3", Description = "Returns Spring for month 3")]
        [TestCase(5, "It's Spring.", TestName = "GetSeason_Spring5", Description = "Returns Spring for month 5")]
        [TestCase(6, "It's Summer.", TestName = "GetSeason_Summer6", Description = "Returns Summer for month 6")]
        [TestCase(8, "It's Summer.", TestName = "GetSeason_Summer8", Description = "Returns Summer for month 8")]
        [TestCase(9, "It's Fall.", TestName = "GetSeason_Fall9", Description = "Returns Fall for month 9")]
        [TestCase(11, "It's Fall.", TestName = "GetSeason_Fall11", Description = "Returns Fall for month 11")]
        [TestCase(12, "It's Winter.", TestName = "GetSeason_Winter12", Description = "Returns Winter for month 12")]
        [TestCase(0, "Invalid month number. Please enter a number between 1 and 12.", TestName = "GetSeason_Invalid0", Description = "Returns Invalid for month 0")]
        [TestCase(-1, "Invalid month number. Please enter a number between 1 and 12.", TestName = "GetSeason_InvalidNegative", Description = "Returns Invalid for negative month")]
        [TestCase(100, "Invalid month number. Please enter a number between 1 and 12.", TestName = "GetSeason_Invalid100", Description = "Returns Invalid for month 100")]
        public void Lv07_GetSeason_AllCases(int month, string expected)
        {
            assignment.Lv07_GetSeason(month);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase(1, 10, 20, "เธเธธเธ“เนเธ”เนเธฃเธฑเธเธชเธดเธเธเนเธฒเนเธฅเนเธง\nเธเธธเธ“เนเธ”เนเธฃเธฑเธเน€เธเธดเธเธ—เธญเธ 10 เธเธฒเธ—", TestName = "PurchasingSystemExample_EnoughMoneyWithChange")]
        [TestCase(1, 10, 10, "เธเธธเธ“เนเธ”เนเธฃเธฑเธเธชเธดเธเธเนเธฒเนเธฅเนเธง", TestName = "PurchasingSystemExample_ExactMoney")]
        [TestCase(1, 10, 5, "เธเธธเธ“เธกเธตเน€เธเธดเธเนเธกเนเธเธญ", TestName = "PurchasingSystemExample_NotEnoughMoney")]
        [TestCase(0, 10, 20, "เธชเธดเธเธเนเธฒเธซเธกเธ”", TestName = "PurchasingSystemExample_OutOfStock")]
        public void Ex01_PurchasingSystemExample(int quantity, int price, int payment, string expected)
        {
            assignment.Ex01_PurchasingSystemExample(quantity, price, payment);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase(0, 0, "เน€เธชเธกเธญ", TestName = "RockPaperScissorsExample_DrawRock")]
        [TestCase(0, 2, "เธเธธเธ“เธเธเธฐ!", TestName = "RockPaperScissorsExample_RockBeatsScissors")]
        [TestCase(1, 0, "เธเธธเธ“เธเธเธฐ!", TestName = "RockPaperScissorsExample_PaperBeatsRock")]
        [TestCase(2, 1, "เธเธธเธ“เธเธเธฐ!", TestName = "RockPaperScissorsExample_ScissorsBeatsPaper")]
        [TestCase(2, 0, "เธเธธเธ“เนเธเน!", TestName = "RockPaperScissorsExample_ScissorsLosesToRock")]
        [TestCase(3, 0, "เธเธฃเธธเธ“เธฒเน€เธฅเธทเธญเธเน€เธเนเธเธ•เธฑเธงเน€เธฅเธเธ—เธตเนเธ–เธนเธเธ•เนเธญเธ", TestName = "RockPaperScissorsExample_InvalidUserChoice")]
        public void Ex02_RockPaperScissorsExample(int userChoice, int computerChoice, string expected)
        {
            assignment.Ex02_RockPaperScissorsExample(userChoice, computerChoice);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected, output);
        }

        [TestCase("sword", 50, 65, TestName = "CalculateWeaponDamage_Sword", Description = "Sword damage with 30% bonus")]
        [TestCase("axe", 50, 70, TestName = "CalculateWeaponDamage_Axe", Description = "Axe damage with 40% bonus")]
        [TestCase("bow", 50, 60, TestName = "CalculateWeaponDamage_Bow", Description = "Bow damage with 20% bonus")]
        [TestCase("staff", 50, 75, TestName = "CalculateWeaponDamage_Staff", Description = "Staff damage with 50% bonus")]
        [TestCase("dagger", 50, 55, TestName = "CalculateWeaponDamage_Dagger", Description = "Dagger damage with 10% bonus")]
        [TestCase("unknown", 50, 50, TestName = "CalculateWeaponDamage_Unknown", Description = "Unknown weapon type, no bonus")]
        public void Ex03_CalculateWeaponDamage_AllCases(string weaponType, int baseDamage, int expected)
        {
            assignment.Ex03_CalculateWeaponDamage(weaponType, baseDamage);
            var output = AssignmentDebugConsole.GetOutput();
            TestUtils.AssertMultilineEqual(expected.ToString(), output);
        }

        [TestCase(-1, 30, "Invalid score or time", TestName = "DeterminePlayerRank_InvalidScore", Description = "Invalid score input (-1)")]
        [TestCase(5000, -5, "Invalid score or time", TestName = "DeterminePlayerRank_InvalidTime", Description = "Invalid time input (-5)")]
        [TestCase(0, 0, "Participation Rank - 50 coins earned!", TestName = "DeterminePlayerRank_Participation_0_0", Description = "Participation, 0 min (fast)")]
        [TestCase(2000, 25, "Participation Rank - 50 coins earned!", TestName = "DeterminePlayerRank_Participation_Fast", Description = "Participation, fast (<=30 min)")]
        [TestCase(2000, 35, "Participation Rank - 35 coins earned!", TestName = "DeterminePlayerRank_Participation_Medium", Description = "Participation, medium (31-60 min)")]
        [TestCase(2000, 70, "Participation Rank - 25 coins earned!", TestName = "DeterminePlayerRank_Participation_Slow", Description = "Participation, slow (>=61 min)")]
        [TestCase(3999, 30, "Participation Rank - 50 coins earned!", TestName = "DeterminePlayerRank_Participation_Boundary_Fast", Description = "Participation, upper boundary, fast")]
        [TestCase(3999, 31, "Participation Rank - 35 coins earned!", TestName = "DeterminePlayerRank_Participation_Boundary_Medium", Description = "Participation, upper boundary, medium")]
        [TestCase(3999, 61, "Participation Rank - 25 coins earned!", TestName = "DeterminePlayerRank_Participation_Boundary_Slow", Description = "Participation, upper boundary, slow")]
        [TestCase(4000, 30, "Bronze Rank - 75 coins earned!", TestName = "DeterminePlayerRank_Bronze_LowerBoundary_Fast", Description = "Bronze, lower boundary, fast")]
        [TestCase(4000, 31, "Bronze Rank - 60 coins earned!", TestName = "DeterminePlayerRank_Bronze_LowerBoundary_Medium", Description = "Bronze, lower boundary, medium")]
        [TestCase(4000, 61, "Bronze Rank - 50 coins earned!", TestName = "DeterminePlayerRank_Bronze_LowerBoundary_Slow", Description = "Bronze, lower boundary, slow")]
        [TestCase(5999, 30, "Bronze Rank - 75 coins earned!", TestName = "DeterminePlayerRank_Bronze_UpperBoundary_Fast", Description = "Bronze, upper boundary, fast")]
        [TestCase(5999, 31, "Bronze Rank - 60 coins earned!", TestName = "DeterminePlayerRank_Bronze_UpperBoundary_Medium", Description = "Bronze, upper boundary, medium")]
        [TestCase(5999, 61, "Bronze Rank - 50 coins earned!", TestName = "DeterminePlayerRank_Bronze_UpperBoundary_Slow", Description = "Bronze, upper boundary, slow")]
        [TestCase(4500, 25, "Bronze Rank - 75 coins earned!", TestName = "DeterminePlayerRank_Bronze_Fast", Description = "Bronze, fast")]
        [TestCase(4500, 45, "Bronze Rank - 60 coins earned!", TestName = "DeterminePlayerRank_Bronze_Medium", Description = "Bronze, medium")]
        [TestCase(4500, 70, "Bronze Rank - 50 coins earned!", TestName = "DeterminePlayerRank_Bronze_Slow", Description = "Bronze, slow")]
        [TestCase(6000, 30, "Silver Rank - 100 coins earned!", TestName = "DeterminePlayerRank_Silver_LowerBoundary_Fast", Description = "Silver, lower boundary, fast")]
        [TestCase(6000, 31, "Silver Rank - 85 coins earned!", TestName = "DeterminePlayerRank_Silver_LowerBoundary_Medium", Description = "Silver, lower boundary, medium")]
        [TestCase(6000, 61, "Silver Rank - 75 coins earned!", TestName = "DeterminePlayerRank_Silver_LowerBoundary_Slow", Description = "Silver, lower boundary, slow")]
        [TestCase(7999, 30, "Silver Rank - 100 coins earned!", TestName = "DeterminePlayerRank_Silver_UpperBoundary_Fast", Description = "Silver, upper boundary, fast")]
        [TestCase(7999, 31, "Silver Rank - 85 coins earned!", TestName = "DeterminePlayerRank_Silver_UpperBoundary_Medium", Description = "Silver, upper boundary, medium")]
        [TestCase(7999, 61, "Silver Rank - 75 coins earned!", TestName = "DeterminePlayerRank_Silver_UpperBoundary_Slow", Description = "Silver, upper boundary, slow")]
        [TestCase(6500, 25, "Silver Rank - 100 coins earned!", TestName = "DeterminePlayerRank_Silver_Fast", Description = "Silver, fast")]
        [TestCase(6500, 45, "Silver Rank - 85 coins earned!", TestName = "DeterminePlayerRank_Silver_Medium", Description = "Silver, medium")]
        [TestCase(6500, 70, "Silver Rank - 75 coins earned!", TestName = "DeterminePlayerRank_Silver_Slow", Description = "Silver, slow")]
        [TestCase(8000, 30, "Gold Rank - 125 coins earned!", TestName = "DeterminePlayerRank_Gold_LowerBoundary_Fast", Description = "Gold, lower boundary, fast")]
        [TestCase(8000, 31, "Gold Rank - 110 coins earned!", TestName = "DeterminePlayerRank_Gold_LowerBoundary_Medium", Description = "Gold, lower boundary, medium")]
        [TestCase(8000, 61, "Gold Rank - 100 coins earned!", TestName = "DeterminePlayerRank_Gold_LowerBoundary_Slow", Description = "Gold, lower boundary, slow")]
        [TestCase(8500, 25, "Gold Rank - 125 coins earned!", TestName = "DeterminePlayerRank_Gold_Fast", Description = "Gold, fast")]
        [TestCase(8500, 45, "Gold Rank - 110 coins earned!", TestName = "DeterminePlayerRank_Gold_Medium", Description = "Gold, medium")]
        [TestCase(8500, 70, "Gold Rank - 100 coins earned!", TestName = "DeterminePlayerRank_Gold_Slow", Description = "Gold, slow")]
        [TestCase(8500, 31, "Gold Rank - 110 coins earned!", TestName = "DeterminePlayerRank_Gold_31min", Description = "Gold, 31 min")]
        [TestCase(8500, 60, "Gold Rank - 110 coins earned!", TestName = "DeterminePlayerRank_Gold_60min", Description = "Gold, 60 min")]
        [TestCase(8500, 61, "Gold Rank - 100 coins earned!", TestName = "DeterminePlayerRank_Gold_61min", Description = "Gold, 61 min")]
        [TestCase(8500, int.MaxValue, "Gold Rank - 100 coins earned!", TestName = "DeterminePlayerRank_Gold_MaxTime", Description = "Gold, max int time")]
        public void Ex04_DeterminePlayerRank_AllCases(int score, int completionTime, string expected)
        {
            assignment.Ex04_DeterminePlayerRank(score, completionTime);
            var output = AssignmentDebugConsole.GetOutput();
            Assert.AreEqual(output.Trim(), expected.Trim());
        }
    }

    public class TestUtils
    {
        internal static void AssertMultilineEqual(string expected, string actual, string message = null)
        {
            string normExpected = expected.Replace("\r\n", "\n").Replace("\r", "\n").Trim();
            string normActual = actual.Replace("\r\n", "\n").Replace("\r", "\n").Trim();
            if (message == null)
                message = $"Expected output:\n{normExpected}\n----\nActual output:\n{normActual}";
            Assert.AreEqual(normExpected, normActual, message);
        }
    }
}

