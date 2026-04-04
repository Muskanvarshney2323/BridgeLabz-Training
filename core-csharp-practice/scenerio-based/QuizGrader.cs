using System;

class QuizGrader
{
    static void Main()
    {
        string[] correctAnswers = { "A", "C", "D", "B", "A", "C", "D", "B", "A", "D" };
        string[] studentAnswers = new string[10];

        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Enter Answers for ques" + (i + 1) + ":");
            studentAnswers[i] = Console.ReadLine();
        }

        int score = CalculateScores(correctAnswers, studentAnswers); //method call

        Console.WriteLine("\nTotal Score: " + score + "/10");

        double percentage = (score / 10.0) * 100;
        Console.WriteLine("Percentage: " + percentage + "%");

        if (percentage >= 40)
            Console.WriteLine("PASS");
        else
            Console.WriteLine("FAIL");
    }

    static int CalculateScores(string[] correct, string[] student)
    {
        int score = 0;

        for (int i = 0; i < correct.Length; i++)
        {
            if (correct[i].ToLower() == student[i].ToLower()) //Convert to lower case for case insensitive comparison
            {
                Console.WriteLine("Question " + (i + 1) + ": Correct");
                score++;
            }
            else
            {
                Console.WriteLine("Question " + (i + 1) + ": Incorrect");
            }
        }

        return score;
    }
}
