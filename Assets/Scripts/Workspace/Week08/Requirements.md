# 📋 ข้อกำหนดโจทย์ข้อสอบปฏิบัติ (Midterm Practical Exam Requirements) - Week 08
## วิชา: Basic Game Programming (การเขียนโปรแกรมเกมเบื้องต้น)

---

## 📌 สรุปโครงสร้างข้อสอบและเกณฑ์คะแนน

ข้อสอบชุดนี้แบ่งออกเป็น **3 ระดับ (Levels)** รวม **10 ข้อทดสอบ (10 คะแนนเต็ม)** 
ทุกคลาสหลักในสัปดาห์นี้จะ**สืบทอดจาก `MonoBehaviour`** เพื่อรองรับการทำงานบน GameObject ใน Unity Scene และการตรวจข้อสอบผ่าน Unity Test Runner

```
[Level 1: 3 คะแนน] CharacterStatus.cs (ตัวแปรสถานะ, เมธอด void, เมธอด return)
[Level 2: 4 คะแนน] MapGenerator.cs (ระบบตาราง 2 มิติ, การค้นหาไอเทม, การตรวจนับ, เรดาร์ตรวจภัยคุกคาม)
[Level 3: 3 คะแนน] Custom Class & Spawner (สร้างคลาส Armor, คลาส ArmorSpawner และ Prefab เกราะ)
```

---

## 🟢 ระดับ 1: ข้อมูลสถานะตัวละครและการคำนวณดาเมจ (Status & Elemental Combat)
- **คะแนนเต็ม:** 3 คะแนน (3 Test Cases)
- **ไฟล์เป้าหมาย:** `Assets/Scripts/Workspace/Week08/CharacterStatus.cs`
- **Namespace:** `Week08.Ex01`
- **คลาส:** `public class CharacterStatus : MonoBehaviour`

### 1.1 การประกาศตัวแปรประจำคลาส (Fields)
ให้นักศึกษาประกาศตัวแปรแบบ `public` ทั้งหมด 6 ตัวแปร พร้อมกำหนดค่าเริ่มต้นดังตาราง:

| ชื่อตัวแปร | ชนิดข้อมูล (Type) | ระดับการเข้าถึง | ค่าเริ่มต้น | หน้าที่ / คำอธิบาย |
| :---| :---: | :---: | :---: | :---|
| `characterName` | `string` | `public` | `"Hero"` | ชื่อของตัวละคร |
| `maxHealth` | `int` | `public` | `100` | ค่าพลังชีวิตสูงสุด |
| `currentHealth` | `int` | `public` | `100` | ค่าพลังชีวิตปัจจุบัน |
| `isAlive` | `bool` | `public` | `true` | สถานะการมีชีวิต (`true` = ยังมีชีวิต, `false` = ตาย) |
| `attackPoint` | `int` | `public` | `10` | ค่าพลังโจมตีพื้นฐาน |
| `defensePoint` | `int` | `public` | `0` | ค่าพลังป้องกันพื้นฐาน |

### 1.2 เมธอดไม่คืนค่า: `TakeDamage(int damage)`
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

### 1.3 เมธอดคืนค่าสถานะ: `IsAlive()`
- **Signature:** `public bool IsAlive()`
- **วัตถุประสงค์:** ตรวจสอบและรายงานว่าตัวละครยังมีชีวิตอยู่หรือไม่
- **ข้อกำหนดและเงื่อนไข:**
  - คืนค่า `true` หาก `currentHealth > 0`
  - คืนค่า `false` หาก `currentHealth <= 0`

### 1.4 เมธอดคืนค่าการคำนวณ: `CalculateDamage(int rawDamage, int defense)`
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

## 🟡 ระดับ 2: เรดาร์สำรวจและค้นหาข้อมูลบนตารางดันเจี้ยน (Dungeon Grid Radar)
- **คะแนนเต็ม:** 4 คะแนน (4 Test Cases)
- **ไฟล์เป้าหมาย:** `Assets/Scripts/Workspace/Week08/MapGenerator.cs`
- **Namespace:** `Week08`
- **คลาส:** `public class MapGenerator : MonoBehaviour`

### 📌 ความหมายของรหัสบนตาราง 2 มิติ (`mapGrid[rows, columns]`)
| รหัส (Code) | สัญลักษณ์ / ความหมาย | บทบาทในเกม |
| :---: | :---| :---|
| `0` | **Empty** | พื้นที่ว่างสำหรับเดิน |
| `1` | **Player** | ตำแหน่งเริ่มต้นของผู้เล่น |
| `2` | **Threat / Enemy** | ภัยคุกคาม / หนาม / ศัตรู |
| `3` | **Armor / Item** | ไอเทมเกราะที่วางบนพื้น |
| `9` | **Exit** | จุดหมายหรือประตูทางออก |

### 2.1 เมธอดค้นหาตำแหน่งไอเทม: `FindItemPosition`
- **Signatures:**
  ```csharp
  public Vector2Int FindItemPosition(int itemCode)
  public Vector2Int FindItemPosition(int[,] grid, int itemCode)
  ```
- **ข้อกำหนดและเงื่อนไข:**
  1. ค้นหาช่องแรกในตารางที่มีค่าเท่ากับ `itemCode` (เช่น ไอเทมรหัส 3)
  2. หากพบ ให้คืนค่าพิกัดเป็น `new Vector2Int(r, c)` ทันที (`r` คือ แถว/Row, `c` คือ คอลัมน์/Column)
  3. หากวนจนจบลูปแล้วไม่พบ หรือตารางเป็น `null` ให้คืนค่า `new Vector2Int(-1, -1)`

### 2.2 เมธอดตรวจนับจำนวนไอเทม: `CountItems`
- **Signatures:**
  ```csharp
  public int CountItems(int itemCode)
  public int CountItems(int[,] grid, int itemCode)
  ```
- **ข้อกำหนดและเงื่อนไข:**
  1. วนลูปอ่านข้อมูลทุกช่องในตาราง 2 มิติ
  2. นับจำนวนช่องทั้งหมดที่มีค่าเท่ากับ `itemCode`
  3. คืนค่าจำนวนไอเทมทั้งหมดที่นับได้ (หากไม่พบไอเทม ให้คืนค่า `0`)

### 2.3 เมธอดเรดาร์ตรวจจับภัยคุกคามรอบตัว: `ScanThreats`
- **Signatures:**
  ```csharp
  public int ScanThreats(Vector2Int playerPos, int threatCode = 2)
  public int ScanThreats(int[,] grid, Vector2Int playerPos, int threatCode = 2)
  ```
- **ข้อกำหนดและเงื่อนไข:**
  1. ตรวจสอบพื้นที่รอบตัวผู้เล่นในรัศมี 1 ช่อง (พื้นที่ 3x3 หรือ 8 ทิศทางรอบพิกัด `playerPos`)
  2. **การตรวจสอบขอบเขต (Boundary Guard):** พิกัด `(r, c)` ที่จะตรวจสอบต้องอยู่ภายในขอบเขตตารางเสมอ (`r >= 0 && r < rows && c >= 0 && c < columns`) เพื่อป้องกัน `IndexOutOfRangeException` เมื่อผู้เล่นอยู่บริเวณมุมหรือขอบแผนที่
  3. **ข้ามช่องที่ผู้เล่นยืนอยู่:** ไม่นับช่องที่พิกัดตรงกับผู้เล่น (`r == playerPos.x && c == playerPos.y`)
  4. หากช่องรอบตัวช่องใดมีค่าเท่ากับ `threatCode` (ค่าเริ่มต้นคือ 2) ให้นับจำนวนเพิ่มขึ้น 1
  5. แสดงข้อความในคอนโซล:
     ```csharp
     Debug.Log($"Found {count} threats around player at {playerPos}");
     ```
  6. คืนค่าจำนวนภัยคุกคามที่นับได้ทั้งหมด

### 2.4 เมธอดค้นหาตำแหน่งผู้เล่น: `FindPlayerPosition`
- **Signatures:**
  ```csharp
  public Vector2Int FindPlayerPosition()
  public Vector2Int FindPlayerPosition(MapGenerator mapGen)
  public Vector2Int FindPlayerPosition(int[,] grid, int playerCode)
  ```
- **ข้อกำหนดและเงื่อนไข:**
  1. `FindPlayerPosition()`: คืนค่าพิกัด `playerStartPos` ประจำตัว `MapGenerator`
  2. `FindPlayerPosition(MapGenerator mapGen)`: ดึงค่า `playerStartPos` จากอินสแตนซ์ที่ส่งเข้ามา (หาก `mapGen == null` ให้คืนค่า `new Vector2Int(-1, -1)`)
  3. `FindPlayerPosition(int[,] grid, int playerCode)`: วนค้นหาพิกัดของผู้เล่นในตาราง หากพบให้คืนค่าพิกัด `new Vector2Int(r, c)` หากไม่พบให้คืนค่า `new Vector2Int(-1, -1)`

---

## 🟠 ระดับ 3: การสร้างคลาสเกราะ และระบบเสกสร้างวัตถุจาก Prefab (Custom Class & Spawner)
- **คะแนนเต็ม:** 3 คะแนน (3 Test Cases)
- **ไฟล์เป้าหมาย:** ให้นักศึกษาสร้างไฟล์สคริปต์ C# ขึ้นมาเองในโฟลเดอร์ `Assets/Scripts/Workspace/Week08/` (เช่น `Armor.cs` หรือ `ArmorSpawner.cs`)
- **Namespace:** `Week08.Ex03`

### 3.1 คลาสไอเทมเกราะ: `Armor`
ให้นักศึกษาเขียนประกาศคลาส `public class Armor : MonoBehaviour` ภายใน namespace `Week08.Ex03`:

1. **การประกาศตัวแปร (Fields):**
   - `public string Name = "Iron Armor";` (ชื่อไอเทมเกราะ)
   - `public int defenseBonus = 10;` (ค่าพลังป้องกันที่เพิ่มให้ผู้เล่น)
2. **เมธอดตรวจจับการชน: `OnTriggerEnter2D(Collider2D other)`:**
   - แสดงข้อความเมื่อเกิดการชน:
     ```csharp
     Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");
     ```
   - ตรวจสอบว่าวัตถุที่ชนมีคอมโพเนนต์ `Player` หรือไม่ ผ่าน `other.GetComponent<Player>()`
   - **เมื่อตรวจพบผู้เล่น (`player != null`):**
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

### 3.2 คลาสตัวเสกไอเทม: `ArmorSpawner`
ให้นักศึกษาเขียนประกาศคลาส `public class ArmorSpawner : MonoBehaviour` ภายใน namespace `Week08.Ex03`:

- **Signature:** `public GameObject SpawnArmor(GameObject prefab, Vector3 spawnPosition)`
- **ข้อกำหนดและเงื่อนไข:**
  1. **กรณี `prefab == null`:**
     - แสดงข้อความในคอนโซล:
       ```csharp
       Debug.Log("Prefab is null");
       ```
     - คืนค่า `null`
  2. **กรณี `prefab != null`:**
     - เสกสร้างวัตถุลงในฉากด้วยคำสั่ง `Instantiate(prefab, spawnPosition, Quaternion.identity);`
     - แสดงข้อความในคอนโซล:
       ```csharp
       Debug.Log($"Spawned {prefab.name} at {spawnPosition}");
       ```
     - คืนค่าอินสแตนซ์ของ `GameObject` ที่เสกสร้างออกมา

### 3.3 การสร้าง Prefab `Iron Armor` ใน Unity Editor
ให้นักศึกษาประกอบชิ้นงาน Prefab ในโปรแกรม Unity ตามขั้นตอนดังนี้:
1. ในหน้าต่าง **Hierarchy** คลิกขวาเลือก **Create Empty** ตั้งชื่อ GameObject ว่า `Iron Armor`
2. เพิ่ม Component **Sprite Renderer**:
   - เลือก Sprite รูปเกราะจากโฟลเดอร์: `Assets/Sprite/Cainos/Pixel Art Icon Pack - RPG/Texture/Equipment/Iron Armor.png`
3. เพิ่ม Component **Box Collider 2D**:
   - **ติ๊กเครื่องหมายถูกที่ช่อง `Is Trigger`**
4. แนบคอมโพเนนต์สคริปต์ `Armor` (เลือกคลาส `Week08.Ex03.Armor` ที่นักศึกษาเขียนขึ้นมา)
   - ตรวจสอบว่าใน Inspector แสดงค่า `Name = "Iron Armor"` และ `defenseBonus = 10`
5. ลาก GameObject `Iron Armor` จาก Hierarchy ไปปล่อยไว้ในโฟลเดอร์:
   ```
   Assets/Prefabs/Exam/Iron Armor.prefab
   ```
   *(หากยังไม่มีโฟลเดอร์ Exam สามารถสร้างโฟลเดอร์ใหม่ใต้ Assets/Prefabs/ ได้)*

---

## 📊 ตารางสรุป 10 Test Cases (การให้คะแนน)

| หมวดหมู่ | ชื่อ Test Case | เกณฑ์การตรวจ | คะแนน |
| :---: | :---| :---| :---: |
| **Level 1** | `Lv1_01_CharacterStatus_FieldsExistAndDefaultValues` | ตรวจสอบตัวแปรทั้ง 6 ตัวและค่าเริ่มต้นใน `CharacterStatus` | 1 คะแนน |
| **Level 1** | `Lv1_02_TakeDamage_VoidMethod` | ตรวจสอบการลด HP, การ clamp เลือดไม่ให้ติดลบ, และการปรับ `isAlive` | 1 คะแนน |
| **Level 1** | `Lv1_03_IsAlive_And_CalculateDamage_ReturnMethods` | ตรวจสอบการคืนค่าสถานะ `IsAlive()` และการคำนวณดาเมจสุทธิ | 1 คะแนน |
| **Level 2** | `Lv2_01_MapGenerator_FindItemPosition` | ตรวจสอบการค้นหาพิกัดไอเทมช่องแรกบนตาราง 2 มิติ | 1 คะแนน |
| **Level 2** | `Lv2_02_MapGenerator_CountItems` | ตรวจสอบการนับจำนวนไอเทมทั้งหมดบนตาราง | 1 คะแนน |
| **Level 2** | `Lv2_03_MapGenerator_ScanThreats` | ตรวจสอบเรดาร์ตรวจภัยคุกคามรอบตัว 8 ทิศทาง และการป้องกันขอบเขต | 1 คะแนน |
| **Level 2** | `Lv2_04_MapGenerator_FindPlayerPosition` | ตรวจสอบการค้นหาพิกัดผู้เล่นทั้งจากตัวแปรและตาราง 2 มิติ | 1 คะแนน |
| **Level 3** | `Lv3_01_Armor_FieldsAndTrigger` | ตรวจสอบฟิลด์ของคลาส `Armor` และการเพิ่ม Defense เมื่อชน Player | 1 คะแนน |
| **Level 3** | `Lv3_02_ArmorSpawner_NullPrefab` | ตรวจสอบการจัดการค่า `null` ในเมธอด `SpawnArmor` | 1 คะแนน |
| **Level 3** | `Lv3_03_ArmorSpawner_InstantiateArmorPrefab` | ตรวจสอบการเสกสร้าง Prefab `Iron Armor.prefab` และพิกัดตำแหน่ง | 1 คะแนน |
| **รวม** | **10 Test Cases** | **ทดสอบผ่าน Unity Test Runner (EditMode)** | **10 คะแนน** |
