
// ...existing code...
using Assignment.Core.Interfaces;

namespace Assignment
{
    public class AssignmentConfig
    {
        /// <inheritdoc/>
        public static string AssignmentName => "Assignment 02";

        /// <inheritdoc/>
        public static string AssignmentDescription => "Introduction to data structures and algorithm Conditional/Selection Statement in C# including If/Else Statements, Nested if statement, C# ternary (? :) Operator and Switch statement";

        /// <inheritdoc/>
        public static string AssignmentResourceID => "bu.2025.sem-1.gi262.as02";

        /// <inheritdoc/>
        public static string AssignmentVersion => "1.0.0";

        /// <inheritdoc/>
        public static string[] AssignmentTestcaseFiles => new string[]
        {
            "Assets/Scripts/Workspace/Week02/Assignment_Testcase.cs"
        };

        /// <inheritdoc/>
        public static string AssignmentTestcaseFilesChecksum => "e6464c35c40d4433cdaa8c2a4827b635";

        /// <inheritdoc/>
        public static string ApiBaseUrl => "https://grading-system-bu.happygocoding.com";
    }
}
