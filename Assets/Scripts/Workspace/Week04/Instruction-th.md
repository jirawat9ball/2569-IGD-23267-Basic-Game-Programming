# Assignment Week 04: อาร์เรย์สองมิติ (2D Array) และ Nested Loop

## 📋 ภาพรวมของ Assignment

สัปดาห์นี้เราจะรู้จัก **อาร์เรย์สองมิติ (2D Array)** ซึ่งก็คือการเก็บข้อมูลเป็น "ตาราง" ที่มีทั้งแถวและคอลัมน์ เหมือนตารางใน Excel
พร้อมกับ **Nested Loop** (ลูปซ้อนลูป) ที่เป็นเครื่องมือหลักในการวนอ่านหรือวนสร้างข้อมูลในตาราง

ในเกม เราใช้ 2D Array บ่อยมาก เช่น แผนที่ด่าน ตารางช่องเก็บของ (inventory) กระดานหมากรุก หรือกระดาน XO

มีแบบฝึกหัดทั้งหมด **22 ข้อ** แบ่งออกเป็นชุดในห้องเรียน (Lecture) และการบ้าน (Homework) ในไฟล์ `Assignment_Student_Week04.cs`
ทุกข้อแสดงผลด้วย `Debug.Log()` และผลลัพธ์ต้องตรงกับที่ test case กำหนดเป๊ะ ๆ

## 🎯 จุดประสงค์การเรียนรู้

- ประกาศ เข้าถึง และแก้ไขค่าใน 2D Array ได้
- หาขนาดของแต่ละมิติด้วย `GetLength(0)` และ `GetLength(1)`
- ใช้ Nested Loop วนอ่านข้อมูลทั้งตารางได้
- นำ 2D Array ไปวางตำแหน่งวัตถุและสร้างแผนที่ในเกมได้
- ประยุกต์ทุกอย่างมารวมกันเป็นเกม XO

## 📚 โครงสร้างของ Assignment

- **Lecture Methods (9 methods: As01 – As09)** — การฝึกเขียนโค้ดเพื่อเรียนรู้พื้นฐานร่วมกันในชั้นเรียน
- **Homework - Level 1: Simple (10 methods: Lv01 – Lv10)** — การบ้านระดับพื้นฐาน เน้นคำนวณและวนลูปตาราง
- **Homework - Level 2: Moderate (3 methods: Ex01 – Ex03)** — การบ้านระดับประยุกต์ร่วมกับ Game Objects และ Game Logic

---

## 💡 ปูพื้นก่อนเริ่ม: 2D Array คืออะไร

Array ธรรมดาเก็บข้อมูลเรียงเป็นแถวเดียว แต่ **2D Array เก็บเป็นตาราง** ต้องบอกทั้ง "แถวที่เท่าไหร่" และ "คอลัมน์ที่เท่าไหร่" ถึงจะหยิบค่าออกมาได้

```csharp
// ประกาศตารางขนาด 3 แถว 3 คอลัมน์ พร้อมใส่ค่าเริ่มต้น
int[,] table = new int[3, 3] { { 1, 2, 3 },
                               { 4, 5, 6 },
                               { 7, 8, 9 } };

int value = table[1, 2];   // แถวที่ 1 คอลัมน์ที่ 2 -> ได้ 6
table[1, 2] = 70;          // เปลี่ยนค่าช่องนั้นเป็น 70

int rowCount = table.GetLength(0);  // จำนวนแถว   -> 3
int colCount = table.GetLength(1);  // จำนวนคอลัมน์ -> 3
```

> **จำไว้:** ในวงเล็บก้ามปู ตัวแรกคือ **แถว (row)** ตัวที่สองคือ **คอลัมน์ (column)** เสมอ และนับเริ่มจาก 0

**Nested Loop** คือลูปซ้อนลูป ใช้วนทุกช่องในตาราง — ลูปนอกวนแถว ลูปในวนคอลัมน์

```csharp
for (int row = 0; row < table.GetLength(0); row++)
{
    for (int col = 0; col < table.GetLength(1); col++)
    {
        // ทำอะไรกับ table[row, col]
    }
}
```

---

# 🔵 Lecture Methods (ในห้องเรียน)

## As01. สร้างและแสดงตาราง 3x3

**วัตถุประสงค์:** ประกาศ 2D Array พร้อมค่าเริ่มต้น และใช้ Nested Loop พิมพ์ออกมาเป็นตาราง

**Method Signature:**
```csharp
void As01_Create2DArray()
```

**Logic ที่ต้อง implement:**
- ประกาศ `int[,] my2DArray` ขนาด 3 x 3 ใส่ค่า `{1,2,3}`, `{4,5,6}`, `{7,8,9}`
- ใช้ `Debug.Log()` พิมพ์ค่าของแต่ละแถวออกมาทีละบรรทัด โดยคั่นตัวเลขด้วยช่องว่าง 1 ช่อง (เช่น `my2DArray[0, 0] + " " + ...`)

**ผลลัพธ์ที่ต้องได้:**
```text
1 2 3
4 5 6
7 8 9
```

---

## As02. หาขนาดของตาราง

**วัตถุประสงค์:** ใช้ `GetLength()` หาจำนวนแถวและคอลัมน์ของ 2D Array

**Method Signature:**
```csharp
void As02_ArraySize(int rows, int cols)
```

**Logic ที่ต้อง implement:**
- สร้าง `int[,]` ขนาด `rows` x `cols`
- หาจำนวนแถวด้วย `GetLength(0)` และจำนวนคอลัมน์ด้วย `GetLength(1)`
- พิมพ์ออกมาสองบรรทัดตามรูปแบบด้านล่าง

**ผลลัพธ์ที่ต้องได้:** (ตัวอย่าง `rows = 3`, `cols = 5`)
```text
rows = 3
cols = 5
```

---

## As03. อ่านค่าและเปลี่ยนค่าในตาราง

**วัตถุประสงค์:** เข้าถึงค่า (get) และกำหนดค่าใหม่ (set) ในช่องที่ต้องการ ทั้งตารางตัวเลขและตารางข้อความ

**Method Signature:**
```csharp
void As03_GetSet2DArray()
```

**Logic ที่ต้อง implement:**
- ประกาศตารางสองตัวนี้
  ```csharp
  int[,] my2DArray = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
  string[,] my2DStringArray = new string[2, 3] { { "A", "B", "C" }, { "D", "E", "F" } };
  ```
- อ่านค่า `my2DArray` แถว 1 คอลัมน์ 2 แล้วพิมพ์ `get : <ค่า>`
- เปลี่ยนค่าช่องนั้นเป็น `70` แล้วพิมพ์ `set : 70`
- พิมพ์เส้นคั่น (เครื่องหมาย `=` จำนวน 28 ตัว) แล้วพิมพ์ตารางทั้งหมด
- ทำแบบเดียวกันกับ `my2DStringArray` แถว 0 คอลัมน์ 2 โดยเปลี่ยนค่าเป็น `Cat`

**ผลลัพธ์ที่ต้องได้:**
```text
get : 6
set : 70
============================
1 2 3
4 5 70
7 8 9
============================
get : C
set : Cat
============================
A B Cat
D E F
```

---

## As04. สร้างแถวกำแพง 1 แถว (Create Wall Row)

**วัตถุประสงค์:** ใช้ลูป 1 มิติ สร้างแถวกำแพงแนวนอนตามความยาวที่กำหนด และ Instantiate ลงฉาก

**Method Signature:**
```csharp
void As04_CreateWallRow(int columns, GameObject wall)
```

**Logic ที่ต้อง implement:**
- วนลูป `x` ตั้งแต่ `0` ถึง `columns - 1`
- แต่ละช่องให้ `Instantiate(wall, new Vector2(x, 0), Quaternion.identity)`
- ต่อตัวอักษร `*` ในตัวแปรข้อความ แล้วพิมพ์ออกมา 1 บรรทัดเมื่อจบลูป

**ผลลัพธ์ที่ต้องได้:** (`columns = 5`)
```text
*****
```

---

## As05. สร้างพื้นแผนที่แบบสุ่ม (Create Floor)

**วัตถุประสงค์:** ใช้ Nested Loop วางวัตถุลงทุกช่องของแผนที่ โดยสุ่มชนิดพื้น

**Method Signature:**
```csharp
void As05_CreateFloor(int columns, int rows, GameObject[] floorTiles)
```

**Logic ที่ต้อง implement:**
- วน Nested Loop ทีละแถว (`y`) และทีละคอลัมน์ (`x`)
- แต่ละช่องสุ่มเลือกพื้นจาก `floorTiles` ด้วย `Random.Range` แล้ว `Instantiate` ที่ตำแหน่ง `(x, y)`
- เก็บชื่อพื้นของแถวนั้นต่อกันเป็นข้อความ แล้วพิมพ์ออกมาบรรทัดละแถว

**ผลลัพธ์ที่ต้องได้:** (`columns = 3`, `rows = 3` และพื้นชื่อ `0`, `1`, `2` — ค่าที่ได้จะเปลี่ยนทุกครั้งเพราะสุ่ม)
```text
211
110
000
```

---

## As06. สร้างกำแพงล้อมรอบแผนที่ (Create Wall)

**วัตถุประสงค์:** ใช้ Nested Loop พร้อมเงื่อนไข เพื่อวางวัตถุเฉพาะขอบนอก

**Method Signature:**
```csharp
void As06_CreateWall(int columns, int rows, GameObject wall)
```

**Logic ที่ต้อง implement:**
- วนลูป `x` ตั้งแต่ `-1` ถึง `columns` และ `y` ตั้งแต่ `-1` ถึง `rows` (คือขยายออกไปด้านละ 1 ช่องเพื่อทำขอบ)
- ถ้าช่องนั้นอยู่ที่ขอบ (`x == -1 || x == columns || y == -1 || y == rows`) → `Instantiate` กำแพงที่ตำแหน่งนั้น และเก็บอักษร `*`
- ถ้าไม่ใช่ขอบ → เว้นว่าง เก็บอักษรเป็นช่องว่าง
- พิมพ์ออกมาบรรทัดละแถว

**ผลลัพธ์ที่ต้องได้:** (`columns = 5`, `rows = 3` → ได้ตารางกว้าง 7 สูง 5)
```text
*******
*     *
*     *
*     *
*******
```

---

## As07. วางวัตถุตามพิกัดในตาราง

**วัตถุประสงค์:** แปลงตำแหน่งแถว/คอลัมน์ให้เป็นตำแหน่งจริงของวัตถุในเกม

**Method Signature:**
```csharp
void As07_SetItemPosition(Transform item, int itemPosX, int itemPosY)
```

**Logic ที่ต้อง implement:**
- กำหนดตำแหน่งของ `item` ให้เป็น `new Vector2(itemPosX, itemPosY)`
- พิมพ์ค่า `item.position` ออกมา

**ผลลัพธ์ที่ต้องได้:** (ตัวอย่าง `itemPosX = 1`, `itemPosY = 2`)
```text
(1.00, 2.00, 0.00)
```

---

## As08. สุ่มวางไอเทมลงแผนที่

**วัตถุประสงค์:** สุ่มทั้งชนิดของและตำแหน่ง แล้ววางลงในแผนที่

**Method Signature:**
```csharp
void As08_RandomFoodItem(int columns, int rows, GameObject[] foodTiles)
```

**Logic ที่ต้อง implement:**
- สุ่มพิกัด `x` ในช่วง `0` ถึง `columns - 1` และ `y` ในช่วง `0` ถึง `rows - 1`
- สุ่มเลือกของ 1 ชิ้นจาก `foodTiles` แล้ว `Instantiate` ที่พิกัดนั้น
- พิมพ์ `<ชื่อของ> at x: <x> y: <y>`

**ผลลัพธ์ที่ต้องได้:** (`columns = 5`, `rows = 5` — ค่าที่ได้จะเปลี่ยนทุกครั้งเพราะสุ่ม)
```text
Hamburger at x: 0 y: 3
```

---

## As09. สร้างไอเทมตามชื่อที่อยู่ในตาราง

**วัตถุประสงค์:** อ่านชื่อไอเทมจากตาราง แล้วหา Prefab ที่ชื่อตรงกันมา Instantiate

**Method Signature:**
```csharp
void As09_CreateItemFromArray(GameObject[] items, int itemPosX, int itemPosY)
```

**Logic ที่ต้อง implement:**
- ใช้ตารางนี้ (แถวคือแกน Y คอลัมน์คือแกน X)
  ```csharp
  string[,] my2DStringArray = new string[3, 3] {
      { " ", "Soda", " " },
      { " ", " ",    " " },
      { " ", " ",    "Food" } };
  ```
- อ่านชื่อที่ช่อง `my2DStringArray[itemPosY, itemPosX]`
- ถ้าช่องนั้นว่าง (เป็นช่องว่าง) → พิมพ์ `No items at x: <x> y: <y>`
- ถ้ามีชื่อ → วนหาใน `items` ว่ามีตัวไหนชื่อตรงกัน
  - เจอ → `Instantiate` ที่ตำแหน่ง `(itemPosX, itemPosY)` แล้วพิมพ์ `Create Item <ชื่อ> at x: <x> y: <y>`
  - ไม่เจอ → พิมพ์ `No items at x: <x> y: <y>`

**ผลลัพธ์ที่ต้องได้:** (ตัวอย่าง `itemPosX = 1`, `itemPosY = 0`)
```text
Create Item Soda at x: 1 y: 0
```

---

# 🟢 Homework: Level 1 (Simple)

## Lv01. หาผลรวมของแถวที่กำหนด

**วัตถุประสงค์:** ใช้ `for` loop วนตามคอลัมน์ของแถวเดียว เพื่อรวมค่า

**Method Signature:**
```csharp
void Lv01_SumRow(int[,] matrix, int row)
```

**Logic ที่ต้อง implement:**
- วนลูปตามจำนวนคอลัมน์ (`matrix.GetLength(1)`) บวกค่าในแถว `row` เข้าด้วยกัน
- พิมพ์ผลรวมออกมาอย่างเดียว

**ผลลัพธ์ที่ต้องได้:** (ตาราง `{{1,2,3},{4,5,6},{7,8,9}}`, `row = 0` → 1+2+3)
```text
6
```

---

## Lv02. หาผลรวมของคอลัมน์ที่กำหนด

**วัตถุประสงค์:** ใช้ `for` loop วนตามแถว เพื่อรวมค่าในคอลัมน์เดียว

**Method Signature:**
```csharp
void Lv02_SumColumn(int[,] matrix, int col)
```

**Logic ที่ต้อง implement:**
- วนลูปตามจำนวนแถว (`matrix.GetLength(0)`) บวกค่าในคอลัมน์ `col` เข้าด้วยกัน
- พิมพ์ผลรวมออกมาอย่างเดียว

**ผลลัพธ์ที่ต้องได้:** (ตาราง `{{1,2,3},{4,5,6},{7,8,9}}`, `col = 0` → 1+4+7)
```text
12
```

---

## Lv03. วาดสี่เหลี่ยมด้วยดาว

**วัตถุประสงค์:** ฝึก Nested Loop พื้นฐาน ลูปนอกคุมจำนวนบรรทัด ลูปในคุมจำนวนตัวอักษร

**Method Signature:**
```csharp
void Lv03_StarPattern(int columns, int rows)
```

**Logic ที่ต้อง implement:**
- พิมพ์ดาว `*` ออกมาเป็นสี่เหลี่ยม กว้าง `columns` ตัว สูง `rows` บรรทัด

**ผลลัพธ์ที่ต้องได้:** (`columns = 3`, `rows = 4`)
```text
***
***
***
***
```

---

## Lv04. วาดสามเหลี่ยม

**วัตถุประสงค์:** ฝึก Nested Loop ที่ลูปในขึ้นกับค่าของลูปนอก

**Method Signature:**
```csharp
void Lv04_TrianglePattern(int size)
```

**Logic ที่ต้อง implement:**
- บรรทัดที่ 1 พิมพ์ดาว 1 ตัว บรรทัดที่ 2 พิมพ์ 2 ตัว ไปเรื่อย ๆ จนถึงบรรทัดที่ `size`

**ผลลัพธ์ที่ต้องได้:** (`size = 5`)
```text
*
**
***
****
*****
```

---

## Lv05. ตารางสูตรคูณหลายแม่พร้อมกัน

**วัตถุประสงค์:** ใช้ Nested Loop จัดข้อมูลออกมาเป็นตารางหลายคอลัมน์

**Method Signature:**
```csharp
void Lv05_MultiplicationTableNested(int fromTable, int toTable)
```

**Logic ที่ต้อง implement:**
- แต่ละบรรทัดคือตัวคูณ 1 ถึง 12
- ในหนึ่งบรรทัด ให้ไล่แม่สูตรคูณตั้งแต่ `fromTable` ถึง `toTable` คั่นแต่ละแม่ด้วย Tab (`\t`)
- รูปแบบของแต่ละช่องคือ `<แม่> x <ตัวคูณ> = <ผลลัพธ์>`

**ผลลัพธ์ที่ต้องได้:** (`fromTable = 2`, `toTable = 4`)
```text
2 x 1 = 2	3 x 1 = 3	4 x 1 = 4
2 x 2 = 4	3 x 2 = 6	4 x 2 = 8
2 x 3 = 6	3 x 3 = 9	4 x 3 = 12
...
2 x 12 = 24	3 x 12 = 36	4 x 12 = 48
```

---

## Lv06. หาค่าสูงสุดในตาราง

**วัตถุประสงค์:** ใช้ Nested Loop วนตรวจหาค่าที่มากที่สุดใน 2D Array พร้อมจำตำแหน่งแถวและคอลัมน์

**Method Signature:**
```csharp
void Lv06_FindMaxInMatrix(int[,] matrix)
```

**Logic ที่ต้อง implement:**
- ตั้งตัวแปร `max` เริ่มต้นด้วยค่าแรกของตาราง `matrix[0, 0]` และจำตำแหน่ง `maxR = 0`, `maxC = 0`
- ใช้ Nested Loop วนอ่านทุกแถวและทุกคอลัมน์
- ถ้าเจอค่าที่มากกว่า `max` ให้บันทึกค่านั้นเป็น `max` ใหม่ พร้อมจำพิกัด `maxR` และ `maxC`
- พิมพ์ผลลัพธ์ในรูปแบบ: `Max value <max> at [<maxR>, <maxC>]`

**ผลลัพธ์ที่ต้องได้:** (ตัวอย่าง Matrix 3x3 ที่มีค่าสูงสุดคือ 9 อยู่ที่แถว 2 คอลัมน์ 2)
```text
Max value 9 at [2, 2]
```

---

## Lv07. นับจำนวนช่องที่มีค่าเป้าหมาย

**วัตถุประสงค์:** ใช้ Nested Loop วนนับจำนวนช่องใน 2D Array ที่มีค่าตรงกับที่กำหนด

**Method Signature:**
```csharp
void Lv07_CountTargetValue(int[,] matrix, int target)
```

**Logic ที่ต้อง implement:**
- ตั้งตัวแปรตัวนับ `count = 0`
- ใช้ Nested Loop วนตรวจทุกช่องในตาราง `matrix`
- ถ้าช่องใดมีค่าเท่ากับ `target` ให้บวก `count` เพิ่ม 1
- พิมพ์ผลลัพธ์ในรูปแบบ: `Found target <target>: <count> cells`

**ผลลัพธ์ที่ต้องได้:** (ตัวอย่าง `target = 1` ในตารางที่มีเลข 1 อยู่ 5 ช่อง)
```text
Found target 1: 5 cells
```

---

## Lv08. หาผลรวมของสมาชิกทุกช่องในตาราง

**วัตถุประสงค์:** ใช้ Nested Loop วนบวกค่าทุกช่องใน 2D Array เข้าด้วยกัน

**Method Signature:**
```csharp
void Lv08_SumAllElements(int[,] matrix)
```

**Logic ที่ต้อง implement:**
- ตั้งตัวแปรผลรวม `int sum = 0`
- ใช้ Nested Loop วนอ่านทุกแถวและทุกคอลัมน์ของ `matrix`
- บวกค่าในแต่ละช่องเข้าไปใน `sum`
- พิมพ์ผลลัพธ์: `Debug.Log(sum)`

**ผลลัพธ์ที่ต้องได้:** (ตัวอย่าง Matrix 3x3 ค่า 1 ถึง 9)
```text
45
```

---

## Lv09. วาดสามเหลี่ยมดาวกลับด้าน

**วัตถุประสงค์:** ฝึกการควบคุม Nested Loop โดยให้จำนวนรอบของลูปในลดลงตามแถว

**Method Signature:**
```csharp
void Lv09_InvertedTrianglePattern(int size)
```

**Logic ที่ต้อง implement:**
- วนแถว `r` จาก `size` ถอยหลังลงมาจนถึง `1`
- ในแต่ละแถว ให้พิมพ์ดาว `*` จำนวน `r` ตัว
- พิมพ์ผลลัพธ์ของแต่ละแถวด้วย `Debug.Log()`

**ผลลัพธ์ที่ต้องได้:** (`size = 4`)
```text
****
***
**
*
```

---

## Lv10. อ่านค่าแนวทแยงมุมหลัก

**วัตถุประสงค์:** เข้าถึงข้อมูลในแนวทแยงมุมหลัก (`matrix[i, i]`) ของตาราง 2 มิติ

**Method Signature:**
```csharp
void Lv10_PrintMainDiagonal(int[,] matrix)
```

**Logic ที่ต้อง implement:**
- หาความยาวแนวทแยงที่อ่านได้: ค่าน้อยสุดระหว่างแถวกับคอลัมน์ `minDim = Mathf.Min(matrix.GetLength(0), matrix.GetLength(1))`
- วนลูปอ่านค่าที่พิกัด `matrix[i, i]` ตั้งแต่ `i = 0` จนถึง `minDim - 1`
- รวมค่าให้อยู่ในบรรทัดเดียว คั่นด้วยช่องว่าง 1 ช่อง (เช่น `"1 5 9"`)
- พิมพ์ผลลัพธ์ด้วย `Debug.Log()`

**ผลลัพธ์ที่ต้องได้:** (ตัวอย่าง Matrix 3x3 ที่มีแนวทแยงคือ 1, 5, 9)
```text
1 5 9
```

---

# 🔴 Homework: Level 2 (Moderate)

## Ex01. เกม XO (Tic-Tac-Toe)

**วัตถุประสงค์:** รวมทุกอย่างของสัปดาห์นี้ — 2D Array, Nested Loop, เงื่อนไข — มาทำเป็นเกมจริง

**Method Signature:**
```csharp
void Ex01_TicTacToe(int[,] moves)
```

**Logic ที่ต้อง implement:**
- สร้างกระดาน `char[,] board` ขนาด 3 x 3 เริ่มต้นเป็นช่องว่างทุกช่อง
- `moves` คือลำดับการเดิน แต่ละแถวคือ `{ แถว, คอลัมน์ }` ผู้เล่นสลับกันโดยเริ่มที่ `X`
- แต่ละตา:
  1. พิมพ์ `Player <X หรือ O>:`
  2. พิมพ์พิกัดที่เดิน `<แถว> <คอลัมน์>`
  3. ถ้าช่องนั้นมีคนลงไปแล้ว → พิมพ์ `cannot set <แถว> <คอลัมน์>` แล้วข้ามไปตาถัดไป (ยังเป็นตาของคนเดิม)
  4. ถ้าลงได้ → ใส่สัญลักษณ์ลงกระดาน แล้วพิมพ์กระดานออกมา
- พิมพ์กระดานด้วยรูปแบบ: เส้นคั่น `-` จำนวน 13 ตัว สลับกับแถวของกระดาน
- หลังลงแต่ละตา ให้ตรวจว่ามีผู้ชนะหรือยัง (แนวนอน 3 แนว, แนวตั้ง 3 แนว, ทแยง 2 แนว)
  - ชนะ → พิมพ์ `X wins!` หรือ `O wins!` แล้วจบเกม
  - ลงครบ 9 ช่องแล้วไม่มีใครชนะ → พิมพ์ `Draw!` แล้วจบเกม

**ผลลัพธ์ที่ต้องได้:** (ตาแรก `X` ลงที่แถว 0 คอลัมน์ 1)
```text
Player X:
0 1
-------------
|   | X |   |
-------------
|   |   |   |
-------------
|   |   |   |
```

---

## Ex02. ตรวจสอบพื้นที่เดินได้ในตารางแผนที่

**วัตถุประสงค์:** ตรวจสอบเงื่อนไขตำแหน่งของตัวละคร (Tile Collision Check) และป้องกันการเดินออกนอกแผนที่

**Method Signature:**
```csharp
void Ex02_CheckWalkableTile(int[,] map, int targetX, int targetY)
```

**Logic ที่ต้อง implement:**
- หาจำนวนแถว `rows = map.GetLength(0)` และจำนวนคอลัมน์ `cols = map.GetLength(1)`
- ตรวจสอบว่าพิกัดหลุดออกนอกตารางหรือไม่ (`targetX < 0 || targetX >= cols || targetY < 0 || targetY >= rows`):
  - ถ้าออกนอกขอบเขต → พิมพ์ `Position (<targetX>, <targetY>) is Out of Bounds` แล้ว `return`
- ถ้าพิกัดอยู่ในแผนที่ (หมายเหตุ: `targetX` คือแกนคอลัมน์, `targetY` คือแกนแถว เข้าถึงด้วย `map[targetY, targetX]`):
  - ถ้าค่าเป็น `0` → พิมพ์ `Position (<targetX>, <targetY>) is Walkable`
  - ถ้าค่าเป็น `1` → พิมพ์ `Position (<targetX>, <targetY>) is Blocked by Wall`
  - ถ้าเป็นค่าอื่น ๆ → พิมพ์ `Position (<targetX>, <targetY>) is Blocked`

**ผลลัพธ์ที่ต้องได้:** (ตัวอย่าง `targetX = 1`, `targetY = 1` ค่าในตารางคือ 0)
```text
Position (1, 1) is Walkable
```

---

## Ex03. วางหีบสมบัติทั้ง 4 มุมแผนที่

**วัตถุประสงค์:** คำนวณพิกัดมุมของกระดานขนาดใด ๆ และสร้าง Prefab ลงในฉากด้วย `Instantiate`

**Method Signature:**
```csharp
void Ex03_SpawnChestsInCorners(int columns, int rows, GameObject chestPrefab)
```

**Logic ที่ต้อง implement:**
- คำนวณพิกัดมุมทั้ง 4 ของแผนที่ขนาด `columns` x `rows`:
  - มุมซ้ายล่าง: `(0, 0)`
  - มุมขวาล่าง: `(columns - 1, 0)`
  - มุมซ้ายบน: `(0, rows - 1)`
  - มุมขวาบน: `(columns - 1, rows - 1)`
- วนลูปสร้างหีบสมบัติด้วย `Instantiate(chestPrefab, <ตำแหน่ง>, Quaternion.identity)` หาก `chestPrefab != null`
- พิมพ์ข้อความยืนยัน: `Spawned 4 chests at corners`

**ผลลัพธ์ที่ต้องได้:**
```text
Spawned 4 chests at corners
```

---

## 📌 ข้อควรระวัง

- ตำแหน่งใน 2D Array คือ `[แถว, คอลัมน์]` **เสมอ** อย่าสลับกัน
- ข้อที่มีคำว่า Loop ต้องเขียน `for` หรือ `while` จริง ๆ ระบบตรวจจะเช็คโค้ดด้วย ไม่ใช่แค่ผลลัพธ์
- ข้อความที่พิมพ์ต้องตรงเป๊ะ ทั้งตัวพิมพ์เล็กใหญ่ ช่องว่าง และเครื่องหมาย
- อย่าใส่ช่องว่างเกินท้ายบรรทัด

**ขอให้สนุกกับการเขียนโค้ดครับ 👨‍💻**
