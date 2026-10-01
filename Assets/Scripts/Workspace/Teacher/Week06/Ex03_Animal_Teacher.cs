using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;

namespace Week06.Teacher.Ex03
{
    public class Animal
    {
        public string name;

        public void MakeSound()
        {
            Debug.Log($"Animal {name} is making sound");
        }
    }

    public class Dog : Animal
    {
        public void Walk()
        {
            Debug.Log($"Dog {name} is walking");
        }
    }

    public class Bird : Animal
    {
        public void Fly()
        {
            Debug.Log($"Bird {name} is flying");
        }
    }

    public class AS03_Inheritance
    {
        public void Start()
        {
            Dog dog = new Dog();
            dog.name = "Buddy";
            dog.MakeSound();
            dog.Walk();

            Bird bird = new Bird();
            bird.name = "Twitty";
            bird.MakeSound();
            bird.Fly();
        }
    }
}
