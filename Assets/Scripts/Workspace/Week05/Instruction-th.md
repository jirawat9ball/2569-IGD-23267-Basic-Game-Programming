# Assignment Week 05: Method และการจัดระเบียบโค้ด (Refactoring)

## 📋 ภาพรวมของ Assignment

สัปดาห์นี้เราจะเรียนเรื่อง **Method** — คือการเอาโค้ดที่ทำงานอย่างหนึ่งมามัดรวมกันไว้ แล้วตั้งชื่อให้ เวลาจะใช้ก็แค่เรียกชื่อนั้น ไม่ต้องเขียนซ้ำ

ที่ผ่านมาเราเขียนโค้ดยาว ๆ ไว้ใน `Start()` ทั้งหมด พอโค้ดเยอะขึ้นจะอ่านยากและแก้ยาก การแตกออกเป็น Method ช่วยให้โค้ดเป็นระเบียบ อ่านรู้เรื่อง และเอาไปใช้ซ้ำได้

มีแบบฝึกหัดทั้งหมด **7 ข้อ** เขียนโค้ดในไฟล์ `Assignment_Student_Week05.cs`

## 🎯 จุดประสงค์การเรียนรู้

- ประกาศ Method แบบไม่คืนค่า (`void`) และแบบคืนค่า (`int`, `bool`) ได้
- รับค่าเข้า Method ผ่าน Parameter และกำหนดค่าเริ่มต้น (default value) ได้
- เขียน Method ชื่อเดียวกันหลายแบบ (Overloading) ได้
- แยกโค้ดก้อนใหญ่ใน `Start()` ออกเป็น Method ย่อย ๆ (Refactoring)
- เข้าใจความต่างของ `public` กับ `private`

## 📚 โครงสร้างของสัปดาห์นี้

### 🏫 Lecture (เรียนและทำในคาบ)
- **ข้อ 1–2:** พื้นฐาน Method — void, parameter, default value, return type (ใน `Assignment_Student_Week05.cs`)
- **ข้อ 3–9:** Method ของตัวละคร — Move, Move (Overload), TakeDamage, TakeDamage (Overload), CheckDead, Heal, CanMove, GetEnergy, GetStatus (ใน `Player.cs`)

### 🏠 Homework (การบ้าน)
- **Ex01:** Refactoring — แยกโค้ดสร้างแผนที่ออกเป็น 5 Method (ทำใน `MapGenerator.cs`)
- **Level 1: Simple (Lv01 – Lv06):** ฟังก์ชันพื้นฐานและระบบเกม (เขียนใน `Assignment_Student_Week05.cs`)

---

## 💡 ปูพื้นก่อนเริ่ม: Method คืออะไร

```csharp
public void SayHello()          // ← ประกาศ Method
{
    Debug.Log("Hello");         // ← โค้ดที่จะทำงาน
}

// เวลาใช้ ก็แค่เรียกชื่อ
SayHello();
```

**ส่วนประกอบของ Method:**

| ส่วน | ตัวอย่าง | ความหมาย |
|---|---|---|
| Access Modifier | `public` / `private` | ใครเรียกใช้ได้บ้าง (`public` = ที่อื่นเรียกได้, `private` = เรียกได้แค่ในคลาสตัวเอง) |
| Return Type | `void` / `int` / `bool` | ส่งค่ากลับหรือไม่ (`void` = ไม่ส่งกลับ) |
| ชื่อ Method | `SayHello` | ตั้งให้สื่อว่าทำอะไร |
| Parameter | `(int damage)` | ค่าที่รับเข้ามาทำงาน |

**Overloading** = Method ชื่อเดียวกันหลายตัว แต่รับพารามิเตอร์ต่างกัน C# จะเลือกให้เองว่าจะใช้ตัวไหนตามที่เราส่งค่าเข้าไป

```csharp
public void Greet() { }                        // ไม่รับอะไร
public void Greet(string name) { }             // รับ 1 ตัว
public void Greet(string name, int age) { }    // รับ 2 ตัว
```

---

## ข้อ 1. Method แบบ void และการรับ Parameter

**วัตถุประสงค์:** เขียน Method ชื่อเดียวกันหลายแบบ (Overloading) และกำหนดค่าเริ่มต้นให้พารามิเตอร์

**Method Signature:**
```csharp
void UserNameIdentification()
void UserNameIdentification(string name)
void UserNameIdentification(string name, int age)
void UserCountry(string country = "Thailand")
```

**Logic ที่ต้อง implement:**
- `UserNameIdentification()` — ไม่รับอะไร พิมพ์ `user name is UntitleUser`
- `UserNameIdentification(string name)` — พิมพ์ `user name is ` ต่อด้วยชื่อ
- `UserNameIdentification(string name, int age)` — พิมพ์ `user name is <ชื่อ> age is <อายุ>`
- `UserCountry(string country = "Thailand")` — พิมพ์ค่า `country` ออกมา ถ้าเรียกโดยไม่ใส่ค่าจะได้ `Thailand`

**ผลลัพธ์ที่ต้องได้:** (เรียกตามลำดับ `UserNameIdentification()`, `("boy")`, `("big", 18)`, `UserCountry()`)
```text
user name is UntitleUser
user name is boy
user name is big age is 18
Thailand
```

---

## ข้อ 2. Method แบบมีค่าส่งกลับ (Return Type)

**วัตถุประสงค์:** เขียน Method ที่ **คืนค่ากลับ** ไปให้คนเรียกใช้ แทนที่จะพิมพ์ออกจอเอง

**Method Signature:**
```csharp
int Add(int a, int b)
string GetGreeting(string name)
bool ConvertInttoBool(int sex)
```

**Logic ที่ต้อง implement:**
- `Add` — บวก `a` กับ `b` แล้ว `return` ผลลัพธ์
- `GetGreeting` — นำคำว่า `"Hello, "` ไปต่อกับ `name` แล้ว `return` ผลลัพธ์ (เช่น `"Hello, " + name`)
- `ConvertInttoBool` — `return true` ถ้า `sex` เท่ากับ 1 นอกนั้น `return false`

> **สำคัญ:** ทั้ง 3 Method นี้ **ไม่ต้องพิมพ์อะไรออกจอ** หน้าที่คือ *ส่งค่ากลับ* อย่างเดียว คนที่เรียกใช้จะเอาค่าไปทำอะไรต่อก็เรื่องของเขา

**ตัวอย่างการใช้งาน:**
```csharp
int sum = Add(1, 9);                         // sum = 10
string greeting = GetGreeting("Alice");      // greeting = "Hello, Alice"
bool isMale = ConvertInttoBool(1);           // isMale = true
```

---

## Ex01. แยกโค้ดสร้างแผนที่ออกเป็น Method (Refactoring)

> 📝 **ไฟล์ที่ใช้ทำ:** `MapGenerator.cs` (ให้นักเรียนเปิดทำในไฟล์ `MapGenerator.cs`)

**วัตถุประสงค์:** ฝึกจัดระเบียบโค้ด โดยย้ายโค้ดก้อนใหญ่ใน `Start()` ออกมาเป็น Method ย่อย ๆ ที่มีหน้าที่ชัดเจน

**Method Signature:**
```csharp
void GenerateFloor()
void GenerateWalls()
void GenerateFoods()
void PlacePlayer()
void PlaceExit()
```

**ตัวแปรที่ใช้ร่วมกัน:**
```csharp
public int columns = 3;
public int rows = 4;
public GameObject[] floorTiles;
public GameObject[] wallTiles;
public GameObject[] foodTiles;
public int foodCount = 3;
public GameObject player;
public GameObject exitTile;
```

**Logic ที่ต้อง implement:**

| Method | หน้าที่ |
|---|---|
| `GenerateFloor()` | Nested Loop วนทุกช่อง (x: 0 ถึง columns-1, y: 0 ถึง rows-1) สุ่มพื้นจาก `floorTiles` แล้ว Instantiate ที่ (x, y) |
| `GenerateWalls()` | วน x: -1 ถึง columns และ y: -1 ถึง rows สร้างกำแพงจาก `wallTiles` **เฉพาะช่องขอบนอก** (`x == -1 \|\| x == columns \|\| y == -1 \|\| y == rows`) |
| `GenerateFoods()` | วนลูป `foodCount` รอบ แต่ละรอบสุ่มอาหารจาก `foodTiles` และสุ่มตำแหน่งในแผนที่ แล้ว Instantiate |
| `PlacePlayer()` | Instantiate `player` ที่มุมซ้ายล่าง คือ (0, 0) |
| `PlaceExit()` | Instantiate `exitTile` ที่มุมขวาบน คือ (columns-1, rows-1) |

**ตัวอย่างโค้ดที่ย้ายมาแล้ว:**
```csharp
public void GenerateFloor()
{
    for (int y = 0; y < rows; y++)
    {
        for (int x = 0; x < columns; x++)
        {
            GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
            Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
        }
    }
}
```

> ข้อนี้ไม่ต้องพิมพ์อะไรออกจอ ระบบจะตรวจจากวัตถุที่เกิดขึ้นในฉากว่าถูกตำแหน่งและครบจำนวนไหม

---

## ข้อ 3. Method ชื่อ Move

**วัตถุประสงค์:** เขียน Method ที่รับทิศทางเข้ามาแล้วขยับตัวละคร พร้อมหักค่าพลังงาน

**Method Signature:**
```csharp
void Move(Vector2 direction)
void Move(float x, float y) // Overloading
```

**Logic ที่ต้อง implement:**
- `Move(Vector2 direction)`:
  - เรียกใช้ `CanMove(direction)` ก่อน ถ้าคืนค่า `false` ให้ `return;` ทันที (ไม่ขยับและไม่ลด energy)
  - ถ้าเดินได้ ให้บวก `direction` เข้ากับ `transform.position` ของตัวละคร (แปลงเป็น `Vector3` ก่อน)
  - ลด `energy` ลง 1
- `Move(float x, float y)` (Overloading):
  - เรียกใช้ `Move(new Vector2(x, y))` เพื่อขยับตัวละคร

**ตัวอย่าง:** แผนที่ 8x8 เริ่มที่ (0, 0) energy = 20 แล้วเดินขวา 3 ครั้ง ขึ้น 3 ครั้ง
```text
ตำแหน่งสุดท้าย x = 3, y = 3, energy = 14
```

---

## ข้อ 4. Method ชื่อ TakeDamage

**วัตถุประสงค์:** เขียน Method ลดพลังงานเมื่อโดนโจมตี พร้อมกันค่าติดลบ

**Method Signature:**
```csharp
void TakeDamage(int Damage)
void TakeDamage(int Damage, string attacker) // Overloading
```

**Logic ที่ต้อง implement:**
- `TakeDamage(int Damage)`:
  - ลด `energy` ลงตามค่า `Damage`
  - **ห้ามให้ `energy` ติดลบ** ถ้าน้อยกว่า 0 ให้ตั้งเป็น 0
  - พิมพ์ `Current Energy : <ค่า energy>` แล้วเรียก `CheckDead()`
- `TakeDamage(int Damage, string attacker)` (Overloading):
  - พิมพ์ `Attacked by <ชื่อ attacker>` ผ่าน `Debug.Log`
  - เรียกใช้ `TakeDamage(Damage)` เพื่อลด energy

**ตัวอย่าง:** energy เริ่มที่ 20 โดนดาเมจ 4, 5, 6 → เหลือ `energy = 5`

---

## ข้อ 5. Method ชื่อ CheckDead

**วัตถุประสงค์:** เขียน Method แบบ `private` และเรียกใช้จาก Method อื่นภายในคลาสเดียวกัน

**Method Signature:**
```csharp
private void CheckDead()
```

**Logic ที่ต้อง implement:**
- ถ้า `energy` น้อยกว่าหรือเท่ากับ 0 → พิมพ์ `You Lose`
- **เพิ่มใน `TakeDamage` จากข้อ 4:** หลังลด energy แล้วให้พิมพ์ `Current Energy : <ค่า energy>` แล้วเรียก `CheckDead()`

> `private` แปลว่า Method นี้เรียกได้เฉพาะจากภายในคลาสตัวเองเท่านั้น สคริปต์อื่นเรียกไม่ได้

**ผลลัพธ์ที่ต้องได้:** (energy เริ่มที่ 40 โดนดาเมจ 10 สี่ครั้ง)
```text
Current Energy : 30
Current Energy : 20
Current Energy : 10
Current Energy : 0
You Lose
```

---

## ข้อ 6. Method ชื่อ Heal

**วัตถุประสงค์:** เขียน Method เพิ่มพลังงานให้ตัวละคร

**Method Signature:**
```csharp
void Heal(int healPoint)
```

**Logic ที่ต้อง implement:**
- เพิ่ม `energy` ขึ้นตามค่า `healPoint` ที่รับเข้ามา (มี default value = 10)

**ตัวอย่าง:** energy เริ่มที่ 20 สั่ง `Heal(4)` → `energy = 24`, สั่ง `Heal()` → `energy = 34`

---

## ข้อ 7. Method ชื่อ CanMove (Return Type bool)

**วัตถุประสงค์:** เขียน Method ตรวจสอบว่าตำแหน่งถัดไปที่จะเดินออกนอกแผนที่หรือไม่ (Return Type bool)

**Method Signature:**
```csharp
public bool CanMove(Vector2 direction)
```

**Logic ที่ต้อง implement:**
- คำนวณตำแหน่งเป้าหมายถัดไปโดยนำตำแหน่งปัจจุบัน `(Vector2)transform.position + direction`
- ตรวจสอบว่าตำแหน่งเป้าหมายอยู่ในขอบเขตแผนที่หรือไม่:
  - แกน x ต้องอยู่ในช่วง `0` ถึง `columns - 1` (`nextPos.x >= 0 && nextPos.x < columns`)
  - แกน y ต้องอยู่ในช่วง `0` ถึง `rows - 1` (`nextPos.y >= 0 && nextPos.y < rows`)
- ถ้าอยู่ในแผนที่ให้ `return true` นอกนั้นให้ `return false`

**ตัวอย่าง:** แผนที่ขนาด `columns = 8, rows = 8` ตัวละครอยู่ที่ `(0, 0)`
- สั่ง `CanMove(Vector2.right)` -> ตำแหน่งถัดไปคือ `(1, 0)` -> คืนค่า `true`
- สั่ง `CanMove(Vector2.left)` -> ตำแหน่งถัดไปคือ `(-1, 0)` -> คืนค่า `false` (ออกนอกขอบซ้าย)

---

## ข้อ 8. Method ชื่อ GetEnergy (Return Type int)

**วัตถุประสงค์:** เขียน Method เพื่ออ่านค่าพลังงานปัจจุบันของตัวละคร (Return Type int)

**Method Signature:**
```csharp
public int GetEnergy()
```

**Logic ที่ต้อง implement:**
- `return` ค่าตัวแปร `energy` ของตัวละคร

---

## ข้อ 9. Method ชื่อ GetStatus (Return Type string)

**วัตถุประสงค์:** เขียน Method สรุปสถานะพลังงานของตัวละครในรูปแบบข้อความ (Return Type string)

**Method Signature:**
```csharp
public string GetStatus()
```

**Logic ที่ต้อง implement:**
- `return` ข้อความ `"Player Energy: "` ต่อด้วยค่า `energy` (เช่น `"Player Energy: 20"`)

---

# 🏠 ส่วนการบ้าน: Level 1: Simple (Lv01 – Lv06)

ให้นักเรียนสร้าง Method ต่อไปนี้ลงในไฟล์ `Assignment_Student_Week05.cs` ภายใต้ Region `#region Level 1: Simple`

---

### Lv01. Method คำนวณดาเมจ: `Lv01_CalculateDamage`

**วัตถุประสงค์:** คำนวณพลังโจมตีสุทธิจากพลังโจมตีพื้นฐานและตัวคูณดาเมจ

**Method Signature:**
```csharp
public int Lv01_CalculateDamage(int baseDamage, float multiplier)
```

**Logic:**
- เอา `baseDamage` คูณกับ `multiplier`
- แปลงผลลัพธ์เป็น `int` แล้วคืนค่ากลับไป เช่น `(int)(baseDamage * multiplier)`
- **ตัวอย่าง:** `Lv01_CalculateDamage(100, 1.5f)` ได้ `150`, `Lv01_CalculateDamage(10, 1.25f)` ได้ `12`

---

### Lv02. Method ตรวจสอบมานาในการร่ายเวท: `Lv02_CanCastSpell`

**วัตถุประสงค์:** ตรวจสอบว่าตัวละครมีมานาเพียงพอสำหรับใช้สกิลหรือไม่

**Method Signature:**
```csharp
public bool Lv02_CanCastSpell(int currentMana, int manaCost)
```

**Logic:**
- ถ้า `currentMana >= manaCost` ให้คืนค่า `true` นอกนั้นคืนค่า `false`
- **ตัวอย่าง:** `Lv02_CanCastSpell(50, 30)` ได้ `true`, `Lv02_CanCastSpell(20, 30)` ได้ `false`

---

### Lv03. Method ค้นหาคะแนนสูงสุด: `Lv03_FindHighestScore`

**วัตถุประสงค์:** ค้นหาค่าตัวเลขที่มากที่สุดใน Array

**Method Signature:**
```csharp
public int Lv03_FindHighestScore(int[] scores)
```

**Logic:**
- วนลูปตรวจสอบสมาชิกทุกตัวใน `scores` เพื่อหาค่าที่มากที่สุด แล้ว return ค่านั้น
- หาก Array เป็น `null` หรือไม่มีข้อมูล (`scores.Length == 0`) ให้คืนค่า `0`
- **ตัวอย่าง:** `Lv03_FindHighestScore(new int[] { 10, 45, 99, 23, 7 })` ได้ `99`

---

### Lv04. Method คำนวณคะแนนรวม: `Lv04_CalculateTotalScore`

**วัตถุประสงค์:** หาผลบวกของคะแนนทั้งหมดใน Array

**Method Signature:**
```csharp
public int Lv04_CalculateTotalScore(int[] scores)
```

**Logic:**
- วนลูปบวกสะสมค่าทั้งหมดใน `scores` แล้ว return ผลรวม
- หาก Array เป็น `null` หรือไม่มีข้อมูล ให้คืนค่า `0`
- **ตัวอย่าง:** `Lv04_CalculateTotalScore(new int[] { 10, 20, 30 })` ได้ `60`

---

### Lv05. Method ตรวจสอบการเลเวลอัป: `Lv05_CheckLevelUp`

**วัตถุประสงค์:** ตรวจสอบว่า EXP สะสมเพียงพอสำหรับการเลเวลอัปหรือไม่

**Method Signature:**
```csharp
public bool Lv05_CheckLevelUp(int currentExp, int requiredExp)
```

**Logic:**
- ถ้า `currentExp >= requiredExp` ให้คืนค่า `true` นอกนั้นคืนค่า `false`
- **ตัวอย่าง:** `Lv05_CheckLevelUp(120, 100)` ได้ `true`, `Lv05_CheckLevelUp(99, 100)` ได้ `false`

---

### Lv06. Method ควบคุมพลังชีวิตให้อยู่ในช่วง: `Lv06_ClampHealth`

**วัตถุประสงค์:** ป้องกันไม่ให้พลังชีวิตต่ำกว่าค่าต่ำสุด (min) หรือสูงเกินค่าสูงสุด (max)

**Method Signature:**
```csharp
public int Lv06_ClampHealth(int currentHealth, int minHealth, int maxHealth)
```

**Logic:**
- ถ้า `currentHealth < minHealth` ให้คืนค่า `minHealth`
- ถ้า `currentHealth > maxHealth` ให้คืนค่า `maxHealth`
- ถ้าอยู่ในช่วง ให้คืนค่า `currentHealth` เดิม
- **ตัวอย่าง:** `Lv06_ClampHealth(120, 0, 100)` ได้ `100`, `Lv06_ClampHealth(-10, 0, 100)` ได้ `0`, `Lv06_ClampHealth(50, 0, 100)` ได้ `50`

---

## 📌 ข้อควรระวัง

- ชื่อ Method, ชนิดค่าที่คืนกลับ และพารามิเตอร์ **ต้องตรงตามที่โจทย์กำหนดเป๊ะ** ระบบตรวจจะเช็คถึงระดับ signature
- ข้อ 2 ให้ `return` ค่า **อย่าพิมพ์ออกจอ**
- ข้อ 5 `CheckDead()` ต้องเป็น `private` และต้องถูกเรียกจากใน `TakeDamage`
- ข้อความที่พิมพ์ต้องตรงเป๊ะ ทั้งตัวพิมพ์เล็กใหญ่ ช่องว่าง และเครื่องหมาย

**ขอให้สนุกกับการเขียนโค้ดครับ 👨‍💻**
