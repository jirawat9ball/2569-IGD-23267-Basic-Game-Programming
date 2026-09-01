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
            As05_GuessingNumberExample(guessingNumber, randomNumber);
            As06_GuessingNumberMoreOrLessExample(guessingNumber, randomNumber);
            As07_VerifyIdentityExample(username, identityPassword, age, isPaid);
        }

        #region Examples

        public void As01_SyntaxIf(bool isSixoClock)
        { 
            // TODO: Implement this method
        }

        public void As02_StringComparisonExample(string password)
        {
            // TODO: Implement this method
        }

        public void As03_NumberComparisonExample(int number)
        {
            // TODO: Implement this method
        }

        public void As04_AndOrOperatorExample(int number)
        {
            // TODO: Implement this method
        }

        public void As05_GuessingNumberExample(int guessingNumber, int randomNumber)
        {
            // TODO: Implement this method
        }

        public void As06_GuessingNumberMoreOrLessExample(int guessingNumber, int randomNumber)
        {
            // TODO: Implement this method
        }

        public void As07_VerifyIdentityExample(string username, string password, int age, bool isPaid)
        {
            // TODO: Implement this method
        }

        #endregion

        #region Level 1: Simple

        public void Lv01_CheckNumberSign(int number)
        {
            // TODO: Implement this method
        }

        public void Lv02_GetDayName(int day)
        {
            // TODO: Implement this method
        }

        public void Lv03_ValidatePassword(string inputPassword, string correctPassword)
        {
            // TODO: Implement this method
        }

        public void Lv04_GetGrade(int score)
        {
            // TODO: Implement this method
        }

        public void Lv05_IsLeapYear(int year)
        {
            // TODO: Implement this method
        }

        public void Lv06_Calculate(double num1, char op, double num2)
        {
            // TODO: Implement this method
        }

        public void Lv07_GetSeason(int month)
        {
            // TODO: Implement this method
        }

        #endregion

        #region Level 2: Moderate

        public void Ex01_PurchasingSystemExample(int quantity, int price, int payment)
        {
            // TODO: Implement this method
        }

        public void Ex02_RockPaperScissorsExample(int userChoice, int computerChoice)
        {
            // TODO: Implement this method
        }

        public void Ex03_CalculateWeaponDamage(string weaponType, int baseDamage)
        {
            // TODO: Implement this method
        }

        public void Ex04_DeterminePlayerRank(int score, int completionTime)
        {
            // TODO: Implement this method
        }

        #endregion

    }
}
