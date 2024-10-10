using System.Text.Json.Serialization;

namespace RealStateApp.Models
{
    public class Owner
    {
        // Primary key
        public int OwnerID { get; set; }

        public string? OwnerFirstName { get; set; }

        public string? OwnerLastName { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
    }
}
