using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    /// <summary>
    /// สคริปต์ควบคุมศัตรู (Enemy)
    /// ใช้สอนเรื่อง: การตรวจจับการชนด้วย OnTriggerEnter2D และการใช้ GetComponent เพื่อเข้าถึง Player
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region 1. ข้อมูลและสถานะของศัตรู (Variables / Fields)

        [Header("ข้อมูลทั่วไป")]
        public string Name = "Enemy";

        [Header("ตำแหน่งบนแผนที่ (Grid Position)")]
        public int positionX;
        public int positionY;
        [HideInInspector] public MapGenerator mapGenerator;

        [Header("ค่าสถานะ (Stats)")]
        public int energy = 10;       // พลังชีวิตของศัตรู (น้อยกว่า Player: 10 vs 20)
        public int attackPoint = 5;   // พลังโจมตีใส่ผู้เล่น (น้อยกว่า Player: 5 vs 10)

        #endregion

        #region วงจรการทำงานของ Unity (Lifecycle)

        private void Awake()
        {
            if (energy <= 0)
            {
                energy = 10;
            }
        }

        #endregion

        #region 2. การตรวจจับการชน (Trigger Detection)

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Guideline:
            // 1. แสดง Log แจ้งเตือนเมื่อเกิดการชน: "[Trigger] <gameObject.name> collided with <other.gameObject.name>"
            // 2. ใช้ other.GetComponent<Player>() เพื่อตรวจสอบว่าชนกับผู้เล่นหรือไม่
            // 3. ถ้าเป็นผู้เล่น ให้เรียก Attack(player, attackPoint) และ TakeDamage(player.attackPoint)

            // Student code starts HERE ...

            // Student code ends HERE ...
        }

        #endregion

        #region 3. ความสามารถและการต่อสู้ (Combat Methods)

        /// <summary>
        /// โจมตีผู้เล่น โดยสั่งให้ผู้เล่นรับดาเมจ
        /// </summary>
        public void Attack(Player target, int damage)
        {
            // Guideline:
            // 1. ตรวจสอบว่า target ไม่เป็น null
            // 2. พิมพ์ Log: "<Name> attacks <target.Name> with <damage> damage!"
            // 3. เรียก target.TakeDamage(damage, Name)

            // Student code starts HERE ...

            // Student code ends HERE ...
        }

        /// <summary>
        /// รับความเสียหาย เมื่อโดนผู้เล่นโจมตี
        /// </summary>
        public void TakeDamage(int damage)
        {
            // Guideline:
            // 1. ลดพลังชีวิต energy ลงตามค่า damage
            // 2. พิมพ์ Log: "<Name> takes <damage> damage! Remaining HP: <energy>"
            // 3. หาก energy <= 0 ให้พิมพ์ "💥 <Name> defeated!" และ Destroy(gameObject)

            // Student code starts HERE ...

            // Student code ends HERE ...
        }

        #endregion
    }
}
