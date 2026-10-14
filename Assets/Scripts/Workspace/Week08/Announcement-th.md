# 📢 ประกาศ: การเตรียมความพร้อมสำหรับการสอบกลางภาค (Midterm Examination)
## 🎮 วิชา: Basic Game Programming (การเขียนโปรแกรมเกมเบื้องต้น) | สัปดาห์ที่ 8 (Week 08)

---

สวัสดีนักศึกษาทุกคนครับ 👨‍💻👩‍💻

ในสัปดาห์นี้จะเป็นการสอบประเมินผลการเรียนรู้กลางภาค (**Midterm Examination**) ครอบคลุมเนื้อหาการเขียนโปรแกรมภาษา C# และการพัฒนาเกมด้วย Unity Engine ตั้งแต่สัปดาห์ที่ 1 ถึง 7 (Week 01 – 07)

ข้อสอบแบ่งออกเป็น **2 ส่วนหลัก (ข้อสอบปรนัย 60 ข้อ + ข้อสอบปฏิบัติ 3 ข้อ)** ขอให้นักศึกษาอ่านรายละเอียดและเตรียมความพร้อมตามขั้นตอนด้านล่างนี้ครับ

---

## 📋 1. ภาพรวมโครงสร้างข้อสอบ (2 ส่วนหลัก)

| ส่วนของข้อสอบ | รูปแบบข้อสอบ | จำนวน | ช่องทางการทำข้อสอบและการส่งงาน |
| :--- | :--- | :---: | :--- |
| **ส่วนที่ 1: ทฤษฎี (ทบทวนความรู้)** | **ข้อสอบปรนัย (ข้อกา)** | **60 ข้อ** | ทำผ่านระบบ [Google Forms (คลิกที่นี่)](https://docs.google.com/forms/d/1D0xN9Uz1ElU1Y5dktE9bmrGWpU1B0RtkPtjpXGEZCgw/viewform) |
| **ส่วนที่ 2: ทักษะการปฏิบัติ** | **ข้อสอบปฏิบัติบน Unity (C#)** | **3 ข้อใหญ่ (3 Levels)** | ทำใน Unity Scene `Week08_Exam` และส่งคะแนนผ่าน `Assignment Submitter` |

---

## 📝 2. รายละเอียดข้อสอบ ส่วนที่ 1: ข้อสอบปรนัย (ข้อกา 60 ข้อ)

* **ช่องทางทำข้อสอบ:** 🔗 [ลิงก์ Google Forms ข้อสอบกลางภาค](https://docs.google.com/forms/d/1D0xN9Uz1ElU1Y5dktE9bmrGWpU1B0RtkPtjpXGEZCgw/viewform)
* **ขอบเขตเนื้อหา:** วัดความรู้ความเข้าใจพื้นฐานการเขียนโปรแกรมเกม (Week 01 – 07)
  1. **Week 01 (Value & Variables):** ชนิดข้อมูล, ตัวแปร, และการดำเนินการทางคณิตศาสตร์ (Data Types, Casting & Arithmetic)
  2. **Week 02 (If-Else Condition):** การควบคุมเงื่อนไขและการตัดสินใจ (If-Else, Logical Operators && || !)
  3. **Week 03 (Array 1D):** โครงสร้างข้อมูลอาเรย์ 1 มิติ, Index, Loop For/Foreach, การหาผลรวมและค่า Min/Max
  4. **Week 04 (Array 2D & TicTacToe):** อาเรย์ 2 มิติ (Matrix), `GetLength(0)`, `GetLength(1)`, Nested Loops, ตาราง Grid
  5. **Week 05 (Method & Grid Map):** ฟังก์ชันและเมธอด (Void, Return Types, Parameters & Overload)
  6. **Week 06 (Class & Grid Game):** คลาส, ออบเจกต์, ฟิลด์, Properties, Prefab & `Instantiate`
  7. **Week 07 (OOP & Grid Game):** การสืบทอด (Inheritance), ระดับการเข้าถึง (`public`, `private`, `protected`), และ Polymorphism (`virtual`, `override`, `base`)
* **ข้อกำหนด:** ให้นักศึกษาล็อกอินด้วยอีเมลของมหาวิทยาลัยก่อนเริ่มทำข้อสอบ และตรวจสอบความถูกต้องก่อนกด Submit

---

## 💻 3. รายละเอียดข้อสอบ ส่วนที่ 2: ข้อสอบปฏิบัติ (3 ข้อใหญ่ 10 คะแนนเต็ม)

ข้อสอบปฏิบัติแบ่งออกเป็น **3 ระดับ (Level 1 – 3)** รวม **10 ข้อทดสอบ (10 คะแนน)** โดยตรวจผลอัตโนมัติผ่าน Unity Test Runner:

```
[Level 1: 3 คะแนน] Status & Elemental Combat  -> สร้างตัวแปร, เมธอด void TakeDamage, เมธอด return IsAlive & CalculateDamage
[Level 2: 4 คะแนน] Dungeon Grid Radar          -> ตาราง 2D Grid, Loop ค้นหาไอเทม, เรดาร์ตรวจภัยคุกคามรอบตัว 8 ทิศทาง
[Level 3: 3 คะแนน] Custom Class & Spawner      -> เขียนคลาส Armor, คลาส ArmorSpawner และประกอบ Prefab เกราะในฉาก
```

### 🟢 ระดับ 1 (Level 1): ระบบสถานะตัวละคร (3 คะแนน)
* **ไฟล์:** `Assets/Scripts/Workspace/Week08/CharacterStatus.cs` (namespace `Week08.Ex01`)
* **สิ่งที่ต้องทำ:**
  1. ประกาศตัวแปร `public` ทั้ง 6 ตัว: `characterName` ("Hero"), `maxHealth` (100), `currentHealth` (100), `isAlive` (true), `attackPoint` (10), `defensePoint` (0)
  2. เมธอด `TakeDamage(int damage)`: ลดเลือด, ป้องกันเลือดติดลบ (clamp 0), ปรับ `isAlive = false` เมื่อเลือดหมด
  3. เมธอด `IsAlive()`: คืนค่า `bool` (เลือด > 0 เป็น true)
  4. เมธอด `CalculateDamage(int rawDamage, int defense)`: คำนวณ `netDamage = rawDamage - defense` (ไม่ให้ติดลบ)

### 🟡 ระดับ 2 (Level 2): เรดาร์สำรวจตารางดันเจี้ยน (4 คะแนน)
* **ไฟล์:** `Assets/Scripts/Workspace/Week08/MapGenerator.cs` (namespace `Week08`)
* **สิ่งที่ต้องทำ (จัดการตาราง 2 มิติ `mapGrid`):**
  1. `FindItemPosition`: ค้นหาพิกัดแรกของไอเทม คืนค่า `new Vector2Int(r, c)` (หากไม่พบ คืนค่า `(-1, -1)`)
  2. `CountItems`: วนลูปนับจำนวนไอเทมทั้งหมดในตารางตามรหัสที่ระบุ
  3. `ScanThreats`: ตรวจจับภัยคุกคามรอบตัวผู้เล่น 8 ทิศทาง (พื้นที่ 3x3) พร้อมเขียนเงื่อนไขป้องกันขอบเขตตาราง (Boundary Guard)
  4. `FindPlayerPosition`: ค้นหาและคืนค่าพิกัดตำแหน่งของผู้เล่น

### 🟠 ระดับ 3 (Level 3): สร้างคลาสเกราะและระบบเสกวัตถุ (3 คะแนน)
* **ไฟล์:** **สร้างไฟล์สคริปต์ C# ขึ้นมาเอง** ในโฟลเดอร์ `Assets/Scripts/Workspace/Week08/` ใน namespace `Week08.Ex03`
* **สิ่งที่ต้องทำ:**
  1. **เขียนคลาส `Armor : MonoBehaviour`:** ตัวแปร `Name` ("Iron Armor"), `defenseBonus` (10), และเมธอด `OnTriggerEnter2D` เพิ่ม Defense ให้ Player และ Destroy ตัวเอง
  2. **เขียนคลาส `ArmorSpawner : MonoBehaviour`:** เมธอด `SpawnArmor(GameObject prefab, Vector3 spawnPosition)` ที่ตรวจสอบกรณี prefab เป็น null และใช้ `Instantiate` เสกวัตถุ
  3. **สร้าง Prefab `Iron Armor` ใน Unity Editor:** สร้าง GameObject ใส่ `Sprite Renderer` (`Iron Armor.png`), `Box Collider 2D` (ติ๊ก `Is Trigger`), แนบสคริปต์ `Armor` และเซฟเป็น Prefab ไว้ที่ `Assets/Prefabs/Exam/Iron Armor.prefab`

---

## ✅ 4. สิ่งที่ต้องเตรียมความพร้อมก่อนเข้าสอบ (Pre-Exam Checklist)

1. [ ] **เครื่องคอมพิวเตอร์และโปรแกรม:** ติดตั้ง Unity พร้อม Editor (VS Code / Visual Studio) ให้คอมไพล์โค้ด C# ได้อย่างราบรื่น
2. [ ] **ดาวน์โหลด Package ข้อสอบ:** นำไฟล์ `Week08_Midterm_Exam_Student.unitypackage` เข้าสู่โปรเจกต์ (`Assets` > `Import Package` > `Custom Package...`)
3. [ ] **ตรวจสอบ Scene ข้อสอบ:** เปิดฉาก `Assets/Scenes/Week08_Exam.unity` ได้ปกติ
4. [ ] **ทดสอบหน้าต่าง Test Runner:** เปิด `Window` > `General` > `Test Runner` (แท็บ EditMode) ตรวจสอบว่ามองเห็นหัวข้อ `Week08_Exam`
5. [ ] **ศึกษาเอกสารประกอบเพิ่มเติม:** อ่านรายละเอียดขั้นตอนได้ใน `Instruction-th.md` และเงื่อนไขละเอียดใน `Requirements.md`

---

## 📤 5. ขั้นตอนการส่งผลการสอบ

1. **ส่วนที่ 1 (ข้อกา 60 ข้อ):** กดปุ่ม **"ส่ง" (Submit)** บน Google Forms ให้เรียบร้อย
2. **ส่วนที่ 2 (ข้อปฏิบัติ Unity):**
   * ตรวจสอบคะแนนใน Unity Test Runner (กด **Run All** ใต้ `Week08_Exam`) จนพอใจกับคะแนน
   * ไปที่ GameObject ที่แนบ **`Assignment_Submitter_Week08`** ใน Scene `Week08_Exam`
   * กรอก **Student ID**, **Student Name**, และ **Section** ให้ถูกต้อง
   * กดปุ่ม **"📤 ส่งงานเข้า Google Sheet (Week 08)"**
   * ตรวจสอบ Console ว่ามีข้อความแจ้งการส่งคะแนนสำเร็จเรียบร้อย

---

## ⚖️ 6. กฎระเบียบและความซื่อสัตย์ทางวิชาการ (Academic Integrity)

* ข้อสอบชุดนี้เป็นการประเมินผลทักษะความรู้รายบุคคล **ห้ามมิให้มีการคัดลอก ดัดแปลง หรือทุจริตข้อสอบทุกกรณี**
* **ไม่อนุญาต** ให้ใช้เครื่องมือ AI (Generative AI) ช่วยสร้างโค้ดเฉลยคำตอบในระหว่างการสอบ
* ระบบ Submitter จะบันทึก Metadata, วันเวลาส่ง และรหัสเครื่อง เพื่อความโปร่งใสและเป็นธรรม
* หากตรวจพบการทุจริต จะได้รับการพิจารณาปรับตกในรายวิชาทันที

---

💡 **คำแนะนำ:** แบ่งเวลาทำข้อสอบให้เหมาะสม (แนะนำทำข้อกาให้เรียบร้อยก่อน แล้วลงมือทำข้อสอบปฏิบัติทีละ Level พร้อมตรวจด้วย Test Runner สม่ำเสมอ)

ขอให้นักศึกษาทุกคนโชคดีและทำคะแนนได้ยอดเยี่ยมครับ! ✌️🎉
