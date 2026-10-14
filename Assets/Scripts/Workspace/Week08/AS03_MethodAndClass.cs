using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08.Ex03
{
    // =========================================================================
    // ระดับ 3: การสร้างคลาสไอเทม และการเสกสร้างวัตถุจาก Prefab (Custom Class & Spawner)
    // =========================================================================

    // 1. คลาส Weapon: ให้นักศึกษาเติมตัวแปรและเมธอดลงในคลาสนี้ (สืบทอดจาก MonoBehaviour)
    //    - ประกาศฟิลด์แบบ public:
    //        * weaponName (string) กำหนดค่าเริ่มต้นเป็น "Wooden Stick"
    //        * attackPower (int) กำหนดค่าเริ่มต้นเป็น 10
    //        * durability (int) กำหนดค่าเริ่มต้นเป็น 20
    //    - เมธอดแบบ public ไม่มีค่าส่งกลับ (void):
    //        * Use():
    //            - ถ้า durability > 0 ให้ลดลง 1
    //            - แสดงข้อความผ่าน Debug.Log: $"{weaponName} used. Durability: {durability}"
    public class Weapon : MonoBehaviour
    {
        // Student code starts HERE ...
        public string weaponName = "Wooden Stick";
        public int attackPower = 10;
        public int durability = 20;

        public void Use()
        {

        }
        // Student code ends HERE ...
    }

    // 2. คลาส WeaponSpawner สำหรับเสกสร้างไอเทมจาก Prefab ลงในฉาก (สืบทอดจาก MonoBehaviour)
    public class WeaponSpawner : MonoBehaviour
    {
        // เมธอด SpawnWeapon(GameObject prefab, Vector3 spawnPosition) คืนค่า GameObject
        // - ตรวจสอบ: ถ้า prefab == null:
        //     - แสดงข้อความผ่าน Debug.Log: "Prefab is null"
        //     - คืนค่า null
        // - ถ้า prefab != null:
        //     - เสกสร้างวัตถุด้วย Instantiate(prefab, spawnPosition, Quaternion.identity);
        //     - แสดงข้อความผ่าน Debug.Log: $"Spawned {prefab.name} at {spawnPosition}"
        //     - คืนค่า GameObject ที่เสกสร้างออกมา
        public GameObject SpawnWeapon(GameObject prefab, Vector3 spawnPosition)
        {
            // Student code starts HERE ...
            return null;
            // Student code ends HERE ...
        }
    }
}
