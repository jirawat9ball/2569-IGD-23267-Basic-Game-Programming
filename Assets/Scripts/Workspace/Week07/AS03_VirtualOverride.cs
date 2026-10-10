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

    public class Dog 
    {
        // student code here ...
        // 1. declare overridden MakeSound() method

        // student code ends ...
    }

    public class Cat 
    {
        // student code here ...
        // 2. declare overridden MakeSound() method

        // student code ends ...
    }

    public class AS03_VirtualOverride
    {
        public void Start()
        {
            // Student code starts HERE ...
            // 3. create instance of Dog and call MakeSound()
            // Dog dog = new Dog();
            // dog.MakeSound();

            // 4. create instance of Cat and call MakeSound()
            // Cat cat = new Cat();
            // cat.MakeSound();
            // Student code ends HERE ...

            // 5. create instance of Animal and call MakeSound()
            Animal animal = new Animal();
            animal.MakeSound();
        }
    }
}

