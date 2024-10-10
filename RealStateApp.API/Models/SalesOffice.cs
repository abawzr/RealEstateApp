using System.Text.Json.Serialization;

namespace RealStateApp.Models
{
    public class SalesOffice
    {
        // Primary key
        public int OfficeID { get; set; }

        public string? OfficeName { get; set; }

        public int? AddressID { get; set; }

        public int? ManagedByEmployeeID { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
    }
}