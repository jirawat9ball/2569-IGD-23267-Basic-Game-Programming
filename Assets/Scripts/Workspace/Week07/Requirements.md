# Week 07 Requirements: Object-Oriented Programming (OOP)

โจทย์สำหรับสัปดาห์ที่ 7 มุ่งเน้นไปที่แนวคิดการเขียนโปรแกรมเชิงวัตถุ (OOP) ครอบคลุมเรื่อง Inheritance, Access Modifiers, และ Virtual/Override Methods (Polymorphism) รวมถึงการประยุกต์ใช้ในเกมจริง โดย **คงคำอธิบายและทฤษฎีทั้งหมดไว้** เพื่อให้นักศึกษาอ่านทำความเข้าใจได้ครบถ้วน โดยมีโจทย์ทั้งหมด 9 ข้อ ดังนี้ (Lecture ข้อ 1–3 และ Homework ข้อ 4–9)

---

## ข้อ 1: การสืบทอดคลาส (Class Inheritance)

**ไฟล์:** `AS01_Inheritance.cs` (namespace `Week07.Ex01`)

### 📖 ความรู้เสริม: Class Inheritance
เป็นหลักการหนึ่งในการเขียนโปรแกรมแบบวัตถุ (OOP) ที่ช่วยให้เราสามารถสร้างคลาสใหม่ๆ โดยใช้คุณสมบัติของคลาสที่มีอยู่แล้วเป็นพื้นฐาน ซึ่งจะทำให้โค้ดของเรามีการซ้ำซ้อนน้อยลงและง่ายต่อการจัดการ
ตัวอย่างเช่น เรามีคลาส `Animal` ซึ่งเป็นคลาสพื้นฐานที่มีชื่อและสามารถทำให้สัตว์ต่างๆ ส่งเสียงได้ จากนั้นเราสามารถสร้างคลาส `Dog` และ `Bird` ที่ "สืบทอด" คุณสมบัติจากคลาส `Animal` ได้ นั่นหมายความว่า `Dog` และ `Bird` สามารถทำทุกอย่างที่ `Animal` ทำได้ และยังมีคุณสมบัติเฉพาะตัวเอง

**ประโยชน์:**
- ลดการซ้ำซ้อนของโค้ด (Reduce Code Duplication)
- จัดระเบียบโค้ดได้ดีขึ้น (Improved Code Organization)
- การขยายโปรแกรมได้ง่าย (Ease of Extensibility)
- การซ่อนรายละเอียดการทำงาน (Encapsulation of Behavior)
- การใช้พอลิมอร์ฟิซึม (Polymorphism)

**โจทย์:**
1. **ในคลาส `Dog` และ `Bird`:**
   - ให้แก้ไขโค้ดให้คลาส `Dog` ทำการสืบทอดจากคลาส `Animal` (`public class Dog : Animal`) หลังจากสืบทอดแล้วจะสามารถใช้งานตัวแปร `name` ได้
   - ในเมธอด `Walk` ให้เพิ่ม `name` เข้าไปในประโยคที่จะพิมพ์ออกมา: `Debug.Log($"Dog {name} is walking");`
   - ให้แก้ไขโค้ดให้คลาส `Bird` ทำการสืบทอดจากคลาส `Animal` (`public class Bird : Animal`)
   - ในเมธอด `Fly` ให้เพิ่ม `name` เข้าไปในประโยคที่จะพิมพ์ออกมา: `Debug.Log($"Bird {name} is flying");`
2. **ในคลาส `AS01_Inheritance` เมธอด `Start()`:**
   - สร้าง instance ของคลาส `Dog` และกำหนดชื่อ `name` เป็น `"Buddy"`
   - เรียกใช้เมธอด `MakeSound()` และ `Walk()` ของ `dog`
   - สร้าง instance ของคลาส `Bird` และกำหนดชื่อ `name` เป็น `"Twitty"`
   - เรียกใช้เมธอด `MakeSound()` และ `Fly()` ของ `bird`

**ตัวอย่าง output ที่ได้จากการ run program:**
```text
Animal Buddy is making sound
Dog Buddy is walking
Animal Twitty is making sound
Bird Twitty is flying
```

<details>
<summary><b>ดูเฉลยแนวทางข้อ 1 (คลิกเพื่อขยาย)</b></summary>

```csharp
using System;
using UnityEngine;

public class Animal
{
    public string name;

    public void MakeSound()
    {
        Debug.Log($"Animal {name} is making sound");
    }
}

// 1. เพิ่มให้ Dog inherit จาก Animal
public class Dog : Animal
{
    public void Walk()
    {
        Debug.Log($"Dog {name} is walking");
    }
}

// 2. เพิ่มให้ Bird inherit จาก Animal
public class Bird : Animal
{
    public void Fly()
    {
        Debug.Log($"Bird {name} is flying");
    }
}

public class AS01_Inheritance
{
    public void Start()
    {
        Dog dog = new Dog();
        dog.name = "Buddy";
        dog.MakeSound();
        dog.Walk();

        Bird bird = new Bird();
        bird.name = "Twitty";
        bird.MakeSound();
        bird.Fly();
    }
}
```
</details>

---

## ข้อ 2: Access Modifiers

**ไฟล์:** `AS02_AccessModifier.cs` (namespace `Week07.Ex02`)

### 📖 ความรู้เสริม: Access Modifiers
ในภาษา C#, ตัวแปรของคลาสสามารถมีการกำหนดระดับการเข้าถึงได้หลายระดับ เพื่อควบคุมการเข้าถึงข้อมูลจากภายนอกคลาส
- **public (สาธารณะ):** เข้าถึงได้จากทุกที่
- **protected (ถูกป้องกัน):** เข้าถึงได้จากภายในคลาสเดียวกันและคลาสที่สืบทอดมาจากคลาสนั้น แต่ไม่สามารถเข้าถึงได้จากคลาสอื่นที่ไม่มีความสัมพันธ์
- **private (ส่วนตัว):** เข้าถึงได้เฉพาะภายในคลาสนั้นเท่านั้น ไม่สามารถเข้าถึงจากคลาสอื่นๆ ได้

**โจทย์:**
กำหนดให้ตัวแปร `health` เป็นตัวแปรของ class `Animal` และมี access modifier เป็นแบบ `private` (`private int health = 10;`)

1. **ให้นักศึกษาเขียน Method `Feed` ในคลาส `Animal`** โดยมีข้อกำหนดดังนี้:
   - ชื่อ Method ว่า `Feed`, access modifier เป็น `public`, ไม่มี return type (`void`), รับ parameter 1 ตัว คือ `int food`
   - ใน function นี้รับ food มาแล้ว บวกเพิ่มค่าให้ตัวแปร health (`health += food;`)
   - พิมพ์ประโยคตาม format นี้: `Debug.Log($"{name} got {food} food");`
2. **ให้นักศึกษาเขียน method `MakeSound` ในคลาส `Animal`** โดยมีข้อกำหนดดังนี้:
   - ชื่อ Method ว่า `MakeSound`, access modifier เป็น `public`, ไม่มี return type (`void`), ไม่รับ parameter
   - ทำการ check ว่า `health` มีค่าเป็นเท่าใดด้วยเงื่อนไข:
     - ถ้า `health > 50` ให้พิมพ์ ชื่อ แล้วตามด้วย happy! (`$"{name} happy!"`)
     - ถ้า `health <= 50` ให้พิมพ์ ชื่อ แล้วตามด้วย weak! (`$"{name} weak!"`)
3. **ใน constructor ของ class `Dog`:**
   - ให้กำหนด `specie = "Dog"` (เนื่องจากเป็น protected จึงทำได้)
   - กำหนดตัวแปร `name` ของคลาส ให้เท่ากับ `name` ที่เป็น parameter (`this.name = name;`)
4. **แก้ไข code ใน function `Start()` ของ class `AS02_AccessModifier`:**
   - ให้พิมพ์ `dog.name` ออกมาในข้อความ `Debug.Log($"my name is {dog.name}");`

<details>
<summary><b>ดูเฉลยแนวทางข้อ 2 (คลิกเพื่อขยาย)</b></summary>

```csharp
using System;
using UnityEngine;

public class Animal
{
    public string name = "";
    protected string specie = "";
    private int health = 10;

    public void Feed(int food)
    {
        health += food;
        Debug.Log($"{name} got {food} food");
    }

    public void MakeSound()
    {
        if (health > 50)
            Debug.Log($"{name} happy!");
        else
            Debug.Log($"{name} weak!");
    }
}

public class Dog : Animal
{
    public Dog(string name)
    {
        specie = "Dog";
        this.name = name;
    }
}

public class AS02_AccessModifier
{
    public void Start()
    {
        Dog dog = new Dog("Buddy");

        // พิมพ์ dog.name ออกมา
        Debug.Log($"my name is {dog.name}");

        dog.MakeSound();
        dog.Feed(50);
        dog.MakeSound();
    }
}
```
</details>

---

## ข้อ 3: Virtual and Override Method

**ไฟล์:** `AS03_VirtualOverride.cs` (namespace `Week07.Ex03`)

### 📖 ความรู้เสริม: Virtual and Override Method
การใช้งาน `virtual` และ `override` เป็นเทคนิคในการเขียนโปรแกรมแบบ OOP ที่ช่วยให้เราสามารถเปลี่ยนแปลงหรือขยายฟังก์ชันของเมทอดในคลาสลูกได้โดยไม่ต้องแก้ไขโค้ดในคลาสแม่
- **คลาสแม่ (Base Class) - Animal:** ต้องทำเมทอดให้เป็น `virtual` (`public virtual void MakeSound()`)
- **คลาสลูก (Derived Class) - Dog, Cat:** ต้อง override เมทอดโดยใช้คีย์เวิร์ด `override` (`public override void MakeSound()`)

**โจทย์:**
- ในคลาส `Animal` มีเมธอด `MakeSound` ซึ่งแสดงข้อความ `"Generic animal sound"` ให้เพิ่มคำว่า `virtual` เพื่อให้คลาสลูกสามารถแก้ไขได้
- ในคลาส `Dog` ต้องการเปลี่ยนเสียงสุนัขเป็น `"Woof!"` ให้เพิ่มคำว่า `override` ในเมธอด `MakeSound`
- ในคลาส `Cat` ต้องการเปลี่ยนเสียงแมวเป็น `"Meow!"` ให้เพิ่มคำว่า `override` ในเมธอด `MakeSound`
- ใน `AS03_VirtualOverride.Start()`:
  - สร้าง `Dog` แล้วเรียก `MakeSound()`
  - สร้าง `Cat` แล้วเรียก `MakeSound()`
  - สร้าง `Animal` แล้วเรียก `MakeSound()`

<details>
<summary><b>ดูเฉลยแนวทางข้อ 3 (คลิกเพื่อขยาย)</b></summary>

```csharp
using System;
using UnityEngine;

public class Animal
{
    public virtual void MakeSound()
    {
        Debug.Log("Generic animal sound");
    }
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Debug.Log("Woof!");
    }
}

public class Cat : Animal
{
    public override void MakeSound()
    {
        Debug.Log("Meow!");
    }
}

public class AS03_VirtualOverride
{
    public void Start()
    {
        Dog dog = new Dog();
        dog.MakeSound();

        Cat cat = new Cat();
        cat.MakeSound();

        Animal someAnimal = new Animal();
        someAnimal.MakeSound();
    }
}
```
</details>

---

---

## ข้อ 4: ผู้เล่น (Player)

**ไฟล์:** `Assets/Scripts/Workspace/Week07/Game/Player.cs` (namespace `Week07.Game`)

**โจทย์:**
1. **คลาส `Player` สืบทอดมาจาก `Character`:**
   - มีฟิลด์ตำแหน่งก่อนหน้า `public int previousPositionX;`, `public int previousPositionY;`
   - มีฟิลด์สถานะกับดัก `public bool isTrapped = false;`
2. **การเดินและตรวจสอบขอบเขต:**
   - เมธอด `CanMove(Vector2 direction)` ตรวจสอบว่าตำแหน่งเป้าหมายอยู่ในขอบเขตแผนที่หรือไม่
   - เมธอด `Move(Vector2 direction)`:
     - หาก `isTrapped == true` ให้พิมพ์แจ้งเตือน ปลดสถานะกับดัก แล้ว return (เดินไม่ได้ 1 ตา)
     - บันทึก `previousPositionX`, `previousPositionY`
     - เรียก `base.Move(direction);`
3. **การย้อนตำแหน่งกลับ:**
   - เมธอด `RevertPosition()`: คืนค่าตำแหน่งไปยัง `previousPositionX`, `previousPositionY` และคืนพลังงาน `energy += 1;`

**ตัวอย่างผลลัพธ์ใน `Ex04_PlayerDemo()`:**
```text
Player position: (1, 0)
Player energy: 99
```

---

## ข้อ 5: ระบบต่อสู้กับศัตรู (Enemy)

**ไฟล์:** `Assets/Scripts/Workspace/Week07/Game/Enemy.cs` และ `Character.cs` (namespace `Week07.Game`)

**โจทย์:**
1. **File `Enemy.cs`**
   - เขียน class `Enemy` โดยให้ Inherit มาจาก class `Character`
   - เขียน `override method Hit()` โดยภายใน method ให้ตรวจสอบก่อนว่า `energy` หมดหรือยัง ถ้า `<= 0` ให้ `return;`
   - สั่งให้ enemy attack player: `this.Attack(mapGenerator.player, attackPoint);`
2. **File `Character.cs`**
   - เพิ่มการ check เงื่อนไขใน scope ภายใต้ `if (HasSomeObject(toX, toY))` ว่าตำแหน่งมี Enemy หรือไม่ โดยใช้ `IsEnemy(toX, toY)`
   - สั่งให้ character (player) โจมตี enemy ในตำแหน่งนั้น: `Enemy e = mapGenerator.enemies[toX, toY]; this.Attack(e, attackPoint);`
   - ตรวจสอบว่าศัตรูตายหรือยัง ถ้า `enemy.energy > 0` ให้สั่งเรียก method `Hit()` ของศัตรู (เพื่อให้ศัตรูตีสวน)
   - ถ้าศัตรูตายแล้ว (`else`) ให้เดินขยับเข้าไปที่ช่องนั้น (กำหนดค่า `positionX`, `positionY` และ `transform.position`)

**คำอธิบายและตัวอย่าง Output ใน `Ex05_BattleDemo()`:**
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

<details>
<summary><b>ดูเฉลยแนวทางข้อ 5 (คลิกเพื่อขยาย)</b></summary>

```csharp
// ในส่วนของ IsEnemy(...)
else if (IsEnemy(toX, toY))
{
    Enemy enemy = mapGenerator.enemies[toX, toY];
    this.Attack(enemy, attackPoint);
    
    if (enemy.energy > 0)
    {
        enemy.Hit();
    }
    else
    {
        positionX = toX;
        positionY = toY;
        transform.position = new Vector3(positionX, positionY, 0);
    }
}
```
</details>

---

## ข้อ 6: ระบบเก็บยาเพิ่มพลัง (ItemPotion)

**ไฟล์:** `Assets/Scripts/Workspace/Week07/Game/ItemPotion.cs` และ `Character.cs` (namespace `Week07.Game`)

**โจทย์:**
1. **File `ItemPotion.cs`**
   - เขียน class `ItemPotion` โดยให้ Inherit มาจาก class `Identity`
   - สร้าง class variable ชื่อ `healPoint` (type `int`, ค่าเริ่มต้น 10, modifier `public`)
   - เขียน `override method Hit()` พิมพ์ข้อความ `Debug.Log($"You got {Name} : {healPoint}");`
   - สั่งเพิ่มเลือด: `mapGenerator.player.Heal(healPoint);`
   - เอาไอเทมออกจาก map: `mapGenerator.mapData[positionX, positionY] = 0; DestroySafe(gameObject);`
2. **File `Character.cs`**
   - เพิ่มเช็ค `IsPotion(toX, toY)` ใน `Move(...)`
   - ถ้าเจอ Potion ให้เรียก `Hit()`: `mapGenerator.potions[toX, toY].Hit();`
   - เลื่อนตำแหน่งผู้เล่นเข้าไปที่ช่องนั้น

<details>
<summary><b>ดูเฉลยแนวทางข้อ 6 (คลิกเพื่อขยาย)</b></summary>

```csharp
// ภายใต้ if (HasSomeObject(toX, toY))
if (IsPotion(toX, toY))
{
    mapGenerator.potions[toX, toY].Hit();
    positionX = toX;
    positionY = toY;
    transform.position = new Vector3(positionX, positionY, 0);
}
```
</details>

---

## ข้อ 7: ระบบเก็บดาบเพิ่มพลังโจมตี (ItemSword)

**ไฟล์:** `Assets/Scripts/Workspace/Week07/Game/ItemSword.cs` และ `Character.cs` (namespace `Week07.Game`)

**โจทย์:**
1. **File `ItemSword.cs`**
   - เขียน class `ItemSword` โดยให้ Inherit มาจาก class `Identity`
   - สร้าง class variable ชื่อ `attackBonus` (type `int`, ค่าเริ่มต้น 10, modifier `public`)
   - เขียน `override method Hit()` พิมพ์ข้อความ `Debug.Log($"You got {Name} : {attackBonus}");`
   - สั่งเพิ่มพลังโจมตี: `mapGenerator.player.IncreaseAttack(attackBonus);`
   - เอาไอเทมออกจาก map แบบเดียวกับ Potion
2. **File `Character.cs`**
   - เพิ่มเช็ค `IsSword(toX, toY)` ใน `Move(...)`
   - ถ้าเจอ Sword ให้เรียก `Hit()`: `mapGenerator.swords[toX, toY].Hit();`
   - เลื่อนตำแหน่งผู้เล่นเข้าไปที่ช่องนั้น

<details>
<summary><b>ดูเฉลยแนวทางข้อ 7 (คลิกเพื่อขยาย)</b></summary>

```csharp
// ภายใต้ if (HasSomeObject(toX, toY))
else if (IsSword(toX, toY))
{
    mapGenerator.swords[toX, toY].Hit();
    positionX = toX;
    positionY = toY;
    transform.position = new Vector3(positionX, positionY, 0);
}
```
</details>

---

## ข้อ 8: กำแพงพังได้ (Wall)

**ไฟล์:** `Assets/Scripts/Workspace/Week07/Game/Wall.cs` และ `Character.cs` (namespace `Week07.Game`)

**โจทย์:**
1. **File `Wall.cs`**
   - เขียน class `Wall` โดยให้ Inherit มาจาก class `Identity`
   - สร้าง class variable ชื่อ `durability` (type `int`, ค่าเริ่มต้น 3, modifier `public`)
   - เขียน `override method Hit()`:
     - ลดค่าความทนทาน `durability--;`
     - พิมพ์ `Debug.Log($"Hit Wall {Name}! Remaining durability: {durability}");`
     - ถ้า `durability <= 0` พิมพ์ `Debug.Log($"Wall {Name} destroyed!");` เคลียร์ช่องแผนที่ `mapGenerator.mapData[positionX, positionY] = 0;` และสั่ง `DestroySafe(gameObject);`
2. **File `Character.cs`**
   - ภายใต้ `if (HasSomeObject(toX, toY))` เมื่อเจอ `IsDemonWall(toX, toY)` ให้เรียก `mapGenerator.walls[toX, toY].Hit();` โดยผู้เล่นจะไม่เดินข้ามกำแพง

**ตัวอย่างผลลัพธ์ใน `Ex08_WallDemo()` (ตีกำแพง 3 ครั้ง):**
```text
Hit Wall Wall1! Remaining durability: 2
Hit Wall Wall1! Remaining durability: 1
Hit Wall Wall1! Remaining durability: 0
Wall Wall1 destroyed!
```

---

## ข้อ 9: กล่องสมบัติสร้างวัตถุใหม่ (Chest)

**ไฟล์:** `Assets/Scripts/Workspace/Week07/Game/Chest.cs` และ `Character.cs` (namespace `Week07.Game`)

**โจทย์:**
1. **File `Chest.cs`**
   - เขียน class `Chest` โดยให้ Inherit มาจาก class `Identity`
   - สร้าง class variables: `public GameObject spawnPrefab;` และ `public bool isOpen = false;`
   - เขียนเมธอด `OpenChest()`:
     - ถ้า `isOpen` แล้ว ให้ return
     - ตั้งค่า `isOpen = true;`
     - พิมพ์ `Debug.Log($"Opened {Name}!");`
     - ถ้า `spawnPrefab != null` ให้สร้างวัตถุใหม่ที่ด้านบน 1 ช่อง (`transform.position + Vector3.up`)
     - เคลียร์ช่องแผนที่ `mapGenerator.mapData[positionX, positionY] = 0;` และสั่ง `DestroySafe(gameObject);`
   - เขียน `override method Hit()` ให้เรียก `OpenChest();`
2. **File `Character.cs`**
   - ภายใต้ `if (HasSomeObject(toX, toY))` เมื่อเจอ `IsChest(toX, toY)` ให้เรียก `mapGenerator.chests[toX, toY].Hit();` และให้ผู้เล่นเดินเข้าไปที่ช่องนั้น

**ตัวอย่างผลลัพธ์ใน `Ex09_ChestDemo()`:**
```text
Opened Chest1!
```
