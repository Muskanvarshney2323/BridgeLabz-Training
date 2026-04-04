namespace UniversityCourseManagement
{
    /// <summary>
    /// Exam-based course type
    /// </summary>
    public class ExamCourse : CourseType
    {
        public int ExamPercentage { get; set; }
        public int MidtermPercentage { get; set; }

        public ExamCourse(string courseName, string courseCode, int credits, int midterm, int exam)
            : base(courseName, courseCode, credits)
        {
            MidtermPercentage = midterm;
            ExamPercentage = exam;
        }

        public override string GetEvaluationMethod()
        {
            return $"Midterm: {MidtermPercentage}% | Final Exam: {ExamPercentage}%";
        }

        public override void DisplayCourseDetails()
        {
            Console.WriteLine($"[EXAM COURSE] {CourseCode}: {CourseName} | Credits: {Credits} | {GetEvaluationMethod()}");
        }
    }
}
