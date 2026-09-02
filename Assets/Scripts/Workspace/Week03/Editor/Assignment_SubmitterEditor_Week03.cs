using UnityEditor;
using UnityEngine;
using UnityEditor.TestTools.TestRunner.Api;

[CustomEditor(typeof(Assignment_Submitter_Week03))]
public class Assignment_SubmitterEditor_Week03 : Editor
{
    private const string TestAssemblyName = "Workspace.Editor.Week03";

    [MenuItem("Assignment/Submit Score Week 03 %#g")]
    public static void SubmitScoreFromMenu()
    {
        var submitter = Object.FindAnyObjectByType<Assignment_Submitter_Week03>();
        if (!ValidateSubmitter(submitter))
            return;

        RunTestsAndSubmit(submitter);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Assignment_Submitter_Week03 submitter = (Assignment_Submitter_Week03)target;

        GUILayout.Space(20);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
        {
            fontStyle = FontStyle.Bold,
            fontSize = 14,
            fixedHeight = 40
        };

        if (GUILayout.Button("📤 ส่งงานเข้า Google Sheet (Week 03)", buttonStyle))
        {
            if (!ValidateSubmitter(submitter))
                return;

            RunTestsAndSubmit(submitter);
        }
    }

    private static bool ValidateSubmitter(Assignment_Submitter_Week03 submitter)
    {
        if (submitter == null)
        {
            Debug.LogError("❌ ไม่พบ GameObject ที่มี Assignment_Submitter_Week03 ใน Scene! กรุณาเพิ่มลงใน Scene ก่อน");
            return false;
        }

        if (string.IsNullOrEmpty(submitter.studentID) || submitter.studentID == "รหัสนักศึกษา"
            || string.IsNullOrEmpty(submitter.studentName) || submitter.studentName == "ชื่อ-นามสกุล")
        {
            Debug.LogError("❌ กรุณากรอก รหัสนักศึกษา และ ชื่อ-นามสกุล ให้เรียบร้อยก่อนส่งงาน");
            return false;
        }

        if (submitter.section == Assignment_Submitter_Week03.StudentSection.Other && string.IsNullOrEmpty(submitter.customSection))
        {
            Debug.LogError("❌ คุณเลือกกลุ่มเรียนเป็น 'Other' กรุณาระบุกลุ่มเรียนในช่อง Custom Section");
            return false;
        }

        return true;
    }

    private static void RunTestsAndSubmit(Assignment_Submitter_Week03 submitter)
    {
        Debug.Log("⏳ กำลังคำนวณคะแนนจากการรันเทส Week 03...");

        var receiver = ScriptableObject.CreateInstance<TestRunnerCallback_Week03>();
        receiver.hideFlags = HideFlags.HideAndDontSave;

        receiver.studentID = submitter.studentID;
        receiver.studentName = submitter.studentName;
        receiver.sectionString = (submitter.section == Assignment_Submitter_Week03.StudentSection.Other)
            ? submitter.customSection
            : submitter.section.ToString().Replace("_", " ");
        receiver.weekName = submitter.weekName;

        var prop = typeof(Assignment_Submitter_Week03).GetProperty("googleSheetWebAppURL",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        receiver.googleSheetWebAppURL = (string)prop.GetValue(submitter);

        var api = ScriptableObject.CreateInstance<TestRunnerApi>();
        api.RegisterCallbacks(receiver);

        api.Execute(new ExecutionSettings(new Filter()
        {
            testMode = TestMode.EditMode,
            assemblyNames = new string[] { TestAssemblyName }
        }));
    }
}

[System.Serializable]
public class TestRunnerCallback_Week03 : ScriptableObject, ICallbacks
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
                    UnityEngine.Debug.Log("<color=green>✅ ส่งงานเรียบร้อยแล้ว!</color> คะแนน " + score + "/" + maxScore + " ถูกบันทึกลง Google Sheet แท็บ Week03");
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
