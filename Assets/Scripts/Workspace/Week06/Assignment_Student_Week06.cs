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
            HW01_SwordDemo();
            HW02_TrapDemo();
            HW03_WallDemo();
            HW04_ChestDemo();
        }

        public void Ex01_CarDemo()
        {
            var go = new GameObject("DemoCar");
            var car = go.AddComponent<Ex01.Car>();
            if (go != null) Destroy(go);
        }

        public void Ex02_DogDemo()
        {
            new Ex02.AS02_ClassConstructor().Start();
        }

        public void Ex03_EnemyDemo()
        {
            // จำลองการเรียกใช้ Enemy จาก Game folder
            var go = new GameObject("DemoEnemy");
            var enemy = go.AddComponent<Game.Enemy>();
            enemy.TakeDamage(10);
            if (go != null) Destroy(go);
        }

        public void Ex04_ExitDemo()
        {
            // จำลองการเรียกใช้ Exit จาก Game folder
            var go = new GameObject("DemoExit");
            var exit = go.AddComponent<Game.Exit>();
            if (go != null) Destroy(go);
        }

        public void Ex05_PotionDemo()
        {
            // จำลองการเรียกใช้ ItemPotion จาก Game folder
            var go = new GameObject("DemoPotion");
            var potion = go.AddComponent<Game.ItemPotion>();
            if (go != null) Destroy(go);
        }

        #region Homework
        public void HW01_SwordDemo()
        {
            var go = new GameObject("DemoSword");
            var sword = go.AddComponent<Game.ItemSword>();
            if (go != null) Destroy(go);
        }

        public void HW02_TrapDemo()
        {
            var go = new GameObject("DemoTrap");
            var trap = go.AddComponent<Game.Trap>();
            if (go != null) Destroy(go);
        }

        public void HW03_WallDemo()
        {
            var go = new GameObject("DemoWall");
            var wall = go.AddComponent<Game.Wall>();
            if (go != null) Destroy(go);
        }

        public void HW04_ChestDemo()
        {
            var go = new GameObject("DemoChest");
            var chest = go.AddComponent<Game.Chest>();
            if (go != null) Destroy(go);
        }
        #endregion
    }
}