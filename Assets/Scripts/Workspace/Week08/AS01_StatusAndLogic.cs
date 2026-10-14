using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08.Ex01
{
    public class CharacterStatus : MonoBehaviour
    {
        // =========================================================================
        // ระดับ 1: ข้อมูลสถานะตัวละครและการคำนวณดาเมจธาตุ (Status & Elemental Combat)
        // =========================================================================

        // 1. ประกาศตัวแปรแบบ public ทั้งหมด 5 ตัวแปร:
        //    - characterName (string) เริ่มต้น "Hero"
        //    - maxHealth (int) เริ่มต้น 100
        //    - currentHealth (int) เริ่มต้น 100
        //    - shield (int) เริ่มต้น 0
        //    - isAlive (bool) เริ่มต้น true
        // Student code starts HERE ...
        public string characterName = "Hero";
        public int maxHealth = 100;
        public int currentHealth = 100;
        public int shield = 0;
        public bool isAlive = true;
        // Student code ends HERE ...

        // 2. เมธอด CheckHealthStatus() คืนค่า string
        //    - ถ้า currentHealth <= 0:
        //        isAlive = false
        //        พิมพ์ $"{characterName} is Defeated"
        //        คืนค่า "Defeated"
        //    - ถ้า currentHealth <= maxHealth * 0.25f (เลือดเหลือ <= 25%):
        //        พิมพ์ $"{characterName} is in Critical condition"
        //        คืนค่า "Critical"
        //    - กรณีอื่นๆ:
        //        พิมพ์ $"{characterName} is Healthy"
        //        คืนค่า "Healthy"
        public string CheckHealthStatus()
        {
            // Student code starts HERE ...
            return "Healthy";
            // Student code ends HERE ...
        }

        // 3. เมธอด CalculateDamage(int rawDamage, string attackElement, string targetWeakness) คืนค่า int
        //    - ถ้า rawDamage <= 0 หรือ attackElement == "None":
        //        พิมพ์ "Invalid Attack"
        //        คืนค่า 0
        //    - ถ้า attackElement == targetWeakness:
        //        ดาเมจคูณ 2 (finalDamage = rawDamage * 2)
        //        พิมพ์ $"Super Effective! Damage: {finalDamage}"
        //        คืนค่า finalDamage
        //    - กรณีอื่นๆ:
        //        finalDamage = rawDamage
        //        พิมพ์ $"Normal Damage: {finalDamage}"
        //        คืนค่า finalDamage
        public int CalculateDamage(int rawDamage, string attackElement, string targetWeakness)
        {
            // Student code starts HERE ...
            return 0;
            // Student code ends HERE ...
        }
    }
}
