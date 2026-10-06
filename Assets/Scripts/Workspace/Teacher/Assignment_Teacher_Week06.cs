using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06
{
    public class Assignment_Teacher_Week06 : MonoBehaviour, IAssignment
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

        private static void SafeDestroy(GameObject go)
        {
            if (go == null) return;
            if (Application.isPlaying)
                Destroy(go);
            else
                DestroyImmediate(go);
        }

        public void Ex01_CarDemo()
        {
            var go = new GameObject("TeacherCarDemo");
            var car = go.AddComponent<Teacher.Ex01.Car>();
            SafeDestroy(go);
        }

        public void Ex02_DogDemo()
        {
            new Teacher.Ex02.AS02_ClassConstructor().Start();
        }

        public void Ex03_EnemyDemo()
        {
            var go = new GameObject("TeacherEnemyDemo");
            var enemy = go.AddComponent<Teacher.Ex03.Enemy>();
            enemy.TakeDamage(10);
            SafeDestroy(go);
        }

        public void Ex04_ExitDemo()
        {
            var go = new GameObject("TeacherExitDemo");
            var exit = go.AddComponent<Teacher.Ex04.Exit>();
            SafeDestroy(go);
        }

        public void Ex05_PotionDemo()
        {
            var go = new GameObject("TeacherPotionDemo");
            var potion = go.AddComponent<Teacher.Ex05.ItemPotion>();
            SafeDestroy(go);
        }

        #region Homework
        public void HW01_SwordDemo()
        {
            var go = new GameObject("TeacherSwordDemo");
            var sword = go.AddComponent<Teacher.HW01.ItemSword>();
            SafeDestroy(go);
        }

        public void HW02_TrapDemo()
        {
            var go = new GameObject("TeacherTrapDemo");
            var trap = go.AddComponent<Teacher.HW02.Trap>();
            SafeDestroy(go);
        }

        public void HW03_WallDemo()
        {
            var go = new GameObject("TeacherWallDemo");
            var wall = go.AddComponent<Teacher.HW03.Wall>();
            SafeDestroy(go);
        }

        public void HW04_ChestDemo()
        {
            var go = new GameObject("TeacherChestDemo");
            var chest = go.AddComponent<Teacher.HW04.Chest>();
            SafeDestroy(go);
        }
        #endregion
    }
}