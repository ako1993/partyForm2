using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using partyForm2.Data;

namespace partyForm2.Components.Pages
{
    public partial class AddUser
    {
        private Employee? Model { get; set; } = new Employee();
        private string SubmissionResult { get; set; } = string.Empty;

        private bool showConfirmationModal = false; //flag to control the modal visibility

        private bool showSearchModal = false;

        private bool showDeleteModal = false;

        private List<Employee> searchResultsList = new List<Employee>();

        private string searchNameInput;//store the value entered in the search field

        private string deleteNameInput;
        private async Task Submit()
        {
            //Store newly created employee in the state service
            EmployeeStateService.SetEmployee(Model);

            //I had to modify this to set the ID to 0 since an id is generated automatically by the database
            Model.Id = 0;
            //Store newly created employee in the Database
            dbContext.Employees.Add(Model);
            await dbContext.SaveChangesAsync();

            Model = new Employee()
            {
                Id =Model.Id,
                Name = Model.Name,
                Email = Model.Email,
            };

            SubmissionResult = $"New Employee has been added\n" +
                $"Employee Id:{Model.Id}\n" +
                $"Employee Name: {Model.Name}\n" +
                $"Employee Email:{Model.Email}";

            Navigation.NavigateTo($"/postemployees?id={Model.Id}&name={Model.Name}&email={Model.Email}");
        
        }

        public void gotoEmployeesPage()
        {
            Debug.WriteLine("Navigating to the show employees page");
            Navigation.NavigateTo("/employees");
        }

        public void clearAllEmployees()
        {
            showConfirmationModal = true; //Show the confirmation modal
        }

        public async Task clearEmployeesConfirmed() //This will be called when a user clicks "yes" to confirm clearing the database
        {
            //Clear all employees from the database
            dbContext.Employees.RemoveRange(dbContext.Employees);
            await dbContext.SaveChangesAsync();

            showConfirmationModal=false;
            SubmissionResult = "All employees have been cleared from the database";
        }

        public void cancelClear() //This will be called when the user clicks "no" in the confirmation modal
        {
            showConfirmationModal = false;
        }

        public void openSearchModal()
        {
            showSearchModal = true;
        }

        public void openDeleteModal()
        {
            showDeleteModal = true;
        }

        public async Task searchForEmployee()
        {
            var searchName = searchNameInput;
            if (!string.IsNullOrEmpty(searchName))
            {
                //Query the database for employees with a matching first name
                var searchResults = await dbContext.Employees
                    .Where(e => e.Name.Contains(searchName))
                    .ToListAsync();

                if (searchResults.Any())
                {
                    searchResultsList = searchResults; //This will hold the results to display them in the UI
                    SubmissionResult = $"{searchResults.Count} employees found!";
                    showSearchModal = false;
                }
                else
                {
                    SubmissionResult = "No employees found with that name";
                    searchResultsList.Clear();
                    showSearchModal = false;
                }
            }
            else
            {
                SubmissionResult = "Please enter a name to search";
                showSearchModal = false;
            }
           
        }

        private async Task deleteEmployee()
        {
            var nameToDelete = deleteNameInput;
            if(!string.IsNullOrEmpty(nameToDelete))
            {
                //Find employees by first name
                var employeesToDelete = await dbContext.Employees
                    .Where(e => e.Name.Contains(nameToDelete))
                    .ToListAsync();
                if (employeesToDelete.Any())
                {
                    //Delete employees from the database
                    dbContext.Employees.RemoveRange(employeesToDelete);
                    await dbContext.SaveChangesAsync();


                    SubmissionResult = $"{employeesToDelete.Count} employee(s) deleted successfully.";
                }
                else
                {
                    SubmissionResult = "No Employees found with that name";
                }
                showDeleteModal = false;
            }
        }
        public void closeSearchModal()
        {
            showSearchModal = false;
            
        }

        public void closeDeleteModal()
        {
            showDeleteModal = false;
        }
    }
}
