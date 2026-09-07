# คู่มือแบบฝึกหัด Week 03: Arrays, Loops, and Instantiation

เอกสารฉบับนี้อธิบายโจทย์ทั้ง 16 ข้อสำหรับ Week 03 ให้นักศึกษาทำความเข้าใจและเขียนโค้ดในไฟล์ `Assignment_Student_Week03.cs` ให้ผ่านเงื่อนไขที่กำหนด

---

## 🚀 เรื่องที่ 1: Array (ข้อ 1-6)

### 1. ประกาศและใช้งาน Array พื้นฐาน
**Method Signature:** `void Ex01_IronManSuit()`
**โจทย์:**
- ประกาศ `string[] IronManSuit` ขนาด 7 ช่อง และกำหนดค่า "Mark I" ถึง "Mark VII" ตามลำดับ
- เก็บค่าช่องแรกไว้ในตัวแปร `TonyStarkWear` แล้วพิมพ์ `TonyStark Wear : <ค่าตัวแปร>`
- พิมพ์ขนาดของ Array `Room size IronManSuit : <ขนาด>`
- พิมพ์ `===All suit in collection===` และตามด้วยชื่อชุดทั้งหมดใน Array (แนะนำให้ใช้ for loop)

### 2. รูปแบบการประกาศ Array
**Method Signature:** `void Ex02_SpiderManAndBatMan()`
**โจทย์:**
- สร้าง Array ชื่อ `spiderMan` แบบไม่ระบุขนาด ({"Classic SpiderMan", ...}) รวม 5 ชุด
- สร้าง Array ชื่อ `BatMan` แบบใช้คำว่า new (new string[4] {...}) รวม 4 ชุด
- พิมพ์ขนาดและเนื้อหาของทั้งสอง Array ออกมาตามรูปแบบที่กำหนด (ดูตัวอย่างในโค้ด)

### 3. การโจมตีเป้าหมายใน Array
**Method Signature:** `void Ex03_AttackTarget(int[] enemyHP, int damage, int target)`
**โจทย์:**
- โจมตีศัตรูตัวแรก: พิมพ์ `FirstEnemy hp :<hp ที่เหลือ>`
- โจมตีศัตรูตัวสุดท้าย: พิมพ์ `LastEnemy hp :<hp ที่เหลือ>`
- โจมตีศัตรูเป้าหมาย: พิมพ์ `TargetEnemy <target> hp :<hp ที่เหลือ>`
- (ลำดับการโจมตีต้องเรียงจาก ตัวแรก -> ตัวสุดท้าย -> ตัวเป้าหมาย)

### 4. สุ่มดรอปไอเทม
**Method Signature:** `void Ex04_RandomItemDrop(GameObject[] items)`
**โจทย์:**
- สุ่มไอเทมจาก Array `items` โดยใช้ `UnityEngine.Random.Range`
- สร้างไอเทมนั้นในฉากด้วยคำสั่ง `Instantiate`
- พิมพ์ข้อความ `Got item: <ชื่อไอเทม>`

### 5. การฟื้นฟู (Heal) เป้าหมาย
**Method Signature:** `void Ex05_HealTarget(int[] enemyHP, int heal, int target)`
**โจทย์:**
- เหมือนข้อ 3 แต่เปลี่ยนจากการลบ HP เป็นการบวก HP แทน
- พิมพ์ข้อความแบบเดียวกัน เช่น `FirstEnemy hp :<hp หลังจาก heal>`

### 6. ระบบสุ่มบทสนทนา
**Method Signature:** `void Ex06_RandomDialogue(string[] npc1Dialogues)`
**โจทย์:**
- ใช้ `UnityEngine.Random.Range` สุ่ม Index ของบทสนทนา
- พิมพ์บทสนทนานั้นออกมาทาง Console

---

## 🔁 เรื่องที่ 2: For Loop (ข้อ 7-10)

### 7. การใช้งาน For Loop พื้นฐาน
**Method Signature:** `void Ex07_ForLoopBasic()`
**โจทย์:**
- ลูปที่ 1: i จาก 0 ถึง 9 พิมพ์ `<10 : i`
- คั่นด้วย `======================`
- ลูปที่ 2: i จาก 1 ถึง 10 พิมพ์ `<=10 : i`

### 8. For Loop ตามจำนวน Input
**Method Signature:** `void Ex08_ForLoopN(int n)`
**โจทย์:**
- วนลูป For จำนวน n ครั้ง (จาก 0 ถึง n-1)
- พิมพ์ตัวเลขรอบนั้นๆ ออกมา

### 9. การปรับค่า Step ของ For Loop
**Method Signature:** `void Ex09_ForLoopStep(string[] suiteNames)`
**โจทย์:**
- พิมพ์ `======Log by One======` วนลูปพิมพ์ทุกชุดใน Array
- พิมพ์ `======Log by Two======` วนลูปพิมพ์แบบข้ามทีละ 2 ชุด (index 0, 2, 4...)

### 10. สูตรคูณ
**Method Signature:** `void Ex10_MultiplicationTable(int n)`
**โจทย์:**
- วนลูป For พิมพ์สูตรคูณแม่ n ตั้งแต่ 1 ถึง 12
- รูปแบบ `n x i = (n*i)`

---

## 🔄 เรื่องที่ 3: While Loop (ข้อ 11-14)

### 11. การใช้งาน While Loop พื้นฐาน
**Method Signature:** `void Ex11_WhileLoopBasic()`
**โจทย์:**
- วนลูป While จาก 0 ถึง 9 และพิมพ์ `while loop : i`

### 12. While Loop ตามจำนวน Input
**Method Signature:** `void Ex12_WhileLoopN(int n)`
**โจทย์:**
- วนลูป While พิมพ์ตัวเลขตั้งแต่ 0 ถึง n-1

### 13. การปรับค่า Step ของ While Loop
**Method Signature:** `void Ex13_WhileLoopStep(string[] suiteNames)`
**โจทย์:**
- เหมือนข้อ 9 (ข้ามทีละ 2) แต่ใช้ While Loop แทน For Loop

### 14. ผลรวมสะสมด้วย While Loop
**Method Signature:** `void Ex14_WhileLoopSum(int n)`
**โจทย์:**
- หาผลรวมของตัวเลขตั้งแต่ 1 ถึง n ด้วย While Loop
- พิมพ์ `ผลรวมของ n จาก 0 ถึง <n> คือ <sum>`

---

## 🎮 เรื่องที่ 4: การสร้างวัตถุและการเคลื่อนที่ (ข้อ 15-16)

### 15. สร้างศัตรูเรียงกัน
**Method Signature:** `void Ex15_InstantiateEnemies(GameObject Enemy, int[] HpEnemy)`
**โจทย์:**
- วนลูป For ตามจำนวนของ HpEnemy
- Instantiate ศัตรู โดยให้ตำแหน่งแกน X ขยับเพิ่มขึ้นทีละ 1 (เริ่มจาก x=1)
- พิมพ์ `new enemy at position x = <ค่า x>`

### 16. การเคลื่อนที่ด้วย Translate
**Method Signature:** `void Ex16_MoveToTarget(Transform positionToMove, float speed)`
**โจทย์:**
- วนลูปจนกว่าค่า x ปัจจุบันจะ >= positionToMove.x
- เคลื่อนที่วัตถุ: `transform.Translate(Vector3.right * speed * 0.1f)`
- พิมพ์ค่า x ทุกรอบด้วย `transform.position.x.ToString("F2")`

---
**ขอให้โชคดี! 👨‍💻**
