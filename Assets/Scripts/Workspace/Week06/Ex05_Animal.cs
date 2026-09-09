using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Ex05
{
    public class Animal
    {
        // Guideline:
        // 1. เติมคำว่า virtual หน้าเมธอด MakeSound เพื่อเปิดให้คลาสลูกเขียนทับได้
        // 2. เนื้อในพิมพ์ "Generic animal sound"
        public virtual void MakeSound()
        {
            Debug.Log("Generic animal sound");
        }
    }

    public class Dog : Animal
    {
        // Guideline:
        // 1. เติมคำว่า override หน้าเมธอด MakeSound เพื่อเขียนทับของคลาสแม่
        // 2. เปลี่ยนข้อความเป็น "Woof!"
        public override void MakeSound()
        {
            Debug.Log("Woof!");
        }
    }
}
