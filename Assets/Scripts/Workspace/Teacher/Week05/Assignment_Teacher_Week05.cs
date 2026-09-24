using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week05
{
    public class Assignment_Teacher_Week05 : MonoBehaviour, IAssignment
    {
        #region ข้อ 1: Method แบบ void และ Parameter (Overloading)

        public void UserNameIdentification()
        {
            Debug.Log("user name is UntitleUser");
        }

        public void UserNameIdentification(string name)
        {
            Debug.Log("user name is " + name);
        }

        public void UserNameIdentification(string name, int age)
        {
            Debug.Log("user name is " + name + " age is " + age);
        }

        public void UserCountry(string country = "Thailand")
        {
            Debug.Log(country);
        }

        #endregion

        #region ข้อ 2: Method แบบมีค่าส่งกลับ (Return Type)

        public int Add(int a, int b)
        {
            int c = a + b;
            return c;
        }

        public int GetStringLength(string text)
        {
            return text.Length;
        }

        public bool ConvertInttoBool(int sex)
        {
            return sex == 1;
        }

        #endregion
    }
}
