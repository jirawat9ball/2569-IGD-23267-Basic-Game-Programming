using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08.Ex01
{
    public class CharacterStatus : MonoBehaviour
    {
        // =========================================================================
        // ระดับ 1: ข้อมูลสถานะตัวละคร และเมธอดแบบคืนค่า/ไม่คืนค่า (Class & Methods)
        // =========================================================================

        // 1. ประกาศตัวแปรแบบ public ทั้งหมด 6 ตัวแปร พร้อมกำหนดค่าเริ่มต้น:
        //    - characterName (string) เริ่มต้น "Hero"
        //    - maxHealth (int) เริ่มต้น 100
        //    - currentHealth (int) เริ่มต้น 100
        //    - isAlive (bool) เริ่มต้น true
        //    - attackPoint (int) เริ่มต้น 10
        //    - defensePoint (int) เริ่มต้น 0
        // Student code starts HERE ...
 
        // Student code ends HERE ...

        // 2. เมธอดแบบไม่คืนค่า (void): TakeDamage(int damage)
        //    - ลด currentHealth ลงตามค่า damage ที่รับเข้ามา
        //    - ถ้า currentHealth <= 0:
        //        currentHealth = 0
        //        isAlive = false
        //        พิมพ์ $"{characterName} is defeated!"
        //    - ถ้า currentHealth > 0:
        //        พิมพ์ $"{characterName} took {damage} damage. Current HP: {currentHealth}"
    
        // 3. เมธอดแบบคืนค่า (bool): IsAlive()
        //    - ตรวจสอบสถานะว่าตัวละครยังมีชีวิตอยู่หรือไม่
        //    - คืนค่า true หาก currentHealth > 0 (หรือ isAlive เป็นจริง)
        //    - คืนค่า false หากเลือดหมดแล้ว (<= 0)
       

        // 4. เมธอดแบบคืนค่า (int): CalculateDamage(int rawDamage, int defense)
        //    - คำนวณดาเมจสุทธิโดยนำ rawDamage ลบด้วย defense (netDamage = rawDamage - defense)
        //    - หาก netDamage < 0 ให้กำหนด netDamage = 0 (ดาเมจสุทธิต้องไม่ติดลบ)
        //    - พิมพ์ $"Damage calculated: {netDamage}"
        //    - คืนค่า netDamage ที่คำนวณได้
       
    }
}
