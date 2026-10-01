using System.Text.Json;
using A1_QuizApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace A1_QuizApp.Pages;

public class QuizResModel : PageModel
{
    public Quiz Quiz { get; private set; } = new();
    public List<int> Answers { get; private set; } = new();
    public List<bool> CorrectAnswers { get; private set; } = new();
    public int Score => CorrectAnswers.Count(isCorrect => isCorrect);
    public string? ErrMessage { get; private set; }

    public void OnGet()
    {
        LoadQuiz();
        if (ErrMessage is not null)
        {
            return;
        }

        var tempAns = TempData["Answers"] as string;
        if (tempAns is null)
        {
            ErrMessage = "Quiz answers could not be found.";
            return;
        }
        Answers = JsonSerializer.Deserialize<List<int>>(tempAns);
        CorrectAnswers = new List<bool>();
        for (int i = 0; i < Quiz.Questions.Count; i++){
            bool isCorrect = i < Answers.Count &&
            Answers[i] == Quiz.Questions[i].AnswerIndex;
            CorrectAnswers.Add(isCorrect);
        }
    }

    private void LoadQuiz()
    {
        try
        {
            string selQuiz = "MathQuiz.json";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "AppData", selQuiz);
            var json = System.IO.File.ReadAllText(path);
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Quiz = JsonSerializer.Deserialize<Quiz>(json, opts);
        }
        catch (Exception)
        {
            ErrMessage = "Quiz Failed to Load";
        }

    }
}
