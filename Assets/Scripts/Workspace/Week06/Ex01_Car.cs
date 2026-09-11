using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Ex01
{
    public class Car
    {
        // Guideline:
        // 1. ประกาศฟิลด์ 3 ตัวแบบ public ได้แก่
        //    name (string), color (string), speed (float)
        public string name;
        public string color;
        public float speed;

        // Guideline:
        // 2. เขียนเมธอด 3 ตัวแบบ public ไม่มีค่าส่งกลับ ไม่รับพารามิเตอร์
        //    Move()  -> พิมพ์ "Car is moving"
        //    Turn()  -> พิมพ์ "Car is turning"
        //    Honk()  -> พิมพ์ "Car is honking"
        public void Move()
        {
            Debug.Log("Car is moving");
        }

        public void Turn()
        {
            Debug.Log("Car is turning");
        }

        public void Honk()
        {
            Debug.Log("Car is honking");
        }
    }
}
