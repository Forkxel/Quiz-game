using quiz_game.Forms;

namespace quiz_game.Tables;

public class TrueFalseQuestion : Question
{
    public bool CorrectAnswer { get; set; }
    private RadioButton trueOption;
    private RadioButton falseOption;
    private Panel quizPanel;
    private RadioButton selectedOption;

    public override void Display(Panel panel, Action<bool> onAnswerSelected)
    {
        quizPanel = panel;
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
            Margin = new Padding(0),
        };

        trueOption = new RadioButton
        {
            Text = "True",
            Font = new Font("Arial", 14),
            AutoSize = true,
            Margin = new Padding(0, 15, 0, 15),
        };

        falseOption = new RadioButton
        {
            Text = "False",
            Font = new Font("Arial", 14),
            AutoSize = true,
            Margin = new Padding(0, 15, 0, 15),
        };

        optionsPanel.Controls.Add(trueOption);
        optionsPanel.Controls.Add(falseOption);

        Panel centerPanel = new Panel
        {
            Dock = DockStyle.Fill,
        };
        centerPanel.Controls.Add(optionsPanel);

        panel.Controls.Add(centerPanel);

        Button confirmButton = new Button
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
            confirmButton.Enabled = trueOption.Checked || falseOption.Checked;
        };

        falseOption.CheckedChanged += (sender, e) =>
        {
            if (falseOption.Checked)
            {
                selectedOption = falseOption;
            }
            confirmButton.Enabled = trueOption.Checked || falseOption.Checked;
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

    protected override void ConfirmAnswer(Button confirmButton, Action<bool> onAnswerConfirmed)
    {
        if (confirmButton.Text == "Submit")
        {
            foreach (var option in new[] { trueOption, falseOption })
                    {
                        option.CheckedChanged -= (sender, e) => { }; // Odstraníme původní event
                        option.CheckedChanged += (sender, e) =>
                        {
                            if (selectedOption != null && option != selectedOption)
                            {
                                option.Checked = false;
                                selectedOption.Checked = true; // Vynutíme návrat na původní stav
                            }
                        };
                    }
            /*
                    if (trueOption.Checked)
                    {
                        //trueOption.ForeColor = CorrectAnswer ? Color.Green : Color.Red;
                        //falseOption.ForeColor = CorrectAnswer ? Color.Gray : Color.Green;
                    }
                    else if (falseOption.Checked)
                    {
                        falseOption.ForeColor = !CorrectAnswer ? Color.Green : Color.Red;
                        trueOption.ForeColor = !CorrectAnswer ? Color.Gray : Color.Green;
                    }
            */
                    trueOption.ForeColor = Color.Green;
                    falseOption.ForeColor = Color.Red;
                    
                    bool isCorrect;
                    
                    if ((trueOption.Checked && CorrectAnswer) || (falseOption.Checked && !CorrectAnswer))
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
                    confirmButton.Click += (sender, e) =>
                    {
                        onAnswerConfirmed.Invoke(isCorrect);
                    };
        } 
        else if (confirmButton.Text == "Next")
        {
            MainForm.CurrentQuestionIndex--;
            onAnswerConfirmed.Invoke(false);
        }
    }

    public override void TimeOut(Action onNextQuestion)
    {
        foreach (var option in new[] { trueOption, falseOption })
        {
            option.CheckedChanged -= (sender, e) => { }; // Odstraníme původní event
            option.CheckedChanged += (sender, e) =>
            {
                if (selectedOption != null && option != selectedOption)
                {
                    option.Checked = false;
                    selectedOption.Checked = true; // Vynutíme návrat na původní stav
                }
            };
        }
        
        trueOption.ForeColor = CorrectAnswer ? Color.Green : Color.Red;
        falseOption.ForeColor = !CorrectAnswer ? Color.Green : Color.Red;
        
        var confirmButton = quizPanel.Controls
            .OfType<Button>()
            .SelectMany(p => p.Controls.OfType<Button>())
            .FirstOrDefault();
        
        confirmButton.Text = "Next";
        confirmButton.Enabled = true;
        MainForm.CurrentQuestionIndex++;
        confirmButton.Click -= (sender, e) => onNextQuestion();
        confirmButton.Click += (sender, e) => onNextQuestion();
    }
}