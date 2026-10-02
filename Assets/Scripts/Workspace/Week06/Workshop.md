# 🎮 Workshop Week 06: Mini 2D Grid Game
## การประยุกต์ใช้ Class, Method, GetComponent และ Trigger ใน Unity

---

## 📋 ภาพรวมของ Workshop

ใน Workshop นี้ นักศึกษาจะได้นำความรู้เรื่อง **Class, Fields (ตัวแปรประจำคลาส), Methods (ฟังก์ชันการทำงาน)** จาก Lecture มาประยุกต์สร้างเกมจริงแนว **2D Grid-based Dungeon Game** ใน Unity!

```
[ จุดเริ่มต้น (0,0) ] ───> [ เก็บยา/ดาบ 🧪⚔️ ] ───> [ สู้กับศัตรู 👾 ] ───> [ ทางออก 🏁 ]
```

### 🎯 วัตถุประสงค์การเรียนรู้
1. เข้าใจการสร้างคลาสใน Unity โดยสืบทอดจาก `MonoBehaviour`
2. กำหนด **ข้อมูลประจำคลาส (Fields)** เช่น พลังชีวิต (`energy`), พลังโจมตี (`attackPoint`), พิกัด (`positionX, positionY`)
3. สร้างและเรียกใช้งาน **พฤติกรรม (Methods)** ของตัวละคร เช่น `Move()`, `TakeDamage()`, `Heal()`, `Attack()`
4. เข้าใจการทำงานของ **`OnTriggerEnter2D`** เพื่อตรวจจับการชนของวัตถุ
5. ใช้คำสั่ง **`GetComponent<T>()`** เพื่อสื่อสารและเรียกใช้ความสามารถของ Object อื่น

---

## 🗂️ โครงสร้างไฟล์ในโฟลเดอร์ Game (`Week06/Game/`)

| สคริปต์ | หน้าที่หลัก | สิ่งที่ได้เรียนรู้ |
|---|---|---|
| [Player.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/Player.cs) | ควบคุมผู้เล่น, การเดิน, เลือด, การโจมตี | Class, Fields, Input, Methods Overloading |
| [Enemy.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/Enemy.cs) | ตัวศัตรู, โจมตีผู้เล่นเมื่อชน, รับดาเมจ | `OnTriggerEnter2D`, `GetComponent<Player>()` |
| [ItemPotion.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/ItemPotion.cs) | ยาฟื้นฟูพลังงาน (+10 Energy) | ดึง Component แล้วสั่ง `player.Heal()` |
| [ItemSword.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/ItemSword.cs) | ดาบเพิ่มพลังโจมตี (+10 Attack) | ดึง Component แล้วสั่ง `player.IncreaseAttack()` |
| [Trap.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/Trap.cs) | กับดักหนาม (-5 Energy) | ตรวจจับการเหยียบแล้วลดเลือดผู้เล่น |
| [Wall.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/Wall.cs) | กำแพงขวางทาง (มีค่าความทนทาน) | การลดค่า Durability และทำลายวัตถุ |
| [Chest.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/Chest.cs) | กล่องสมบัติ (Spawner) | เสกสร้างวัตถุใหม่ลงฉากด้วย `Instantiate` |
| [Exit.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/Exit.cs) | ประตูทางออก | ตรวจจับผู้เล่นเพื่อประกาศชัยชนะ |
| [MapGenerator.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/MapGenerator.cs) | สุ่มสร้างแผนที่ วางพื้น กำแพง ผู้เล่น ศัตรู ไอเทม | การใช้ `Instantiate` สร้าง Object ลงใน Scene |

---

## 🛠️ ขั้นตอนการทดลองทีละสเต็ป (Step-by-Step Hands-on)

### 🔹 Step 1: การสร้างคลาสผู้เล่น ([Player.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/Player.cs))

ผู้เล่นเป็นคลาสหลักที่เก็บข้อมูลสถานะและระบบควบคุมการเดิน:

#### 1. กำหนดตัวแปร (Fields)
```csharp
[Header("ข้อมูลทั่วไป")]
public string Name = "Player";
public int positionX;
public int positionY;

[Header("ค่าสถานะ (Stats)")]
public int energy = 20;       // พลังงานเริ่มต้น (เดิน 1 ก้าว เสีย 1 energy)
public int attackPoint = 10;  // พลังโจมตีเริ่มต้น
```

#### 2. ตรวจจับการกดปุ่มใน `Update()`
ใช้คำสั่ง `Input.GetKeyDown` เพื่อรับปุ่มลูกศรหรือ W, A, S, D:
```csharp
void Update()
{
    if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        Move(Vector2.right);
    else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        Move(Vector2.left);
    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        Move(Vector2.up);
    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        Move(Vector2.down);
}
```

#### 3. พฤติกรรมการเดินและการหักพลังงาน
```csharp
public void Move(Vector2 direction)
{
    if (!CanMove(direction)) return; // ตรวจขอบเขตแผนที่

    positionX += (int)direction.x;
    positionY += (int)direction.y;
    transform.position = new Vector3(positionX, positionY, 0);

    TakeDamage(1); // ก้าวเดิน 1 ก้าว เสียพลังงาน 1 หน่วย
}
```

#### 4. เมธอดความสามารถ (Methods)
- `TakeDamage(int damage)`: ลดค่า energy และตรวจว่าตายหรือยังด้วย `CheckDead()`
- `Heal(int healPoint)`: เพิ่มค่า energy
- `IncreaseAttack(int value)`: เพิ่มค่า attackPoint ถาวร
- `Attack(Enemy target, int damage)`: สั่งให้เป้าหมายรับความเสียหาย

---

### 🔹 Step 2: การสร้างไอเทมและการใช้ `GetComponent` ([ItemPotion.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/ItemPotion.cs) & [ItemSword.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/ItemSword.cs))

เมื่อผู้เล่นเดินมาทับช่องไอเทม วัตถุจะตรวจจับการชนผ่าน `OnTriggerEnter2D`:

```csharp
private void OnTriggerEnter2D(Collider2D other)
{
    // 1. ตรวจสอบว่าสิ่งที่ชนมีคอมโพเนนต์ Player หรือไม่
    Player player = other.GetComponent<Player>();

    // 2. ถ้าใช่ Player ให้เรียกความสามารถของ Player แล้วทำลายตัวเองทิ้ง
    if (player != null)
    {
        Debug.Log($"✨ Picked up {Name}! +{healPoint} Energy");
        player.Heal(healPoint); // เรียก Method บน Player
        Destroy(gameObject);    // ลบไอเทมออกจากฉาก
    }
}
```

> 💡 **หัวใจสำคัญ:** การใช้ `other.GetComponent<Player>()` ทำให้อ็อบเจกต์ไอเทมไม่ต้องรู้จักตัวแปร Player มาตั้งแต่แรก แต่จะค้นหาตัวตนของผู้เล่นเฉพาะเมื่อเกิดการชนกันเท่านั้น!

---

### 🔹 Step 3: การสร้างศัตรูและระบบต่อสู้ ([Enemy.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/Enemy.cs))

ศัตรูจะมีค่าพลังชีวิต (`energy = 10`) และพลังโจมตี (`attackPoint = 5`) ซึ่งน้อยกว่าผู้เล่น:

```csharp
private void OnTriggerEnter2D(Collider2D other)
{
    Player player = other.GetComponent<Player>();
    if (player != null)
    {
        Attack(player, attackPoint); // ศัตรูตีผู้เล่น 5 ดาเมจ
    }
}

public void TakeDamage(int damage)
{
    energy -= damage;
    Debug.Log($"{Name} takes {damage} damage! Remaining HP: {energy}");

    if (energy <= 0)
    {
        Debug.Log($"💥 {Name} defeated!");
        Destroy(gameObject); // ศัตรูถูกกำจัดเมื่อเลือดหมด
    }
}
```

ในขณะเดียวกัน ที่ฝั่ง **[Player.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/Player.cs)** เมื่อชนศัตรู:
```csharp
Enemy enemy = other.GetComponent<Enemy>();
if (enemy != null)
{
    Attack(enemy, attackPoint); // ผู้เล่นตีศัตรู 10 ดาเมจ
}
```
ผลลัพธ์คือเกิดการต่อสู้ขึ้นทันทีเมื่อเดินชนกัน ศัตรูเลือด 10 โดนฟัน 10 ดาเมจจะตายทันที ส่วนผู้เล่นเลือด 20 จะถูกศัตรูตีสวน 5 ดาเมจ เหลือ 15!

---

### 🔹 Step 4: กำแพงและทางออก ([Wall.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/Wall.cs) & [Exit.cs](file:///d:/Unity/2569-IGD-23267-Basic-Game-Programming/Assets/Scripts/Workspace/Week06/Game/Exit.cs))

- **Wall (กำแพง):** มีฟิลด์ `durability = 2` ทุกครั้งที่ชนจะเรียก `Hit()` ลดความทนทาน เมื่อชนครบ 2 ครั้ง กำแพงจะพังลง
- **Exit (ทางออก):** เมื่อ Player เดินมาชน จะแสดงข้อความ `"🎉 You Win! Reached the exit!"`

---

## ⚙️ การตั้งค่า Unity Physics 2D (ที่สำคัญมาก!)

เพื่อให้ระบบ `OnTriggerEnter2D` ทำงานได้อย่างถูกต้อง:
1. Prefabs ทุกตัว (`Player`, `Enemy`, `Food/Potion`, `Soda/Sword`, `Exit`, `Wall`) ต้องมีคอมโพเนนต์ **`BoxCollider2D`**
2. ต้องติ๊กเครื่องหมายถูกที่ช่อง **`Is Trigger`**
3. ปรับขนาด Collider Size ให้เหมาะสม (เช่น `x: 0.5, y: 0.5`) เพื่อป้องกันการชนเหลื่อมข้ามช่อง
4. ตัว **`Player`** ต้องมีคอมโพเนนต์ **`Rigidbody2D`** โดยตั้งค่า `Body Type = Kinematic` หรือ `Dynamic (Gravity Scale = 0)`

---

## 🧪 การทดสอบรันเกม (Testing)

1. เปิด Scene ของเกมใน Unity Editor
2. กดปุ่ม **Play ▶️**
3. สังเกตหน้าต่าง **Console**:
   - กดปุ่ม **W, A, S, D** หรือ **ลูกศร** เพื่อเดินสังเกตการหักค่า Energy: `Current Energy : 19...`
   - เดินไปเก็บยา Potion: `✨ Picked up Potion! +10 Energy`
   - เดินไปเก็บดาบ Sword: `⚔️ Picked up Sword! +10 Attack`
   - เดินชนศัตรู Enemy: เกิดการต่อสู้ แสดงดาเมจ และศัตรูหายไป
   - เดินไปถึงจุด Exit ที่มุมขวาบน: `🎉 You Win! Reached the exit!`

---

## 💡 สรุปแนวคิดสำคัญสำหรับผู้เรียน

```
┌────────────────────────────────────────────────────────┐
│                      คลาส (Class)                       │
├────────────────────────────────────────────────────────┤
│ 1. Fields  (ข้อมูล)    : ตัวแปรเก็บค่า เช่น energy, attack │
│ 2. Methods (พฤติกรรม) : ฟังก์ชันการทำงาน เช่น Move(), Heal() │
│ 3. Trigger & Component: ใช้ OnTriggerEnter2D ตรวจจับชน  │
│                         และ GetComponent<T>() สั่งงาน   │
└────────────────────────────────────────────────────────┘
```
