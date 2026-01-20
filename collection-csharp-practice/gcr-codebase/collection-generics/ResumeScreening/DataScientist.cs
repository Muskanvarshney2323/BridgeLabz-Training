namespace ResumeScreening
{
    /// <summary>
    /// Data Scientist job role
    /// </summary>
    public class DataScientist : JobRole
    {
        public DataScientist()
            : base("Data Scientist", 3,
                new string[] { "Python", "Machine Learning", "SQL", "Statistics", "TensorFlow", "Pandas" })
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

            // 75% based on skills match, 25% based on experience
            score += (matchedSkills * 75.0 / RequiredSkills.Length);
            score += System.Math.Min(experience * 4, 25); // Up to 25% for experience

            return System.Math.Min(score, 100);
        }

        public override void DisplayJobRequirements()
        {
            System.Console.WriteLine($"[DATA SCIENTIST] Min Experience: {MinimumExperience} years");
            System.Console.WriteLine($"Required Skills: {string.Join(", ", RequiredSkills)}");
        }
    }
}
