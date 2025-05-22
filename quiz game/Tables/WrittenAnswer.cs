namespace quiz_game.Tables;

public class WrittenAnswer : Question
{
    public string CorrectAnswer { get; set; }
    private TextBox answerTextBox;
    private Button confirmButton;
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
        centerPanel.Resize += (s, e) =>
        {
            answerTextBox.Location = new Point(
                (centerPanel.Width - answerTextBox.Width) / 2,
                (centerPanel.Height - answerTextBox.Height) / 2
            );
        };

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
            Enabled = true
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

    public override void TimeOut(Action onNextQuestion)
    {
        answerTextBox.ReadOnly = true;
        answerTextBox.BackColor = Color.Red;

        confirmButton.Text = "Next";
        confirmButton.Enabled = true;
        MyForm.CurrentQuestionIndex++;

        confirmButton.Click -= (sender, e) => ConfirmAnswer(confirmButton, _ => { });
        confirmButton.Click += (sender, e) => onNextQuestion();
    }

    private void ConfirmAnswer(Button confirmButton, Action<bool> onAnswerConfirmed)
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
            
            var form = (MyForm)confirmButton.FindForm();
            form.QuestionTimer.Stop();
            
            confirmButton.Click -= (sender, e) => ConfirmAnswer(confirmButton, onAnswerConfirmed);
            confirmButton.Click += (sender, e) =>
            {
                onAnswerConfirmed(isCorrect);
            };
        }
        else if (confirmButton.Text == "Next")
        {
            MyForm.CurrentQuestionIndex--;
            onAnswerConfirmed(false);
        }
    }
}