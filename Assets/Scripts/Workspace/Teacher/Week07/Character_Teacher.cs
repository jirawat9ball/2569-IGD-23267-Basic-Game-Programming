using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Teacher.Game
{
    public class Character : Week07.Game.Identity
    {
        public int energy;
        public int attackPoint;

        protected void GetRemainEnergy()
        {
            Debug.Log($"{Name} : {energy}");
        }

        public virtual void Move(Vector2 direction)
        {
            int toX = (int)(positionX + direction.x);
            int toY = (int)(positionY + direction.y);

            bool hasItemOrObstacle = HasSomeObject(toX, toY);

            positionX = toX;
            positionY = toY;
            transform.position = new Vector3(positionX, positionY, 0);

            if (!hasItemOrObstacle)
            {
                TakeDamage(1);
            }

            TriggerAt(toX, toY);
        }

        public void TriggerAt(int x, int y)
        {
            Week07.Game.Identity target = GetIdentityAt(x, y);
            if (target != null && target != this)
            {
                Collider2D col = GetComponent<Collider2D>();
                if (col == null)
                {
                    col = gameObject.AddComponent<BoxCollider2D>();
                }
                target.OnTriggerEnter2D(col);
            }
        }

        public Week07.Game.Identity GetIdentityAt(int x, int y)
        {
            var map = mapGenerator as MapGenerator;
            if (map == null) return null;

            if (map.enemies != null && x >= 0 && x < map.Row && y >= 0 && y < map.Col && map.enemies[x, y] != null)
                return map.enemies[x, y];

            if (map.walls != null && x >= 0 && x < map.Row && y >= 0 && y < map.Col && map.walls[x, y] != null)
                return map.walls[x, y];

            if (map.potions != null && x >= 0 && x < map.Row && y >= 0 && y < map.Col && map.potions[x, y] != null)
                return map.potions[x, y];

            if (map.swords != null && x >= 0 && x < map.Row && y >= 0 && y < map.Col && map.swords[x, y] != null)
                return map.swords[x, y];

            if (map.chests != null && x >= 0 && x < map.Row && y >= 0 && y < map.Col && map.chests[x, y] != null)
                return map.chests[x, y];

            if (map.exitObject != null && map.exitObject.positionX == x && map.exitObject.positionY == y)
                return (object)map.exitObject as Week07.Game.Identity;

            return null;
        }

        public bool HasSomeObject(int x, int y)
        {
            if (mapGenerator == null) return false;
            int mapdata = mapGenerator.GetMapData(x, y);
            return mapdata != mapGenerator.empty;
        }

        public bool IsDemonWall(int x, int y)
        {
            if (mapGenerator == null) return false;
            return mapGenerator.GetMapData(x, y) == mapGenerator.demonWall;
        }

        public bool IsEnemy(int x, int y)
        {
            if (mapGenerator == null) return false;
            return mapGenerator.GetMapData(x, y) == mapGenerator.enemy;
        }

        public bool IsSword(int x, int y)
        {
            if (mapGenerator == null) return false;
            return mapGenerator.GetMapData(x, y) == mapGenerator.sword;
        }

        public bool IsPotion(int x, int y)
        {
            if (mapGenerator == null) return false;
            return mapGenerator.GetMapData(x, y) == mapGenerator.potion;
        }

        public bool IsChest(int x, int y)
        {
            if (mapGenerator == null) return false;
            return mapGenerator.GetMapData(x, y) == mapGenerator.chest;
        }

        public bool IsExit(int x, int y)
        {
            if (mapGenerator == null) return false;
            return mapGenerator.GetMapData(x, y) == mapGenerator.exit;
        }

        public virtual void Attack(Character target, int damage)
        {
            target.TakeDamage(damage);
        }

        public virtual void TakeDamage(int Damage)
        {
            energy -= Damage;
            CheckDead();
        }

        public virtual void Heal(int healPoint)
        {
            Heal(healPoint, false);
        }

        public virtual void Heal(int healPoint, bool Bonuse)
        {
            energy += healPoint * (Bonuse ? 2 : 1);
        }

        public void IncreaseAttack(int value)
        {
            attackPoint += value;
        }

        protected virtual void CheckDead()
        {
            if (energy <= 0)
            {
                DestroySafe(gameObject);
            }
        }
    }
}
