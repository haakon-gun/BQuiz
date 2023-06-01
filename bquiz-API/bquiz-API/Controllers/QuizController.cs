using bquiz_API.Models;
using Newtonsoft.Json;

public class QuizService
{
    private List<Question> _questions;
    private Dictionary<int, int> _correctAnswers = new Dictionary<int, int>();
    private int _currentQuestionIndex = -1;

    // Konstruktør som henter og setter opp spørsmål ved opprettelsen av QuizService-objektet
    public QuizService()
    {
        var jsonData = File.ReadAllText("questions.json"); // Henter spørsmålene fra en JSON-fil
        var root = JsonConvert.DeserializeObject<Root>(jsonData); // Parser JSON til Root-objekt
        _questions = root.Quiz.Questions; // Setter opp spørsmålsliste
        for (int i = 0; i < _questions.Count; i++)
        {
            int id = i + 1;
            _questions[i].Id = id; // Setter id for hvert spørsmål
            _correctAnswers[id] = _questions[i].CorrectAnswer; // Lagrer riktige svar i et ordbok
        }
    }

    // Metode for å hente quizens tittel
    public string GetQuizTitle()
    {
        var jsonData = File.ReadAllText("questions.json");
        var root = JsonConvert.DeserializeObject<Root>(jsonData);
        return root.Quiz.Title; // Returnerer quizens tittel
    }

    // Metode for å hente et tilfeldig spørsmål fra listen
    public PublicQuestion GetRandomQuestion()
    {
        var random = new Random();
        var index = random.Next(_questions.Count);
        var question = _questions[index]; // Velger et tilfeldig spørsmål

        var publicQuestion = new PublicQuestion
        {
            Id = question.Id,
            QuestionText = question.QuestionText,
            Options = question.Options
        };

        return publicQuestion; // Returnerer det tilfeldige spørsmålet
    }

    // Metode for å hente det nåværende spørsmålet basert på _currentQuestionIndex
    public PublicQuestion GetCurrentQuestion()
    {
        if (_currentQuestionIndex < 0 || _currentQuestionIndex >= _questions.Count)
        {
            return null; // Returnerer null hvis indeksen ikke er gyldig
        }

        var question = _questions[_currentQuestionIndex];

        var publicQuestion = new PublicQuestion
        {
            Id = question.Id,
            QuestionText = question.QuestionText,
            Options = question.Options
        };

        return publicQuestion; // Returnerer det nåværende spørsmålet
    }

    // Metode for å få antallet spørsmål i quizzen
    public int GetQuestionsCount()
    {
        return _questions.Count; // Returnerer antallet spørsmål
    }

    // Metode for å sjekke om et gitt svar er riktig
    public bool CheckAnswer(QuizModels answer)
    {
        // Sjekker om svaret er riktig og returnerer resultatet
        return _correctAnswers.TryGetValue(answer.QuestionId, out var correctAnswer) && correctAnswer == answer.UserAnswer;
    }

    // Metode for å flytte til neste spørsmål ved å øke _currentQuestionIndex
    public void MoveToNextQuestion()
    {
        _currentQuestionIndex++;
    }
}
