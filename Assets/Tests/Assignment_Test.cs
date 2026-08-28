using NUnit.Framework;
using UnityEngine;
using System.Reflection;
using System;

public class Week01
{
    [Test]
    public void _01_Check_CharacterName()
    {
        CheckField("characterName", typeof(string));
    }

    [Test]
    public void _02_Check_Level()
    {
        CheckField("level", typeof(int));
    }

    [Test]
    public void _03_Check_MoveSpeed()
    {
        CheckField("moveSpeed", typeof(float));
    }

    [Test]
    public void _04_Check_IsAlive()
    {
        CheckField("isAlive", typeof(bool));
    }

    [Test]
    public void _05_Check_MaxHealth_InspectorVisible()
    {
        CheckField("maxHealth", typeof(int), true);
    }

    [Test]
    public void _06_Check_CurrentHealth_Hidden()
    {
        CheckField("currentHealth", typeof(int), false);
    }

    [Test]
    public void _07_Check_PlayerMoney()
    {
        CheckField("playerMoney", typeof(int));
    }

    [Test]
    public void _08_Check_ItemPrice()
    {
        CheckField("itemPrice", typeof(int));
    }

    [Test]
    public void _09_Check_Timer()
    {
        CheckField("timer", typeof(float));
    }

    // =====================================
    // แบบทดสอบที่เพิ่มใหม่ (ระดับ 5)
    // =====================================

    [Test]
    public void _10_Check_Heart_GameObject()
    {
        CheckField("Heart", typeof(GameObject), true);
    }

    [Test]
    public void _11_Check_SpwanHeart_Transform()
    {
        CheckField("SpwanHeart", typeof(Transform), true);
    }

    [Test]
    public void _12_Check_C1_FirstPersonMovement()
    {
        // ใช้ Reflection ดึง Type ของ FirstPersonMovement จาก Assembly-CSharp
        Type componentType = Type.GetType("FirstPersonMovement, Assembly-CSharp");
        if (componentType != null)
        {
            CheckField("C1", componentType, true);
        }
        else
        {
            Assert.Fail("❌ ไม่พบสคริปต์ FirstPersonMovement ในโปรเจกต์!");
        }
    }

    [Test]
    public void _13_Check_C2_FirstPersonInterface()
    {
        // ใช้ Reflection ดึง Type ของ FirstPersonInterface จาก Assembly-CSharp
        Type componentType = Type.GetType("FirstPersonInterface, Assembly-CSharp");
        if (componentType != null)
        {
            CheckField("C2", componentType, true);
        }
        else
        {
            Assert.Fail("❌ ไม่พบสคริปต์ FirstPersonInterface ในโปรเจกต์!");
        }
    }

    private void CheckField(string varName, Type expectedType, bool? shouldBeExposed = null)
    {
        // ใช้ Reflection เพื่อดึงคลาส Assignment_Student จากโปรเจกต์หลัก (Assembly-CSharp) 
        Type studentType = Type.GetType("Assignment_Student, Assembly-CSharp");
        
        Assert.IsNotNull(studentType, "❌ ไม่พบคลาส 'Assignment_Student' ในโปรเจกต์ (ลบไฟล์สคริปต์ไปหรือเปล่า?)");

        FieldInfo field = studentType.GetField(varName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        
        Assert.IsNotNull(field, $"❌ ไม่พบตัวแปรชื่อ '{varName}' (ตรวจสอบตัวพิมพ์เล็ก-พิมพ์ใหญ่ให้ตรงเป๊ะ)");
        Assert.AreEqual(expectedType, field.FieldType, $"❌ ตัวแปร '{varName}' ชนิดผิด! ควรจะเป็น {expectedType.Name} แต่ตอนนี้เป็น {field.FieldType.Name}");

        if (shouldBeExposed.HasValue)
        {
            bool isExposed = field.IsPublic || Attribute.IsDefined(field, typeof(SerializeField));
            if (shouldBeExposed.Value)
            {
                Assert.IsTrue(isExposed, $"⚠️ ตัวแปร '{varName}' ควรตั้งค่าเป็น public หรือเพิ่ม [SerializeField] ให้ปรับค่าใน Inspector ได้");
            }
            else
            {
                Assert.IsFalse(isExposed, $"⚠️ ตัวแปร '{varName}' ควรตั้งค่าเป็น private ไม่ให้แสดงใน Inspector");
            }
        }
    }
}
