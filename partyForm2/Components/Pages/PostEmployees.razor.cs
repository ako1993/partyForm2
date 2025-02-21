using Microsoft.AspNetCore.Components;
using Microsoft.Identity.Client;
using partyForm2.Data;

namespace partyForm2.Components.Pages
{
    public partial class PostEmployees
    {
        private Employee? Employee { get; set; }
        protected override void OnInitialized()
        {
            //Parse Query paramteres from the URL
            var queryParameters = Navigation.ToAbsoluteUri(Navigation.Uri).Query;
            var parameters = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryParameters);

            //Only override the Employee if it hasn't already been set (from a state service)
            if (parameters.ContainsKey("id") && parameters.ContainsKey("name") && parameters.ContainsKey("email"))
            {

                Employee = new Employee
                {
                    Id = int.Parse(parameters["id"]),
                    Name = parameters["name"],
                    Email = parameters["email"]
                };
            }
        }

        public void showEmployees()
        {
            Console.WriteLine("Navigating to /employees");
            Navigation.NavigateTo("/employees");
        }
    }
}
