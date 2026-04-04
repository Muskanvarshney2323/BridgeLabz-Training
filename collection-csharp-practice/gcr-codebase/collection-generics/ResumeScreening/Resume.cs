using System;
using System.Collections.Generic;

namespace ResumeScreening
{
    /// <summary>
    /// Generic Resume class with constraint T : JobRole
    /// Demonstrates: Generic Classes, Constraints
    /// </summary>
    public class Resume<T> where T : JobRole
    {
        public string CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string[] Skills { get; set; }
        public int ExperienceYears { get; set; }
        public string Education { get; set; }
        public double MatchScore { get; set; }
        public T AppliedRole { get; set; }

        public Resume(string candidateId, string candidateName, string[] skills, int experience, string education, T appliedRole)
        {
            CandidateId = candidateId;
            CandidateName = candidateName;
            Skills = skills;
            ExperienceYears = experience;
            Education = education;
            AppliedRole = appliedRole;
            MatchScore = 0;
        }

        /// <summary>
        /// Calculate match score for the applied role
        /// </summary>
        public void CalculateScore()
        {
            MatchScore = AppliedRole.CalculateMatchScore(Skills, ExperienceYears);
        }

        /// <summary>
        /// Check if candidate meets minimum experience requirement
        /// </summary>
        public bool MeetsExperienceRequirement()
        {
            return ExperienceYears >= AppliedRole.MinimumExperience;
        }

        /// <summary>
        /// Display resume information
        /// </summary>
        public void DisplayResume()
        {
            Console.WriteLine($"\n─── Resume ───");
            Console.WriteLine($"ID: {CandidateId}");
            Console.WriteLine($"Name: {CandidateName}");
            Console.WriteLine($"Education: {Education}");
            Console.WriteLine($"Experience: {ExperienceYears} years");
            Console.WriteLine($"Skills: {string.Join(", ", Skills)}");
            Console.WriteLine($"Applied for: {AppliedRole.RoleName}");
        }

        /// <summary>
        /// Display screening result
        /// </summary>
        public void DisplayScreeningResult()
        {
            string status = MatchScore >= 60 ? "✓ PASS" : "❌ FAIL";
            string expStatus = MeetsExperienceRequirement() ? "✓" : "❌";
            Console.WriteLine($"{status} | {CandidateName} ({CandidateId}) | Score: {MatchScore:F1}% | Exp: {expStatus} ({ExperienceYears}y)");
        }

        public override string ToString()
        {
            return $"{CandidateName} for {AppliedRole.RoleName} - Score: {MatchScore:F1}%";
        }
    }
}
