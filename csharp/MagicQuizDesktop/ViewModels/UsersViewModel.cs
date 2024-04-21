using MagicQuizDesktop.Commands;
using MagicQuizDesktop.Models;
using MagicQuizDesktop.Repositories;
using MagicQuizDesktop.Services;
using MagicQuizDesktop.View.Windows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Message = MagicQuizDesktop.Models.Message;
using User = MagicQuizDesktop.Models.User;

namespace MagicQuizDesktop.ViewModels;

/// <summary>
///     Represents a view model for managing users.
/// </summary>
public class UsersViewModel : ViewModelBase
{
    /// <summary>
    ///     The base URI where avatar resources are located.
    /// </summary>
    private const string BaseAvatarUri = "/media/";

    /// <summary>
    ///     Represents a user repository.
    /// </summary>
#pragma warning disable CA1859
    private readonly IUserRepository _userRepository;
#pragma warning restore CA1859

    /// <summary>
    ///     Properties
    /// </summary>
    private User _currentUser;

    private string _isActiveActionText;
    private Message _message;
    private User _selectedUser;
    private string _selectedUserActiveText;
    private string _selectedUserColor;
    private List<User> _users;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UsersViewModel" /> class.
    /// </summary>
    public UsersViewModel()
    {
        CurrentUser = SessionManager.Instance.CurrentUser;
        InitializeProperties();
        Genders = ["férfi", "nő", "egyéb"];
        _userRepository = new UserRepository();
        ShowUserWindowCommand = new RelayCommand(ExecuteShowUserWindowCommand);
        SubmitProfileCommand = new AsyncRelayCommand(_ => UpdateSelectedUserAsync());
        DeOrActivateCommand = new AsyncRelayCommand(_ => DeOrActivateSelectedUserAsync());
    }

    public User CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            OnPropertyChanged(nameof(CurrentUser));
        }
    }

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

    public string SelectedUserActiveText
    {
        get => _selectedUserActiveText;
        set
        {
            _selectedUserActiveText = value;
            OnPropertyChanged(nameof(SelectedUserActiveText));
        }
    }

    public string SelectedUserColor
    {
        get => _selectedUserColor;
        set
        {
            _selectedUserColor = value;
            OnPropertyChanged(nameof(SelectedUserColor));
        }
    }

    public string IsActiveActionText
    {
        get => _isActiveActionText;
        set
        {
            _isActiveActionText = value;
            OnPropertyChanged(nameof(IsActiveActionText));
        }
    }

    public List<User> Users
    {
        get => _users;
        set
        {
            if (_users != value)
            {
                _users = value;
                SetUserGendersAndAvatars(_users);
                OnPropertyChanged(nameof(Users));
            }
        }
    }

    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            if (_selectedUser != value)
            {
                _selectedUser = value;
                SetUserGenderAndAvatar();
                OnPropertyChanged(nameof(SelectedUser));
                UpdateActiveStatusProperties(_selectedUser.IsActive);
            }
        }
    }

    public List<string> Genders { get; }


    /// <summary>
    ///     Gets the command for showing the user window.
    /// </summary>
    public ICommand ShowUserWindowCommand { get; }

    /// <summary>
    ///     Gets the command to submit the user data.
    /// </summary>
    public ICommand SubmitProfileCommand { get; }

    /// <summary>
    ///     Gets the command that is used to de-activate or activate the selected user.
    /// </summary>
    public ICommand DeOrActivateCommand { get; }

    /// <summary>
    ///     Initializes the method asynchronously for the users page.
    /// </summary>
    public async Task InitializeAsync()
    {
        await GetUsers();
        SelectedUser = new User();
    }

    /// <summary>
    ///     Initializes the class properties to their default values.
    ///     It sets _message to a new Message, _isActiveActionText, _selectedUserActiveText,
    ///     and _selectedUserColor to an empty string.
    ///     A new User is also created for _selectedUser, and _users is set to an empty array.
    /// </summary>
    private void InitializeProperties()
    {
        _message = new Message();
        _isActiveActionText = string.Empty;
        _selectedUser = new User();
        _selectedUserActiveText = string.Empty;
        _selectedUserColor = string.Empty;
        _users = [];
    }

    /// <summary>
    ///     Updates the active status of properties based on the specified boolean value.
    ///     To handle the user active status
    /// </summary>
    /// <param name="isActive">Specifies whether the item is active or not.</param>
    private void UpdateActiveStatusProperties(bool isActive)
    {
        SelectedUserActiveText = isActive ? "Aktív" : "Inaktív";
        SelectedUserColor = isActive ? "Green" : "Red";
        _isActiveActionText = isActive ? "Deaktiválás" : "Aktiválás";

        OnPropertyChanged(nameof(SelectedUserActiveText));
        OnPropertyChanged(nameof(SelectedUserColor));
        OnPropertyChanged(nameof(IsActiveActionText));
    }

    /// <summary>
    ///     Activate or deactivate the selected user asynchronously.
    /// </summary>
    public async Task DeOrActivateSelectedUserAsync()
    {
        if (SelectedUser.IsActive)
        {
            try
            {
                var response = await _userRepository.Inactivate(SelectedUser, CurrentUser.AuthToken);
                if (response.Success)
                    SetMessage("Felhasználó sikeresen deaktiválva!", "Green");
                else
                    SetMessage($"{response.StatusCode}: {response.Message}", "Red");
            }
            catch (Exception ex)
            {
                SetMessage($"{ex.Message}", "Red");
            }
        }
        else
        {
            if (!ValidateProfileFields()) return;
            try
            {
                SelectedUser.IsActive = true;
                SetUserGenderAndAvatar();
                var response = await _userRepository.UpdateUser(SelectedUser, CurrentUser.AuthToken);
                if (response.Success)
                    SetMessage("Felhasználó sikeresen aktiválva!", "Green");
                else
                    SetMessage($"{response.StatusCode}: {response.Message}", "Red");
            }
            catch (Exception ex)
            {
                SetMessage($"{ex.Message}", "Red");
            }
        }
    }

    /// <summary>
    ///     Retrieves the list of users asynchronously.
    ///     If successful, the retrieved users are assigned to the 'Users' property.
    ///     If an error occurs, the error message is assigned to the 'ErrorMessage' property.
    /// </summary>
    public async Task GetUsers()
    {
        try
        {
            var response = await _userRepository.GetByAll(CurrentUser.AuthToken);
            if (response.Success)
            {
                Users = response.Data;
                Message = new Message();
            }
            else
            {
                SetMessage($"{response.StatusCode}:{response.Message}", "Red");
            }
        }
        catch (Exception ex)
        {
            SetMessage($"{ex.Message}", "Red");
        }
    }

    /// <summary>
    ///     Executes the command to show the user window.
    /// </summary>
    /// <param name="param">The parameter representing the user object.</param>
    private void ExecuteShowUserWindowCommand(object param)
    {
        if (param is User user)
        {
            SelectedUser = user;
            ProfileWindow window = new() { DataContext = this };
            window.Closed += (sender, args) =>
            {
                SelectedUser = new User();
                _ = InitializeAsync();
                Message = new Message();
            };
            window.ShowDialog();
        }
        else
        {
            SetMessage("Hoppá! A megadott objektum nem érvényes felhasználó!", "Red");
        }
    }

    /// <summary>
    ///     Updates the selected user asynchronously.
    /// </summary>
    public async Task UpdateSelectedUserAsync()
    {
        if (!ValidateProfileFields()) return;
        try
        {
            SetUserGenderAndAvatar();
            var response = await _userRepository.UpdateUser(SelectedUser, CurrentUser.AuthToken);
            if (response.Success)
                SetMessage("Sikeres módosítás!!", "Green");
            else
                SetMessage($"{response.StatusCode}: {response.Message}", "Red");
        }
        catch (Exception ex)
        {
            SetMessage($"Hiba: {ex.Message}", "Red");
        }
    }

    /// <summary>
    ///     Validates the profile fields.
    /// </summary>
    /// <returns>Returns true if all profile fields are valid; otherwise, false.</returns>
    private bool ValidateProfileFields()
    {
        if (string.IsNullOrEmpty(SelectedUser.Name))
        {
            SetMessage("A név megadása kötelező!", "Red");
            return false;
        }

        if (string.IsNullOrEmpty(SelectedUser.Email))
        {
            SetMessage("Az e-mail cím megadása kötelező!", "Red");
            return false;
        }

        if (!string.IsNullOrEmpty(SelectedUser.Gender)) return true;
        SetMessage("A nem megadása kötelező!", "Red");
        return false;
    }

    /// <summary>
    ///     Sets the gender of the selected user based on the provided value from the view.
    ///     Updates the user's gender and avatar accordingly.
    /// </summary>
    public void SetUserGenderAndAvatar()
    {
        var lowerGender = SelectedUser.Gender.ToLower();
        switch (lowerGender)
        {
            case "férfi":
            case "male":
                SelectedUser.Gender = "férfi";
                SelectedUser.Avatar = $"{BaseAvatarUri}male_avatar.png";
                break;
            case "nő":
            case "female":
                SelectedUser.Gender = "nő";
                SelectedUser.Avatar = $"{BaseAvatarUri}female_avatar.png";
                break;
            default:
                SelectedUser.Gender = "egyéb";
                SelectedUser.Avatar = $"{BaseAvatarUri}unknown_avatar.png";
                break;
        }

        OnPropertyChanged(nameof(SelectedUser.Gender));
        OnPropertyChanged(nameof(SelectedUser.Avatar));
    }

    /// <summary>
    ///     Sets avatars for a list of users based on their gender.
    ///     <para>If the gender is "férfi" or "male", sets the user's gender to "férfi" and assigns the male avatar.</para>
    ///     <para>If the gender is "nő" or "female", sets the user's gender to "nő" and assigns the female avatar.</para>
    ///     <para>Otherwise, sets the user's gender to "egyéb" and assigns the unknown avatar.</para>
    /// </summary>
    private static void SetUserGendersAndAvatars(List<User> users)
    {
        foreach (var user in users)
        {
            var lowerGender = user.Gender.ToLower();
            switch (lowerGender)
            {
                case "férfi":
                case "male":
                    user.Gender = "férfi";
                    user.Avatar = $"{BaseAvatarUri}male_avatar.png";
                    break;
                case "nő":
                case "female":
                    user.Gender = "nő";
                    user.Avatar = $"{BaseAvatarUri}female_avatar.png";
                    break;
                default:
                    user.Gender = "egyéb";
                    user.Avatar = $"{BaseAvatarUri}unknown_avatar.png";
                    break;
            }
        }
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