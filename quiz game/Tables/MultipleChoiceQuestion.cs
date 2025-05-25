using quiz_game.Forms;

namespace quiz_game.Tables;

public class MultipleChoiceQuestion : Question
{
    public string Option1 { get; set; }
    public string Option2 { get; set; }
    public string Option3 { get; set; }
    public List<string> CorrectAnswers { get; set; }
    private Panel quizPanel;
    private List<CheckBox> selectedAnswers;
    
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

        FlowLayoutPanel checkBoxPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Anchor = AnchorStyles.None,
            Padding = new Padding(0),
            Margin = new Padding(0)
        };

        Panel centerPanel = new Panel
        {
            Dock = DockStyle.Fill
        };
        centerPanel.Controls.Add(checkBoxPanel);

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
        
        List<string> options = new List<string> { Option1, Option2, Option3 }
            .Where(option => !string.IsNullOrEmpty(option))
            .ToList();
        
        var rnd = new Random();
        options = options.OrderBy(x => rnd.Next()).ToList();

        selectedAnswers = new List<CheckBox>();

        foreach (var option in options)
        {
            CheckBox checkBox = new CheckBox
            {
                Text = option,
                AutoSize = true,
                Font = new Font("Arial", 16, FontStyle.Regular),
                Margin = new Padding(0, 15, 0, 15),
                TextAlign = ContentAlignment.MiddleLeft
            };

            checkBox.CheckedChanged += (sender, e) =>
            {
                if (confirmButton.Text != "Next")
                {
                   confirmButton.Enabled = selectedAnswers.Any(cb => cb.Checked); 
                }
            };

            selectedAnswers.Add(checkBox);
            checkBoxPanel.Controls.Add(checkBox);
        }

        Panel buttonPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 90
        };
        buttonPanel.Controls.Add(confirmButton);
        confirmButton.Location = new Point((buttonPanel.Width - confirmButton.Width) / 2, (buttonPanel.Height - confirmButton.Height) / 2);

        panel.Controls.Add(buttonPanel);

        buttonPanel.Resize += (sender, e) =>
        {
            confirmButton.Location = new Point((buttonPanel.Width - confirmButton.Width) / 2, (buttonPanel.Height - confirmButton.Height) / 2);
        };

        confirmButton.Click += (sender, e) => ConfirmAnswer(confirmButton, onAnswerSelected);

        panel.Visible = true;
    }
    
    protected override void ConfirmAnswer(Button confirmButton, Action<bool> onAnswerConfirmed)
    {
        if (confirmButton.Text == "Submit")
        {
            var selectedTexts = selectedAnswers
                .Where(cb => cb.Checked)
                .Select(cb => cb.Text)
                .ToList();

            foreach (var checkBox in selectedAnswers)
            {
                if (CorrectAnswers.Contains(checkBox.Text))
                {
                    checkBox.ForeColor = Color.Green;
                }
                else
                {
                    checkBox.ForeColor = Color.Red;
                }
            }

            confirmButton.Text = "Next";
            confirmButton.Enabled = true;

            foreach (var checkBox in selectedAnswers)
            {
                checkBox.Click -= (sender, e) =>
                {
                    ((CheckBox)sender).Checked = false;
                    if (CorrectAnswers.Contains(checkBox.Text))
                    {
                        checkBox.Checked = true;
                    }
                };
                checkBox.Click += (sender, e) =>
                {
                    ((CheckBox)sender).Checked = false;
                    if (CorrectAnswers.Contains(checkBox.Text))
                    {
                        checkBox.Checked = true;
                    }
                };
            }

            bool isCorrect;
            if (selectedTexts.Count == CorrectAnswers.Count && selectedTexts.All(CorrectAnswers.Contains))
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }

            var form = (MainForm)confirmButton.FindForm();
            form.QuestionTimer.Stop();

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
        foreach (var checkBox in selectedAnswers)
        {
            if (CorrectAnswers.Contains(checkBox.Text))
            {
                checkBox.ForeColor = Color.Green;
            }
            else
            {
                checkBox.ForeColor = Color.Red;
            }
            checkBox.Click -= (sender, e) =>
            {
                ((CheckBox)sender).Checked = false;
            };
            checkBox.Click += (sender, e) =>
            {
                ((CheckBox)sender).Checked = false;
            };
        }

        var confirmButton = quizPanel.Controls
            .OfType<Panel>()
            .SelectMany(p => p.Controls.OfType<Button>())
            .FirstOrDefault();

        confirmButton.Text = "Next";
        confirmButton.Enabled = true;
        MainForm.CurrentQuestionIndex++;
        confirmButton.Click -= (sender, e) => onNextQuestion();
        confirmButton.Click += (sender, e) => onNextQuestion();
    }
}