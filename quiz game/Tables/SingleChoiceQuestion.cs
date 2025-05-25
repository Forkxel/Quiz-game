using quiz_game.Forms;
using Timer = System.Windows.Forms.Timer;

namespace quiz_game.Tables;

public class SingleChoiceQuestion : Question  
{
    public string Option1 { get; set; }
    public string Option2 { get; set; }
    public string Option3 { get; set; }
    public string CorrectAnswer { get; set; }
    private RadioButton selectedAnswer;
    private Panel quizPanel;
    private List<RadioButton> answers;

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
        
        FlowLayoutPanel radioPanel = new FlowLayoutPanel()
        {
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Anchor = AnchorStyles.None,
            Padding = new Padding(0),
            Margin = new Padding(0),
        };
        
        Panel centerPanel = new Panel()
        {
            Dock = DockStyle.Fill,
        };
        centerPanel.Controls.Add(radioPanel);

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
        
        Font radioFont = new Font("Arial", 16, FontStyle.Regular);
        List<string> options = new List<string> { Option1, Option2, Option3 };
        
        var rnd = new Random();
        options = options.OrderBy(x => rnd.Next()).ToList();
        
        answers = new List<RadioButton>();

        foreach (var opt in options)
        {
            RadioButton rb = new RadioButton
            {
                Text = opt,
                AutoSize = true,
                Margin = new Padding(0, 15, 0, 15),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = radioFont
            };
            
            rb.CheckedChanged += (sender, e) =>
            {
                if (confirmButton.Text != "Next")
                {
                    confirmButton.Enabled = answers.Any(r => r.Checked);
                }
            };
            answers.Add(rb);
            radioPanel.Controls.Add(rb);
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
            string selectedText = "";
            foreach (RadioButton rb in answers)
            {
                if (rb.Checked)
                {
                    selectedText = rb.Text;
                    selectedAnswer = rb;
                }
            }

            foreach (var rb in answers)
            {
                if (rb.Text == CorrectAnswer)
                {
                    rb.ForeColor = Color.Green;
                }
                else if (rb.Text != CorrectAnswer)
                {
                    rb.ForeColor = Color.Red;
                }
            }

            foreach (var rb in answers)
            {
                rb.Click -= (sender, e) => { };
                rb.CheckedChanged -= (sender, e) => { };
                
                rb.CheckedChanged += (sender, e) =>
                {
                    var radio = (RadioButton)sender;
                    if (selectedAnswer != null)
                    {
                        if (radio != selectedAnswer && radio.Checked)
                        {
                            radio.Checked = false;
                            selectedAnswer.Checked = true;
                        }
                    }
                    else
                    {
                        if (radio.Checked)
                        {
                            radio.Checked = false;
                        }
                    }
                };
            }
            
            confirmButton.Text = "Next";
            confirmButton.Enabled = true;

            bool isCorrect;

            if (selectedText == CorrectAnswer)
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
        else if(confirmButton.Text == "Next")
        {
            MainForm.CurrentQuestionIndex--;
            onAnswerConfirmed.Invoke(false);
        }
    }

    public override void TimeOut(Action onNextQuestion)
    {
        foreach (var rb in answers)
        {
            if (rb.Text == CorrectAnswer)
            {
                rb.ForeColor = Color.Green;
            }
            else
            {
                rb.ForeColor = Color.Red;
            }

            rb.Click -= (sender, e) => { };
            rb.CheckedChanged -= (sender, e) => { };

            rb.CheckedChanged += (sender, e) =>
            {
                var radio = (RadioButton)sender;
                if (selectedAnswer != null)
                {
                    if (radio != selectedAnswer && radio.Checked)
                    {
                        radio.Checked = false;
                        selectedAnswer.Checked = true;
                    }
                }
                else
                {
                    if (radio.Checked)
                    {
                        radio.Checked = false;
                    }
                }
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