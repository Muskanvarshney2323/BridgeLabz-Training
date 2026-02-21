using HealthClinicApp.Services;

namespace HealthClinicApp.Menus
{
    public class AdminMenu
    {
        AdminService service = new AdminService();

        public void Show()
        {
            Console.WriteLine("\n--- Admin Menu ---");
            Console.WriteLine("1. Add Specialty");
            Console.WriteLine("2. View Specialties");
            Console.WriteLine("3. Update Specialty");
            Console.WriteLine("4. Delete Specialty");
            Console.WriteLine("5. Backup Tables");
            Console.WriteLine("6. View Audit Logs");
            Console.WriteLine("0. Back");

            int ch = int.Parse(Console.ReadLine());

            switch (ch)
            {
                case 1:
                    service.AddSpecialty(Console.ReadLine(), Console.ReadLine());
                    break;
                case 2:
                    service.ViewSpecialties();
                    break;
                case 3:
                    service.UpdateSpecialty(int.Parse(Console.ReadLine()), Console.ReadLine(), Console.ReadLine());
                    break;
                case 4:
                    service.DeleteSpecialty(int.Parse(Console.ReadLine()));
                    break;
                case 5:
                    service.BackupCriticalTables();
                    break;
                case 6:
                    service.ViewAuditLogs(
                        Console.ReadLine(),
                        Console.ReadLine(),
                        DateTime.Parse(Console.ReadLine()),
                        DateTime.Parse(Console.ReadLine()));
                    break;
                case 7: return;
            }
        }
    }
}
