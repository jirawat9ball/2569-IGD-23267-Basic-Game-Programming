using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08.Teacher.Ex01
{
    public class CharacterStatus : MonoBehaviour
    {
        public string characterName = "Hero";
        public int maxHealth = 100;
        public int currentHealth = 100;
        public int shield = 0;
        public bool isAlive = true;

        public string CheckHealthStatus()
        {
            if (currentHealth <= 0)
            {
                isAlive = false;
                Debug.Log($"{characterName} is Defeated");
                return "Defeated";
            }

            if (currentHealth <= maxHealth * 0.25f)
            {
                Debug.Log($"{characterName} is in Critical condition");
                return "Critical";
            }

            Debug.Log($"{characterName} is Healthy");
            return "Healthy";
        }

        public int CalculateDamage(int rawDamage, string attackElement, string targetWeakness)
        {
            if (rawDamage <= 0 || attackElement == "None")
            {
                Debug.Log("Invalid Attack");
                return 0;
            }

            if (attackElement == targetWeakness)
            {
                int finalDamage = rawDamage * 2;
                Debug.Log($"Super Effective! Damage: {finalDamage}");
                return finalDamage;
            }
            else
            {
                int finalDamage = rawDamage;
                Debug.Log($"Normal Damage: {finalDamage}");
                return finalDamage;
            }
        }
    }
}
