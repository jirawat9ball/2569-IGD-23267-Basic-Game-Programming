using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// กำแพงสิ่งกีดขวางในแผนที่ สืบทอดจาก Identity
    /// มีค่าความทนทาน (durability) เมื่อถูกโจมตี/ชน จะลดความทนทานลง และทำลายตัวเองเมื่อ durability หมด
    /// </summary>
    public class Wall : Identity
    {
        public int durability = 3;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Wall";
            }
        }

        public override void Hit()
        {
            durability--;
            Debug.Log($"Hit Wall {Name}! Remaining durability: {durability}");
            if (durability <= 0)
            {
                Debug.Log($"💥 Wall {Name} destroyed!");
                if (mapGenerator != null && mapGenerator.mapData != null)
                {
                    mapGenerator.mapData[positionX, positionY] = 0;
                }
                DestroySafe(gameObject);
            }
        }
    }
}
