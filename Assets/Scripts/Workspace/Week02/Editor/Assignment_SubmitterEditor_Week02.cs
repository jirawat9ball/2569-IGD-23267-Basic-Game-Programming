using UnityEditor;
using UnityEngine;
using UnityEditor.TestTools.TestRunner.Api;

[CustomEditor(typeof(Assignment_Submitter_Week02))]
public class Assignment_SubmitterEditor_Week02 : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Assignment_Submitter_Week02 submitter = (Assignment_Submitter_Week02)target;

        GUILayout.Space(20);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontStyle = FontStyle.Bold;
        buttonStyle.fontSize = 14;
        buttonStyle.fixedHeight = 40;
        
        if (GUILayout.Button("?? ส่งงานเข้า Google Sheet", buttonStyle))
        {
            if (string.IsNullOrEmpty(submitter.studentID) || submitter.studentID == "รหัสนักศึกษา" || string.IsNullOrEmpty(submitter.studentName))
            {
                Debug.LogError("? กรุณาใส่ รหัสนักศึกษา และ ชื่อ-นามสกุล ให้เรียบร้อยก่อนส่งงาน");
                return;
            }

            if (submitter.section == Assignment_Submitter_Week02.StudentSection.Other && string.IsNullOrEmpty(submitter.customSection))
            {
                Debug.LogError("? คุณเลือกกลุ่มเรียน 'Other' กรุณาระบุกลุ่มเรียนในช่อง Custom Section ด้วยครับ");
                return;
            }

            Debug.Log("? กำลังคำนวณคะแนนและส่งงาน...");

            TestRunnerCallback receiver = ScriptableObject.CreateInstance<TestRunnerCallback>();
            
            receiver.studentID = submitter.studentID;
            receiver.studentName = submitter.studentName;
            receiver.sectionString = (submitter.section == Assignment_Submitter_Week02.StudentSection.Other) ? submitter.customSection : submitter.section.ToString().Replace("_", " ");
            receiver.weekName = submitter.weekName;

            var prop = typeof(Assignment_Submitter_Week02).GetProperty("googleSheetWebAppURL", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            receiver.googleSheetWebAppURL = (string)prop.GetValue(submitter);

            var api = ScriptableObject.CreateInstance<TestRunnerApi>();
            api.RegisterCallbacks(receiver);

            // รันแบบ EditMode เพื่อความรวดเร็วและไม่กระตุก (เหมือนวีค 1)
            api.Execute(new ExecutionSettings(new Filter() { 
                testMode = TestMode.EditMode,
                assemblyNames = new string[] { "Workspace.dll" } 
            }));
        }
    }
}

[System.Serializable]
public class TestRunnerCallback : ScriptableObject, ICallbacks
{
    public string studentID;
    public string studentName;
    public string sectionString;
    public string weekName;
    public string googleSheetWebAppURL;

    public void RunFinished(ITestResultAdaptor result)
    {
        int passCount = result.PassCount;
        int totalCount = result.PassCount + result.FailCount + result.InconclusiveCount + result.SkipCount;

        if (totalCount == 0)
        {
            Debug.LogError("? ไม่พบเทสต์เคส ระบบส่งคะแนน 0/0");
        }
        else
        {
            Debug.Log($"? คำนวณคะแนนเสร็จสิ้น! คุณได้คะแนน {passCount}/{totalCount}");
        }

        SendScoreToGoogleSheet(passCount.ToString(), totalCount.ToString());

        var api = ScriptableObject.CreateInstance<TestRunnerApi>();
        api.UnregisterCallbacks(this);
    }

    private void SendScoreToGoogleSheet(string score, string maxScore)
    {
        try
        {
            using (var client = new System.Net.WebClient())
            {
                var values = new System.Collections.Specialized.NameValueCollection();
                values["studentId"] = studentID;
                values["studentName"] = studentName;
                values["section"] = sectionString;
                values["score"] = score;
                values["maxScore"] = maxScore;
                values["week"] = weekName;

                byte[] response = client.UploadValues(googleSheetWebAppURL, values);
                string responseString = System.Text.Encoding.UTF8.GetString(response);
                
                Debug.Log($"<color=green>? ส่งงานเรียบร้อยแล้ว!</color> ข้อมูลถูกบันทึกลง Google Sheet แท็บ Week02 (คะแนน {score}/{maxScore})");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("? การส่งข้อมูลล้มเหลว: " + e.Message);
        }
    }

    public void RunStarted(ITestAdaptor testsToRun) { }
    public void TestStarted(ITestAdaptor test) { }
    public void TestFinished(ITestResultAdaptor result) { }
}
