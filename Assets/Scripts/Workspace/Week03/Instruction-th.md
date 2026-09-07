# คู่มือแบบฝึกหัด Week 03: Arrays, Loops, and Instantiation

เอกสารฉบับนี้อธิบายโจทย์ทั้ง 16 ข้อสำหรับ Week 03 ให้นักศึกษาทำความเข้าใจและเขียนโค้ดในไฟล์ `Assignment_Student_Week03.cs` ให้ผ่านเงื่อนไขที่กำหนด

## 📚 โครงสร้างของ Assignment
- **Lecture / Example Methods (7 ข้อ):** `As01` ถึง `As07` — เรียนรู้และฝึกปฏิบัติพร้อมกันในห้องเรียน
- **Homework - Level 1: Simple (5 ข้อ):** `Lv01` ถึง `Lv05` — แบบฝึกหัดพื้นฐาน
- **Homework - Level 2: Moderate (4 ข้อ):** `Ex01` ถึง `Ex04` — แบบฝึกหัดประยุกต์ร่วมกับ Game Mechanics

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
- พิมพ์ข้อความ `Got item: <ชื่อไอเทม>`

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

### 7. การใช้งาน While Loop พื้นฐาน
**Method Signature:** `void As07_WhileLoopBasic()`
**โจทย์:**
- วนลูป While จาก 0 ถึง 9 และพิมพ์ `while loop : i`

---

## 📝 Homework

### 🟢 Level 1: Simple

### 8. การโจมตีเป้าหมายใน Array
**Method Signature:** `void Lv01_AttackTarget(int[] enemyHP, int damage, int target)`
**โจทย์:**
- โจมตีศัตรูตัวแรก: พิมพ์ `FirstEnemy hp :<hp ที่เหลือ>`
- โจมตีศัตรูตัวสุดท้าย: พิมพ์ `LastEnemy hp :<hp ที่เหลือ>`
- โจมตีศัตรูเป้าหมาย: พิมพ์ `TargetEnemy <target> hp :<hp ที่เหลือ>`
- (ลำดับการโจมตีต้องเรียงจาก ตัวแรก -> ตัวสุดท้าย -> ตัวเป้าหมาย)

### 9. สูตรคูณ
**Method Signature:** `void Lv02_MultiplicationTable(int n)`
**โจทย์:**
- วนลูป For พิมพ์สูตรคูณแม่ n ตั้งแต่ 1 ถึง 12
- รูปแบบ `n x i = (n*i)`

### 10. While Loop ตามจำนวน Input
**Method Signature:** `void Lv03_WhileLoopN(int n)`
**โจทย์:**
- วนลูป While พิมพ์ตัวเลขตั้งแต่ 0 ถึง n-1

### 11. การปรับค่า Step ของ While Loop
**Method Signature:** `void Lv04_WhileLoopStep(string[] suiteNames)`
**โจทย์:**
- เหมือนข้อ 6 (ข้ามทีละ 2) แต่ใช้ While Loop แทน For Loop

### 12. ผลรวมสะสมด้วย While Loop
**Method Signature:** `void Lv05_WhileLoopSum(int n)`
**โจทย์:**
- หาผลรวมของตัวเลขตั้งแต่ 1 ถึง n ด้วย While Loop
- พิมพ์ `Sum of n from 0 to <n> is <sum>`

---

### 🟡 Level 2: Moderate

### 13. การฟื้นฟู (Heal) เป้าหมาย
**Method Signature:** `void Ex01_HealTarget(int[] enemyHP, int heal, int target)`
**โจทย์:**
- เหมือนข้อ 8 แต่เปลี่ยนจากการลบ HP เป็นการบวก HP แทน
- พิมพ์ข้อความแบบเดียวกัน เช่น `FirstEnemy hp :<hp หลังจาก heal>`

### 14. ระบบสุ่มบทสนทนา
**Method Signature:** `void Ex02_RandomDialogue(string[] npc1Dialogues)`
**โจทย์:**
- ใช้ `UnityEngine.Random.Range` สุ่ม Index ของบทสนทนา
- พิมพ์บทสนทนานั้นออกมาทาง Console

### 15. สร้างศัตรูเรียงกัน
**Method Signature:** `void Ex03_InstantiateEnemies(GameObject Enemy, int[] HpEnemy)`
**โจทย์:**
- วนลูป For ตามจำนวนของ HpEnemy
- Instantiate ศัตรู โดยให้ตำแหน่งแกน X ขยับเพิ่มขึ้นทีละ 1 (เริ่มจาก x=1)
- พิมพ์ `new enemy at position x = <ค่า x>`

### 16. การเคลื่อนที่ด้วย Translate
**Method Signature:** `void Ex04_MoveToTarget(Transform positionToMove, float speed)`
**โจทย์:**
- วนลูปจนกว่าค่า x ปัจจุบันจะ >= positionToMove.x
- เคลื่อนที่วัตถุ: `transform.Translate(Vector3.right * speed * 0.1f)`
- พิมพ์ค่า x ทุกรอบด้วย `transform.position.x.ToString("F2")`

---
**ขอให้โชคดี! 👨‍💻**
