using quiz_game.Forms;

namespace quiz_game.Tables;

/// <summary>
/// Class for question with true or false answers
/// </summary>
public class TrueFalseQuestion : Question
{
    public bool CorrectAnswer { get; set; }
    private RadioButton trueOption;
    private RadioButton falseOption;
    private RadioButton selectedOption;
    private Button confirmButton;

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

        FlowLayoutPanel optionsPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Anchor = AnchorStyles.None,
            Padding = new Padding(0),
            Margin = new Padding(0)
        };

        trueOption = new RadioButton
        {
            Text = "True",
            Font = new Font("Arial", 14),
            AutoSize = true,
            Margin = new Padding(0, 15, 0, 15)
        };

        falseOption = new RadioButton
        {
            Text = "False",
            Font = new Font("Arial", 14),
            AutoSize = true,
            Margin = new Padding(0, 15, 0, 15)
        };

        optionsPanel.Controls.Add(trueOption);
        optionsPanel.Controls.Add(falseOption);

        Panel centerPanel = new Panel
        {
            Dock = DockStyle.Fill
        };
        centerPanel.Controls.Add(optionsPanel);

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
            Enabled = false
        };

        trueOption.CheckedChanged += (sender, e) =>
        {
            if (trueOption.Checked)
            {
                selectedOption = trueOption;
            }
            else if (falseOption.Checked)
            {
                selectedOption = falseOption;
            }
            else
            {
                selectedOption = null;
            }

            if (selectedOption != null)
            {
                confirmButton.Enabled = true;
            }
            else
            {
                confirmButton.Enabled = false;
            }

        };

        falseOption.CheckedChanged += (sender, e) =>
        {
            if (trueOption.Checked)
            {
                selectedOption = trueOption;
            }
            else if (falseOption.Checked)
            {
                selectedOption = falseOption;
            }
            else
            {
                selectedOption = null;
            }

            if (selectedOption != null)
            {
                confirmButton.Enabled = true;
            }
            else
            {
                confirmButton.Enabled = false;
            }

        };

        Panel buttonPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 90
        };
        buttonPanel.Controls.Add(confirmButton);

        confirmButton.Location = new Point((buttonPanel.Width - confirmButton.Width) / 2, (buttonPanel.Height - confirmButton.Height) / 2);

        buttonPanel.Resize += (sender, e) =>
        {
            confirmButton.Location = new Point((buttonPanel.Width - confirmButton.Width) / 2, (buttonPanel.Height - confirmButton.Height) / 2);
        };

        panel.Controls.Add(buttonPanel);

        confirmButton.Click += (sender, e) => ConfirmAnswer(confirmButton, onAnswerSelected);
    }

    /// <summary>
    /// Method to confirm the answer using submit button
    /// </summary>
    /// <param name="confirmButton">The button used for submission or proceeding to the next question</param>
    /// <param name="onAnswerConfirmed">Callback invoked with a boolean indicating whether the answer was correct</param>
    protected override void ConfirmAnswer(Button confirmButton, Action<bool> onAnswerConfirmed)
    {
        if (confirmButton.Text == "Submit")
        {
            if (!CorrectAnswer)
            {
                trueOption.ForeColor = Color.Green; // True je správná odpověď, takže je zelená
                falseOption.ForeColor = Color.Red; // False je špatná odpověď, takže je červená
            }
            else
            {
                trueOption.ForeColor = Color.Red; // True je špatná odpověď, takže je červená
                falseOption.ForeColor = Color.Green; // False je správná odpověď, takže je zelená
            }
            
            bool trueChecked = trueOption.Checked;
            bool falseChecked = falseOption.Checked;
            
            trueOption.CheckedChanged -= (sender, e) => { };
            falseOption.CheckedChanged -= (sender, e) => { };
            
            trueOption.CheckedChanged += (sender, e) =>
            {
                trueOption.Checked = trueChecked;
                falseOption.Checked = falseChecked;
            };
            falseOption.CheckedChanged += (sender, e) =>
            {
                trueOption.Checked = trueChecked;
                falseOption.Checked = falseChecked;
            };

            bool isCorrect;

            if (trueOption.Checked && !CorrectAnswer)
            {
                isCorrect = true;
            }
            else if (falseOption.Checked && CorrectAnswer)
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }

            var form = (MainForm)confirmButton.FindForm();
            form.QuestionTimer.Stop();

            confirmButton.Text = "Next";
            confirmButton.Click -= (sender, e) => ConfirmAnswer(confirmButton, onAnswerConfirmed);
            confirmButton.Click += (sender, e) => onAnswerConfirmed.Invoke(isCorrect);
        }
        else if (confirmButton.Text == "Next")
        {
            MainForm.CurrentQuestionIndex--;
            onAnswerConfirmed.Invoke(false);
        }
    }

    /// <summary>
    /// Method used for when the timer runs out
    /// </summary>
    /// <param name="onNextQuestion">Callback invoked to proceed to the next question</param>
    public override void TimeOut(Action onNextQuestion)
    {
        if (!CorrectAnswer)
        {
            trueOption.ForeColor = Color.Green; // True je správná odpověď, takže je zelená
            falseOption.ForeColor = Color.Red; // False je špatná odpověď, takže je červená
        }
        else
        {
            trueOption.ForeColor = Color.Red; // True je špatná odpověď, takže je červená
            falseOption.ForeColor = Color.Green; // False je správná odpověď, takže je zelená
        }
        
        bool trueChecked = trueOption.Checked;
        bool falseChecked = falseOption.Checked;
        
        trueOption.CheckedChanged -= (sender, e) => { };
        falseOption.CheckedChanged -= (sender, e) => { };
        
        trueOption.CheckedChanged += (sender, e) =>
        {
            trueOption.Checked = trueChecked;
            falseOption.Checked = falseChecked;
            confirmButton.Enabled = true; 
        };
        falseOption.CheckedChanged += (sender, e) =>
        {
            trueOption.Checked = trueChecked;
            falseOption.Checked = falseChecked;
            confirmButton.Enabled = true;
        };

        confirmButton.Text = "Next";
        confirmButton.Enabled = true;
        MainForm.CurrentQuestionIndex++;
        confirmButton.Click -= (sender, e) => onNextQuestion();
        confirmButton.Click += (sender, e) => onNextQuestion();
    }
}