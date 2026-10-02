# Assignment Week 06: คลาสและการสร้างอ็อบเจกต์ (Class & Constructor)

## 📋 ภาพรวมของ Assignment

สัปดาห์นี้เราจะเรียนเรื่อง **Class (คลาส)** และ **Constructor (คอนสตรัคเตอร์)** ซึ่งเป็นพื้นฐานสำคัญที่สุดของการเขียนโปรแกรมเชิงวัตถุ

- **คลาส (Class)** = พิมพ์เขียว (Blueprint) บอกว่าสิ่งนั้นมีคุณสมบัติอะไร (Fields/Properties) และทำอะไรได้ (Methods)
- **อ็อบเจกต์ (Object)** = ของจริงที่ถูกสร้างขึ้นมาจากพิมพ์เขียวด้วยคำสั่ง `new`
- **คอนสตรัคเตอร์ (Constructor)** = เมธอดพิเศษที่ทำงานทันทีตอนสร้างอ็อบเจกต์ เพื่อกำหนดค่าเริ่มต้นให้กับข้อมูล

```csharp
Car myCar = new Car();   // สร้างรถ 1 คันจากพิมพ์เขียว Car
myCar.name = "Toyota";
myCar.Move();
```

มีแบบฝึกหัดทั้งหมด **2 ข้อ**
- **ข้อ 1** การสร้างคลาสเบื้องต้น กำหนดฟิลด์และเมธอด (`AS01_Car.cs`)
- **ข้อ 2** การสร้างและใช้งาน Constructor เพื่อกำหนดค่าเริ่มต้น (`AS02_ClassConstructor.cs`)

---

## 🎯 จุดประสงค์การเรียนรู้

- สร้างคลาสของตัวเอง กำหนดฟิลด์และเมธอดได้
- เข้าใจความแตกต่างระหว่าง Class และ Object
- สร้างและใช้งาน Constructor เพื่อกำหนดค่าเริ่มต้นให้กับตัวแปร
- เรียกใช้งานพฤติกรรม (Method) ของ Object ได้อย่างถูกต้อง

---

## 📚 รายการไฟล์ใน Assignment

| ข้อ | ไฟล์ | ตำแหน่ง | รายละเอียด |
|---|---|---|---|
| 1 | `AS01_Car.cs` | `Assets/.../Week06/` | เขียนคลาส `Car` มี name, color, speed และ Move(), Turn(), Honk() |
| 2 | `AS02_ClassConstructor.cs` | `Assets/.../Week06/` | เขียน Constructor ของ `Dog` และสร้าง instance ใน Start() |
| รวม | `Assignment_Student_Week06.cs` | `Assets/.../Week06/` | สคริปต์หลักสำหรับรัน Demo และส่งตรวจงาน |

---

## ข้อ 1. การสร้างคลาสเบื้องต้น (Class Declaration)

### 📖 รายละเอียดโจทย์
ในไฟล์ `AS01_Car.cs` (namespace `Week06.Ex01`) ให้เขียนคลาส `Car` สืบทอดจาก `MonoBehaviour`

1. **ประกาศฟิลด์แบบ public:**
   - `string name`
   - `string color`
   - `float speed`
2. **ประกาศเมธอดแบบ public (ไม่มีค่าส่งกลับ ไม่รับพารามิเตอร์):**
   - `Move()` พิมพ์ `"Car is moving"`
   - `Turn()` พิมพ์ `"Car is turning"`
   - `Honk()` พิมพ์ `"Car is honking"`
3. **กำหนดค่าเริ่มต้นใน `Start()`:**
   - `name = "civic"`
   - `color = "black"`
   - `speed = 110`
4. **ตรวจจับการกดปุ่มใน `Update()`:**
   - กด Spacebar -> เรียก `Honk()`
   - กดปุ่ม A -> เรียก `Turn()`
   - กดปุ่ม W -> เรียก `Move()`

---

## ข้อ 2. คอนสตรัคเตอร์ (Constructor)

### 📖 รายละเอียดโจทย์
ในไฟล์ `AS02_ClassConstructor.cs` (namespace `Week06.Ex02`)

1. **ในคลาส `Dog`:**
   - มีฟิลด์ `name` (string), `breed` (string), `age` (int)
   - สร้าง Constructor รับพารามิเตอร์ 3 ตัว: `(string name, string breed, int age)` และนำค่าไปกำหนดให้ฟิลด์ของคลาส
   - เมธอด `Bark()` พิมพ์ `"<name> is barking"`
   - เมธอด `WagTail()` พิมพ์ `"<name> is wagging tail"`
   - เมธอด `StopBarking()` พิมพ์ `"<name> stopped barking"`
2. **ในคลาส `AS02_ClassConstructor` เมธอด `Start()`:**
   - สร้าง object `dog1` โดยใช้ Constructor กำหนดค่า:
     - `name = "Buddy"`
     - `breed = "Golden Retriever"`
     - `age = 3`
   - เมื่อรันแล้วจะเรียก `Bark()`, `WagTail()`, `StopBarking()` ตามลำดับ

**ผลลัพธ์ที่ต้องได้:**
```text
Buddy is barking
Buddy is wagging tail
Buddy stopped barking
```

---

## 📌 ข้อควรระวัง

- ข้อความที่พิมพ์ต้องตรงตามโจทย์ทุกตัวอักษร
- Constructor ต้องรับพารามิเตอร์ตามลำดับ: `name`, `breed`, `age`
- ตรวจสอบชื่อ namespace ให้ถูกต้อง (`Week06.Ex01`, `Week06.Ex02`)
