using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    /// <summary>
    /// ตัวผู้เล่น — จัดการการควบคุม การเดิน เลือด และพฤติกรรมทั้งหมดในคลาสเดียว (ยังไม่ใช้การสืบทอด)
    /// </summary>
    public class Player : MonoBehaviour
    {
        [Header("ข้อมูลตัวละคร")]
        public string Name = "Player";
        public int positionX;
        public int positionY;
        public MapGenerator mapGenerator;

        [Header("สถานะตัวละคร")]
        public int energy = 20;
        public int attackPoint = 10;

        private void Awake()
        {
            if (energy <= 0)
            {
                energy = 20;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector2.right);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                Move(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector2.down);
            }
        }

        #region การเดินและการเคลื่อนที่

        /// <summary>เดินไปยังทิศทางที่กำหนด</summary>
        public void Move(Vector2 direction)
        {
            if (!CanMove(direction)) return;

            int toX = (int)(positionX + direction.x);
            int toY = (int)(positionY + direction.y);

            if (HasSomeObject(toX, toY))
            {
                if (IsPotion(toX, toY))
                {
                    if (mapGenerator != null && mapGenerator.potions != null && mapGenerator.potions[toX, toY] != null)
                    {
                        mapGenerator.potions[toX, toY].Hit();
                    }
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (IsDemonWall(toX, toY))
                {
                    if (mapGenerator != null && mapGenerator.walls != null && mapGenerator.walls[toX, toY] != null)
                    {
                        mapGenerator.walls[toX, toY].Hit();
                    }
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

        /// <summary>Method Overloading: รับพารามิเตอร์แกน x และ y</summary>
        public void Move(float x, float y)
        {
            Move(new Vector2(x, y));
        }

        public bool HasSomeObject(int x, int y)
        {
            if (mapGenerator == null) return false;
            string mapdata = mapGenerator.GetMapData(x, y);
            return mapdata != mapGenerator.empty;
        }

        public bool IsDemonWall(int x, int y)
        {
            if (mapGenerator == null) return false;
            string mapdata = mapGenerator.GetMapData(x, y);
            return mapdata == mapGenerator.demonWall;
        }

        public bool IsPotion(int x, int y)
        {
            if (mapGenerator == null) return false;
            string mapdata = mapGenerator.GetMapData(x, y);
            return mapdata == mapGenerator.potion || mapdata == mapGenerator.bonuesPotion;
        }

        public bool IsExit(int x, int y)
        {
            if (mapGenerator == null) return false;
            string mapdata = mapGenerator.GetMapData(x, y);
            return mapdata == mapGenerator.exit;
        }

        /// <summary>ตรวจสอบว่าทิศทางที่จะเดินไปอยู่ในขอบเขตแผนที่หรือไม่</summary>
        public bool CanMove(Vector2 direction)
        {
            int targetX = (int)(positionX + direction.x);
            int targetY = (int)(positionY + direction.y);

            if (mapGenerator != null)
            {
                return targetX >= 0 && targetX < mapGenerator.Row && targetY >= 0 && targetY < mapGenerator.Col;
            }

            return true;
        }

        #endregion

        #region การต่อสู้และพลังชีวิต

        public void Attack(Enemy target, int damage)
        {
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

        /// <summary>รับดาเมจ ลด energy และแสดงผล</summary>
        public void TakeDamage(int Damage)
        {
            energy -= Damage;
            if (energy < 0) energy = 0;
            Debug.Log("Current Energy : " + energy);
            CheckDead();
        }

        /// <summary>Method Overloading: รับชื่อผู้โจมตีด้วย</summary>
        public void TakeDamage(int Damage, string attacker)
        {
            Debug.Log("Attacked by " + attacker);
            TakeDamage(Damage);
        }

        public void Heal(int healPoint)
        {
            Heal(healPoint, false);
        }

        public void Heal(int healPoint, bool bonus)
        {
            energy += healPoint * (bonus ? 2 : 1);
        }

        /// <summary>เพิ่มเลือดตามค่าเริ่มต้น (10)</summary>
        public void Heal()
        {
            Heal(10);
        }

        public void IncreaseAttack(int value)
        {
            attackPoint += value;
        }

        /// <summary>ตรวจว่าผู้เล่นตายหรือยัง</summary>
        protected void CheckDead()
        {
            if (energy <= 0)
            {
                Debug.Log("You Lose");
                DestroySafe(gameObject);
            }
        }

        public int GetEnergy()
        {
            return energy;
        }

        public string GetStatus()
        {
            return "Player Energy: " + energy;
        }

        protected static void DestroySafe(GameObject target)
        {
            if (target == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                Destroy(target);
            }
            else
            {
                target.SetActive(false);
            }
        }

        #region การตรวจจับการชนด้วย Trigger (OnTriggerEnter)

        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleTriggerEnter(other.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleTriggerEnter(other.gameObject);
        }

        private void HandleTriggerEnter(GameObject target)
        {
            if (target == null) return;

            // ตรวจสอบการชนกับไอเทมยา
            ItemPotion potion = target.GetComponent<ItemPotion>();
            if (potion != null)
            {
                potion.Hit();
                return;
            }

            // ตรวจสอบการชนกับไอเทมดาบ
            ItemSword sword = target.GetComponent<ItemSword>();
            if (sword != null)
            {
                sword.Hit();
                return;
            }

            // ตรวจสอบการชนกับศัตรู
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                Attack(enemy, attackPoint);
                enemy.Hit();
                return;
            }

            // ตรวจสอบการชนกับกำแพง
            Wall wall = target.GetComponent<Wall>();
            if (wall != null)
            {
                wall.Hit();
                return;
            }
        }

        #endregion

        #endregion
    }
}

