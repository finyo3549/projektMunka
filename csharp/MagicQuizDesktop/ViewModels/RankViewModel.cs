using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MagicQuizDesktop.Commands;
using MagicQuizDesktop.Models;
using MagicQuizDesktop.Repositories;
using MagicQuizDesktop.Services;

namespace MagicQuizDesktop.ViewModels;

/// <summary>
///     Represents a view model for ranking.
/// </summary>
public class RankViewModel : ViewModelBase
{
    /// <summary>
    ///     Represents a readonly instance of a rank repository.
    /// </summary>
    public readonly IRankRepository _rankRepository;

    private User _currentUser;
    private Message _message;
    private string _name;
    private ObservableCollection<Rank> _rankList;
    private List<Rank> _ranks;
    private int _score;
    private int _userId;


    /// <summary>
    ///     Initializes a new instance of the RankViewModel class, setting up the CurrentUser, RankRepository and RankList.
    ///     It also initializes the ResetCommand and UpdateCommand.
    /// </summary>
    public RankViewModel()
    {
        CurrentUser = SessionManager.Instance.CurrentUser;
        _rankRepository = new RankRepository();
        _ranks = [];
        RankList = new ObservableCollection<Rank>(_ranks);
        ResetCommand = new AsyncRelayCommand(async _ => await ResetRankList());
        UpdateCommand = new AsyncRelayCommand(async _ => await UpdateData());
        _ = SetRankOrder();
    }

    /// <summary>
    ///     Gets or sets the current user. This property also triggers an event when the current user property changes.
    /// </summary>
    public User CurrentUser
    {
        get => _currentUser;
        set
        {
            if (_currentUser == value) return;
            _currentUser = value;
            OnPropertyChanged(nameof(CurrentUser));
        }
    }

    /// <summary>
    ///     Gets or sets a list of Rank objects. This list is observed for changes.
    /// </summary>
    public ObservableCollection<Rank> RankList
    {
        get => _rankList;
        set
        {
            _rankList = value;
            OnPropertyChanged(nameof(RankList));
        }
    }

    /// <summary>
    ///     Gets or sets the value for 'Name'. Triggers a property change notification upon setting.
    /// </summary>
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    /// <summary>
    ///     Gets or sets the UserId.
    /// </summary>
    public int UserId
    {
        get => _userId;
        set
        {
            _userId = value;
            OnPropertyChanged(nameof(UserId));
        }
    }

    /// <summary>
    ///     Represents the user in game score with OnPropertyChange event.
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
    ///     Gets or sets the Message object. Changes to the Message property triggers the PropertyChanged event.
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
    ///     Gets the Command for reset the rank.
    /// </summary>
    public ICommand ResetCommand { get; }

    /// <summary>
    ///     Gets the Command for update the rank.
    /// </summary>
    public ICommand UpdateCommand { get; }

    /// <summary>
    ///     Asynchronously updates the data by setting the rank order and a
    ///     success message indicating that the rank list is refreshed.
    /// </summary>
    public async Task UpdateData()
    {
        await SetRankOrder();
        if (_ranks is not null && _ranks.Count > 0) SetMessage("A ranglista naprakész!", "Green");
    }


    /// <summary>
    ///     Asynchronously resets the rank list. If the reset is successful it updates the rank order and sets a success
    ///     message,
    ///     if not it sets a fail message with the status code and response message. In case of any exceptions, it sets a
    ///     failure message containing the exception details.
    /// </summary>
    public async Task ResetRankList()
    {
        Message = new Message();
        try
        {
            var response = await _rankRepository.ResetRanks(CurrentUser.AuthToken);
            if (response.Success)
            {
                SetMessage("A ranglista kiürítve!", "Green");
                await SetRankOrder();
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
    ///     Asynchronous method to get ranks data from the rank repository using current user's authentication token.
    ///     If retrieval is successful, updates the rank list with response data, else, sets an error message.
    /// </summary>
    public async Task GetRanks()
    {
        Message = new Message();
        try
        {
            var response = await _rankRepository.GetRanks(CurrentUser.AuthToken);
            if (response.Success)
            {
                _ranks = response.Data;
                RankList = new ObservableCollection<Rank>(_ranks);
            }
            else
            {
                SetMessage($"{response.StatusCode}: {response.Message}", "Red");
            }
        }
        catch (ArgumentNullException nullException)
        {
            SetMessage($"Null érték: {nullException.ParamName}", "Red");
        }
        catch (Exception ex)
        {
            SetMessage($"Hiba: {ex.Message}", "Red");
        }
    }


    /// <summary>
    ///     Asynchronously sets the rank order of players based on their scores in descending order.
    ///     Assigns a rank number and color to each player. Fill the RankList with such ordered ranks.
    /// </summary>
    public async Task SetRankOrder()
    {
        await GetRanks();
        try
        {
            _ranks = [.. _ranks.OrderByDescending(r => r.Score)];
            for (var i = 0; i < _ranks.Count; i++)
            {
                _ranks[i].RankNumber = i + 1;

                _ranks[i].RankColor = i switch
                {
                    0 => "#FFD700",
                    1 => "#C0C0C0",
                    2 => "#CD7F32",
                    _ => "#07F3C0"
                };
            }

            RankList = new ObservableCollection<Rank>(_ranks);
        }
        catch (ArgumentNullException nullException)
        {
            SetMessage($"Null érték: {nullException.ParamName} Forrás: {nullException.Source}", "Red");
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