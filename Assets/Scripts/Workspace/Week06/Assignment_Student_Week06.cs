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
            var t = System.Type.GetType("Week06.Game.Enemy, Workspace");
            if (t != null)
            {
                var go = new GameObject("DemoEnemy");
                var enemy = go.AddComponent(t);
                t.GetMethod("TakeDamage", new System.Type[] { typeof(int) })?.Invoke(enemy, new object[] { 10 });
                if (go != null) Destroy(go);
            }
        }

        public void Ex04_ExitDemo()
        {
            var t = System.Type.GetType("Week06.Game.Exit, Workspace");
            if (t != null)
            {
                var go = new GameObject("DemoExit");
                go.AddComponent(t);
                if (go != null) Destroy(go);
            }
        }

        public void Ex05_PotionDemo()
        {
            var t = System.Type.GetType("Week06.Game.ItemPotion, Workspace");
            if (t != null)
            {
                var go = new GameObject("DemoPotion");
                go.AddComponent(t);
                if (go != null) Destroy(go);
            }
        }

        #region Homework
        public void HW01_SwordDemo()
        {
            var t = System.Type.GetType("Week06.Game.ItemSword, Workspace");
            if (t != null)
            {
                var go = new GameObject("DemoSword");
                go.AddComponent(t);
                if (go != null) Destroy(go);
            }
        }

        public void HW02_TrapDemo()
        {
            var t = System.Type.GetType("Week06.Game.Trap, Workspace");
            if (t != null)
            {
                var go = new GameObject("DemoTrap");
                go.AddComponent(t);
                if (go != null) Destroy(go);
            }
        }

        public void HW03_WallDemo()
        {
            var t = System.Type.GetType("Week06.Game.Wall, Workspace");
            if (t != null)
            {
                var go = new GameObject("DemoWall");
                go.AddComponent(t);
                if (go != null) Destroy(go);
            }
        }

        public void HW04_ChestDemo()
        {
            var t = System.Type.GetType("Week06.Game.Chest, Workspace");
            if (t != null)
            {
                var go = new GameObject("DemoChest");
                go.AddComponent(t);
                if (go != null) Destroy(go);
            }
        }
        #endregion
    }
}