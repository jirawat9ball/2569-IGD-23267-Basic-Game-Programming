using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    public class ItemSword : MonoBehaviour
    {
        public string Name = "Sword";
        public int positionX;
        public int positionY;
        public MapGenerator mapGenerator;

        public int attackBonus = 10;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            // ใช้ GetComponent เพื่อเข้าถึงและเรียกใช้ความสามารถของ Player โดยตรง
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log($"You got {Name} : {attackBonus}");
                player.IncreaseAttack(attackBonus);
                Destroy(gameObject);
            }
        }

        public void Hit()
        {
            Player player = (mapGenerator != null) ? mapGenerator.player : null;
            if (player != null)
            {
                Debug.Log($"You got {Name} : {attackBonus}");
                player.IncreaseAttack(attackBonus);
            }
            Destroy(gameObject);
        }
    }
}

