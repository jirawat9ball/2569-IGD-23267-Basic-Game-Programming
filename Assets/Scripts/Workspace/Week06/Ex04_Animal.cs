using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Ex04
{
    public class Animal
    {
        public string name = "";
        protected string specie = "";
        private int health = 10;

        // Guideline:
        // 1. เขียนเมธอด Feed แบบ public ไม่มีค่าส่งกลับ รับพารามิเตอร์ int food
        // 2. บวก food เข้ากับ health
        // 3. พิมพ์ "<ชื่อ> got <food> food"
        public void Feed(int food)
        {
            health += food;
            Debug.Log($"{name} got {food} food");
        }

        // Guideline:
        // 1. เขียนเมธอด MakeSound แบบ public ไม่มีค่าส่งกลับ ไม่รับพารามิเตอร์
        // 2. ถ้า health มากกว่า 50 ให้พิมพ์ "<ชื่อ> happy!"
        // 3. ถ้าไม่ใช่ (น้อยกว่าหรือเท่ากับ 50) ให้พิมพ์ "<ชื่อ> weak!"
        public void MakeSound()
        {
            if (health > 50)
            {
                Debug.Log($"{name} happy!");
            }
            else
            {
                Debug.Log($"{name} weak!");
            }
        }
    }

    public class Dog : Animal
    {
        // Guideline:
        // 1. เขียน Constructor ของ Dog รับพารามิเตอร์ string name
        // 2. กำหนด specie = "Dog" (ทำได้เพราะ specie เป็น protected คลาสลูกจึงเข้าถึงได้)
        // 3. กำหนด this.name = name;
        //    หมายเหตุ: health เป็น private ของ Animal คลาสลูกอย่าง Dog เข้าถึงไม่ได้
        public Dog(string name)
        {
            specie = "Dog";
            this.name = name;
        }
    }
}
