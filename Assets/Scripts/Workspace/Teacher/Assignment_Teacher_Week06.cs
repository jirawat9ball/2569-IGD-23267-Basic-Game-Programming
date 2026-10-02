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

        public void Ex03_EnemyDemo()
        {
            var go = new GameObject("TeacherEnemyDemo");
            var enemy = go.AddComponent<Teacher.Ex03.Enemy>();
            enemy.TakeDamage(10);
            if (go != null) Destroy(go);
        }

        public void Ex04_ExitDemo()
        {
            var go = new GameObject("TeacherExitDemo");
            var exit = go.AddComponent<Teacher.Ex04.Exit>();
            if (go != null) Destroy(go);
        }

        public void Ex05_PotionDemo()
        {
            var go = new GameObject("TeacherPotionDemo");
            var potion = go.AddComponent<Teacher.Ex05.ItemPotion>();
            if (go != null) Destroy(go);
        }

        #region Homework
        public void HW01_SwordDemo()
        {
            var go = new GameObject("TeacherSwordDemo");
            var sword = go.AddComponent<Teacher.HW01.ItemSword>();
            if (go != null) Destroy(go);
        }

        public void HW02_TrapDemo()
        {
            var go = new GameObject("TeacherTrapDemo");
            var trap = go.AddComponent<Teacher.HW02.Trap>();
            if (go != null) Destroy(go);
        }

        public void HW03_WallDemo()
        {
            var go = new GameObject("TeacherWallDemo");
            var wall = go.AddComponent<Teacher.HW03.Wall>();
            if (go != null) Destroy(go);
        }

        public void HW04_ChestDemo()
        {
            var go = new GameObject("TeacherChestDemo");
            var chest = go.AddComponent<Teacher.HW04.Chest>();
            if (go != null) Destroy(go);
        }
        #endregion
    }
}