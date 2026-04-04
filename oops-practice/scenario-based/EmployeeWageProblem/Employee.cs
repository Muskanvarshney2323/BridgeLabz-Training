public class Employee
{
    private int empID;
    private string empName;
    private bool isPresent;
    private int dailyWage;

    public Employee(int empID, string empName, bool isPresent)
    {
        this.empID = empID;
        this.empName = empName;
        this.isPresent = isPresent;
        CalculateDailyWage();
    }
     private void CalculateDailyWage()
    {
        dailyWage = isPresent ? 800 : 0;
    }

    // Getter methods
    public int GetEmpID()
    {
        return empID;
    }

    public string GetEmpName()
    {
        return empName;
    }

    public bool IsPresent()
    {
        return isPresent;
    }
     public int GetDailyWage()
    {
        return dailyWage;
    }
}
