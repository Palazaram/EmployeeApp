namespace EmployeeApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string Patronimyc { get; set;}
        public DateTime Birthday { get; set;}
        public string Department { get; set;}
        public string Position { get; set;}
        public decimal Salary { get; set;}
    }
}
