namespace UniversityCourseManagement
{
    /// <summary>
    /// Abstract base class for course types
    /// </summary>
    public abstract class CourseType
    {
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int Credits { get; set; }

        protected CourseType(string courseName, string courseCode, int credits)
        {
            CourseName = courseName;
            CourseCode = courseCode;
            Credits = credits;
        }

        public abstract string GetEvaluationMethod();
        public abstract void DisplayCourseDetails();
    }
}
