using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// ประตูทางออกของเกม สืบทอดจาก Identity
    /// เมื่อผู้เล่นเดินมาถึง (Hit) จะแสดงข้อความประกาศชัยชนะ
    /// </summary>
    public class Exit : MonoBehaviour
    {
        public string Name = "Exit";
        public int positionX;
        public int positionY;
        public MapGenerator mapGenerator;
        public bool isLevelClear = false;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Exit";
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                isLevelClear = true;
                if (player != null) player.enabled = false;
                Debug.Log("Level Complete! You reached the exit!");
            }
        }

        public void Hit(Player player = null)
        {
        }


    }
}
