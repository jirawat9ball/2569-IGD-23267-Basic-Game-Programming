using UnityEngine;
using Workspace.Core;

[RequireComponent(typeof(Assignment_Student_Week01))]
public class Assignment_Submitter : Assignment_Submitter_Base
{
    private void Reset()
    {
        weekName = "Week01";
    }

    public Assignment_Submitter()
    {
        weekName = "Week01";
    }
}