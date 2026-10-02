using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    /// <summary>
    /// สคริปต์ประตูทางออก (Exit)
    /// ใช้สอนเรื่อง: การตรวจจับการชนด้วย OnTriggerEnter2D และเงื่อนไขการชนะเกม (Win Condition)
    /// </summary>
    public class Exit : MonoBehaviour
    {
        #region 1. ข้อมูลของทางออก (Variables / Fields)

        [Header("ตำแหน่งบนแผนที่ (Grid Position)")]
        public int positionX;
        public int positionY;
        [HideInInspector] public MapGenerator mapGenerator;

        #endregion

        #region 2. การตรวจจับการชน (Trigger Detection)

        private void OnTriggerEnter2D(Collider2D other)
        {
            // แสดง Log แจ้งเตือนเมื่อเกิดการชน
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            // ตรวจสอบว่าผู้เล่นเดินมาถึงทางออกหรือยัง
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("🎉 You Win! Reached the exit!");
            }
        }

        #endregion
    }
}
