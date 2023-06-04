using bquiz_API.Models;

public interface IQuizService
{
    bool CheckAnswer(QuizModels answer);
    PublicQuestion GetCurrentQuestion();
    int GetQuestionsCount();
    string GetQuizTitle();
    PublicQuestion GetRandomQuestion();
    void MoveToNextQuestion();
}