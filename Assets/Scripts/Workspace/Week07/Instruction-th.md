# Assignment Week 07: การเขียนโปรแกรมเชิงวัตถุ (OOP)

## 📋 ภาพรวมของ Assignment

สัปดาห์นี้เราจะเรียน **OOP (Object-Oriented Programming)** — แนวคิดที่นำการจัดโครงสร้างแบบวัตถุมาประยุกต์ใช้เพื่อลดความซ้ำซ้อนของโค้ดและเพิ่มความยืดหยุ่นในการพัฒนาเกม

- **การสืบทอด (Inheritance):** ถ่ายทอดคุณสมบัติและความสามารถจากคลาสแม่สู่คลาสลูก
- **การควบคุมการเข้าถึง (Access Modifiers):** กำหนดขอบเขตการเข้าถึงตัวแปรด้วย `public`, `protected`, `private`
- **พอลิมอร์ฟิซึม (Polymorphism):** ให้คลาสลูกปรับเปลี่ยนพฤติกรรมจากคลาสแม่ด้วย `virtual` และ `override`

มีแบบฝึกหัดทั้งหมด **6 ข้อ**

- **ข้อ 1–3** ฝึก OOP พื้นฐานทีละเรื่อง (Inheritance, Access Modifiers, Virtual & Override)
- **ข้อ 4–6** นำ OOP ไปใช้จริงในเกมเดินแผนที่ (ตีศัตรู / เก็บยา / เก็บดาบ)

## 🎯 จุดประสงค์การเรียนรู้

- ใช้การสืบทอด (Inheritance) เพื่อลดความซ้ำซ้อนของโค้ด
- เข้าใจและเลือกใช้ Access Modifier (`public` / `protected` / `private`) ได้อย่างถูกต้องตามหลัก Encapsulation
- ใช้ `virtual` และ `override` เพื่อเปิดให้คลาสลูกเขียนทับพฤติกรรมของคลาสแม่ (Polymorphism)
- เชื่อมโยงลำดับชั้นคลาส (Class Hierarchy) ในระบบเกม Unity

## 📚 รายการไฟล์ที่นักเรียนต้องจัดการ

ในสัปดาห์นี้ โครงสร้างไฟล์สำหรับ Week 07 มีดังนี้:

| ข้อ | ไฟล์ | ตำแหน่งโฟลเดอร์ | สิ่งที่ต้องทำ |
|---|---|---|---|
| 1 | `AS01_Inheritance.cs` | `Assets/.../Week07/` | เขียนคลาส `Animal`, `Dog`, `Bird` สืบทอดกัน |
| 2 | `AS02_AccessModifier.cs` | `Assets/.../Week07/` | จัดการ Access Modifiers (`Feed`, `MakeSound`, `protected specie`, `private health`) |
| 3 | `AS03_VirtualOverride.cs` | `Assets/.../Week07/` | ใส่ `virtual` กับ `override` ในคลาส `Animal`, `Dog`, `Cat` |
| 4 | `Game/Enemy.cs` + `Game/Character.cs` | `Week07/Game/` | เขียนคลาส `Enemy` และส่วนตีศัตรูใน `Move()` |
| 5 | `Game/ItemPotion.cs` + `Game/Character.cs` | `Week07/Game/` | เขียนคลาส `ItemPotion` และส่วนเก็บยาใน `Move()` |
| 6 | `Game/ItemSword.cs` + `Game/Character.cs` | `Week07/Game/` | เขียนคลาส `ItemSword` และส่วนเก็บดาบใน `Move()` |
| ทุกข้อ | `Assignment_Student_Week07.cs` | `Week07/` | สคริปต์หลักสำหรับรัน Demo และส่งตรวจงาน |

> **ไฟล์โครงสร้างเกมที่เตรียมไว้ให้:** `Game/Identity.cs`, `Game/MapGenerator.cs`, `Game/Player.cs`, `Game/Wall.cs`, `Game/Exit.cs` — เป็นโครงสร้างเกม 2D Grid

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

| หัวข้อ | Pure C# Class (แบบข้อ 1–3 ในใบงาน) | Unity MonoBehaviour (แบบในเกมข้อ 4–6) |
|---|---|---|
| **การสร้างอ็อบเจกต์** | ใช้คำสั่ง `new MyClass()` | **ห้ามใช้ `new` เด็ดขาด!** ต้องใช้ `AddComponent<T>()` หรือแปะบน Prefab/GameObject |
| **การตั้งค่าเริ่มต้น** | ใช้ Constructor (`public MyClass(...)`) | ใช้ Unity Lifecycle (`Awake()`, `Start()`) หรือสร้างฟังก์ชัน `Initialize(...)` |
| **ตำแหน่งในระบบ** | อยู่ใน RAM ของโปรแกรม | ต้องเป็น **Component** แปะอยู่กับ **GameObject** ใน Scene เสมอ |
| **การปรับค่าข้อมูล** | แก้ไขผ่านโค้ดเท่านั้น | ใช้ `[SerializeField]` หรือ `public` ให้ปรับแต่งค่าผ่าน **Inspector** ได้ |

> 📌 **สรุปวิธีคิด Unity:**
> - **GameObject** เปรียบเสมือนตัวหุ่นเปล่า ๆ ในเกม (เช่น ผู้เล่น, ศัตรู, กล่อง)
> - **MonoBehaviour Script** คือ "ชิ้นส่วน/ความสามารถ" (Component) ที่เราสร้างขึ้น แล้วนำไปแปะใส่ตัวหุ่น
> - สังเกตในข้อ 4–6 โค้ดของเกมจะสืบทอด `MonoBehaviour -> Identity -> Character -> Enemy` ซึ่งเป็นโครงสร้าง Component-Based ร่วมกับ OOP แท้จริงของ Unity

---

## ข้อ 1. การสืบทอดคลาส (Inheritance)

### 📖 ความรู้เสริม

การสืบทอดช่วยให้สร้างคลาสใหม่โดยใช้คลาสเดิมเป็นฐาน ทำให้โค้ดซ้ำน้อยลงและจัดการง่ายขึ้น

เช่น มีคลาส `Animal` ที่มีชื่อและส่งเสียงได้ แล้วให้ `Dog` กับ `Bird` สืบทอดไป — ทั้งคู่จะทำทุกอย่างที่ `Animal` ทำได้ **บวกกับ** ความสามารถเฉพาะตัว

**ประโยชน์:** ลดโค้ดซ้ำ · จัดระเบียบโค้ดดีขึ้น · ขยายโปรแกรมง่าย · เตรียมทางไปสู่ Polymorphism

**ไฟล์:** `AS01_Inheritance.cs` (namespace `Week07.Ex01`)

**Logic ที่ต้อง implement:**
- ในไฟล์ `AS01_Inheritance.cs` มีคลาส `Animal`, `Dog`, และ `Bird`
- ทำให้ `Dog` สืบทอดจาก `Animal` โดยเขียน `: Animal` ต่อท้ายชื่อคลาส → พอสืบทอดแล้วจะใช้ตัวแปร `name` ได้เลย
- ในเมธอด `Walk()` พิมพ์ `Dog <ชื่อ> is walking`
- ทำให้ `Bird` สืบทอดจาก `Animal` เหมือนกัน
- ในเมธอด `Fly()` พิมพ์ `Bird <ชื่อ> is flying`

ใน `Ex01_InheritanceDemo()` (ใน `Assignment_Student_Week07.cs`):
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

## ข้อ 2. Access Modifiers

### 📖 ความรู้เสริม

ตัวแปรในคลาสกำหนดระดับการเข้าถึงได้ เพื่อคุมว่าใครแก้ข้อมูลได้บ้าง

- **public** — เข้าถึงได้จากทุกที่
- **protected** — เข้าถึงได้จากในคลาสเดียวกันและคลาสที่สืบทอดไป แต่คลาสอื่นที่ไม่เกี่ยวข้องเข้าไม่ได้
- **private** — เข้าถึงได้เฉพาะในคลาสนั้นเท่านั้น

**ไฟล์:** `AS02_AccessModifier.cs` (namespace `Week07.Ex02`)

ในคลาส `Animal` กำหนดฟิลด์:
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
4. ใน `Ex02_AccessModifierDemo()`
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

## ข้อ 3. Virtual และ Override

### 📖 ความรู้เสริม

`virtual` และ `override` ทำให้คลาสลูกเปลี่ยนพฤติกรรมของคลาสแม่ได้ โดยไม่ต้องไปแก้โค้ดคลาสแม่

- **คลาสแม่** เขียน `public virtual void MakeSound()` — เปิดให้ลูกเขียนทับได้
- **คลาสลูก** เขียน `public override void MakeSound()` — เขียนทับของแม่

**ไฟล์:** `AS03_VirtualOverride.cs` (namespace `Week07.Ex03`)

**Logic ที่ต้อง implement:**
- ในคลาส `Animal` เติมคำว่า `virtual` หน้าเมธอด `MakeSound` (พิมพ์ `Generic animal sound`)
- ในคลาส `Dog` สืบทอดจาก `Animal` เติมคำว่า `override` หน้าเมธอด `MakeSound` และเปลี่ยนข้อความเป็น `Woof!`
- ในคลาส `Cat` สืบทอดจาก `Animal` เติมคำว่า `override` หน้าเมธอด `MakeSound` และเปลี่ยนข้อความเป็น `Meow!`

ใน `Ex03_VirtualOverrideDemo()`:
- สร้าง `Dog` เรียก `MakeSound()`
- สร้าง `Cat` เรียก `MakeSound()`
- สร้าง `Animal` เรียก `MakeSound()`

**ผลลัพธ์ที่ต้องได้:**
```text
Woof!
Meow!
Generic animal sound
```

> **ลองสังเกต:** ถ้าเขียน `Animal a = new Dog(); a.MakeSound();` จะได้ `Woof!` — เพราะ `override` ทำให้เรียกเมธอดของคลาสลูกเสมอ นี่คือ **Polymorphism**

---

# เกมเดินแผนที่ (ข้อ 4–6)

สามข้อนี้เอา OOP ที่เรียนมาไปใช้จริงในเกมเดินตารางบนแผนที่ ผู้เล่นเดินบนแผนที่ เก็บของ และต่อสู้กับศัตรู

**โครงคลาสของเกม**
```
MonoBehaviour
└── Identity                 ← ของทุกอย่างบนแผนที่ (Name, positionX/Y, mapGenerator, Hit())
    ├── Character            ← มี energy, attackPoint, Move(), Attack(), TakeDamage(), Heal()
    │   ├── Player
    │   └── Enemy            ← ข้อ 4
    ├── ItemPotion           ← ข้อ 5
    └── ItemSword            ← ข้อ 6
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

## ข้อ 4. คลาส Enemy และระบบต่อสู้

**วัตถุประสงค์:** ใช้การสืบทอดและ `override` สร้างศัตรูที่ตีสวนกลับได้ (สืบทอดโครงสร้าง MonoBehaviour ผ่าน `Character`)

**ไฟล์:** `Assets/Scripts/Workspace/Week07/Game/Enemy.cs` และ `Character.cs`

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

## ข้อ 5. คลาส ItemPotion (ยาเพิ่มพลัง)

**วัตถุประสงค์:** สร้างไอเทมที่สืบทอดจาก `Identity` (MonoBehaviour) แล้ว `override Hit()` ให้ทำงานตอนถูกเดินชน

**ไฟล์:** `Assets/Scripts/Workspace/Week07/Game/ItemPotion.cs`

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

## ข้อ 6. คลาส ItemSword (ดาบเพิ่มพลังโจมตี)

**วัตถุประสงค์:** ทำแบบเดียวกับยา แต่เปลี่ยนเป็นเพิ่มพลังโจมตี

**ไฟล์:** `Assets/Scripts/Workspace/Week07/Game/ItemSword.cs`

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
- ข้อ 3 ต้องใช้ `override` เท่านั้น ใช้ `new` แทนจะไม่ผ่าน (พฤติกรรมต่างกัน)
- แต่ละข้ออยู่คนละ namespace (`Week07.Ex01`, `Week07.Ex02`, `Week07.Ex03`) เพราะชื่อคลาสซ้ำกันหลายข้อ — เขียนโค้ดในไฟล์ของข้อนั้นได้ตามปกติ ไม่ต้องกังวล

**ขอให้สนุกกับการเขียนโค้ดครับ 👨‍💻**
