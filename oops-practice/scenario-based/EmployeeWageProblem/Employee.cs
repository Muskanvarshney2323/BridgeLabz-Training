public class Employee
{
    private int empID;
    private string empName;
    private bool isPresent;

    public Employee(int empID, string empName, bool isPresent)
    {
        this.empID = empID;
        this.empName = empName;
        this.isPresent = isPresent;
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
}
