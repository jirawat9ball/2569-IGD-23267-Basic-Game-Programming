using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Reflection;
using System;

namespace Week01
{
    public class Homework
{
    [OneTimeSetUp]
    public void LoadTestScene()
    {
        // เธเธฑเธเธเธฑเธเน€เธเธดเธ” Scene เธ—เธตเนเธ•เนเธญเธเธเธฒเธฃเน€เธ—เธชเธ•เน เน€เธเธทเนเธญเนเธซเนเธ”เธถเธเธเนเธฒเธงเธฑเธ•เธ–เธธเนเธ Scene เนเธ”เน
        EditorSceneManager.OpenScene("Assets/Scenes/Week01 Value.unity", OpenSceneMode.Single);
    }

    [Test]
    public void Lv01_Check_CharacterName()
    {
        CheckField("characterName", typeof(string));
    }

    [Test]
    public void Lv02_Check_Level()
    {
        CheckField("level", typeof(int));
    }

    [Test]
    public void Lv03_Check_MoveSpeed()
    {
        CheckField("moveSpeed", typeof(float));
    }

    [Test]
    public void Lv04_Check_IsAlive()
    {
        CheckField("isAlive", typeof(bool));
    }

    [Test]
    public void Lv05_Check_MaxHealth_InspectorVisible()
    {
        CheckField("maxHealth", typeof(int), true);
    }

    [Test]
    public void Lv06_Check_CurrentHealth_Hidden()
    {
        CheckField("currentHealth", typeof(int), false);
    }

    [Test]
    public void Lv07_Check_Level3_Variables()
    {
        CheckField("NAME", typeof(string));
        CheckField("LASTNAME", typeof(string));
        CheckField("HP", typeof(int));
        CheckField("DAMAGE", typeof(int));
        CheckField("SPEED", typeof(float));
        CheckField("TIME", typeof(float));
        CheckField("DISTANCE", typeof(float));
    }

    [Test]
    public void Lv09_Check_Timer_And_Reset()
    {
        CheckField("timer", typeof(float));

        Type studentType = Type.GetType("Assignment_Student_Week01, Assembly-CSharp");
        UnityEngine.Object studentScript = null;
        
        foreach (var mb in Resources.FindObjectsOfTypeAll<MonoBehaviour>())
        {
            if (mb.GetType().Name == "Assignment_Student_Week01" && !PrefabUtility.IsPartOfPrefabAsset(mb))
            {
                studentScript = mb;
                break;
            }
        }
        if (studentScript != null)
        {
            var timerField = studentType.GetField("timer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (timerField != null)
            {
                float origTimer = (float)timerField.GetValue(studentScript);

                // Set timer to a value >= 3 to trigger reset
                timerField.SetValue(studentScript, 3.5f);
                
                MethodInfo updateMethod = studentType.GetMethod("Update", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (updateMethod != null)
                {
                    try { updateMethod.Invoke(studentScript, null); } catch { }
                    float newTimer = (float)timerField.GetValue(studentScript);
                    
                    // After Update, timer should be reset to 0 (or near 0 if Time.deltaTime was added before reset)
                    Assert.IsTrue(newTimer < 1f, "โ เธ•เธฑเธงเนเธเธฃ timer เนเธกเนเธ–เธนเธเธฃเธตเน€เธเนเธ•เธเธฅเธฑเธเน€เธเนเธ 0 เน€เธกเธทเนเธญเน€เธงเธฅเธฒเน€เธเธดเธ 3 เธงเธดเธเธฒเธ—เธต (เน€เธเธตเธขเธเน€เธเธทเนเธญเธเนเธ if(timer >= 3) เธฃเธตเน€เธเนเธ•เธเนเธฒเธซเธฃเธทเธญเธขเธฑเธ?)");
                }

                // Restore
                timerField.SetValue(studentScript, origTimer);
            }
        }
    }

    // =====================================
    // เนเธเธเธ—เธ”เธชเธญเธเธฃเธฐเธ”เธฑเธ 5
    // =====================================

    [Test]
    public void Lv10_Check_StartPosition()
    {
        CheckField("StartPosition", typeof(Vector3), true);
    }

    [Test]
    public void Lv11_Check_ColorPlayer()
    {
        CheckField("colorPlayer", typeof(Color), true);
    }

    [Test]
    public void Lv12_Check_PlayerMesh()
    {
        CheckField("playerMesh", typeof(MeshRenderer), true);
        CheckAssignment("playerMesh", false); // เธ•เนเธญเธเน€เธเนเธ Scene Object
    }

    // =====================================
    // เนเธเธเธ—เธ”เธชเธญเธเธฃเธฐเธ”เธฑเธ 6
    // =====================================

    [Test]
    public void Lv13_Check_Heart_GameObject()
    {
        CheckField("Heart", typeof(GameObject), true);
        CheckAssignment("Heart", true); // เธ•เนเธญเธเน€เธเนเธ Asset (Prefab เธเธฒเธเนเธเธฅเน€เธ”เธญเธฃเน)
    }

    [Test]
    public void Lv14_Check_SpawnHeart_Transform()
    {
        CheckField("SpawnHeart", typeof(Transform), true);
        CheckAssignment("SpawnHeart", false); // เธ•เนเธญเธเน€เธเนเธ Scene Object
    }

    [Test]
    public void Lv15_Check_C1_FirstPersonMovement()
    {
        Type componentType = Type.GetType("FirstPersonMovement, Assembly-CSharp");
        if (componentType != null)
        {
            CheckField("C1", componentType, true);
            CheckAssignment("C1", false); // เธ•เนเธญเธเน€เธเนเธ Scene Object
        }
        else
        {
            Assert.Fail("โ เนเธกเนเธเธเธชเธเธฃเธดเธเธ•เน FirstPersonMovement เนเธเนเธเธฃเน€เธเธเธ•เน!");
        }
    }

    [Test]
    public void Lv16_Check_C2_FirstPersonInterface()
    {
        Type componentType = Type.GetType("FirstPersonInterface, Assembly-CSharp");
        if (componentType != null)
        {
            CheckField("C2", componentType, true);
            CheckAssignment("C2", false); // เธ•เนเธญเธเน€เธเนเธ Scene Object
        }
        else
        {
            Assert.Fail("โ เนเธกเนเธเธเธชเธเธฃเธดเธเธ•เน FirstPersonInterface เนเธเนเธเธฃเน€เธเธเธ•เน!");
        }
    }

    private void CheckField(string varName, Type expectedType, bool? shouldBeExposed = null)
    {
        // เนเธเน Reflection เน€เธเธทเนเธญเธ”เธถเธเธเธฅเธฒเธช Assignment_Student_Week01 เธเธฒเธเนเธเธฃเน€เธเธเธ•เนเธซเธฅเธฑเธ (Assembly-CSharp) 
        Type studentType = Type.GetType("Assignment_Student_Week01, Assembly-CSharp");
        
        Assert.IsNotNull(studentType, "โ เนเธกเนเธเธเธเธฅเธฒเธช 'Assignment_Student_Week01' เนเธเนเธเธฃเน€เธเธเธ•เน (เธฅเธเนเธเธฅเนเธชเธเธฃเธดเธเธ•เนเนเธเธซเธฃเธทเธญเน€เธเธฅเนเธฒ?)");

        FieldInfo field = studentType.GetField(varName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        
        Assert.IsNotNull(field, $"โ เนเธกเนเธเธเธ•เธฑเธงเนเธเธฃเธเธทเนเธญ '{varName}' (เธ•เธฃเธงเธเธชเธญเธเธ•เธฑเธงเธเธดเธกเธเนเน€เธฅเนเธ-เธเธดเธกเธเนเนเธซเธเนเนเธซเนเธ•เธฃเธเน€เธเนเธฐ)");
        Assert.AreEqual(expectedType, field.FieldType, $"โ เธ•เธฑเธงเนเธเธฃ '{varName}' เธเธเธดเธ”เธเธดเธ”! เธเธงเธฃเธเธฐเน€เธเนเธ {expectedType.Name} เนเธ•เนเธ•เธญเธเธเธตเนเน€เธเนเธ {field.FieldType.Name}");

        if (shouldBeExposed.HasValue)
        {
            bool isExposed = field.IsPublic || Attribute.IsDefined(field, typeof(SerializeField));
            if (shouldBeExposed.Value)
            {
                Assert.IsTrue(isExposed, $"โ ๏ธ เธ•เธฑเธงเนเธเธฃ '{varName}' เธเธงเธฃเธ•เธฑเนเธเธเนเธฒเน€เธเนเธ public เธซเธฃเธทเธญเน€เธเธดเนเธก [SerializeField] เนเธซเนเธเธฃเธฑเธเธเนเธฒเนเธ Inspector เนเธ”เน");
            }
            else
            {
                Assert.IsFalse(isExposed, $"โ ๏ธ เธ•เธฑเธงเนเธเธฃ '{varName}' เธเธงเธฃเธ•เธฑเนเธเธเนเธฒเน€เธเนเธ private เนเธกเนเนเธซเนเนเธชเธ”เธเนเธ Inspector");
            }
        }
    }

    private void CheckAssignment(string varName, bool shouldBeAsset)
    {
        Type studentType = Type.GetType("Assignment_Student_Week01, Assembly-CSharp");
        Assert.IsNotNull(studentType, "โ เนเธกเนเธเธเธเธฅเธฒเธช 'Assignment_Student_Week01'");

        UnityEngine.Object studentScript = null;
        foreach (var mb in Resources.FindObjectsOfTypeAll<MonoBehaviour>())
        {
            if (mb.GetType().Name == "Assignment_Student_Week01" && !PrefabUtility.IsPartOfPrefabAsset(mb))
            {
                studentScript = mb;
                break;
            }
        }

        Assert.IsNotNull(studentScript, "โ เนเธกเนเธเธ Component 'Assignment_Student_Week01' เนเธ Scene เธเธฑเธเธเธธเธเธฑเธ (เน€เธเธดเธ” Scene เธ–เธนเธเธ•เนเธญเธเนเธฅเธฐเธฅเธฒเธเธชเธเธฃเธดเธเธ•เนเนเธชเน GameObject เธซเธฃเธทเธญเธขเธฑเธ?)");

        FieldInfo field = studentType.GetField(varName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(field, $"โ เนเธกเนเธเธเธ•เธฑเธงเนเธเธฃเธเธทเนเธญ '{varName}'");

        object fieldValue = field.GetValue(studentScript);
        
        bool isNull = false;
        if (fieldValue == null) isNull = true;
        else if (fieldValue is UnityEngine.Object uObj && uObj == null) isNull = true;

        Assert.IsFalse(isNull, $"โ เธขเธฑเธเนเธกเนเนเธ”เนเธฅเธฒเธเธญเธญเธเน€เธเธเธ•เนเธกเธฒเนเธชเนเนเธเธเนเธญเธ '{varName}' เนเธ Inspector");

        bool isAsset = false;
        if (fieldValue is GameObject go)
        {
            isAsset = PrefabUtility.IsPartOfPrefabAsset(go);
        }
        else if (fieldValue is Component compObj)
        {
            isAsset = PrefabUtility.IsPartOfPrefabAsset(compObj);
        }
        
        if (shouldBeAsset)
        {
            Assert.IsTrue(isAsset, $"โ เธเนเธญเธ '{varName}' เธเธดเธ”เธเธฅเธฒเธ”! เธ•เนเธญเธเธฅเธฒเธ Prefab (เธซเธฃเธทเธญ Asset) เธเธฒเธเธซเธเนเธฒเธ•เนเธฒเธ Project เธ”เนเธฒเธเธฅเนเธฒเธเธกเธฒเนเธชเน เธซเนเธฒเธกเธฅเธฒเธเธเธฒเธ Hierarchy เนเธ Scene");
        }
        else
        {
            Assert.IsFalse(isAsset, $"โ เธเนเธญเธ '{varName}' เธเธดเธ”เธเธฅเธฒเธ”! เธ•เนเธญเธเธฅเธฒเธเธงเธฑเธ•เธ–เธธเธเธฒเธเธซเธเนเธฒเธ•เนเธฒเธ Hierarchy (เธเธญเธเธ—เธตเนเธญเธขเธนเนเนเธ Scene) เธกเธฒเนเธชเน");
        }
    }
    }
}

