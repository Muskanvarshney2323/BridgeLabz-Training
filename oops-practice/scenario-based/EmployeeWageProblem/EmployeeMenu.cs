using System;

public class EmployeeMenu
{
    IEmployee empUtil;

    public void ShowMenu()
    {
        empUtil = new EmployeeUtilityImpl();
        while (true)
        {
            Console.WriteLine("\n====== Employee Wage System ======");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Check Employee Attendance");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    empUtil.AddEmployee();
                    break;

                case 2:
                    empUtil.CheckEmployeeAttendance();
                    break;
                case 3:
                    empUtil.CalculateDailyWage();
                    break;

                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid Choice ");
                    break;
            }
        }
    }
}
