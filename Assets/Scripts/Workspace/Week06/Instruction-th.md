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

มีเนื้อหาการเรียนรู้แบ่งเป็น 2 ส่วน:
1. **แบบฝึกหัดเดี่ยว (Individual Assignments):**
   - **ข้อ 1** การสร้างคลาสเบื้องต้น กำหนดฟิลด์และเมธอด (`AS01_Car.cs`)
   - **ข้อ 2** การสร้างและใช้งาน Constructor เพื่อกำหนดค่าเริ่มต้น (`AS02_ClassConstructor.cs`)
2. **กิจกรรมในชั้นเรียน (In-class Workshop):**
   - พัฒนาเกมเดินตาราง 2D Mini Game ประยุกต์ใช้ Class, Method, `GetComponent<T>()` และ `OnTriggerEnter2D` (ดูคู่มือฉบับเต็มที่ [Workshop.md](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Workshop.md))

---

## 🎯 จุดประสงค์การเรียนรู้

- สร้างคลาสของตัวเอง กำหนดฟิลด์และเมธอดได้
- เข้าใจความแตกต่างระหว่าง Class และ Object
- สร้างและใช้งาน Constructor เพื่อกำหนดค่าเริ่มต้นให้กับตัวแปร
- เรียกใช้งานพฤติกรรม (Method) ของ Object ได้อย่างถูกต้อง
- เชื่อมต่อการทำงานระหว่างสคริปต์ใน Unity ด้วย `GetComponent` และ `OnTriggerEnter2D`

---

## 📚 รายการไฟล์ใน Assignment & Workshop

| ส่วน | ไฟล์ | ตำแหน่ง | รายละเอียด |
|---|---|---|---|
| ข้อ 1 | `AS01_Car.cs` | `Assets/.../Week06/` | เขียนคลาส `Car` มี name, color, speed และ Move(), Turn(), Honk() |
| ข้อ 2 | `AS02_ClassConstructor.cs` | `Assets/.../Week06/` | เขียน Constructor ของ `Dog` และสร้าง instance ใน Start() |
| ข้อ 3 | `AS03_Enemy.cs` | `Assets/.../Week06/` | เขียนคลาส `Enemy` มี energy, attackPoint, Attack(), TakeDamage(), Trigger |
| ข้อ 4 | `AS04_Exit.cs` | `Assets/.../Week06/` | เขียนคลาส `Exit` มี positionX, positionY, Trigger ตรวจจับผู้เล่นชนะเกม |
| ข้อ 5 | `AS05_ItemPotion.cs` | `Assets/.../Week06/` | เขียนคลาส `ItemPotion` มี healPoint, Trigger สั่ง Heal ผู้เล่นแล้วทำลายตัวเอง |
| **HW 1** | `HW01_ItemSword.cs` | `Assets/.../Week06/` | ไอเทมดาบเพิ่มพลังโจมตี `attackBonus` ให้ผู้เล่นแล้วทำลายตัวเอง |
| **HW 2** | `HW02_Trap.cs` | `Assets/.../Week06/` | กับดักหนาม ลดพลังงานผู้เล่นตามค่า `damage` เมื่อเหยียบ |
| **HW 3** | `HW03_Wall.cs` | `Assets/.../Week06/` | กำแพงพังได้ มี `durability` ลดลงเมื่อชน หากหมดจะพังทลาย |
| **HW 4** | `HW04_Chest.cs` | `Assets/.../Week06/` | กล่องสมบัติ เสกสร้างวัตถุใหม่ `Instantiate(spawnPrefab)` แล้วทำลายกล่อง |
| สรุป | `Assignment_Student_Week06.cs` | `Assets/.../Week06/` | สคริปต์หลักสำหรับรัน Demo และส่งตรวจงาน |
| **Workshop** | โฟลเดอร์ `Game/` + `Workshop.md` | `Assets/.../Week06/` | คู่มือสร้างเกม 2D Dungeon: Player, Enemy, Item, Trigger |

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

## ข้อ 3. ศัตรูและการต่อสู้ (Enemy)

### 📖 รายละเอียดโจทย์
ในไฟล์ `AS03_Enemy.cs` (namespace `Week06.Ex03`) ให้เขียนคลาส `Enemy` สืบทอดจาก `MonoBehaviour`

1. **ประกาศฟิลด์แบบ public:**
   - `string name = "Enemy"`
   - `int energy = 10` (เลือดของศัตรู น้อยกว่า Player)
   - `int attackPoint = 5` (ดาเมจของศัตรู น้อยกว่า Player)
2. **ใน `Awake()`:**
   - ถ้า `energy <= 0` ให้กำหนดค่าเป็น `10`
3. **ประกาศเมธอดแบบ public:**
   - `Attack(Player target, int damage)`:
     - พิมพ์ `"<name> attacks <target.Name> with <damage> damage!"`
     - สั่งให้ `target.TakeDamage(damage, name)`
   - `TakeDamage(int damage)`:
     - ลดค่า `energy -= damage`
     - พิมพ์ `"<name> takes <damage> damage! Remaining HP: <energy>"`
     - ถ้า `energy <= 0` พิมพ์ `"💥 <name> defeated!"` และสั่ง `Destroy(gameObject)`
4. **ประกาศเมธอด `OnTriggerEnter2D(Collider2D other)` แบบ private:**
   - พิมพ์ `"[Trigger] <gameObject.name> collided with <other.gameObject.name>"`
   - ดึง `Player player = other.GetComponent<Player>()`
   - ถ้าเจอ Player ให้สั่ง `Attack(player, attackPoint)` และ `TakeDamage(player.attackPoint)`

---

## ข้อ 4. ทางออกของเกม (Exit)

### 📖 รายละเอียดโจทย์
ในไฟล์ `AS04_Exit.cs` (namespace `Week06.Ex04`) ให้เขียนคลาส `Exit` สืบทอดจาก `MonoBehaviour`

1. **ประกาศฟิลด์แบบ public:**
   - `int positionX`
   - `int positionY`
2. **ประกาศเมธอด `OnTriggerEnter2D(Collider2D other)` แบบ private:**
   - พิมพ์ `"[Trigger] <gameObject.name> collided with <other.gameObject.name>"`
   - ดึง `Player player = other.GetComponent<Player>()`
   - ถ้าเจอ Player ให้พิมพ์ `"🎉 You Win! Reached the exit!"`

---

## ข้อ 5. ไอเทมยาฟื้นพลัง (ItemPotion)

### 📖 รายละเอียดโจทย์
ในไฟล์ `AS05_ItemPotion.cs` (namespace `Week06.Ex05`) ให้เขียนคลาส `ItemPotion` สืบทอดจาก `MonoBehaviour`

1. **ประกาศฟิลด์แบบ public:**
   - `string name = "Potion"`
   - `int healPoint = 10`
2. **ประกาศเมธอด `OnTriggerEnter2D(Collider2D other)` แบบ private:**
   - พิมพ์ `"[Trigger] <gameObject.name> collided with <other.gameObject.name>"`
   - ดึง `Player player = other.GetComponent<Player>()`
   - ถ้าเจอ Player ให้สั่ง:
     - `player.Heal(healPoint)`
     - พิมพ์ `"✨ Picked up <name>! +<healPoint> Energy"`
     - สั่ง `Destroy(gameObject)` เพื่อทำลายไอเทมออกจากฉาก

---

## 📝 ส่วนการบ้าน (Homework)

### HW 1. ไอเทมดาบเพิ่มพลังโจมตี (ItemSword)

**ไฟล์:** `HW01_ItemSword.cs` (namespace `Week06.HW01`)
1. **ประกาศฟิลด์แบบ public:**
   - `string Name = "Sword"`
   - `int attackBonus = 10`
2. **ประกาศเมธอด `OnTriggerEnter2D(Collider2D other)` แบบ private:**
   - พิมพ์ `"[Trigger] <gameObject.name> collided with <other.gameObject.name>"`
   - ดึง `Player player = other.GetComponent<Player>()`
   - ถ้าเจอ Player ให้สั่ง:
     - `player.IncreaseAttack(attackBonus)`
     - พิมพ์ `"⚔️ Picked up <Name>! +<attackBonus> Attack"`
     - สั่ง `Destroy(gameObject)` เพื่อทำลายไอเทมออกจากฉาก

---

### HW 2. กับดักหนาม (Trap)

**ไฟล์:** `HW02_Trap.cs` (namespace `Week06.HW02`)
1. **ประกาศฟิลด์แบบ public:**
   - `string Name = "Trap"`
   - `int damage = 5`
2. **ประกาศเมธอด `OnTriggerEnter2D(Collider2D other)` แบบ private:**
   - พิมพ์ `"[Trigger] <gameObject.name> collided with <other.gameObject.name>"`
   - ดึง `Player player = other.GetComponent<Player>()`
   - ถ้าเจอ Player ให้สั่ง:
     - `player.isTrapped = true` (ทำให้เดินไม่ได้ 1 ครั้ง)
     - `player.TakeDamage(damage)`
     - พิมพ์ `"⚠️ Stepped on <Name>! Trapped for 1 turn (-<damage> Energy)"`

---

### HW 3. กำแพงพังได้ (Wall)

**ไฟล์:** `HW03_Wall.cs` (namespace `Week06.HW03`)
1. **ประกาศฟิลด์แบบ public:**
   - `string Name = "Wall"`
   - `int durability = 2`
2. **ประกาศเมธอด `Hit()` แบบ public:**
   - ลดค่าความทนทานลง 1 (`durability--`)
   - พิมพ์ `"🧱 <Name> was hit! Remaining durability: <durability>"`
   - หาก `durability <= 0` ให้พิมพ์ `"💥 <Name> destroyed!"` และเรียก `Destroy(gameObject)`
3. **ประกาศเมธอด `OnTriggerEnter2D(Collider2D other)` แบบ private:**
   - พิมพ์ `"[Trigger] <gameObject.name> collided with <other.gameObject.name>"`
   - ดึง `Player player = other.GetComponent<Player>()`
   - ถ้าเจอ Player ให้สั่ง:
     - `Hit()`
     - `player.RevertPosition()` (ผู้เล่นจะกลับไปอยู่ที่เดิม เดินผ่านไม่ได้)

---

### HW 4. กล่องสมบัติสร้างวัตถุใหม่ (Chest)

**ไฟล์:** `HW04_Chest.cs` (namespace `Week06.HW04`)
1. **ประกาศฟิลด์แบบ public:**
   - `string Name = "Chest"`
   - `GameObject spawnPrefab` (Prefab วัตถุที่จะเสกออกมา)
2. **ประกาศเมธอด `OpenChest()` แบบ public:**
   - พิมพ์ `"📦 Opened <Name>!"`
   - หากมี `spawnPrefab != null` ให้เสกสร้างวัตถุใหม่ลงฉากที่ตำแหน่ง **ด้านบน 1 ช่อง (y + 1)**:
     `Vector3 spawnPosition = transform.position + Vector3.up;`
     `Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);`
   - เรียก `Destroy(gameObject)` เพื่อลบกล่องสมบัติออกจากฉาก
3. **ประกาศเมธอด `OnTriggerEnter2D(Collider2D other)` แบบ private:**
   - พิมพ์ `"[Trigger] <gameObject.name> collided with <other.gameObject.name>"`
   - ดึง `Player player = other.GetComponent<Player>()`
   - ถ้าเจอ Player ให้สั่ง `OpenChest()`

---

## 📌 ข้อควรระวัง

- ข้อความที่พิมพ์ต้องตรงตามโจทย์ทุกตัวอักษร
- ตรวจสอบชื่อ namespace ให้ถูกต้อง (`Week06.Ex01` ถึง `Week06.Ex05`, และ `Week06.HW01` ถึง `Week06.HW04`)

