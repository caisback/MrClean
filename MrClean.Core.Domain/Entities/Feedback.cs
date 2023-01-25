using Newtonsoft.Json;

namespace MrClean.Core.Domain.Entities
{
    public class Feedback
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Comments { get; set; } = null!;
        public int FixtureId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
