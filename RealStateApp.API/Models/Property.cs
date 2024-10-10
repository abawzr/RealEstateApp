using System.Text.Json.Serialization;

namespace RealStateApp.Models
{
    public class Property
    {
        // Primary key
        public int PropertyID { get; set; }

        public int? ListPrice { get; set; }

        public bool? Status { get; set; }
        
        public int? NoOfBedrooms { get; set; }
        
        public int? NoOfBathrooms { get; set; }
        
        public string? City { get; set; }

        public int SalesOfficeID { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
    }
}
