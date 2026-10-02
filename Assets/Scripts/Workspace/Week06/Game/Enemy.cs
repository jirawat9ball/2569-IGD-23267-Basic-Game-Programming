using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    public class Enemy : MonoBehaviour
    {
        public string Name = "Enemy";
        public int positionX;
        public int positionY;
        public MapGenerator mapGenerator;

        public int energy = 20;
        public int attackPoint = 5;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            // ใช้ GetComponent เพื่อเข้าถึงและโจมตี Player โดยตรง
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Attack(player, attackPoint);
            }
        }

        public void Hit(Player player = null)
        {
            if (energy <= 0) return;
            if (player == null && mapGenerator != null) player = mapGenerator.player;
            if (player != null)
            {
                Attack(player, attackPoint);
            }
        }

        public void Attack(Player target, int damage)
        {
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

        public void TakeDamage(int damage)
        {
            energy -= damage;
            if (energy <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}


