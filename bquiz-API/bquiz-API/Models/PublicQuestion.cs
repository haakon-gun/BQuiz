using Newtonsoft.Json;

namespace bquiz_API.Models
{
    public class PublicQuestion
    {
        public int Id { get; set; }
        [JsonProperty("question")]
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
    }
}

