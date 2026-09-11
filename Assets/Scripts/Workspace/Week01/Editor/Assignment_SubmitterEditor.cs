using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor.TestTools.TestRunner.Api;

[CustomEditor(typeof(Assignment_Submitter))]
public class Assignment_SubmitterEditor : Editor
{
    [MenuItem("Assignment/Submit Score Week 01")]
    public static void SubmitScoreFromMenu()
    {
        var submitter = Object.FindAnyObjectByType<Assignment_Submitter>();
        if (!ValidateSubmitter(submitter))
            return;

        RunTestsAndSubmit(submitter);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Assignment_Submitter submitter = (Assignment_Submitter)target;

        GUILayout.Space(20);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
        {
            fontStyle = FontStyle.Bold,
            fontSize = 14,
            fixedHeight = 40
        };

        if (GUILayout.Button("📤 ส่งงานเข้า Google Sheet (Week 01)", buttonStyle))
        {
            if (!ValidateSubmitter(submitter))
                return;

            RunTestsAndSubmit(submitter);
        }
    }

    private static bool ValidateSubmitter(Assignment_Submitter submitter)
    {
        if (submitter == null)
        {
            Debug.LogError("❌ ไม่พบ GameObject ที่มี Assignment_Submitter ใน Scene! กรุณาเปิด Scene Week01_Value ก่อนครับ");
            return false;
        }

        return submitter.ValidateStudentInfo();
    }

    private static void RunTestsAndSubmit(Assignment_Submitter submitter)
    {
        Debug.Log("⏳ กำลังคำนวณคะแนนจากการรันเทส Week 01 (EditMode)...");

        var receiver = ScriptableObject.CreateInstance<TestRunnerCallback_Week01>();
        receiver.hideFlags = HideFlags.HideAndDontSave;

        receiver.studentID = submitter.studentID;
        receiver.studentName = submitter.studentName;
        receiver.sectionString = submitter.GetSectionString();
        receiver.weekName = submitter.weekName;

        receiver.googleSheetWebAppURL = submitter.googleSheetWebAppURL;

        var api = ScriptableObject.CreateInstance<TestRunnerApi>();
        api.RegisterCallbacks(receiver);

        api.Execute(new ExecutionSettings(new Filter()
        {
            testMode = TestMode.EditMode,
            assemblyNames = new string[] { "Workspace.Editor", "Assembly-CSharp-Editor", "Assembly-CSharp-Editor-testable" },
            groupNames = new string[] { submitter.weekName }
        }));
    }
}

[System.Serializable]
public class TestRunnerCallback_Week01 : ScriptableObject, ICallbacks
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
            Debug.LogError("❌ ไม่พบเทสที่จะรัน ระบบจะส่งคะแนน 0/0");
        }
        else
        {
            Debug.Log($"✅ คำนวณคะแนนเสร็จสิ้น! คุณได้คะแนน {passCount}/{totalCount}");
        }

        SendScoreToGoogleSheet(passCount.ToString(), totalCount.ToString());

        var api = ScriptableObject.CreateInstance<TestRunnerApi>();
        api.UnregisterCallbacks(this);
    }

    private void SendScoreToGoogleSheet(string score, string maxScore)
    {
        UnityEditor.EditorApplication.CallbackFunction updateCallback = null;

        WWWForm form = new WWWForm();
        form.AddField("studentId", studentID);
        form.AddField("studentName", studentName);
        form.AddField("section", sectionString);
        form.AddField("score", score);
        form.AddField("maxScore", maxScore);
        form.AddField("week", weekName);

        UnityWebRequest www = UnityWebRequest.Post(googleSheetWebAppURL, form);
        www.SendWebRequest();

        updateCallback = () =>
        {
            if (www.isDone)
            {
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("❌ การส่งคะแนนล้มเหลว: " + www.error);
                }
                else
                {
                    Debug.Log($"<color=green>✅ ส่งคะแนนสำเร็จ!</color> บันทึกลง Google Sheet: {score}/{maxScore} คะแนน (Week 01)");
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
