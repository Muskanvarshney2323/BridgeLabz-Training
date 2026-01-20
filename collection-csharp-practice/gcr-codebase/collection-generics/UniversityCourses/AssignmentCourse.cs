namespace UniversityCourseManagement
{
    /// <summary>
    /// Assignment-based course type
    /// </summary>
    public class AssignmentCourse : CourseType
    {
        public int AssignmentPercentage { get; set; }
        public int ProjectPercentage { get; set; }
        public int ParticipationPercentage { get; set; }

        public AssignmentCourse(string courseName, string courseCode, int credits, int assignment, int project, int participation)
            : base(courseName, courseCode, credits)
        {
            AssignmentPercentage = assignment;
            ProjectPercentage = project;
            ParticipationPercentage = participation;
        }

        public override string GetEvaluationMethod()
        {
            return $"Assignments: {AssignmentPercentage}% | Projects: {ProjectPercentage}% | Participation: {ParticipationPercentage}%";
        }

        public override void DisplayCourseDetails()
        {
            Console.WriteLine($"[ASSIGNMENT COURSE] {CourseCode}: {CourseName} | Credits: {Credits} | {GetEvaluationMethod()}");
        }
    }
}
