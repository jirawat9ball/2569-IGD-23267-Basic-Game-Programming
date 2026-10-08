using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;

namespace Week06.Ex02
{
    public class Dog
    {
        // properties including name, breed, age ...
        public string name;
        public string breed;
        public int age;
        // end of properties ...

        // สร้าง constructor ที่รับ parameter 3 ตัว และกำหนดค่าให้กับ properties ของ class
        // โดยทั้ง 3 parameter คือ name, breed, age ตามลำดับ
   

        // behaviors ...
        public void Bark()
        {
            Debug.Log($"{name} is barking");
        }

        public void WagTail()
        {
            Debug.Log($"{name} is wagging tail");
        }

        public void StopBarking()
        {
            Debug.Log($"{name} stopped barking");
        }
        // end of behaviors ...
    }

    public class AS02_ClassConstructor
    {
        Dog dog1;

        public void Start()
        {
            // สร้าง object dog1 ของ class Dog โดยใช้ constructor ที่รับ parameter 3 ตัว
            // และกำหนดค่าให้กับ properties ของ object นั้น
            // กำหนดให้ name = "Buddy", breed = "Golden Retriever", age = 3

            // Student code starts HERE ...

            // Student code ends HERE ...

            // เรียกใช้ method ของ object นั้น
           
        }
    }
}
