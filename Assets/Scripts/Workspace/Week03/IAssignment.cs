using System.Collections;
using UnityEngine;

namespace Week03
{
    /// <summary>
    /// Week 03 - Array, For Loop, While Loop, Instantiate, Translate
    /// signature + รูปแบบ output อ้างอิงจาก Instruction-th.md
    /// </summary>
    public interface IAssignment
    {
        #region Lecture

        void As01_IronManSuit();

        void As02_SpiderManAndBatMan();

        void As03_RandomItemDrop(GameObject[] items);

        void As04_ForLoopBasic();

        void As05_ForLoopN(int n);

        void As06_ForLoopWithArray(string[] suiteNames);

        void As07_InstantiateEnemies(GameObject Enemy, int[] HpEnemy);

        void As08_WhileLoopBasic();

        IEnumerator As09_MoveToTarget(Transform character, Transform target, float speed);

        #endregion

        #region Homework

        #region Level 1: Simple

        void Lv01_SetArrayValues();

        void Lv02_InspectArray(string[] items);

        void Lv03_RandomDialogue(string[] npc1Dialogues);

        void Lv04_AttackTarget(int[] enemyHP, int damage, int target);

        void Lv05_MultiplicationTable(int n);

        void Lv06_ForLoopReverse(string[] suiteNames);

        void Lv07_FindHighestScore(int[] scores);

        void Lv08_CalculateTotalScore(int[] scores);

        void Lv09_WhileLoopN(int n);

        #endregion

        #region Level 2: Moderate

        void Ex01_HealTarget(int[] enemyHP, int heal, int target, int maxHP);

        void Ex02_DialogueInteraction(string[] npc1Dialogues, string[] npc2Dialogues);

        void Ex03_SpawnEnemiesWithSpacing(GameObject Enemy, int count, float spacing);

        void Ex04_FindItemOrBreak(string[] inventory, string targetItem);

        void Ex05_SkipDefeatedEnemies(int[] enemyHPs);

        void Ex06_WhileLoopStep(string[] suiteNames);

        void Ex07_WhileLoopSum(int n);

        #endregion
        #endregion
    }
}
