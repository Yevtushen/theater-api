using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace WebApi.Models
{
    public class TicketDTO
    {
        [DataMember(Name = "id")][JsonPropertyName("id")] public Guid Id { get; set; }

        [DataMember(Name = "row")][JsonPropertyName("row")] public int Row { get; set; }

        [DataMember(Name = "place")][JsonPropertyName("place")] public int Place { get; set; }

        [DataMember(Name = "price")][JsonPropertyName("price")] public double Price { get; set; }

        [DataMember(Name = "perfomanceId")][JsonPropertyName("perfomanceId")] public Guid PerfomanceId { get; set; }

        [DataMember(Name = "name")][JsonPropertyName("name")] public string Name { get; set; } = null!;

        [DataMember(Name = "email")][JsonPropertyName("email")] public string Email { get; set; } = null!;

    }
}
