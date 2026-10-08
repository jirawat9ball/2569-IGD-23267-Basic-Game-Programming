using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Teacher.Game
{
    public class ItemSword : Week07.Game.Identity
    {
        public int attackBonus = 10;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Sword";
            }
        }

        public override void Hit()
        {
            Debug.Log($"You got {Name} : {attackBonus}");

            var map = mapGenerator as MapGenerator;
            if (map != null && map.player != null)
            {
                map.player.IncreaseAttack(attackBonus);
            }

            if (map != null && map.mapData != null)
            {
                map.mapData[positionX, positionY] = 0;
            }

            DestroySafe(gameObject);
        }
    }
}

namespace Week07.Teacher.Ex07
{
    public class ItemSword : Week07.Teacher.Game.ItemSword { }
}
