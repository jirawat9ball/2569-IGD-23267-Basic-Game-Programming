using UnityEditor;
using UnityEngine;
using UnityEditor.TestTools.TestRunner.Api;

[CustomEditor(typeof(Assignment_Submitter_Week05))]
public class Assignment_SubmitterEditor_Week05 : Editor
{
    [MenuItem("Assignment/Submit Score Week 05 %#j")]
    public static void SubmitScoreFromMenu()
    {
        var submitter = Object.FindAnyObjectByType<Assignment_Submitter_Week05>();
        if (!ValidateSubmitter(submitter))
            return;

        RunTestsAndSubmit(submitter);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Assignment_Submitter_Week05 submitter = (Assignment_Submitter_Week05)target;

        GUILayout.Space(20);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
        {
            fontStyle = FontStyle.Bold,
            fontSize = 14,
            fixedHeight = 40
        };

        if (GUILayout.Button("📤 ส่งงานเข้า Google Sheet (Week 05)", buttonStyle))
        {
            if (!ValidateSubmitter(submitter))
                return;

            RunTestsAndSubmit(submitter);
        }
    }

    private static bool ValidateSubmitter(Assignment_Submitter_Week05 submitter)
    {
        if (submitter == null)
        {
            Debug.LogError("❌ ไม่พบ GameObject ที่มี Assignment_Submitter_Week05 ใน Scene! กรุณาเพิ่มลงใน Scene ก่อน");
            return false;
        }

        return submitter.ValidateStudentInfo();
    }

    private static void RunTestsAndSubmit(Assignment_Submitter_Week05 submitter)
    {
        Debug.Log("⏳ กำลังคำนวณคะแนนจากการรันเทส Week 05...");

        var receiver = ScriptableObject.CreateInstance<TestRunnerCallback_Week05>();
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
public class TestRunnerCallback_Week05 : ScriptableObject, ICallbacks
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
                    UnityEngine.Debug.LogError("❌ การส่งข้อมูลล้มเหลว: " + www.error);
                }
                else
                {
                    UnityEngine.Debug.Log("<color=green>✅ ส่งงานเรียบร้อยแล้ว!</color> คะแนน " + score + "/" + maxScore + " ถูกบันทึกลง Google Sheet แท็บ Week05");
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
