using System;

namespace LoanApproval
{
    // Loan types
    public enum LoanType { Personal, Home, Auto }

    // Loan status (changes are restricted to internal processes)
    public enum LoanStatus { Pending, Approved, Rejected }

    // Applicant with encapsulated credit score (private)
    public class Applicant
    {
        public string Name { get; }
        private int creditScore; // encapsulated as private
        public double Income { get; }
        public double RequestedLoanAmount { get; }

        public Applicant(string name, int creditScore, double income, double requestedLoanAmount)
        {
            Name = name;
            this.creditScore = creditScore;
            Income = income;
            RequestedLoanAmount = requestedLoanAmount;
        }

        // Internal accessor for approval logic only (not publicly exposed)
        internal int GetCreditScoreForApproval() => creditScore;
    }

    // Approve interface
    public interface IApprovable
    {
        bool ApproveLoan(Applicant applicant);
        double CalculateEMI();
    }

    // Base loan application class
    public abstract class LoanApplication : IApprovable
    {
        public LoanType Type { get; }
        public double Principal { get; protected set; }
        public int TermMonths { get; protected set; }
        protected double AnnualInterestRate { get; set; }

        // Loan status setter is internal to restrict modifications to internal processes only
        public LoanStatus Status { get; internal set; } = LoanStatus.Pending;

        protected LoanApplication(LoanType type, double principal, int termMonths, double annualInterestRate)
        {
            Type = type;
            Principal = principal;
            TermMonths = termMonths;
            AnnualInterestRate = annualInterestRate;
        }

        // EMI calculation using standard formula: P*R*(1+R)^N / ((1+R)^N - 1)
        public virtual double CalculateEMI()
        {
            double monthlyRate = AnnualInterestRate / 12.0 / 100.0; // convert to decimal
            double rPowN = Math.Pow(1 + monthlyRate, TermMonths);
            if (monthlyRate == 0) return Principal / TermMonths;
            return Principal * monthlyRate * rPowN / (rPowN - 1);
        }

        // Approve loan using internal approval logic
        public bool ApproveLoan(Applicant applicant)
        {
            bool approved = EvaluateApproval(applicant);
            Status = approved ? LoanStatus.Approved : LoanStatus.Rejected;
            return approved;
        }

        // Encapsulated evaluation logic (private)
        private bool EvaluateApproval(Applicant applicant)
        {
            // Use applicant's internal credit score accessor (not publicly exposed)
            int score = applicant.GetCreditScoreForApproval();

            // Basic checks common to all loan types
            if (applicant.Income <= 0 || applicant.RequestedLoanAmount <= 0) return false;

            // Ratio of income to requested loan (simple affordability check)
            double affordability = applicant.Income * 12 / applicant.RequestedLoanAmount; // yearly income ratio

            // Delegate to type-specific rules
            switch (Type)
            {
                case LoanType.Personal:
                    // personal loans need higher credit score and reasonable affordability
                    return score >= 650 && affordability >= 0.5;
                case LoanType.Home:
                    // home loans accept slightly lower credit score but require strong affordability
                    return score >= 600 && affordability >= 1.0;
                case LoanType.Auto:
                    // auto loans have moderate criteria
                    return score >= 620 && affordability >= 0.4;
                default:
                    return false;
            }
        }
    }

    // Personal loan (uses base logic and base EMI)
    public class PersonalLoan : LoanApplication
    {
        public PersonalLoan(double principal, int termMonths, double annualInterestRate)
            : base(LoanType.Personal, principal, termMonths, annualInterestRate)
        {
        }
    }

    // Home loan: may have different EMI calculation (e.g., subsidized rate or longer term)
    public class HomeLoan : LoanApplication
    {
        public HomeLoan(double principal, int termMonths, double annualInterestRate)
            : base(LoanType.Home, principal, termMonths, annualInterestRate)
        {
        }

        public override double CalculateEMI()
        {
            // Home loans sometimes have a processing buffer or lower effective rate; we emulate a small subsidy
            double effectiveRate = AnnualInterestRate - 0.25; // 0.25% concession as example
            double monthlyRate = effectiveRate / 12.0 / 100.0;
            double rPowN = Math.Pow(1 + monthlyRate, TermMonths);
            if (monthlyRate == 0) return Principal / TermMonths;
            return Principal * monthlyRate * rPowN / (rPowN - 1);
        }
    }

    // Auto loan: may add a processing fee or higher rate
    public class AutoLoan : LoanApplication
    {
        public double ProcessingFee { get; } = 500; // flat processing fee added to principal

        public AutoLoan(double principal, int termMonths, double annualInterestRate)
            : base(LoanType.Auto, principal + 500, termMonths, annualInterestRate)
        {
            ProcessingFee = 500;
        }

        public override double CalculateEMI()
        {
            // Auto loans might have slightly higher rate due to risk
            double effectiveRate = AnnualInterestRate + 0.5; // 0.5% extra
            double monthlyRate = effectiveRate / 12.0 / 100.0;
            double rPowN = Math.Pow(1 + monthlyRate, TermMonths);
            if (monthlyRate == 0) return Principal / TermMonths;
            return Principal * monthlyRate * rPowN / (rPowN - 1);
        }
    }

    // Simple demonstration runner
    public static class LoanBuddyDemo
    {
        public static void Main()
        {
            var alice = new Applicant("Alice", creditScore: 680, income: 60000, requestedLoanAmount: 15000); // personal
            var bob = new Applicant("Bob", creditScore: 610, income: 120000, requestedLoanAmount: 200000); // home
            var carol = new Applicant("Carol", creditScore: 630, income: 40000, requestedLoanAmount: 15000); // auto

            IApprovable personal = new PersonalLoan(principal: 15000, termMonths: 36, annualInterestRate: 10.5);
            IApprovable home = new HomeLoan(principal: 200000, termMonths: 240, annualInterestRate: 6.5);
            IApprovable auto = new AutoLoan(principal: 15000, termMonths: 60, annualInterestRate: 9.0);

            Console.WriteLine("=== LoanBuddy Demo ===");

            Console.WriteLine($"Personal loan EMI (Alice): {personal.CalculateEMI():F2}");
            bool personalApproved = personal.ApproveLoan(alice);
            Console.WriteLine($"Personal loan approved: {personalApproved}");

            Console.WriteLine($"Home loan EMI (Bob): {home.CalculateEMI():F2}");
            bool homeApproved = home.ApproveLoan(bob);
            Console.WriteLine($"Home loan approved: {homeApproved}");

            Console.WriteLine($"Auto loan EMI (Carol): {auto.CalculateEMI():F2}");
            bool autoApproved = auto.ApproveLoan(carol);
            Console.WriteLine($"Auto loan approved: {autoApproved}");

            // Show status via cast back to LoanApplication to access Status (demonstration only)
            var ha = home as LoanApplication;
            Console.WriteLine($"Home loan status: {ha.Status}");
        }
    }
}
