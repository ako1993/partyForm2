using System.ComponentModel.DataAnnotations;
namespace partyForm2.Data
{
    public class Employee
    {
        public int Id { get; set; }

        //Validation is written into the object class. These requirements will be utilized due to the DataAnnotationsValidator in the form UI

        [Required(ErrorMessage ="Employee Name is required.")]
        [StringLength(100, ErrorMessage ="Employee name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Employee email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }   
    }
}
