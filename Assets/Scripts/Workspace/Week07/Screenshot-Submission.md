# 📸 คำสั่งส่งภาพสกรีนช็อต (Screenshot) — Week 07: การเชื่อมโยงสคริปต์ใหม่ (Re-linking Scripts)

## 📋 คำชี้แจง

ให้นักศึกษา**จับภาพหน้าจอ (Screenshot)** ใน Unity Editor เพื่อยืนยันว่าสคริปต์ได้รับการตรวจสอบและเชื่อมโยงใหม่ (Re-linked / Attached) เข้ากับ GameObject ใน Scene อย่างถูกต้องสมบูรณ์ ไม่มีปัญหา `Missing (Mono Script)` หรือข้อผิดพลาดการคอมไพล์

> [!IMPORTANT]
> ภาพสกรีนช็อตต้องแสดงให้เห็นหน้าต่าง **Inspector** ที่สคริปต์ถูกเชื่อมโยงกับ GameObject เรียบร้อยแล้ว ควบคู่กับหน้าต่าง **Hierarchy** และ **Console** (ต้องไม่มี Error สีแดง) ให้เห็นชัดเจนในภาพเดียวกัน

---

## 🎯 สิ่งที่ต้องแสดงในภาพสกรีนช็อต (Requirements)

เปิด Scene `Assets/Scenes/Week07_OOP.unity` ใน Unity Editor:

### 1. การเชื่อมโยงสคริปต์ในหน้าต่าง Inspector
- คลิกเลือก GameObject ในหน้าต่าง **Hierarchy** เช่น:
  - **`Assingment`**: ต้องมี Component `Assignment_Student_Week07` และ `Assignment_Submitter_Week07` เชื่อมโยงอยู่ครบถ้วน
  - **`MapGenerator`**: ต้องมี Component `MapGenerator` ที่เชื่อมโยงสคริปต์และ Prefabs/References ต่างๆ อย่างถูกต้อง
- ในหน้าต่าง **Inspector** ต้องแสดงชื่อสคริปต์และตัวแปรต่างๆ อย่างสมบูรณ์
- ❌ **ต้องไม่ขึ้นข้อความเตือน:** `The associated script can't be loaded` หรือ `Missing (Mono Script)`

### 2. สถานะของโปรเจกต์ (Console Window)
- หน้าต่าง **Console** ต้องสะอาด ไม่มีข้อผิดพลาดสีแดง (Compile Errors) ขวางการทำงาน

---

## 📸 รายละเอียดสิ่งที่ต้องปรากฏในภาพ

```
┌───────────────────────────┬────────────────────────────────────────────────────────┐
│      Hierarchy Window     │                    Inspector Window                    │
│  - Main Camera            │  GameObject: Assingment / MapGenerator                 │
│  - MapGenerator           │  ✅ สคริปต์เชื่อมโยงสำเร็จ (ไม่ขึ้น Missing Script)     │
│  - Assingment [เลือกอยู่] │  ✅ แสดงตัวแปรและ References ครบถ้วน                   │
├───────────────────────────┴────────────────────────────────────────────────────────┤
│                                  Console Window                                    │
│  ✅ ไม่มี Compile Error สีแดง (Console สะอาดหรือกด Clear ได้)                      │
└────────────────────────────────────────────────────────────────────────────────────┘
```

1. **Hierarchy:** เห็นชื่อ GameObject (`Assingment` หรือ `MapGenerator`) ที่กำลังถูกเลือก (Highlighted)
2. **Inspector:** เห็น Component สคริปต์ที่ผูกติดอยู่ชัดเจน ตัวหนังสืออ่านง่าย ไม่ถูกบดบัง
3. **Console:** มองเห็นหน้าต่าง Console แสดงสถานะปกติ ไม่ติด Error

---

## 🛠️ วิธีการจับภาพหน้าจอ (Screenshot)

- **Windows Shortcut:**
  - กดปุ่ม `Win + Shift + S` แล้วลากคลุมบริเวณหน้าต่าง Unity Editor ทั้งหมด
  - หรือกดปุ่ม `Print Screen` (PrtScn) แล้วนำไปวาง (Ctrl + V) บันทึกเป็นไฟล์ภาพ
- **บันทึกภาพเป็นไฟล์นามสกุล:** `.png` หรือ `.jpg`

---

## 📌 ข้อกำหนดการตั้งชื่อไฟล์และการส่งงาน

| หัวข้อ | รายละเอียด |
|---|---|
| **จำนวนภาพ** | 1 ภาพ (เห็นทั้ง Hierarchy, Inspector และ Console) |
| **รูปแบบไฟล์** | `.png` หรือ `.jpg` |
| **การตั้งชื่อไฟล์** | `รหัสนักศึกษา_Week07_ScriptLinked.png`<br>*(ตัวอย่าง: `66012345_Week07_ScriptLinked.png`)* |
| **สิ่งที่ต้องเห็นในภาพ** | Hierarchy + Inspector แสดงสคริปต์ที่เชื่อมโยงสมบูรณ์ + Console |

---

## ⚠️ ข้อควรระวังและเกณฑ์การให้คะแนน

- ❌ ภาพที่ติดแถบเตือนสีเหลือง/แดงว่า **`Missing (Mono Script)`** จะไม่ได้รับคะแนน
- ❌ ภาพที่ไม่เห็นหน้าต่าง Inspector หรือมองไม่เห็นตัวสคริปต์ที่เชื่อมโยงจะไม่ได้รับคะแนน
- ❌ ห้ามตัดต่อรูปภาพ หรือนำภาพของผู้อื่นมาส่งโดยเด็ดขาด

> [!TIP]
> **หากสคริปต์หลุด (ขึ้น Missing Script) ต้องทำอย่างไร?**
> 1. ตรวจสอบว่าโค้ดไม่มี Compile Error ค้างอยู่ใน Console
> 2. ลากไฟล์สคริปต์จาก Project View (`Assets/Scripts/Workspace/Week07/...`) มาวางใส่ GameObject ในหน้าต่าง Inspector ใหม่
> 3. หรือกดปุ่ม **Remove Component** อันที่เสียออก แล้วกดปุ่ม **Add Component** ค้นหาชื่อสคริปต์เพื่อใส่ใหม่อีกครั้ง
