# Week 08 Requirements: Midterm Practical Exam (ระดับ 1 - 3)

ข้อสอบชุดนี้แบ่งเป็น 3 ระดับ รวมทั้งหมด 3 ข้อ โดยทุกคลาสหลักในสัปดาห์นี้จะ**สืบทอดจาก `MonoBehaviour`** เพื่อให้สามารถแนบเป็น Component บน GameObject ในฉาก Unity และแสดงผลใน Inspector ได้อย่างถูกต้อง

---

## 🟢 ระดับ 1: ระบบสถานะตัวละครและการคำนวณดาเมจธาตุ (Status & Elemental Combat)

**ไฟล์:** `AS01_StatusAndLogic.cs` (namespace `Week08.Ex01`)

### รายละเอียดโจทย์:
ในคลาส `CharacterStatus` (**สืบทอดจาก `MonoBehaviour`**):

1. **การประกาศตัวแปร (Fields):**
   - `characterName` (ชนิด `string`) กำหนดค่าเริ่มต้นเป็น `"Hero"`
   - `maxHealth` (ชนิด `int`) กำหนดค่าเริ่มต้นเป็น `100`
   - `currentHealth` (ชนิด `int`) กำหนดค่าเริ่มต้นเป็น `100`
   - `shield` (ชนิด `int`) กำหนดค่าเริ่มต้นเป็น `0`
   - `isAlive` (ชนิด `bool`) กำหนดค่าเริ่มต้นเป็น `true`
   *(ทุกตัวแปรต้องสามารถเข้าถึงได้จากภายนอกคลาส / public)*

2. **เมธอด `CheckHealthStatus()` (คืนค่า `string`):**
   - หาก `currentHealth <= 0`:
     - กำหนด `isAlive = false`
     - แสดงข้อความผ่าน SimpleDebugConsole: `Debug.Log($"{characterName} is Defeated");`
     - คืนค่า `"Defeated"`
   - หาก `currentHealth <= maxHealth * 0.25f` (เลือดเหลือน้อยกว่าหรือเท่ากับ 25%):
     - แสดงข้อความ: `Debug.Log($"{characterName} is in Critical condition");`
     - คืนค่า `"Critical"`
   - กรณีอื่นๆ (เลือดมากกว่า 25%):
     - แสดงข้อความ: `Debug.Log($"{characterName} is Healthy");`
     - คืนค่า `"Healthy"`

3. **เมธอด `CalculateDamage(int rawDamage, string attackElement, string targetWeakness)` (คืนค่า `int`):**
   - ตรวจสอบความถูกต้องของค่าโจมตี:
     - หาก `rawDamage <= 0` หรือ `attackElement == "None"`:
       - แสดงข้อความ: `Debug.Log("Invalid Attack");`
       - คืนค่า `0`
   - ตรวจสอบจุดอ่อนธาตุ (เช่น `"Fire"`, `"Water"`, `"Grass"`):
     - หาก `attackElement == targetWeakness`:
       - ดาเมจสุทธิเป็น 2 เท่า (`rawDamage * 2`)
       - แสดงข้อความ: `Debug.Log($"Super Effective! Damage: {finalDamage}");`
       - คืนค่าดาเมจสุทธิ
     - กรณีอื่นๆ:
       - ดาเมจสุทธิเท่ากับ `rawDamage` ปกติ
       - แสดงข้อความ: `Debug.Log($"Normal Damage: {finalDamage}");`
       - คืนค่าดาเมจสุทธิ

---

## 🟡 ระดับ 2: การค้นหาในกระเป๋าและเรดาร์สำรวจดันเจี้ยน (Inventory & Grid Radar)

**ไฟล์:** `AS02_InventoryAndGrid.cs` (namespace `Week08.Ex02`)

### รายละเอียดโจทย์:
ในคลาส `InventoryAndGridScanner` (**สืบทอดจาก `MonoBehaviour`**):

1. **เมธอด `FindItemPosition(int[,] mapGrid, int itemCode)` (คืนค่า `Vector2Int`):**
   - รับตารางแผนที่ 2 มิติ `mapGrid` และรหัสไอเทม `itemCode` (เช่น ไม้สติ๊ก รหัส 3)
   - ใช้ Nested Loop ค้นหาช่องแรกที่มีค่าเท่ากับ `itemCode`
   - หากพบ ให้คืนค่าพิกัด `new Vector2Int(r, c)` ทันที โดยที่ `r` คือแถว (row) และ `c` คือคอลัมน์ (column)
   - หากวนจนจบลูปแล้วไม่พบ ให้คืนค่า `new Vector2Int(-1, -1)`

2. **เมธอด `CountItems(int[,] mapGrid, int itemCode)` (คืนค่า `int`):**
   - รับตารางแผนที่ 2 มิติ `mapGrid` และรหัสไอเทม `itemCode`
   - ใช้ Nested Loop วนอ่านข้อมูลทุกแถวและคอลัมน์ใน `mapGrid`
   - นับจำนวนช่องทั้งหมดที่มีค่าเท่ากับ `itemCode`
   - คืนค่าจำนวนไอเทมที่นับได้ทั้งหมดบนตาราง

3. **เมธอด `ScanThreats(int[,] mapGrid, int threatCode)` (คืนค่า `int`):**
   - รับตารางแผนที่ 2 มิติ `mapGrid`
   - ใช้ Nested Loop วนอ่านข้อมูลทุกแถวและคอลัมน์ โดยหาขนาดจาก `mapGrid.GetLength(0)` (แถว) และ `mapGrid.GetLength(1)` (คอลัมน์)
   - นับจำนวนช่องทั้งหมดที่มีค่าเท่ากับ `threatCode`
   - แสดงข้อความ: `Debug.Log($"Found {threatCount} threats of code {threatCode}");`
   - คืนค่าจำนวนช่องที่เป็นภัยคุกคาม

4. **เมธอด `FindExitPosition(int[,] mapGrid, int exitCode)` (คืนค่า `Vector2Int`):**
   - ใช้ Nested Loop วนหาพิกัดแรกที่มีค่าเท่ากับ `exitCode`
   - หากพบ ให้คืนค่า `new Vector2Int(r, c)` ทันที โดยที่ `r` คือแถว และ `c` คือคอลัมน์
   - หากไม่พบช่องทางออกเลย ให้คืนค่า `new Vector2Int(-1, -1)`

---

## 🟠 ระดับ 3: การสร้างคลาสไอเทม และการเสกสร้างวัตถุจาก Prefab (Custom Class & Spawner)

**ไฟล์:** `AS03_MethodAndClass.cs` (namespace `Week08.Ex03`)

### รายละเอียดโจทย์:

1. **คลาส `Weapon` (สืบทอดจาก `MonoBehaviour`):**
   - ให้นักศึกษาประกาศฟิลด์แบบ `public` ภายในคลาส `Weapon`:
     - `weaponName` (ชนิด `string`) กำหนดค่าเริ่มต้นเป็น `"Wooden Stick"`
     - `attackPower` (ชนิด `int`) กำหนดค่าเริ่มต้นเป็น `10`
     - `durability` (ชนิด `int`) กำหนดค่าเริ่มต้นเป็น `20`
   - เขียนเมธอด `Use()` (คืนค่า `void`):
     - หาก `durability > 0` ให้ลด `durability` ลง 1
     - แสดงข้อความ: `Debug.Log($"{weaponName} used. Durability: {durability}");`

2. **คลาส `WeaponSpawner` (สืบทอดจาก `MonoBehaviour`):**
   - เขียนเมธอด `SpawnWeapon(GameObject prefab, Vector3 spawnPosition)` (คืนค่า `GameObject`):
     - ตรวจสอบความถูกต้องของพารามิเตอร์:
       - หาก `prefab == null`:
         - แสดงข้อความ: `Debug.Log("Prefab is null");`
         - คืนค่า `null`
       - หาก `prefab != null`:
         - เสกสร้างวัตถุลงในฉากด้วยคำสั่ง `Instantiate(prefab, spawnPosition, Quaternion.identity);`
         - แสดงข้อความ: `Debug.Log($"Spawned {prefab.name} at {spawnPosition}");`
         - คืนค่าตัวแปร `GameObject` ที่เสกสร้างออกมา
