using System.Text.Json.Serialization;

namespace RealStateApp.MAUI.Models
{
    public class Owner
    {
        public int OwnerID { get; set; }

        public string? OwnerFirstName { get; set; }

        public string? OwnerLastName { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
    }
}
