using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using quiz_game.Tables;
using Timer = System.Windows.Forms.Timer;

namespace quiz_game;

public partial class MyForm : Form
{
    private Panel quizPanel;
    private List<Question> currentQuestions = new();
    public static int CurrentQuestionIndex { get; set; }
    private const int MaxQuestions = 5;
    private int score;
    private Label scoreLabel;
    private Label timerLabel;
    private RadioButton selectedOption = null;
    public Timer QuestionTimer { get; set; }
    private int timeLeft;
    private Panel infoPanel;
    private DatabaseServices services;
    private int currentTime = 0;
    
    public MyForm()
    {
        InitializeComponent();
        InitializeLayout();
        services = new DatabaseServices();
    }
    
    private void InitializeLayout()
    {
        infoPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 50,
            Padding = new Padding(10),
            BackColor = Color.LightGray,
            Visible = false
        };
        quizPanel = new Panel
        {
            Visible = false,
            Dock = DockStyle.Fill
        };
        
        scoreLabel = new Label
        {
            Text = "Score: 0",
            Font = new Font("Arial", 12, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft,
            Dock = DockStyle.Left,
            Width = 150,
            Padding = new Padding(5),
            Visible = false
        };
        infoPanel.Controls.Add(scoreLabel);
        
        timerLabel = new Label
        {
            Text = "Time left: 30",
            Font = new Font("Arial", 12, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleRight,
            Dock = DockStyle.Right,
            Width = 150,
            Padding = new Padding(5),
            Visible = false
        };
        infoPanel.Controls.Add(timerLabel);

        Controls.Add(quizPanel);
        
        Controls.Add(infoPanel);    
    }

    private void startButton_Click(object sender, EventArgs e)
    {
        object selectedCategory = ((dynamic)categoriesCombo.SelectedItem)?.Id;
        string selectedDifficulty = ((dynamic)difficultyCombo.SelectedItem)?.Id?.ToString();
        currentQuestions.Clear();
        CurrentQuestionIndex = 0;
        
        var singleQuestions = services.GetSingleQuestions(selectedCategory, selectedDifficulty);
        var writtenAnswer = services.GetWrittenQuestions(selectedCategory, selectedDifficulty);
        
        List<Question> questions = singleQuestions.Concat(writtenAnswer).ToList();
        
        var random = new Random();
        currentQuestions = questions
            .DistinctBy(q => q.QuestionText) 
            .OrderBy(q => random.Next()) 
            .Take(MaxQuestions)        
            .ToList();
        
        DisplayNextQuestion();

        panel.Visible = false;
        scoreLabel.Visible = true;
        timerLabel.Visible = true;
        infoPanel.Visible = true;
    }

    private void DisplayNextQuestion()
    {
        if (CurrentQuestionIndex < currentQuestions.Count)
        {
            timeLeft = 10;
            timerLabel.Text = $"Time: {timeLeft}";
            QuestionTimer.Start();
            
            var currentQuestion = currentQuestions[CurrentQuestionIndex];
            currentQuestion.Display(quizPanel, isCorrect =>
            {
                if (isCorrect)
                {   
                    score += currentTime / 2;
                    scoreLabel.Text = $"Score: {score}";
                }
                CurrentQuestionIndex++;
                DisplayNextQuestion();
            });
        }
        else
        {
            QuestionTimer.Stop();
            DisplayFinalScorePanel();
            quizPanel.Visible = false;
        }
    }
    
    private void DisplayFinalScorePanel()
    {
        Panel finalScorePanel = new Panel
        {
            Dock = DockStyle.Fill
        };

        Label finalScoreLabel = new Label
        {
            Text = $"Your final score: {score}",
            Font = new Font("Arial", 16, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };

        finalScorePanel.Controls.Add(finalScoreLabel);
        Controls.Clear();
        Controls.Add(finalScorePanel); 
    }
    
    private void QuestionTimer_Tick(object sender, EventArgs e)
    {
        timeLeft--;
        timerLabel.Text = $"Time: {timeLeft}";
        currentTime = timeLeft;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            timerLabel.Text = "Time: 0";
            QuestionTimer.Stop();

            if (CurrentQuestionIndex < currentQuestions.Count)
            {
                var question = currentQuestions[CurrentQuestionIndex];
                question.TimeOut(DisplayNextQuestion);
            }
        }
    }

    private void Form_Load(object sender, EventArgs e)
    {
        QuestionTimer = new Timer
        {
            Interval = 1000
        };
        QuestionTimer.Tick += QuestionTimer_Tick;
        
        var categories = services.GetCategories();
        var difficulties = services.GetDifficulties();

        categoriesCombo.Items.AddRange(categories.ToArray());
        difficultyCombo.Items.AddRange(difficulties.ToArray());

        categoriesCombo.DisplayMember = "Name";
        difficultyCombo.DisplayMember = "Name";
        categoriesCombo.Items.Add(new { Id = (object)null, Name = "Mixed" });
    }
}   