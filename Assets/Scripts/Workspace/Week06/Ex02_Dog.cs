using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Ex02
{
    public class Dog
    {
        public string name;
        public string breed;
        public int age;

        // Guideline:
        // 1. เขียน Constructor ของคลาส Dog ให้รับพารามิเตอร์ 3 ตัว คือ name, breed, age
        // 2. Constructor ใช้ชื่อเดียวกับคลาสเสมอ และไม่มี return type
        // 3. ใช้ this.name = name; เพื่อบอกว่าตัวไหนคือฟิลด์ของคลาส ตัวไหนคือพารามิเตอร์
        public Dog(string name, string breed, int age)
        {
            this.name = name;
            this.breed = breed;
            this.age = age;
        }

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
    }
}
