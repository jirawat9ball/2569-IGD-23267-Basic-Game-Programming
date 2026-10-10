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
            // ===== student code starts HERE =====
            if (isTrapped)
            {
                Debug.Log("You are trapped! Cannot move for 1 turn.");
                isTrapped = false;
                return;
            }

            if (!CanMove(direction)) return;

            previousPositionX = positionX;
            previousPositionY = positionY;

            base.Move(direction);
            // ===== student code ends HERE =====
        }

        public void Move(float x, float y)
        {
            Move(new Vector2(x, y));
        }

        public bool CanMove(Vector2 direction)
        {
            // ===== student code starts HERE =====
            int targetX = (int)(positionX + direction.x);
            int targetY = (int)(positionY + direction.y);

            var map = mapGenerator as MapGenerator;
            int row = map != null ? map.Row : MapGenerator.MapSize;
            int col = map != null ? map.Col : MapGenerator.MapSize;

            return targetX >= 0 && targetX < row && targetY >= 0 && targetY < col;
            // ===== student code ends HERE =====
        }

        public void RevertPosition()
        {
            // ===== student code starts HERE =====
            positionX = previousPositionX;
            positionY = previousPositionY;
            transform.position = new Vector3(positionX, positionY, 0);
            energy += 1;
            // ===== student code ends HERE =====
        }

        public int GetEnergy()
        {
            return energy;
        }
    }
}
