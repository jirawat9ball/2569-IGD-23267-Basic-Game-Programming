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

        [Header("As05 / Lv05 / Lv09 / Ex05 Variables")]
        public int n = 5;

        [Header("As06 & Lv06 & Ex04 Variables")]
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
            "Yes, I heard there are monsters inside!",
            "Welcome, traveler!",
            "No, I haven't seen any cats around."
        };

        [Header("Lv04 Variables")]
        public int[] enemyHP = { 100, 80, 60, 40 };
        public int damage = 10;
        public int target = 2;

        [Header("Lv07 & Lv08 Variables")]
        public int[] scores = { 10, 50, 30, 90, 40 };

        #endregion

        #region Level 2 Variables

        [Header("Ex01 Variables")]
        public int heal = 10;
        public int maxHP = 100;

        [Header("Ex02 Variables")]
        public string[] npc2Dialogues =
        {
            "Yes, it's a great day for an adventure!",
            "Are you ready to explore the cave?",
            "Thank you, good to see you!",
            "Have you seen my cat?"
        };

        [Header("Ex03 Variables")]
        public int spawnCount = 3;
        public float spawnSpacing = 2f;

        [Header("Ex04 Variables")]
        public string searchTarget = "Key";
        public string[] backpack = { "Potion", "Shield", "Key", "Herb", "Key" };

        [Header("Ex05 Variables")]
        public int[] battleEnemiesHP = { 100, 0, 50, -10, 80 };

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
            Lv06_ForLoopReverse(suiteNames);
            Lv07_FindHighestScore(scores);
            Lv08_CalculateTotalScore(scores);
            Lv09_WhileLoopN(n);

            Ex01_HealTarget(enemyHP, heal, target, maxHP);
            Ex02_DialogueInteraction(npc1Dialogues, npc2Dialogues);
            Ex03_SpawnEnemiesWithSpacing(Enemy, spawnCount, spawnSpacing);
            Ex04_FindItemOrBreak(backpack, searchTarget);
            Ex05_SkipDefeatedEnemies(battleEnemiesHP);
            Ex06_WhileLoopStep(suiteNames);
            Ex07_WhileLoopSum(n);
        }

        #region Lecture

        public void As01_IronManSuit()
        {
            // Guideline: 
            // 1. สร้าง Array string[] IronManSuit ขนาด 4 เก็บ "Mark I", "Mark II", "Mark III", "Mark IV"
            // 2. นำชุดแรกมาใส่ (index 0) เก็บในตัวแปร TonyStarkWear แล้วพิมพ์ "TonyStark Wear : " + TonyStarkWear
            // 3. พิมพ์ขนาดของ Array "Room size IronManSuit : " + IronManSuit.Length
            // 4. พิมพ์ "===All suit in collection===" แล้วใช้ Debug.Log พิมพ์ชุดทั้งหมดออกมาทีละตัว
        }

        public void As02_SpiderManAndBatMan()
        {
            // Guideline:
            // 1. สร้าง Array spiderMan เก็บ "Classic SpiderMan", "Symbiote SpiderMan", "Iron Spider"
            // 2. สร้าง Array BatMan ขนาด 4 เก็บ "Classic BatMan", "Dark Knight", "Batman Beyond", "The Batman"
            // 3. พิมพ์ "Room size spiderMan : " + spiderMan.Length ตามด้วย "===All spiderMan in collection===" และ Debug.Log แสดงทีละตัว
            // 4. พิมพ์ "Room size BatMan : " + BatMan.Length ตามด้วย "===All BatMan in collection===" และ Debug.Log แสดงทีละตัว
        }

        public void As03_RandomItemDrop(GameObject[] items)
        {
            // Guideline:
            // 1. สุ่ม index จาก items โดยใช้ Random.Range(0, items.Length)
            // 2. สั่ง Instantiate ไอเทมที่สุ่มได้ที่ตำแหน่ง (0, 3, 0) ด้วย Quaternion.identity
            // 3. พิมพ์ "Got item : " + picked.name
        }

        public void As04_ForLoopBasic()
        {
            // Guideline:
            // 1. วนลูป For ตั้งแต่ i = 0 ถึง 9 พิมพ์ "<10 : " + i
            // 2. พิมพ์ "======================"
            // 3. วนลูป For ตั้งแต่ i = 1 ถึง 10 พิมพ์ "<=10 : " + i
        }

        public void As05_ForLoopN(int n)
        {
            // Guideline:
            // วนลูป For ตั้งแต่ i = 0 ถึง n - 1 แล้วพิมพ์ค่า i ออกมา
        }

        public void As06_ForLoopWithArray(string[] suiteNames)
        {
            // Guideline:
            // 1. พิมพ์ "======Log by One======" แล้ววนลูป For พิมพ์สมาชิกทีละ 1 ตัว (i++)
            // 2. พิมพ์ "======Log by Two======" แล้ววนลูป For ข้ามทีละ 2 ตัว (i += 2)
        }

        public void As07_InstantiateEnemies(GameObject Enemy, int[] HpEnemy)
        {
            // Guideline:
            // วนลูป For ตามจำนวนของ HpEnemy:
            // - Instantiate(Enemy) ที่ตำแหน่ง x = i + 1 (new Vector3(i + 1, 0f, 0f))
            // - พิมพ์ "new enemy at position x = " + (i + 1)
        }

        public void As08_WhileLoopBasic()
        {
            // Guideline:
            // วนลูป While ตั้งแต่ i = 0 ถึง 9 แล้วพิมพ์ "while loop : " + i
        }

        public IEnumerator As09_MoveToTarget(Transform character, Transform target, float speed)
        {
            // Guideline:
            // 1. ใช้ Coroutine เลื่อน character ไปหา target โดยวนลูป While ตราบใดที่ character.position.x < target.position.x
            // 2. เลื่อนตำแหน่ง character ด้วย character.Translate(Vector3.right * speed * 0.1f)
            // 3. พิมพ์ตำแหน่ง x ปัจจุบันด้วย character.position.x.ToString("F2") และสะสมเวลา timer += Time.deltaTime
            // 4. yield return null ในแต่ละเฟรม และเมื่อถึงเป้าหมายให้พิมพ์ "Time : " + timer
            yield break;
        }

        #endregion

        #region Homework

        #region Level 1: Simple

        public void Lv01_SetArrayValues()
        {
            // Guideline:
            // 1. สร้าง Array string[] weapons ขนาด 3 เก็บ "Sword", "Axe", "Bow"
            // 2. สร้าง Array int[] damage ขนาด 3 แล้วกำหนดค่าทีละ index: [0] = 100, [1] = 200, [2] = 300
            // 3. พิมพ์จับคู่กัน เช่น "Sword damage : 100", "Axe damage : 200", "Bow damage : 300"
        }

        public void Lv02_InspectArray(string[] items)
        {
            // Guideline: ตรวจสอบและพิมพ์ข้อมูลของ Array items:
            // - จำนวนไอเทมทั้งหมด: "Total items : " + items.Length
            // - ไอเทมตัวแรก (index 0): "First item : " + items[0]
            // - ไอเทมตรงกลาง (index Length / 2): "Middle item : " + items[items.Length / 2]
            // - ไอเทมตัวสุดท้าย (index Length - 1): "Last item : " + items[items.Length - 1]
        }

        public void Lv03_RandomDialogue(string[] npc1Dialogues)
        {
            // Guideline:
            // สุ่ม Index ของบทสนทนาจาก npc1Dialogues ด้วย Random.Range(0, npc1Dialogues.Length)
            // แล้วพิมพ์บทสนทนานั้นออกมาทาง Console
        }

        public void Lv04_AttackTarget(int[] enemyHP, int damage, int target)
        {
            // Guideline:
            // ลดเลือด enemyHP ด้วย damage ตามลำดับ:
            // 1. ศัตรูตัวแรก (index 0) แล้วพิมพ์ "FirstEnemy hp : " + enemyHP[0]
            // 2. ศัตรูตัวสุดท้าย (index Length - 1) แล้วพิมพ์ "LastEnemy hp : " + enemyHP[last]
            // 3. ศัตรูเป้าหมาย (index target) แล้วพิมพ์ "TargetEnemy " + target + " hp : " + enemyHP[target]
        }

        public void Lv05_MultiplicationTable(int n)
        {
            // Guideline:
            // วนลูป For พิมพ์สูตรคูณแม่ n ตั้งแต่ 1 ถึง 12 ในรูปแบบ "{n} x {i} = {n * i}"
        }

        public void Lv06_ForLoopReverse(string[] suiteNames)
        {
            // Guideline:
            // 1. พิมพ์ "======Log Reverse======"
            // 2. วนลูป For ย้อนกลับพิมพ์สมาชิกตั้งแต่ index ตัวสุดท้ายลงมาถึง index 0
        }

        public void Lv07_FindHighestScore(int[] scores)
        {
            // Guideline:
            // 1. ตรวจสอบหาก scores ว่าง ให้ return
            // 2. กำหนดตัวแปร int highest = scores[0]; เก็บค่าแรกไว้เปรียบเทียบ
            // 3. วนลูป For ตั้งแต่ index 1 ถึงตัวสุดท้าย หาก scores[i] > highest ให้ปรับ highest = scores[i]
            // 4. เมื่อจบลูป ให้พิมพ์ "Highest score : " + highest
        }

        public void Lv08_CalculateTotalScore(int[] scores)
        {
            // Guideline:
            // 1. ประกาศตัวแปร int total = 0; ไว้นอกลูป
            // 2. วนลูป For นำค่าใน scores แต่ละช่องมาบวกสะสมเข้าใน total
            // 3. แสดงผลลัพธ์ "Total score : " + total
        }

        public void Lv09_WhileLoopN(int n)
        {
            // Guideline:
            // วนลูป While พิมพ์ตัวเลขตั้งแต่ 0 ถึง n - 1
        }

        #endregion

        #region Level 2: Moderate

        public void Ex01_HealTarget(int[] enemyHP, int heal, int target, int maxHP)
        {
            // Guideline:
            // เพิ่มเลือด enemyHP ด้วย heal โดยจำกัดไม่ให้เกิน maxHP (เช่น ใช้ Mathf.Min(hp + heal, maxHP))
            // 1. ศัตรูตัวแรก (index 0) แล้วพิมพ์ "FirstEnemy hp : " + enemyHP[0]
            // 2. ศัตรูตัวสุดท้าย (index Length - 1) แล้วพิมพ์ "LastEnemy hp : " + enemyHP[last]
            // 3. ศัตรูเป้าหมาย (index target) แล้วพิมพ์ "TargetEnemy " + target + " hp : " + enemyHP[target]
        }

        public void Ex02_DialogueInteraction(string[] npc1Dialogues, string[] npc2Dialogues)
        {
            // Guideline:
            // 1. หาจำนวนรอบของบทสนทนา เช่น int rounds = Mathf.Min(npc1Dialogues.Length, npc2Dialogues.Length);
            // 2. วนลูป For ตั้งแต่ i = 0 ถึง rounds - 1
            // 3. ในแต่ละรอบ:
            //    - แสดงหัวข้อรอบ: "[Round " + (i + 1) + "]"
            //    - ตรวจสอบเงื่อนไขการสลับคนพูด (เช่น i % 2 == 0):
            //      - ถ้ารอบคู่ (i = 0, 2, ...): NPC1 พูดก่อน แล้วตามด้วย NPC2
            //      - ถ้ารอบคี่ (i = 1, 3, ...): NPC2 พูดก่อน แล้วตามด้วย NPC1
        }

        public void Ex03_SpawnEnemiesWithSpacing(GameObject Enemy, int count, float spacing)
        {
            // Guideline:
            // วนลูป For จำนวน count รอบ (i = 0 ถึง count - 1):
            // 1. คำนวณตำแหน่ง posX = (i + 1) * spacing
            // 2. Instantiate(Enemy) และตั้งตำแหน่ง transform.position = new Vector3(posX, 0f, 0f)
            // 3. พิมพ์ "Spawn enemy at position x : " + posX
        }

        public void Ex04_FindItemOrBreak(string[] inventory, string targetItem)
        {
            // Guideline:
            // 1. สร้างตัวแปร bool found = false; เพื่อเช็คว่าเจอไอเทมหรือไม่
            // 2. วนลูป For ตรวจสอบ inventory ทีละช่อง (i = 0 ถึง Length - 1):
            //    - ถ้าพบ inventory[i] == targetItem ให้พิมพ์:
            //      "Found " + targetItem + " at slot " + i
            //      เปลี่ยน found = true; แล้วใช้คำสั่ง break; เพื่อหยุดการวนลูปทันที
            // 3. ภายนอกลูป ถ้า !found (หาไม่เจอ) ให้พิมพ์:
            //    "Item " + targetItem + " not found"
        }

        public void Ex05_SkipDefeatedEnemies(int[] enemyHPs)
        {
            // Guideline:
            // วนลูป For ตรวจสอบศัตรูทีละตัว (i = 0 ถึง Length - 1):
            // 1. ถ้า enemyHPs[i] <= 0 (ศัตรูตายแล้ว) ให้ใช้คำสั่ง continue; เพื่อข้ามรอบนี้ทันที
            // 2. ถ้าศัตรูยังมีชีวิต ให้พิมพ์:
            //    "Enemy " + i + " HP : " + enemyHPs[i]
        }

        public void Ex06_WhileLoopStep(string[] suiteNames)
        {
            // Guideline:
            // 1. พิมพ์ "======Log by One======" แล้ววนลูป While พิมพ์ทีละ 1 (i++)
            // 2. พิมพ์ "======Log by Two======" แล้ววนลูป While พิมพ์ข้ามทีละ 2 (i += 2)
        }

        public void Ex07_WhileLoopSum(int n)
        {
            // Guideline:
            // ใช้ While Loop คำนวณผลรวมของตัวเลขตั้งแต่ 1 ถึง n
            // แล้วพิมพ์ "Sum of n from 0 to " + n + " is " + sum
        }

        #endregion

        #endregion // End Homework
    }
}
