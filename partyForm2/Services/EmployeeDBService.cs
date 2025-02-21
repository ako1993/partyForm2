using Microsoft.EntityFrameworkCore;
using partyForm2.Data;
using partyForm2.Database;
namespace partyForm2.Services
{
    public class EmployeeDBService
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeDBService(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        //Method to retrieve all employees from the database
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeDbContext.Employees.ToListAsync();
        }

        //Method to save a new employee to the database
        public async Task SaveEmployeeAsync(Employee employee)
        {
            _employeeDbContext.Employees.Add(employee);
            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
