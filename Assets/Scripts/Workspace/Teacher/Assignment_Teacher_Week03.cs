using System.Collections;
using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week03
{
    public class Assignment_Teacher_Week03 : MonoBehaviour, IAssignment
    {
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

        public void Lv06_ForLoopReverse(string[] suiteNames)
        {
            Debug.Log("======Log Reverse======");
            for (int i = suiteNames.Length - 1; i >= 0; i--)
            {
                Debug.Log(suiteNames[i]);
            }
        }

        public void Lv07_FindHighestScore(int[] scores)
        {
            if (scores == null || scores.Length == 0) return;

            int highest = scores[0];
            for (int i = 1; i < scores.Length; i++)
            {
                if (scores[i] > highest)
                {
                    highest = scores[i];
                }
            }
            Debug.Log("Highest score : " + highest);
        }

        public void Lv08_CalculateTotalScore(int[] scores)
        {
            int total = 0;
            for (int i = 0; i < scores.Length; i++)
            {
                total += scores[i];
            }
            Debug.Log("Total score : " + total);
        }

        public void Lv09_WhileLoopN(int n)
        {
            int i = 0;
            while (i < n)
            {
                Debug.Log(i);
                i++;
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
            int rounds = Mathf.Min(npc1Dialogues.Length, npc2Dialogues.Length);
            for (int i = 0; i < rounds; i++)
            {
                Debug.Log("NPC1 : " + npc1Dialogues[i]);
                Debug.Log("NPC2 : " + npc2Dialogues[i]);
            }
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
