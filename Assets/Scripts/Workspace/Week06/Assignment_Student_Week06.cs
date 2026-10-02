using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06
{
    public class Assignment_Student_Week06 : MonoBehaviour, IAssignment
    {
        void Start()
        {
            Ex01_CarDemo();
            Ex02_DogDemo();
        }

        public void Ex01_CarDemo()
        {
            // Guideline: (โค้ดของ Car ทำงานใน Start() และ Update() ของไฟล์ AS01_Car.cs)
        }

        public void Ex02_DogDemo()
        {
            new Ex02.AS02_ClassConstructor().Start();
        }
    }
}