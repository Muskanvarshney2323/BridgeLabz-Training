using System;
using System.Collections.Generic;

namespace ResumeScreening
{
    /// <summary>
    /// Problem 5: AI-Driven Resume Screening System
    /// Concepts: Generic Classes, Generic Methods, Constraints
    /// 
    /// Story: Build an intelligent resume screening system that processes resumes
    /// for different job roles (Software Engineer, Data Scientist, DevOps Engineer)
    /// while ensuring type safety through generics and constraints.
    /// 
    /// Key Features:
    /// - Abstract JobRole base class with role-specific requirements
    /// - Generic Resume<T> class constrained to JobRole
    /// - Generic ScreenResumes<T>() method for pipeline processing
    /// - List<T> to manage multiple job role applications
    /// - Intelligent scoring based on skills and experience match
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║  PROBLEM 5: AI-Driven Resume Screening        ║");
            Console.WriteLine("║  Concepts: Generic Classes & Methods          ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝\n");

            var pipeline = new ResumeScreeningPipeline();

            // Define job roles
            var swRole = new SoftwareEngineer();
            var dsRole = new DataScientist();
            var devopsRole = new DevOpsEngineer();

            // Display job requirements
            Console.WriteLine("─── Job Requirements ───");
            swRole.DisplayJobRequirements();
            Console.WriteLine();
            dsRole.DisplayJobRequirements();
            Console.WriteLine();
            devopsRole.DisplayJobRequirements();

            // Create resumes for Software Engineer position
            Console.WriteLine("\n─── Processing Software Engineer Candidates ───");
            var swResume1 = new Resume<SoftwareEngineer>("SW001", "Alice Johnson", 
                new string[] { "C#", "Java", "SQL", "Git", "Python" }, 3, "B.Tech CS", swRole);
            var swResume2 = new Resume<SoftwareEngineer>("SW002", "Bob Smith", 
                new string[] { "Java", "C", "HTML" }, 1, "B.Tech IT", swRole);
            var swResume3 = new Resume<SoftwareEngineer>("SW003", "Charlie Brown", 
                new string[] { "C#", "SQL", "OOP", "Git", "Python", "Java" }, 5, "M.Tech CS", swRole);

            pipeline.AddSWResume(swResume1);
            pipeline.AddSWResume(swResume2);
            pipeline.AddSWResume(swResume3);

            // Create resumes for Data Scientist position
            Console.WriteLine("\n─── Processing Data Scientist Candidates ───");
            var dsResume1 = new Resume<DataScientist>("DS001", "Diana Prince", 
                new string[] { "Python", "Machine Learning", "TensorFlow", "SQL" }, 4, "M.Tech ML", dsRole);
            var dsResume2 = new Resume<DataScientist>("DS002", "Ethan Hunt", 
                new string[] { "Python", "SQL", "Statistics" }, 2, "B.Tech CS", dsRole);
            var dsResume3 = new Resume<DataScientist>("DS003", "Fiona Green", 
                new string[] { "Python", "Machine Learning", "TensorFlow", "Pandas", "SQL", "Statistics" }, 6, "PhD Data Science", dsRole);

            pipeline.AddDSResume(dsResume1);
            pipeline.AddDSResume(dsResume2);
            pipeline.AddDSResume(dsResume3);

            // Create resumes for DevOps Engineer position
            Console.WriteLine("\n─── Processing DevOps Engineer Candidates ───");
            var devopsResume1 = new Resume<DevOpsEngineer>("DO001", "George Miller", 
                new string[] { "Docker", "Kubernetes", "AWS", "Linux", "Jenkins" }, 3, "B.Tech IT", devopsRole);
            var devopsResume2 = new Resume<DevOpsEngineer>("DO002", "Hannah Woods", 
                new string[] { "Docker", "Git", "Linux" }, 1, "B.Tech CS", devopsRole);
            var devopsResume3 = new Resume<DevOpsEngineer>("DO003", "Ian Clark", 
                new string[] { "Docker", "Kubernetes", "AWS", "Linux", "Jenkins", "Git" }, 4, "B.Tech IT", devopsRole);

            pipeline.AddDevOpsResume(devopsResume1);
            pipeline.AddDevOpsResume(devopsResume2);
            pipeline.AddDevOpsResume(devopsResume3);

            // Display individual resumes
            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("CANDIDATE DETAILS:");
            Console.WriteLine("═══════════════════════════════════════════════");
            
            swResume1.DisplayResume();
            swResume2.DisplayResume();
            swResume3.DisplayResume();

            dsResume1.DisplayResume();
            dsResume2.DisplayResume();
            dsResume3.DisplayResume();

            devopsResume1.DisplayResume();
            devopsResume2.DisplayResume();
            devopsResume3.DisplayResume();

            // Perform screening using generic method with List<T>
            var screenedSW = pipeline.ScreenResumes(new List<Resume<SoftwareEngineer>> { swResume1, swResume2, swResume3 });
            var screenedDS = pipeline.ScreenResumes(new List<Resume<DataScientist>> { dsResume1, dsResume2, dsResume3 });
            var screenedDO = pipeline.ScreenResumes(new List<Resume<DevOpsEngineer>> { devopsResume1, devopsResume2, devopsResume3 });

            // Display results
            pipeline.DisplayAllResults();

            // Display statistics
            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("SCREENING STATISTICS:");
            Console.WriteLine("═══════════════════════════════════════════════");
            
            pipeline.DisplayStatistics("Software Engineer", new List<Resume<SoftwareEngineer>> { swResume1, swResume2, swResume3 });
            pipeline.DisplayStatistics("Data Scientist", new List<Resume<DataScientist>> { dsResume1, dsResume2, dsResume3 });
            pipeline.DisplayStatistics("DevOps Engineer", new List<Resume<DevOpsEngineer>> { devopsResume1, devopsResume2, devopsResume3 });

            // Display shortlisted candidates
            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("SHORTLISTED CANDIDATES (RANKED):");
            Console.WriteLine("═══════════════════════════════════════════════");
            
            Console.WriteLine("\n─── Software Engineer Shortlist ───");
            foreach (var resume in screenedSW)
            {
                Console.WriteLine($"✓ {resume.CandidateName} - {resume.MatchScore:F1}%");
            }

            Console.WriteLine("\n─── Data Scientist Shortlist ───");
            foreach (var resume in screenedDS)
            {
                Console.WriteLine($"✓ {resume.CandidateName} - {resume.MatchScore:F1}%");
            }

            Console.WriteLine("\n─── DevOps Engineer Shortlist ───");
            foreach (var resume in screenedDO)
            {
                Console.WriteLine($"✓ {resume.CandidateName} - {resume.MatchScore:F1}%");
            }
        }
    }
}
