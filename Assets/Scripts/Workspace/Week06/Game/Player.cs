using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    /// <summary>
    /// สคริปต์ควบคุมตัวละครผู้เล่น (Player)
    /// ใช้สอนพื้นฐานเรื่อง: Class, Variables (Fields), Methods, และการใช้ OnTriggerEnter2D ร่วมกับ GetComponent
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region 1. ข้อมูลและสถานะของตัวละคร (Variables / Fields)

        [Header("ข้อมูลทั่วไป")]
        public string Name = "Player";

        [Header("ตำแหน่งบนแผนที่ (Grid Position)")]
        public int positionX;
        public int positionY;
        public int previousPositionX;
        public int previousPositionY;
        [HideInInspector] public MapGenerator mapGenerator;

        [Header("ค่าสถานะ (Stats)")]
        public int energy = 20;       // พลังงาน/พลังชีวิต (เดิน 1 ก้าว เสีย 1 energy)
        public int attackPoint = 10;  // พลังโจมตีเริ่มต้น
        public bool isTrapped = false; // ติดกับดัก (ทำให้เดินไม่ได้ 1 ครั้ง)

        #endregion

        #region 2. วงจรการทำงานของ Unity (Lifecycle Methods)

        private void Awake()
        {
            // กำหนดค่าพลังงานเริ่มต้นหากยังไม่ได้ตั้งค่า
            if (energy <= 0)
            {
                energy = 20;
            }
        }

        private void Update()
        {
            // รับอินพุตจากแป้นพิมพ์เพื่อบังคับทิศทางการเดินของผู้เล่น
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

        #endregion

        #region 3. ระบบการเคลื่อนที่ (Movement)

        /// <summary>
        /// เดินไปยังทิศทางที่กำหนด (Vector2: บน, ล่าง, ซ้าย, ขวา)
        /// </summary>
        public void Move(Vector2 direction)
        {
            // หากติดกับดัก จะเดินไม่ได้ 1 ครั้ง
            if (isTrapped)
            {
                Debug.Log("⛓️ You are trapped! Cannot move for 1 turn.");
                isTrapped = false; // ปลดกับดักเพื่อให้เดินได้ในตาถัดไป
                return;
            }

            // 1. ตรวจสอบก่อนว่าเดินไปได้หรือไม่ (ติดขอบแมพหรือไม่)
            if (!CanMove(direction)) return;

            // 2. บันทึกตำแหน่งก่อนหน้า
            previousPositionX = positionX;
            previousPositionY = positionY;

            // 3. อัปเดตพิกัดตำแหน่งของผู้เล่น
            positionX += (int)direction.x;
            positionY += (int)direction.y;
            transform.position = new Vector3(positionX, positionY, 0);

            // 4. ทุกครั้งที่ก้าวเดิน จะเสียพลังงาน 1 หน่วย
            TakeDamage(1);
        }

        /// <summary>
        /// ถอยกลับไปตำแหน่งเดิมก่อนเดิน (เช่น เมื่อเดินชนกำแพง ทำให้เดินผ่านไม่ได้)
        /// </summary>
        public void RevertPosition()
        {
            positionX = previousPositionX;
            positionY = previousPositionY;
            transform.position = new Vector3(positionX, positionY, 0);
            energy += 1; // คืนพลังงานที่เสียไปจากการเดินชน
        }

        /// <summary>
        /// เมธอด Overload: เดินด้วยค่าแกน x, y
        /// </summary>
        public void Move(float x, float y)
        {
            Move(new Vector2(x, y));
        }

        /// <summary>
        /// ตรวจสอบว่าพิกัดเป้าหมายอยู่ในขอบเขตแผนที่หรือไม่
        /// </summary>
        public bool CanMove(Vector2 direction)
        {
            int targetX = (int)(positionX + direction.x);
            int targetY = (int)(positionY + direction.y);

            if (mapGenerator != null)
            {
                return targetX >= 0 && targetX < mapGenerator.Row &&
                       targetY >= 0 && targetY < mapGenerator.Col;
            }

            return true;
        }

        #endregion

        #region 4. ระบบความสามารถและพลังชีวิต (Combat & Stats)

        /// <summary>
        /// โจมตีศัตรูเป้าหมาย
        /// </summary>
        public void Attack(MonoBehaviour target, int damage)
        {
            if (target != null)
            {
                var nameVal = target.GetType().GetField("Name")?.GetValue(target)?.ToString() ?? target.name;
                Debug.Log($"{Name} attacks {nameVal} with {damage} damage!");
                target.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }

        /// <summary>
        /// ลดพลังงาน/พลังชีวิตของผู้เล่น
        /// </summary>
        public void TakeDamage(int damage)
        {
            energy -= damage;
            if (energy < 0) energy = 0;
            Debug.Log($"Current Energy : {energy}");
            CheckDead();
        }

        /// <summary>
        /// เมธอด Overload: รับดาเมจพร้อมระบุชื่อผู้โจมตี
        /// </summary>
        public void TakeDamage(int damage, string attacker)
        {
            Debug.Log($"Attacked by {attacker} (-{damage})");
            TakeDamage(damage);
        }

        /// <summary>
        /// ฟื้นฟูพลังงานของผู้เล่น
        /// </summary>
        public void Heal(int healPoint)
        {
            energy += healPoint;
            Debug.Log($"Healed +{healPoint}! Current Energy: {energy}");
        }

        /// <summary>
        /// เพิ่มพลังโจมตีถาวรให้กับผู้เล่น (เช่น เมื่อเก็บดาบ)
        /// </summary>
        public void IncreaseAttack(int value)
        {
            attackPoint += value;
            Debug.Log($"Attack increased by +{value}! Current Attack: {attackPoint}");
        }

        /// <summary>
        /// ตรวจสอบว่าผู้เล่นพลังงานหมด (ตาย) หรือยัง
        /// </summary>
        private void CheckDead()
        {
            if (energy <= 0)
            {
                Debug.Log("💀 You Lose! Out of energy.");
                Destroy(gameObject);
            }
        }

        public int GetEnergy()
        {
            return energy;
        }

        #endregion

       
    }
}
