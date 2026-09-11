# Week 04 Requirements: 2D Array & Nested Loop

โจทย์สำหรับสัปดาห์ที่ 4 มุ่งเน้นไปที่การใช้งาน **2D Array** และ **Nested Loop** โดยมีแบบฝึกหัดทั้งหมด 14 ข้อ ดังนี้

---

## ข้อ 1: การสร้าง 2D Array เริ่มต้น
**โจทย์:** จงเขียนโปรแกรมภาษา C# เพื่อสร้างอาร์เรย์สองมิติ (2D array) ชื่อ `my2DArray` ที่มีขนาด 3 x 3 โดยมีค่าเริ่มต้นดังนี้:
`{ 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }`

**ตัวอย่างผลลัพธ์:**
```text
1 2 3 
4 5 6 
7 8 9 
```

**คำอธิบายเพิ่มเติม:**
อาร์เรย์สองมิติ หรือ 2D Array เปรียบเสมือนตารางที่มีทั้งแถวและคอลัมน์ โดยแต่ละช่องเก็บข้อมูลได้หนึ่งค่า เช่นเดียวกับตารางใน Excel
ใช้สำหรับจัดเก็บข้อมูลที่มีโครงสร้างเป็นตาราง (เช่น ตารางคะแนน, แผนที่เกม) และสามารถเข้าถึงข้อมูลในตำแหน่งใดๆ ได้โดยตรงผ่านดัชนีของแถวและคอลัมน์
ตัวอย่างการประกาศตัวแปร: `int[,] my2DArray = new int[3, 5]`

---

## ข้อ 2: การเข้าถึงขนาดของ 2D Array
**โจทย์:** จงเขียนโปรแกรมภาษา C# เพื่อเข้าถึงขนาดของ Array สองมิติ โดยกำหนดตัวแปรให้ดังนี้:
```csharp
int[,] my2DArray = new int[3, 5] { 
     { 1, 2, 3, 4, 5 }, 
     { 1, 2, 3, 4, 5 }, 
     { 1, 2, 3, 4, 5 }};
```

ให้หาขนาดของ 2D Array ในมิติที่ 1 (แถว) และมิติที่ 2 (คอลัมน์) โดยใช้ `GetLength(0)` และ `GetLength(1)` และ Log ผลลัพธ์ออกมา
ให้พิมพ์ออกมาว่าขนาดในมิติที่ 1 และ 2 (Row และ Col) มีค่าเท่าไหร่ตามลำดับ

**ตัวอย่าง Input / Output:**
```text
number of rows (มิติที่ 1) ...
4
number of cols (มิติที่ 2) ...
8
rows = 4
cols = 8
```
```text
number of rows (มิติที่ 1) ...
3
number of cols (มิติที่ 2) ...
5
rows = 3
cols = 5
```

---

## ข้อ 3: การ Get และ Set ค่าใน 2D Array
**โจทย์:** ให้นักศึกษา get และ set ค่าใหม่ใน 2D Array โดยกำหนดให้มีตัวแปร 2D Array 2 ตัวดังนี้:
```csharp
int[,] my2DArray = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
string[,] my2DStringArray = new string[2, 3] { { "A", "B", "C" }, { "D", "E", "F" } };
```

1. **การเข้าถึงค่า (Get):** `my2DArray` แถวที่ 1 คอลัมน์ที่ 2 และ Log ค่าออกมาหน้าจอ
2. **การกำหนดค่าใหม่ (Set):** `my2DArray` เปลี่ยนค่าที่แถวที่ 1 คอลัมน์ที่ 2 เป็น `70` และ Log ค่าที่ set ออกมารวมถึงค่าทั้งหมดใน Array
3. **การเข้าถึงค่า (Get):** `my2DStringArray` แถวที่ 0 คอลัมน์ที่ 2 และ Log ออกมา
4. **การกำหนดค่าใหม่ (Set):** `my2DStringArray` เปลี่ยนค่าที่แถวที่ 0 คอลัมน์ที่ 2 เป็น `Cat` และ Log ออกมา

**ตัวอย่างผลลัพธ์:**
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

## ข้อ 4: เซ็ตตำแหน่งวัตถุในเกมให้ตรงกับตาราง
**โจทย์:** ให้นักศึกษาเซ็ตตำแหน่งวัตถุในเกมให้ตรงกับในตาราง โดยกำหนดตัวแปร 3 ตัวดังนี้:
```csharp
public Transform Item;
public int ItemPosX;
public int ItemPosY;
```
กำหนดค่าใหม่ให้ `Item.transform.position` ด้วย `new Vector2(ItemPosX, ItemPosY)` และ Log ค่าออกมาที่หน้าจอ

**ตัวอย่างผลลัพธ์:**
```text
ItemPosX ...
1
ItemPosY ...
2
(1.00, 2.00, 0.00)
```
```text
ItemPosX ...
11
ItemPosY ...
31
(11.00, 31.00, 0.00)
```

---

## ข้อ 5: สร้างไอเทมตามค่าใน 2D Array
**โจทย์:** ให้นักศึกษาอ่านค่าในตัวแปร 2D Array เมื่อเจอชื่อไอเทม ให้ทำการสร้าง Item (Instantiate) โดยกำหนดตัวแปรดังนี้:
```csharp
public GameObject[] Items;
public int ItemPosX;
public int ItemPosY;
public string[,] my2DStringArray = new string[3, 3] {
{ " ", "Soda", " "},
{ " ", " ", " "},
{ " ", " ", "Food"}};
```
เมื่อระบุตำแหน่ง `ItemPosX` และ `ItemPosY` หากเจอชื่อไอเทมใน `my2DStringArray` ให้ Loop หาใน `Items` ที่ชื่อตรงกัน แล้ว `Instantiate` ที่ตำแหน่ง X, Y หากไม่พบข้อมูลให้ Log: `No items at x: {ItemPosX} y: {ItemPosY}` 
หากพบชื่อ ให้ Instantiate และ Log: `Create Item {newItem.name} at x: {ItemPosX} y: {ItemPosY}`

**ตัวอย่างผลลัพธ์:**
```text
ItemPosX ...
1
ItemPosY ...
0
Create Item Soda at x: 1, y: 0
```
```text
ItemPosX ...
0
ItemPosY ...
0
No items at x: 0 y: 0
```

---

## ข้อ 6: หาผลรวมของตัวเลขใน Row (แถว)
**โจทย์:** ให้นักศึกษาเขียนโปรแกรมหาผลรวมของตัวเลขใน Row (แถว) โดยใช้ `for` loop
```csharp
public int[,] matrix = {
 { 1, 2, 3 }, 
 { 4, 5, 6 }, 
 { 7, 8, 9 } };
public int row;
```
ให้หาผลรวมของ Row ที่ระบุ โดยใช้ `matrix.GetLength(1)`

**ตัวอย่างผลลัพธ์:**
```text
Row ...
0
6
```
*(คำอธิบาย: Row #0 = 1 + 2 + 3 = 6)*

---

## ข้อ 7: หาผลรวมของแถว (แนวตั้ง - Column)
**โจทย์:** ให้นักศึกษาเขียนโปรแกรมหาผลรวมของคอลัมน์ โดยใช้ `for` loop
```csharp
public int[,] matrix = {
 { 1, 2, 3 }, 
 { 4, 5, 6 }, 
 { 7, 8, 9 } };
public int col;
```
ให้เข้าถึงขนาดด้วย `matrix.GetLength(0)`

**ตัวอย่างผลลัพธ์:**
```text
Col ...
0
12
```
*(คำอธิบาย: Col #0 = 1 + 4 + 7 = 12)*

---

## ข้อ 8: สร้างรูปแบบด้วย Nested Loop
**โจทย์:** จงเขียนโปรแกรมแสดงผลลัพธ์เป็นกลุ่มดาว `*` ด้วย Nested Loop
```csharp
public int columns = 3;
public int rows = 4;
```
**ตัวอย่างผลลัพธ์:**
```text
Column ...
3
Row ...
4
***
***
***
***
```

---

## ข้อ 9: สร้างแผนที่ 2D ด้วย Nested Loop (สุ่มพื้น)
**โจทย์:** เขียนโปรแกรมสร้างแผนที่ 2D วาง `floorTiles` แบบสุ่มในแต่ละช่องตาราง (Instantiate) 
```csharp
public int columns = 5;
public int rows = 5;
public GameObject[] floorTiles;
```
สมมติว่า `floorTiles` มีชื่อเป็น `0`, `1`, `2` ให้พิมพ์ชื่อของ tile เพื่อแสดง pattern

**ตัวอย่างผลลัพธ์:**
```text
Column ...
3
Row ...
3
211
110
000 
```

---

## ข้อ 10: สร้างกำแพงรอบนอกด้วย Nested Loop
**โจทย์:** เขียนโปรแกรมสร้างกำแพงรอบนอกขนาด `columns` x `rows` 
```csharp
public int columns = 5;
public int rows = 5;
public GameObject wall; // สมมติว่ามีชื่อ "*"
```
วางกำแพงขอบนอกเท่านั้น (ตรงกลางปล่อยว่าง) 
เงื่อนไขขอบคือ: `x == 0 || x == columns - 1 || y == 0 || y == rows - 1`

**ตัวอย่างผลลัพธ์:**
```text
Column ...
5
Row ...
3
*******
*     *
*     *
*     *
*******
```
*(หมายเหตุ: ต้องขยายพิกัดจากตำแหน่งขอบนอกด้วยตามสเปกโจทย์ x-1 ถึง columns+1)*

---

## ข้อ 11: สุ่มตำแหน่งไอเทมและตั้งค่าตำแหน่ง
**โจทย์:** สุ่มเลือกของจาก `foodTiles` (`Soda`, `Hamburger`) และสุ่มพิกัดมาวาง 1 ชิ้น
```csharp
Vector2 randomPosition = new Vector2(UnityEngine.Random.Range(0, columns), UnityEngine.Random.Range(0, rows));
GameObject tileChoice = foodTiles[UnityEngine.Random.Range(0, foodTiles.Length)];
GameObject item = Instantiate(tileChoice, randomPosition, Quaternion.identity);
```

**ตัวอย่างผลลัพธ์:**
```text
Column ...
5
Row ...
5
Hamburger at x: 0 y: 3
```

---

## ข้อ 12: สร้างแผนที่รูปสามเหลี่ยม
**โจทย์:** แสดงวิธีคิดการวาดสามเหลี่ยมโดยใช้ Nested Loop
```csharp
int size = 5;
```
**ตัวอย่างผลลัพธ์:**
```text
Size ...
5
*
**
***
****
*****
```

---

## ข้อ 13: ตารางสูตรคูณด้วย Nested Loop
**โจทย์:** แสดงตารางสูตรคูณตั้งแต่แม่ 2 ถึง 4 (คูณ 1 ถึง 12) คั่นด้วย Tab `\t`

**ตัวอย่างผลลัพธ์:**
```text
2 x 1 = 2       3 x 1 = 3       4 x 1 = 4
2 x 2 = 4       3 x 2 = 6       4 x 2 = 8
...
2 x 12 = 24     3 x 12 = 36     4 x 12 = 48
```

---

## ข้อ 14: เกม XO
**โจทย์:** จำลองเกม XO ขนาด 3x3 
```csharp
public static char[,] board = new char[3, 3] { ... };
```
สลับตาเล่น X และ O โดยรับ input `row col` ถ้าใส่ทับให้ขึ้น `cannot set ...` ตรวจสอบผลแพ้ชนะ/เสมอ `X wins!`, `O wins!`, `Draw!`

**ตัวอย่างการพิมพ์ตาราง:**
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
