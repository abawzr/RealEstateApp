using System.Text.Json.Serialization;

namespace RealStateApp.MAUI.Models
{
    public class Address
    {
        public int AddressID { get; set; }

        public string? AddressLine { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public int? ZipCode { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
    }
}