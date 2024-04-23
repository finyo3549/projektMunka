using System.Threading.Tasks;
using System.Windows.Input;
using MagicQuizDesktop.Commands;
using MagicQuizDesktop.Models;
using MagicQuizDesktop.Repositories;
using MagicQuizDesktop.Services;
using MagicQuizDesktop.View.Windows;

namespace MagicQuizDesktop.ViewModels;

/// <summary>
///     Represents a view model for logging in.
/// </summary>
public class LoginViewModel : ViewModelBase
{
    /// <summary>
    ///     Represents a read-only instance of an IUserRepository.
    /// </summary>
    public readonly IUserRepository _userRepository;

    private string _email;

    private string _errorMessage = string.Empty;

    private string _password;

    private bool _visible = true;

    /// <summary>
    ///     Initializes a new instance of the LoginViewModel class.
    ///     This constructor also initializes _userRepository and LoginCommand.
    ///     _userRepository is constructed as a new instance of UserRepository
    ///     and LoginCommand is constructed as a new instance of AsyncRelayCommand,
    ///     passing ExecuteLoginCommand as the execution target and CanExecuteLoginCommand as the execution condition.
    /// </summary>
    public LoginViewModel()
    {
        _userRepository = new UserRepository();
        LoginCommand = new AsyncRelayCommand(ExecuteLoginCommand, _ => CanExecuteLoginCommand());
    }

    /// <summary>
    ///     Gets or sets the email. When setting, it raises the PropertyChanged event if the new value is different from the
    ///     old one.
    /// </summary>
    public string Email
    {
        get => _email;
        set
        {
            if (_email == value) return;
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    /// <summary>
    ///     Represents a password that notifies when the value has changed.
    /// </summary>
    public string Password
    {
        get => _password;
        set
        {
            if (_password == value) return;
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    /// <summary>
    ///     Gets or sets the error message.
    /// </summary>
    public string ErrorMessage
    {
        get => _errorMessage;

        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    /// <summary>
    ///     Gets or sets the visibility of the LoginView
    ///     This property will also trigger a PropertyChanged event when the value changes.
    /// </summary>
    public bool Visible
    {
        get => _visible;

        set
        {
            _visible = value;
            OnPropertyChanged(nameof(Visible));
        }
    }

    /// <summary>
    ///     Gets or sets the login command.
    /// </summary>
    public ICommand LoginCommand { get; private set; }


    /// <summary>
    ///     Validates the login input. It checks if email and password fields are not empty,
    ///     if email structure is valid, and if the password has the minimum required length.
    /// </summary>
    public bool ValidateLoginInput()
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            ErrorMessage = "Az e-mail cím megadása kötelező.";
            return false;
        }

        if (!Email.Contains('@') || !Email.Contains("."))
        {
            ErrorMessage = "Érvénytelen e-mail cím.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "A jelszó megadása kötelező.";
            return false;
        }

        if (Password.Length >= 8) return true;
        ErrorMessage = "A jelszónak legalább 8 karakter hosszúnak kell lennie.";
        return false;
    }


    /// <summary>
    ///     Executes the login command, validates input, makes a request to authenticate the user, and then sets the user if
    ///     the authentication is successful.
    ///     If the authentication or user retrieval is not successful, it stores the response message in an error message.
    /// </summary>
    /// <param name="obj">The object to execute the login command on. Not actually used in the method.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task ExecuteLoginCommand(object obj)
    {
        if (!ValidateLoginInput()) return;

        var loginResponse = await _userRepository.AuthenticateUser(Email, Password);
        if (loginResponse.Success && !string.IsNullOrEmpty(loginResponse.Data.Token))
        {
            var userResponse = await _userRepository.GetById(loginResponse.Data.UserId, loginResponse.Data.Token);
            if (userResponse.Success)
            {
                userResponse.Data.AuthToken = loginResponse.Data.Token;
                SessionManager.Instance.SetCurrentUser(userResponse.Data);
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Visible = false;
            }
            else
            {
                ErrorMessage = userResponse.Message;
            }
        }
        else
        {
            ErrorMessage = loginResponse.Message;
        }
    }

    /// <summary>
    ///     Determines whether the login command can be executed.
    /// </summary>
    /// <returns><c>true</c> if the login command can be executed; otherwise, <c>false</c>.</returns>
    public bool CanExecuteLoginCommand()
    {
        return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
    }
}