using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.Ex03
{
    /// <summary>
    /// Assignment ข้อ 3: การสร้างคลาส Enemy สำหรับตรวจจับการชนและระบบต่อสู้
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        // Guideline:
        // 1. ประกาศฟิลด์แบบ public ได้แก่:
        //    - name (string) กำหนดค่าเริ่มต้นเป็น "Enemy"
        //    - energy (int) กำหนดค่าเริ่มต้นเป็น 10 (เลือดน้อยกว่า Player)
        //    - attackPoint (int) กำหนดค่าเริ่มต้นเป็น 5 (ดาเมจน้อยกว่า Player)
        public string name = "Enemy";
        public int energy = 10;
        public int attackPoint = 5;

        // Guideline:
        // 2. ใน Awake() กำหนดค่าเริ่มต้นหาก energy <= 0 ให้เป็น 10
        private void Awake()
        {
            if (energy <= 0)
            {
                energy = 10;
            }
        }

        // Guideline:
        // 3. เขียนเมธอด Attack(Player target, int damage) แบบ public:
        //    - ตรวจสอบว่า target != null หรือไม่
        //    - แสดงผลข้อความ "{name} attacks {target.Name} with {damage} damage!"
        //    - เรียกใช้ target.TakeDamage(damage, name)
        public void Attack(Player target, int damage)
        {
            if (target != null)
            {
                Debug.Log($"{name} attacks {target.Name} with {damage} damage!");
                target.TakeDamage(damage, name);
            }
        }

        // Guideline:
        // 4. เขียนเมธอด TakeDamage(int damage) แบบ public:
        //    - ลดค่า energy ด้วย damage
        //    - แสดงผลข้อความ "{name} takes {damage} damage! Remaining HP: {energy}"
        //    - ถ้า energy <= 0 แสดงผล "💥 {name} defeated!" และทำลายตัวเอง Destroy(gameObject)
        public void TakeDamage(int damage)
        {
            energy -= damage;
            Debug.Log($"{name} takes {damage} damage! Remaining HP: {energy}");

            if (energy <= 0)
            {
                Debug.Log($"💥 {name} defeated!");
                Destroy(gameObject);
            }
        }

        // Guideline:
        // 5. เขียนเมธอด OnTriggerEnter2D(Collider2D other) แบบ private:
        //    - แสดงผลข้อความ "[Trigger] {gameObject.name} collided with {other.gameObject.name}"
        //    - ใช้ other.GetComponent<Player>() เพื่อตรวจหาตัวผู้เล่น
        //    - หากพบผู้เล่น ให้เรียก Attack(player, attackPoint) และ TakeDamage(player.attackPoint)
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
