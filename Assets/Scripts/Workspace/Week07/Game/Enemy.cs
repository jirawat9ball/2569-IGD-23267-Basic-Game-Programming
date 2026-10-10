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
    /// 2. ลบตัวแปรซ้ำซ้อน (Name, energy, attackPoint) ออก เนื่องจาก Character และ Identity มีให้อยู่แล้ว
    /// 3. ลบเมธอด Attack, TakeDamage, OnTriggerEnter2D ออก (ใช้ระบบของ Character แทน)
    /// 4. เขียน override void Hit() เพื่อตีสวนผู้เล่นเมื่อถูกโจมตี
    /// </summary>
    public class Enemy : Character
    {
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

        // ===== student code starts HERE =====
        public override void Hit(Player player = null)
        {
            player.Attack(this, player.attackPoint);
                if (energy > 0)
                {
                    Hit(player);
                    player.positionX = player.previousPositionX;
                    player.positionY = player.previousPositionY;
                    player.transform.position = new Vector3(player.positionX, player.positionY, 0);
                }
                else
                {
                    var map = mapGenerator as MapGenerator;
                    if (map != null && map.mapData != null)
                    {
                        map.mapData[positionX, positionY] = 0;
                    }
                }
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Hit(player);
            }
        }
        // ===== student code ends HERE =====
    }
}
