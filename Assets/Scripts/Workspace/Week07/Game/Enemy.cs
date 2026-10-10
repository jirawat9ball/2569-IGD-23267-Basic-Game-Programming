using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// สคริปต์ศัตรู (Enemy) - ยกมาจาก Week 06
    /// 
    /// 🎯 โจทย์ Week 07 (ข้อ 5): ปรับปรุงเป็น OOP
    /// 1. เปลี่ยนการสืบทอดจาก MonoBehaviour ให้สืบทอดจาก Character:
    ///    public class Enemy : Character
    /// 2. ลบตัวแปรซ้ำซ้อน (Name, energy, attackPoint, positionX, positionY, mapGenerator) ออก เนื่องจาก Character และ Identity มีให้อยู่แล้ว
    /// 3. ลบเมธอด Attack, TakeDamage, OnTriggerEnter2D ออก (ใช้ระบบของ Character แทน)
    /// 4. เขียน override void Hit() เพื่อตีสวนผู้เล่นเมื่อถูกโจมตี
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        public string Name = "Enemy";
        public int positionX;
        public int positionY;
        public int energy = 10;
        public int attackPoint = 5;
        public MapGenerator mapGenerator;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Enemy";
            }
            if (energy <= 0)
            {
                energy = 10;
            }
            if (attackPoint <= 0)
            {
                attackPoint = 5;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (energy > 0)
                {
                    Debug.Log($"{Name} attacks {player.Name}!");
                    Attack(player, attackPoint);
                }
            }
        }

        // ===== student code starts HERE =====
        // 🎯 ข้อ 5: รีแฟกเตอร์จาก OnTriggerEnter2D มาเป็น Hit()
        public void Hit(Player player = null)
        {
        }
        // ===== student code ends HERE =====

        public void Attack(Player target, int damage)
        {
            target?.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            energy -= damage;
        }
    }
}
