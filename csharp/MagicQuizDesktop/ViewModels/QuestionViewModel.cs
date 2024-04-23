using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MagicQuizDesktop.Commands;
using MagicQuizDesktop.Models;
using MagicQuizDesktop.Services;
using MagicQuizDesktop.View.Windows;

namespace MagicQuizDesktop.ViewModels;

/// <summary>
///     Represents a ViewModel for managing questions.
/// </summary>
public class QuestionViewModel : ViewModelBase
{
    /// <summary>
    ///     Represents a readonly instance of a question repository.
    /// </summary>
    public readonly IQuestionRepository _questionRepository;

    /// <summary>
    ///     Represents a readonly instance of a topic repository.
    /// </summary>
    public readonly ITopicRepository _topicRepository;

    private Answer? _answer1;
    private Answer? _answer2;
    private Answer? _answer3;
    private Answer? _answer4;
    private List<Answer>? _answers;

    private int? _correctAnswerNumber;

    //For the user
    private User? _currentUser;
    private ObservableCollection<Question>? _filteredQuestions;

    //For messages

    private Message _message;

    //Lists
    private List<Question>? _questions;
    private string? _questionText;

    private string? _searchText;

    //For the questions
    private Question? _selectedQuestion;
    private string? _topicName;
    private List<Topic>? _topics;


    /// <summary>
    ///     Initializes a new instance of the <see cref="QuestionViewModel" /> class using the specified repositories,
    ///     the current user session, a set of four answer numbers, and initiates the command with an asynchronous method.
    /// </summary>
    /// <param name="questionRepository">An instance of an object that implements the IQuestionRepository interface.</param>
    /// <param name="topicRepository">An instance of an object that implements the ITopicRepository interface.</param>
    public QuestionViewModel(IQuestionRepository questionRepository, ITopicRepository topicRepository)
    {
        CurrentUser = SessionManager.Instance.CurrentUser;
        _topicRepository = topicRepository;
        _questionRepository = questionRepository;
        AnswerNumbers = new List<int> { 1, 2, 3, 4 };
        InitializeCommands();
        _ = InitializeAsync();
    }


    //Public fields

    /// <summary>
    ///     Gets or sets the current user. If it's being set and the provided value is different from the existing one, it
    ///     raises an event to notify about the change.
    /// </summary>
    public User? CurrentUser
    {
        get => _currentUser;
        set
        {
            if (_currentUser == value) return;
            _currentUser = value;
            OnPropertyChanged(nameof(CurrentUser));
        }
    }

    //For the questions
    /// <summary>
    ///     Gets or sets the selected question. When a new question is set, a property changed event is raised.
    /// </summary>
    public Question? SelectedQuestion
    {
        get => _selectedQuestion;
        set
        {
            if (_selectedQuestion == value) return;
            _selectedQuestion = value;
            OnPropertyChanged(nameof(SelectedQuestion));
        }
    }

    /// <summary>
    ///     Gets or sets the search text. If the provided text is null or empty,
    ///     the property is reset to its default value.
    /// </summary>
    public string? SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged(nameof(SearchText));
            // Call the method if the text is empty
            if (string.IsNullOrEmpty(SearchText)) ResetToDefault();
        }
    }

    /// <summary>
    ///     Gets or sets the question text.
    /// </summary>
    public string? QuestionText
    {
        get => _questionText;
        set
        {
            _questionText = value;
            OnPropertyChanged(nameof(QuestionText));
        }
    }

    /// <summary>
    ///     Gets or sets the number of the correct answer.
    /// </summary>
    public int? CorrectAnswerNumber
    {
        get => _correctAnswerNumber;
        set
        {
            _correctAnswerNumber = value;
            OnPropertyChanged(nameof(CorrectAnswerNumber));
        }
    }

    /// <summary>
    ///     Represents a list of answer numbers.
    /// </summary>
    public List<int> AnswerNumbers { get; }

    /// <summary>
    ///     Gets or sets the first Answer property. Triggers PropertyChanged event if the value changes.
    /// </summary>
    public Answer? Answer1
    {
        get => _answer1;
        set
        {
            if (_answer1 != value)
            {
                _answer1 = value;
                OnPropertyChanged(nameof(Answer1));
            }
        }
    }

    /// <summary>
    ///     Gets or sets the secondary answer.
    ///     Notifies about a property change whenever the value changes.
    /// </summary>
    public Answer? Answer2
    {
        get => _answer2;
        set
        {
            if (_answer2 != value)
            {
                _answer2 = value;
                OnPropertyChanged(nameof(Answer2));
            }
        }
    }

    /// <summary>
    ///     Gets or sets the third answer. Raises a property changed event if the value of Answer3 changes.
    /// </summary>
    public Answer? Answer3
    {
        get => _answer3;
        set
        {
            if (_answer3 != value)
            {
                _answer3 = value;
                OnPropertyChanged(nameof(Answer3));
            }
        }
    }

    /// <summary>
    ///     Gets or sets the fourth answer. Triggers a property changed event when the value is updated.
    /// </summary>
    public Answer? Answer4
    {
        get => _answer4;
        set
        {
            if (_answer4 != value)
            {
                _answer4 = value;
                OnPropertyChanged(nameof(Answer4));
            }
        }
    }

    /// <summary>
    ///     Gets or sets a nullable TopicName. Raises PropertyChanged event when the value is changed.
    /// </summary>
    public string? TopicName
    {
        get => _topicName;
        set
        {
            if (_topicName != value)
            {
                _topicName = value;
                OnPropertyChanged(nameof(TopicName));
            }
        }
    }

    //Lists
    /// <summary>
    ///     Gets or sets a list of questions.
    /// </summary>
    public List<Question>? Questions
    {
        get => _questions;
        set
        {
            _questions = value;
            OnPropertyChanged(nameof(Questions));
        }
    }

    /// <summary>
    ///     Gets or sets the filtered questions. Upon setting, triggers a property change notification.
    /// </summary>
    public ObservableCollection<Question>? FilteredQuestions
    {
        get => _filteredQuestions;
        set
        {
            _filteredQuestions = value;
            OnPropertyChanged(nameof(FilteredQuestions));
        }
    }

    /// <summary>
    ///     Gets or sets the list of topics. Notifies about a property change whenever the topics are set.
    /// </summary>
    public List<Topic>? Topics
    {
        get => _topics;
        set
        {
            _topics = value;
            OnPropertyChanged(nameof(Topics));
        }
    }

    /// <summary>
    ///     Gets or sets the list of 'Answer' objects. Notifies property change when the value is set.
    /// </summary>
    public List<Answer>? Answers
    {
        get => _answers;
        set
        {
            _answers = value;
            OnPropertyChanged(nameof(Answers));
        }
    }

    /// <summary>
    ///     Gets or sets the Message. Raises PropertyChanged event when the value is changed.
    /// </summary>
    public Message Message
    {
        get => _message;
        set
        {
            if (_message == value) return;
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    //Commands


    /// <summary>
    ///     Gets the command that initiates a search operation.
    /// </summary>
    public ICommand SearchCommand { get; private set; }

    /// <summary>
    ///     Represents a command that updates the data.
    /// </summary>
    public ICommand UpdateDataCommand { get; private set; }

    /// <summary>
    ///     This command opens a question window.
    /// </summary>
    public ICommand OpenQuestionWindowCommand { get; private set; }

    /// <summary>
    ///     Represents the command that opens a new question window.
    /// </summary>
    public ICommand OpenNewQuestionWindowCommand { get; private set; }

    /// <summary>
    ///     Gets the command that executes when the Submit Question action is triggered.
    /// </summary>
    public ICommand SubmitQuestionCommand { get; private set; }

    /// <summary>
    ///     Gets the delete command.
    /// </summary>
    public ICommand DeleteCommand { get; private set; }

    /// <summary>
    ///     Resets the FilteredQuestions variable to its default value to update the view.
    /// </summary>
    private void ResetToDefault()
    {
        _ = GetQuestions();
    }


    /// <summary>
    ///     Initializes the commands used in the class.
    /// </summary>
    public void InitializeCommands()
    {
        UpdateDataCommand = new AsyncRelayCommand(async _ => await GetQuestions());
        SubmitQuestionCommand = new AsyncRelayCommand(async _ => await QuestionCommand());
        OpenQuestionWindowCommand = new RelayCommand(PerformOpenQuestionWindow);
        OpenNewQuestionWindowCommand = new RelayCommand(_ => PerformOpenNewQuestionWindow());
        DeleteCommand = new AsyncRelayCommand(async _ => await DeleteQuestion());
        SearchCommand = new RelayCommand(_ => PerformSearch());
    }

    /// <summary>
    ///     Initializes the object asynchronously.
    ///     If a SelectedQuestion is provided,
    ///     it sets the fields based on the SelectedQuestion.
    ///     Otherwise, it retrieves questions and sets answers.
    /// </summary>
    public async Task InitializeAsync()
    {
        if (SelectedQuestion != null)
        {
            SetFieldsFromSelectedQuestion(SelectedQuestion);
        }
        else
        {
            await GetQuestions();
            SetAnswers();
        }
    }


    /// <summary>
    ///     Open a new QuestionWindow to update data
    /// </summary>
    /// <param name="param"></param>
    public void PerformOpenQuestionWindow(object param)
    {
        if (param is Question question)
        {
            SelectedQuestion = question;
            _ = InitializeAsync();
            var window = new QuestionWindow { DataContext = this };
            window.Closed += (sender, args) =>
            {
                Message = new Message();
                ResetToDefault();
            };
            window.ShowDialog();
        }
        else
        {
            SetMessage("Hoppá! Nem megfelelő objektum", "Red");
        }
    }

    /// <summary>
    ///     Open a new QuestionWindow to add a new Question
    /// </summary>
    public void PerformOpenNewQuestionWindow()
    {
        QuestionWindow window = new();
        window.Closed += (sender, args) =>
        {
            Message = new Message();
            ResetCurrentQuestion();
            ResetToDefault();
        };
        window.ShowDialog();
    }

    /// <summary>
    ///     Get all Questions with Answers and Topics
    /// </summary>
    /// <returns>Task representing the asynchronous operation.</returns>
    public async Task GetQuestions()
    {
        Message = new Message();
        try
        {
            if (CurrentUser?.AuthToken != null)
            {
                var topicResponse = await _topicRepository.GetByAll(CurrentUser.AuthToken);
                var questionResponse = await _questionRepository.GetByAll(CurrentUser.AuthToken);

                if (topicResponse.Success && questionResponse.Success)
                {
                    Topics = topicResponse.Data;
                    Questions = questionResponse.Data;
                    FilteredQuestions = new ObservableCollection<Question>(Questions);
                }
                else
                {
                    SetMessage($"Hiba történt az adatok lekérésekor: " +
                               $"Témák: {topicResponse.Message} " +
                               $"Kérdések: {questionResponse.Message}", "Red");
                }
            }
            else
            {
                SetMessage("Hiba: Nem értelmezhető felhasználó adatok.", "Red");
            }
        }
        catch (Exception ex)
        {
            SetMessage($"Hiba történt az adatok frissítése közben: {ex.Message}", "Red");
        }
    }

    /// <summary>
    ///     Sets the fields for the selected question.
    /// </summary>
    /// <param name="question">The question object selected by the user.</param>
    public void SetFieldsFromSelectedQuestion(Question question)
    {
        TopicName = GetTopicNameById(question.TopicId);
        for (var i = 0; i < question.Answers.Count; i++)
        {
            if (question.Answers[i].IsCorrect) CorrectAnswerNumber = i + 1;
            switch (i)
            {
                case 0:
                    Answer1 = new Answer
                    {
                        AnswerText = question.Answers[0].AnswerText,
                        Id = question.Answers[0].Id
                    };
                    break;

                case 1:
                    Answer2 = new Answer
                    {
                        AnswerText = question.Answers[1].AnswerText,
                        Id = question.Answers[1].Id
                    };
                    break;

                case 2:
                    Answer3 = new Answer
                    {
                        AnswerText = question.Answers[2].AnswerText,
                        Id = question.Answers[2].Id
                    };
                    break;

                case 3:
                    Answer4 = new Answer
                    {
                        AnswerText = question.Answers[3].AnswerText,
                        Id = question.Answers[3].Id
                    };
                    break;
            }
        }

        if (SelectedQuestion != null) QuestionText = SelectedQuestion.QuestionText;
    }


    /// <summary>
    ///     This method is used for the QuestionCommand. If a Question is selected, it makes an update to the question
    ///     using the async API call from _questionRepository object. If the API response is successful, it resets the
    ///     SelectedQuestion object and
    ///     sets a success message. If the API response is unsuccessful, it sets a message with the reason for failure.
    ///     If no question is selected, it attempts to add a Question using the _questionRepository object. Again, if the API
    ///     response is
    ///     successful, it resets the SelectedQuestion object and sets a success message. If the API response is unsuccessful,
    ///     it sets a message
    ///     with an error. Exceptions are caught and their message is set as the error message.
    /// </summary>
    public async Task QuestionCommand()
    {
        Message = new Message();
        if (SelectedQuestion != null)
        {
            if (!MakeQuestionObject()) return;
            try
            {
                if (CurrentUser != null)
                {
                    var questionResponse =
                        await _questionRepository.UpdateAsync(SelectedQuestion, CurrentUser.AuthToken);

                    if (questionResponse.Success)
                    {
                        SetMessage("Adatok sikeresen frissítve!", "Green");
                        SelectedQuestion = new Question();
                    }
                    else
                    {
                        SetMessage($"{questionResponse.StatusCode}: {questionResponse.Message}", "Red");
                    }
                }
                else
                {
                    SetMessage("Hiba: Érvénytelen felhasználói adatok!", "Red");
                }
            }
            catch (Exception ex)
            {
                SetMessage($"Hiba: {ex.Message}", "Red");
            }
        }
        else
        {
            if (!MakeQuestionObject()) return;

            try
            {
                if (SelectedQuestion != null && CurrentUser != null)
                {
                    var questionResponse = await _questionRepository.AddAsync(SelectedQuestion, CurrentUser.AuthToken);
                    if (questionResponse.Success)
                    {
                        SetMessage("Sikeres kérdés felvétel!", "Green");
                        SelectedQuestion = new Question();
                    }
                    else
                    {
                        SetMessage($"{questionResponse.StatusCode}: {questionResponse.Message}", "Red");
                    }
                }
                else
                {
                    SetMessage("Hiba: Érvénytelen felhasználói/kérdés adatok!", "Red");
                }
            }
            catch (Exception ex)
            {
                SetMessage($"Hiba: {ex.Message}", "Red");
            }
        }
    }

    /// <summary>
    ///     Get all questions with a specific topic name
    /// </summary>
    public void PerformSearch()
    {
        if (Topics == null)
        {
            ResetToDefault();
            return;
        }

        var relevantTopicIds = Topics
            .Where(t => t.TopicName.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            .Select(t => t.Id)
            .ToList();
        if (Questions != null)
        {
            var filtered =
                new ObservableCollection<Question>(Questions.Where(q => relevantTopicIds.Contains(q.TopicId)).ToList());

            FilteredQuestions?.Clear();
            FilteredQuestions = filtered;
        }

        if (FilteredQuestions != null && !FilteredQuestions.Any()) SetMessage("Nincs ilyen Téma", "Red");
    }

    /// <summary>
    ///     Get an id of a topic by its name
    /// </summary>
    /// <param name="topicName"></param>
    /// <returns></returns>
    public int GetTopicIdByName(string topicName)
    {
        if (Topics == null)
            return 0;

        var topicId = Topics
            .Where(t => t.TopicName.Contains(topicName, StringComparison.OrdinalIgnoreCase))
            .Select(t => t.Id)
            .FirstOrDefault();

        return topicId;
    }

    /// <summary>
    ///     Retrieves the topic name by the given id.
    /// </summary>
    /// <param name="selectedTopicId">The identifier of the selected topic.</param>
    /// <returns>The name of the topic.</returns>
    public string GetTopicNameById(int selectedTopicId)
    {
        if (Topics == null || Topics.Count == 0)
        {
            SetMessage("A témák listája üres vagy nem inicializált!", "Red");
            return "";
        }

        var topicName = Topics
            .Where(t => t.Id == selectedTopicId)
            .Select(t => t.TopicName)
            .FirstOrDefault();

        if (string.IsNullOrEmpty(topicName))
        {
            SetMessage("Nem található téma az adott azonosítóval.", "Red");
            return "";
        }

        return topicName;
    }

    /// <summary>
    ///     Sets the correct answer for a given question.
    /// </summary>
    /// <param name="question">The question to set the correct answer for.</param>
    /// <param name="correctAnswerNumber">The number representing the correct answer (between 1 and 4).</param>
    public void SetCorrectAnswer(Question question, int correctAnswerNumber)
    {
        foreach (var answer in question.Answers) answer.IsCorrect = false;

        switch (correctAnswerNumber)
        {
            case 1:
                question.Answers[0].IsCorrect = true;
                break;

            case 2:
                question.Answers[1].IsCorrect = true;
                break;

            case 3:
                question.Answers[2].IsCorrect = true;
                break;

            case 4:
                question.Answers[3].IsCorrect = true;
                break;

            default:
                SetMessage("A helyes válasz száma érványtelen: 1-4", "Red");
                break;
        }
    }

    /// <summary>
    ///     Makes a question object based on the given information get from the view.
    ///     Validates question fields and assigns values to the object properties.
    ///     Returns true if successful, otherwise false.
    /// </summary>
    public bool MakeQuestionObject()
    {
        if (!ValidateQuestionFields()) return false;

        if (TopicName != null)
        {
            var topicId = GetTopicIdByName(TopicName);
            if (topicId == 0)
            {
                SetMessage($"Nincs ilyen téma, vegye fel a következőt: {TopicName}", "Red");
                return false;
            }

            SetAnswersList();

            SelectedQuestion ??= new Question();
            SelectedQuestion.TopicId = topicId;
        }

        if (_answers != null)
            if (SelectedQuestion != null)
                SelectedQuestion.Answers = _answers;

        if (CorrectAnswerNumber is 0 or null)
        {
            SetMessage("A helyes válasz száma érványtelen: 1-4", "Red");
            return false;
        }

        if (SelectedQuestion == null) return true;
        SetCorrectAnswer(SelectedQuestion, (int)CorrectAnswerNumber);
        if (QuestionText != null) SelectedQuestion.QuestionText = QuestionText;

        return true;
    }

    /// <summary>
    ///     Validates the question fields.
    /// </summary>
    /// <returns>True if all question fields are valid, otherwise false.</returns>
    public bool ValidateQuestionFields()
    {
        if (string.IsNullOrEmpty(TopicName))
        {
            SetMessage("A téma megadása kötelező.", "Red");
            return false;
        }

        SetAnswers();
        if (string.IsNullOrEmpty(Answer1?.AnswerText) || string.IsNullOrEmpty(Answer2?.AnswerText) ||
            string.IsNullOrEmpty(Answer3?.AnswerText) || string.IsNullOrEmpty(Answer4?.AnswerText))
        {
            SetMessage("Minden válaszlehetőség kitöltése kötelező.", "Red");
            return false;
        }

        if (string.IsNullOrEmpty(QuestionText))
        {
            SetMessage("A kérdés szövege nem lehet üres.", "Red");
            return false;
        }

        if (CorrectAnswerNumber != 0 && !(CorrectAnswerNumber > 4)) return true;
        SetMessage("A helyes válasz száma érvénytelen!(0-4)", "Red");
        return false;
    }

    /// <summary>
    ///     Sets the answers list.
    ///     If Answer1 is null, adds a new Answer instance ...etc.
    /// </summary>
    public void SetAnswersList()
    {
        _answers =
        [
            Answer1 ?? new Answer(),
            Answer2 ?? new Answer(),
            Answer3 ?? new Answer(),
            Answer4 ?? new Answer()
        ];
    }

    /// <summary>
    ///     Sets the answers to avoid null exceptions.
    /// </summary>
    public void SetAnswers()
    {
        Answer1 ??= new Answer();

        Answer2 ??= new Answer();

        Answer3 ??= new Answer();

        Answer4 ??= new Answer();
    }

    /// <summary>
    ///     Resets the current question.
    /// </summary>
    public void ResetCurrentQuestion()
    {
        SelectedQuestion = null;
    }

    /// <summary>
    ///     Deletes a question by a give id.
    /// </summary>
    /// <returns>Task representing the asynchronous operation.</returns>
    public async Task DeleteQuestion()
    {
        Message = new Message();
        try
        {
            if (SelectedQuestion != null && CurrentUser != null)
            {
                var response = await _questionRepository.DeleteAsync(SelectedQuestion.Id, CurrentUser.AuthToken);
                if (response.Success)
                {
                    SetMessage("A kérdés sikeresen törölve.", "Green");
                    Questions?.Remove(SelectedQuestion);
                    SelectedQuestion = null;
                }
                else
                {
                    SetMessage($"{response.StatusCode}: {response.Message}", "Red");
                }
            }
            else
            {
                SetMessage("Hiba: Érvénytelen felhasználói/kérdés adatok!", "Red");
            }
        }
        catch (Exception ex)
        {
            SetMessage($"Hiba: {ex.Message}", "Red");
        }
    }

    /// <summary>
    ///     Sets a message with the specified text and color.
    /// </summary>
    /// <param name="text">The text of the message.</param>
    /// <param name="color">The color of the message.</param>
    public void SetMessage(string text, string color)
    {
        Message.MessageText = text;
        Message.MessageColor = color;
    }
}