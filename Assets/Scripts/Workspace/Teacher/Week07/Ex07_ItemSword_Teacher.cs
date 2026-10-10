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

        public override void Hit(Week07.Game.Player player = null)
        {
            Debug.Log($"You got {Name} : {attackBonus}");

            if (player != null)
            {
                player.IncreaseAttack(attackBonus);
            }
            else
            {
                var map = mapGenerator as MapGenerator;
                if (map != null && map.player != null)
                {
                    map.player.IncreaseAttack(attackBonus);
                }
            }

            if (mapGenerator != null && mapGenerator.mapData != null)
            {
                mapGenerator.mapData[positionX, positionY] = 0;
            }

            DestroySafe(gameObject);
        }
    }
}

namespace Week07.Teacher.Ex07
{
    public class ItemSword : Week07.Teacher.Game.ItemSword { }
}
