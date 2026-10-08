using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Teacher.Game
{
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

        public override void Move(Vector2 direction)
        {
            if (isTrapped)
            {
                Debug.Log("⛓️ You are trapped! Cannot move for 1 turn.");
                isTrapped = false;
                return;
            }

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

            var map = mapGenerator as MapGenerator;
            int row = map != null ? map.Row : MapGenerator.MapSize;
            int col = map != null ? map.Col : MapGenerator.MapSize;

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

namespace Week07.Teacher.Ex04
{
    public class Player : Week07.Teacher.Game.Player { }
}
