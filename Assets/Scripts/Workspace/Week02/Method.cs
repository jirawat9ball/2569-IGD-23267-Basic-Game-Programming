using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week02
{
    public class Method : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            //UserNameIdentification();
            //UserNameIdentification("nuttawut");
            //UserNameIdentification("boy",18);
            //UserNameIdentification("boy",18,);
            //string[] s = new string[] { "A","B","C"};
            //TellMeUserName(s);
            //UserCountry("USA");
            //UserCountry();
            //UserCountry("Japaness");
            Debug.Log(sumAmound(1, 2));
            Debug.Log(mergeText("jirawat", "Tachaprechawong"));
            Debug.Log(convertInttoBool(0));
            Debug.Log(convertInttoBool(1));
        }
        //Method void
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
        //Method set default value
        public void UserNameIdentification(string name, int age, string country = "Thailand")
        {
            Debug.Log("user name is " + name + " age is " + age + " country is " + country);
        }
        public void UserNameIdentification(string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                UserNameIdentification(names[i]);
            }
        }

        //Method return type value
        public bool convertInttoBool(int sex)
        {
            if (sex == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
            //return sex == 1;
        }
        public int sumAmound(int a, int b)
        {
            return a + b;
        }
        public string mergeText(string name, string lastname)
        {
            return name + " " + lastname;
        }
        //Method set default value
        public void UserCountry(string country = "Thailand")
        {
            Debug.Log(country);
        }


        public void notReturnMethod()
        {
            string name = "jirawat";
            int sex = 0;
            string sport = "hocky";

            Debug.Log(name + sex + sport);
        }
        public int ReturnMethod(int a, int b)
        {
            int sum = a + b;
            return sum;
        }

    }
}