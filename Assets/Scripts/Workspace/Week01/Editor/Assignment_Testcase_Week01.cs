using System;
using System.Reflection;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Week01_Value
{
    public class Lecture
    {
        [OneTimeSetUp]
        public void LoadTestScene()
        {
            // บังคับเปิด Scene ที่ต้องการเทสต์ เพื่อให้ดึงค่าวัตถุใน Scene ได้
            EditorSceneManager.OpenScene("Assets/Scenes/Week01_Value.unity", OpenSceneMode.Single);
        }

        [Test]
        public void As01_Check_CharacterName()
        {
            CheckField("characterName", typeof(string));
        }

        [Test]
        public void As02_Check_Level()
        {
            CheckField("level", typeof(int));
        }

        [Test]
        public void As03_Check_MoveSpeed()
        {
            CheckField("moveSpeed", typeof(float));
        }

        [Test]
        public void As04_Check_IsAlive()
        {
            CheckField("isAlive", typeof(bool));
        }

        [Test]
        public void As05_Check_MaxHealth_InspectorVisible()
        {
            CheckField("maxHealth", typeof(int), true);
        }

        [Test]
        public void As06_Check_CurrentHealth_Hidden()
        {
            CheckField("currentHealth", typeof(int), false);
        }

        [Test]
        public void As07_Check_Level3_Variables()
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
        public void As09_Check_Timer_And_Reset()
        {
            CheckField("timer", typeof(float));

            Type targetType = TargetType;
            UnityEngine.Object studentScript = null;

            foreach (var mb in Resources.FindObjectsOfTypeAll<MonoBehaviour>())
            {
                if (mb.GetType().Name == TargetTypeName && !PrefabUtility.IsPartOfPrefabAsset(mb))
                {
                    studentScript = mb;
                    break;
                }
            }

            Assert.IsNotNull(studentScript, $"❌ ไม่พบ Component '{TargetTypeName}' ใน Scene ปัจจุบัน (เปิด Scene ถูกต้องและลากสคริปต์ใส่ GameObject หรือยัง?)");

            var timerField = targetType.GetField("timer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(timerField, "❌ ไม่พบตัวแปรชื่อ 'timer'");

            float origTimer = (float)timerField.GetValue(studentScript);

            // Set timer to a value >= 3 to trigger reset
            timerField.SetValue(studentScript, 3.5f);

            MethodInfo updateMethod = targetType.GetMethod("Update", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod != null)
            {
                try { updateMethod.Invoke(studentScript, null); } catch { }
                float newTimer = (float)timerField.GetValue(studentScript);

                // After Update, timer should be reset to 0 (or near 0 if Time.deltaTime was added before reset)
                Assert.IsTrue(newTimer < 1f, "❌ ตัวแปร timer ไม่ถูกรีเซ็ตกลับเป็น 0 เมื่อเวลาเกิน 3 วินาที (เขียนเงื่อนไข if(timer >= 3) รีเซ็ตค่าหรือยัง?)");
            }

            // Restore
            timerField.SetValue(studentScript, origTimer);
        }

        // =====================================
        // แบบทดสอบระดับ 5
        // =====================================

        [Test]
        public void As10_Check_StartPosition()
        {
            CheckField("StartPosition", typeof(Vector3), true);
        }

        [Test]
        public void As11_Check_ColorPlayer()
        {
            CheckField("colorPlayer", typeof(Color), true);
        }

        [Test]
        public void As12_Check_PlayerMesh()
        {
            CheckField("playerMesh", typeof(MeshRenderer), true);
            CheckAssignment("playerMesh", false); // ต้องเป็น Scene Object
        }

        // =====================================
        // แบบทดสอบระดับ 6
        // =====================================

        [Test]
        public void As13_Check_Heart_GameObject()
        {
            CheckField("Heart", typeof(GameObject), true);
            CheckAssignment("Heart", true); // ต้องเป็น Asset (Prefab จากโฟลเดอร์)
        }

        [Test]
        public void As14_Check_SpawnHeart_Transform()
        {
            CheckField("SpawnHeart", typeof(Transform), true);
            CheckAssignment("SpawnHeart", false); // ต้องเป็น Scene Object
        }

        [Test]
        public void As15_Check_C1_FirstPersonMovement()
        {
            Type componentType = typeof(FirstPersonMovement);
            CheckField("C1", componentType, true);
            CheckAssignment("C1", false); // ต้องเป็น Scene Object
        }

        [Test]
        public void As16_Check_C2_FirstPersonInterface()
        {
            Type componentType = typeof(FirstPersonInterface);
            CheckField("C2", componentType, true);
            CheckAssignment("C2", false); // ต้องเป็น Scene Object
        }

        // =========================================================================================
        // 🎯 สลับตรวจไฟล์ อ. หรือ นักเรียน: เปลี่ยนเป็น true เมื่อต้องการตรวจไฟล์เฉลยอาจารย์
        // =========================================================================================
        private const bool isTeacherMode = false;

        private static Type TargetType => isTeacherMode
            ? typeof(Assignment_Teacher_Week01)
            : typeof(Assignment_Student_Week01);

        private static string TargetTypeName => TargetType.Name;

        private void CheckField(string varName, Type expectedType, bool? shouldBeExposed = null)
        {
            Type targetType = TargetType;

            Assert.IsNotNull(targetType, $"❌ ไม่พบคลาส '{TargetTypeName}' ในโปรเจกต์ (ลบไฟล์สคริปต์ไปหรือเปล่า?)");

            FieldInfo field = targetType.GetField(varName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

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

        private void CheckAssignment(string varName, bool shouldBeAsset)
        {
            Type targetType = TargetType;
            Assert.IsNotNull(targetType, $"❌ ไม่พบคลาส '{TargetTypeName}'");

            UnityEngine.Object studentScript = null;
            foreach (var mb in Resources.FindObjectsOfTypeAll<MonoBehaviour>())
            {
                if (mb.GetType().Name == TargetTypeName && !PrefabUtility.IsPartOfPrefabAsset(mb))
                {
                    studentScript = mb;
                    break;
                }
            }

            Assert.IsNotNull(studentScript, $"❌ ไม่พบ Component '{TargetTypeName}' ใน Scene ปัจจุบัน (เปิด Scene ถูกต้องและลากสคริปต์ใส่ GameObject หรือยัง?)");

            FieldInfo field = targetType.GetField(varName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(field, $"❌ ไม่พบตัวแปรชื่อ '{varName}'");

            object fieldValue = field.GetValue(studentScript);

            bool isNull = false;
            if (fieldValue == null) isNull = true;
            else if (fieldValue is UnityEngine.Object uObj && uObj == null) isNull = true;

            Assert.IsFalse(isNull, $"❌ ยังไม่ได้ลากออบเจกต์มาใส่ในช่อง '{varName}' ใน Inspector");

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
                Assert.IsTrue(isAsset, $"❌ ช่อง '{varName}' ผิดพลาด! ต้องลาก Prefab (หรือ Asset) จากหน้าต่าง Project ด้านล่างมาใส่ ห้ามลากจาก Hierarchy ใน Scene");
            }
            else
            {
                Assert.IsFalse(isAsset, $"❌ ช่อง '{varName}' ผิดพลาด! ต้องลากวัตถุจากหน้าต่าง Hierarchy (ของที่อยู่ใน Scene) มาใส่");
            }
        }
    }
}
