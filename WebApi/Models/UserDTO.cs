using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace WebApi.Models
{
    public class UserDTO
    {
        [DataMember(Name = "id")][JsonPropertyName("id")] public Guid Id { get; set; }

        [DataMember(Name = "email")][JsonPropertyName("email")] public string Email { get; set; } = null!;
        [DataMember(Name = "password")][JsonPropertyName("password")] public string Password { get; set; } = null!;
    }
}
