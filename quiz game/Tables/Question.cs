namespace quiz_game.Tables;

public abstract class Question
{
    public string QuestionText { get; set; }
    public string Difficulty { get; set; }
    public string Category { get; set; }
    
    public abstract void Display(Panel panel, Action<bool> onAnswerSelected);

    public abstract void TimeOut(Action onNextQuestion);
}