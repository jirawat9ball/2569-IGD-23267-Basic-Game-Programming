using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;

namespace Week07.Ex01
{
    public class Animal
    {
        public string name;

        public void MakeSound()
        {
            Debug.Log($"Animal {name} is making sound");
        }
    }

    // Guideline:
    // 1. ทำให้ Dog สืบทอด (inherit) จาก Animal โดยเขียน  : Animal  ต่อท้ายชื่อคลาส
    // 2. พอสืบทอดแล้ว Dog จะใช้ตัวแปร name และเมธอด MakeSound() ของ Animal ได้เลย
    // 3. ในเมธอด Walk ให้พิมพ์ "Dog <ชื่อ> is walking"
    public class Dog : Animal
    {
        public void Walk()
        {
            Debug.Log($"Dog {name} is walking");
        }
    }

    // Guideline:
    // 1. ทำให้ Bird สืบทอดจาก Animal เหมือนกัน
    // 2. ในเมธอด Fly ให้พิมพ์ "Bird <ชื่อ> is flying"
    public class Bird : Animal
    {
        public void Fly()
        {
            Debug.Log($"Bird {name} is flying");
        }
    }

    public class AS01_Inheritance
    {
        public void Start()
        {
            // 1. สร้าง instance ของ class Dog โดยกำหนดชื่อตัวแปรว่า dog
            // + กำหนดชื่อ (name) ว่า "Buddy"
            // + เรียกใช้ method MakeSound() ของ dog
            // + เรียกใช้ method Walk() ของ dog
            Dog dog = new Dog();
            dog.name = "Buddy";
            dog.MakeSound();
            dog.Walk();

            // 2. สร้าง instance ของ class Bird โดยกำหนดชื่อตัวแปรว่า bird
            // + กำหนดชื่อ (name) ว่า "Twitty"
            // + เรียกใช้ method MakeSound() ของ bird
            // + เรียกใช้ method Fly() ของ bird
            Bird bird = new Bird();
            bird.name = "Twitty";
            bird.MakeSound();
            bird.Fly();
        }
    }
}
