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

        /// <summary>
        /// specie เป็น protected จึงสามารถเข้าถึงได้จากภายใน class ที่สืบทอด Animal
        /// จากการออกแบบนี้ จะทำให้การกำหนดค่าให้กับ specie จะต้องทำผ่าน class ที่สืบทอด Animal เท่านั้น
        /// เช่นผ่าน constructor ของ Dog เพื่อกำหนดค่า specie = "Dog"
        /// ไม่สามารถกำหนดค่าให้กับ specie จากภายนอก class ได้
        /// </summary>

        /// <summary>
        /// health เป็น private จึงสามารถเข้าถึงได้เฉพาะภายใน class นี้ (Animal) เท่านั้น
        /// </summary>

        

        /// <summary>
        /// MakeSound method จะ Debug.Log ข้อความออกมาด้วยเงื่อนไข
        /// + ถ้า health > 50 จะพิมพ์ "{name} happy!"
        /// + ถ้า health <= 50 จะพิมพ์ "{name} weak!"
        /// </summary>
        
    }

    public class Dog 
    {
        public Dog(string name)
        {
            // 1. กำหนด specie = "Dog"
            // สามารถเข้าถึง specie ได้เนื่องจาก specie เป็น protected
            // ทำให้สามารถเข้าถึงได้จาก class ที่สืบทอด Animal ได้

            // 2. กำหนด this.name = name ที่รับเป็น parameter จาก constructor
            // สามารถเข้าถึง name ได้เนื่องจาก name เป็น public
            // ทำให้สามารถเข้าถึงได้จาก class นี้ได้ เนื่องจาก class นี้สืบทอด Animal

            // ไม่สามารถเข้าถึง health ได้เนื่องจาก health เป็น private
            // ทำให้ใช้งานได้แค่ใน class ที่ประกาศ health ไว้เท่านั้น ซึ่งก็คือ class Animal
            // this.health = 100; ==> COMPILE ERROR
        }
    }

    public class AS02_AccessModifier
    {
        public void Start()
        {
            Dog dog = new Dog("Buddy");

            // student code start HERE ...
            // 1. พิมพ์ dog.name ออกมาในข้อความ $"my name is {dog.name}"

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
