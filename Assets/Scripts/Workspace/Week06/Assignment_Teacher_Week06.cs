using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06
{
    public class Assignment_Teacher_Week06 : MonoBehaviour, IAssignment
    {
        public void Ex01_CarDemo()
        {
            Ex01.Car honda = new Ex01.Car();
            honda.name = "civic";
            honda.color = "black";
            honda.speed = 110;

            honda.Move();
            honda.Turn();
            honda.Honk();
        }

        public void Ex02_DogDemo()
        {
            Ex02.Dog dog1 = new Ex02.Dog("Buddy", "Golden Retriever", 3);

            dog1.Bark();
            dog1.WagTail();
            dog1.StopBarking();
        }

        public void Ex03_InheritanceDemo()
        {
            Ex03.Dog dog = new Ex03.Dog();
            dog.name = "Buddy";
            dog.MakeSound();
            dog.Walk();

            Ex03.Bird bird = new Ex03.Bird();
            bird.name = "Twitty";
            bird.MakeSound();
            bird.Fly();
        }

        public void Ex04_AccessModifierDemo()
        {
            Ex04.Dog dog = new Ex04.Dog("Buddy");

            Debug.Log($"my name is {dog.name}");

            dog.MakeSound();
            dog.Feed(50);
            dog.MakeSound();
        }

        public void Ex05_VirtualOverrideDemo()
        {
            Ex05.Dog dog = new Ex05.Dog();
            dog.MakeSound();

            Ex05.Animal someAnimal = new Ex05.Animal();
            someAnimal.MakeSound();
        }

        public void Ex06_BattleDemo()
        {
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

        public void Ex07_PotionDemo()
        {
            Game.MapGenerator map = Game.MapGenerator.CreateDemoMap();
            Game.Character player = map.player;

            player.Move(Vector2.up);
            player.Move(Vector2.up);
            player.Move(Vector2.right);
            player.Move(Vector2.right);   // เหยียบยา

            Debug.Log($"Player energy after picking up potion: {player.energy}");

            map.ClearMap();
        }

        public void Ex08_SwordDemo()
        {
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
