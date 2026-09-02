using UnityEngine;
using Debug = AssignmentSystem.Services.AssignmentDebugConsole;

namespace Week03
{
    public class Assignment_Student_Week03 : MonoBehaviour, IAssignment
    {
        [Header("Ex03 & Ex05 Variables")]
        public int[] enemyHP = { 100, 80, 60, 40 };
        public int damage = 10;
        public int heal = 10;
        public int target = 2;

        [Header("Ex04 Variables")]
        public GameObject[] items;

        [Header("Ex06 Variables")]
        public string[] npc1Dialogues =
        {
            "Nice weather today, isn't it?",
            "I heard there are monsters in the cave.",
            "Welcome, traveler!",
            "Have you seen my cat?"
        };

        [Header("Ex08 / Ex10 / Ex12 / Ex14 Variables")]
        public int n = 5;

        [Header("Ex09 & Ex13 Variables")]
        public string[] suiteNames = { "Mark I", "Mark II", "Mark III", "Mark IV", "Mark V", "Mark VI" };

        [Header("Ex15 Variables")]
        public GameObject Enemy;
        public int[] HpEnemy = { 10, 20, 30 };

        [Header("Ex16 Variables")]
        public Transform positionToMove;
        public float speed = 10f;

        void Start()
        {
            Ex01_IronManSuit();
            Ex02_SpiderManAndBatMan();
            Ex03_AttackTarget(enemyHP, damage, target);
        }

        #region Array (ข้อ 1-6)

        public void Ex01_IronManSuit()
        {
            string[] IronManSuit = new string[7]
            {
                "Mark I", "Mark II", "Mark III", "Mark IV", "Mark V", "Mark VI", "Mark VII"
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

        public void Ex02_SpiderManAndBatMan()
        {
            string[] spiderMan =
            {
                "Classic SpiderMan", "Symbiote SpiderMan", "Iron Spider", "Miles Morales", "Spider-Man 2099"
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

        public void Ex03_AttackTarget(int[] enemyHP, int damage, int target)
        {
            int last = enemyHP.Length - 1;

            enemyHP[0] -= damage;
            Debug.Log("FirstEnemy hp :" + enemyHP[0]);

            enemyHP[last] -= damage;
            Debug.Log("LastEnemy hp :" + enemyHP[last]);

            enemyHP[target] -= damage;
            Debug.Log("TargetEnemy " + target + " hp :" + enemyHP[target]);
        }

        public void Ex04_RandomItemDrop(GameObject[] items)
        {
            int index = Random.Range(0, items.Length);
            GameObject picked = items[index];
            Instantiate(picked);
            Debug.Log("Got item: " + picked.name);
        }

        public void Ex05_HealTarget(int[] enemyHP, int heal, int target)
        {
            int last = enemyHP.Length - 1;

            enemyHP[0] += heal;
            Debug.Log("FirstEnemy hp :" + enemyHP[0]);

            enemyHP[last] += heal;
            Debug.Log("LastEnemy hp :" + enemyHP[last]);

            enemyHP[target] += heal;
            Debug.Log("TargetEnemy " + target + " hp :" + enemyHP[target]);
        }

        public void Ex06_RandomDialogue(string[] npc1Dialogues)
        {
            int index = Random.Range(0, npc1Dialogues.Length);
            Debug.Log(npc1Dialogues[index]);
        }

        #endregion

        #region For Loop (ข้อ 7-10)

        public void Ex07_ForLoopBasic()
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

        public void Ex08_ForLoopN(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Debug.Log(i);
            }
        }

        public void Ex09_ForLoopStep(string[] suiteNames)
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

        public void Ex10_MultiplicationTable(int n)
        {
            for (int i = 1; i <= 12; i++)
            {
                Debug.Log(n + " x " + i + " = " + (n * i));
            }
        }

        #endregion

        #region While Loop (ข้อ 11-14)

        public void Ex11_WhileLoopBasic()
        {
            int i = 0;
            while (i < 10)
            {
                Debug.Log("while loop : " + i);
                i++;
            }
        }

        public void Ex12_WhileLoopN(int n)
        {
            int i = 0;
            while (i < n)
            {
                Debug.Log(i);
                i++;
            }
        }

        public void Ex13_WhileLoopStep(string[] suiteNames)
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

        public void Ex14_WhileLoopSum(int n)
        {
            int i = 1;
            int sum = 0;
            while (i <= n)
            {
                sum += i;
                i++;
            }
            Debug.Log("ผลรวมของ n จาก 0 ถึง " + n + " คือ " + sum);
        }

        #endregion

        #region Instantiate & Translate (ข้อ 15-16)

        public void Ex15_InstantiateEnemies(GameObject Enemy, int[] HpEnemy)
        {
            for (int i = 0; i < HpEnemy.Length; i++)
            {
                GameObject spawned = Instantiate(Enemy);
                spawned.transform.position = new Vector3(i + 1, 0f, 0f);
                Debug.Log("new enemy at position x = " + (i + 1));
            }
        }

        public void Ex16_MoveToTarget(Transform positionToMove, float speed)
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
    }
}
