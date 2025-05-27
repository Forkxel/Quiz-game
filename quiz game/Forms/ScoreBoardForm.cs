using quiz_game.Database;

namespace quiz_game.Forms;

/// <summary>
/// Class for score board form
/// </summary>
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
        MaximizeBox = false;
    }

    /// <summary>
    /// Method to add top 5 scores to score board
    /// </summary>
    private void AddScores()
    {
        Dictionary<string, int> scores = services.GetTopScores();
        foreach (var score in scores.OrderByDescending(s => s.Value))
        { 
            scoreLabels[count - 1].Text = $"{count}.       {score.Key}     -     {score.Value}";
            count++;
        }
    }

    /// <summary>
    /// Method to close form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void closeButton_Click(object sender, EventArgs e)
    {
        Close();
    }
}