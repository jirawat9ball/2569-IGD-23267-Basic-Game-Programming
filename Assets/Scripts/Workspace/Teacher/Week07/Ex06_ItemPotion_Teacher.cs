using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Teacher.Game
{
    public class ItemPotion : Week07.Game.Identity
    {
        public int healPoint = 10;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Potion";
            }
        }

        public override void Hit(Week07.Game.Player player = null)
        {
            Debug.Log($"You got {Name} : {healPoint}");

            if (player != null)
            {
                player.Heal(healPoint);
            }
            else
            {
                var map = mapGenerator as MapGenerator;
                if (map != null && map.player != null)
                {
                    map.player.Heal(healPoint);
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

namespace Week07.Teacher.Ex06
{
    public class ItemPotion : Week07.Teacher.Game.ItemPotion { }
}
