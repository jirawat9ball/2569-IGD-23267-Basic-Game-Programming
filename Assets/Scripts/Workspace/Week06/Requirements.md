# Week 06 Requirements: คลาสและการสร้างอ็อบเจกต์ (Class & Constructor)

โจทย์สำหรับสัปดาห์ที่ 6 มุ่งเน้นไปที่แนวคิดพื้นฐานเรื่อง Class, Object, Fields, Methods และ Constructor โดยมีโจทย์ทั้งหมด 2 ข้อ ดังนี้

---

## ข้อ 1: การสร้างคลาสเบื้องต้น (Class Declaration)

**ไฟล์:** `AS01_Car.cs` (namespace `Week06.Ex01`)

1. สร้างคลาส `Car` สืบทอดจาก `MonoBehaviour`
2. กำหนดฟิลด์แบบ `public`:
   - `name`: string
   - `color`: string
   - `speed`: float
3. สร้างเมธอดแบบ `public`:
   - `Move()`: แสดงข้อความ `"Car is moving"`
   - `Turn()`: แสดงข้อความ `"Car is turning"`
   - `Honk()`: แสดงข้อความ `"Car is honking"`
4. ใน `Start()`:
   - กำหนดค่าเริ่มต้น `name = "civic"`, `color = "black"`, `speed = 110`
5. ใน `Update()`:
   - Spacebar -> `Honk()`
   - A -> `Turn()`
   - W -> `Move()`

---

## ข้อ 2: การสร้าง Constructor (Constructor Declaration)

**ไฟล์:** `AS02_ClassConstructor.cs` (namespace `Week06.Ex02`)

1. คลาส `Dog`:
   - ฟิลด์: `name` (string), `breed` (string), `age` (int)
   - Constructor: รับ `(string name, string breed, int age)` แล้วเซ็ตค่าให้กับฟิลด์
   - เมธอด:
     - `Bark()` -> `"<name> is barking"`
     - `WagTail()` -> `"<name> is wagging tail"`
     - `StopBarking()` -> `"<name> stopped barking"`
2. ใน `AS02_ClassConstructor.Start()`:
   - สร้าง instance `dog1 = new Dog("Buddy", "Golden Retriever", 3)`
   - เรียกใช้ `Bark()`, `WagTail()`, `StopBarking()`
