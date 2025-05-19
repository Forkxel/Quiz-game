namespace quiz_game.Tables;

public class Question
{
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public string CorrectAnswer { get; set; }
    public string Option1 { get; set; }
    public string Option2 { get; set; }
    public string Option3 { get; set; }
    public string Difficulty { get; set; }
    public string Category { get; set; }

    public override string ToString()
    {
        return QuestionText + "\n" + CorrectAnswer + "\n" + Option1 + "\n" + Option2 + "\n" + Option3 + "\n";
    }
}