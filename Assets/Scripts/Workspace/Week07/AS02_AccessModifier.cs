using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;

namespace Week07.Ex02
{
    public class Animal
    {
        /// <summary>
        /// name เป็น public จึงสามารถเข้าถึงได้จากภายนอก class
        /// รวมถึงภายใน method ของ class ที่สืบทอด Animal ไปด้วย
        /// </summary>
        public string name = "";

        /// <summary>
        /// specie เป็น protected จึงสามารถเข้าถึงได้จากภายใน class ที่สืบทอด Animal
        /// จากการออกแบบนี้ จะทำให้การกำหนดค่าให้กับ specie จะต้องทำผ่าน class ที่สืบทอด Animal เท่านั้น
        /// เช่นผ่าน constructor ของ Dog เพื่อกำหนดค่า specie = "Dog"
        /// ไม่สามารถกำหนดค่าให้กับ specie จากภายนอก class ได้
        /// </summary>
        protected string specie = "";

        /// <summary>
        /// health เป็น private จึงสามารถเข้าถึงได้เฉพาะภายใน class นี้ (Animal) เท่านั้น
        /// </summary>
        private int health = 10;

        public void Feed(int food)
        {
            // student code starts HERE ...
            health += food;
            Debug.Log($"{name} got {food} food");
            // student code ends HERE
        }

        /// <summary>
        /// MakeSound method จะ Debug.Log ข้อความออกมาด้วยเงื่อนไข
        /// + ถ้า health > 50 จะพิมพ์ "{name} happy!"
        /// + ถ้า health <= 50 จะพิมพ์ "{name} weak!"
        /// </summary>
        public void MakeSound()
        {
            // student code starts HERE ...
            if (health > 50)
            {
                Debug.Log($"{name} happy!");
            }
            else
            {
                Debug.Log($"{name} weak!");
            }
            // student code ends HERE
        }
    }

    public class Dog : Animal
    {
        public Dog(string name)
        {
            // student code starts HERE ...
            specie = "Dog";
            this.name = name;
            // student code ends HERE
        }
    }

    public class AS02_AccessModifier
    {
        public void Start()
        {
            // student code start HERE ...
            Dog dog = new Dog("Buddy");
            Debug.Log($"my name is {dog.name}");
            dog.MakeSound();
            dog.Feed(50);
            dog.MakeSound();
            // student code ends HERE

            // NOTE #1
            // จะไม่สามารถเข้าถึง specie ได้เนื่องจาก specie เป็น protected
            // จึงเรียกใช้งานได้เฉพาะภายใน class เท่านั้น
            // ไม่สามารถเข้าถึงผ่าน object ที่สร้างจาก class ที่สืบทอด Animal ได้
            // Debug.Log($"I am {dog.specie}");

            // NOTE #2
            // ไม่สามารถเข้าถึง health ได้เนื่องจาก health เป็น private ของ class Animal
            // ซึ่งเป็น class แม่ ของ Dog และเนื่องจากเป็น private 
            // ตัวแปร health จึงไม่ถูกสืบทอดต่อมาที่ class Dog ได้
            // ไม่สามารถเรียกใช้งานตัวแปร health จาก dog ได้
            // Debug.Log($"my health {dog.health}");
        }
    }
}
