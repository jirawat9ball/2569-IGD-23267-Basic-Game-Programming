using UnityEngine;

namespace Week05
{
    public interface IAssignment
    {
        #region Lecture

        #region ข้อ 1: Method แบบ void และ Parameter (Overloading)

        void UserNameIdentification();

        void UserNameIdentification(string name);

        void UserNameIdentification(string name, int age);

        void UserCountry(string country = "Thailand");

        #endregion

        #region ข้อ 2: Method แบบมีค่าส่งกลับ (Return Type)

        int Add(int a, int b);

        int GetStringLength(string text);

        bool ConvertInttoBool(int sex);

        #endregion

        #endregion

        #region Homework

        #region Level 1: Simple

        int Lv01_CalculateDamage(int baseDamage, float multiplier);

        bool Lv02_CanCastSpell(int currentMana, int manaCost);

        int Lv03_FindHighestScore(int[] scores);

        int Lv04_CalculateTotalScore(int[] scores);

        bool Lv05_CheckLevelUp(int currentExp, int requiredExp);

        int Lv06_ClampHealth(int currentHealth, int minHealth, int maxHealth);

        #endregion

        #endregion
    }
}
