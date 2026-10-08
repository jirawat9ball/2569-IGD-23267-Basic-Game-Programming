using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;

namespace Week06.Teacher.Ex01
{
    public class Car : MonoBehaviour
    {
        public string Name;
        public Color carColor;
        public float Speed;

        void Start()
        {
            Name = "civic";
            carColor = Color.black;
            Speed = 110;

            gameObject.name = Name;
            gameObject.GetComponent<MeshRenderer>().material.color = carColor;
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