using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;

namespace Week07.Teacher.Ex02
{
    public class Animal
    {
        public string name = "";
        protected string specie = "";
        private int health = 10;

        public void Feed(int food)
        {
            health += food;
            Debug.Log($"{name} got {food} food");
        }

        public void MakeSound()
        {
            if (health > 50)
            {
                Debug.Log($"{name} happy!");
            }
            else
            {
                Debug.Log($"{name} weak!");
            }
        }
    }

    public class Dog : Animal
    {
        public Dog(string name)
        {
            specie = "Dog";
            this.name = name;
        }
    }

    public class AS02_AccessModifier
    {
        public void Start()
        {
            Dog dog = new Dog("Buddy");
            Debug.Log($"my name is {dog.name}");

            dog.MakeSound();
            dog.Feed(50);
            dog.MakeSound();
        }
    }
}
