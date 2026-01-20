namespace ResumeScreening
{
    /// <summary>
    /// Abstract base class for job roles
    /// </summary>
    public abstract class JobRole
    {
        public string RoleName { get; set; }
        public int MinimumExperience { get; set; }
        public string[] RequiredSkills { get; set; }

        protected JobRole(string roleName, int minExperience, string[] requiredSkills)
        {
            RoleName = roleName;
            MinimumExperience = minExperience;
            RequiredSkills = requiredSkills;
        }

        public abstract double CalculateMatchScore(string[] candidateSkills, int experience);
        public abstract void DisplayJobRequirements();
    }
}
