using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Teacher.Game
{
    public class Wall : Week07.Game.Identity
    {
        public int durability = 3;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Wall";
            }
        }

        public override void Hit(Week07.Game.Player player = null)
        {
            durability--;
            Debug.Log($"Hit Wall {Name}! Remaining durability: {durability}");
            if (durability <= 0)
            {
                Debug.Log($"Wall {Name} destroyed!");
                if (mapGenerator != null && mapGenerator.mapData != null)
                {
                    mapGenerator.mapData[positionX, positionY] = 0;
                }
                DestroySafe(gameObject);
            }
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Hit();
                player.positionX = player.previousPositionX;
                player.positionY = player.previousPositionY;
                player.transform.position = new Vector3(player.positionX, player.positionY, 0);
            }
        }
    }
}

namespace Week07.Teacher.Ex08
{
    public class Wall : Week07.Teacher.Game.Wall { }
}
