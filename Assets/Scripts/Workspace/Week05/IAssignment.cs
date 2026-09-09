using UnityEngine;

namespace Week05
{
    public interface IAssignment
    {
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

        #region ข้อ 3: แยกโค้ดสร้างแผนที่ออกเป็น Method (Refactoring)

        void GenerateFloor();

        void GenerateWalls();

        void GenerateFoods();

        void PlacePlayer();

        void PlaceExit();

        #endregion

        #region ข้อ 4-7: Method ของตัวละคร

        void Move(Vector2 direction);

        void TakeDamage(int Damage);

        void Heal(int healPoint);

        #endregion
    }
}
