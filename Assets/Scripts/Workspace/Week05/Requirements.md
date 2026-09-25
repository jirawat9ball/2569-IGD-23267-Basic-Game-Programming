# Week 05 Requirements: Methods & Refactoring

โจทย์สำหรับสัปดาห์ที่ 5 มุ่งเน้นไปที่การสร้างและใช้งาน **Method** ในภาษา C# ทั้งแบบ Void, Parameter, Return Type, Overloading และการนำโค้ดมารีแฟคเตอร์ (Refactor) แบ่งออกเป็น:
- **Lecture (ข้อ 1–7):** เรียนรู้ในคาบ (Method พื้นฐาน, Return Type, และ Player Controller)
- **Homework (Ex01, Lv01–Lv06):** การบ้าน (Refactoring แผนที่ และ Method คำนวณระบบเกม Level 1: Simple)

---

## ข้อ 1: การประกาศ Method เบื้องต้น (Void & Parameters)
**โจทย์:** ให้นักศึกษาประกาศ Method ใน C# ตามเงื่อนไขดังนี้
1. **ไม่มีพารามิเตอร์:** `UserNameIdentification();` -> พิมพ์ `"user name is UntitleUser"`
2. **รับพารามิเตอร์ 1 ตัว:** `UserNameIdentification(string name)` -> พิมพ์ `"user name is " + name`
3. **รับพารามิเตอร์ 2 ตัว:** `UserNameIdentification(string name, int age)` -> พิมพ์ `"user name is " + name + " age is " + age`
4. **มีค่าเริ่มต้น:** `UserCountry(string country = "Thailand")` -> พิมพ์ค่าของ `country`

**ตัวอย่างผลลัพธ์:**
```text
user name is UntitleUser
user name is boy
user name is big age is 18
Thailand
```

<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
using System;
using UnityEngine;

class Assignment : MonoBehavior
{
    public void Start(){
        UserNameIdentification();
        UserNameIdentification("boy");
        UserNameIdentification("big", 18);
        UserCountry();
    }

    public void UserNameIdentification(){
        Debug.Log("user name is UntitleUser");
    }
    public void UserNameIdentification(string name){
        Debug.Log("user name is "+ name);
    }
    public void UserNameIdentification(string name, int age){
        Debug.Log("user name is " + name + " age is " + age);
    }
    public void UserCountry(string country = "Thailand"){
        Debug.Log(country);
    }
}
```
</details>

---

## ข้อ 2: การประกาศ Method ประเภท Return Type
**โจทย์:** ให้นักศึกษาประกาศ Method ประเภทมีการคืนค่ากลับ (Return Type) ดังนี้
1. `Add(int a, int b)`: นำมาบวกกันแล้ว return ค่า
2. `GetStringLength(string text)`: return จำนวนตัวอักษรของข้อความ (`text.Length`)
3. `ConvertInttoBool(int sex)`: คืนค่าเป็น `true` ถ้า sex == 1, นอกนั้นเป็น `false`

**ตัวอย่างผลลัพธ์:**
```text
a ...
1
b ...
9
1+9=10
text ...
hello
length: 5
sex input 0||1
1
is male: True
```

<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
using System;
using UnityEngine;

class Assignment : MonoBehavior
{
    public int Add(int a,int b){
        int c = a+b;
        return c;
    }
    
    public int GetStringLength(string text){
        return text.Length;
    }

    public bool ConvertInttoBool(int sex){
        return sex == 1;
    }
}
```
</details>

---

## Ex01: การย้ายโค้ดสร้างเป็น Method (Refactoring)
> 📝 **เขียนในไฟล์:** `MapGenerator.cs`

**โจทย์:** ให้นักศึกษาย้ายโค้ดสร้างแผนที่จาก `Start()` ไปสร้างเป็น Method แยกต่างหาก 5 ตัวดังนี้:
1. `GenerateFloor()`
2. `GenerateWalls()`
3. `GenerateFoods()`
4. `PlacePlayer()`
5. `PlaceExit()`

**ตัวอย่างเอาโค้ดสร้างพื้นไปใส่ใน `GenerateFloor()`:**
```csharp
public void GenerateFloor()
{
    for (int y = 0; y < rows; y++)
    {
        for (int x = 0; x < columns; x++)
        {
            GameObject toInstantiate = floorTiles[UnityEngine.Random.Range(0, floorTiles.Length)];
            GameObject tile = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
        }
    }
}
```

<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
using System;
using UnityEngine;

class MapGenerator : MonoBehavior
{
    public int columns = 3;
    public int rows = 4;
    public GameObject[] floorTiles;
    // (ตัวแปรอื่นๆ)

    public void Start()
    {
        GenerateFloor();
        // GenerateWalls();
        // GenerateFoods();
        // PlacePlayer();
        // PlaceExit();
    }

    public void GenerateFloor()
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                GameObject toInstantiate = floorTiles[UnityEngine.Random.Range(0, floorTiles.Length)];
                GameObject tile = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
            }
        }
    }
    // เพิ่ม Method ส่วนที่เหลือตามโจทย์...
}
```
</details>

---

## ข้อ 3: การเขียน Method ชื่อ Move (Void & Parameter)
**โจทย์:** ในคลาส `Player` จะมีตัวแปร `energy = 20` ให้เขียนเมธอด `Move` ดังนี้:
- Access Modifier เป็น `public`
- Return Type เป็น `void`
- พารามิเตอร์ 1 ตัว ประเภท `Vector2` ชื่อ `direction`
- การทำงาน: ให้เปลี่ยนค่า `transform.position` ของตัวละครตามทิศทางที่ส่งเข้ามา และลดค่า `energy` ลงทีละ 1 ทุกครั้งที่เดิน

**ตัวอย่างผลลัพธ์:** เมื่อเดิน ขวา, ขวา, ขวา, บน, บน, บน
```text
player pos x: 3 y: 3 energy: 14
```

<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
using UnityEngine;

public class Player : MonoBehaviour
{
    public int energy = 20;

    public void Move(Vector2 direction)
    {
        transform.position += new Vector3(direction.x, direction.y, 0);
        energy -= 1;
    }
}
```
</details>

---

## ข้อ 4: การเขียน Method ชื่อ TakeDamage
**โจทย์:** ให้เขียนเมธอดสำหรับลด `energy` ลงตามค่า Damage ที่กำหนด
- Access Modifier เป็น `public`
- Return Type เป็น `void`
- พารามิเตอร์ 1 ตัว ประเภท `int` ชื่อ `Damage`
- การทำงาน:
  - ลด `energy` ตาม `Damage` ที่รับมา (ค่า `energy` ต้องไม่ต่ำกว่า 0)
  - แสดงค่าพลังงานที่เหลือผ่าน `Debug.Log("Current Energy : " + energy);`
  - เรียกใช้ `CheckDead()` เพื่อตรวจว่าตัวละครตายหรือยัง

**ตัวอย่างผลลัพธ์:** (เมื่อโดน Damage ไป 4, 5, 6 จากค่าเริ่มต้น 20)
```text
Current Energy : 16
Current Energy : 11
Current Energy : 5
```

---

## ข้อ 5: การเขียน Method ชื่อ CheckDead() (Private Scope)
**โจทย์:** ให้เขียนเมธอดตรวจสอบสถานะการตายของตัวละคร
- Access Modifier เป็น `private`
- Return Type เป็น `void` ไม่รับพารามิเตอร์
- การทำงาน: ถ้า `energy` น้อยกว่าหรือเท่ากับ 0 ให้พิมพ์ข้อความ `"You Lose"` ผ่าน `Debug.Log`

**ตัวอย่างผลลัพธ์:**
```text
Current Energy : 30
Current Energy : 20
Current Energy : 10
Current Energy : 0
You Lose
```

<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
using UnityEngine;

public class Player : MonoBehaviour
{
    public int energy = 20;

    public void TakeDamage(int Damage)
    {
        energy -= Damage;
        if (energy < 0)
        {
            energy = 0;
        }

        Debug.Log("Current Energy : " + energy);
        CheckDead();
    }

    private void CheckDead()
    {
        if (energy <= 0)
        {
            Debug.Log("You Lose");
        }
    }
}
```
</details>

---

## ข้อ 6: การเขียน Method ชื่อ Heal (Default Parameter)
**โจทย์:** ให้เขียนเมธอดสำหรับเพิ่ม `energy` ให้ตัวละคร โดยมีค่าเริ่มต้นของพารามิเตอร์ (Default Parameter):
- Access Modifier เป็น `public`
- Return Type เป็น `void`
- พารามิเตอร์ 1 ตัว ประเภท `int` ชื่อ `healPoint` โดยมีค่าเริ่มต้นเป็น 10 (`int healPoint = 10`)
- การทำงาน: เพิ่มค่า `energy` ขึ้นตาม `healPoint` ที่ได้รับ

**ตัวอย่างผลลัพธ์:**
- สั่ง `Heal(4)` (จาก 20) -> ได้ 24
- สั่ง `Heal()` แบบไม่ส่งค่า (จาก 15) -> ได้ 25 (เพิ่มตามค่าเริ่มต้น 10)

<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
using UnityEngine;

public class Player : MonoBehaviour
{
    public int energy = 20;

    public void Heal(int healPoint = 10)
    {
        energy += healPoint;
    }
}
```
</details>

---

## ข้อ 7: การเขียน Method ชื่อ CanMove() (Return Type bool)
**โจทย์:** ให้เขียนเมธอดตรวจสอบว่าตัวละครยังมีพลังงานพอที่จะเดินต่อได้หรือไม่
- Access Modifier เป็น `public`
- Return Type เป็น `bool` ไม่รับพารามิเตอร์
- การทำงาน: คืนค่า `true` ถ้า `energy` มากกว่า 0 นอกนั้นให้คืนค่า `false`

**ตัวอย่างผลลัพธ์:**
- ถ้า `energy = 20` -> `CanMove()` คืนค่า `true`
- ถ้า `energy = 0` -> `CanMove()` คืนค่า `false`

<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
using UnityEngine;

public class Player : MonoBehaviour
{
    public int energy = 20;

    public bool CanMove()
    {
        return energy > 0;
    }
}
```
</details>

---

# Homework: Level 1: Simple (โจทย์การบ้าน Method ระบบเกม Lv01 – Lv06)

เขียนในไฟล์ `Assignment_Student_Week05.cs` ภายใต้ `#region Level 1: Simple`

---

## Lv01: คำนวณดาเมจสุทธิ (Lv01_CalculateDamage)
**โจทย์:** คำนวณค่าดาเมจจากการโจมตีโดยนำพลังโจมตีพื้นฐานคูณกับตัวคูณดาเมจ
- Signature: `public int Lv01_CalculateDamage(int baseDamage, float multiplier)`
- การทำงาน: เอา `baseDamage` คูณกับ `multiplier` แปลงเป็น `int` แล้ว return ค่ากลับไป
- **ตัวอย่าง:** `Lv01_CalculateDamage(100, 1.5f)` -> `150`, `Lv01_CalculateDamage(10, 1.25f)` -> `12`

<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
public int Lv01_CalculateDamage(int baseDamage, float multiplier)
{
    return (int)(baseDamage * multiplier);
}
```
</details>

---

## Lv02: ตรวจสอบมานาในการร่ายเวท (Lv02_CanCastSpell)
**โจทย์:** ตรวจสอบว่ามานาปัจจุบันเพียงพอต่อการร่ายสกิลหรือไม่
- Signature: `public bool Lv02_CanCastSpell(int currentMana, int manaCost)`
- การทำงาน: ถ้า `currentMana >= manaCost` คืนค่า `true` นอกนั้นคืนค่า `false`
- **ตัวอย่าง:** `Lv02_CanCastSpell(50, 30)` -> `true`, `Lv02_CanCastSpell(20, 30)` -> `false`

<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
public bool Lv02_CanCastSpell(int currentMana, int manaCost)
{
    return currentMana >= manaCost;
}
```
</details>

---

## Lv03: ค้นหาคะแนนสูงสุด (Lv03_FindHighestScore)
**โจทย์:** หาค่าตัวเลขที่มากที่สุดใน Array
- Signature: `public int Lv03_FindHighestScore(int[] scores)`
- การทำงาน: วนลูปหาค่าสูงสุดใน Array `scores` แล้ว return ค่านั้นกลับไป (หาก Array เป็น null หรือว่าง ให้คืนค่า 0)
- **ตัวอย่าง:** `Lv03_FindHighestScore(new int[] { 10, 45, 99, 23, 7 })` -> `99`

<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
public int Lv03_FindHighestScore(int[] scores)
{
    if (scores == null || scores.Length == 0) return 0;
    int max = scores[0];
    for (int i = 1; i < scores.Length; i++)
    {
        if (scores[i] > max)
        {
            max = scores[i];
        }
    }
    return max;
}
```
</details>

---

## Lv04: คำนวณคะแนนรวม (Lv04_CalculateTotalScore)
**โจทย์:** หาผลรวมของตัวเลขทั้งหมดใน Array
- Signature: `public int Lv04_CalculateTotalScore(int[] scores)`
- การทำงาน: วนลูปบวกผลรวมสมาชิกทั้งหมดใน `scores` แล้ว return ผลรวม (หาก Array เป็น null หรือว่าง ให้คืนค่า 0)
- **ตัวอย่าง:** `Lv04_CalculateTotalScore(new int[] { 10, 20, 30 })` -> `60`

<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
public int Lv04_CalculateTotalScore(int[] scores)
{
    if (scores == null || scores.Length == 0) return 0;
    int total = 0;
    for (int i = 0; i < scores.Length; i++)
    {
        total += scores[i];
    }
    return total;
}
```
</details>

---

## Lv05: ตรวจสอบการเลเวลอัป (Lv05_CheckLevelUp)
**โจทย์:** ตรวจสอบว่า EXP ปัจจุบันถึงเกณฑ์ที่ต้องใช้ในการอัปเลเวลหรือไม่
- Signature: `public bool Lv05_CheckLevelUp(int currentExp, int requiredExp)`
- การทำงาน: ถ้า `currentExp >= requiredExp` คืนค่า `true` นอกนั้นคืนค่า `false`
- **ตัวอย่าง:** `Lv05_CheckLevelUp(120, 100)` -> `true`, `Lv05_CheckLevelUp(99, 100)` -> `false`
<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
public bool Lv05_CheckLevelUp(int currentExp, int requiredExp)
{
    return currentExp >= requiredExp;
}
```
</details>

---

## Lv06: จำกัดช่วงพลังชีวิต (Lv06_ClampHealth)
**โจทย์:** จำกัดค่าพลังชีวิตไม่ให้ต่ำกว่า `minHealth` และไม่ให้เกิน `maxHealth`
- Signature: `public int Lv06_ClampHealth(int currentHealth, int minHealth, int maxHealth)`
- การทำงาน:
  - ถ้า `currentHealth < minHealth` คืนค่า `minHealth`
  - ถ้า `currentHealth > maxHealth` คืนค่า `maxHealth`
  - นอกนั้นคืนค่า `currentHealth` เดิม
- **ตัวอย่าง:** `Lv06_ClampHealth(120, 0, 100)` -> `100`, `Lv06_ClampHealth(-10, 0, 100)` -> `0`, `Lv06_ClampHealth(50, 0, 100)` -> `50`

<details>
<summary><b>ดูเฉลยแนวทาง (คลิกเพื่อขยาย)</b></summary>

```csharp
public int Lv06_ClampHealth(int currentHealth, int minHealth, int maxHealth)
{
    if (currentHealth < minHealth) return minHealth;
    if (currentHealth > maxHealth) return maxHealth;
    return currentHealth;
}
```
</details>
