using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;

namespace Week06.Teacher.Ex02
{
    public class Dog
    {
        public string name;
        public string breed;
        public int age;

        public Dog(string name, string breed, int age)
        {
            this.name = name;
            this.breed = breed;
            this.age = age;
        }

        public void Bark()
        {
            Debug.Log($"{name} is barking");
        }

        public void WagTail()
        {
            Debug.Log($"{name} is wagging tail");
        }

        public void StopBarking()
        {
            Debug.Log($"{name} stopped barking");
        }
    }

    public class AS02_ClassConstructor
    {
        Dog dog1;

        public void Start()
        {
            dog1 = new Dog("Buddy", "Golden Retriever", 3);
            dog1.Bark();
            dog1.WagTail();
            dog1.StopBarking();
        }
    }
}
