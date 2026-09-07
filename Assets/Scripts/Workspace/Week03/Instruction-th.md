# คู่มือแบบฝึกหัด Week 03: Arrays, Loops, and Instantiation

เอกสารฉบับนี้อธิบายโจทย์ทั้ง 16 ข้อสำหรับ Week 03 ให้นักศึกษาทำความเข้าใจและเขียนโค้ดในไฟล์ `Assignment_Student_Week03.cs` ให้ผ่านเงื่อนไขที่กำหนด

## 📚 โครงสร้างของ Assignment
- **Lecture / Example Methods (9 ข้อ):** `As01` ถึง `As09` — เรียนรู้และฝึกปฏิบัติพร้อมกันในห้องเรียน
- **Homework - Level 1: Simple (5 ข้อ):** `Lv01` ถึง `Lv05` — แบบฝึกหัดพื้นฐาน
- **Homework - Level 2: Moderate (2 ข้อ):** `Ex01` ถึง `Ex02` — แบบฝึกหัดประยุกต์ร่วมกับ Game Mechanics

---

## 📖 Lecture (ตัวอย่างในห้องเรียน)

### 1. ประกาศและใช้งาน Array พื้นฐาน
**Method Signature:** `void As01_IronManSuit()`
**โจทย์:**
- ประกาศ `string[] IronManSuit` ขนาด 4 ช่อง และกำหนดค่า "Mark I" ถึง "Mark IV" ตามลำดับ
- เก็บค่าช่องแรกไว้ในตัวแปร `TonyStarkWear` แล้วพิมพ์ `TonyStark Wear : <ค่าตัวแปร>`
- พิมพ์ขนาดของ Array `Room size IronManSuit : <ขนาด>`
- พิมพ์ `===All suit in collection===` และตามด้วยชื่อชุดทั้งหมดใน Array (แนะนำให้ใช้ for loop)

### 2. รูปแบบการประกาศ Array
**Method Signature:** `void As02_SpiderManAndBatMan()`
**โจทย์:**
- สร้าง Array ชื่อ `spiderMan` แบบไม่ระบุขนาด ({"Classic SpiderMan", ...}) รวม 3 ชุด
- สร้าง Array ชื่อ `BatMan` แบบใช้คำว่า new (new string[4] {...}) รวม 4 ชุด
- พิมพ์ขนาดและเนื้อหาของทั้งสอง Array ออกมาตามรูปแบบที่กำหนด (ดูตัวอย่างในโค้ด)

### 3. สุ่มดรอปไอเทม
**Method Signature:** `void As03_RandomItemDrop(GameObject[] items)`
**โจทย์:**
- สุ่มไอเทมจาก Array `items` โดยใช้ `UnityEngine.Random.Range`
- สร้างไอเทมนั้นในฉากด้วยคำสั่ง `Instantiate`
- พิมพ์ข้อความ `Got item : <ชื่อไอเทม>`

### 4. การใช้งาน For Loop พื้นฐาน
**Method Signature:** `void As04_ForLoopBasic()`
**โจทย์:**
- ลูปที่ 1: i จาก 0 ถึง 9 พิมพ์ `<10 : i`
- คั่นด้วย `======================`
- ลูปที่ 2: i จาก 1 ถึง 10 พิมพ์ `<=10 : i`

### 5. For Loop ตามจำนวน Input
**Method Signature:** `void As05_ForLoopN(int n)`
**โจทย์:**
- วนลูป For จำนวน n ครั้ง (จาก 0 ถึง n-1)
- พิมพ์ตัวเลขรอบนั้นๆ ออกมา

### 6. การปรับค่า Step ของ For Loop ร่วมกับ Array
**Method Signature:** `void As06_ForLoopWithArray(string[] suiteNames)`
**โจทย์:**
- พิมพ์ `======Log by One======` วนลูปพิมพ์ทุกชุดใน Array
- พิมพ์ `======Log by Two======` วนลูปพิมพ์แบบข้ามทีละ 2 ชุด (index 0, 2, 4...)

### 7. สร้างศัตรูเรียงกัน
**Method Signature:** `void As07_InstantiateEnemies(GameObject Enemy, int[] HpEnemy)`
**โจทย์:**
- วนลูป For ตามจำนวนของ HpEnemy
- Instantiate ศัตรู โดยให้ตำแหน่งแกน X ขยับเพิ่มขึ้นทีละ 1 (เริ่มจาก x=1)
- พิมพ์ `new enemy at position x = <ค่า x>`

### 8. การใช้งาน While Loop พื้นฐาน
**Method Signature:** `void As08_WhileLoopBasic()`
**โจทย์:**
- วนลูป While จาก 0 ถึง 9 และพิมพ์ `while loop : i`

### 9. การเคลื่อนที่ด้วย Coroutine
**Method Signature:** `IEnumerator As09_MoveToTarget(Transform character, Transform target, float speed)`
**โจทย์:**
- เขียน Coroutine (IEnumerator) เพื่อเคลื่อนที่ตัวละคร `character` ไปยังปลายทาง `target` ด้วย While Loop จนกว่าค่า x ของ character จะ >= target.position.x
- เคลื่อนที่ตัวละคร: `character.Translate(Vector3.right * speed * 0.1f)`
- พิมพ์ค่า x ของ character ทุกรอบด้วย `character.position.x.ToString("F2")`
- รอ 1 เฟรมในแต่ละรอบด้วย `yield return null;`
- เรียกใช้งานใน `Start()` ด้วยคำสั่ง `StartCoroutine(As09_MoveToTarget(character, positionToMove, speed));`

---

## 📝 Homework

### 🟢 Level 1: Simple

### 10. กำหนดค่าตัวแปร Array ตามคำสั่ง
**Method Signature:** `void Lv01_SetArrayValues()`
**โจทย์:**
- สร้าง Array ชนิดข้อความ `string[] weapons = new string[3];`
- กำหนดค่าตาม Index ดังนี้: `weapons[0] = "Sword"`, `weapons[1] = "Axe"`, `weapons[2] = "Bow"`
- สร้าง Array ชนิดตัวเลขจำนวนเต็ม `int[] damage = new int[3];`
- กำหนดค่าตาม Index ดังนี้: `damage[0] = 100`, `damage[1] = 200`, `damage[2] = 300`
- พิมพ์ข้อมูลอาวุธจับคู่กับ Damage แต่ละตัว:
  - `Sword damage : 100`
  - `Axe damage : 200`
  - `Bow damage : 300`

### 11. การตรวจสอบข้อมูลพื้นฐานของ Array
**Method Signature:** `void Lv02_InspectArray(string[] items)`
**โจทย์:**
- ตรวจสอบและพิมพ์จำนวนไอเทมทั้งหมดใน Array ด้วย `.Length`:
  - `Total items : <ความยาว array>`
- ตรวจสอบและพิมพ์ไอเทมตัวแรก (index 0):
  - `First item : <ไอเทมตัวแรก>`
- ตรวจสอบและพิมพ์ไอเทมตรงกลาง (index `Length / 2`):
  - `Middle item : <ไอเทมตรงกลาง>`
- ตรวจสอบและพิมพ์ไอเทมตัวสุดท้าย (index `Length - 1`):
  - `Last item : <ไอเทมตัวสุดท้าย>`

### 12. ระบบสุ่มบทสนทนา
**Method Signature:** `void Lv03_RandomDialogue(string[] npc1Dialogues)`
**โจทย์:**
- ใช้ `UnityEngine.Random.Range` สุ่ม Index ของบทสนทนาจาก `npc1Dialogues`
- พิมพ์บทสนทนานั้นออกมาทาง Console

### 13. การโจมตีเป้าหมายใน Array
**Method Signature:** `void Lv04_AttackTarget(int[] enemyHP, int damage, int target)`
**โจทย์:**
- โจมตีศัตรูตัวแรก: พิมพ์ `FirstEnemy hp : <hp ที่เหลือ>`
- โจมตีศัตรูตัวสุดท้าย: พิมพ์ `LastEnemy hp : <hp ที่เหลือ>`
- โจมตีศัตรูเป้าหมาย: พิมพ์ `TargetEnemy <target> hp : <hp ที่เหลือ>`
- (ลำดับการโจมตีต้องเรียงจาก ตัวแรก -> ตัวสุดท้าย -> ตัวเป้าหมาย)

### 14. สูตรคูณ
**Method Signature:** `void Lv05_MultiplicationTable(int n)`
**โจทย์:**
- วนลูป For พิมพ์สูตรคูณแม่ n ตั้งแต่ 1 ถึง 12
- รูปแบบ `n x i = (n*i)`

### 15. While Loop ตามจำนวน Input
**Method Signature:** `void Lv06_WhileLoopN(int n)`
**โจทย์:**
- วนลูป While พิมพ์ตัวเลขตั้งแต่ 0 ถึง n-1

### 16. วนลูป Array แบบย้อนกลับ (Reverse Loop)
**Method Signature:** `void Lv07_ForLoopReverse(string[] suiteNames)`
**โจทย์:**
- พิมพ์ `======Log Reverse======`
- วนลูป For พิมพ์สมาชิกใน Array จาก index ตัวสุดท้ายย้อนกลับมายัง index ตัวแรก (index 0)

---

### 🟡 Level 2: Moderate

### 17. การฟื้นฟู (Heal) เป้าหมายพร้อมจำกัด HP สูงสุด
**Method Signature:** `void Ex01_HealTarget(int[] enemyHP, int heal, int target, int maxHP)`
**โจทย์:**
- ทำการเพิ่ม HP ด้วยค่า `heal` ให้กับ:
  1. ศัตรูตัวแรก (index `0`)
  2. ศัตรูตัวสุดท้าย (index `enemyHP.Length - 1`)
  3. ศัตรูตัวที่ระบุด้วย `target`
- โดยมีเงื่อนไขว่า **เลือดหลังฟื้นฟูต้องไม่เกินค่า `maxHP`** (เช่น ใช้ `Mathf.Min(hp + heal, maxHP)`)
- แสดงผลลัพธ์ผ่าน Debug.Log ตามลำดับ:
  - `FirstEnemy hp : <hp หลังจาก heal>`
  - `LastEnemy hp : <hp หลังจาก heal>`
  - `TargetEnemy <target> hp : <hp หลังจาก heal>`

### 18. ระบบบทสนทนาโต้ตอบระหว่าง 2 NPC
**Method Signature:** `void Ex02_DialogueInteraction(string[] npc1Dialogues, string[] npc2Dialogues)`
**โจทย์:**
- ใช้ `UnityEngine.Random.Range` สุ่ม Index บทสนทนาจาก `npc1Dialogues` แล้วพิมพ์:
  - `NPC1 : <บทสนทนาที่สุ่มได้>`
- ใช้ `UnityEngine.Random.Range` สุ่ม Index บทสนทนาจาก `npc2Dialogues` แล้วพิมพ์:
  - `NPC2 : <บทสนทนาที่สุ่มได้>`

### 19. การสร้างศัตรูพร้อมกำหนดระยะห่าง (Spacing)
**Method Signature:** `void Ex03_SpawnEnemiesWithSpacing(GameObject Enemy, int count, float spacing)`
**โจทย์:**
- วนลูป For จำนวน `count` รอบ (ตั้งแต่ `i = 0` ถึง `count - 1`)
- คำนวณตำแหน่งแกน X ด้วยสูตร `posX = (i + 1) * spacing`
- สั่ง `Instantiate(Enemy)` และกำหนดตำแหน่ง `transform.position = new Vector3(posX, 0f, 0f)`
- พิมพ์ `Spawn enemy at position x : <ค่า posX>`

### 20. การปรับค่า Step ของ While Loop
**Method Signature:** `void Ex04_WhileLoopStep(string[] suiteNames)`
**โจทย์:**
- เหมือนข้อ 6 (ข้ามทีละ 2) แต่ใช้ While Loop แทน For Loop

### 21. ผลรวมสะสมด้วย While Loop
**Method Signature:** `void Ex05_WhileLoopSum(int n)`
**โจทย์:**
- หาผลรวมของตัวเลขตั้งแต่ 1 ถึง n ด้วย While Loop
- พิมพ์ `Sum of n from 0 to <n> is <sum>`

---
**ขอให้โชคดี! 👨‍💻**
