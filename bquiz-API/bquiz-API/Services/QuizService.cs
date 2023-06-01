using bquiz_API.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class QuizController : ControllerBase
{
    private QuizService _quizService;

    public QuizController(QuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpGet("title")]
    public ActionResult<string> GetTitle()
    {
        var title = _quizService.GetQuizTitle();
        return Ok(new { title = title });
    }

    [HttpGet]
    public ActionResult<Question> Get()
    {
        if (_quizService.GetCurrentQuestion() == null)
        {
            _quizService.MoveToNextQuestion();
            var question = _quizService.GetRandomQuestion();
            return Ok(question);
        }
        else
        {
            var question = _quizService.GetCurrentQuestion();
            return Ok(question);
        }
    }

    [HttpPost]
    public ActionResult<Question> Post([FromBody] QuizModels answer)
    {
        if (_quizService.GetCurrentQuestion() == null)
        {
            return BadRequest(new { error = "You must fetch the initial question first." });
        }

        var isCorrect = _quizService.CheckAnswer(answer);
        if (isCorrect)
        {
            _quizService.MoveToNextQuestion();
            var question = _quizService.GetRandomQuestion();
            return Ok(question);
        }
        else
        {
            return BadRequest(new { error = "Incorrect Answer!" });
        }
    }


}
