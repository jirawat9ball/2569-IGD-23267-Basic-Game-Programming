using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Ex03
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
}
