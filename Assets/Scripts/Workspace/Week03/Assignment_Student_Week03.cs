using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week03
{
    public class Assignment_Student_Week03 : MonoBehaviour, IAssignment
    {
        [Header("Lv01 & Ex01 Variables")]
        public int[] enemyHP = { 100, 80, 60, 40 };
        public int damage = 10;
        public int heal = 10;
        public int target = 2;

        [Header("As03 Variables")]
        public GameObject[] items;

        [Header("Ex02 Variables")]
        public string[] npc1Dialogues =
        {
            "Nice weather today, isn't it?",
            "I heard there are monsters in the cave.",
            "Welcome, traveler!",
            "Have you seen my cat?"
        };

        [Header("As05 / Lv02 / Lv03 / Lv05 Variables")]
        public int n = 5;

        [Header("As06 & Lv04 Variables")]
        public string[] suiteNames = { "Mark I", "Mark II", "Mark III", "Mark IV", "Mark V", "Mark VI" };

        [Header("Ex03 Variables")]
        public GameObject Enemy;
        public int[] HpEnemy = { 10, 20, 30 };

        [Header("Ex04 Variables")]
        public Transform positionToMove;
        public float speed = 10f;

        void Start()
        {
            As01_IronManSuit();
            As02_SpiderManAndBatMan();
            Lv01_AttackTarget(enemyHP, damage, target);
        }

        #region Lecture

        public void As01_IronManSuit()
        {
            string[] IronManSuit = new string[4]
            {
                "Mark I", "Mark II", "Mark III", "Mark IV"
            };

            string TonyStarkWear = IronManSuit[0];
            Debug.Log("TonyStark Wear : " + TonyStarkWear);
            Debug.Log("Room size IronManSuit : " + IronManSuit.Length);
            Debug.Log("===All suit in collection===");

            for (int i = 0; i < IronManSuit.Length; i++)
            {
                Debug.Log(IronManSuit[i]);
            }
        }

        public void As02_SpiderManAndBatMan()
        {
            string[] spiderMan =
            {
                "Classic SpiderMan", "Symbiote SpiderMan", "Iron Spider"
            };
            string[] BatMan = new string[4]
            {
                "Classic BatMan", "Dark Knight", "Batman Beyond", "The Batman"
            };

            Debug.Log("Room size spiderMan : " + spiderMan.Length);
            Debug.Log("===All spiderMan in collection===");
            for (int i = 0; i < spiderMan.Length; i++)
            {
                Debug.Log(spiderMan[i]);
            }

            Debug.Log("Room size BatMan : " + BatMan.Length);
            Debug.Log("===All BatMan in collection===");
            for (int i = 0; i < BatMan.Length; i++)
            {
                Debug.Log(BatMan[i]);
            }
        }

        public void As03_RandomItemDrop(GameObject[] items)
        {
            int index = Random.Range(0, items.Length);
            GameObject picked = items[index];
            Instantiate(picked);
            Debug.Log("Got item: " + picked.name);
        }

        public void As04_ForLoopBasic()
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log("<10 : " + i);
            }

            Debug.Log("======================");

            for (int i = 1; i <= 10; i++)
            {
                Debug.Log("<=10 : " + i);
            }
        }

        public void As05_ForLoopN(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Debug.Log(i);
            }
        }

        public void As06_ForLoopWithArray(string[] suiteNames)
        {
            Debug.Log("======Log by One======");
            for (int i = 0; i < suiteNames.Length; i++)
            {
                Debug.Log(suiteNames[i]);
            }

            Debug.Log("======Log by Two======");
            for (int i = 0; i < suiteNames.Length; i += 2)
            {
                Debug.Log(suiteNames[i]);
            }
        }

        public void As07_WhileLoopBasic()
        {
            int i = 0;
            while (i < 10)
            {
                Debug.Log("while loop : " + i);
                i++;
            }
        }

        #endregion

        #region Homework

        #region Level 1: Simple

        public void Lv01_AttackTarget(int[] enemyHP, int damage, int target)
        {
            int last = enemyHP.Length - 1;

            enemyHP[0] -= damage;
            Debug.Log("FirstEnemy hp :" + enemyHP[0]);

            enemyHP[last] -= damage;
            Debug.Log("LastEnemy hp :" + enemyHP[last]);

            enemyHP[target] -= damage;
            Debug.Log("TargetEnemy " + target + " hp :" + enemyHP[target]);
        }

        public void Lv02_MultiplicationTable(int n)
        {
            for (int i = 1; i <= 12; i++)
            {
                Debug.Log(n + " x " + i + " = " + (n * i));
            }
        }

        public void Lv03_WhileLoopN(int n)
        {
            int i = 0;
            while (i < n)
            {
                Debug.Log(i);
                i++;
            }
        }

        public void Lv04_WhileLoopStep(string[] suiteNames)
        {
            Debug.Log("======Log by One======");
            int i = 0;
            while (i < suiteNames.Length)
            {
                Debug.Log(suiteNames[i]);
                i++;
            }

            Debug.Log("======Log by Two======");
            i = 0;
            while (i < suiteNames.Length)
            {
                Debug.Log(suiteNames[i]);
                i += 2;
            }
        }

        public void Lv05_WhileLoopSum(int n)
        {
            int i = 1;
            int sum = 0;
            while (i <= n)
            {
                sum += i;
                i++;
            }
            Debug.Log("Sum of n from 0 to " + n + " is " + sum);
        }

        #endregion

        #region Level 2: Moderate

        public void Ex01_HealTarget(int[] enemyHP, int heal, int target)
        {
            int last = enemyHP.Length - 1;

            enemyHP[0] += heal;
            Debug.Log("FirstEnemy hp :" + enemyHP[0]);

            enemyHP[last] += heal;
            Debug.Log("LastEnemy hp :" + enemyHP[last]);

            enemyHP[target] += heal;
            Debug.Log("TargetEnemy " + target + " hp :" + enemyHP[target]);
        }

        public void Ex02_RandomDialogue(string[] npc1Dialogues)
        {
            int index = Random.Range(0, npc1Dialogues.Length);
            Debug.Log(npc1Dialogues[index]);
        }

        public void Ex03_InstantiateEnemies(GameObject Enemy, int[] HpEnemy)
        {
            for (int i = 0; i < HpEnemy.Length; i++)
            {
                GameObject spawned = Instantiate(Enemy);
                spawned.transform.position = new Vector3(i + 1, 0f, 0f);
                Debug.Log("new enemy at position x = " + (i + 1));
            }
        }

        public void Ex04_MoveToTarget(Transform positionToMove, float speed)
        {
            int safety = 0;
            while (transform.position.x < positionToMove.position.x && safety < 10000)
            {
                transform.Translate(Vector3.right * speed * 0.1f);
                Debug.Log(transform.position.x.ToString("F2"));
                safety++;
            }
        }

        #endregion

        #endregion // End Homework
    }
}
