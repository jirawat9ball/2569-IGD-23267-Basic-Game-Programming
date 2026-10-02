using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06
{
    public class Assignment_Teacher_Week06 : MonoBehaviour, IAssignment
    {
        public void Ex01_CarDemo()
        {
            // Guideline: (โค้ดของ Car ทำงานใน Start() และ Update() ของไฟล์ Ex01_Car.cs)
        }

        public void Ex02_DogDemo()
        {
            new Teacher.Ex02.AS02_ClassConstructor().Start();
        }
    }
}