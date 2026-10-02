using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.Ex04
{
    /// <summary>
    /// Assignment ข้อ 4: การสร้างคลาส Exit สำหรับตรวจจับเงื่อนไขการชนะเกม
    /// </summary>
    public class Exit : MonoBehaviour
    {
        // Guideline:
        // 1. ประกาศฟิลด์แบบ public ได้แก่:
        //    - positionX (int)
        //    - positionY (int)
        public int positionX;
        public int positionY;

        // Guideline:
        // 2. เขียนเมธอด OnTriggerEnter2D(Collider2D other) แบบ private:
        //    - แสดงผลข้อความ "[Trigger] {gameObject.name} collided with {other.gameObject.name}"
        //    - ใช้ other.GetComponent<Player>() เพื่อตรวจหาตัวผู้เล่น
        //    - หากพบผู้เล่น ให้แสดงผลข้อความ "🎉 You Win! Reached the exit!"
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("🎉 You Win! Reached the exit!");
            }
        }
    }
}
