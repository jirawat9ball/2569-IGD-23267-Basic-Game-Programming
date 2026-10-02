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
            // Guideline:
            // 1. แสดง Log แจ้งเตือนเมื่อเกิดการชน: "[Trigger] <gameObject.name> collided with <other.gameObject.name>"
            // 2. ใช้ other.GetComponent<Player>() เพื่อตรวจสอบว่าชนกับผู้เล่นหรือไม่
            // 3. ถ้าเป็นผู้เล่น ให้แสดง Log ประกาศชัยชนะ: "🎉 You Win! Reached the exit!"

            // Student code starts HERE ...

            // Student code ends HERE ...
        }

        #endregion
    }
}
