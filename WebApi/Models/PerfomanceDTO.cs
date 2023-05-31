using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace WebApi.Models
{
    public class PerfomanceDTO
    {
        [DataMember(Name = "id")][JsonPropertyName("id")] public Guid Id { get; set; }

        [DataMember(Name = "name")][JsonPropertyName("name")] public string Name { get; set; } = null!;

        [DataMember(Name = "description")][JsonPropertyName("description")] public string Description { get; set; } = null!;

        [DataMember(Name = "dateAndTime")][JsonPropertyName("dateAndTime")] public DateTimeOffset DateAndTime { get; set; }
    }
}
