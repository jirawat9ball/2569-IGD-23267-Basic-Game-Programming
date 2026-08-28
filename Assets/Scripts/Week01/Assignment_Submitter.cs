using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Assignment_Student_Week01))]
public class Assignment_Submitter : MonoBehaviour
{
    [Header("ข้อมูลนักศึกษา (ห้ามเว้นว่าง)")]
    public string studentID = "รหัสนักศึกษา";
    public string studentName = "ชื่อ-นามสกุล";
    
    public enum StudentSection
    {
        Sec_001,
        Sec_101,
        Sec_002,
        Sec_102,
        Sec_003,
        Sec_103,
        Other
    }
    [Tooltip("เลือกกลุ่มเรียน")]
    public StudentSection section = StudentSection.Sec_001;
    
    [Tooltip("หากเลือก Other ให้ระบุกลุ่มเรียนที่นี่")]
    public string customSection = "";

    string weekName = "Week01";
    private float lastSubmitTime = 0f; // ป้องกันการกดปุ่มรัวๆ (ดีเลย์ 1 วินาที)


    // ซ่อนลิงก์ Web App URL ด้วยการเข้ารหัส Base64 และแยกส่วน (Obfuscation) เพื่อป้องกันนักเรียนค้นหาเจอ
    private string googleSheetWebAppURL
    {
        get
        {
            string _p1 = "aHR0cHM6Ly9zY3JpcHQuZ29vZ2";
            string _p2 = "xlLmNvbS9tYWNyb3Mvcy9BS2Z5";
            string _p3 = "Y2J5VUtWN3BoQUpjbTA0NHA5Z0";
            string _p4 = "w1MFgxVFFyOFhndlFhS3owQVVi";
            string _p5 = "UHl6ejRmamxrc1JTS0E0Z2w5TF";
            string _p6 = "JzcWZhV3hwTlIvZXhlYw==";
            byte[] _d = System.Convert.FromBase64String(_p1 + _p2 + _p3 + _p4 + _p5 + _p6);
            return System.Text.Encoding.UTF8.GetString(_d);
        }
    }

    public void SubmitAssignment()
    {
        // ดีเลย์ 1 วินาที เพื่อป้องกันการกดปุ่มรัวๆ (Spam click)
        if (Time.time - lastSubmitTime < 1f)
        {
            return;
        }
        lastSubmitTime = Time.time;

        if (string.IsNullOrEmpty(googleSheetWebAppURL) || googleSheetWebAppURL == "YOUR_WEB_APP_URL_HERE")
        {
            Debug.LogError("❌ ยังไม่ได้ใส่ Web App URL กรุณาตั้งค่าใน Inspector");
            return;
        }

        if (string.IsNullOrEmpty(studentID) || studentID == "รหัสนักศึกษา" || string.IsNullOrEmpty(studentName))
        {
            Debug.LogError("❌ กรุณาใส่ รหัสนักศึกษา และ ชื่อ-นามสกุล ให้เรียบร้อยก่อนส่งงาน");
            return;
        }

        // เช็คกรณีเลือกกลุ่มเรียน Other แต่ไม่ได้พิมพ์บอกไว้
        if (section == StudentSection.Other && string.IsNullOrEmpty(customSection))
        {
            Debug.LogError("❌ คุณเลือกกลุ่มเรียน 'Other' กรุณาระบุกลุ่มเรียนในช่อง Custom Section ด้วยครับ");
            return;
        }

        int score = CalculateScore();
        int maxScore = 13; // มีตัวแปร 13 ตัวที่ต้องตรวจ

        Debug.Log($"⏳ กำลังส่งงาน... (คะแนนที่ได้ {score}/{maxScore})");
        StartCoroutine(SendPostRequest(studentID, studentName, score.ToString(), maxScore.ToString()));
    }

    private int CalculateScore()
    {
        int totalScore = 0;
        Assignment_Student_Week01 studentScript = GetComponent<Assignment_Student_Week01>();

        // ตรวจโจทย์ระดับ 1-4
        if (IsFieldCorrect(studentScript, "characterName", typeof(string))) totalScore++;
        if (IsFieldCorrect(studentScript, "level", typeof(int))) totalScore++;
        if (IsFieldCorrect(studentScript, "moveSpeed", typeof(float))) totalScore++;
        if (IsFieldCorrect(studentScript, "isAlive", typeof(bool))) totalScore++;
        
        if (IsFieldCorrect(studentScript, "maxHealth", typeof(int), true)) totalScore++;
        if (IsFieldCorrect(studentScript, "currentHealth", typeof(int), false)) totalScore++;
        
        if (IsFieldCorrect(studentScript, "playerMoney", typeof(int))) totalScore++;
        if (IsFieldCorrect(studentScript, "itemPrice", typeof(int))) totalScore++;
        if (IsFieldCorrect(studentScript, "timer", typeof(float))) totalScore++;

        // ตรวจโจทย์ระดับ 5
        if (IsFieldCorrect(studentScript, "Heart", typeof(GameObject), true)) totalScore++;
        if (IsFieldCorrect(studentScript, "SpwanHeart", typeof(Transform), true)) totalScore++;

        Type c1Type = Type.GetType("FirstPersonMovement, Assembly-CSharp");
        if (c1Type != null && IsFieldCorrect(studentScript, "C1", c1Type, true)) totalScore++;

        Type c2Type = Type.GetType("FirstPersonInterface, Assembly-CSharp");
        if (c2Type != null && IsFieldCorrect(studentScript, "C2", c2Type, true)) totalScore++;

        return totalScore;
    }

    private bool IsFieldCorrect(object target, string varName, Type expectedType, bool? shouldBeExposed = null)
    {
        FieldInfo field = target.GetType().GetField(varName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (field == null) return false;
        if (field.FieldType != expectedType) return false;

        if (shouldBeExposed.HasValue)
        {
            bool isExposed = field.IsPublic || Attribute.IsDefined(field, typeof(SerializeField));
            if (shouldBeExposed.Value && !isExposed) return false;
            if (!shouldBeExposed.Value && isExposed) return false;
        }

        return true; // ถูกต้องสมบูรณ์
    }

    IEnumerator SendPostRequest(string id, string name, string score, string maxScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("studentId", id);
        form.AddField("studentName", name);
        
        // เลือกส่ง section ตามที่เลือก (ถ้าเป็น Other ให้ดึงข้อความจาก customSection)
        string sectionString = "";
        if (section == StudentSection.Other)
        {
            sectionString = customSection;
        }
        else
        {
            // เปลี่ยนจาก 'Sec_001' เป็น 'Sec 001' เพื่อให้ Google Sheet มองเป็นข้อความ
            sectionString = section.ToString().Replace("_", " ");
        }
        form.AddField("section", sectionString);
        
        form.AddField("score", score);
        form.AddField("maxScore", maxScore);
        form.AddField("week", weekName);

        using (UnityWebRequest www = UnityWebRequest.Post(googleSheetWebAppURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("❌ การส่งข้อมูลล้มเหลว: " + www.error);
            }
            else
            {
                Debug.Log("<color=green>✅ ส่งงานเรียบร้อยแล้ว!</color> ข้อมูลถูกบันทึกลง Google Sheet");
            }
        }
    }
}