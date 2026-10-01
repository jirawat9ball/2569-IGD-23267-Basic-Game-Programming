# Assignment Week 06: การเขียนโปรแกรมเชิงวัตถุ (OOP)

## 📋 ภาพรวมของ Assignment

สัปดาห์นี้เราจะเรียน **OOP (Object-Oriented Programming)** — แนวคิดที่เอา "สิ่งของ" ในโลกจริงมาทำเป็นโค้ด

เช่น รถยนต์ 1 คัน มี *คุณสมบัติ* (ชื่อ, สี, ความเร็ว) และมี *พฤติกรรม* (วิ่ง, เลี้ยว, บีบแตร) เราก็เขียนโค้ดให้มันเหมือนจริงแบบนั้นได้

**คลาส (Class)** = พิมพ์เขียว บอกว่าสิ่งนั้นมีอะไรและทำอะไรได้
**อ็อบเจกต์ (Object)** = ของจริงที่สร้างขึ้นมาจากพิมพ์เขียว

```csharp
Car myCar = new Car();   // สร้างรถ 1 คันจากพิมพ์เขียว Car
myCar.name = "Toyota";
myCar.Move();
```

มีแบบฝึกหัดทั้งหมด **8 ข้อ**

- **ข้อ 1–5** ฝึก OOP พื้นฐานทีละเรื่อง
- **ข้อ 6–8** เอา OOP ไปใช้จริงในเกมเดินแผนที่ (ตีศัตรู / เก็บยา / เก็บดาบ)

## 🎯 จุดประสงค์การเรียนรู้

- สร้างคลาสของตัวเอง กำหนดฟิลด์และเมธอดได้
- ใช้ Constructor กำหนดค่าเริ่มต้นตอนสร้างอ็อบเจกต์
- ใช้การสืบทอด (Inheritance) เพื่อไม่ต้องเขียนโค้ดซ้ำ
- เข้าใจและใช้ Access Modifier (`public` / `protected` / `private`) ได้ถูกที่
- ใช้ `virtual` และ `override` ให้คลาสลูกเปลี่ยนพฤติกรรมของคลาสแม่ได้

## 📚 รายการไฟล์ที่นักเรียนต้องสร้างและจัดการ

ในสัปดาห์นี้ **นักเรียนจะต้องสร้างไฟล์ C# Script ด้วยตัวเองใน Unity Editor** ตามโครงสร้างดังนี้:

| ข้อ | ไฟล์ที่ต้องสร้าง/เขียน | ตำแหน่งโฟลเดอร์ | สิ่งที่ต้องทำ |
|---|---|---|---|
| 1 | `Ex01_Car.cs` | `Assets/.../Week06/` | สร้างไฟล์และเขียนคลาส `Car` |
| 2 | `LCT02ClassConstructor.cs` | `Assets/.../Week06/` | สร้างไฟล์และเขียน Constructor ของ `Dog` |
| 3 | `LCT03Inheritance.cs` | `Assets/.../Week06/` | สร้างไฟล์และเขียนคลาส `Animal`, `Dog`, `Bird` สืบทอดกัน |
| 4 | `LCT04AccessModifier.cs` | `Assets/.../Week06/` | สร้างไฟล์และจัดการ Access Modifiers (`Feed`, `MakeSound`) |
| 5 | `LCT05VirtualOverride.cs` | `Assets/.../Week06/` | สร้างไฟล์และใส่ `virtual` กับ `override` |
| 6 | `Game/Enemy.cs` + `Game/Character.cs` | `Week06/Game/` | สร้าง/เขียนคลาส `Enemy` และส่วนตีศัตรูใน `Move()` |
| 7 | `Game/ItemPotion.cs` + `Game/Character.cs` | `Week06/Game/` | สร้าง/เขียนคลาส `ItemPotion` และส่วนเก็บยาใน `Move()` |
| 8 | `Game/ItemSword.cs` + `Game/Character.cs` | `Week06/Game/` | สร้าง/เขียนคลาส `ItemSword` และส่วนเก็บดาบใน `Move()` |
| ทุกข้อ | `Assignment_Student_Week06.cs` | `Week06/` | สคริปต์หลักสำหรับรัน Demo และส่งตรวจงาน |

> **ไฟล์ที่ไม่ต้องสร้าง/แก้:** `Game/Identity.cs`, `Game/MapGenerator.cs`, `Game/Player.cs` — เป็นโครงสร้างของเกมที่เตรียมไว้ให้แล้ว

---

## 🛠️ ขั้นตอนการสร้างไฟล์ C# Script ด้วยตัวเองใน Unity Editor

ในการทำงานจริงกับ Unity นักพัฒนาจะต้องสร้างสคริปต์ใหม่ด้วยตนเองเสมอ โดยมีขั้นตอนดังนี้:

1. **เปิดหน้าต่าง Project Window ใน Unity:**
   - นำทางไปยังโฟลเดอร์ที่ต้องการสร้างไฟล์ เช่น `Assets/Scripts/Workspace/Week06/` (หรือโฟลเดอร์ `Game/`)
2. **สร้าง C# Script ใหม่:**
   - **คลิกขวา** บนพื้นที่ว่างใน Project Window
   - เลือก **Create** > **C# Script** (หรือ **Scripting** > **C# Script** ใน Unity เวอร์ชั่นใหม่)
3. **ตั้งชื่อไฟล์ทันที (⚠️ กฎเหล็กของ Unity):**
   - พิมพ์ชื่อไฟล์ให้ตรงกับโจทย์เป๊ะ ๆ เช่น `Ex01_Car` หรือ `Enemy` แล้วกด Enter
   - **ข้อควรระวัง:** ชื่อไฟล์ (`.cs`) และชื่อคลาสภายใน (`public class ...`) **ต้องสะกดตรงกันทุกตัวอักษร** (Case-sensitive) ห้ามมีเว้นวรรค หากชื่อไฟล์ไม่ตรงกับชื่อคลาส Unity จะขึ้นข้อความแจ้งเตือนและจะไม่สามารถแนบสคริปต์ลงบน GameObject ได้!
4. **เปิดไฟล์ขึ้นมาเขียนโค้ด:**
   - ดับเบิลคลิกที่ไฟล์สคริปต์เพื่อเปิดในโปรแกรมเขียนโค้ด (VS Code / Visual Studio / Rider)

---

## 💡 วิธีคิดแบบ Unity (Component-Based & MonoBehaviour)

เมื่อสร้าง C# Script ขึ้นมาใหม่ใน Unity ตัว Editor จะสร้างโค้ดเริ่มต้นที่สืบทอด `: MonoBehaviour` ให้ทันที:

```csharp
using UnityEngine;

public class MyScript : MonoBehaviour
{
    void Start() { }
    void Update() { }
}
```

### 🧠 ความแตกต่างระหว่าง "Pure C# Class" กับ "MonoBehaviour"

| หัวข้อ | Pure C# Class (แบบข้อ 1–5 ในใบงาน) | Unity MonoBehaviour (แบบในเกมข้อ 6–8) |
|---|---|---|
| **การสร้างอ็อบเจกต์** | ใช้คำสั่ง `new MyClass()` | **ห้ามใช้ `new` เด็ดขาด!** ต้องใช้ `AddComponent<T>()` หรือแปะบน Prefab/GameObject |
| **การตั้งค่าเริ่มต้น** | ใช้ Constructor (`public MyClass(...)`) | ใช้ Unity Lifecycle (`Awake()`, `Start()`) หรือสร้างฟังก์ชัน `Initialize(...)` |
| **ตำแหน่งในระบบ** | อยู่ใน RAM ของโปรแกรม | ต้องเป็น **Component** แปะอยู่กับ **GameObject** ใน Scene เสมอ |
| **การปรับค่าข้อมูล** | แก้ไขผ่านโค้ดเท่านั้น | ใช้ `[SerializeField]` หรือ `public` ให้ปรับแต่งค่าผ่าน **Inspector** ได้ |

> 📌 **สรุปวิธีคิด Unity:**
> - **GameObject** เปรียบเสมือนตัวหุ่นเปล่า ๆ ในเกม (เช่น ผู้เล่น, ศัตรู, รถ)
> - **MonoBehaviour Script** คือ "ชิ้นส่วน/ความสามารถ" (Component) ที่เราสร้างขึ้น แล้วนำไปแปะใส่ตัวหุ่น
> - สังเกตในข้อ 6–8 โค้ดของเกมจะสืบทอด `MonoBehaviour -> Identity -> Character -> Enemy` ซึ่งเป็นโครงสร้าง Component-Based แท้จริงของ Unity

---

## 💡 ปูพื้น: ส่วนประกอบของคลาส

```csharp
public class Car          // ← Access Modifier + ชื่อคลาส
{
    public string name;   // ← Field (ตัวแปรของคลาส)

    public Car() { }      // ← Constructor (เมธอดพิเศษ ทำงานตอนสร้างอ็อบเจกต์)

    public void Move()    // ← Method (พฤติกรรม)
    {
        Debug.Log("Car is moving");
    }
}
```

**Access Modifier** ควบคุมว่าใครเข้าถึงได้บ้าง:

| คำ | ใครเข้าถึงได้ |
|---|---|
| `public` | ทุกที่ |
| `protected` | ในคลาสตัวเอง **และคลาสลูกที่สืบทอดไป** |
| `private` | ในคลาสตัวเองเท่านั้น |

---

## ข้อ 1. สร้างคลาส Car

**วัตถุประสงค์:** ฝึกสร้างไฟล์และคลาสของตัวเอง กำหนดฟิลด์และเมธอด แล้วสร้างอ็อบเจกต์มาใช้งาน

**การสร้างไฟล์:**
- ใน Unity ให้คลิกขวาที่โฟลเดอร์ `Week06/` > Create > C# Script ตั้งชื่อว่า `AS01_Car.cs`

**Logic ที่ต้อง implement:**

ในคลาส `Car` (อยู่ใน namespace `Week06.Ex01`)
- ฟิลด์แบบ `public` 3 ตัว: `name` (string), `color` (string), `speed` (float)
- เมธอดแบบ `public` 3 ตัว ไม่มีค่าส่งกลับ ไม่รับพารามิเตอร์
  - `Move()` → พิมพ์ `Car is moving`
  - `Turn()` → พิมพ์ `Car is turning`
  - `Honk()` → พิมพ์ `Car is honking`

ใน `Ex01_CarDemo()` (ในไฟล์ `Assignment_Student_Week06.cs`)
- สร้างรถด้วย `new Car()` กำหนด `name`, `color`, `speed`
- เรียก `Move()`, `Turn()`, `Honk()` ตามลำดับ

**ผลลัพธ์ที่ต้องได้:**
```text
Car is moving
Car is turning
Car is honking
```

---

## ข้อ 2. Constructor

**วัตถุประสงค์:** ใช้ Constructor กำหนดค่าให้อ็อบเจกต์ตั้งแต่ตอนสร้าง แทนการมาไล่กำหนดทีละบรรทัด

**การสร้างไฟล์:**
- ใน Unity ให้คลิกขวาที่โฟลเดอร์ `Week06/` > Create > C# Script ตั้งชื่อว่า `AS02_ClassConstructor.cs`

**Logic ที่ต้อง implement:**

ในคลาส `Dog` (อยู่ใน namespace `Week06.Ex02`)
- กำหนดฟิลด์ `public string name;`, `public string breed;`, `public int age;`
- เขียน Constructor รับพารามิเตอร์ 3 ตัว: `name`, `breed`, `age`
- ใช้ `this.name = name;` เพื่อบอกว่าตัวไหนคือฟิลด์ ตัวไหนคือพารามิเตอร์

```csharp
public Dog(string name, string breed, int age)
{
    this.name = name;
    this.breed = breed;
    this.age = age;
}
```

> Constructor ใช้ชื่อเดียวกับคลาสเสมอ และ **ไม่มี return type**

ใน `Ex02_DogDemo()`
- สร้าง `dog1` ด้วย Constructor โดยให้ `name` เป็น `"Buddy"`
- เรียก `Bark()`, `WagTail()`, `StopBarking()`

**ผลลัพธ์ที่ต้องได้:**
```text
Buddy is barking
Buddy is wagging tail
Buddy stopped barking
```

---

## ข้อ 3. การสืบทอดคลาส (Inheritance)

### 📖 ความรู้เสริม

การสืบทอดช่วยให้สร้างคลาสใหม่โดยใช้คลาสเดิมเป็นฐาน ทำให้โค้ดซ้ำน้อยลงและจัดการง่ายขึ้น

เช่น มีคลาส `Animal` ที่มีชื่อและส่งเสียงได้ แล้วให้ `Dog` กับ `Bird` สืบทอดไป — ทั้งคู่จะทำทุกอย่างที่ `Animal` ทำได้ **บวกกับ** ความสามารถเฉพาะตัว

**ประโยชน์:** ลดโค้ดซ้ำ · จัดระเบียบโค้ดดีขึ้น · ขยายโปรแกรมง่าย · เตรียมทางไปสู่ Polymorphism

**การสร้างไฟล์:**
- ใน Unity ให้คลิกขวาที่โฟลเดอร์ `Week06/` > Create > C# Script ตั้งชื่อว่า `AS03_Inheritance.cs`

**Logic ที่ต้อง implement:** (ใน namespace `Week06.Ex03`)
- ในไฟล์ `AS03_Inheritance.cs` สามารถประกาศคลาส `Animal`, `Dog`, และ `Bird` ได้
- ทำให้ `Dog` สืบทอดจาก `Animal` โดยเขียน `: Animal` ต่อท้ายชื่อคลาส → พอสืบทอดแล้วจะใช้ตัวแปร `name` ได้เลย
- ในเมธอด `Walk()` พิมพ์ `Dog <ชื่อ> is walking`
- ทำให้ `Bird` สืบทอดจาก `Animal` เหมือนกัน
- ในเมธอด `Fly()` พิมพ์ `Bird <ชื่อ> is flying`

ใน `Ex03_InheritanceDemo()`
- สร้าง `Dog` ตั้งชื่อ `"Buddy"` → เรียก `MakeSound()` แล้ว `Walk()`
- สร้าง `Bird` ตั้งชื่อ `"Twitty"` → เรียก `MakeSound()` แล้ว `Fly()`

**ผลลัพธ์ที่ต้องได้:**
```text
Animal Buddy is making sound
Dog Buddy is walking
Animal Twitty is making sound
Bird Twitty is flying
```

---

## ข้อ 4. Access Modifiers

### 📖 ความรู้เสริม

ตัวแปรในคลาสกำหนดระดับการเข้าถึงได้ เพื่อคุมว่าใครแก้ข้อมูลได้บ้าง

- **public** — เข้าถึงได้จากทุกที่
- **protected** — เข้าถึงได้จากในคลาสเดียวกันและคลาสที่สืบทอดไป แต่คลาสอื่นที่ไม่เกี่ยวข้องเข้าไม่ได้
- **private** — เข้าถึงได้เฉพาะในคลาสนั้นเท่านั้น

**การสร้างไฟล์:**
- ใน Unity ให้คลิกขวาที่โฟลเดอร์ `Week06/` > Create > C# Script ตั้งชื่อว่า `AS04_AccessModifier.cs`

ในคลาส `Animal` (namespace `Week06.Ex04`) กำหนดฟิลด์:
```csharp
public string name = "";
protected string specie = "";
private int health = 10;
```

**Logic ที่ต้อง implement:**

1. เมธอด `Feed` — `public`, ไม่มีค่าส่งกลับ, รับ `int food`
   - บวก `food` เข้ากับ `health`
   - พิมพ์ `<ชื่อ> got <food> food`
2. เมธอด `MakeSound` — `public`, ไม่มีค่าส่งกลับ, ไม่รับพารามิเตอร์
   - ถ้า `health > 50` → พิมพ์ `<ชื่อ> happy!`
   - ถ้าไม่ใช่ → พิมพ์ `<ชื่อ> weak!`
3. Constructor ของ `Dog` (รับ `string name`)
   - กำหนด `specie = "Dog"` (ทำได้เพราะเป็น `protected`)
   - กำหนด `this.name = name;`
   - > `health` เป็น `private` ของ `Animal` คลาสลูกอย่าง `Dog` **เข้าถึงไม่ได้**
4. ใน `Ex04_AccessModifierDemo()`
   - สร้าง `Dog("Buddy")` แล้วพิมพ์ `my name is <ชื่อ>`
   - เรียก `MakeSound()` → `Feed(50)` → `MakeSound()` อีกครั้ง

**ผลลัพธ์ที่ต้องได้:**
```text
my name is Buddy
Buddy weak!
Buddy got 50 food
Buddy happy!
```

---

## ข้อ 5. Virtual และ Override

### 📖 ความรู้เสริม

`virtual` และ `override` ทำให้คลาสลูกเปลี่ยนพฤติกรรมของคลาสแม่ได้ โดยไม่ต้องไปแก้โค้ดคลาสแม่

- **คลาสแม่** เขียน `public virtual void MakeSound()` — เปิดให้ลูกเขียนทับได้
- **คลาสลูก** เขียน `public override void MakeSound()` — เขียนทับของแม่

**การสร้างไฟล์:**
- ใน Unity ให้คลิกขวาที่โฟลเดอร์ `Week06/` > Create > C# Script ตั้งชื่อว่า `AS05_VirtualOverride.cs`

**Logic ที่ต้อง implement:** (ใน namespace `Week06.Ex05`)
- ในคลาส `Animal` เติมคำว่า `virtual` หน้าเมธอด `MakeSound` (พิมพ์ `Generic animal sound`)
- ในคลาส `Dog` เติมคำว่า `override` หน้าเมธอด `MakeSound` และเปลี่ยนข้อความเป็น `Woof!`

ใน `Ex05_VirtualOverrideDemo()`
- สร้าง `Dog` เรียก `MakeSound()`
- สร้าง `Animal` เรียก `MakeSound()`

**ผลลัพธ์ที่ต้องได้:**
```text
Woof!
Generic animal sound
```

> **ลองสังเกต:** ถ้าเขียน `Animal a = new Dog(); a.MakeSound();` จะได้ `Woof!` — เพราะ `override` ทำให้เรียกเมธอดของคลาสลูกเสมอ นี่คือ **Polymorphism**

---

---

# เกมเดินแผนที่ (ข้อ 6–8)

สามข้อนี้เอา OOP ที่เรียนมาไปใช้จริงในเกมเล็ก ๆ ผู้เล่นเดินบนแผนที่ 10×10 เก็บของ และตีศัตรู

**โครงคลาสของเกม**
```
MonoBehaviour
└── Identity                 ← ของทุกอย่างบนแผนที่ (Name, positionX/Y, mapGenerator, Hit())
    ├── Character            ← มี energy, attackPoint, Move(), Attack(), TakeDamage(), Heal()
    │   ├── Player
    │   └── Enemy            ← ข้อ 6
    ├── ItemPotion           ← ข้อ 7
    └── ItemSword            ← ข้อ 8
```

**กติกาของเกม**
- เดินเข้าช่องว่าง → เสีย energy 1
- เดินเข้าช่องที่มีของ (ยา/ดาบ/ศัตรู) → **ไม่เสีย** energy
- `Hit()` คือ "สิ่งที่เกิดขึ้นเมื่อถูกเดินชน" — ยาก็เพิ่มเลือด ดาบก็เพิ่มพลังโจมตี ศัตรูก็ตีสวน

**ฉากตัวอย่างที่ระบบเตรียมไว้**

| สิ่งของ | ตำแหน่ง | ค่า |
|---|---|---|
| Player | (0, 0) | energy 100, attackPoint 10 |
| Potion1 | (2, 2) | healPoint 20 |
| Sword1 | (3, 2) | attackBonus 10 |
| Enemy1 | (3, 3) | energy 60, attackPoint 5 |

---

## ข้อ 6. คลาส Enemy และระบบต่อสู้

**วัตถุประสงค์:** ใช้การสืบทอดและ `override` สร้างศัตรูที่ตีสวนกลับได้ (สืบทอดโครงสร้าง MonoBehaviour ผ่าน `Character`)

**การสร้างไฟล์:**
- ใน Unity ให้คลิกขวาที่โฟลเดอร์ `Week06/Game/` > Create > C# Script ตั้งชื่อว่า `Enemy.cs`
- (ส่วน `Character.cs` มีไฟล์อยู่แล้วในโฟลเดอร์ `Game/` ให้เปิดขึ้นมาแก้ไขเมธอด `Move()`)

**Logic ที่ต้อง implement:**

ใน `Enemy.cs`
- เขียน `class Enemy` ให้สืบทอดจาก `Character`
- เขียน `override void Hit()` ทำตามลำดับ
  1. ถ้า `energy <= 0` (ตายแล้ว) → `return;` ออกไปเลย ไม่ต้องทำอะไร
  2. ถ้ายังไม่ตาย → ตีผู้เล่นกลับด้วย `this.Attack(mapGenerator.player, attackPoint);`

ใน `Character.cs` ส่วน `else if (IsEnemy(toX, toY))`
1. เก็บศัตรูไว้ในตัวแปร: `Enemy e = mapGenerator.enemies[toX, toY];`
2. **ผู้เล่นตีก่อน:** `this.Attack(e, attackPoint);`
3. ถ้าศัตรู **ยังไม่ตาย** (`e.energy > 0`) → ให้ศัตรูตีสวน `mapGenerator.enemies[toX, toY].Hit();`
4. ถ้าศัตรู **ตายแล้ว** → ผู้เล่นเดินเข้าไปที่ช่องนั้น (กำหนด `positionX`, `positionY`, `transform.position`)

**ผลลัพธ์ของฉากตัวอย่างทั้งหมด:**
```text
You got Potion1 : 20
You got Sword1 : 10
Player energy after picking up potion: 117
Player attack point after picking up sword: 20
first attack ...
Player energy after attack: 112
Enemy energy after attack: 40
second attack ...
Player energy after attack: 107
Enemy energy after attack: 20
thrid attack ...
Player energy after attack: 107
Enemy energy after attack: 0
```

**ที่มาของตัวเลข**
```
เริ่ม energy 100, attack 10
เดินช่องว่าง 3 ช่อง            → 100 − 3 = 97
เหยียบยา (2,2)                 → 97 + 20 = 117
เหยียบดาบ (3,2)                → attack 10 + 10 = 20
ตีครั้งที่ 1: ศัตรู 60 − 20 = 40 ยังไม่ตาย → สวน 5 → ผู้เล่น 117 − 5 = 112
ตีครั้งที่ 2: ศัตรู 40 − 20 = 20 ยังไม่ตาย → สวน 5 → ผู้เล่น 112 − 5 = 107
ตีครั้งที่ 3: ศัตรู 20 − 20 = 0  ตายแล้ว   → ไม่สวน → ผู้เล่น 107 เท่าเดิม แล้วเดินเข้าช่อง
```

---

## ข้อ 7. คลาส ItemPotion (ยาเพิ่มพลัง)

**วัตถุประสงค์:** สร้างไอเทมที่สืบทอดจาก `Identity` (MonoBehaviour) แล้ว `override Hit()` ให้ทำงานตอนถูกเดินชน

**การสร้างไฟล์:**
- ใน Unity ให้คลิกขวาที่โฟลเดอร์ `Week06/Game/` > Create > C# Script ตั้งชื่อว่า `ItemPotion.cs`

**Logic ที่ต้อง implement:**

ใน `ItemPotion.cs`
- เขียน `class ItemPotion` ให้สืบทอดจาก **`Identity`** (ไม่ใช่ `Character` เพราะยาไม่มี energy ไม่ต้องเดิน)
- ประกาศตัวแปร `public int healPoint = 10;`
- เขียน `override void Hit()`
  1. พิมพ์ `You got <ชื่อไอเทม> : <healPoint>`
  2. เพิ่มเลือดผู้เล่น: `mapGenerator.player.Heal(healPoint);`
  3. เอาไอเทมออกจากแผนที่: ตั้ง `mapGenerator.mapData[positionX, positionY] = 0;` แล้วทำลายวัตถุ

ใน `Character.cs` ส่วน `if (IsPotion(toX, toY))`
- เรียก `mapGenerator.potions[toX, toY].Hit();` แล้วขยับผู้เล่นเข้าไปที่ช่องนั้น

**ผลลัพธ์ที่ต้องได้:** (เดินไปเหยียบยาที่ (2,2))
```text
You got Potion1 : 20
Player energy after picking up potion: 117
```

---

## ข้อ 8. คลาส ItemSword (ดาบเพิ่มพลังโจมตี)

**วัตถุประสงค์:** ทำแบบเดียวกับยา แต่เปลี่ยนเป็นเพิ่มพลังโจมตี

**การสร้างไฟล์:**
- ใน Unity ให้คลิกขวาที่โฟลเดอร์ `Week06/Game/` > Create > C# Script ตั้งชื่อว่า `ItemSword.cs`

**Logic ที่ต้อง implement:**

ใน `ItemSword.cs`
- เขียน `class ItemSword` ให้สืบทอดจาก **`Identity`**
- ประกาศตัวแปร `public int attackBonus = 10;`
- เขียน `override void Hit()`
  1. พิมพ์ `You got <ชื่อไอเทม> : <attackBonus>`
  2. เพิ่มพลังโจมตีผู้เล่น: `mapGenerator.player.IncreaseAttack(attackBonus);`
  3. เอาไอเทมออกจากแผนที่แบบเดียวกับยา

ใน `Character.cs` ส่วน `else if (IsSword(toX, toY))`
- เรียก `mapGenerator.swords[toX, toY].Hit();` แล้วขยับผู้เล่นเข้าไปที่ช่องนั้น

**ผลลัพธ์ที่ต้องได้:** (เดินผ่านยาแล้วไปเหยียบดาบที่ (3,2))
```text
You got Potion1 : 20
You got Sword1 : 10
Player attack point after picking up sword: 20
```

---

## 📌 ข้อควรระวัง

- ข้อความที่พิมพ์ต้องตรงเป๊ะ ทั้งตัวพิมพ์เล็กใหญ่ ช่องว่าง และเครื่องหมาย (`!`, `:`)
- Access Modifier ต้องตรงตามโจทย์ — ระบบตรวจเช็คถึงระดับว่า `health` เป็น `private` จริงไหม, `specie` เป็น `protected` จริงไหม
- ข้อ 5 ต้องใช้ `override` เท่านั้น ใช้ `new` แทนจะไม่ผ่าน (พฤติกรรมต่างกัน)
- แต่ละข้ออยู่คนละ namespace (`Week06.Ex01`, `Week06.Ex02`, ...) เพราะชื่อคลาสซ้ำกันหลายข้อ — เขียนโค้ดในไฟล์ของข้อนั้นได้ตามปกติ ไม่ต้องกังวล

**ขอให้สนุกกับการเขียนโค้ดครับ 👨‍💻**
