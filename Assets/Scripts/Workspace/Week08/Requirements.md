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
   - `isAlive` (ชนิด `bool`) กำหนดค่าเริ่มต้นเป็น `true`
   *(ทุกตัวแปรต้องสามารถเข้าถึงได้จากภายนอกคลาส / public)*

2. **เมธอดไม่คืนค่า `TakeDamage(int damage)` (ชนิด `void`):**
   - ลดค่า `currentHealth` ลงตามค่า `damage`
   - หาก `currentHealth <= 0`:
     - กำหนด `currentHealth = 0` (ไม่ให้เลือดติดลบ)
     - กำหนด `isAlive = false`
     - แสดงข้อความผ่าน SimpleDebugConsole: `Debug.Log($"{characterName} is defeated!");`
   - หากยังมีชีวิตอยู่ (`currentHealth > 0`):
     - แสดงข้อความ: `Debug.Log($"{characterName} took {damage} damage. Current HP: {currentHealth}");`

3. **เมธอดคืนค่า `IsAlive()` (คืนค่า `bool`):**
   - คืนค่า `true` หากตัวละครยังมีชีวิตอยู่ (`currentHealth > 0`)
   - คืนค่า `false` หากตัวละครตายแล้ว (`currentHealth <= 0`)

4. **เมธอดคืนค่า `CalculateDamage(int rawDamage, int defense)` (คืนค่า `int`):**
   - คำนวณดาเมจสุทธิ: `int netDamage = rawDamage - defense;`
   - หาก `netDamage < 0` ให้กำหนด `netDamage = 0` (ดาเมจสุทธิต้องไม่ติดลบ)
   - แสดงข้อความ: `Debug.Log($"Damage calculated: {netDamage}");`
   - คืนค่า `netDamage` ที่คำนวณได้

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

3. **เมธอด `ScanThreats(int[,] mapGrid, Vector2Int playerPos, int threatCode)` (คืนค่า `int`):**
   - รับตารางแผนที่ 2 มิติ `mapGrid`, ตำแหน่งผู้เล่น `playerPos` (โดย `playerPos.x` คือแถว และ `playerPos.y` คือคอลัมน์) และรหัสภัยคุกคาม `threatCode`
   - ตรวจสอบพื้นที่รอบตัวผู้เล่นในรัศมี 1 ช่อง (พื้นที่ 3x3 หรือ 8 ทิศทางรอบตัว):
     - วนลูป `r` จาก `playerPos.x - 1` ถึง `playerPos.x + 1`
     - วนลูป `c` จาก `playerPos.y - 1` ถึง `playerPos.y + 1`
     - ตรวจสอบว่าพิกัดอยู่ในขอบเขตของตาราง (`r >= 0 && r < mapGrid.GetLength(0) && c >= 0 && c < mapGrid.GetLength(1)`)
     - ข้ามช่องที่ผู้เล่นยืนอยู่ (`r == playerPos.x && c == playerPos.y`)
     - หากช่องนั้นมีค่าเท่ากับ `threatCode` ให้นับจำนวนเพิ่มขึ้น 1
   - แสดงข้อความ: `Debug.Log($"Found {threatCount} threats around player");`
   - คืนค่าจำนวนภัยคุกคามที่ตรวจพบรอบตัวผู้เล่น

4. **เมธอด `FindPlayerPosition(int[,] mapGrid, int playerCode)` (คืนค่า `Vector2Int`):**
   - รับตารางแผนที่ 2 มิติ `mapGrid` และรหัสผู้เล่น `playerCode` (เช่น รหัส 1)
   - ใช้ Nested Loop วนหาพิกัดแรกที่มีค่าเท่ากับ `playerCode`
   - หากพบ ให้คืนค่า `new Vector2Int(r, c)` ทันที โดยที่ `r` คือแถว และ `c` คือคอลัมน์
   - หากไม่พบช่องผู้เล่นเลย ให้คืนค่า `new Vector2Int(-1, -1)`

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
