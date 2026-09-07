using System.Collections;
using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week03
{
    public class Assignment_Student_Week03 : MonoBehaviour, IAssignment
    {
        #region Lecture Variables
        [Header("As01/02 Variables")]
        //Impremment it by yourself
        
        [Header("As03 Variables")]
        public GameObject[] items;

        [Header("As05 / Lv05 / Lv06 / Ex05 Variables")]
        public int n = 5;

        [Header("As06 & Lv07 & Ex04 Variables")]
        public string[] suiteNames = { "Mark I", "Mark II", "Mark III", "Mark IV", "Mark V", "Mark VI" };

        [Header("As07 Variables")]
        public GameObject Enemy;
        public int[] HpEnemy = { 10, 20, 30 };

        [Header("As09 Variables")]
        public Transform character;
        public Transform positionToMove;
        public float speed = 10f;

        #endregion

        #region Level 1 Variables

        [Header("Lv02 Variables")]
        public string[] inventory = { "Potion", "Sword", "Bow", "Shield" };

        [Header("Lv03 & Ex02 Variables")]
        public string[] npc1Dialogues =
        {
            "Nice weather today, isn't it?",
            "I heard there are monsters in the cave.",
            "Welcome, traveler!",
            "Have you seen my cat?"
        };

        [Header("Lv04 Variables")]
        public int[] enemyHP = { 100, 80, 60, 40 };
        public int damage = 10;
        public int target = 2;

        #endregion

        #region Level 2 Variables

        [Header("Ex01 Variables")]
        public int heal = 10;
        public int maxHP = 100;

        [Header("Ex02 Variables")]
        public string[] npc2Dialogues =
        {
            "Yes, it's a great day for an adventure!",
            "I will prepare my sword and shield.",
            "Thank you, good to see you!",
            "No, I haven't seen any cats around."
        };

        [Header("Ex03 Variables")]
        public int spawnCount = 3;
        public float spawnSpacing = 2f;

        #endregion

        void Start()
        {
            As01_IronManSuit();
            As02_SpiderManAndBatMan();
            As03_RandomItemDrop(items);
            As04_ForLoopBasic();
            As05_ForLoopN(n);
            As06_ForLoopWithArray(suiteNames);
            As07_InstantiateEnemies(Enemy, HpEnemy);
            As08_WhileLoopBasic();
            StartCoroutine(As09_MoveToTarget(character, positionToMove, speed));

            Lv01_SetArrayValues();
            Lv02_InspectArray(inventory);
            Lv03_RandomDialogue(npc1Dialogues);
            Lv04_AttackTarget(enemyHP, damage, target);
            Lv05_MultiplicationTable(n);
            Lv06_WhileLoopN(n);
            Lv07_ForLoopReverse(suiteNames);

            Ex01_HealTarget(enemyHP, heal, target, maxHP);
            Ex02_DialogueInteraction(npc1Dialogues, npc2Dialogues);
            Ex03_SpawnEnemiesWithSpacing(Enemy, spawnCount, spawnSpacing);
            Ex04_WhileLoopStep(suiteNames);
            Ex05_WhileLoopSum(n);
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
            Instantiate(picked,new Vector3(0, 3, 0), Quaternion.identity);
            Debug.Log("Got item : " + picked.name);
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

        public void As07_InstantiateEnemies(GameObject Enemy, int[] HpEnemy)
        {
            for (int i = 0; i < HpEnemy.Length; i++)
            {
                GameObject spawned = Instantiate(Enemy);
                spawned.transform.position = new Vector3(i + 1, 0f, 0f);
                Debug.Log("new enemy at position x = " + (i + 1));
            }
        }

        public void As08_WhileLoopBasic()
        {
            int i = 0;
            while (i < 10)
            {
                Debug.Log("while loop : " + i);
                i++;
            }
        }

        public IEnumerator As09_MoveToTarget(Transform character, Transform target, float speed)
        {
            float timer = 0f;
            while (character.position.x < target.position.x)
            {
                character.Translate(Vector3.right * speed * 0.1f);
                Debug.Log(character.position.x.ToString("F2"));
                timer += Time.deltaTime;
                yield return null;
            }
            Debug.Log("Time : " + timer);
        }

        #endregion

        #region Homework

        #region Level 1: Simple

        public void Lv01_SetArrayValues()
        {
            string[] weapons = new string[3];
            weapons[0] = "Sword";
            weapons[1] = "Axe";
            weapons[2] = "Bow";

            int[] damage = new int[3];
            damage[0] = 100;
            damage[1] = 200;
            damage[2] = 300;

            Debug.Log(weapons[0] + " damage : " + damage[0]);
            Debug.Log(weapons[1] + " damage : " + damage[1]);
            Debug.Log(weapons[2] + " damage : " + damage[2]);
        }

        public void Lv02_InspectArray(string[] items)
        {
            Debug.Log("Total items : " + items.Length);
            Debug.Log("First item : " + items[0]);
            Debug.Log("Middle item : " + items[items.Length / 2]);
            Debug.Log("Last item : " + items[items.Length - 1]);
        }

        public void Lv03_RandomDialogue(string[] npc1Dialogues)
        {
            int index = Random.Range(0, npc1Dialogues.Length);
            Debug.Log(npc1Dialogues[index]);
        }

        public void Lv04_AttackTarget(int[] enemyHP, int damage, int target)
        {
            int last = enemyHP.Length - 1;

            enemyHP[0] -= damage;
            Debug.Log("FirstEnemy hp : " + enemyHP[0]);

            enemyHP[last] -= damage;
            Debug.Log("LastEnemy hp : " + enemyHP[last]);

            enemyHP[target] -= damage;
            Debug.Log("TargetEnemy " + target + " hp : " + enemyHP[target]);
        }

        public void Lv05_MultiplicationTable(int n)
        {
            for (int i = 1; i <= 12; i++)
            {
                Debug.Log(n + " x " + i + " = " + (n * i));
            }
        }

        public void Lv06_WhileLoopN(int n)
        {
            int i = 0;
            while (i < n)
            {
                Debug.Log(i);
                i++;
            }
        }

        public void Lv07_ForLoopReverse(string[] suiteNames)
        {
            Debug.Log("======Log Reverse======");
            for (int i = suiteNames.Length - 1; i >= 0; i--)
            {
                Debug.Log(suiteNames[i]);
            }
        }

        #endregion

        #region Level 2: Moderate

        public void Ex01_HealTarget(int[] enemyHP, int heal, int target, int maxHP)
        {
            int last = enemyHP.Length - 1;

            enemyHP[0] = Mathf.Min(enemyHP[0] + heal, maxHP);
            Debug.Log("FirstEnemy hp : " + enemyHP[0]);

            enemyHP[last] = Mathf.Min(enemyHP[last] + heal, maxHP);
            Debug.Log("LastEnemy hp : " + enemyHP[last]);

            enemyHP[target] = Mathf.Min(enemyHP[target] + heal, maxHP);
            Debug.Log("TargetEnemy " + target + " hp : " + enemyHP[target]);
        }

        public void Ex02_DialogueInteraction(string[] npc1Dialogues, string[] npc2Dialogues)
        {
            int index1 = Random.Range(0, npc1Dialogues.Length);
            Debug.Log("NPC1 : " + npc1Dialogues[index1]);

            int index2 = Random.Range(0, npc2Dialogues.Length);
            Debug.Log("NPC2 : " + npc2Dialogues[index2]);
        }

        public void Ex03_SpawnEnemiesWithSpacing(GameObject Enemy, int count, float spacing)
        {
            for (int i = 0; i < count; i++)
            {
                float posX = (i + 1) * spacing;
                GameObject spawned = Instantiate(Enemy);
                spawned.transform.position = new Vector3(posX, 0f, 0f);
                Debug.Log("Spawn enemy at position x : " + posX);
            }
        }

        public void Ex04_WhileLoopStep(string[] suiteNames)
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

        public void Ex05_WhileLoopSum(int n)
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

        #endregion // End Homework
    }
}
