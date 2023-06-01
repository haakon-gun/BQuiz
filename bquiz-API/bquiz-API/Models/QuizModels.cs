using bquiz_API.Models;
using Newtonsoft.Json;

namespace bquiz_API.Models
{
    public class QuizModels
    {
        public int QuestionId { get; set; }
        public int UserAnswer { get; set; }
    }

    public class PublicQuestion
    {
        public int Id { get; set; }
        [JsonProperty("question")]
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
    }

    public class Question : PublicQuestion
    {
        public int CorrectAnswer { get; set; }
    }



    public class Quiz
    {
        public string Title { get; set; }
        public List<Question> Questions { get; set; }
    }

    public class Root
    {
        public Quiz Quiz { get; set; }
    }
}

