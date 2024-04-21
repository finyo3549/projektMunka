using MagicQuizDesktop.Commands;
using MagicQuizDesktop.Models;
using MagicQuizDesktop.Services;
using MagicQuizDesktop.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MagicQuizDesktop.ViewModels;

/// <summary>
///     Represents a view model for the Topic page.
/// </summary>
/// <remarks>
///     This view model is used to handle the logic and data operations
///     for the Topic page and window in the application.
/// </remarks>
public class TopicViewModel : ViewModelBase
{
    /// <summary>
    ///     Interfaces
    /// </summary>
    private readonly ITopicRepository _topicRepository;

    /// <summary>
    ///     Private Fields
    /// </summary>
    private User _currentUser;

    private ObservableCollection<Topic> _filteredTopics;
    private Message _message;
    private string _searchText;
    private Topic _selectedTopic;
    private string _topicName;
    private List<Topic> _topics;



    /// <summary>
    /// Initializes a new instance of the <see cref="TopicViewModel"/> class.
    /// Gets the current user and topics, initializes commands, and calls the Initialize function.
    /// </summary>
    /// <param name="topicRepository">The repository that will be used to manage topics.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="topicRepository"/> is null.</exception>
    public TopicViewModel(ITopicRepository topicRepository)
    {
        Topics = new List<Topic>();
        _topicRepository = topicRepository ?? throw new ArgumentNullException(nameof(topicRepository));
        CurrentUser = SessionManager.Instance.CurrentUser;
        InitializeCommands();
        _ = GetTopics();
        Initialize();
    }

    /// <summary>
    ///     Public Fields
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
            }
        }
    }

    /// <summary>
    /// Gets or sets the SearchText property. The property-change notification is handled automatically.
    /// If the SearchText is null or empty, the default settings are reset.
    /// </summary>
    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged(nameof(SearchText));

            if (string.IsNullOrEmpty(SearchText)) ResetToDefault();
        }
    }

    public List<Topic> Topics
    {
        get => _topics;
        set
        {
            _topics = value;
            OnPropertyChanged(nameof(Topics));
        }
    }

    public Topic SelectedTopic
    {
        get => _selectedTopic;
        set
        {
            _selectedTopic = value;
            OnPropertyChanged(nameof(SelectedTopic));
        }
    }


    /// <summary>
    ///     Inform the user if the request was succesful
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

    public string TopicName
    {
        get => _topicName;
        set
        {
            if (_topicName == value) return;
            _topicName = value;
            OnPropertyChanged(nameof(TopicName));
        }
    }

    /// <summary>
    /// Gets or sets the collection of filtered topics.
    /// This property does utilize the OnPropertyChanged method to notify when the collection has been changed.
    /// </summary>
    public ObservableCollection<Topic> FilteredTopics
    {
        get => _filteredTopics;
        set
        {
            _filteredTopics = value;
            OnPropertyChanged(nameof(FilteredTopics));
        }
    }

    /// <summary>
    ///     Represents a command for searching.
    /// </summary>
    public ICommand SearchCommand { get; private set; }

    /// <summary>
    ///     Gets the command for updating datas.
    /// </summary>
    public ICommand UpdateDataCommand { get; private set; }

    /// <summary>
    ///     Gets or sets the command used to open a topic window.
    /// </summary>
    public ICommand OpenTopicWindowCommand { get; private set; }

    /// <summary>
    ///     Gets or sets the command to open a new topic window.
    /// </summary>
    public ICommand OpenNewTopicWindowCommand { get; private set; }

    /// <summary>
    ///     Gets the command that is executed when a topic is submitted.
    /// </summary>
    public ICommand SubmitTopicCommand { get; private set; }

    /// <summary>
    ///     Gets or sets the command to delete a topic.
    /// </summary>
    public ICommand DeleteCommand { get; private set; }



    /// <summary>
    /// Asynchronously retrieves a list of topics from the Topic Repository. 
    /// If successful, it populates the Topics and FilteredTopics properties, 
    /// else an empty Observable Collection is returned. 
    /// Any exceptions encountered are caught and an error message is set up.
    /// </summary>
    public async Task GetTopics()
    {
        try
        {
            var topicResponse = await _topicRepository.GetByAll(CurrentUser.AuthToken);

            if (topicResponse.Success)
            {
                Topics = topicResponse.Data;
                FilteredTopics = new ObservableCollection<Topic>(Topics);
            }
            else
            {
                FilteredTopics = [];
                SetMessage($"{topicResponse.Message}: {topicResponse.Message}", "Red");
            }
        }
        catch (Exception ex)
        {
            FilteredTopics = [];
            SetMessage($"Hiba: {ex.Message}", "Red");
        }
    }


    /// <summary>
    ///     Reset the list used by the view to
    ///     update the user interface
    /// </summary>
    private void ResetToDefault()
    {
        _ = GetTopics();
    }

    /// <summary>
    ///     Get the topic with a specific topic name
    /// </summary>
    private void PerformSearch()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            SetMessage($"A keresési szöveg nem lehet üres!", "Red");
            return;
        }

        var relevantTopic = Topics
            .FirstOrDefault(t =>
                t.TopicName.Equals(SearchText, StringComparison.OrdinalIgnoreCase));

        if (relevantTopic == null)
        {
            SetMessage($"Nincs ilyen téma!", "Red");
            FilteredTopics.Clear();
            return;
        }

        FilteredTopics.Clear();
        FilteredTopics.Add(relevantTopic);
    }

    /// <summary>
    ///     Open a new TopicWindow to update selected topic
    /// </summary>
    /// <param name="param">Selected topic as a Topic object</param>
    private void PerformOpenTopicWindow(object param)
    {
        if (param is Topic topic)
        {
            SelectedTopic = topic;
            Initialize();
            TopicWindow window = new() { DataContext = this };
            window.Closed += (sender, args) =>
            {
                Message = new Message();
                ResetToDefault();
            };
            window.Show();
        }
        else
        {
            SetMessage($"Hoppá nem megfelelő objektum!", "Red");
        }
    }

    /// <summary>
    ///     Open a new TopicWindow to add a new Question
    /// </summary>
    private void PerformOpenNewTopicWindow()
    {
        TopicWindow window = new();
        window.Closed += (sender, args) =>
        {
            Message = new Message();
            Initialize();
            ResetToDefault();
        };
        window.Show();
    }


    /// <summary>
    /// Initializes the instance by setting the TopicName according to the SelectedTopic. 
    /// If the SelectedTopic is a new Topic, it sets up a new Topic for SelectedTopic
    /// and sets an empty string for TopicName.
    /// </summary>
    private void Initialize()
    {
        if (SelectedTopic != new Topic() && SelectedTopic is not null)
        {
            
            TopicName = SelectedTopic.TopicName;
        }
        else
        {
            SelectedTopic = new Topic();
            TopicName = string.Empty;
        }
    }

    /// <summary>
    ///     Initialize Commands
    /// </summary>
    private void InitializeCommands()
    {
        UpdateDataCommand = new AsyncRelayCommand(async _ => await GetTopics());
        OpenTopicWindowCommand = new RelayCommand(PerformOpenTopicWindow);
        OpenNewTopicWindowCommand = new RelayCommand(_ => PerformOpenNewTopicWindow());
        SubmitTopicCommand = new AsyncRelayCommand(async _ => await TopicCommand());
        DeleteCommand = new AsyncRelayCommand(async _ => await DeleteTopic(), CanExecuteCommand);
        SearchCommand = new RelayCommand(_ => PerformSearch());
    }


    /// <summary>
    /// Asynchronously performs the Topic command. If the selected topic is valid and has an ID higher than 0,
    /// it tries to update the topic. Otherwise, it tries to add the topic. It handles any exceptions
    /// and updates the appropriate message or error message fields, and resets the selected topic and other properties.
    /// </summary>
    public async Task TopicCommand()
    {
        Message = new Message();
        if (SelectedTopic != new Topic() && SelectedTopic.Id > 0)
        {
            if (!MakeTopicObject()) return;
            try
            {
                var topicResponse = await _topicRepository.UpdateAsync(SelectedTopic, CurrentUser.AuthToken);

                if (topicResponse.Success)
                {
                    SetMessage($"Téma sikeresen frissítve!", "Green");
                    SelectedTopic = new Topic();
                }
                else
                {
                    SetMessage($"{topicResponse.StatusCode}: {topicResponse.Message}", "Red");
                }
            }
            catch (Exception ex)
            {
                SetMessage($"Hiba: {ex.Message}", "Red");
            }
        }
        else
        {
            if (!MakeTopicObject()) return;

            try
            {
                var topicResponse = await _topicRepository.AddAsync(SelectedTopic, CurrentUser.AuthToken);
                if (topicResponse.Success)
                {
                    SetMessage($"Téma sikeresen felvéve!", "Green");
                }
                else
                {
                    SetMessage($"{topicResponse.StatusCode}: {topicResponse.Message}", "Red");
                }
            }
            catch (Exception ex)
            {
                SetMessage($"Hiba: {ex.Message}", "Red");
            }
        }
    }


    /// <summary>
    /// Checks if TopicName is null or empty. If it is, sets a message and returns false. Otherwise, gets the TopicId by the TopicName. 
    /// If TopicId is not found (equals 0), sets the TopicName. If TopicId is found, sets both the TopicName and Id of SelectedTopic. 
    /// Returns true at the end of the method.
    /// </summary>
    private bool MakeTopicObject()
    {
        if (string.IsNullOrEmpty(TopicName))
        {
            SetMessage($"A téma megadása kötelező!", "Red");
            return false;
        }

        var topicId = GetTopicIdByName(TopicName);

        if (topicId == 0)
        {
            SelectedTopic.TopicName = TopicName;
        }
        else
        {
            SelectedTopic.TopicName = TopicName;
            SelectedTopic.Id = topicId;
        }

        return true;
    }

    /// <summary>
    /// Gets the ID of the topic by its name.
    /// </summary>
    /// <param name="topicName">The name of the topic.</param>
    /// <returns>Returns the ID of the topic.</returns>
    private int GetTopicIdByName(string topicName)
    {
        var topicId = Topics
            .Where(t => t.TopicName.Contains(topicName, StringComparison.OrdinalIgnoreCase))
            .Select(t => t.Id)
            .FirstOrDefault();

        return topicId;
    }


    /// <summary>
    /// Asynchronously deletes a topic by its name.
    /// If successful, it resets the selected topic and state. If an error occurs during the deletion, it sets error messages accordingly.
    /// </summary>
    public async Task DeleteTopic()
    {
        Message = new Message();
        if (string.IsNullOrEmpty(TopicName))
        {
            SetMessage($"A téma megadása kötelező!", "Red");
            return;
        }

        var topicId = GetTopicIdByName(TopicName);

        if (topicId == 0)
        {
            SetMessage($"Nincs ilyen téma!", "Red");
            return;
        }

        try
        {
            var response = await _topicRepository.DeleteAsync(topicId, CurrentUser.AuthToken);
            if (response.Success)
            {
                SetMessage($"A téma sikeresen törölve", "Green");
                SelectedTopic = new Topic();
                ResetToDefault();
            }
            else
            {
                SetMessage($"{response.StatusCode}: {response.Message}", "Red");
            }
        }
        catch (Exception ex)
        {
            SetMessage($"Hiba: {ex.Message}", "Red");
        }
    }


    /// <summary>
    /// Determines whether the command can be executed based on certain conditions.
    /// It checks if the "TopicName" is not null or white space, and if the Id of "SelectedTopic" is greater than 0.
    /// </summary>
    /// <param name="obj">The object under consideration for executing the command.</param>
    /// <returns>Returns a boolean value indicating whether the command can be executed or not.</returns>
    private bool CanExecuteCommand(object obj)
    {
        return !string.IsNullOrWhiteSpace(TopicName) && SelectedTopic.Id > 0;
    }

    /// <summary>
    ///     Sets a message with the specified text and color.
    /// </summary>
    /// <param name="text">The text of the message.</param>
    /// <param name="color">The color of the message.</param>
    private void SetMessage(string text, string color)
    {
        Message.MessageText = text;
        Message.MessageColor = color;
    }
}