using System.Text.Json.Serialization;

namespace RealStateApp.Models
{
    public class Employee
    {
        // Primary key
        public int EmpID { get; set; }

        public string? EmpFirstName { get; set; }

        public string? EmpLastName { get; set; }

        public int? SalesOfficeID { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? Age { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }

    }
}