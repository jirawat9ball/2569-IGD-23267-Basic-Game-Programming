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

---

## ข้อ 3: การสร้างคลาสศัตรู (Enemy)

**ไฟล์:** `AS03_Enemy.cs` (namespace `Week06.Ex03`)

1. สร้างคลาส `Enemy` สืบทอดจาก `MonoBehaviour`
2. กำหนดฟิลด์แบบ `public`:
   - `name`: string = `"Enemy"`
   - `energy`: int = `10`
   - `attackPoint`: int = `5`
3. ใน `Awake()`: กำหนดค่าเริ่มต้นถ้า `energy <= 0` ให้เป็น 10
4. เมธอดแบบ `public`:
   - `Attack(Player target, int damage)`: โจมตีผู้เล่น
   - `TakeDamage(int damage)`: ลดเลือด เมื่อเลือดหมดทำลายตัวเอง `Destroy(gameObject)`
5. เมธอด `OnTriggerEnter2D(Collider2D other)`: เมื่อชนผู้เล่น ให้โจมตีและรับดาเมจจากผู้เล่น

---

## ข้อ 4: การสร้างคลาสทางออก (Exit)

**ไฟล์:** `AS04_Exit.cs` (namespace `Week06.Ex04`)

1. สร้างคลาส `Exit` สืบทอดจาก `MonoBehaviour`
2. กำหนดฟิลด์แบบ `public`:
   - `positionX`: int
   - `positionY`: int
3. เมธอด `OnTriggerEnter2D(Collider2D other)`: ตรวจสอบการชนกับ Player เพื่อแจ้งเตือนชนะเกม

---

## ข้อ 5: การสร้างคลาสไอเทมยาฟื้นพลัง (ItemPotion)

**ไฟล์:** `AS05_ItemPotion.cs` (namespace `Week06.Ex05`)

1. สร้างคลาส `ItemPotion` สืบทอดจาก `MonoBehaviour`
2. กำหนดฟิลด์แบบ `public`:
   - `name`: string = `"Potion"`
   - `healPoint`: int = `10`
3. เมธอด `OnTriggerEnter2D(Collider2D other)`: ตรวจสอบการชนกับ Player เรียก `player.Heal(healPoint)` และทำลายตัวเอง

---

## 🎮 กิจกรรม Workshop ในห้องเรียน (In-class Workshop)

**คู่มือฉบับเต็ม:** [Workshop.md](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Workshop.md)

สร้างมินิเกม 2D Grid Dungeon Crawler ในโฟลเดอร์ `Week06/Game/`:
- **Player.cs:** ควบคุมการเดิน (W/A/S/D), รับดาเมจ, ฮีลเลือด
- **Enemy.cs:** โจมตีผู้เล่นเมื่อชนกัน, รับความเสียหาย
- **ItemPotion.cs & ItemSword.cs:** ตรวจจับผู้เล่นด้วย `OnTriggerEnter2D` และเรียก `GetComponent<Player>()`
- **Wall.cs & Exit.cs:** กำแพงที่มีความทนทาน และประตูทางออกสู่ชัยชนะ
