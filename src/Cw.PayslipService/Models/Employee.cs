namespace Cw.PayslipService.Models
{
    public class Employee : AuthenticateModel
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}
