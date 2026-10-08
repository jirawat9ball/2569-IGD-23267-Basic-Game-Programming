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

            var map = mapGenerator as MapGenerator;

            if (HasSomeObject(toX, toY))
            {
                if (IsPotion(toX, toY))
                {
                    if (map != null && map.potions != null && map.potions[toX, toY] != null)
                    {
                        map.potions[toX, toY].Hit();
                    }
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (IsSword(toX, toY))
                {
                    if (map != null && map.swords != null && map.swords[toX, toY] != null)
                    {
                        map.swords[toX, toY].Hit();
                    }
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (IsEnemy(toX, toY))
                {
                    if (map != null && map.enemies != null && map.enemies[toX, toY] != null)
                    {
                        Enemy e = map.enemies[toX, toY];
                        this.Attack(e, attackPoint);

                        if (e.energy > 0)
                        {
                            e.Hit();
                        }
                        else
                        {
                            positionX = toX;
                            positionY = toY;
                            transform.position = new Vector3(positionX, positionY, 0);
                        }
                    }
                }
                else if (IsDemonWall(toX, toY))
                {
                    if (map != null && map.walls != null && map.walls[toX, toY] != null)
                    {
                        map.walls[toX, toY].Hit();
                    }
                }
                else if (IsChest(toX, toY))
                {
                    if (map != null && map.chests != null && map.chests[toX, toY] != null)
                    {
                        map.chests[toX, toY].Hit();
                    }
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (IsExit(toX, toY))
                {
                    if (map != null && map.exitObject != null)
                    {
                        map.exitObject.Hit();
                    }
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
            }
            else
            {
                positionX = toX;
                positionY = toY;
                transform.position = new Vector3(positionX, positionY, 0);
                TakeDamage(1);
            }
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
