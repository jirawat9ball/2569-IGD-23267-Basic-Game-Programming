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

## 📚 โครงสร้างของ Assignment

- **ข้อ 1–2: พื้นฐาน Method** — void, parameter, default value, return type
- **ข้อ 3: Refactoring** — แยกโค้ดสร้างแผนที่ออกเป็น 5 Method
- **ข้อ 4–7: Method ของตัวละคร** — เดิน, รับดาเมจ, เช็คตาย, ฮีล

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
int GetStringLength(string text)
bool ConvertInttoBool(int sex)
```

**Logic ที่ต้อง implement:**
- `Add` — บวก `a` กับ `b` แล้ว `return` ผลลัพธ์
- `GetStringLength` — `return` จำนวนตัวอักษรของ `text` (ใช้ `text.Length`)
- `ConvertInttoBool` — `return true` ถ้า `sex` เท่ากับ 1 นอกนั้น `return false`

> **สำคัญ:** ทั้ง 3 Method นี้ **ไม่ต้องพิมพ์อะไรออกจอ** หน้าที่คือ *ส่งค่ากลับ* อย่างเดียว คนที่เรียกใช้จะเอาค่าไปทำอะไรต่อก็เรื่องของเขา

**ตัวอย่างการใช้งาน:**
```csharp
int sum = Add(1, 9);                    // sum = 10
int len = GetStringLength("hello");     // len = 5
bool isMale = ConvertInttoBool(1);      // isMale = true
```

---

## ข้อ 3. แยกโค้ดสร้างแผนที่ออกเป็น Method (Refactoring)

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

## ข้อ 4. Method ชื่อ Move

**วัตถุประสงค์:** เขียน Method ที่รับทิศทางเข้ามาแล้วขยับตัวละคร พร้อมหักค่าพลังงาน

**Method Signature:**
```csharp
void Move(Vector2 direction)
```

**Logic ที่ต้อง implement:**
- บวก `direction` เข้ากับ `transform.position` ของตัวละคร (แปลงเป็น `Vector3` ก่อน)
- ทุกครั้งที่เดิน ลด `energy` ลง 1

**ตัวอย่าง:** เริ่มที่ (0, 0) energy = 20 แล้วเดินขวา 3 ครั้ง ขึ้น 3 ครั้ง
```text
ตำแหน่งสุดท้าย x = 3, y = 3, energy = 14
```

---

## ข้อ 5. Method ชื่อ TakeDamage

**วัตถุประสงค์:** เขียน Method ลดพลังงานเมื่อโดนโจมตี พร้อมกันค่าติดลบ

**Method Signature:**
```csharp
void TakeDamage(int Damage)
```

**Logic ที่ต้อง implement:**
- ลด `energy` ลงตามค่า `Damage`
- **ห้ามให้ `energy` ติดลบ** ถ้าน้อยกว่า 0 ให้ตั้งเป็น 0

**ตัวอย่าง:** energy เริ่มที่ 20 โดนดาเมจ 4, 5, 6 → เหลือ `energy = 5`

---

## ข้อ 6. Method ชื่อ CheckDead

**วัตถุประสงค์:** เขียน Method แบบ `private` และเรียกใช้จาก Method อื่นภายในคลาสเดียวกัน

**Method Signature:**
```csharp
private void CheckDead()
```

**Logic ที่ต้อง implement:**
- ถ้า `energy` น้อยกว่าหรือเท่ากับ 0 → พิมพ์ `You Lose`
- **เพิ่มใน `TakeDamage` จากข้อ 5:** หลังลด energy แล้วให้พิมพ์ `Current Energy : <ค่า energy>` แล้วเรียก `CheckDead()`

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

## ข้อ 7. Method ชื่อ Heal

**วัตถุประสงค์:** เขียน Method เพิ่มพลังงานให้ตัวละคร

**Method Signature:**
```csharp
void Heal(int healPoint)
```

**Logic ที่ต้อง implement:**
- เพิ่ม `energy` ขึ้นตามค่า `healPoint` ที่รับเข้ามา

**ตัวอย่าง:** energy เริ่มที่ 20 สั่ง `Heal(4)` → `energy = 24`

---

## 📌 ข้อควรระวัง

- ชื่อ Method, ชนิดค่าที่คืนกลับ และพารามิเตอร์ **ต้องตรงตามที่โจทย์กำหนดเป๊ะ** ระบบตรวจจะเช็คถึงระดับ signature
- ข้อ 2 ให้ `return` ค่า **อย่าพิมพ์ออกจอ**
- ข้อ 6 `CheckDead()` ต้องเป็น `private` และต้องถูกเรียกจากใน `TakeDamage`
- ข้อความที่พิมพ์ต้องตรงเป๊ะ ทั้งตัวพิมพ์เล็กใหญ่ ช่องว่าง และเครื่องหมาย

**ขอให้สนุกกับการเขียนโค้ดครับ 👨‍💻**
