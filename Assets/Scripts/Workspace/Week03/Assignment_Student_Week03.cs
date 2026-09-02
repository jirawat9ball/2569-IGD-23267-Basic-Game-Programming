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
            // เปิด-ปิดคอมเมนต์เพื่อทดสอบทีละข้อได้
            Ex01_IronManSuit();
            Ex02_SpiderManAndBatMan();
            Ex03_AttackTarget(enemyHP, damage, target);
        }

        #region Array (ข้อ 1-6)

        public void Ex01_IronManSuit()
        {
            // Guideline: ประกาศ string[] IronManSuit ขนาด 7 ("Mark I"–"Mark VII")
            // เก็บช่องแรกไว้ใน TonyStarkWear แล้วพิมพ์ "TonyStark Wear : <ค่า>"
            // พิมพ์ "Room size IronManSuit : <ขนาด>"
            // พิมพ์ "===All suit in collection===" แล้ววนลูปพิมพ์ทุกชุด
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
            // Guideline: spiderMan ประกาศแบบไม่ระบุขนาด (5 ชุด), BatMan ประกาศด้วย new string[4]
            // พิมพ์ขนาดและเนื้อหาของทั้งสอง array
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
            // Guideline: โจมตี (ลด HP) ตัวแรก -> ตัวสุดท้าย -> ตัวเป้าหมาย ตามลำดับ
            // พิมพ์ "FirstEnemy hp :<hp>", "LastEnemy hp :<hp>", "TargetEnemy <target> hp :<hp>"
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
            // Guideline: สุ่ม item ด้วย Random.Range แล้ว Instantiate จากนั้นพิมพ์ "Got item: <ชื่อ>"
            int index = Random.Range(0, items.Length);
            GameObject picked = items[index];
            Instantiate(picked);
            Debug.Log("Got item: " + picked.name);
        }

        public void Ex05_HealTarget(int[] enemyHP, int heal, int target)
        {
            // Guideline: เหมือนข้อ 3 แต่บวก HP แทน (ตัวแรก -> ตัวสุดท้าย -> ตัวเป้าหมาย)
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
            // Guideline: สุ่ม index ด้วย Random.Range แล้วพิมพ์บทสนทนานั้น
            int index = Random.Range(0, npc1Dialogues.Length);
            Debug.Log(npc1Dialogues[index]);
        }

        #endregion

        #region For Loop (ข้อ 7-10)

        public void Ex07_ForLoopBasic()
        {
            // Guideline: ลูป 1 i 0..9 พิมพ์ "<10 : i" | คั่นด้วย "======================" | ลูป 2 i 1..10 พิมพ์ "<=10 : i"
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
            // Guideline: วนลูป for จาก 0 ถึง n-1 พิมพ์ตัวเลขรอบนั้น
            for (int i = 0; i < n; i++)
            {
                Debug.Log(i);
            }
        }

        public void Ex09_ForLoopStep(string[] suiteNames)
        {
            // Guideline: "======Log by One======" วนพิมพ์ทุกชุด | "======Log by Two======" วนพิมพ์ข้ามทีละ 2 (index 0,2,4...)
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
            // Guideline: วนลูป for พิมพ์สูตรคูณแม่ n ตั้งแต่ 1 ถึง 12 รูปแบบ "n x i = (n*i)"
            for (int i = 1; i <= 12; i++)
            {
                Debug.Log(n + " x " + i + " = " + (n * i));
            }
        }

        #endregion

        #region While Loop (ข้อ 11-14)

        public void Ex11_WhileLoopBasic()
        {
            // Guideline: วนลูป while 0..9 พิมพ์ "while loop : i"
            int i = 0;
            while (i < 10)
            {
                Debug.Log("while loop : " + i);
                i++;
            }
        }

        public void Ex12_WhileLoopN(int n)
        {
            // Guideline: วนลูป while พิมพ์ตัวเลข 0 ถึง n-1
            int i = 0;
            while (i < n)
            {
                Debug.Log(i);
                i++;
            }
        }

        public void Ex13_WhileLoopStep(string[] suiteNames)
        {
            // Guideline: เหมือนข้อ 9 แต่ใช้ while loop
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
            // Guideline: ใช้ while loop หาผลรวม 1..n แล้วพิมพ์ "ผลรวมของ n จาก 0 ถึง <n> คือ <sum>"
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
            // Guideline: วนลูป for ตามจำนวน HpEnemy, Instantiate โดยแกน X เพิ่มทีละ 1 (เริ่ม x=1)
            // พิมพ์ "new enemy at position x = <x>"
            for (int i = 0; i < HpEnemy.Length; i++)
            {
                GameObject spawned = Instantiate(Enemy);
                spawned.transform.position = new Vector3(i + 1, 0f, 0f);
                Debug.Log("new enemy at position x = " + (i + 1));
            }
        }

        public void Ex16_MoveToTarget(Transform positionToMove, float speed)
        {
            // Guideline: วนลูปจนกว่า x ปัจจุบัน >= positionToMove.position.x
            // เคลื่อนที่ด้วย transform.Translate(Vector3.right * speed * 0.1f)
            // พิมพ์ transform.position.x.ToString("F2") ทุกรอบ
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
