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

        private static System.Type FindType(string typeName, string altNamespace = null)
        {
            var t = System.Type.GetType($"Week06.Game.{typeName}, Workspace")
                ?? System.Type.GetType($"Week06.Game.{typeName}, Assembly-CSharp")
                ?? System.Type.GetType($"Week06.Game.{typeName}");
            if (t != null) return t;

            if (!string.IsNullOrEmpty(altNamespace))
            {
                t = System.Type.GetType($"{altNamespace}.{typeName}, Workspace")
                    ?? System.Type.GetType($"{altNamespace}.{typeName}, Assembly-CSharp")
                    ?? System.Type.GetType($"{altNamespace}.{typeName}");
            }
            return t;
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
            var go = new GameObject("DemoCar");
            var car = go.AddComponent<Ex01.Car>();
            SafeDestroy(go);
        }

        public void Ex02_DogDemo()
        {
            new Ex02.AS02_ClassConstructor().Start();
        }

        public void Ex03_EnemyDemo()
        {
            var t = FindType("Enemy", "Week06.Ex03");
            if (t != null)
            {
                var go = new GameObject("DemoEnemy");
                var enemy = go.AddComponent(t);
                t.GetMethod("TakeDamage", new System.Type[] { typeof(int) })?.Invoke(enemy, new object[] { 10 });
                SafeDestroy(go);
            }
        }

        public void Ex04_ExitDemo()
        {
            var t = FindType("Exit", "Week06.Ex04");
            if (t != null)
            {
                var go = new GameObject("DemoExit");
                go.AddComponent(t);
                SafeDestroy(go);
            }
        }

        public void Ex05_PotionDemo()
        {
            var t = FindType("ItemPotion", "Week06.Ex05");
            if (t != null)
            {
                var go = new GameObject("DemoPotion");
                go.AddComponent(t);
                SafeDestroy(go);
            }
        }

        #region Homework
        public void HW01_SwordDemo()
        {
            var t = FindType("ItemSword");
            if (t != null)
            {
                var go = new GameObject("DemoSword");
                go.AddComponent(t);
                SafeDestroy(go);
            }
        }

        public void HW02_TrapDemo()
        {
            var t = FindType("Trap");
            if (t != null)
            {
                var go = new GameObject("DemoTrap");
                go.AddComponent(t);
                SafeDestroy(go);
            }
        }

        public void HW03_WallDemo()
        {
            var t = FindType("Wall");
            if (t != null)
            {
                var go = new GameObject("DemoWall");
                go.AddComponent(t);
                SafeDestroy(go);
            }
        }

        public void HW04_ChestDemo()
        {
            var t = FindType("Chest");
            if (t != null)
            {
                var go = new GameObject("DemoChest");
                go.AddComponent(t);
                SafeDestroy(go);
            }
        }
        #endregion

        private void SafeDestroy(GameObject go)
        {
            if (go == null) return;
            if (Application.isPlaying)
                Destroy(go);
            else
                DestroyImmediate(go);
        }
    }
}