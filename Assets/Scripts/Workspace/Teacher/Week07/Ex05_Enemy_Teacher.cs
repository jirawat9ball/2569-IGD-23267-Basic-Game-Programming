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

        public override void Hit()
        {
            if (energy <= 0)
            {
                return;
            }

            var map = mapGenerator as MapGenerator;
            if (map != null && map.player != null)
            {
                this.Attack(map.player, attackPoint);
            }
        }
    }
}

namespace Week07.Teacher.Ex05
{
    public class Enemy : Week07.Teacher.Game.Enemy { }
}
