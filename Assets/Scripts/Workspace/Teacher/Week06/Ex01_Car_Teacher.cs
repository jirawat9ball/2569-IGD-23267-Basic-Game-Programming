using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;

namespace Week06.Teacher.Ex01
{
    public class Car : MonoBehaviour
    {
        public string name;
        public string color;
        public float speed;

        void Start()
        {
            name = "civic";
            color = "black";
            speed = 110;
        }

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