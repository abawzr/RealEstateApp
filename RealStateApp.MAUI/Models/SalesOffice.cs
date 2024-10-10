using System.Text.Json.Serialization;

namespace RealStateApp.MAUI.Models
{
    public class SalesOffice
    {
        public int OfficeID { get; set; }

        public string? OfficeName { get; set; }

        public int? AddressID { get; set; }

        public int? ManagedByEmployeeID { get; set; }

        // Helper property to display "N/A" if ManagedByEmployeeID is null
        public string ManagedByEmployeeDisplay => ManagedByEmployeeID.HasValue ? ManagedByEmployeeID.Value.ToString() : "N/A";

        public string AddressLine { get; set; }
        public string ManagerName { get; set; }
        public int NumberOfProperties { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
    }
}
