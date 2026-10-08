using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.Teacher.Ex03
{
    public class Enemy : MonoBehaviour
    {
        public string Name = "Enemy";
        public int energy = 10;
        public int attackPoint = 5;

        private void Awake()
        {
            if (energy <= 0) energy = 10;
        }

        public void Attack(Player target, int damage)
        {
            if (target != null)
            {
                Debug.Log($"{Name} attacks {target.Name} with {damage} damage!");
                target.TakeDamage(damage, Name);
            }
        }

        public void TakeDamage(int damage)
        {
            energy -= damage;
            Debug.Log($"{Name} takes {damage} damage! Remaining HP: {energy}");

            if (energy <= 0)
            {
                Debug.Log($"{Name} defeated!");
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Attack(player, attackPoint);
                TakeDamage(player.attackPoint);
            }
        }
    }
}
