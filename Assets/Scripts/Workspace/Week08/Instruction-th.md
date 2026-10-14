# ข้อสอบปฏิบัติกลางภาค (Midterm Practical Exam) - Week 08
## วิชา: Basic Game Programming (การเขียนโปรแกรมเกมเบื้องต้น)

---

## 📋 ภาพรวมของข้อสอบปฏิบัติ

ข้อสอบนี้เป็นการประเมินผลการเรียนรู้เชิงปฏิบัติ (Practical Examination) ครอบคลุมเนื้อหาการเขียนโปรแกรมเกมด้วยภาษา C# บน Unity Engine ตั้งแต่สัปดาห์ที่ 1 ถึง 6 แบ่งเนื้อหาออกเป็น **3 ระดับ (Level 1 – 3)** เพื่อวัดทักษะตั้งแต่พื้นฐานไปจนถึงการประยุกต์ใช้ในการสร้างระบบเกม

---

## 🎯 วัตถุประสงค์การวัดผล

1. **ระดับ 1 (Level 1): การสร้างคลาส และเมธอดแบบคืนค่า/ไม่คืนค่า (Week 01 – 03)**
   - ประกาศและกำหนดค่าเริ่มต้นของตัวแปรประเภทต่างๆ (`int`, `string`, `bool`) ภายในคลาส
   - การเขียนเมธอดแบบไม่คืนค่า (`void`) เช่น `TakeDamage(int damage)` เพื่อลดค่าตัวแปรและปรับสถานะ
   - การเขียนเมธอดแบบคืนค่า (Return Types) เช่น `IsAlive()` (คืนค่า `bool`) และ `CalculateDamage(int, int)` (คืนค่า `int`)

2. **ระดับ 2 (Level 2): โครงสร้างข้อมูลตาราง 2 มิติ และการวนลูป Loop (Week 04 – 05)**
   - การจัดการข้อมูลแผนที่แบบ 2 มิติ (2D Array Grid) ร่วมกับ Nested Loop (`GetLength(0)`, `GetLength(1)`)
   - การเขียนอัลกอริทึมค้นหาพิกัดไอเทมบนตาราง (`FindItemPosition`), การตรวจนับจำนวนไอเทม (`CountItems`), ตรวจจับภัยคุกคามรอบตัวผู้เล่น (`ScanThreats`) และค้นหาตำแหน่งผู้เล่น (`FindPlayerPosition`)

3. **ระดับ 3 (Level 3): การสร้างคลาส และการเสกวัตถุจาก Prefab (Week 05 – 06)**
   - การสร้าง Custom Class พร้อม Properties/Fields และกำหนดค่าเริ่มต้น
   - การเขียนเมธอดลดค่าความทนทานของอาวุธ
   - การสร้างเมธอดเสกสร้างวัตถุ (Instantiate) จาก Prefab ลงในฉาก

---

## 📚 รายการไฟล์ที่นักศึกษาต้องทำ

| ระดับ | ไฟล์ที่ต้องเขียน | Namespace | หัวข้อหลัก |
|---|---|---|---|
| **Level 1** | `AS01_StatusAndLogic.cs` | `Week08.Ex01` | Class, Void & Return Type Methods |
| **Level 2** | `AS02_InventoryAndGrid.cs` | `Week08.Ex02` | Dungeon Grid Scanner & Item Finder |
| **Level 3** | `AS03_MethodAndClass.cs` | `Week08.Ex03` | Weapon Class & Prefab Spawner |

---

## 🚀 วิธีการทดสอบและส่งผลคะแนน

1. **เปิด Unity Editor** แล้วไปที่หน้าต่าง **Test Runner** (`Window` > `General` > `Test Runner`)
2. เลือกหมวด **EditMode** แล้วค้นหาเทสเคสของ **`Week08_Exam`**
3. กดปุ่ม **Run All** เพื่อดูผลว่าโค้ดผ่านข้อใดบ้าง
4. เมื่อทำเสร็จและต้องการส่งคะแนน:
   - นำ Prefab หรือแนบ Component `Assignment_Submitter_Week08` ไว้ใน Scene
   - กรอกรหัสนักศึกษา (StudentID), ชื่อ-สกุล (StudentName) และ Section ให้ถูกต้อง
   - กดปุ่ม **"📤 ส่งงานเข้า Google Sheet (Week 08)"** ใน Inspector หรือไปที่เมนู `Assignment` > `Submit Score Week 08`
