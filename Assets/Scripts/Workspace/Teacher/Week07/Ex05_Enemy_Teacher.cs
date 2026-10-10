using UnityEngine;

namespace Week07.Teacher.Game
{
    public class Enemy : Character
    {
        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Enemy";
            }
            if (energy <= 0)
            {
                energy = 10;
            }
            if (attackPoint <= 0)
            {
                attackPoint = 5;
            }
        }

        public override void Hit(Week07.Game.Player player = null)
        {
            if (energy <= 0)
            {
                return;
            }

            if (player != null)
            {
                player.TakeDamage(attackPoint);
                return;
            }

            var map = mapGenerator as MapGenerator;
            if (map != null && map.player != null)
            {
                this.Attack(map.player, attackPoint);
            }
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Attack(this, player.attackPoint);
                if (energy > 0)
                {
                    Hit();
                    player.positionX = player.previousPositionX;
                    player.positionY = player.previousPositionY;
                    player.transform.position = new Vector3(player.positionX, player.positionY, 0);
                }
                else
                {
                    var map = mapGenerator as MapGenerator;
                    if (map != null && map.mapData != null)
                    {
                        map.mapData[positionX, positionY] = 0;
                    }
                }
            }
        }
    }
}

namespace Week07.Teacher.Ex05
{
    public class Enemy : Week07.Teacher.Game.Enemy { }
}
