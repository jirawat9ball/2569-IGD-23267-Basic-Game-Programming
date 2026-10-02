using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    /// <summary>
    /// สคริปต์ไอเทมดาบเพิ่มพลังโจมตี (ItemSword)
    /// ใช้สอนเรื่อง: การตรวจจับการชนด้วย OnTriggerEnter2D และเรียกใช้เมธอด IncreaseAttack() ของ Player ผ่าน GetComponent
    /// </summary>
    public class ItemSword : MonoBehaviour
    {
        #region 1. ข้อมูลของไอเทม (Variables / Fields)

        [Header("ข้อมูลทั่วไป")]
        public string Name = "Sword";

        [Header("ตำแหน่งบนแผนที่ (Grid Position)")]
        public int positionX;
        public int positionY;
        [HideInInspector] public MapGenerator mapGenerator;

        [Header("คุณสมบัติของไอเทม")]
        public int attackBonus = 10; // พลังโจมตีที่จะเพิ่มให้ผู้เล่น

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
                // เรียกเมธอด IncreaseAttack() ของ Player เพื่อเพิ่มพลังโจมตี
                Debug.Log($"⚔️ Picked up {Name}! +{attackBonus} Attack");
                player.IncreaseAttack(attackBonus);

                // ทำลายไอเทมออกจากฉากหลังจากถูกเก็บแล้ว
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
