namespace ResumeScreening
{
    /// <summary>
    /// DevOps Engineer job role
    /// </summary>
    public class DevOpsEngineer : JobRole
    {
        public DevOpsEngineer()
            : base("DevOps Engineer", 2,
                new string[] { "Docker", "Kubernetes", "AWS", "Linux", "Jenkins", "Git" })
        {
        }

        public override double CalculateMatchScore(string[] candidateSkills, int experience)
        {
            double score = 0;
            int matchedSkills = 0;

            foreach (var skill in candidateSkills)
            {
                if (System.Linq.Enumerable.Contains(RequiredSkills, skill, System.StringComparer.OrdinalIgnoreCase))
                {
                    matchedSkills++;
                }
            }

            // 70% based on skills match, 30% based on experience
            score += (matchedSkills * 70.0 / RequiredSkills.Length);
            score += System.Math.Min(experience * 5, 30); // Up to 30% for experience

            return System.Math.Min(score, 100);
        }

        public override void DisplayJobRequirements()
        {
            System.Console.WriteLine($"[DEVOPS ENGINEER] Min Experience: {MinimumExperience} years");
            System.Console.WriteLine($"Required Skills: {string.Join(", ", RequiredSkills)}");
        }
    }
}
