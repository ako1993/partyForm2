using partyForm2.Data;

namespace partyForm2.Components.Pages
{
    public partial class ShowEmployees
    {
        private List<Employee> employees;

        protected override async Task OnInitializedAsync()
        {
            employees = await EmployeeDBService.GetAllEmployeesAsync();
        }
    }
}
