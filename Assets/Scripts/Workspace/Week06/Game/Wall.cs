using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    /// <summary>
    /// สคริปต์กำแพง / สิ่งกีดขวาง (Wall)
    /// ใช้สอนเรื่อง: การตรวจจับการชนด้วย OnTriggerEnter2D และการลดค่าความทนทาน (Durability)
    /// </summary>
    public class Wall : MonoBehaviour
    {
        #region 1. ข้อมูลของกำแพง (Variables / Fields)

        [Header("ข้อมูลทั่วไป")]
        public string Name = "Wall";

        [Header("ตำแหน่งบนแผนที่ (Grid Position)")]
        public int positionX;
        public int positionY;
        [HideInInspector] public MapGenerator mapGenerator;

        [Header("ค่าสถานะ")]
        public int durability = 2; // ความทนทานของกำแพง (ตี 2 ครั้งพัง)

        #endregion

        #region 2. การตรวจจับการชน (Trigger Detection)

        private void OnTriggerEnter2D(Collider2D other)
        {
            // แสดง Log แจ้งเตือนเมื่อเกิดการชน
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            // ตรวจสอบว่าผู้เล่นเดินมาชนกำแพงหรือไม่
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Hit();
            }
        }

        #endregion

        #region 3. ความเสียหายและการทำลาย (Durability & Destruction)

        /// <summary>
        /// เมื่อกำแพงถูกโจมตี จะลดค่าความทนทานลง
        /// </summary>
        public void Hit()
        {
            durability--;
            Debug.Log($"🧱 {Name} was hit! Remaining durability: {durability}");

            // หากความทนทานหมด ให้ทำลายกำแพงออกจากฉาก
            if (durability <= 0)
            {
                Debug.Log($"💥 {Name} destroyed!");
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
