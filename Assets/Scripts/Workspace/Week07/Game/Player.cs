using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// สคริปต์ควบคุมตัวละครผู้เล่น (Player)
    /// สืบทอดคุณสมบัติทั้งหมดจาก Character (ซึ่งสืบทอดจาก Identity)
    /// ปรับปรุงจาก Week 06 โดยนำระบบควบคุมการเดินด้วยคีย์บอร์ด (WASD / ลูกศร)
    /// และการตรวจจับสถานะมาต่อยอดบนโครงสร้าง OOP
    /// </summary>
    public class Player : Character
    {
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

        public override void Move(Vector2 direction)
        {
            // หากติดกับดัก จะเดินไม่ได้ 1 ครั้ง
            if (isTrapped)
            {
                Debug.Log("⛓️ You are trapped! Cannot move for 1 turn.");
                isTrapped = false;
                return;
            }

            // ตรวจสอบขอบเขตแผนที่ก่อนเดิน
            if (!CanMove(direction)) return;

            previousPositionX = positionX;
            previousPositionY = positionY;

            base.Move(direction);
        }

        public void Move(float x, float y)
        {
            Move(new Vector2(x, y));
        }

        public bool CanMove(Vector2 direction)
        {
            int targetX = (int)(positionX + direction.x);
            int targetY = (int)(positionY + direction.y);

            int row = mapGenerator != null ? mapGenerator.Row : MapGenerator.MapSize;
            int col = mapGenerator != null ? mapGenerator.Col : MapGenerator.MapSize;

            return targetX >= 0 && targetX < row && targetY >= 0 && targetY < col;
        }

        public void RevertPosition()
        {
            positionX = previousPositionX;
            positionY = previousPositionY;
            transform.position = new Vector3(positionX, positionY, 0);
            energy += 1;
        }

        public int GetEnergy()
        {
            return energy;
        }
    }
}
