using bquiz_API.Models;
using Newtonsoft.Json;


public class QuizService : IQuizService
{
    private List<Question> _questions;
    private Dictionary<int, int> _correctAnswers = new Dictionary<int, int>();
    private int _currentQuestionIndex = -1;

    public QuizService()
    {
        var jsonData = File.ReadAllText("questions.json");
        var root = JsonConvert.DeserializeObject<Root>(jsonData);
        _questions = root.Quiz.Questions;
        for (int i = 0; i < _questions.Count; i++)
        {
            int id = i + 1;
            _questions[i].Id = id;
            _correctAnswers[id] = _questions[i].CorrectAnswer;
        }
    }

    public string GetQuizTitle()
    {
        var jsonData = File.ReadAllText("questions.json");
        var root = JsonConvert.DeserializeObject<Root>(jsonData);
        return root.Quiz.Title;
    }

    public PublicQuestion GetRandomQuestion()
    {
        var random = new Random();
        var index = random.Next(_questions.Count);
        var question = _questions[index];

        var publicQuestion = new PublicQuestion
        {
            Id = question.Id,
            QuestionText = question.QuestionText,
            Options = question.Options
        };

        return publicQuestion;
    }

    public PublicQuestion GetCurrentQuestion()
    {
        if (_currentQuestionIndex < 0 || _currentQuestionIndex >= _questions.Count)
        {
            return null;
        }

        var question = _questions[_currentQuestionIndex];

        var publicQuestion = new PublicQuestion
        {
            Id = question.Id,
            QuestionText = question.QuestionText,
            Options = question.Options
        };

        return publicQuestion;
    }

    public int GetQuestionsCount()
    {
        return _questions.Count;
    }


    public bool CheckAnswer(QuizModels answer)
    {
        return _correctAnswers.TryGetValue(answer.QuestionId, out var correctAnswer) && correctAnswer == answer.UserAnswer;
    }

    public void MoveToNextQuestion()
    {
        _currentQuestionIndex++;
    }
}
