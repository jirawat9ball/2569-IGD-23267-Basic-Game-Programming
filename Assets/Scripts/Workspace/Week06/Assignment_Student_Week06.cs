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
            Ex03_EnemyDemo();
            Ex04_ExitDemo();
            Ex05_PotionDemo();
        }

        public void Ex01_CarDemo()
        {
            // Guideline: (โค้ดของ Car ทำงานใน Start() และ Update() ของไฟล์ AS01_Car.cs)
        }

        public void Ex02_DogDemo()
        {
            new Ex02.AS02_ClassConstructor().Start();
        }

        public void Ex03_EnemyDemo()
        {
            // จำลองการเรียกใช้ Enemy
            var go = new GameObject("DemoEnemy");
            var enemy = go.AddComponent<Ex03.Enemy>();
            enemy.TakeDamage(10);
            if (go != null) Destroy(go);
        }

        public void Ex04_ExitDemo()
        {
            // จำลองการเรียกใช้ Exit
            var go = new GameObject("DemoExit");
            var exit = go.AddComponent<Ex04.Exit>();
            if (go != null) Destroy(go);
        }

        public void Ex05_PotionDemo()
        {
            // จำลองการเรียกใช้ ItemPotion
            var go = new GameObject("DemoPotion");
            var potion = go.AddComponent<Ex05.ItemPotion>();
            if (go != null) Destroy(go);
        }
    }
}