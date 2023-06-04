
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bquiz_API.Models;
using bquiz_API.Controllers;
using System.Collections.Generic;
using System.IO;

namespace bquiz_API.Tests
{
    [TestClass]
    public class QuizServiceTests
    {
        private QuizService _quizService;

        [TestInitialize]
        public void Setup()
        {
            var mockJsonData = @"
            {
                'Quiz': {
                    'Title': 'Test Quiz',
                    'Questions': [
                        {
                            'question': 'Test Question 1',
                            'CorrectAnswer': 1,
                            'Options': ['Option 1', 'Option 2', 'Option 3']
                        },
                        {
                            'question': 'Test Question 2',
                            'CorrectAnswer': 2,
                            'Options': ['Option 1', 'Option 2', 'Option 3']
                        }
                    ]
                }
            }";

            File.WriteAllText("questions.json", mockJsonData);

            _quizService = new QuizService();
        }

        [TestMethod]
        public void TestGetQuizTitle()
        {
            var title = _quizService.GetQuizTitle();
            Assert.AreEqual("Test Quiz", title);
        }

        [TestMethod]
        public void TestGetRandomQuestion()
        {
            var question = _quizService.GetRandomQuestion();
            Assert.IsNotNull(question);
            Assert.IsTrue(question.Id == 1 || question.Id == 2);
        }

        [TestMethod]
        public void TestGetCurrentQuestion()
        {
            var question = _quizService.GetCurrentQuestion();
            Assert.IsNull(question);
            _quizService.MoveToNextQuestion();
            question = _quizService.GetCurrentQuestion();
            Assert.IsNotNull(question);
        }

        [TestMethod]
        public void TestCheckAnswer()
        {
            var answer = new QuizModels(1, 1);
            var result = _quizService.CheckAnswer(answer);
            Assert.IsTrue(result);
        }


        [TestCleanup]
        public void Cleanup()
        {
            File.Delete("questions.json");
        }
    }
}
