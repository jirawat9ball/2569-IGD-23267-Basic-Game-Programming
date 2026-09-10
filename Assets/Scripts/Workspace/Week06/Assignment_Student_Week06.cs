using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06
{
    public class Assignment_Student_Week06 : MonoBehaviour, IAssignment
    {
        void Start()
        {
            Ex01_CarDemo();
            Ex02_DogDemo();
            Ex03_InheritanceDemo();
            Ex04_AccessModifierDemo();
            Ex05_VirtualOverrideDemo();
        }

        public void Ex01_CarDemo()
        {
            // Guideline: (คลาส Car อยู่ในไฟล์ Ex01_Car.cs)
            // 1. สร้าง object จากคลาส Car ด้วยคำสั่ง new
            // 2. กำหนดค่า name, color, speed ให้รถคันนั้น
            // 3. เรียกเมธอด Move(), Turn(), Honk() ตามลำดับ
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
            // Guideline: (คลาส Dog อยู่ในไฟล์ Ex02_Dog.cs)
            // 1. สร้าง object จากคลาส Dog โดยส่งค่าผ่าน Constructor
            //    ให้ name เป็น "Buddy"
            // 2. เรียกเมธอด Bark(), WagTail(), StopBarking() ตามลำดับ
            Ex02.Dog dog1 = new Ex02.Dog("Buddy", "Golden Retriever", 3);

            dog1.Bark();
            dog1.WagTail();
            dog1.StopBarking();
        }

        public void Ex03_InheritanceDemo()
        {
            // Guideline: (คลาสอยู่ในไฟล์ Ex03_Animal.cs)
            // 1. สร้าง Dog แล้วกำหนด name เป็น "Buddy"
            // 2. เรียก MakeSound() (ได้มาจากการสืบทอดคลาส Animal) แล้วตามด้วย Walk()
            // 3. สร้าง Bird แล้วกำหนด name เป็น "Twitty"
            // 4. เรียก MakeSound() แล้วตามด้วย Fly()
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
            // Guideline: (คลาสอยู่ในไฟล์ Ex04_Animal.cs)
            // 1. สร้าง Dog ผ่าน Constructor โดยส่งชื่อ "Buddy" เข้าไป
            // 2. พิมพ์ "my name is <ชื่อ>"  (name เป็น public จึงอ่านจากข้างนอกได้)
            // 3. เรียก MakeSound() ครั้งแรก (ตอนนี้ health = 10 จึงได้ weak!)
            // 4. เรียก Feed(50) เพื่อเพิ่ม health
            // 5. เรียก MakeSound() อีกครั้ง (health = 60 จึงได้ happy!)
            Ex04.Dog dog = new Ex04.Dog("Buddy");

            Debug.Log($"my name is {dog.name}");

            dog.MakeSound();
            dog.Feed(50);
            dog.MakeSound();
        }

        public void Ex05_VirtualOverrideDemo()
        {
            // Guideline: (คลาสอยู่ในไฟล์ Ex05_Animal.cs)
            // 1. สร้าง Dog แล้วเรียก MakeSound() -> จะได้เสียงที่ override ไว้ ("Woof!")
            // 2. สร้าง Animal แล้วเรียก MakeSound() -> จะได้เสียงของคลาสแม่ ("Generic animal sound")
            Ex05.Dog dog = new Ex05.Dog();
            dog.MakeSound();

            Ex05.Animal someAnimal = new Ex05.Animal();
            someAnimal.MakeSound();
        }

        public void Ex06_BattleDemo()
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

        public void Ex07_PotionDemo()
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

        public void Ex08_SwordDemo()
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
