using partyForm2.Data;

namespace partyForm2.Services
{
    public class EmployeeStateService
    {
        //property to store the current employee
        public Employee? CurrentEmployee { get; private set; }

        //method to set the employee

        public void SetEmployee(Employee employee)
        {
            CurrentEmployee = employee; 
        }

        //method to get the current employee

        public Employee? GetEmployee()
        {
            return CurrentEmployee;
        }
    }
}
