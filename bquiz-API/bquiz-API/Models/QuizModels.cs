using bquiz_API.Models;

namespace bquiz_API.Models
{
    public record QuizModels(int QuestionId, int UserAnswer);

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

