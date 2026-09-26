using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week05
{
    public class Player_Teacher_Week05 : MonoBehaviour
    {
        [Header("ตัวแปรของตัวละคร")]
        public int energy = 20;

        [Header("ขอบเขตแผนที่")]
        public int columns = 8;
        public int rows = 8;

        private const string LineSeparator = "============================";

        private void Start()
        {
            // =========================================================================
            // ทดสอบเรียก Method ของ Player ตามแนวคิด ข้อ 1 และ ข้อ 2
            // =========================================================================

            // ข้อ 1: ทดสอบเรียก Method แบบ void, Parameter, Overloading และ Default Parameter
            Move(Vector2.right);                      // Move(Vector2 direction)
            Move(0f, 1f);                             // Move(float x, float y) - Overloading
            TakeDamage(5);                            // TakeDamage(int Damage)
            TakeDamage(3, "Slime");                   // TakeDamage(int Damage, string attacker) - Overloading
            Heal();                                   // Heal() - Default Parameter (10)
            Heal(5);                                  // Heal(int healPoint)

            Debug.Log(LineSeparator);

            // ข้อ 2: ทดสอบเรียก Method แบบมีค่าส่งกลับ (Return Type)
            Debug.Log("GetEnergy() = " + GetEnergy());                           // Return Type: int
            Debug.Log("GetStatus() = " + GetStatus());                           // Return Type: string
            Debug.Log("CanMove(Vector2.right) = " + CanMove(Vector2.right));     // Return Type: bool
            Debug.Log("CanMove(Vector2.left) = " + CanMove(Vector2.left));       // Return Type: bool
        }

        private void Update()
        {
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

        #region ข้อ 1: Method แบบ void, Parameter, Overloading และ Default Parameter

        // ข้อ 3: Method แบบ void + Parameter
        public void Move(Vector2 direction)
        {
            if (!CanMove(direction)) return;

            transform.position += new Vector3(direction.x, direction.y, 0f);
            energy -= 1;
        }

        // ข้อ 3.1: Method Overloading (รับพารามิเตอร์ float x, float y)
        public void Move(float x, float y)
        {
            Move(new Vector2(x, y));
        }

        // ข้อ 4: Method แบบ void + Parameter (ลด energy และเรียก CheckDead)
        public void TakeDamage(int Damage)
        {
            energy -= Damage;
            if (energy < 0)
            {
                energy = 0;
            }

            Debug.Log("Current Energy : " + energy);
            CheckDead();
        }

        // ข้อ 4.1: Method Overloading (รับพารามิเตอร์ int Damage, string attacker)
        public void TakeDamage(int Damage, string attacker)
        {
            Debug.Log("Attacked by " + attacker);
            TakeDamage(Damage);
        }

        // ข้อ 5: Method Scope (private void ไม่รับพารามิเตอร์)
        private void CheckDead()
        {
            if (energy <= 0)
            {
                Debug.Log("You Lose");
                Destroy(gameObject);
            }
        }

        // ข้อ 6: Method แบบ Default Parameter (ค่าเริ่มต้น 10)
        public void Heal(int healPoint = 10)
        {
            energy += healPoint;
        }

        #endregion

        #region ข้อ 2: Method แบบมีค่าส่งกลับ (Return Type)

        // ข้อ 7: Method แบบมีค่าส่งกลับ (Return Type bool) - ตรวจสอบว่าตำแหน่งถัดไปไม่เดินออกนอกแผนที่
        public bool CanMove(Vector2 direction)
        {
            Vector2 targetPos = (Vector2)transform.position + direction;
            return targetPos.x >= 0 && targetPos.x < columns && targetPos.y >= 0 && targetPos.y < rows;
        }

        // ข้อ 8: Method แบบมีค่าส่งกลับ (Return Type int)
        public int GetEnergy()
        {
            return energy;
        }

        // ข้อ 9: Method แบบมีค่าส่งกลับ (Return Type string)
        public string GetStatus()
        {
            return "Player Energy: " + energy;
        }

        #endregion
    }
}
