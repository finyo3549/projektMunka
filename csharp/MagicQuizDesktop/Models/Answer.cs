using Newtonsoft.Json;
using System.ComponentModel;

namespace MagicQuizDesktop.Models;

/// <summary>
///     Represents the Answer class that implements the INotifyPropertyChanged interface.
///     Properties include Id, QuestionId, AnswerText, IsCorrect, and IsActive.
///     There are two constructors available: a default one which initializes properties with default values, and a
///     parameterized one which allows all properties to be set.
///     Provides a FromJson method to create a Rank object from a JSON string.
/// </summary>
public class Answer : INotifyPropertyChanged
{
    private string _answerText;
    private int _id;
    private bool _isActive;
    private bool _isCorrect;
    private int _questionId;


    /// <summary>
    ///     Default constructor. Initializes an Answer with default values.
    /// </summary>
    public Answer()
    {
        Id = 0;
        QuestionId = 0;
        AnswerText = "Default Answer";
        IsCorrect = false;
        IsActive = true;
    }

    /// <summary>
    ///     Parameterized constructor. Allows setting all properties at the time of object creation.
    /// </summary>
    /// <param name="id">The unique identifier for the answer.</param>
    /// <param name="questionId">The ID of the question this answer belongs to.</param>
    /// <param name="answerText">The text of the answer.</param>
    /// <param name="isCorrect">Indicates whether the answer is correct or not.</param>
    /// <param name="isActive">Indicates whether the answer is active or not.</param>
    public Answer(int id, int questionId, string answerText, bool isCorrect, bool isActive)
    {
        Id = id;
        QuestionId = questionId;
        AnswerText = answerText;
        IsCorrect = isCorrect;
        IsActive = isActive;
    }

    /// <summary>
    ///     The property 'Id' is decorated with JsonProperty attribute for serialization purposes.
    ///     It also implements OnPropertyChanged to notify any listeners when
    ///     the property changes its value.
    /// </summary>
    [JsonProperty("id")]
    public int Id
    {
        get => _id;
        set
        {
            if (_id == value) return;
            _id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    /// <summary>
    ///     Gets or Sets the Question ID. This ID corresponds to the unique identifier for a question.
    ///     Also, this triggers a property change notification when value is updated.
    /// </summary>
    [JsonProperty("question_id")]
    public int QuestionId
    {
        get => _questionId;
        set
        {
            if (_questionId == value) return;
            _questionId = value;
            OnPropertyChanged(nameof(QuestionId));
        }
    }

    /// <summary>
    ///     Gets or sets the answer text. Triggers PropertyChanged event when the value is modified.
    /// </summary>
    [JsonProperty("answer_text")]
    public string AnswerText
    {
        get => _answerText;
        set
        {
            if (_answerText == value) return;
            _answerText = value;
            OnPropertyChanged(nameof(AnswerText));
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether it's correct.
    ///     Raises OnPropertyChanged event whenever the value changes.
    /// </summary>
    [JsonProperty("is_correct")]
    public bool IsCorrect
    {
        get => _isCorrect;
        set
        {
            if (_isCorrect == value) return;
            _isCorrect = value;
            OnPropertyChanged(nameof(IsCorrect));
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether the object is active.
    ///     When set, it triggers the OnPropertyChanged event.
    /// </summary>
    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (_isActive == value) return;
            _isActive = value;
            OnPropertyChanged(nameof(IsActive));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    ///     Invokes the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///     Creates an Answer object from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string to convert into an Answer object.</param>
    /// <returns>An Answer object derived from the JSON string.</returns>
    public static Answer FromJson(string json)
    {
        return JsonConvert.DeserializeObject<Answer>(json, Converter.Settings);
    }
}