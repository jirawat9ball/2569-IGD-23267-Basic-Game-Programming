# Week 06 Requirements: Object-Oriented Programming (OOP)

โจทย์สำหรับสัปดาห์ที่ 6 มุ่งเน้นไปที่แนวคิดการเขียนโปรแกรมเชิงวัตถุ (OOP) ครอบคลุมเรื่อง Class, Constructor, Inheritance, Access Modifiers, และ Virtual/Override Methods โดย **คงคำอธิบายและทฤษฎีทั้งหมดไว้** เพื่อให้นักศึกษาอ่านทำความเข้าใจได้ครบถ้วน โดยมีโจทย์ทั้งหมด 8 ข้อ ดังนี้

---

## ข้อ 1: การสร้างคลาสเบื้องต้น (Class Declaration)

**โจทย์:**
ให้สร้างคลาสที่ชื่อว่า `Car` ซึ่งแทนรถยนต์ โดยคลาสนี้ต้องประกอบไปด้วยคุณสมบัติ (Properties) และพฤติกรรม (Methods) ที่รถยนต์สามารถมีได้ และสามารถแสดงการทำงานได้อย่างถูกต้องในโปรแกรม

**ขั้นตอนการทำใบงาน**
1. **สร้างคลาส Car:** กำหนดชื่อคลาสว่า `Car` และให้มีการเข้าถึงแบบ public
2. **กำหนดฟิลด์ (Fields) สำหรับคุณสมบัติของรถยนต์:**
   - `name`: ชื่อของรถ (ชนิดข้อมูล `string`)
   - `color`: สีของรถ (ชนิดข้อมูล `string`)
   - `speed`: ความเร็วของรถ (ชนิดข้อมูล `float`)
3. **สร้างพฤติกรรม (Methods) สำหรับรถยนต์:**
   - `Move()`: แสดงข้อความว่า `"Car is moving"`
   - `Turn()`: แสดงข้อความว่า `"Car is turning"`
   - `Honk()`: แสดงข้อความว่า `"Car is honking"`
4. **ทดสอบการสร้างวัตถุ (Object) จากคลาส Car:**
   - ใน method `Start()` ใน file `Assignment.cs`
   - สร้างรถยนต์จากคลาส `Car` แล้วกำหนดค่า `name`, `color`, และ `speed` ด้วยตนเอง
   - เรียกใช้เมธอด `Move()`, `Turn()`, และ `Honk()` ของวัตถุที่สร้างขึ้นมา

**ตัวอย่างผลลัพธ์ที่คาดหวัง:**
เมื่อ program เริ่ม object car จะถูกสร้างขึ้นตามที่กำหนดไว้ใน code Assignment.cs และเมื่อคำสั่งเรียกใช้ method `Move()`, `Turn()`, `Honk()` ทำงานมันจะพิมพ์ออกมา 3 ประโยค
```text
Car is moving
Car is turning
Car is honking
```

### 📖 ความรู้เสริม: แนวคิดของการเขียนโปรแกรมเชิงวัตถุ (OOP)
คือการทำให้วัตถุ (Object) ในโลกความจริง เช่น รถยนต์ (Car) มีคุณสมบัติและพฤติกรรมเหมือนจริงในโลกของคอมพิวเตอร์ เราสามารถกำหนดว่ารถยนต์จะมีลักษณะ (properties) และการทำงาน (behaviors) อย่างไรได้บ้าง
ในโค้ดที่เราเห็นนี้ มีการสร้าง "คลาส" ที่ชื่อว่า `Car` คลาสนี้เป็นเหมือนพิมพ์เขียวที่บอกว่ารถยนต์ควรมีอะไรบ้าง ซึ่งในที่นี้จะประกอบไปด้วย:
- **คุณสมบัติของรถยนต์ (Properties):** `name` (เช่น "Toyota"), `color` (เช่น "แดง"), `speed` (เช่น "80 กม./ชม.")
- **พฤติกรรมของรถยนต์ (Behaviors):** `Move()` (วิ่ง), `Turn()` (เลี้ยว), `Honk()` (บีบแตร)

**การสร้างคลาส (Class Declaration):** คลาสคือพิมพ์เขียวที่กำหนดวัตถุจะคุณสมบัติและพฤติกรรมอะไรบ้าง เมื่อเราสร้างคลาสแล้ว เราสามารถสร้างวัตถุที่เป็น "รถยนต์" ขึ้นมาและให้มันทำงานได้ตามที่เรากำหนด ตัวอย่างเช่น:
```csharp
Car myCar = new Car();
myCar.name = "Toyota";
myCar.color = "แดง";
myCar.speed = 120;

myCar.Move(); // ขับเคลื่อนรถ
myCar.Turn(); // เลี้ยวรถ
myCar.Honk(); // บีบแตร
```

### 📖 ความรู้เสริม: How to make class
ส่วนประกอบอะไรบ้างในคลาส `Car` ที่เราได้เขียนไป ซึ่งส่วนประกอบเหล่านี้คือสิ่งที่สำคัญในการสร้างคลาสใน C#:
1. **Access Modifier (ตัวระบุการเข้าถึง):** บอกว่าเราสามารถเข้าถึงข้อมูลหรือเมธอดในคลาสนี้ได้จากที่ไหนบ้าง
   - `public`: ข้อมูลหรือเมธอดที่ใช้คำนี้สามารถเข้าถึงได้จากทุกที่
   - `private`: ข้อมูลหรือเมธอดที่ใช้คำนี้สามารถเข้าถึงได้เฉพาะภายในคลาสนี้เท่านั้น
2. **Class Name (ชื่อคลาส):** ชื่อคลาสเป็นชื่อที่เราใช้เรียกเพื่อบอกว่าเรากำลังสร้างอะไร เช่น `public class Car { ... }`
3. **Fields (ฟิลด์ หรือ ตัวแปรในคลาส):** ฟิลด์คือตัวแปรที่ใช้เก็บข้อมูลต่าง ๆ ที่เกี่ยวกับคลาสนั้น ๆ เช่น `public string name;`
4. **Methods (เมธอด หรือ ฟังก์ชัน):** การทำงานหรือพฤติกรรมที่เรากำหนดให้คลาสทำงานได้ เช่น `public void Move() { ... }`
5. **Constructor (คอนสตรัคเตอร์):** คอนสตรัคเตอร์คือเมธอดพิเศษที่ใช้ในการสร้างวัตถุใหม่จากคลาส และใช้กำหนดค่าฟิลด์ตั้งต้นเมื่อสร้างวัตถุขึ้นมา
6. **Properties (คุณสมบัติ):** ช่วยให้เราสามารถควบคุมการเข้าถึงฟิลด์ได้ (ใช้ get/set)

<details>
<summary><b>ดูเฉลยแนวทางข้อ 1 (คลิกเพื่อขยาย)</b></summary>

```csharp
using System;
using UnityEngine;

public class Car
{
    // properties including name, color, speed ...
    public string name;
    public string color;
    public float speed;
    // end of properties ...

    // behaviors: Move(), Turn(), Honk() ...
    public void Move(){
        Debug.Log("Car is moving");
    }
    public void Turn(){
        Debug.Log("Car is turning");
    }
    public void Honk(){
        Debug.Log("Car is honking");
    }
    // end of behaviors ...
}

public class Assignment
{
    public void Start()
    {
        Car honda = new Car();
        honda.name = "civic";
        honda.color = "black";
        honda.speed = 110;
        
        // Student code start HERE ...
        honda.Move();
        honda.Turn();
        honda.Honk();
        // Student code ends HERE 
    }
}
```
</details>

---

## ข้อ 2: Constructor

**การสร้าง Constructor ในคลาสของภาษา C#**
Constructor เป็นเมธอดพิเศษที่จะถูกเรียกใช้งานอัตโนมัติเมื่อมีการสร้างอินสแตนซ์ของคลาสนั้นๆ ซึ่งในกรณีนี้คือคลาส `Dog`

**โจทย์:**
1. ใน file `Dog.cs` สร้าง Constructor สำหรับคลาส `Dog`:
   - Constructor นี้ต้องรับพารามิเตอร์ 3 ตัว ได้แก่ `name`, `breed`, และ `age`
   - ใน Constructor, ให้กำหนดค่าของพารามิเตอร์เหล่านี้ให้กับ properties ของคลาส `Dog` (ใช้คำสั่ง `this` เพื่ออ้างอิงถึง instance ปัจจุบันของคลาส)
2. ใน file `Assignment.cs` ให้สร้าง object ใหม่ด้วย constructor ในขั้นตอนที่ 1 และกำหนด object นั้น ให้ตัวแปร `dog1` โดยกำหนดให้ `name` จะต้องมีค่าเป็น `"Buddy"`

**ตัวอย่างโค้ด (การใช้ this):**
```csharp
public Dog(string name, string breed, int age)
{
    this.name = name;
    this.breed = breed;
    this.age = age;
}
```
*หมายเหตุ:* อย่าลืมว่า Constructor ไม่มีการรีเทิร์นค่าใดๆ เราจะใช้ constructor เป็นที่กำหนดค่าเริ่มต้นของตัวแปรใน class เมื่อ object ใหม่ถูกสร้างขึ้นมา

**ตัวอย่าง output จากการ run program:**
```text
Buddy is barking
Buddy is wagging tail
Buddy stopped barking
```

<details>
<summary><b>ดูเฉลยแนวทางข้อ 2 (คลิกเพื่อขยาย)</b></summary>

```csharp
using System;
using UnityEngine;

public class Dog
{
    public string name;
    public string breed;
    public int age;

    // Student code starts HERE ...
    public Dog(string name, string breed, int age)
    {
        this.name = name;
        this.breed = breed;
        this.age = age;
    }
    // Student code ends HERE ...

    public void Bark()
    {
        Debug.Log($"{name} is barking");
    }
    public void WagTail()
    {
        Debug.Log($"{name} is wagging tail");
    }
    public void StopBarking()
    {
        Debug.Log($"{name} stopped barking");
    }
}

public class Assignment
{
    Dog dog1;

    public void Start()
    {
        // Student code starts HERE ...
        dog1 = new Dog("Buddy", "Golden Retriever", 3);
        // Student code ends HERE ...

        dog1.Bark();
        dog1.WagTail();
        dog1.StopBarking();
    }
}
```
</details>

---

## ข้อ 3: การสืบทอดคลาส (Class Inheritance)

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
1. **ใน File `Animal.cs`**
   - ให้แก้ไข code ให้ class `Dog` มีการ inherit จาก class `Animal` (หลังจาก inherit แล้วจะสามารถเรียกใช้งานตัวแปร `name` ได้)
   - ใน method `Walk` ให้เพิ่ม `name` เข้าไปในประโยคที่จะพิมพ์ออกมา: `Debug.Log($"Dog {name} is walking");`
   - ให้แก้ไข code ให้ class `Bird` มีการ inherit จาก class `Animal`
   - ใน method `Fly` ให้เพิ่ม `name` เข้าไปในประโยคที่จะพิมพ์ออกมา: `Debug.Log($"Bird {name} is flying");`
2. **ใน File `Assignment.cs`**
   - กำหนดชื่อ (name) ของ dog เป็น `"Buddy"`
   - เรียกใช้ method `MakeSound()` ของ dog ที่ได้มาจากการ inherit class Animal
   - กำหนดชื่อ (name) ของ bird เป็น `"Twitty"`
   - เรียกใช้ method `MakeSound()` ของ bird

**ตัวอย่าง output ที่ได้จากการ run program:**
```text
Animal Buddy is making sound
Dog Buddy is walking
Animal Twitty is making sound
Bird Twitty is flying
```

<details>
<summary><b>ดูเฉลยแนวทางข้อ 3 (คลิกเพื่อขยาย)</b></summary>

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

public class Assignment
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

## ข้อ 4: Access Modifiers

### 📖 ความรู้เสริม: Access Modifiers
ในภาษา C#, ตัวแปรของคลาสสามารถมีการกำหนดระดับการเข้าถึงได้หลายระดับ เพื่อควบคุมการเข้าถึงข้อมูลจากภายนอกคลาส
- **public (สาธารณะ):** เข้าถึงได้จากทุกที่
- **protected (ถูกป้องกัน):** เข้าถึงได้จากภายในคลาสเดียวกันและคลาสที่สืบทอดมาจากคลาสนั้น แต่ไม่สามารถเข้าถึงได้จากคลาสอื่นที่ไม่มีความสัมพันธ์
- **private (ส่วนตัว):** เข้าถึงได้เฉพาะภายในคลาสนั้นเท่านั้น ไม่สามารถเข้าถึงจากคลาสอื่นๆ ได้

**โจทย์:**
กำหนดให้ตัวแปร `health` เป็นตัวแปรของ class `Animal` และมี access modifier เป็นแบบ `private` (`private int health = 10;`)

1. **ให้นักศึกษาเขียน Method `Feed`** โดยมีข้อกำหนดดังนี้:
   - ชื่อ Method ว่า `Feed`, access modifier เป็น `public`, ไม่มี return type (`void`), รับ parameter 1 ตัว คือ `int food`
   - ใน function นี้รับ food มาแล้ว บวกเพิ่มค่าให้ตัวแปร health (`health += food;`)
   - พิมพ์ประโยคตาม format นี้: `Debug.Log($"{name} got {food} food");`
2. **ให้นักศึกษาเขียน method `MakeSound`** โดยมีข้อกำหนดดังนี้:
   - ชื่อ Method ว่า `MakeSound`, access modifier เป็น `public`, ไม่มี return type (`void`), ไม่รับ parameter
   - ทำการ check ว่า `health` มีค่าเป็นเท่าใดด้วยเงื่อนไข:
     - ถ้า `health > 50` ให้พิมพ์ ชื่อ แล้วตามด้วย happy! (`$"{name} happy!"`)
     - ถ้า `health <= 50` ให้พิมพ์ ชื่อ แล้วตามด้วย weak! (`$"{name} weak!"`)
3. **ใน constructor ของ class `Dog`**
   - ให้กำหนด `specie = "Dog"` (เนื่องจากเป็น protected จึงทำได้)
   - กำหนดตัวแปร `name` ของคลาส ให้เท่ากับ `name` ที่เป็น parameter (`this.name = name;`)
4. **แก้ไข code ใน function `Start()` ของ class `Assignment`**
   - ให้พิมพ์ `dog.name` ออกมาในข้อความ `Debug.Log($"my name is {dog.name}");`

<details>
<summary><b>ดูเฉลยแนวทางข้อ 4 (คลิกเพื่อขยาย)</b></summary>

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

public class Assignment
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

## ข้อ 5: Virtual and Override Method

### 📖 ความรู้เสริม: Virtual and Override Method
การใช้งาน `virtual` และ `override` เป็นเทคนิคในการเขียนโปรแกรมแบบ OOP ที่ช่วยให้เราสามารถเปลี่ยนแปลงหรือขยายฟังก์ชันของเมทอดในคลาสลูกได้โดยไม่ต้องแก้ไขโค้ดในคลาสแม่
- **คลาสแม่ (Base Class) - Animal:** ต้องทำเมทอดให้เป็น `virtual` (`public virtual void MakeSound()`)
- **คลาสลูก (Derived Class) - Dog:** ต้อง override เมทอดโดยใช้คีย์เวิร์ด `override` (`public override void MakeSound()`)

**โจทย์:**
- ในคลาส `Animal` มีเมธอด `MakeSound` ซึ่งแสดงข้อความ `"Generic animal sound"` ให้เพิ่มคำว่า `virtual` เพื่อให้คลาสลูกสามารถแก้ไขได้
- ในคลาส `Dog` ต้องการเปลี่ยนเสียงสุนัขเป็น `"Woof!"` ให้เพิ่มคำว่า `override` ในเมธอด `MakeSound`

<details>
<summary><b>ดูเฉลยแนวทางข้อ 5 (คลิกเพื่อขยาย)</b></summary>

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

public class Assignment
{
    public void Start()
    {
        Dog dog = new Dog();
        dog.MakeSound();

        Animal someAnimal = new Animal();
        someAnimal.MakeSound();
    }
}
```
</details>

---

## ข้อ 6: ระบบต่อสู้กับศัตรู (File Enemy.cs และ Character.cs)

**โจทย์:**
1. **File `Enemy.cs`**
   - เขียน class `Enemy` โดยให้ Inherit มาจาก class `Character`
   - เขียน `override method Hit()` โดยภายใน method ให้ตรวจสอบก่อนว่า `energy` หมดหรือยัง ถ้า `<= 0` ให้ `return;`
   - สั่งให้ enemy attack player: `this.Attack(mapGenerator.player, attackPoint);`
2. **File `Character.cs`**
   - เพิ่มการ check เงื่อนไขใน scope ภายใต้ `if (HasSomeObject(toX, toY))` ว่าตำแหน่งมี Enemy หรือไม่ โดยใช้ `IsEnemy(toX, toY)`
   - ถ้ามี ให้ call method `Hit()` ของศัตรู: `mapGenerator.enemies[toX, toY].Hit();`
   - สั่งให้ character (player) โจมตี enemy ในตำแหน่งนั้น: `Enemy e = mapGenerator.enemies[toX, toY]; this.Attack(e, attackPoint);`
   - ตรวจสอบว่าศัตรูตายหรือยัง ถ้า `enemy.energy > 0` ให้สั่งเรียก method `Hit()` ของศัตรู (เพื่อให้ศัตรูตีสวน)
   - ถ้าศัตรูตายแล้ว (`else`) ให้เดินขยับเข้าไปที่ช่องนั้น (กำหนดค่า `positionX`, `positionY` และ `transform.position`)

**คำอธิบายและตัวอย่าง Output:**
เมื่อ Player เดินไปตีศัตรู ระบบจะคำนวณ Damage โจมตีกันไปมา หากศัตรูตายแล้วจะโจมตีกลับไม่ได้อีก

```text
You got Potion1 : 20
Player energy after picking up potion: 119
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
<summary><b>ดูเฉลยแนวทางข้อ 6 (คลิกเพื่อขยาย)</b></summary>

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

## ข้อ 7: ระบบเก็บยาเพิ่มพลัง (File ItemPotion.cs และ Character.cs)

**โจทย์:**
1. **File `ItemPotion.cs`**
   - เขียน class `ItemPotion` โดยให้ Inherit มาจาก class `Identity`
   - สร้าง class variable ชื่อ `healPoint` (type `int`, ค่าเริ่มต้น 10, modifier `public`)
   - เขียน `override method Hit()` พิมพ์ข้อความ `Debug.Log($"You got {Name} : {healPoint}");`
   - สั่งเพิ่มเลือด: `mapGenerator.player.Heal(healPoint);`
   - เอาไอเทมออกจาก map: `mapGenerator.mapData[positionX, positionY] = 0; Destroy(gameObject);`
2. **File `Character.cs`**
   - เพิ่มเช็ค `IsPotion(toX, toY)` ใน `Move(...)`
   - ถ้าเจอ Potion ให้เรียก `Hit()`: `mapGenerator.potions[toX, toY].Hit();`
   - เลื่อนตำแหน่งผู้เล่นเข้าไปที่ช่องนั้น

<details>
<summary><b>ดูเฉลยแนวทางข้อ 7 (คลิกเพื่อขยาย)</b></summary>

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

## ข้อ 8: ระบบเก็บดาบเพิ่มพลังโจมตี (File ItemSword.cs และ Character.cs)

**โจทย์:**
1. **File `ItemSword.cs`**
   - เขียน class `ItemSword` โดยให้ Inherit มาจาก class `Identity`
   - สร้าง class variable ชื่อ `attackBonus` (type `int`, ค่าเริ่มต้น 10, modifier `public`)
   - เขียน `override method Hit()` พิมพ์ข้อความ `Debug.Log($"You got {Name} : {attackBonus}");` *(หมายเหตุในโจทย์เขียนผิดเป็น healPoint ให้ใช้ attackBonus)*
   - สั่งเพิ่มพลังโจมตี: `mapGenerator.player.IncreaseAttack(attackBonus);`
   - เอาไอเทมออกจาก map แบบเดียวกับ Potion
2. **File `Character.cs`**
   - เพิ่มเช็ค `IsSword(toX, toY)` ใน `Move(...)`
   - ถ้าเจอ Sword ให้เรียก `Hit()`: `mapGenerator.swords[toX, toY].Hit();`
   - เลื่อนตำแหน่งผู้เล่นเข้าไปที่ช่องนั้น

<details>
<summary><b>ดูเฉลยแนวทางข้อ 8 (คลิกเพื่อขยาย)</b></summary>

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
