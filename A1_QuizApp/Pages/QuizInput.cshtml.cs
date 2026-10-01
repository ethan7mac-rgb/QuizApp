using System.Text.Json;
using A1_QuizApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace A1_QuizApp.Pages;

public class QuizInpModel : PageModel
{
    public Quiz Quiz { get; private set; } = new();
    [BindProperty]
    public List<int> Answers { get; set; } = new();
    public string ErrMessage { get; private set; }


    public void OnGet()
    {
        LoadQuiz();
    }

    public IActionResult OnPost()
    {
        LoadQuiz();
        if (Answers.Count < Quiz.Questions.Count)
        {
            ErrMessage = "Please answer all questions before submitting.";
            return Page();
        }
        TempData["Answers"] = JsonSerializer.Serialize(Answers);
        return RedirectToPage("/QuizResults");
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
