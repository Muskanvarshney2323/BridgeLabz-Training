using System;
using System.Collections.Generic;
using System.Linq;

namespace ResumeScreening
{
    /// <summary>
    /// Resume screening pipeline with generic support
    /// Demonstrates: Generic Methods, List<T> for multiple job roles
    /// </summary>
    public class ResumeScreeningPipeline
    {
        private List<Resume<SoftwareEngineer>> swResumes;
        private List<Resume<DataScientist>> dsResumes;
        private List<Resume<DevOpsEngineer>> devopsResumes;

        public ResumeScreeningPipeline()
        {
            swResumes = new List<Resume<SoftwareEngineer>>();
            dsResumes = new List<Resume<DataScientist>>();
            devopsResumes = new List<Resume<DevOpsEngineer>>();
        }

        /// <summary>
        /// Add resume to Software Engineer pipeline
        /// </summary>
        public void AddSWResume(Resume<SoftwareEngineer> resume)
        {
            swResumes.Add(resume);
        }

        /// <summary>
        /// Add resume to Data Scientist pipeline
        /// </summary>
        public void AddDSResume(Resume<DataScientist> resume)
        {
            dsResumes.Add(resume);
        }

        /// <summary>
        /// Add resume to DevOps Engineer pipeline
        /// </summary>
        public void AddDevOpsResume(Resume<DevOpsEngineer> resume)
        {
            devopsResumes.Add(resume);
        }

        /// <summary>
        /// Generic method to screen resumes
        /// Demonstrates: Generic Methods with List<T> and constraints
        /// </summary>
        public List<Resume<T>> ScreenResumes<T>(List<Resume<T>> resumeList, double passThreshold = 60) where T : JobRole
        {
            var passed = new List<Resume<T>>();

            foreach (var resume in resumeList)
            {
                resume.CalculateScore();
                if (resume.MatchScore >= passThreshold && resume.MeetsExperienceRequirement())
                {
                    passed.Add(resume);
                }
            }

            return passed.OrderByDescending(r => r.MatchScore).ToList();
        }

        /// <summary>
        /// Display all screening results
        /// </summary>
        public void DisplayAllResults()
        {
            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("RESUME SCREENING RESULTS");
            Console.WriteLine("═══════════════════════════════════════════════");

            DisplayScreeningResults("Software Engineer Position", swResumes);
            DisplayScreeningResults("Data Scientist Position", dsResumes);
            DisplayScreeningResults("DevOps Engineer Position", devopsResumes);
        }

        private void DisplayScreeningResults<T>(string position, List<Resume<T>> resumes) where T : JobRole
        {
            Console.WriteLine($"\n─── {position} ───");

            if (resumes.Count == 0)
            {
                Console.WriteLine("No resumes submitted.");
                return;
            }

            foreach (var resume in resumes)
            {
                resume.CalculateScore();
                resume.DisplayScreeningResult();
            }
        }

        /// <summary>
        /// Get statistics for a position
        /// </summary>
        public void DisplayStatistics<T>(string position, List<Resume<T>> resumes) where T : JobRole
        {
            if (resumes.Count == 0)
            {
                Console.WriteLine($"No data for {position}");
                return;
            }

            var screened = ScreenResumes(resumes);
            double avgScore = resumes.Average(r => { r.CalculateScore(); return r.MatchScore; });

            Console.WriteLine($"\n─── {position} Statistics ───");
            Console.WriteLine($"Total Applications: {resumes.Count}");
            Console.WriteLine($"Passed Screening: {screened.Count}");
            Console.WriteLine($"Pass Rate: {(screened.Count * 100.0 / resumes.Count):F1}%");
            Console.WriteLine($"Average Score: {avgScore:F1}%");
        }
    }
}
