using bquiz_API.Models;
using Microsoft.AspNetCore.Mvc;

// Definerer en API-kontroller for quizzen
[ApiController]
[Route("[controller]")]
public class QuizController : ControllerBase
{
    private QuizService _quizService;

    // QuizController konstruktør som tar QuizService som parameter
    public QuizController(QuizService quizService)
    {
        _quizService = quizService; // Lagrer en referanse til QuizService
    }

    // Endpoint for å hente tittelen på quizzen
    [HttpGet("title")]
    public ActionResult<string> GetTitle()
    {
        var title = _quizService.GetQuizTitle(); // Henter tittelen fra QuizService
        return Ok(new { title = title }); // Returnerer tittelen i en OK-respons
    }

    // Endpoint for å hente et spørsmål
    [HttpGet]
    public ActionResult<Question> Get()
    {
        // Sjekker om det finnes et nåværende spørsmål, og henter et nytt spørsmål om nødvendig
        if (_quizService.GetCurrentQuestion() == null)
        {
            _quizService.MoveToNextQuestion();
            var question = _quizService.GetRandomQuestion();
            return Ok(question); // Returnerer spørsmålet i en OK-respons
        }
        else
        {
            var question = _quizService.GetCurrentQuestion();
            return Ok(question); // Returnerer det nåværende spørsmålet i en OK-respons
        }
    }

    // Endpoint for å poste et svar
    [HttpPost]
    public ActionResult<Question> Post([FromBody] QuizModels answer)
    {
        // Sjekker om det finnes et nåværende spørsmål
        if (_quizService.GetCurrentQuestion() == null)
        {
            // Returnerer en BadRequest-respons om det ikke finnes et nåværende spørsmål
            return BadRequest(new { error = "You must fetch the initial question first." });
        }

        var isCorrect = _quizService.CheckAnswer(answer); // Sjekker svaret
        if (isCorrect)
        {
            // Hvis svaret er riktig, beveger vi oss til neste spørsmål og returnerer det nye spørsmålet
            _quizService.MoveToNextQuestion();
            var question = _quizService.GetRandomQuestion();
            return Ok(question);
        }
        else
        {
            // Hvis svaret er feil, returnerer vi en BadRequest-respons
            return BadRequest(new { error = "Incorrect Answer!" });
        }
    }
}
