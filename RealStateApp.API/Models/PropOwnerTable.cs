using System.Text.Json.Serialization;

namespace RealStateApp.Models
{
    public class PropOwnerTable
    {
        // Composite key
        public int OwnerID { get; set; }
        public int PropertyID { get; set; }

        // Percent with two decimal, it has been configured in AppDbContext.cs
        public decimal? PercentOwned { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
    }
}
