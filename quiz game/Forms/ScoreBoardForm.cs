namespace quiz_game;

public partial class ScoreBoardForm : Form
{
    private DatabaseServices services;
    private List<Label> scoreLabels;
    private int count = 1;
    
    public ScoreBoardForm()
    {
        InitializeComponent();
        services = new DatabaseServices();
        scoreLabels = new List<Label> { label1, label2, label3, label4, label5 };
        AddScores();
    }

    private void AddScores()
    {
        Dictionary<string, int> scores = services.GetTopScores();
        foreach (var score in scores.OrderByDescending(s => s.Value))
        { 
            scoreLabels[count - 1].Text = $"{count}.       {score.Key}     -     {score.Value}";
            count++;
        }
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        Close();
    }
}