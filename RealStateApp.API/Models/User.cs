using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RealStateApp.Models
{
    public class User
    {
        public User()
        {
            UserID = Guid.NewGuid();
        }

        public Guid UserID { get; set; }

        [EmailAddress]
        public string UserEmail { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
    }
}
