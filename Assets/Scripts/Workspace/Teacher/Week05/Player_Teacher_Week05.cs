using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week05
{
    public class Player_Teacher_Week05 : MonoBehaviour
    {
        [Header("ข้อ 4-8: ตัวแปรของตัวละคร")]
        public int energy = 20;

        private void Update()
        {
            if (!CanMove()) return;

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

        #region ข้อ 4-8: Method ของตัวละคร

        // ข้อ 4: Method แบบ void + Parameter
        public void Move(Vector2 direction)
        {
            transform.position += new Vector3(direction.x, direction.y, 0f);
            energy -= 1;
        }

        // ข้อ 5: Method แบบ void + Parameter (ลด energy และเรียก CheckDead)
        public void TakeDamage(int Damage)
        {
            energy -= Damage;
            if (energy < 0)
            {
                energy = 0;
            }

            Debug.Log("Current Energy : " + energy);
            CheckDead();
        }

        // ข้อ 6: Method Scope (private void ไม่รับพารามิเตอร์)
        private void CheckDead()
        {
            if (energy <= 0)
            {
                Debug.Log("You Lose");
                Destroy(gameObject);
            }
        }

        // ข้อ 7: Method แบบ Default Parameter (ค่าเริ่มต้น 10)
        public void Heal(int healPoint = 10)
        {
            energy += healPoint;
        }

        // ข้อ 8: Method แบบมีค่าส่งกลับ (Return Type bool)
        public bool CanMove()
        {
            return energy > 0;
        }

        #endregion
    }
}
