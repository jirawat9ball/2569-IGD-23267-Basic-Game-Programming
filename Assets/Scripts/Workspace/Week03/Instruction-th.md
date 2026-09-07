# Assignment 03: การเรียนรู้ Array, Loop (For, While), Instantiate และการเคลื่อนที่ สำหรับ Game Development

## 📋 ภาพรวมของ Assignment

เรียนรู้การจัดการชุดข้อมูลด้วย **Array**, การควบคุมการทำงานซ้ำด้วย **For Loop** และ **While Loop**, การควบคุมลูปด้วย **Break** และ **Continue**, การสร้างวัตถุในเกมแบบ Dynamic ด้วย **Instantiate** ตลอดจนการสร้างการเคลื่อนที่ด้วย **Coroutine** โดยการ implement 25 methods ที่ใช้งานจริงในกระบวนการสร้างเกม Assignment นี้เน้นการฝึกทักษะการคำนวณ Index, การวนลูปเข้าถึงสมาชิกใน Array, การจัดการเงื่อนไขการทำงานซ้ำ และการโต้ตอบกับ Game Objects ใน Unity แต่ละ method จะแสดงผลลัพธ์ผ่าน `Debug.Log()` และต้องตรงกับผลลัพธ์ที่คาดหวังจาก Test Cases อย่างแม่นยำ

---

## 🎯 จุดประสงค์การเรียนรู้

- เข้าใจหลักการทำงาน โครงสร้าง และการเข้าถึงข้อมูลของ **Array** (0-based Indexing, `.Length`)
- สามารถประยุกต์ใช้ **For Loop** ในการเข้าถึงข้อมูลทั้งแบบเรียงลำดับ, ก้าวกระโดด (Step), และย้อนกลับ (Reverse)
- สามารถประยุกต์ใช้ **While Loop** ในการทำงานตามเงื่อนไข และการคำนวณผลรวมสะสม
- เข้าใจการควบคุมลูปด้วยคำสั่ง **Break** (หยุดการทำงานทันที) และ **Continue** (ข้ามรอบปัจจุบัน)
- เข้าใจการสุ่มค่าด้วย `UnityEngine.Random.Range` ร่วมกับ Index ของ Array
- สามารถใช้ `Instantiate` เพื่อสร้าง GameObject ขึ้นในฉากตามตำแหน่งและระยะห่างที่คำนวณได้
- เข้าใจการเขียน Coroutine (`IEnumerator`, `yield return null`) เพื่อจัดการการเคลื่อนที่แบบ Frame-by-Frame
- พัฒนาโค้ดที่ถูกต้องตาม Clean Code และ Best Practices ของการพัฒนาเกม

---

## 📚 โครงสร้างของ Assignment

- **Lecture Methods (9 methods: As01 – As09)** - การฝึกเขียนโค้ดเพื่อเรียนรู้พื้นฐานร่วมกันในชั้นเรียน
- **Level 1: Simple (9 methods: Lv01 – Lv09)** - การบ้านระดับพื้นฐาน เน้นความเข้าใจ Array และ Loop
- **Level 2: Moderate (7 methods: Ex01 – Ex07)** - การบ้านระดับท้าทาย ผสมผสาน Loop, Array, Break, Continue และ Game Mechanics

---

## 🔵 Lecture Methods (ในห้องเรียน)

### 1. As01_IronManSuit (1 test case)

**วัตถุประสงค์:** แสดงความเข้าใจการประกาศ Array, การกำหนดค่าเริ่มต้น, การเข้าถึงสมาชิกตัวแรก และการวนลูป For เพื่อแสดงผลสมาชิกทั้งหมด

**Method Signature:**
```csharp
void As01_IronManSuit()
```

**Logic ที่ต้อง implement:**
1. สร้าง Array ประเภท `string[]` ชื่อ `IronManSuit` ขนาด 4 ช่อง บรรจุ: `"Mark I"`, `"Mark II"`, `"Mark III"`, `"Mark IV"`
2. ดึงชุดตัวแรก (index 0) มาเก็บในตัวแปร `string TonyStarkWear`
3. แสดงผลชุดที่สวมใส่: `TonyStark Wear : <TonyStarkWear>`
4. แสดงผลขนาดของ Array: `Room size IronManSuit : <IronManSuit.Length>`
5. แสดงหัวข้อ: `===All suit in collection===`
6. ใช้ For Loop แสดงชุดเกราะทั้งหมดออกมาทีละบรรทัด

**💡 แนวทางการเขียนโค้ด (Guideline):**
- ประกาศ Array พร้อมกำหนดค่าทันทีด้วยวงเล็บปีกกา `{ ... }` หรือระบุขนาด `new string[4]`
- สมาชิกตัวแรกของ Array จะอยู่ที่ Index `[0]` เสมอ
- เรียกใช้ Property `.Length` ของ Array เพื่อตรวจสอบจำนวนช่องทั้งหมด
- ตั้ง For Loop ให้ตัวนับรอบเริ่มจาก `i = 0` และวนตราบใดที่ `i < IronManSuit.Length`

**Output ที่คาดหวัง:**
```
TonyStark Wear : Mark I
Room size IronManSuit : 4
===All suit in collection===
Mark I
Mark II
Mark III
Mark IV
```

**Game Context:** ระบบ Equipment สวมใส่ไอเทมเริ่มต้น และระบบ Inventory ตรวจสอบชุดเกราะทั้งหมดของผู้เล่น

---

### 2. As02_SpiderManAndBatMan (1 test case)

**วัตถุประสงค์:** ฝึกการประกาศ Array 2 รูปแบบ (แบบกำหนดค่าทันที และแบบระบุขนาด `new string[4]`) พร้อมทั้งเข้าถึงข้อมูลผ่านลูป

**Method Signature:**
```csharp
void As02_SpiderManAndBatMan()
```

**Logic ที่ต้อง implement:**
1. สร้าง Array `spiderMan` บรรจุ: `"Classic SpiderMan"`, `"Symbiote SpiderMan"`, `"Iron Spider"`
2. สร้าง Array `BatMan` ขนาด 4 ช่อง บรรจุ: `"Classic BatMan"`, `"Dark Knight"`, `"Batman Beyond"`, `"The Batman"`
3. แสดงผล `Room size spiderMan : <ความยาว>` ตามด้วย `===All spiderMan in collection===` และวนลูปแสดงชื่อทั้งหมด
4. แสดงผล `Room size BatMan : <ความยาว>` ตามด้วย `===All BatMan in collection===` และวนลูปแสดงชื่อทั้งหมด

**💡 แนวทางการเขียนโค้ด (Guideline):**
- ชุดแรกประกาศแบบย่อ `string[] spiderMan = { ... };` และชุดสองประกาศแบบระบุขนาด `string[] BatMan = new string[4] { ... };`
- แสดงข้อความหัวข้อให้เรียบร้อยก่อนเริ่มวนลูปในแต่ละชุด
- วนลูปแยก 2 ลูป โดยลูปแรกใช้อ้างอิง `spiderMan.Length` และลูปที่สองใช้อ้างอิง `BatMan.Length`

**Output ที่คาดหวัง:**
```
Room size spiderMan : 3
===All spiderMan in collection===
Classic SpiderMan
Symbiote SpiderMan
Iron Spider
Room size BatMan : 4
===All BatMan in collection===
Classic BatMan
Dark Knight
Batman Beyond
The Batman
```

**Game Context:** ระบบคอลเลกชันตัวละครหรือสกินที่ผู้เล่นสามารถปลดล็อกได้ในเกม

---

### 3. As03_RandomItemDrop (1 test case)

**วัตถุประสงค์:** สุ่มเลือก GameObject จาก Array ด้วย `Random.Range` และทำการ Instantiate ในฉาก

**Method Signature:**
```csharp
void As03_RandomItemDrop(GameObject[] items)
```

**Logic ที่ต้อง implement:**
1. สุ่ม Index ของไอเทมในช่วง `[0, items.Length)` โดยใช้ `Random.Range(0, items.Length)`
2. ดึง GameObject ที่สุ่มได้จาก Array
3. สั่ง `Instantiate(picked, new Vector3(0, 3, 0), Quaternion.identity)`
4. แสดงผลชื่อไอเทมที่ได้รับ: `Got item : <picked.name>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- `Random.Range(min, max)` ของเลขจำนวนเต็ม (int) ค่าขอบบน `max` จะเป็น Exclusive (ไม่นำมารวม) ดังนั้นการใส่ `items.Length` จะได้ค่าสุ่มตั้งแต่ `0` ถึง `Length - 1` ซึ่งปลอดภัยจาก Index หลุดขอบ
- นำ index ที่ได้มาดึงสมาชิกใส่ตัวแปร GameObject ก่อนส่งเข้าฟังก์ชัน `Instantiate`
- ดึงชื่อไอเทมผ่าน Property `.name` ของ GameObject

**ตัวอย่าง Output:**
```
Got item : Coin
```

**Game Context:** ระบบ Loot Drop จากศัตรู หรือกล่องสุ่มกาชา (Gacha Box)

---

### 4. As04_ForLoopBasic (1 test case)

**วัตถุประสงค์:** ฝึกการใช้งาน For Loop ขั้นพื้นฐาน ทั้งแบบ index เริ่มจาก 0 (เงื่อนไข `<`) และ index เริ่มจาก 1 (เงื่อนไข `<=`)

**Method Signature:**
```csharp
void As04_ForLoopBasic()
```

**Logic ที่ต้อง implement:**
1. ลูปแรก: วนลูปตั้งแต่ `i = 0` ถึง `9` (`i < 10`) พิมพ์ `<10 : <i>`
2. พิมพ์เส้นคั่น: `======================`
3. ลูปสอง: วนลูปตั้งแต่ `i = 1` ถึง `10` (`i <= 10`) พิมพ์ `<=10 : <i>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- สังเกตความแตกต่างของเงื่อนไขการหยุดลูป: ลูปแรกใช้ `< 10` ตัวเลขจะหยุดที่ 9, ลูปสองใช้ `<= 10` ตัวเลขจะวิ่งไปถึง 10
- ตรวจสอบรูปแบบการเว้นวรรค string ให้ถูกต้อง เช่น `"<10 : "` และ `"<=10 : "`

**Output ที่คาดหวัง:**
```
<10 : 0
<10 : 1
<10 : 2
...
<10 : 9
======================
<=10 : 1
<=10 : 2
...
<=10 : 10
```

**Game Context:** การสร้างตาราง Loop และการนับรอบเวลานับถอยหลังของเกม

---

### 5. As05_ForLoopN (3 test cases)

**วัตถุประสงค์:** ฝึกการใช้ For Loop แบบ Dynamic ตามตัวเลขที่ส่งเข้ามาผ่าน parameter `n`

**Method Signature:**
```csharp
void As05_ForLoopN(int n)
```

**Logic ที่ต้อง implement:**
- วนลูป For ตั้งแต่ `i = 0` ถึง `n - 1`
- แสดงผลตัวเลข `i` ออกมาทีละบรรทัด (ถ้า `n <= 0` จะไม่แสดงผลใดๆ)

**💡 แนวทางการเขียนโค้ด (Guideline):**
- กำหนดเงื่อนไข `i < n` ซึ่งถ้าค่า `n <= 0` เงื่อนไขจะเป็นเท็จตั้งแต่เริ่มต้นและลูปจะไม่ทำงานโดยอัตโนมัติ ไม่จำเป็นต้องเขียน `if` ดักแยก
- พิมพ์เฉพาะค่าของตัวแปร `i` ออกมาโดยตรง

**ตัวอย่าง Input/Output:**
- Input: `n = 5`
  ```
  0
  1
  2
  3
  4
  ```

**Game Context:** การทำกระบวนการซ้ำตามจำนวนครั้ง เช่น การโจมตีต่อเนื่อง n ครั้ง หรือการสร้าง Wave ศัตรู

---

### 6. As06_ForLoopWithArray (3 test cases)

**วัตถุประสงค์:** ควบคุมการเพิ่มค่า Step ของตัวแปรนับรอบใน For Loop เพื่อข้ามสมาชิกใน Array

**Method Signature:**
```csharp
void As06_ForLoopWithArray(string[] suiteNames)
```

**Logic ที่ต้อง implement:**
1. พิมพ์ `======Log by One======` แล้ววนลูป For ทีละ 1 (`i++`) แสดงสมาชิกทั้งหมด
2. พิมพ์ `======Log by Two======` แล้ววนลูป For ข้ามทีละ 2 (`i += 2`) แสดงเฉพาะสมาชิกใน index คู่

**💡 แนวทางการเขียนโค้ด (Guideline):**
- ลูปชุดแรกใช้ตัวนับรอบแบบปกติ `i++`
- ลูปชุดที่สองเปลี่ยนส่วนการเพิ่มค่าใน For Loop จาก `i++` เป็น `i += 2` เพื่อให้ค่า index ข้ามทีละ 2 ช่อง (0, 2, 4, ...)
- สมาชิกที่นำมาแสดงผลคือ `suiteNames[i]`

**ตัวอย่าง Input/Output:**
- Input: `["Mark I", "Mark II", "Mark III", "Mark IV"]`
  ```
  ======Log by One======
  Mark I
  Mark II
  Mark III
  Mark IV
  ======Log by Two======
  Mark I
  Mark III
  ```

**Game Context:** การแสดงผลข้อมูลในตารางแบบสลับแถว (Zebra striping) หรือการเลือกยูนิตฝั่งใดฝั่งหนึ่ง

---

### 7. As07_InstantiateEnemies (2 test cases)

**วัตถุประสงค์:** วนลูปสร้างศัตรูตามจำนวนสมาชิกใน Array และวางตำแหน่งบนแกน X อัตโนมัติ

**Method Signature:**
```csharp
void As07_InstantiateEnemies(GameObject Enemy, int[] HpEnemy)
```

**Logic ที่ต้อง implement:**
1. วนลูปตามความยาวของ `HpEnemy`
2. สร้างศัตรูด้วย `Instantiate(Enemy)`
3. กำหนดตำแหน่ง `spawned.transform.position = new Vector3(i + 1, 0f, 0f)`
4. แสดงผล: `new enemy at position x = <i + 1>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- จำนวนรอบของลูปยึดตาม `HpEnemy.Length`
- ฟังก์ชัน `Instantiate(Enemy)` จะคืนค่า GameObject ตัวที่เพิ่งถูกสร้างขึ้นมา สามารถเก็บใส่ตัวแปรเพื่อนำไปกำหนดตำแหน่ง `.transform.position` ต่อได้
- พิกัดบนแกน X เริ่มต้นที่ 1 และเพิ่มขึ้นทีละ 1 ตามลำดับรอบของลูป (`i + 1`)

**Game Context:** การ Spawn กองทัพศัตรูเป็นแถวหน้ากระดานตามตำแหน่งที่กำหนด

---

### 8. As08_WhileLoopBasic (1 test case)

**วัตถุประสงค์:** แสดงโครงสร้าง While Loop พื้นฐาน การกำหนดตัวแปรนับ และการเพิ่มค่าเพื่อป้องกัน Infinite Loop

**Method Signature:**
```csharp
void As08_WhileLoopBasic()
```

**Logic ที่ต้อง implement:**
1. ประกาศตัวแปร `int i = 0;`
2. วนลูป While ตราบใดที่ `i < 10`
3. แสดงผล `while loop : <i>` และเพิ่มค่า `i++`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- ตัวแปรนับรอบ (Counter) ของ While Loop จะต้องถูกประกาศไว้ *นอกลูป* เสมอ
- ในแต่ละรอบของ While Loop ต้องมีคำสั่งเพิ่มค่า `i++` อยู่ภายในบล็อก เพื่อให้ตัวเลขเพิ่มขึ้นจนเงื่อนไขเป็นเท็จและหลุดจากลูปได้

**Output ที่คาดหวัง:**
```
while loop : 0
while loop : 1
...
while loop : 9
```

**Game Context:** การรอลูปตรวจจับสถานะของระบบเครือข่าย หรือกระบวนการโหลดข้อมูล

---

### 9. As09_MoveToTarget (3 test cases)

**วัตถุประสงค์:** การเขียน Coroutine ควบคุมการเคลื่อนที่ของตัวละครไปยังเป้าหมายอย่างต่อเนื่องในแต่ละเฟรม

**Method Signature:**
```csharp
IEnumerator As09_MoveToTarget(Transform character, Transform target, float speed)
```

**Logic ที่ต้อง implement:**
1. สร้างตัวแปรจับเวลา `float timer = 0f;`
2. วนลูป While ตราบใดที่ตำแหน่ง `character.position.x < target.position.x`
3. ขยับตัวละคร: `character.Translate(Vector3.right * speed * 0.1f)`
4. แสดงผลตำแหน่งปัจจุบันด้วยทศนิยม 2 ตำแหน่ง: `Debug.Log(character.position.x.ToString("F2"))`
5. สะสมเวลา `timer += Time.deltaTime;`
6. หน่วงการทำงานแต่ละเฟรมด้วย `yield return null;`
7. เมื่อถึงเป้าหมายให้พิมพ์: `Time : <timer>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- ฟังก์ชันประเภท `IEnumerator` ต้องมีคำสั่ง `yield return` อยู่ภายในเสมอ เพื่อบอกให้ Unity หยุดรอจังหวะเวลาที่กำหนดก่อนกลับมาทำงานต่อในเฟรมถัดไป
- `yield return null;` หมายถึงการรอจนกว่าจะถึงเฟรมถัดไป
- ใช้ `.ToString("F2")` เพื่อจัดฟอร์แมตตัวเลขทศนิยมให้แสดงผล 2 ตำแหน่งอย่างแน่นอน

**Game Context:** ระบบ Cutscene ตัวละครเดินไปหา NPC หรือการเคลื่อนที่ของศัตรูแบบ Patrol

---

## 🟢 Level 1: Simple (การบ้านระดับพื้นฐาน)

### 10. Lv01_SetArrayValues (1 test case)

**วัตถุประสงค์:** แสดงการสร้าง Array 2 ชุด และกำหนดค่าทีละ index จากนั้นนำมาแสดงผลจับคู่กัน

**Method Signature:**
```csharp
void Lv01_SetArrayValues()
```

**Logic ที่ต้อง implement:**
1. สร้าง Array `string[] weapons = new string[3];` กำหนด `[0] = "Sword"`, `[1] = "Axe"`, `[2] = "Bow"`
2. สร้าง Array `int[] damage = new int[3];` กำหนด `[0] = 100`, `[1] = 200`, `[2] = 300`
3. แสดงผลการจับคู่อาวุธและพลังโจมตีผ่าน Debug.Log ตามลำดับ:
   - `Sword damage : 100`
   - `Axe damage : 200`
   - `Bow damage : 300`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- สร้าง Array ขนาด 3 ช่องด้วยคีย์เวิร์ด `new string[3]` และ `new int[3]`
- กำหนดค่าลงในแต่ละช่องโดยตรงผ่านเครื่องหมายก้ามปู เช่น `weapons[0] = "Sword";`
- สั่ง `Debug.Log` โดยนำค่าจาก `weapons[i]` และ `damage[i]` มาเชื่อมต่อ String ด้วย `" damage : "`

**Game Context:** ตารางข้อมูลค่าสเตตัสของอาวุธประเภทต่างๆ ในเกม RPG

---

### 11. Lv02_InspectArray (2 test cases)

**วัตถุประสงค์:** ตรวจสอบโครงสร้างและดึงข้อมูลตำแหน่งสำคัญของ Array ได้แก่ สมาชิกตัวแรก, ตัวกลาง, และตัวสุดท้าย

**Method Signature:**
```csharp
void Lv02_InspectArray(string[] items)
```

**Logic ที่ต้อง implement:**
- แสดงจำนวนไอเทมทั้งหมด: `Total items : <items.Length>`
- แสดงไอเทมตัวแรก (index `0`): `First item : <items[0]>`
- แสดงไอเทมตรงกลาง (index `items.Length / 2`): `Middle item : <items[items.Length / 2]>`
- แสดงไอเทมตัวสุดท้าย (index `items.Length - 1`): `Last item : <items[items.Length - 1]>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- เข้าถึงความยาวทั้งหมดของ Array ด้วย Property `.Length`
- การหาตำแหน่งตรงกลาง สามารถนำความยาวหารด้วย 2 (`items.Length / 2`) ซึ่งในภาษา C# การหารจำนวนเต็มจะปัดเศษทิ้งให้อัตโนมัติ
- สมาชิกตัวสุดท้ายจะอยู่ที่ตำแหน่งความยาวลบหนึ่งเสมอ (`items.Length - 1`)

**ตัวอย่าง Input/Output:**
- Input: `["Potion", "Sword", "Bow", "Shield"]`
  ```
  Total items : 4
  First item : Potion
  Middle item : Bow
  Last item : Shield
  ```

**Game Context:** ระบบช่องสวมใส่ Quick Slots และการตรวจสอบไอเทมในกระเป๋า

---

### 12. Lv03_RandomDialogue (1 test case)

**วัตถุประสงค์:** การสุ่มเลือกข้อความบทสนทนาจาก Array โดยใช้ `UnityEngine.Random.Range`

**Method Signature:**
```csharp
void Lv03_RandomDialogue(string[] npc1Dialogues)
```

**Logic ที่ต้อง implement:**
1. สุ่ม Index ของบทสนทนาในช่วง `0` ถึง `npc1Dialogues.Length - 1` โดยใช้ `Random.Range(0, npc1Dialogues.Length)`
2. แสดงผลข้อความบทสนทนานั้นออกมาทาง Console

**💡 แนวทางการเขียนโค้ด (Guideline):**
- สุ่มตัวเลข Index โดยระบุช่วงตั้งแต่ 0 ถึงความยาวของ Array (`npc1Dialogues.Length`)
- นำ Index ที่ได้ไปดึง String ออกจาก Array แล้วส่งเข้าคำสั่ง `Debug.Log` โดยตรง

**Game Context:** ระบบ NPC ประชาชนพูดคุยแบบสุ่มเมื่อผู้เล่นเดินผ่าน

---

### 13. Lv04_AttackTarget (6 test cases)

**วัตถุประสงค์:** การแก้ไขค่าข้อมูลใน Array ผ่าน Index เพื่อจำลองการลด HP ของศัตรู

**Method Signature:**
```csharp
void Lv04_AttackTarget(int[] enemyHP, int damage, int target)
```

**Logic ที่ต้อง implement:**
- ลดเลือดศัตรูตัวแรก (index 0) ด้วย `damage` แล้วพิมพ์: `FirstEnemy hp : <hp ที่เหลือ>`
- ลดเลือดศัตรูตัวสุดท้าย (index `enemyHP.Length - 1`) ด้วย `damage` แล้วพิมพ์: `LastEnemy hp : <hp ที่เหลือ>`
- ลดเลือดศัตรูเป้าหมาย (index `target`) ด้วย `damage` แล้วพิมพ์: `TargetEnemy <target> hp : <hp ที่เหลือ>`
- *(ลำดับการทำงานต้องเป็น ตัวแรก ➔ ตัวสุดท้าย ➔ ตัวเป้าหมาย)*

**💡 แนวทางการเขียนโค้ด (Guideline):**
- หาตำแหน่งตัวสุดท้ายไว้ในตัวแปร เช่น `int last = enemyHP.Length - 1;`
- ปรับลดค่าเลือดใน Array โดยตรงด้วยเครื่องหมาย `-=` เช่น `enemyHP[0] -= damage;`
- พิมพ์ผลลัพธ์ทันทีหลังจากลดเลือดในแต่ละตำแหน่ง โดยระวังการเว้นวรรค `"FirstEnemy hp : "`

**ตัวอย่าง Input/Output:**
- Input: `enemyHP = [100, 80, 60, 40], damage = 10, target = 2`
  ```
  FirstEnemy hp : 90
  LastEnemy hp : 30
  TargetEnemy 2 hp : 50
  ```

**Game Context:** สกิลการโจมตีแบบ AoE (Area of Effect) ที่โดนตัวหน้าสุด ตัวหลังสุด และตัวที่เล็งไว้

---

### 14. Lv05_MultiplicationTable (6 test cases)

**วัตถุประสงค์:** ฝึกฝนการใช้ For Loop คำนวณสูตรคูณแม่ n ตั้งแต่ 1 ถึง 12

**Method Signature:**
```csharp
void Lv05_MultiplicationTable(int n)
```

**Logic ที่ต้อง implement:**
- วนลูป For ตั้งแต่ `i = 1` ถึง `12`
- แสดงผลในรูปแบบ: `<n> x <i> = <n * i>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- กำหนดให้ตัวนับรอบเริ่มจาก 1 (`int i = 1`) และสิ้นสุดที่ 12 (`i <= 12`)
- นำค่า `n` มาคูณกับ `i` ภายในลูป และจัดรูปแบบการแสดงผลด้วยเครื่องหมาย `x` และ `=` ให้ถูกต้อง

**ตัวอย่าง Input/Output:**
- Input: `n = 2`
  ```
  2 x 1 = 2
  2 x 2 = 4
  ...
  2 x 12 = 24
  ```

**Game Context:** การคำนวณอัตราความเสียหายตามระดับเลเวล (Level Multiplier)

---

### 15. Lv06_ForLoopReverse (4 test cases)

**วัตถุประสงค์:** ฝึกการวนลูป For แบบย้อนกลับ (Reverse Loop) จาก Index สุดท้ายมายัง Index 0

**Method Signature:**
```csharp
void Lv06_ForLoopReverse(string[] suiteNames)
```

**Logic ที่ต้อง implement:**
1. แสดงหัวข้อ: `======Log Reverse======`
2. วนลูป For เริ่มจาก `i = suiteNames.Length - 1` ถอยหลังลงมาจนถึง `i >= 0` ด้วย `i--`
3. แสดงสมาชิกใน Array ออกมาทีละบรรทัด

**💡 แนวทางการเขียนโค้ด (Guideline):**
- ค่าเริ่มต้นของตัวนับรอบต้องเริ่มที่ตำแหน่งสุดท้ายคือ `suiteNames.Length - 1`
- เงื่อนไขการวนลูปคือต้องยังมากกว่าหรือเท่ากับ 0 (`i >= 0`) เพื่อให้วนถึงสมาชิกตัวแรกที่ index 0
- ส่วนการปรับค่าให้ใช้ `i--` เพื่อลดค่าลงทีละ 1 ในทุกรอบ

**ตัวอย่าง Input/Output:**
- Input: `["Mark I", "Mark II", "Mark III"]`
  ```
  ======Log Reverse======
  Mark III
  Mark II
  Mark I
  ```

**Game Context:** การแสดงผลประวัติการแชทย้อนหลัง (Chat Log) หรือระบบ Replay ย้อนเวลา

---

### 16. Lv07_FindHighestScore (5 test cases)

**วัตถุประสงค์:** ฝึกการใช้ For Loop ร่วมกับคำสั่งเงื่อนไข `if` เพื่อค้นหาค่าที่มากที่สุด (Maximum Value) ใน Array

**Method Signature:**
```csharp
void Lv07_FindHighestScore(int[] scores)
```

**Logic ที่ต้อง implement:**
1. ตรวจสอบหาก Array `scores` ว่าง ให้หยุดการทำงาน
2. สร้างตัวแปรเก็บค่าสูงสุดเริ่มต้น `int highest = scores[0];`
3. วนลูป For ตั้งแต่ index `1` ถึงตัวสุดท้าย (`scores.Length - 1`)
4. ในแต่ละรอบ หากพบว่า `scores[i] > highest` ให้ปรับค่า `highest = scores[i];`
5. เมื่อวนลูปครบทุกตัว ให้แสดงผลลัพธ์: `Highest score : <highest>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- กำหนดค่าเริ่มต้นให้กับตัวแปร `highest` ด้วยสมาชิกตัวแรกของ Array (`scores[0]`) ก่อนเริ่มลูป
- กำหนดให้ตัวนับรอบเริ่มจาก `i = 1` เพราะได้นำตัวแรกที่ index `0` ไปเป็นค่าตั้งต้นแล้ว
- ใช้คำสั่ง `if (scores[i] > highest)` ในการเปรียบเทียบและอัปเดตค่าสูงสุด
- สั่งพิมพ์ผลลัพธ์ด้วย `Debug.Log` ภายนอกลูปหลังจากเปรียบเทียบครบทุกตัวแล้ว

**ตัวอย่าง Input/Output:**
- Input: `scores = [10, 50, 30, 90, 40]`
  ```
  Highest score : 90
  ```
- Input: `scores = [-10, -50, -5, -20]`
  ```
  Highest score : -5
  ```

**Game Context:** ระบบค้นหาผู้เล่นที่ทำคะแนนสูงสุดประจำแมตช์ (MVP / High Score) หรือการค้นหาอาวุธที่สร้างดาเมจได้สูงสุด

---

### 17. Lv08_CalculateTotalScore (5 test cases)

**วัตถุประสงค์:** ฝึกการใช้ For Loop วนลูปผ่าน Array เพื่อคำนวณผลรวมของคะแนนทั้งหมด (Array Accumulation)

**Method Signature:**
```csharp
void Lv08_CalculateTotalScore(int[] scores)
```

**Logic ที่ต้อง implement:**
1. ประกาศตัวแปรสะสมผลรวม `int total = 0;`
2. วนลูป For ตั้งแต่ `i = 0` จนถึง `scores.Length - 1`
3. นำค่าคะแนนในแต่ละช่อง `scores[i]` มาบวกสะสมเข้าใน `total`
4. แสดงผลลัพธ์: `Total score : <total>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- ประกาศตัวแปรผลรวม `int total = 0;` ไว้นอกลูป For เสมอ
- ใช้ For Loop ปกติวนตั้งแต่ `i = 0` ถึง `i < scores.Length`
- ภายในลูป ใช้เครื่องหมาย `+=` เพื่อนำค่า `scores[i]` มาบวกสะสม
- สั่งพิมพ์ผลลัพธ์ผ่าน `Debug.Log` นอกลูปหลังจากบวกครบทุกช่องแล้ว ระวังการเว้นวรรค `"Total score : "`

**ตัวอย่าง Input/Output:**
- Input: `scores = [10, 20, 30]`
  ```
  Total score : 60
  ```
- Input: `scores = [100, 200, 300, 400]`
  ```
  Total score : 1000
  ```

**Game Context:** การคำนวณคะแนนรวมของผู้เล่นหลังจบด่าน หรือการรวม EXP จากการทำเควสต์

---

### 18. Lv09_WhileLoopN (5 test cases)

**วัตถุประสงค์:** ฝึกการเขียน While Loop ควบคุมการวนซ้ำตามจำนวนรอบที่ระบุ

**Method Signature:**
```csharp
void Lv09_WhileLoopN(int n)
```

**Logic ที่ต้อง implement:**
- ประกาศตัวแปรนับรอบ `int i = 0;`
- วนลูป While ตราบใดที่ `i < n`
- แสดงผล `i` ออกมาทีละบรรทัด พร้อม `i++`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- กำหนดตัวแปรนับรอบ `int i = 0;` ไว้นอกลูป
- เงื่อนไขใน While คือ `i < n` ซึ่งถ้าค่า `n <= 0` ลูปจะไม่ทำงาน
- ต้องเพิ่มค่า `i++` ภายในลูปเสมอ

**ตัวอย่าง Input/Output:**
- Input: `n = 3`
  ```
  0
  1
  2
  ```

**Game Context:** การประมวลผล Queue ของคำสั่งตามจำนวนที่มีอยู่ในคิว

---

## 🟡 Level 2: Moderate (การบ้านระดับท้าทาย)

### 19. Ex01_HealTarget (6 test cases)

**วัตถุประสงค์:** การฟื้นฟูค่า HP ของศัตรูในตำแหน่งต่างๆ โดยมีระบบป้องกัน Overheal ด้วย `Mathf.Min`

**Method Signature:**
```csharp
void Ex01_HealTarget(int[] enemyHP, int heal, int target, int maxHP)
```

**Logic ที่ต้อง implement:**
- เพิ่ม HP ด้วยค่า `heal` ให้กับศัตรูตัวแรก (index 0), ตัวสุดท้าย (index `enemyHP.Length - 1`), และตัวเป้าหมาย (index `target`)
- **เงื่อนไขสำคัญ:** เลือดหลังฮีลต้องไม่เกิน `maxHP` (ใช้สูตร `enemyHP[index] = Mathf.Min(enemyHP[index] + heal, maxHP)`)
- แสดงผลลัพธ์ผ่าน Debug.Log ตามลำดับ:
  - `FirstEnemy hp : <hp หลังจาก heal>`
  - `LastEnemy hp : <hp หลังจาก heal>`
  - `TargetEnemy <target> hp : <hp หลังจาก heal>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- นำค่าเดิมมาบวกกับ `heal` ก่อน แล้วใช้ฟังก์ชัน `Mathf.Min(hp + heal, maxHP)` เพื่อเลือกค่าที่น้อยกว่า ถ้าบวกแล้วเกิน `maxHP` ตัวฟังก์ชันจะคืนค่า `maxHP` ออกมาให้ทันที
- ดำเนินการตามลำดับ 3 ตำแหน่ง: ตัวแรก `[0]`, ตัวท้าย `[last]`, และเป้าหมาย `[target]`

**ตัวอย่าง Input/Output:**
- Input: `enemyHP = [95, 80, 60, 40], heal = 10, target = 1, maxHP = 100`
  ```
  FirstEnemy hp : 100
  LastEnemy hp : 50
  TargetEnemy 1 hp : 90
  ```

**Game Context:** สกิลฟื้นฟูพลังชีวิตของ Paladin / Priest ที่ฮีลเพื่อนร่วมทีมและจำกัดไม่ให้เกิน Max HP

---

### 20. Ex02_DialogueInteraction (4 test cases)

**วัตถุประสงค์:** การประยุกต์ใช้ For Loop ร่วมกับคำสั่งเงื่อนไข `if` เพื่อแสดงบทสนทนาโต้ตอบแบบสลับคนเริ่มพูดตามรอบ (Alternating Turn) พร้อมหัวข้อระบุรอบ

**Method Signature:**
```csharp
void Ex02_DialogueInteraction(string[] npc1Dialogues, string[] npc2Dialogues)
```

**Logic ที่ต้อง implement:**
1. หาจำนวนรอบที่สามารถโต้ตอบกันได้จากความยาวของทั้งสอง Array โดยเลือกค่าที่น้อยกว่า (เช่น ใช้ `Mathf.Min(npc1Dialogues.Length, npc2Dialogues.Length)`) เพื่อป้องกัน Index Out of Range หาก Array มีขนาดไม่เท่ากัน
2. วนลูป For ตั้งแต่รอบแรก (`i = 0`) จนถึงรอบสุดท้ายตามจำนวนรอบที่หาได้
3. ในแต่ละรอบ ให้ดำเนินการดังนี้:
   - แสดงหัวข้อระบุรอบ: `[Round <i + 1>]` (เช่น รอบแรก `i = 0` จะแสดง `[Round 1]`)
   - ตรวจสอบเงื่อนไขว่ารอบนี้ใครเริ่มพูดก่อน (ใช้ modulo `i % 2 == 0`):
     - **ถ้ารอบคู่ (`i = 0, 2, ...`)**: NPC1 เป็นฝ่ายเริ่มพูดก่อน แล้วตามด้วย NPC2 ตอบ
       - `NPC1 : <ข้อความของ npc1Dialogues[i]>`
       - `NPC2 : <ข้อความของ npc2Dialogues[i]>`
     - **ถ้ารอบคี่ (`i = 1, 3, ...`)**: NPC2 เป็นฝ่ายเริ่มพูดก่อน แล้วตามด้วย NPC1 ตอบ
       - `NPC2 : <ข้อความของ npc2Dialogues[i]>`
       - `NPC1 : <ข้อความของ npc1Dialogues[i]>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- ใช้ `Mathf.Min(npc1Dialogues.Length, npc2Dialogues.Length)` เพื่อหาขอบเขตจำนวนรอบ (Rounds) ที่ปลอดภัย
- สร้าง For Loop วนตั้งแต่ `i = 0` จนถึง `i < rounds`
- ในแต่ละรอบ พิมพ์หัวข้อ `"[Round " + (i + 1) + "]"` ก่อน
- ใช้คำสั่ง `if (i % 2 == 0)` เพื่อแยกว่ารอบนั้น NPC1 พูดก่อน (`NPC1` แล้ว `NPC2`) หรือ NPC2 พูดก่อน (`NPC2` แล้ว `NPC1`)

**ตัวอย่าง Output:**
```
[Round 1]
NPC1 : Nice weather today, isn't it?
NPC2 : Yes, it's a great day for an adventure!
[Round 2]
NPC2 : Are you ready to explore the cave?
NPC1 : Yes, I heard there are monsters inside!
[Round 3]
NPC1 : Welcome, traveler!
NPC2 : Thank you, good to see you!
[Round 4]
NPC2 : Have you seen my cat?
NPC1 : No, I haven't seen any cats around.
```

**Game Context:** บทสนทนา Ambient Dialogue ระหว่าง NPC ในเมืองที่มีการผลัดกันเปิดประเด็นคุยอย่างเป็นธรรมชาติในฉาก Cutscene

---

### 21. Ex03_SpawnEnemiesWithSpacing (4 test cases)

**วัตถุประสงค์:** การใช้วงลูปสร้างวัตถุจำนวนมากพร้อมคำนวณตำแหน่งแบบกระจายตัวตามระยะห่าง (Spacing)

**Method Signature:**
```csharp
void Ex03_SpawnEnemiesWithSpacing(GameObject Enemy, int count, float spacing)
```

**Logic ที่ต้อง implement:**
1. วนลูป For จำนวน `count` รอบ (ตั้งแต่ `i = 0` ถึง `count - 1`)
2. คำนวณตำแหน่งพิกัดแกน X ด้วยสูตร: `posX = (i + 1) * spacing`
3. สั่ง `GameObject spawned = Instantiate(Enemy);`
4. กำหนดตำแหน่ง `spawned.transform.position = new Vector3(posX, 0f, 0f);`
5. แสดงผลข้อความ: `Spawn enemy at position x : <posX>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- ใช้ For Loop เริ่มที่ `i = 0` จนถึง `i < count`
- คำนวณระยะห่างด้วยสูตร `float posX = (i + 1) * spacing;` เพื่อให้ตัวแรกอยู่ที่ตำแหน่ง `1 * spacing`
- เรียก `Instantiate(Enemy)` แล้วนำผลลัพธ์ไปกำหนดพิกัด X ผ่าน `new Vector3(posX, 0f, 0f)`

**ตัวอย่าง Input/Output:**
- Input: `count = 3, spacing = 2f`
  ```
  Spawn enemy at position x : 2
  Spawn enemy at position x : 4
  Spawn enemy at position x : 6
  ```

**Game Context:** ระบบ Procedural Generation ในการจัดเรียงไอเทม สิ่งกีดขวาง หรือกองทหาร

---

### 22. Ex04_FindItemOrBreak (5 test cases)

**วัตถุประสงค์:** การประยุกต์ใช้คำสั่ง `break` เพื่อหยุดการค้นหาข้อมูลใน Array ทันทีเมื่อพบเป้าหมาย (Early Exit Search Pattern)

**Method Signature:**
```csharp
void Ex04_FindItemOrBreak(string[] inventory, string targetItem)
```

**Logic ที่ต้อง implement:**
1. สร้างตัวแปร boolean เช่น `bool found = false;` เพื่อบันทึกสถานะการพบไอเทม
2. วนลูป For ตรวจสอบสมาชิกใน `inventory` ทีละช่อง (index `0` ถึง `inventory.Length - 1`)
3. ถ้าพบว่าช่องใดตรงกับ `targetItem` (`inventory[i] == targetItem`):
   - แสดงผล: `Found <targetItem> at slot <i>`
   - ปรับค่า `found = true;`
   - ใช้คำสั่ง `break;` เพื่อหยุดการทำงานของลูปทันที (ไม่ตรวจสอบช่องที่เหลือ)
4. เมื่อจบลูปแล้ว หากตรวจสอบพบว่าไม่เจอไอเทม (`!found`):
   - แสดงผล: `Item <targetItem> not found`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- การค้นหาแบบ Early Exit ช่วยให้โปรแกรมไม่ต้องประมวลผลต่อเมื่อได้ผลลัพธ์ที่ต้องการแล้ว
- กำหนด `bool found = false;` ไว้นอกลูป และเมื่อเจอของให้เปลี่ยนเป็น `true` ก่อนสั่ง `break;`
- เช็คเงื่อนไข `if (!found)` หลังจบลูปเพื่อจัดการกรณีที่ไม่มีไอเทมชิ้นนั้นในกระเป๋า

**ตัวอย่าง Input/Output:**
- Input: `inventory = ["Potion", "Shield", "Key", "Herb", "Key"], targetItem = "Key"`
  ```
  Found Key at slot 2
  ```
  *(สังเกตว่าลูปจะหยุดที่ช่อง 2 ทันทีและไม่ประมวลผลไปถึงช่อง 4)*
- Input: `inventory = ["Potion", "Sword", "Shield"], targetItem = "Axe"`
  ```
  Item Axe not found
  ```

**Game Context:** ระบบค้นหาไอเทมเควสต์ในกระเป๋าเป้ของผู้เล่น เมื่อพบไอเทมชิ้นแรกที่ตรงกันจะหยุดค้นหาทันทีเพื่อประหยัดทรัพยากรการประมวลผล

---

### 23. Ex05_SkipDefeatedEnemies (4 test cases)

**วัตถุประสงค์:** การประยุกต์ใช้คำสั่ง `continue` เพื่อข้ามการประมวลผลสมาชิกที่ไม่ตรงเงื่อนไข (Filtering Pattern)

**Method Signature:**
```csharp
void Ex05_SkipDefeatedEnemies(int[] enemyHPs)
```

**Logic ที่ต้อง implement:**
1. วนลูป For ตรวจสอบค่า HP ของศัตรูทีละตัวใน Array `enemyHPs` (index `0` ถึง `enemyHPs.Length - 1`)
2. ในแต่ละรอบ ให้ตรวจสอบว่าศัตรูตัวนั้นพ่ายแพ้ไปแล้วหรือไม่ (`enemyHPs[i] <= 0`):
   - หาก `enemyHPs[i] <= 0` ให้ใช้คำสั่ง `continue;` เพื่อข้ามการทำงานที่เหลือในรอบนี้ และเริ่มรอบถัดไปทันที
3. หากศัตรูยังมีชีวิตอยู่ (`HP > 0`) ให้แสดงผล:
   - `Enemy <i> HP : <enemyHPs[i]>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- คำสั่ง `continue` จะสั่งให้ลูปกระโดดข้ามคำสั่งที่อยู่ด้านล่างทั้งหมดในรอบนั้น แล้วไปเริ่มรอบถัดไป (เพิ่มค่า `i++`) ทันที
- ใช้เงื่อนไข `if (enemyHPs[i] <= 0)` ร่วมกับ `continue;` วางไว้เป็นคำสั่งแรกๆ ภายในลูป
- คำสั่ง `Debug.Log` จะถูกเรียกเฉพาะศัตรูที่รอดชีวิตเท่านั้น

**ตัวอย่าง Input/Output:**
- Input: `enemyHPs = [100, 0, 50, -10, 80]`
  ```
  Enemy 0 HP : 100
  Enemy 2 HP : 50
  Enemy 4 HP : 80
  ```
  *(ช่อง index 1 ที่มี HP = 0 และ index 3 ที่มี HP = -10 จะถูกข้ามไป ไม่ถูกพิมพ์ออกมา)*

**Game Context:** ระบบการประมวลผลสกิลโจมตีหมู่ (AOE) หรือการจัดการเทิร์นของ AI ศัตรู โดยจะข้ามศัตรูที่ HP หมดไปแล้วในสนามรบ

---

### 24. Ex06_WhileLoopStep (4 test cases)

**วัตถุประสงค์:** การควบคุม Step การวนซ้ำใน While Loop เพื่อข้ามสมาชิกใน Array

**Method Signature:**
```csharp
void Ex06_WhileLoopStep(string[] suiteNames)
```

**Logic ที่ต้อง implement:**
1. พิมพ์ `======Log by One======`
2. วนลูป While แสดงสมาชิกทั้งหมดทีละ 1 ตัว (`i++`)
3. พิมพ์ `======Log by Two======`
4. วนลูป While แสดงสมาชิกข้ามทีละ 2 ตัว (`i += 2`)

**💡 แนวทางการเขียนโค้ด (Guideline):**
- แบ่งการทำงานเป็น 2 ส่วนชัดเจน โดยแสดงหัวข้อก่อนเริ่มลูปของแต่ละส่วน
- ในส่วนแรก ให้ใช้ `i++` เพื่อขยับทีละช่อง
- อย่าลืมรีเซ็ตค่าตัวแปรนับรอบให้กลับเป็น `0` ก่อนเริ่มลูปชุดที่สอง
- ในส่วนที่สอง ให้ใช้ `i += 2` เพื่อข้ามทีละ 2 ช่อง

**ตัวอย่าง Input/Output:**
- Input: `["A", "B", "C", "D"]`
  ```
  ======Log by One======
  A
  B
  C
  D
  ======Log by Two======
  A
  C
  ```

**Game Context:** การวนลูปดึงข้อมูลเฉพาะบางประเภท หรือการข้าม Frame Animation

---

### 25. Ex07_WhileLoopSum (5 test cases)

**วัตถุประสงค์:** การใช้ While Loop ในการคำนวณผลรวมสะสมทางคณิตศาสตร์ (Accumulative Sum)

**Method Signature:**
```csharp
void Ex07_WhileLoopSum(int n)
```

**Logic ที่ต้อง implement:**
1. กำหนดตัวแปร `int i = 1;` และ `int sum = 0;`
2. วนลูป While ตราบใดที่ `i <= n` นำค่า `i` มาบวกสะสมเข้าใน `sum` แล้วเพิ่ม `i++`
3. เมื่อลูปเสร็จสิ้น ให้แสดงผล: `Sum of n from 0 to <n> is <sum>`

**💡 แนวทางการเขียนโค้ด (Guideline):**
- สร้างตัวแปรสะสมผลรวม `int sum = 0;` และตัวนับ `int i = 1;` ไว้นอกลูป While
- ภายในลูป ใช้คำสั่ง `sum += i;` และ `i++;` เพื่อสะสมค่าจาก 1 ไปเรื่อยๆ จนถึง `n`
- สั่งแสดงผลด้วย `Debug.Log` ภายนอกลูปหลังจากคำนวณเสร็จสิ้นแล้วเท่านั้น

**ตัวอย่าง Input/Output:**
- Input: `n = 5`
  ```
  Sum of n from 0 to 5 is 15
  ```
- Input: `n = 10`
  ```
  Sum of n from 0 to 10 is 55
  ```

**Game Context:** การคำนวณค่าประสบการณ์รวม (Total EXP Required) สำหรับการอัปเลเวลในเกม

---

## 💡 ข้อควรระวังและ Best Practices

1. **Zero-based Indexing:** สมาชิกตัวแรกของ Array จะอยู่ที่ Index `0` เสมอ และสมาชิกตัวสุดท้ายจะอยู่ที่ Index `Array.Length - 1`
2. **IndexOutOfRangeException:** ห้ามเข้าถึง Index ที่น้อยกว่า 0 หรือมากกว่า/เท่ากับ `Array.Length`
3. **รูปแบบ String และการเว้นวรรค:** สังเกตช่องว่างรอบเครื่องหมายโคลอน `" : "` ให้ถูกต้อง เช่น `"FirstEnemy hp : " + hp`
4. **การป้องกัน Infinite Loop ใน While:** ต้องมั่นใจเสมอว่ามีการปรับค่าตัวแปรเงื่อนไข (เช่น `i++` หรือ `i += 2`) ในทุกรอบของ While Loop
5. **การใช้ Mathf.Min:** ใช้ `Mathf.Min(currentVal, maxVal)` เพื่อควบคุมไม่ให้ค่าเกินเพดานที่กำหนดอย่างมีประสิทธิภาพ

---
**ขอให้สนุกกับการเขียนโค้ดและสร้างสรรค์เกม! 🎮👨‍💻**
