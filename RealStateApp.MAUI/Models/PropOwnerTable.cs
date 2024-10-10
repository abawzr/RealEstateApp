using System.Text.Json.Serialization;

namespace RealStateApp.MAUI.Models
{
    public class PropOwnerTable
    {
        public int OwnerID { get; set; }
        public int PropertyID { get; set; }

        public decimal? PercentOwned { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
    }
}
