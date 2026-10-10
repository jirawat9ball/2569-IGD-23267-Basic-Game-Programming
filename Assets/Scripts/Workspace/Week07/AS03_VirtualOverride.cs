using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;

namespace Week07.Ex03
{
    public class Animal
    {
        // 0. make MakeSound method to virtual method
        public virtual void MakeSound()
        {
            Debug.Log("Generic animal sound");
        }
    }

    public class Dog : Animal
    {
        // student code here ...
        public override void MakeSound()
        {
            Debug.Log("Woof!");
        }
        // student code ends ...
    }

    public class Cat : Animal
    {
        // student code here ...
        public override void MakeSound()
        {
            Debug.Log("Meow!");
        }
        // student code ends ...
    }

    public class AS03_VirtualOverride
    {
        public void Start()
        {
            // Student code starts HERE ...
            Dog dog = new Dog();
            dog.MakeSound();

            Cat cat = new Cat();
            cat.MakeSound();

            Animal animal = new Animal();
            animal.MakeSound();
            // Student code ends HERE ...
        }
    }
}

