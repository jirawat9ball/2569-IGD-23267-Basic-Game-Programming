using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08.Teacher.Ex01
{
    public class CharacterStatus : MonoBehaviour
    {
        public string characterName = "Hero";
        public int maxHealth = 100;
        public int currentHealth = 100;
        public bool isAlive = true;

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isAlive = false;
                Debug.Log($"{characterName} is defeated!");
            }
            else
            {
                Debug.Log($"{characterName} took {damage} damage. Current HP: {currentHealth}");
            }
        }

        public bool IsAlive()
        {
            return currentHealth > 0 && isAlive;
        }

        public int CalculateDamage(int rawDamage, int defense)
        {
            int netDamage = rawDamage - defense;
            if (netDamage < 0)
            {
                netDamage = 0;
            }

            Debug.Log($"Damage calculated: {netDamage}");
            return netDamage;
        }
    }
}
