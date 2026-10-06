using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// ประตูทางออกของเกม สืบทอดจาก Identity
    /// เมื่อผู้เล่นเดินมาถึง (Hit) จะแสดงข้อความประกาศชัยชนะ
    /// </summary>
    public class Exit : Identity
    {
        public bool isLevelClear = false;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Exit";
            }
        }

        public override void Hit()
        {
            isLevelClear = true;
            Debug.Log("🎉 Level Complete! You reached the exit!");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Player p = other.GetComponent<Player>();
            if (p != null)
            {
                Hit();
            }
        }
    }
}
