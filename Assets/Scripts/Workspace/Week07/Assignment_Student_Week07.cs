using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07
{
    public class Assignment_Student_Week07 : MonoBehaviour, IAssignment
    {
        void Start()
        {
            Ex01_InheritanceDemo();
            Ex02_AccessModifierDemo();
            Ex03_VirtualOverrideDemo();
        }

        public void Ex01_InheritanceDemo()
        {
            new Ex01.AS01_Inheritance().Start();
        }

        public void Ex02_AccessModifierDemo()
        {
            new Ex02.AS02_AccessModifier().Start();
        }

        public void Ex03_VirtualOverrideDemo()
        {
            new Ex03.AS03_VirtualOverride().Start();
        }

        public void Ex04_BattleDemo()
        {
            // Guideline: (คลาสของเกมอยู่ในโฟลเดอร์ Game/)
            // ฉากนี้เดินตามโจทย์: Player (0,0) energy 100 attack 10
            // ผ่านช่องว่าง 3 ช่อง -> เก็บยาที่ (2,2) -> เก็บดาบที่ (3,2) -> ตีศัตรูที่ (3,3) 3 ครั้ง
            Game.MapGenerator map = Game.MapGenerator.CreateDemoMap();
            Game.Character player = map.player;
            Game.Enemy enemy = map.enemies[3, 3];

            player.Move(Vector2.up);      // (0,1) ช่องว่าง -> energy 99
            player.Move(Vector2.up);      // (0,2) ช่องว่าง -> energy 98
            player.Move(Vector2.right);   // (1,2) ช่องว่าง -> energy 97
            player.Move(Vector2.right);   // (2,2) ยา       -> energy 117
            player.Move(Vector2.right);   // (3,2) ดาบ      -> attack 20

            Debug.Log($"Player energy after picking up potion: {player.energy}");
            Debug.Log($"Player attack point after picking up sword: {player.attackPoint}");

            Debug.Log("first attack ...");
            player.Move(Vector2.up);
            Debug.Log($"Player energy after attack: {player.energy}");
            Debug.Log($"Enemy energy after attack: {enemy.energy}");

            Debug.Log("second attack ...");
            player.Move(Vector2.up);
            Debug.Log($"Player energy after attack: {player.energy}");
            Debug.Log($"Enemy energy after attack: {enemy.energy}");

            Debug.Log("thrid attack ...");
            player.Move(Vector2.up);
            Debug.Log($"Player energy after attack: {player.energy}");
            Debug.Log($"Enemy energy after attack: {enemy.energy}");

            map.ClearMap();
        }

        public void Ex05_PotionDemo()
        {
            // Guideline: เดินไปเหยียบยาที่ (2,2) แล้วดูว่า energy เพิ่มขึ้นไหม
            Game.MapGenerator map = Game.MapGenerator.CreateDemoMap();
            Game.Character player = map.player;

            player.Move(Vector2.up);
            player.Move(Vector2.up);
            player.Move(Vector2.right);
            player.Move(Vector2.right);   // เหยียบยา

            Debug.Log($"Player energy after picking up potion: {player.energy}");

            map.ClearMap();
        }

        public void Ex06_SwordDemo()
        {
            // Guideline: เดินไปเหยียบดาบที่ (3,2) แล้วดูว่า attackPoint เพิ่มขึ้นไหม
            Game.MapGenerator map = Game.MapGenerator.CreateDemoMap();
            Game.Character player = map.player;

            player.Move(Vector2.up);
            player.Move(Vector2.up);
            player.Move(Vector2.right);
            player.Move(Vector2.right);   // เหยียบยา
            player.Move(Vector2.right);   // เหยียบดาบ

            Debug.Log($"Player attack point after picking up sword: {player.attackPoint}");

            map.ClearMap();
        }
    }
}

