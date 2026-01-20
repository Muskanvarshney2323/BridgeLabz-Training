namespace UniversityCourseManagement
{
    /// <summary>
    /// Practical-based course type
    /// </summary>
    public class PracticalCourse : CourseType
    {
        public int LabPercentage { get; set; }
        public int ReportPercentage { get; set; }
        public int VivPercentage { get; set; }

        public PracticalCourse(string courseName, string courseCode, int credits, int lab, int report, int viv)
            : base(courseName, courseCode, credits)
        {
            LabPercentage = lab;
            ReportPercentage = report;
            VivPercentage = viv;
        }

        public override string GetEvaluationMethod()
        {
            return $"Lab Work: {LabPercentage}% | Reports: {ReportPercentage}% | Viva: {VivPercentage}%";
        }

        public override void DisplayCourseDetails()
        {
            Console.WriteLine($"[PRACTICAL COURSE] {CourseCode}: {CourseName} | Credits: {Credits} | {GetEvaluationMethod()}");
        }
    }
}
