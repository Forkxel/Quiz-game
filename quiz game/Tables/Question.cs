namespace quiz_game.Tables;

/// <summary>
/// Mother class for all questions
/// </summary>
public abstract class Question
{
    public string QuestionText { get; set; }
    public string Difficulty { get; set; }
    public string Category { get; set; }
    
    /// <summary>
    /// Method to display question on form
    /// </summary>
    /// <param name="panel">The panel on which the question UI will be displayed</param>
    /// <param name="onAnswerSelected">Callback invoked with a boolean indicating whether the user's answer was correct</param>
    public abstract void Display(Panel panel, Action<bool> onAnswerSelected);

    /// <summary>
    /// Method used when timer runs out
    /// </summary>
    /// <param name="onNextQuestion">Callback invoked to proceed to the next question</param>
    public abstract void TimeOut(Action onNextQuestion);
    
    /// <summary>
    /// Method to confirm answer with submit button
    /// </summary>
    /// <param name="confirmButton">The button used for submission or proceeding to the next question</param>
    /// <param name="onAnswerConfirmed">Callback invoked with a boolean indicating whether the answer was correct</param>
    protected abstract void ConfirmAnswer(Button confirmButton, Action<bool> onAnswerConfirmed);
}