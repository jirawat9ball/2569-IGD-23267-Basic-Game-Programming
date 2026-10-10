using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// สคริปต์ควบคุมตัวละครผู้เล่น (Player)
    /// 
    /// 🎯 โจทย์ Week 07 (ข้อ 4): ปรับปรุงเป็น OOP
    /// 1. เปลี่ยนการสืบทอดจาก MonoBehaviour ให้สืบทอดจาก Character:
    ///    public class Player : Character
    /// 2. ลบตัวแปรซ้ำซ้อน (Name, positionX, positionY, energy, attackPoint, mapGenerator) ออก เนื่องจาก Character และ Identity มีให้อยู่แล้ว
    /// 3. คงตัวแปรเฉพาะของ Player ไว้: previousPositionX, previousPositionY, isTrapped
    /// 4. เขียน override void Move(Vector2 direction)
    /// 5. เขียนเมธอด CanMove(Vector2 direction)
    /// 6. เขียนเมธอด RevertPosition()
    /// </summary>
    public class Player : MonoBehaviour
    {
        [Header("ข้อมูลและพิกัดตัวละคร")]
        public string Name = "Player";
        public int positionX;
        public int positionY;
        public int energy = 20;
        public int attackPoint = 10;
        public MapGenerator mapGenerator;

        [Header("ตำแหน่งก่อนหน้า (Previous Position)")]
        public int previousPositionX;
        public int previousPositionY;
        public bool isTrapped = false;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Player";
            }
            if (energy <= 0)
            {
                energy = 20;
            }
            if (attackPoint <= 0)
            {
                attackPoint = 10;
            }
        }

        private void Update()
        {
            // รับอินพุตจากแป้นพิมพ์เพื่อบังคับทิศทางการเดินของผู้เล่น
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector2.right);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                Move(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector2.down);
            }
        }

        // ===== student code starts HERE =====
        public void Move(Vector2 direction)
        {
            // Guideline: (ข้อ 4)
            // 1. หากติดกับดัก (isTrapped เป็นจริง) ให้แสดงข้อความ
            //    "You are trapped! Cannot move for 1 turn."
            //    แล้วปลดสถานะ isTrapped = false; และ return ออกทันที
            // 2. ตรวจสอบขอบเขตแผนที่ก่อนเดิน ถ้าเดินไม่ได้ (!CanMove(direction)) ให้ return
            // 3. บันทึกตำแหน่งก่อนหน้า previousPositionX = positionX; และ previousPositionY = positionY;
            // 4. เรียก base.Move(direction);
        }

        public void Move(float x, float y)
        {
            Move(new Vector2(x, y));
        }

        public bool CanMove(Vector2 direction)
        {
            // Guideline: (ข้อ 4)
            // คำนวณหา targetX และ targetY จาก positionX/Y + direction.x/y
            // ดึงจำนวน row และ col จาก mapGenerator (หรือ MapGenerator.MapSize ถ้า mapGenerator เป็น null)
            // คืนค่า true ถ้า targetX และ targetY อยู่ในช่วง 0 ถึง row/col
            return false;
        }

        public void RevertPosition()
        {
            // Guideline: (ข้อ 4)
            // 1. คืนค่าตำแหน่ง: positionX = previousPositionX; positionY = previousPositionY;
            // 2. อัปเดต transform.position = new Vector3(positionX, positionY, 0);
            // 3. คืนพลังงาน: energy += 1;
        }
        // ===== student code ends HERE =====

        public int GetEnergy()
        {
            return energy;
        }

        public void Attack(Character target, int damage)
        {
            target?.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            energy -= damage;
        }

        public void Heal(int healPoint)
        {
            energy += healPoint;
        }

        public void IncreaseAttack(int value)
        {
            attackPoint += value;
        }
    }
}
