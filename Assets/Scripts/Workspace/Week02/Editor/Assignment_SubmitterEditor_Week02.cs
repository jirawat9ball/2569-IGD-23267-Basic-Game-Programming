using UnityEditor;
using UnityEngine;
using UnityEditor.TestTools.TestRunner.Api;

[CustomEditor(typeof(Assignment_Submitter_Week02))]
public class Assignment_SubmitterEditor_Week02 : Editor
{
    [MenuItem("Assignment/Submit Score Week 02 %g")]
    public static void SubmitScoreFromMenu()
    {
        var submitter = Object.FindObjectOfType<Assignment_Submitter_Week02>();
        if (submitter == null)
        {
            Debug.LogError("ไม่พบ GameObject ชื่อ Assignment_Submitter_Week02 ใน Scene! กรุณาเพิ่มลงใน Scene ด้วยครับ");
            return;
        }

        if (string.IsNullOrEmpty(submitter.studentID) || submitter.studentID == "รหัสนักศึกษา" || string.IsNullOrEmpty(submitter.studentName))
        {
            Debug.LogError("กรุณาใส่ รหัสนักศึกษา และ ชื่อ-นามสกุล");
            return;
        }

        if (submitter.section == Assignment_Submitter_Week02.StudentSection.Other && string.IsNullOrEmpty(submitter.customSection))
        {
            Debug.LogError("หากเลือก 'Other' กรุณาระบุ Section ในช่อง Custom Section ด้วยครับ");
            return;
        }

        Debug.Log("กำลังเตรียมส่งคะแนน...");

        TestRunnerCallback receiver = ScriptableObject.CreateInstance<TestRunnerCallback>(); 
        receiver.hideFlags = HideFlags.HideAndDontSave;
        
        receiver.studentID = submitter.studentID;
        receiver.studentName = submitter.studentName;
        receiver.sectionString = (submitter.section == Assignment_Submitter_Week02.StudentSection.Other) ? submitter.customSection : submitter.section.ToString().Replace("_", " ");
        receiver.weekName = submitter.weekName;

        var prop = typeof(Assignment_Submitter_Week02).GetProperty("googleSheetWebAppURL", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        receiver.googleSheetWebAppURL = (string)prop.GetValue(submitter);

        var api = ScriptableObject.CreateInstance<TestRunnerApi>();
        api.RegisterCallbacks(receiver);

        api.Execute(new ExecutionSettings(new Filter() { 
            testMode = TestMode.EditMode,
            assemblyNames = new string[] { "Workspace.Editor", "Assembly-CSharp-Editor", "Assembly-CSharp-Editor-testable" },
            groupNames = new string[] { submitter.weekName }
        }));
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Assignment_Submitter_Week02 submitter = (Assignment_Submitter_Week02)target;

        GUILayout.Space(20);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontStyle = FontStyle.Bold;
        buttonStyle.fontSize = 14;
        buttonStyle.fixedHeight = 40;
        
        if (GUILayout.Button("ส่งงานเข้า Google Sheet", buttonStyle))
        {
            if (string.IsNullOrEmpty(submitter.studentID) || submitter.studentID == "รหัสนักศึกษา" || string.IsNullOrEmpty(submitter.studentName))
            {
                Debug.LogError("กรุณาใส่ รหัสนักศึกษา และ ชื่อ-นามสกุล ก่อนส่งงาน");
                return;
            }

            if (submitter.section == Assignment_Submitter_Week02.StudentSection.Other && string.IsNullOrEmpty(submitter.customSection))
            {
                Debug.LogError("หากเลือก 'Other' กรุณาระบุ Section ในช่อง Custom Section ด้วยครับ");
                return;
            }

            Debug.Log("กำลังตรวจคะแนนและส่งงาน...");

            TestRunnerCallback receiver = ScriptableObject.CreateInstance<TestRunnerCallback>(); receiver.hideFlags = HideFlags.HideAndDontSave;
            
            receiver.studentID = submitter.studentID;
            receiver.studentName = submitter.studentName;
            receiver.sectionString = (submitter.section == Assignment_Submitter_Week02.StudentSection.Other) ? submitter.customSection : submitter.section.ToString().Replace("_", " ");
            receiver.weekName = submitter.weekName;

            var prop = typeof(Assignment_Submitter_Week02).GetProperty("googleSheetWebAppURL", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            receiver.googleSheetWebAppURL = (string)prop.GetValue(submitter);

            var api = ScriptableObject.CreateInstance<TestRunnerApi>();
            api.RegisterCallbacks(receiver);

            // รัน EditMode เพื่อตรวจสอบ (เหมือนการรัน Test) โดยกรองเฉพาะประจำสัปดาห์นั้น
            api.Execute(new ExecutionSettings(new Filter() { 
                testMode = TestMode.EditMode,
                assemblyNames = new string[] { "Workspace.Editor", "Assembly-CSharp-Editor", "Assembly-CSharp-Editor-testable" },
                groupNames = new string[] { submitter.weekName }
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
            Debug.LogError("ไม่พบการทดสอบ ได้คะแนน 0/0");
        }
        else
        {
            Debug.Log($"ตรวจคะแนนเสร็จสิ้น! คุณได้คะแนน {passCount}/{totalCount}");
        }

        SendScoreToGoogleSheet(passCount.ToString(), totalCount.ToString());

        var api = ScriptableObject.CreateInstance<TestRunnerApi>();
        api.UnregisterCallbacks(this);
    }

    private void SendScoreToGoogleSheet(string score, string maxScore)
    {
        UnityEditor.EditorApplication.CallbackFunction updateCallback = null;
        UnityEngine.WWWForm form = new UnityEngine.WWWForm();
        form.AddField("studentId", studentID);
        form.AddField("studentName", studentName);
        form.AddField("section", sectionString);
        form.AddField("score", score);
        form.AddField("maxScore", maxScore);
        form.AddField("week", weekName);
        UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Post(googleSheetWebAppURL, form);
        www.SendWebRequest();
        updateCallback = () =>
        {
            if (www.isDone)
            {
                if (www.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    UnityEngine.Debug.LogError("Error: " + www.error);
                }
                else
                {
                    UnityEngine.Debug.Log("<color=green>Success!</color> Score " + score + "/" + maxScore + " submitted to Google Sheet.");
                }
                www.Dispose();
                UnityEditor.EditorApplication.update -= updateCallback;
            }
        };
        UnityEditor.EditorApplication.update += updateCallback;
    }

    public void RunStarted(ITestAdaptor testsToRun) { }
    public void TestStarted(ITestAdaptor test) { }
    public void TestFinished(ITestResultAdaptor result) { }
}
