using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    /// <summary>
    /// สคริปต์กับดักหนาม (Trap)
    /// ใช้สอนเรื่อง: การตรวจจับการชนด้วย OnTriggerEnter2D และการลดพลังชีวิตของผู้เล่นผ่าน player.TakeDamage()
    /// </summary>
    public class Trap : MonoBehaviour
    {
        #region 1. ข้อมูลของกับดัก (Variables / Fields)

        [Header("ข้อมูลทั่วไป")]
        public string Name = "Trap";

        [Header("ตำแหน่งบนแผนที่ (Grid Position)")]
        public int positionX;
        public int positionY;
        [HideInInspector] public MapGenerator mapGenerator;

        [Header("คุณสมบัติของกับดัก")]
        public int damage = 5; // ดาเมจที่จะสร้างแก่ผู้เล่นเมื่อเดินเหยียบ

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
                Debug.Log($"⚠️ Stepped on {Name}! -{damage} Energy");
                player.TakeDamage(damage);
            }
        }

        #endregion
    }
}
