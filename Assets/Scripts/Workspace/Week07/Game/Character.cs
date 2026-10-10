using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    public class Character : Identity
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

            // ขยับพิกัดตัวละครไปยังช่องเป้าหมาย (เหมือน Week 06)
            positionX = toX;
            positionY = toY;
            transform.position = new Vector3(positionX, positionY, 0);

            // ถ้าเป็นช่องว่าง (ไม่มีไอเทม/สิ่งกีดขวาง) จะเสียพลังงาน 1 หน่วย
            if (!hasItemOrObstacle)
            {
                TakeDamage(1);
            }

            // ส่งสัญญาณ Trigger ชนกับวัตถุในช่องเป้าหมาย ให้ OnTriggerEnter2D ทำงาน
            TriggerAt(toX, toY);
        }

        public void TriggerAt(int x, int y)
        {
            Identity target = GetIdentityAt(x, y);
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

        public Identity GetIdentityAt(int x, int y)
        {
            if (mapGenerator == null) return null;

            if (mapGenerator.enemies != null && x >= 0 && x < mapGenerator.Row && y >= 0 && y < mapGenerator.Col && mapGenerator.enemies[x, y] != null)
                return mapGenerator.enemies[x, y];

            if (mapGenerator.walls != null && x >= 0 && x < mapGenerator.Row && y >= 0 && y < mapGenerator.Col && mapGenerator.walls[x, y] != null)
                return mapGenerator.walls[x, y];

            if (mapGenerator.potions != null && x >= 0 && x < mapGenerator.Row && y >= 0 && y < mapGenerator.Col && mapGenerator.potions[x, y] != null)
                return mapGenerator.potions[x, y];

            if (mapGenerator.swords != null && x >= 0 && x < mapGenerator.Row && y >= 0 && y < mapGenerator.Col && mapGenerator.swords[x, y] != null)
                return mapGenerator.swords[x, y];

            if (mapGenerator.chests != null && x >= 0 && x < mapGenerator.Row && y >= 0 && y < mapGenerator.Col && mapGenerator.chests[x, y] != null)
                return mapGenerator.chests[x, y];

            if (mapGenerator.exitObject != null && mapGenerator.exitObject.positionX == x && mapGenerator.exitObject.positionY == y)
                return mapGenerator.exitObject;

            return null;
        }

        public bool HasSomeObject(int x, int y)
        {
            int mapdata = mapGenerator.GetMapData(x, y);
            return mapdata != mapGenerator.empty;
        }

        public bool IsDemonWall(int x, int y)
        {
            int mapdata = mapGenerator.GetMapData(x, y);
            return mapdata == mapGenerator.demonWall;
        }

        public bool IsEnemy(int x, int y)
        {
            int mapdata = mapGenerator.GetMapData(x, y);
            return mapdata == mapGenerator.enemy;
        }

        public bool IsSword(int x, int y)
        {
            int mapdata = mapGenerator.GetMapData(x, y);
            return mapdata == mapGenerator.sword;
        }

        public bool IsPotion(int x, int y)
        {
            int mapdata = mapGenerator.GetMapData(x, y);
            return mapdata == mapGenerator.potion;
        }

        public bool IsChest(int x, int y)
        {
            int mapdata = mapGenerator.GetMapData(x, y);
            return mapdata == mapGenerator.chest;
        }

        public bool IsExit(int x, int y)
        {
            int mapdata = mapGenerator.GetMapData(x, y);
            return mapdata == mapGenerator.exit;
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
