using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    /// <summary>
    /// ตัวผู้เล่น — สืบทอดความสามารถจาก Character และเพิ่มความสามารถของ Week 05 (การควบคุม, ตรวจสอบการเดิน, และ Overloading)
    /// </summary>
    public class Player : Character
    {
        private void Awake()
        {
            if (energy <= 0)
            {
                energy = 20;
            }
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

        #region ความสามารถจาก Week 05: Method แบบ void, Parameter, Overloading และ Default Parameter

        /// <summary>เดินไปยังทิศทางที่กำหนด โดยตรวจ CanMove ก่อน</summary>
        public override void Move(Vector2 direction)
        {
            if (!CanMove(direction)) return;
            base.Move(direction);
        }

        /// <summary>Method Overloading: รับพารามิเตอร์แกน x และ y</summary>
        public void Move(float x, float y)
        {
            Move(new Vector2(x, y));
        }

        /// <summary>รับดาเมจ ลด energy และแสดงผล</summary>
        public override void TakeDamage(int Damage)
        {
            base.TakeDamage(Damage);
            if (energy < 0) energy = 0;
            Debug.Log("Current Energy : " + energy);
        }

        /// <summary>Method Overloading: รับชื่อผู้โจมตีด้วย</summary>
        public void TakeDamage(int Damage, string attacker)
        {
            Debug.Log("Attacked by " + attacker);
            TakeDamage(Damage);
        }

        /// <summary>ตรวจว่าผู้เล่นตายหรือยัง</summary>
        protected override void CheckDead()
        {
            if (energy <= 0)
            {
                Debug.Log("You Lose");
                base.CheckDead();
            }
        }

        /// <summary>เพิ่มเลือดตามค่าเริ่มต้น (10)</summary>
        public void Heal()
        {
            Heal(10);
        }

        #endregion

        #region ความสามารถจาก Week 05: Method แบบมีค่าส่งกลับ (Return Type)

        /// <summary>ตรวจสอบว่าทิศทางที่จะเดินไปอยู่ในขอบเขตแผนที่หรือไม่</summary>
        public bool CanMove(Vector2 direction)
        {
            int targetX = (int)(positionX + direction.x);
            int targetY = (int)(positionY + direction.y);

            if (mapGenerator != null)
            {
                return targetX >= 0 && targetX < mapGenerator.Row && targetY >= 0 && targetY < mapGenerator.Col;
            }

            return true;
        }

        /// <summary>ส่งกลับค่า energy ปัจจุบัน</summary>
        public int GetEnergy()
        {
            return energy;
        }

        /// <summary>ส่งกลับสถานะ energy ของผู้เล่น</summary>
        public string GetStatus()
        {
            return "Player Energy: " + energy;
        }

        #endregion
    }
}
