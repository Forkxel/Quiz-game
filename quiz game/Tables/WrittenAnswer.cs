using quiz_game.Forms;

namespace quiz_game.Tables;

/// <summary>
/// Class for questions that have written answers
/// </summary>
public class WrittenAnswer : Question
{
    public string CorrectAnswer { get; set; }
    private TextBox answerTextBox;
    private Button confirmButton;
    private Label correctAnswerLabel;
    
    /// <summary>
    /// Method to display question on form
    /// </summary>
    /// <param name="panel">The panel on which the question UI will be displayed</param>
    /// <param name="onAnswerSelected">Callback invoked with a boolean indicating whether the user's answer was correct</param>
    public override void Display(Panel panel, Action<bool> onAnswerSelected)
    {
        panel.Controls.Clear();

        Label questionLabel = new Label
        {
            Text = QuestionText,
            Font = new Font("Arial", 14, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top,
            Height = 100,
            Margin = new Padding(0, 25, 5, 10),
            AutoSize = false
        };
        panel.Controls.Add(questionLabel);
        
        Panel centerPanel = new Panel()
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10)
        };

        answerTextBox = new TextBox
        {
            Font = new Font("Arial", 16, FontStyle.Regular),
            Size = new Size(300, 40),
            TextAlign = HorizontalAlignment.Center,
            Anchor = AnchorStyles.None
        };
        
        centerPanel.Controls.Add(answerTextBox);
        answerTextBox.Location = new Point(
            (centerPanel.Width - answerTextBox.Width) / 2,
            (centerPanel.Height - answerTextBox.Height) / 2
        );

        panel.Controls.Add(centerPanel);

        confirmButton = new Button
        {
            Text = "Submit",
            AutoSize = true,
            Anchor = AnchorStyles.Bottom,
            Padding = new Padding(10),
            Margin = new Padding(0, 10, 0, 20),
            Width = 160,
            Height = 60,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 16, FontStyle.Bold),
            Enabled = true,
            Cursor = Cursors.Hand
        };
        
        correctAnswerLabel = new Label
        {
            Text = $"Correct Answer: {CorrectAnswer}",
            Font = new Font("Arial", 12, FontStyle.Italic),
            ForeColor = Color.Blue,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Bottom,
            Height = 50,
            Visible = false
        };

        Panel buttonPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 90
        };
        buttonPanel.Controls.Add(confirmButton);
        
        buttonPanel.Resize += (s, e) =>
        {
            confirmButton.Location = new Point((buttonPanel.Width - confirmButton.Width) / 2, (buttonPanel.Height - confirmButton.Height) / 2);
        };
        panel.Controls.Add(buttonPanel);

        confirmButton.Click += (sender, e) => ConfirmAnswer(confirmButton, onAnswerSelected);

        panel.Visible = true;
    }

    /// <summary>
    /// Method used when the timer runs out
    /// </summary>
    /// <param name="onNextQuestion">Callback invoked to proceed to the next question</param>
    public override void TimeOut(Action onNextQuestion)
    {
        answerTextBox.ReadOnly = true;
        answerTextBox.BackColor = Color.Red;
        
        Panel parentPanel = (Panel)answerTextBox.Parent;
        parentPanel.Controls.Add(correctAnswerLabel);
        correctAnswerLabel.Visible = true;

        confirmButton.Text = "Next";
        confirmButton.Enabled = true;
        MainForm.CurrentQuestionIndex++;
        
        confirmButton.Click += (sender, e) => onNextQuestion();
    }

    /// <summary>
    /// Method used for confirming the answer using submit button
    /// </summary>
    /// <param name="confirmButton">The button used for submission or proceeding to the next question</param>
    /// <param name="onAnswerConfirmed">Callback invoked with a boolean indicating whether the answer was correct</param>
    protected override void ConfirmAnswer(Button confirmButton, Action<bool> onAnswerConfirmed)
    {
        if (confirmButton.Text == "Submit")
        {
            string userAnswer = answerTextBox.Text.Trim();
            bool isCorrect;
            
            if (string.Equals(userAnswer, CorrectAnswer, StringComparison.OrdinalIgnoreCase))
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
                Panel parentPanel = (Panel)answerTextBox.Parent;
                parentPanel.Controls.Add(correctAnswerLabel);
                correctAnswerLabel.Visible = true;
            }

            answerTextBox.ReadOnly = true;
            if (isCorrect)
            {
                answerTextBox.BackColor = Color.LimeGreen;
            }
            else
            {
                answerTextBox.BackColor = Color.Red;
            }

            confirmButton.Text = "Next";
            confirmButton.Enabled = true;
            
            var form = (MainForm)confirmButton.FindForm();
            form.QuestionTimer.Stop();
            
            confirmButton.Click += (sender, e) => onAnswerConfirmed(isCorrect);
        }
        else if (confirmButton.Text == "Next")
        {
            MainForm.CurrentQuestionIndex--;
            onAnswerConfirmed.Invoke(false);
        }
    }
}