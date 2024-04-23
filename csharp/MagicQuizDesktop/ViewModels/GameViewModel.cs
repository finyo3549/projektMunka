using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MagicQuizDesktop.Commands;
using MagicQuizDesktop.Models;
using MagicQuizDesktop.Repositories;
using MagicQuizDesktop.Services;
using Message = MagicQuizDesktop.Models.Message;
using MessageBox = System.Windows.MessageBox;

namespace MagicQuizDesktop.ViewModels;

/// <summary>
///     Represents a the viewmodel for the game.
/// </summary>
public class GameViewModel : ViewModelBase
{
    /// <summary>
    ///     Repository for storing and retrieving questions.
    /// </summary>
#pragma warning disable CA1859
    public readonly IQuestionRepository _questionRepository;
#pragma warning restore CA1859

    /// <summary>
    ///     Represents a timer for asking questions.
    /// </summary>
    private readonly DispatcherTimer _questionTimer;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Random" /> class.
    /// </summary>
    private readonly Random _rand = new();

    /// <summary>
    ///     Repository for storing and retrieving ranks
    /// </summary>
#pragma warning disable CA1859
    public readonly IRankRepository _rankRepository;
#pragma warning restore CA1859


    /// -------------
    /// Interfaces
    /// ------------
    /// <summary>
    ///     Repository for storing and retrieving topics.
    /// </summary>
    public readonly ITopicRepository _topicRepository;

    /// <summary>
    ///     Represents the collection of used question indexes.
    /// </summary>
    private readonly List<int> _usedQuestionIndexes = new();

    /// <summary>
    ///     The actual score of the game from database.
    /// </summary>
    private int _actualScore;


    /// <summary>
    ///     Represents the private answers for the code.
    /// </summary>
    private Answer _answer1 = null!;

    /// <summary>
    ///     Initializes a new instance of the <see cref="_answer1Background" /> class with a solid color
    ///     brush set to the color blue.
    /// </summary>
    private Brush _answer1Background = new SolidColorBrush(Colors.Blue);

    private Answer _answer2 = null!;

    /// <summary>
    ///     Initializes a new instance of the SolidColorBrush class with the specified color.
    /// </summary>
    private Brush _answer2Background = new SolidColorBrush(Colors.Blue);

    private Answer _answer3 = null!;

    /// <summary>
    ///     Initializes a new instance of the SolidColorBrush class with the color set to blue.
    /// </summary>
    private Brush _answer3Background = new SolidColorBrush(Colors.Blue);

    private Answer _answer4 = null!;

    /// <summary>
    ///     Sets the background color to blue.
    /// </summary>
    private Brush _answer4Background = new SolidColorBrush(Colors.Blue);

    /// <summary>
    ///     A variable representing the answers.
    /// </summary>
    private readonly List<Answer> _answers;

    /// <summary>
    ///     Represents the help status for the audience.
    /// </summary>
    private bool _audienceHelpStatus;

    /// ----------
    /// Private properties
    /// -----------
    private User _currentUser = null!;

    /// <summary>
    ///     Represents a flag indicating whether the game is running or not.
    /// </summary>
    private bool _gameRunning;

    /// <summary>
    ///     Represents the status of the half booster.
    /// </summary>
    private bool _halfBoosterStatus;


    private Message _message = null!;

    /// <summary>
    ///     Represents the status of a phone friend help.
    /// </summary>
    private bool _phoneFriendHelpStatus;

    /// <summary>
    ///     Represents a collection of the questions.
    /// </summary>
    private List<Question> _questions = null!;

    /// <summary>
    ///     Represents the text of a question.
    /// </summary>
    /// <remarks>
    ///     The value of this field should be assigned by the code that populates the question.
    /// </remarks>
    private string _questionText = null!;

    /// <summary>
    ///     The _score variable to save the user points.
    /// </summary>
    private int _score;

    /// <summary>
    ///     Represents the amount of time left.
    /// </summary>
    private int _timeLeft;

    /// <summary>
    ///     The name of the topic.
    /// </summary>
    private string _topicName = null!;

    /// <summary>
    ///     Represents a collection of the topics.
    /// </summary>
    private List<Topic> _topics = null!;

    /// <summary>
    ///     This variable represents whether the object has been updated or not.
    /// </summary>
    private bool _updated;

    /// <summary>
    ///     The rank of the user.
    /// </summary>
    private Rank _userRank = null!;

    /// <summary>
    ///     Initializes a new instance of the GameViewModel class.
    /// </summary>
    public GameViewModel()
    {
        CurrentUser = SessionManager.Instance.CurrentUser;
        Message = new Message();
        _topicRepository = new TopicRepository();
        _questionRepository = new QuestionRepository();
        _rankRepository = new RankRepository();
        InitializeProperties();
        InitializeCommands();
        _answers = [];
        InitializeBoosters();
        _questionTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _questionTimer.Tick += QuestionTimer_Tick;
        InitializeAnswers();
    }

    /// -----------
    /// Public properties
    /// -----------
    /// <summary>
    ///     Gets the value of the Clock property.
    /// </summary>
    public string Clock => _timeLeft.ToString();

    /// <summary>
    ///     Gets or sets the current user.
    /// </summary>
    public User CurrentUser
    {
        get => _currentUser;
        set
        {
            if (_currentUser != value)
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                // Itt frissítheted a userId-t is, ha szükséges
            }
        }
    }

    /// <summary>
    ///     Gets or sets the message.
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


    /// <summary>
    ///     Gets or sets the score.
    /// </summary>
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnPropertyChanged(nameof(Score));
        }
    }

    /// <summary>
    ///     Gets or sets the name of the topic.
    /// </summary>
    public string TopicName
    {
        get => _topicName;
        set
        {
            _topicName = value;
            OnPropertyChanged(nameof(TopicName));
        }
    }

    /// <summary>
    ///     Gets or sets the question text.
    /// </summary>
    public string QuestionText
    {
        get => _questionText;
        set
        {
            _questionText = value;
            OnPropertyChanged(nameof(QuestionText));
        }
    }

    /// <summary>
    ///     Gets or sets the first answer.
    /// </summary>
    public Answer Answer1
    {
        get => _answer1;
        set
        {
            if (_answer1 == value) return;
            _answer1 = value;
            OnPropertyChanged(nameof(Answer1));
        }
    }

    /// <summary>
    ///     Gets or sets the second answer.
    /// </summary>
    public Answer Answer2
    {
        get => _answer2;
        set
        {
            if (_answer2 == value) return;
            _answer2 = value;
            OnPropertyChanged(nameof(Answer2));
        }
    }

    /// <summary>
    ///     Gets or sets the answer.
    /// </summary>
    public Answer Answer3
    {
        get => _answer3;
        set
        {
            if (_answer3 == value) return;
            _answer3 = value;
            OnPropertyChanged(nameof(Answer3));
        }
    }

    /// <summary>
    ///     Gets or sets the answer.
    /// </summary>
    public Answer Answer4
    {
        get => _answer4;
        set
        {
            if (_answer4 == value) return;
            _answer4 = value;
            OnPropertyChanged(nameof(Answer4));
        }
    }

    /// <summary>
    ///     Gets or sets the status of the phone friend help.
    /// </summary>
    public bool PhoneFriendHelpStatus
    {
        get => _phoneFriendHelpStatus;
        set
        {
            _phoneFriendHelpStatus = value;
            OnPropertyChanged(nameof(PhoneFriendHelpStatus));
        }
    }

    /// <summary>
    ///     Gets or sets the half booster status.
    /// </summary>
    public bool HalfBoosterStatus
    {
        get => _halfBoosterStatus;
        set
        {
            _halfBoosterStatus = value;
            OnPropertyChanged(nameof(HalfBoosterStatus));
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether the Audience Help status is enabled.
    /// </summary>
    public bool AudienceHelpStatus
    {
        get => _audienceHelpStatus;
        set
        {
            _audienceHelpStatus = value;
            OnPropertyChanged(nameof(AudienceHelpStatus));
        }
    }

    /// <summary>
    ///     Gets or sets the value indicating whether the instance has been updated.
    /// </summary>
    public bool Updated
    {
        get => _updated;
        set
        {
            _updated = value;
            Message = new Message();
            OnPropertyChanged(nameof(Updated));
            OnPropertyChanged(nameof(Message));
        }
    }

    /// <summary>
    ///     Gets or sets the rank of the user.
    /// </summary>
    public Rank UserRank
    {
        get => _userRank;
        set
        {
            if (_userRank == value) return;
            _userRank = value;
            OnPropertyChanged(nameof(UserRank));
        }
    }

    /// <summary>
    ///     Gets or sets the background brush for Answer1.
    /// </summary>
    public Brush Answer1Background
    {
        get => _answer1Background;
        set
        {
            if (_answer1Background == value) return;
            _answer1Background = value;
            OnPropertyChanged(nameof(Answer1Background));
        }
    }

    /// <summary>
    ///     Gets or sets the background brush for Answer2.
    /// </summary>
    public Brush Answer2Background
    {
        get => _answer2Background;
        set
        {
            if (_answer2Background == value) return;
            _answer2Background = value;
            OnPropertyChanged(nameof(Answer2Background));
        }
    }

    /// <summary>
    ///     Gets or sets the background brush for Answer 3.
    /// </summary>
    public Brush Answer3Background
    {
        get => _answer3Background;
        set
        {
            if (_answer3Background == value) return;
            _answer3Background = value;
            OnPropertyChanged(nameof(Answer3Background));
        }
    }

    /// <summary>
    ///     Gets or sets the background brush for Answer4.
    /// </summary>
    public Brush Answer4Background
    {
        get => _answer4Background;
        set
        {
            if (_answer4Background == value) return;
            _answer4Background = value;
            OnPropertyChanged(nameof(Answer4Background));
        }
    }

    /// <summary>
    ///     Gets or sets the UpdateDataCommand.
    /// </summary>

    public ICommand UpdateDataCommand { get; private set; } = null!;

    /// <summary>
    ///     Gets or sets the StartGameCommand.
    /// </summary>
    public ICommand StartGameCommand { get; private set; } = null!;

    /// <summary>
    ///     Gets the answer command.
    /// </summary>
    public ICommand AnswerCommand { get; private set; } = null!;

    /// <summary>
    ///     Gets the half booster command.
    /// </summary>
    public ICommand HalfBoosterCommand { get; private set; } = null!;

    /// <summary>
    ///     Gets or sets the command for friend's phone.
    /// </summary>
    public ICommand FriendPhoneCommand { get; private set; } = null!;

    /// <summary>
    ///     Gets or sets the command for helping the audience.
    /// </summary>
    public ICommand AudienceHelpCommand { get; private set; } = null!;

    /// <summary>
    ///     Initializes properties to their default values.
    /// </summary>
    public void InitializeProperties()
    {
        _answer1 = new Answer();
        _answer2 = new Answer();
        _answer3 = new Answer();
        _answer4 = new Answer();
        _message = new Message();
        _questions = [];
        _questionText = string.Empty;
        _topicName = string.Empty;
        _topics = [];
        _userRank = new Rank();
    }

    /// <summary>
    ///     Initializes the commands used in the program.
    /// </summary>
    public void InitializeCommands()
    {
        UpdateDataCommand = new AsyncRelayCommand(async _ => await UpdateData());
        StartGameCommand = new RelayCommand(_ => StartGame());
        AnswerCommand = new RelayCommand(AnswerClicked);
        HalfBoosterCommand = new RelayCommand(_ => ApplyHalfBoosterEffect());
        FriendPhoneCommand = new RelayCommand(_ => ApplyPhoneFriendHelp());
        AudienceHelpCommand = new RelayCommand(_ => ApplyAudienceHelp());
    }

    /// <summary>
    ///     Initializes the boosters.
    /// </summary>
    public void InitializeBoosters()
    {
        HalfBoosterStatus = false;
        PhoneFriendHelpStatus = false;
        AudienceHelpStatus = false;
    }

    /// <summary>
    ///     Updates the datas --> Get the questions and topics.
    /// </summary>
    public async Task UpdateData()
    {
        Updated = false;
        try
        {
            var topicResponse = await _topicRepository.GetByAll(CurrentUser.AuthToken);
            var questionResponse = await _questionRepository.GetByAll(CurrentUser.AuthToken);
            await GetUserRank();

            if (topicResponse.Success && questionResponse.Success)
            {
                _topics = topicResponse.Data;
                _questions = questionResponse.Data;
                SetMessage("Adatok sikeresen frissítve!", "Green");
                await GetUserRank();
            }
            else
            {
                SetMessage($"Témák: {topicResponse.Message} " +
                           $"Kérdések: {questionResponse.Message}", "Red");
            }
        }
        catch (Exception ex)
        {
            SetMessage($"Hiba történt az adatok frissítése közben: {ex.Message}", "Red");
        }

        _gameRunning = false;
    }

    /// <summary>
    ///     Retrieves the user's rank by requesting the score from the rank repository.
    ///     If the request is successful, the actual score is updated.
    /// </summary>
    public async Task GetUserRank()
    {
        var response = await _rankRepository.GetScore(CurrentUser.Id, CurrentUser.AuthToken);
        if (response.Success) _actualScore = response.Data.Score;
        else
            SetMessage("Nem sikerült lekérni a felhasználó eredményét.", "Red");
    }

    /// <summary>
    ///     Decrements the time left by one, updates the clock property, and checks if the time is up.
    ///     If the time is up, it stops the question timer, displays a message box, and proceeds to the next question.
    /// </summary>
    public void QuestionTimer_Tick(object? sender, EventArgs e)
    {
        _timeLeft--;
        OnPropertyChanged(nameof(Clock));
        if (_timeLeft > 0) return;
        _questionTimer.Stop();
        MessageBox.Show("Az idő lejárt! Következő kérdés...");
        DisplayCurrentQuestion(); // The next question
    }

    /// <summary>
    ///     Starts a question timer with the specified number of seconds.
    /// </summary>
    /// <param name="seconds">The number of seconds for the question timer.</param>
    public void StartQuestionTimer(int seconds)
    {
        _timeLeft = seconds;
        OnPropertyChanged(nameof(Clock));
        _questionTimer.Start();
    }

    /// <summary>
    ///     Resumes the timer for the question.
    /// </summary>
    public void ResumeTimer()
    {
        if (!_questionTimer.IsEnabled && _gameRunning) _questionTimer.Start();
    }

    /// <summary>
    ///     Pauses the timer if it is currently enabled.
    /// </summary>
    public void PauseTimer()
    {
        if (_questionTimer.IsEnabled) _questionTimer.Stop();
    }

    /// <summary>
    ///     Starts the game by checking if the questions list is null or empty.
    ///     If so, sets the Updated flag to false and displays an error message.
    ///     Otherwise, sets the Updated flag to true, resets the colors, and initializes the score and various help statuses.
    ///     Displays the current question.
    /// </summary>
    public void StartGame()
    {
        if (_questions.Count == 0)
        {
            Updated = false;
            SetMessage("Kérlek, először frissítsd az adatokat!", "Red");
            return;
        }

        Updated = true;
        ResetColors();
        _score = 0;
        Score = _score;
        HalfBoosterStatus = true;
        PhoneFriendHelpStatus = true;
        AudienceHelpStatus = true;
        DisplayCurrentQuestion();
    }

    /// <summary>
    ///     Displays the current question. Retrieves a random question from the collection of questions and presents it to the
    ///     user.
    ///     Sets the topic name, question text, and answer options for the current question.
    ///     Activates the buttons for selecting answers and starts a timer for the question.
    /// </summary>
    public async void DisplayCurrentQuestion()
    {
        _gameRunning = true;
        if (_usedQuestionIndexes.Count < 10)
        {
            Random rand = new();
            int questionIndex;
            do
            {
                questionIndex = rand.Next(_questions.Count);
            } while (_usedQuestionIndexes.Contains(questionIndex));

            _usedQuestionIndexes.Add(questionIndex);
            var currentQuestion = _questions[questionIndex];
            var topic = _topics.FirstOrDefault(t => t.Id == currentQuestion.TopicId);
            TopicName = $"Téma: {topic?.TopicName ?? "Ismeretlen téma"}";
            QuestionText = currentQuestion.QuestionText;

            // Set Answers
            _answers.Clear();

            Answer1 = currentQuestion.Answers.Count > 0 ? currentQuestion.Answers[0] : new Answer();
            Answer2 = currentQuestion.Answers.Count > 1 ? currentQuestion.Answers[1] : new Answer();
            Answer3 = currentQuestion.Answers.Count > 2 ? currentQuestion.Answers[2] : new Answer();
            Answer4 = currentQuestion.Answers.Count > 3 ? currentQuestion.Answers[3] : new Answer();

            InOrActivateButtons(true);

            _answers.Add(Answer1);
            _answers.Add(Answer2);
            _answers.Add(Answer3);
            _answers.Add(Answer4);

            StartQuestionTimer(20);
        }
        else
        {
            Updated = false;
            _gameRunning = false;
            _actualScore += _score;
            SetMessage($"A játék véget ért! Pontszámod: {_actualScore}", "Green");
            UserRank = new Rank
            {
                UserId = CurrentUser.Id,
                Score = _actualScore
            };
            var response = await _rankRepository.PutScore(CurrentUser.Id, UserRank, CurrentUser.AuthToken);
            if (response.Success)
                MessageBox.Show("Sikeresen frissítettük a pontszámodat!");
            else
                MessageBox.Show("Nem sikerült frissíteni a felhasználó rangját.");
            Application.Current.Dispatcher.Invoke(() =>
            {
                var result = MessageBox.Show("Szeretnél még egy játékot játszani?", "Játék vége",
                    MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _usedQuestionIndexes.Clear();
                    StartGame();
                }
                else
                {
                    Updated = false;
                    _usedQuestionIndexes.Clear();
                    SetMessage($"Összpontszámod: {_actualScore}", "Green");
                }
            });
        }
    }

    /// <summary>
    ///     Applies the half booster effect.
    /// </summary>
    public void ApplyHalfBoosterEffect()
    {
        var currentQuestionIndex = _usedQuestionIndexes.Last();
        var currentAnswers = _questions[currentQuestionIndex].Answers;
        var incorrectAnswers = currentAnswers.Where(a => !a.IsCorrect).ToList();

        var numberOfAnswersToRemove = incorrectAnswers.Count - 1;

        while (numberOfAnswersToRemove > 0)
        {
            var toRemove = incorrectAnswers[_rand.Next(incorrectAnswers.Count)];

            var index = currentAnswers.IndexOf(toRemove);
            SetAnswerInactiveAndRed(index);
            incorrectAnswers.Remove(toRemove);
            numberOfAnswersToRemove--;
        }

        HalfBoosterStatus = false;
    }

    /// <summary>
    ///     Sets the specified answer at the given index to inactive and sets its background color to red.
    /// </summary>
    /// <param name="answerIndex">The index of the answer to be set inactive and red.</param>
    public void SetAnswerInactiveAndRed(int answerIndex)
    {
        switch (answerIndex)
        {
            case 0:
                Answer1.IsActive = false;
                Answer1Background = new SolidColorBrush(Colors.Red);
                break;

            case 1:
                Answer2.IsActive = false;
                Answer2Background = new SolidColorBrush(Colors.Red);
                break;

            case 2:
                Answer3.IsActive = false;
                Answer3Background = new SolidColorBrush(Colors.Red);
                break;

            case 3:
                Answer4.IsActive = false;
                Answer4Background = new SolidColorBrush(Colors.Red);
                break;
        }
    }

    /// <summary>
    ///     Applies the "Phone Friend Help" feature.
    ///     Determines the index of the current question. Retrieves the answers for the current question.
    ///     Calculates the probability of the correct answer based on a preset value.
    ///     Determines whether the helper is correct based on a random number comparison with the probability of the correct
    ///     answer.
    ///     Determines the index of the answer to highlight for the phone friend help.
    ///     Resets the phone friend help status and highlights the answer for the phone friend help.
    /// </summary>
    public void ApplyPhoneFriendHelp()
    {
        var currentQuestionIndex = _usedQuestionIndexes.Last();
        var currentAnswers = _questions[currentQuestionIndex].Answers;

        var probabilityOfCorrectAnswer = 0.70;

        var helperIsCorrect = _rand.NextDouble() < probabilityOfCorrectAnswer;

        int answerIndex;
        if (helperIsCorrect || !currentAnswers.Any(a => a.IsCorrect))
        {
            var correctAnswers = currentAnswers.Where(a => a.IsCorrect).ToList();
            answerIndex = currentAnswers.IndexOf(correctAnswers[_rand.Next(correctAnswers.Count)]);
        }
        else
        {
            var incorrectAnswers = currentAnswers.Where(a => !a.IsCorrect).ToList();
            answerIndex = currentAnswers.IndexOf(incorrectAnswers[_rand.Next(incorrectAnswers.Count)]);
        }

        PhoneFriendHelpStatus = false;
        HighlightAnswerForPhoneFriendHelp(answerIndex);
    }

    /// <summary>
    ///     Highlights the answer for phone friend help.
    /// </summary>
    /// <param name="answerIndex">The index of the answer.</param>
    public void HighlightAnswerForPhoneFriendHelp(int answerIndex)
    {
        MessageBox.Show(
            $"A telefonos segítő válasza: {_questions[_usedQuestionIndexes.Last()].Answers[answerIndex].AnswerText}");
    }

    /// <summary>
    ///     Applies the audience help feature to select the votes for each answer in the current question.
    /// </summary>
    public void ApplyAudienceHelp()
    {
        var currentQuestionIndex = _usedQuestionIndexes.Last();
        var currentAnswers = _questions[currentQuestionIndex].Answers;

        var audienceSize = _rand.Next(50, 101);

        var votes = new Dictionary<int, int>();
        foreach (var answer in currentAnswers) votes[currentAnswers.IndexOf(answer)] = 0;

#pragma warning disable CS8604 // Possible null reference argument.
        var correctAnswerIndex = currentAnswers.IndexOf(currentAnswers.FirstOrDefault(a => a.IsCorrect));
#pragma warning restore CS8604 // Possible null reference argument.
        var bonusVotesForCorrect = (int)(audienceSize * 0.3);

        for (var i = 0; i < audienceSize; i++)
        {
            var voteIndex = _rand.Next(currentAnswers.Count);
            if (_rand.NextDouble() < 0.7) voteIndex = correctAnswerIndex;
            votes[voteIndex]++;
        }

        votes[correctAnswerIndex] += bonusVotesForCorrect;

        AudienceHelpStatus = false;

        ShowAudiencePollResults(votes, audienceSize);
    }

    /// <summary>
    ///     Shows the audience poll results.
    /// </summary>
    /// <param name="votes">The dictionary containing the votes.</param>
    /// <param name="audienceSize">The size of the audience.</param>
    public static void ShowAudiencePollResults(Dictionary<int, int> votes, int audienceSize)
    {
        StringBuilder result = new();
        result.AppendLine("Közönség szavazatai:");
        foreach (var vote in votes)
            result.AppendLine($"Válasz {vote.Key + 1}: {(double)vote.Value / audienceSize * 100:0.0}%");
        MessageBox.Show(result.ToString());
    }

    /// <summary>
    ///     Handles the event when the answer button is clicked.
    /// </summary>
    /// <param name="parameter">The parameter representing the index of the selected answer.</param>
    public async void AnswerClicked(object parameter)
    {
        _questionTimer.Stop();
        if (int.TryParse(parameter.ToString(), out var answerIndex) && _usedQuestionIndexes.Any())
        {
            InOrActivateButtons(false);
            var currentQuestionIndex = _usedQuestionIndexes.Last();

            if (!ValidateAnswers(currentQuestionIndex)) return;

            var selectedAnswer = _questions[currentQuestionIndex].Answers[answerIndex];
            var correctAnswer = _questions[currentQuestionIndex].Answers.FirstOrDefault(a => a.IsCorrect);
            if (correctAnswer != null)
            {
                var correctAnswerIndex = _questions[currentQuestionIndex].Answers.IndexOf(correctAnswer);

                ChangeAnswerBackground(false, answerIndex);

                if (selectedAnswer.IsCorrect)
                {
                    ChangeAnswerBackground(true, answerIndex);
                    Score += 100;
                }
                else
                {
                    ChangeAnswerBackground(false, answerIndex);
                    ChangeAnswerBackground(true, correctAnswerIndex);
                }
            }

            await Task.Delay(3000); // Wait 3 seconds for the next question to see the right answer

            DisplayCurrentQuestion();
            ResetColors();
        }
        else
        {
            Updated = false;
            _gameRunning = false;
            SetMessage($"Nem sikerült értelmezni a választ: {parameter}", "Red");
        }
    }

    /// <summary>
    ///     Resets the background colors of answer options to blue.
    /// </summary>
    public void ResetColors()
    {
        Answer1Background = new SolidColorBrush(Colors.Blue);
        Answer2Background = new SolidColorBrush(Colors.Blue);
        Answer3Background = new SolidColorBrush(Colors.Blue);
        Answer4Background = new SolidColorBrush(Colors.Blue);
    }

    /// <summary>
    ///     Changes the background color of the answer based on correctness.
    /// </summary>
    /// <param name="correct">A boolean value indicating whether the answer is correct.</param>
    /// <param name="answerIndex">An integer indicating the index of the answer.</param>
    public void ChangeAnswerBackground(bool correct, int answerIndex)
    {
        switch (answerIndex)
        {
            case 0:
                Answer1Background = correct ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Gold);
                break;

            case 1:
                Answer2Background = correct ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Gold);
                break;

            case 2:
                Answer3Background = correct ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Gold);
                break;

            case 3:
                Answer4Background = correct ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Gold);
                break;
        }
    }

    /// <summary>
    ///     InOrActivateButtons is a method used to set the IsActive property of Answer1, Answer2, Answer3, and Answer4 based
    ///     on the given status value.
    /// </summary>
    public void InOrActivateButtons(bool status)
    {
        Answer1.IsActive = status;
        Answer2.IsActive = status;
        Answer3.IsActive = status;
        Answer4.IsActive = status;
    }

    /// <summary>
    ///     Initializes the answers.
    /// </summary>
    private void InitializeAnswers()
    {
        Answer1 = new Answer { IsActive = false };
        Answer2 = new Answer { IsActive = false };
        Answer3 = new Answer { IsActive = false };
        Answer4 = new Answer { IsActive = false };
    }

    /// <summary>
    ///     Validates the answers for a given question.
    /// </summary>
    /// <param name="currentQuestionIndex">The index of the current question.</param>
    /// <returns>True if the answers are valid, otherwise false.</returns>
    public bool ValidateAnswers(int currentQuestionIndex)
    {
        var question = _questions.ElementAtOrDefault(currentQuestionIndex);

        if (question == null || question.Answers.Count == 0)
        {
            Updated = false;
            _gameRunning = false;
            SetMessage("Elnézést, valamilyen hiba történt, kezdje újra!", "Red");
            return false;
        }

        if (!question.Answers.Any(nswer => false)) return true;
        Updated = false;
        _gameRunning = false;
        SetMessage("Elnézést, valamilyen hiba történt, kezdje újra!", "Red");
        return false;
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