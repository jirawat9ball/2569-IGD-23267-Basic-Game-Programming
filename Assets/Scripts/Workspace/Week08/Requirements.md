# 📋 ข้อกำหนดโจทย์ข้อสอบปฏิบัติ (Midterm Practical Exam Requirements) - Week 08
## วิชา: Basic Game Programming (การเขียนโปรแกรมเกมเบื้องต้น)

---

## 📌 สรุปโครงสร้างข้อสอบและเกณฑ์คะแนน

ข้อสอบชุดนี้แบ่งออกเป็น **3 ระดับ (Levels)** รวม **11 ข้อทดสอบ (11 คะแนนเต็ม)** 
ทุกคลาสหลักในสัปดาห์นี้จะ**สืบทอดจาก `MonoBehaviour`** เพื่อรองรับการทำงานบน GameObject ใน Unity Scene และการตรวจข้อสอบผ่าน Unity Test Runner

```
[Level 1: 4 คะแนน] CharacterStatus.cs (ตัวแปรสถานะ, เมธอด void, เมธอด return: IsAlive และ CalculateDamage แยกเป็น 2 ข้อ)
[Level 2: 2 คะแนน] Custom Class & Prefab (สร้างคลาส Armor และประกอบชิ้นงาน Prefab Iron Armor ใน Unity)
[Level 3: 5 คะแนน] MapGenerator.cs (ระบบตาราง 2 มิติ, PlaceExit, PlaceArmor, GenerateLWall, ScanThreats, และคอมเมนต์ปิด SpawnItemPotions)
```

---

## 🟢 ระดับ 1: ข้อมูลสถานะตัวละครและการคำนวณดาเมจ (Status & Elemental Combat)
- **คะแนนเต็ม:** 4 คะแนน (4 Test Cases)
- **ไฟล์เป้าหมาย:** `Assets/Scripts/Workspace/Week08/CharacterStatus.cs`
- **Namespace:** `Week08.Ex01`
- **คลาส:** `public class CharacterStatus : MonoBehaviour`

### 1.1 การประกาศตัวแปรประจำคลาส (Fields - `Lv1_01`)
ให้นักศึกษาประกาศตัวแปรแบบ `public` ทั้งหมด 6 ตัวแปร พร้อมกำหนดค่าเริ่มต้นดังตาราง:

| ชื่อตัวแปร | ชนิดข้อมูล (Type) | ระดับการเข้าถึง | ค่าเริ่มต้น | หน้าที่ / คำอธิบาย |
| :---| :---: | :---: | :---: | :---|
| `characterName` | `string` | `public` | `"Hero"` | ชื่อของตัวละคร |
| `maxHealth` | `int` | `public` | `100` | ค่าพลังชีวิตสูงสุด |
| `currentHealth` | `int` | `public` | `100` | ค่าพลังชีวิตปัจจุบัน |
| `isAlive` | `bool` | `public` | `true` | สถานะการมีชีวิต (`true` = ยังมีชีวิต, `false` = ตาย) |
| `attackPoint` | `int` | `public` | `10` | ค่าพลังโจมตีพื้นฐาน |
| `defensePoint` | `int` | `public` | `0` | ค่าพลังป้องกันพื้นฐาน |

### 1.2 เมธอดไม่คืนค่า: `TakeDamage(int damage)` (`Lv1_02`)
- **Signature:** `public void TakeDamage(int damage)`
- **วัตถุประสงค์:** ลดค่าพลังชีวิตปัจจุบันของตัวละครตามค่าดาเมจที่ได้รับเข้ามา
- **ข้อกำหนดและเงื่อนไข:**
  1. นำค่า `damage` ไปหักออกจาก `currentHealth`
  2. **กรณีเลือดหมด (`currentHealth <= 0`):**
     - กำหนด `currentHealth = 0` (ป้องกันเลือดติดลบ)
     - ปรับสถานะ `isAlive = false`
     - แสดงข้อความในคอนโซล:
       ```csharp
       Debug.Log($"{characterName} is defeated!");
       ```
  3. **กรณีเลือดยังเหลือ (`currentHealth > 0`):**
     - แสดงข้อความในคอนโซล:
       ```csharp
       Debug.Log($"{characterName} took {damage} damage. Current HP: {currentHealth}");
       ```

### 1.3 เมธอดคืนค่าสถานะ: `IsAlive()` (`Lv1_03`)
- **Signature:** `public bool IsAlive()`
- **วัตถุประสงค์:** ตรวจสอบและรายงานว่าตัวละครยังมีชีวิตอยู่หรือไม่
- **ข้อกำหนดและเงื่อนไข:**
  - คืนค่า `true` หาก `currentHealth > 0`
  - คืนค่า `false` หาก `currentHealth <= 0`

### 1.4 เมธอดคืนค่าการคำนวณ: `CalculateDamage(int rawDamage, int defense)` (`Lv1_04`)
- **Signature:** `public int CalculateDamage(int rawDamage, int defense)`
- **วัตถุประสงค์:** คำนวณดาเมจสุทธิหลังหักลบด้วยพลังป้องกัน
- **ข้อกำหนดและเงื่อนไข:**
  1. คำนวณดาเมจสุทธิ: `int netDamage = rawDamage - defense;`
  2. หากดาเมจสุทธิน้อยกว่าศูนย์ (`netDamage < 0`) ให้ปรับเป็นศูนย์ `netDamage = 0` (ดาเมจสุทธิต้องไม่ติดลบ)
  3. แสดงข้อความในคอนโซล:
     ```csharp
     Debug.Log($"Damage calculated: {netDamage}");
     ```
  4. คืนค่า `netDamage` ที่คำนวณได้ (`return netDamage;`)

---

## 🟡 ระดับ 2: การสร้างคลาสเกราะ และประกอบชิ้นงาน Prefab (Custom Class & Prefab)
- **คะแนนเต็ม:** 3 คะแนน (3 Test Cases)
- **ไฟล์เป้าหมาย:** สร้างไฟล์สคริปต์ `Armor.cs` ในโฟลเดอร์ `Assets/Scripts/Workspace/Week08/`
- **คำแนะนำเกี่ยวกับ Namespace:** ด้านบนสุดของไฟล์ให้เพิ่มคำสั่ง `using Week08;` เพื่อเรียกใช้งานคลาส `Player` (หรือครอบคลาสด้วย `namespace Week08`):
  ```csharp
  using UnityEngine;
  using Week08; // <-- เพิ่มบรรทัดนี้เพื่อเรียกใช้งานคลาส Player

  public class Armor : MonoBehaviour
  {
      // โค้ดของนักศึกษา
  }
  ```

### 2.1 คลาสและตัวแปรไอเทมเกราะ: `Armor` (`Lv2_01`)
ให้นักศึกษาเขียนประกาศคลาส `public class Armor : MonoBehaviour`:

1. **การประกาศตัวแปร (Fields):**
   - `public string Name = "Iron Armor";` (ชื่อไอเทมเกราะ ค่าเริ่มต้นต้องเป็น `"Iron Armor"`)
   - `public int defenseBonus = 10;` (ค่าพลังป้องกันที่เพิ่มให้ผู้เล่น ค่าเริ่มต้นต้องเป็น `10`)

### 2.2 เมธอดตรวจจับการชน: `OnTriggerEnter2D(Collider2D other)` (`Lv2_02`)
ให้นักศึกษาเขียนเมธอดตรวจจับการชนในคลาส `Armor`:

1. แสดงข้อความเมื่อเกิดการชน:
   ```csharp
   Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");
   ```
2. ตรวจสอบว่าวัตถุที่ชนมีคอมโพเนนต์ `Player` หรือไม่ ผ่าน `other.GetComponent<Player>()`
3. **เมื่อตรวจพบผู้เล่น (`player != null`):**
   - แสดงข้อความแจ้งเตือน:
     ```csharp
     Debug.Log($"Picked up {Name}! +{defenseBonus} Defense");
     ```
   - เพิ่มพลังป้องกันให้ตัวละครผู้เล่น:
     ```csharp
     player.IncreaseDefense(defenseBonus); // หรือ player.defensePoint += defenseBonus;
     ```
   - ทำลายไอเทมเกราะออกจากฉาก:
     ```csharp
     Destroy(gameObject);
     ```

### 2.3 การสร้าง Prefab `Iron Armor` ใน Unity Editor (`Lv2_03`)
ให้นักศึกษาประกอบชิ้นงาน Prefab ในโปรแกรม Unity ตามขั้นตอนดังนี้:
1. ในหน้าต่าง **Hierarchy** คลิกขวาเลือก **Create Empty** ตั้งชื่อ GameObject ว่า `Iron Armor`
2. เพิ่ม Component **Sprite Renderer**:
   - เลือก Sprite รูปเกราะจากโฟลเดอร์: `Assets/Sprite/Cainos/Pixel Art Icon Pack - RPG/Texture/Equipment/Iron Armor.png`
3. เพิ่ม Component **Box Collider 2D**:
   - **ติ๊กเครื่องหมายถูกที่ช่อง `Is Trigger`**
4. แนบคอมโพเนนต์สคริปต์ `Armor` (เลือกคลาส `Armor` ที่นักศึกษาเขียนขึ้นมา)
   - ตรวจสอบว่าใน Inspector แสดงค่า `Name = "Iron Armor"` และ `defenseBonus = 10`
5. ลาก GameObject `Iron Armor` จาก Hierarchy ไปปล่อยไว้ในโฟลเดอร์:
   ```
   Assets/Prefabs/Exam/Iron Armor.prefab
   ```
   *(หากยังไม่มีโฟลเดอร์ Exam สามารถสร้างโฟลเดอร์ใหม่ใต้ Assets/Prefabs/ ได้)*

---

## 🟠 ระดับ 3: การสร้างฉากและจัดวางตำแหน่งออบเจกต์ (MapGenerator Object Placement)
- **คะแนนเต็ม:** 5 คะแนน (5 Test Cases)
- **ไฟล์เป้าหมาย:** `Assets/Scripts/Workspace/Week08/MapGenerator.cs`
- **Namespace:** `Week08`
- **คลาส:** `public class MapGenerator : MonoBehaviour`

### 📌 ความหมายของรหัสบนตาราง 2 มิติ (`mapGrid[rows, columns]`)
| รหัส (Code) | สัญลักษณ์ / ความหมาย | บทบาทในเกม |
| :---: | :---| :---|
| `0` | **Empty** | พื้นที่ว่างสำหรับเดิน |
| `1` | **Wall** | กำแพงภายในฉาก / กำแพงรูปตัว L |
| `2` | **Threat / Enemy** | ภัยคุกคาม / หนาม / ศัตรู |
| `3` | **Armor / Item** | ไอเทมเกราะที่วางบนพื้น |
| `9` | **Exit** | จุดหมายหรือประตูทางออก |

### 3.1 เมธอดกำหนดตำแหน่งทางออก: `PlaceExit` (`Lv3_01`)
- **Signature:**
  ```csharp
  public virtual void PlaceExit()
  ```
- **ข้อกำหนดและเงื่อนไข:**
  1. หาก `exitPrefab != null` ให้เสกสร้างวัตถุทางออกด้วยคำสั่ง:
     ```csharp
     var exitObj = Instantiate(exitPrefab, new Vector3(exitPos.y, exitPos.x, 0), Quaternion.identity);
     exitObj.name = "Exit";
     ```

### 3.2 เมธอดวางไอเทมเกราะลงในฉาก: `PlaceArmor` (`Lv3_02`)
- **Signature:**
  ```csharp
  public virtual void PlaceArmor()
  ```
- **ข้อกำหนดและเงื่อนไข:**
  1. หาก `armorPrefab == null` ให้หยุดการทำงาน (`return`)
  2. วนลูปจำนวน `armorCount` รอบ สุ่มพิกัดแถวและคอลัมน์ (`int X = Random.Range(0, rows); int Y = Random.Range(0, columns);`)
  3. เสกสร้างวัตถุเกราะด้วย `Instantiate(armorPrefab, new Vector3(Y, X, 0), Quaternion.identity, itemParent)`
  4. ตั้งชื่อออบเจกต์เป็น `$"Armor_{X}_{Y}"`

### 3.3 เมธอดสร้างกำแพงรูปตัว L: `GenerateLWall` (`Lv3_03`)
- **Signature:**
  ```csharp
  public virtual void GenerateLWall()
  ```
- **ข้อกำหนดและเงื่อนไข:**
  1. คำนวณความสูงและกว้างของแนวกำแพง:
     ```csharp
     int wallHeight = rows / 2;
     int wallWidth = columns / 2;
     var wallPrefab = wallPrefabs[0];
     ```
  2. วนลูปแนวดิ่งตามแนวแถว `r` จาก `0` ถึง `wallHeight` (ที่คอลัมน์ `wallWidth`): เสกวัตถุกำแพงด้วย:
     ```csharp
     Instantiate(wallPrefab, new Vector3(wallWidth, r, 0), Quaternion.identity, wallParent);
     ```
  3. วนลูปแนวนอนตามแนวคอลัมน์ `c` จาก `0` ถึง `wallWidth` (ที่แถว `wallHeight`): เสกวัตถุกำแพงด้วย:
     ```csharp
     Instantiate(wallPrefab, new Vector3(c, wallHeight, 0), Quaternion.identity, wallParent);
     ```

### 3.4 เมธอดเรดาร์ตรวจจับภัยคุกคามรอบตัว: `ScanThreats` (`Lv3_04`)
- **Signatures:**
  ```csharp
  public virtual int ScanThreats(int[,] grid, Vector2Int playerPos, int threatCode = 2)
  public int ScanThreats(Vector2Int playerPos, int threatCode = 2)
  ```
- **ข้อกำหนดและเงื่อนไข:**
  1. ตรวจสอบพื้นที่รอบตัวผู้เล่นในรัศมี 1 ช่อง (พื้นที่ 3x3 หรือ 8 ทิศทางรอบพิกัด `playerPos`)
  2. **การตรวจสอบขอบเขต (Boundary Guard):** ระบบเตรียมฟังก์ชันช่วย `IsInBounds(grid, r, c)` และโครงสร้างลูป 3x3 ไว้ให้ในโค้ดแล้ว
  3. **ข้ามช่องที่ผู้เล่นยืนอยู่:** ไม่นับช่องที่พิกัดตรงกับผู้เล่น (`r == playerPos.x && c == playerPos.y`)
  4. หากช่องรอบตัวช่องใดมีค่าเท่ากับ `threatCode` (ค่าเริ่มต้นคือ 2) ให้นับจำนวนเพิ่มขึ้น 1
  5. แสดงข้อความในคอนโซล:
     ```csharp
     Debug.Log($"Found {count} threats around player at {playerPos}");
     ```
  6. คืนค่าจำนวนภัยคุกคามที่นับได้ทั้งหมด

### 3.5 ปิดการเสกไอเทมยาฟื้นพลัง: คอมเมนต์ปิดการเรียกใช้ `SpawnItemPotions()` (`Lv3_05`)
- **ไฟล์เป้าหมาย:** `Assets/Scripts/Workspace/Week08/MapGenerator.cs` ในเมธอด `GenerateMap()`
- **วัตถุประสงค์:** ปิดการเสกไอเทมยาฟื้นพลัง (`Health Potion`) ไม่ให้ทำงานเมื่อเริ่มสร้างแผนที่
- **ข้อกำหนดและเงื่อนไข:**
  1. ในเมธอด `GenerateMap()` ของ `MapGenerator.cs` จะมีคำสั่งเรียกใช้งาน:
     ```csharp
     // [Level 3 - ข้อ 5] ให้นักศึกษาคอมเมนต์ปิดบรรทัดด้านล่างนี้ เพื่อปิดการเสกยาลงในฉาก:
     SpawnItemPotions();
     ```
  2. ให้นักศึกษาทำการใส่เครื่องหมายคอมเมนต์ `//` ปิดบรรทัดดังกล่าว:
     ```csharp
     // SpawnItemPotions();
     ```
  3. เมื่อเรียกใช้ `GenerateMap()` จะต้องไม่มีไอเทม Blue Potion หรือ Red Potion ถูกสร้างขึ้นมาในฉาก

---

## 📊 ตารางสรุป 12 Test Cases (การให้คะแนน)

| หมวดหมู่ | ชื่อ Test Case | เกณฑ์การตรวจ | คะแนน |
| :---: | :---| :---| :---: |
| **Level 1** | `Lv1_01_CharacterStatus_FieldsExistAndDefaultValues` | ตรวจสอบตัวแปรทั้ง 6 ตัวและค่าเริ่มต้นใน `CharacterStatus` | 1 คะแนน |
| **Level 1** | `Lv1_02_TakeDamage_VoidMethod` | ตรวจสอบการลด HP, การ clamp เลือดไม่ให้ติดลบ, และการปรับ `isAlive` | 1 คะแนน |
| **Level 1** | `Lv1_03_IsAlive_ReturnMethod` | ตรวจสอบการคืนค่าสถานะ `IsAlive()` (`true`/`false`) | 1 คะแนน |
| **Level 1** | `Lv1_04_CalculateDamage_ReturnMethod` | ตรวจสอบการคำนวณดาเมจสุทธิ `CalculateDamage(rawDamage, defense)` ไม่ติดลบ | 1 คะแนน |
| **Level 2** | `Lv2_01_Armor_Fields` | ตรวจสอบคลาส `Armor` และฟิลด์ `Name`, `defenseBonus` พร้อมค่าเริ่มต้น | 1 คะแนน |
| **Level 2** | `Lv2_02_Armor_OnTriggerEnter2D` | ตรวจสอบเมธอด `OnTriggerEnter2D` เมื่อชน Player เพิ่ม Defense และ Destroy ตนเอง | 1 คะแนน |
| **Level 2** | `Lv2_03_Armor_Prefab_Configuration` | ตรวจสอบการประกอบ Prefab `Iron Armor.prefab` (Sprite, BoxCollider2D IsTrigger, สคริปต์ Armor) | 1 คะแนน |
| **Level 3** | `Lv3_01_MapGenerator_PlaceExit` | ตรวจสอบการเสกสร้าง Exit Prefab ที่ตำแหน่ง `exitPos` | 1 คะแนน |
| **Level 3** | `Lv3_02_MapGenerator_PlaceArmor` | ตรวจสอบการเสกสร้างไอเทมเกราะจำนวน `armorCount` ชิ้นลงใน `itemParent` | 1 คะแนน |
| **Level 3** | `Lv3_03_MapGenerator_GenerateLWall` | ตรวจสอบการสร้างกำแพงรูปตัว L สูง rows/2 และกว้าง cols/2 ลงใน `wallParent` | 1 คะแนน |
| **Level 3** | `Lv3_04_MapGenerator_ScanThreats` | ตรวจสอบเรดาร์ตรวจภัยคุกคามรอบตัว 8 ทิศทาง และการป้องกันขอบเขต | 1 คะแนน |
| **Level 3** | `Lv3_05_MapGenerator_DisableSpawnItemPotions` | ตรวจสอบการคอมเมนต์ปิด `SpawnItemPotions()` ใน `GenerateMap()` ไม่ให้มี Blue Potion หรือ Red Potion ในฉาก | 1 คะแนน |
| **รวม** | **12 Test Cases** | **ทดสอบผ่าน Unity Test Runner (EditMode)** | **12 คะแนน** |
