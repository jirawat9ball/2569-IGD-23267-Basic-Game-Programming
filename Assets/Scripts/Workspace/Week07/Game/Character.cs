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

            if (HasSomeObject(toX, toY))
            {
                // ===== student code starts HERE =====
                // Guideline:
                // 1. ถ้าช่องนั้นเป็นยา (IsPotion) -> เรียก mapGenerator.potions[toX, toY].Hit();
                //    แล้วขยับตัวละครเข้าไปที่ช่องนั้น (กำหนด positionX, positionY และ transform.position)
                // 2. ถ้าเป็นดาบ (IsSword) -> เรียก mapGenerator.swords[toX, toY].Hit(); แล้วขยับเข้าไปเหมือนกัน
                // 3. ถ้าเป็นศัตรู (IsEnemy) -> ทำตามลำดับนี้
                //    3.1 เก็บศัตรูไว้ในตัวแปร: Enemy e = mapGenerator.enemies[toX, toY];
                //    3.2 ให้เราตีศัตรูก่อน: this.Attack(e, attackPoint);
                //    3.3 ถ้าศัตรูยังไม่ตาย (e.energy > 0) -> ให้ศัตรูตีสวนกลับด้วย mapGenerator.enemies[toX, toY].Hit();
                //    3.4 ถ้าศัตรูตายแล้ว -> ขยับตัวละครเข้าไปที่ช่องนั้น
                // 4. ถ้าเป็นกำแพง (IsDemonWall) -> เรียก mapGenerator.walls[toX, toY].Hit(); (ไม่ขยับเข้าช่องกำแพง)
                // 5. ถ้าเป็นกล่องสมบัติ (IsChest) -> เรียก mapGenerator.chests[toX, toY].Hit(); แล้วขยับตัวละครเข้าไป
                // 6. ถ้าเป็นทางออก (IsExit) -> เรียก mapGenerator.exitObject.Hit(); แล้วขยับตัวละครเข้าไป
                // หมายเหตุ: ช่องที่มีของวางอยู่จะไม่โดนหัก energy (ต่างจากช่องว่างด้านล่าง)

                // ===== student code ends HERE =====
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
