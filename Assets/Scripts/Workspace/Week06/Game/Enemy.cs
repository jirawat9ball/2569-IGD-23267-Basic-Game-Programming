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
        public int energy = 20;       // พลังชีวิตของศัตรู
        public int attackPoint = 5;   // พลังโจมตีใส่ผู้เล่น

        #endregion

        #region 2. การตรวจจับการชน (Trigger Detection)

        private void OnTriggerEnter2D(Collider2D other)
        {
            // แสดง Log แจ้งเตือนเมื่อเกิดการชน
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            // ตรวจสอบว่าวัตถุที่ชนมีคอมโพเนนต์ Player หรือไม่
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // ถ้าชนกับผู้เล่น ให้ศัตรูโจมตีผู้เล่น
                Attack(player, attackPoint);
            }
        }

        #endregion

        #region 3. ความสามารถและการต่อสู้ (Combat Methods)

        /// <summary>
        /// โจมตีผู้เล่น โดยสั่งให้ผู้เล่นรับดาเมจ
        /// </summary>
        public void Attack(Player target, int damage)
        {
            if (target != null)
            {
                Debug.Log($"{Name} attacks {target.Name} with {damage} damage!");
                target.TakeDamage(damage, Name);
            }
        }

        /// <summary>
        /// รับความเสียหาย เมื่อโดนผู้เล่นโจมตี
        /// </summary>
        public void TakeDamage(int damage)
        {
            energy -= damage;
            Debug.Log($"{Name} takes {damage} damage! Remaining HP: {energy}");

            // หากพลังชีวิตหมด ให้ทำลายตัวเองออกจากฉาก
            if (energy <= 0)
            {
                Debug.Log($"💥 {Name} defeated!");
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
