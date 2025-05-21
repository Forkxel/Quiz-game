namespace quiz_game.Tables;

public class SingleChoiceQuestion : Question  
{
    public string Option1 { get; set; }
    public string Option2 { get; set; }
    public string Option3 { get; set; }
    public string CorrectAnswer { get; set; }
    private RadioButton selectedAnswer;

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
            Margin = new Padding(0, 20, 0, 10),
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
        
        Font radioFont = new Font("Arial", 16, FontStyle.Regular);
        List<string> options = new List<string> { Option1, Option2, Option3 };
        
        var rnd = new Random();
        options = options.OrderBy(x => rnd.Next()).ToList();
        
        List<RadioButton> radioButtons = new List<RadioButton>();

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
            radioButtons.Add(rb);
            radioPanel.Controls.Add(rb);
        }
        
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
        };
        
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

        confirmButton.Click += (sender, e) => ConfirmAnswer(radioButtons, confirmButton, onAnswerSelected);

        panel.Visible = true;
    }
    
    private void ConfirmAnswer(List<RadioButton> radioButtons, Button confirmButton, Action<bool> onAnswerConfirmed)
    {
        if (confirmButton.Text == "Submit")
        {
            string selectedText = "";
            foreach (RadioButton rb in radioButtons)
            {
                if (rb.Checked)
                {
                    selectedText = rb.Text;
                    selectedAnswer = rb;
                }
            }

            foreach (var rb in radioButtons)
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
            
            confirmButton.Text = "Next";

            foreach (var rb in radioButtons)
            {
                rb.Click -= DisableClick;
                rb.Click += DisableClick;
            }
           
            bool isCorrect = selectedText == CorrectAnswer;
            
            confirmButton.Click -= (sender, e) => ConfirmAnswer(radioButtons, confirmButton, onAnswerConfirmed);
            confirmButton.Click += (sender, e) =>
            {
                onAnswerConfirmed?.Invoke(isCorrect);
            };
        }
        else
        {
            Form.CurrentQuestionIndex--;
            onAnswerConfirmed?.Invoke(false);
        }
    }
    
    private void DisableClick(object sender, EventArgs e)
    {
        ((RadioButton)sender).Checked = false;
        selectedAnswer.Checked = true;
    }
}