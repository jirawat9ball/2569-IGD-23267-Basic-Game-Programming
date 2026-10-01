using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;

namespace Week06.Ex01
{
    public class Car : MonoBehaviour
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


        // Guideline:
        // 3. กำหนดค่าเริ่มต้นให้กับตัวแปรใน Start()
        void Start()
        {
            name = "civic";
            color = "black";
            speed = 110;
        }

        // Guideline:
        // 4. ใน Update() ตรวจจับการกดปุ่มเพื่อสั่งงานพฤติกรรมของรถ
        //    - Spacebar -> Honk()
        //    - A        -> Turn()
        //    - W        -> Move()
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Honk();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Turn();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Move();
            }
        }
    }
}
