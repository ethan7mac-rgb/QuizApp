using System.Text.Json;
using A1_QuizApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace A1_QuizApp.Pages;

public class QuizInpModel : PageModel
{
    public Quiz Quiz { get; private set; } = new();
    public List<int> Answers { get; set; } = new();


    public void OnGet()
    {
        LoadQuiz();
    }

    private void LoadQuiz()
    {
        string selQuiz = "MathQuiz.json";
        var path = Path.Combine(Directory.GetCurrentDirectory(), "AppData", selQuiz);
        var json = System.IO.File.ReadAllText(path);
        var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        Quiz = JsonSerializer.Deserialize<Quiz>(json, opts);
    }
}
