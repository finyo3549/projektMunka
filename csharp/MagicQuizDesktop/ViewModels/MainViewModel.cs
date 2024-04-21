using FontAwesome.Sharp;
using MagicQuizDesktop.Commands;
using MagicQuizDesktop.Models;
using MagicQuizDesktop.Repositories;
using MagicQuizDesktop.Services;
using MagicQuizDesktop.View.Pages;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MagicQuizDesktop.ViewModels
{
    /// <summary>
    /// Represents the main view model for the application.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The current child view.
        /// </summary>
        private Page _currentChildView;
        /// <summary>
        /// Represents a caption.
        /// </summary>
        private string _caption;

        /// <summary>
        /// Represents an icon.
        /// </summary>
        private IconChar _icon;
        /// <summary>
        /// The repository for managing user data.
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        /// <summary>
        /// Represents the current user.
        /// </summary>
        private User _currentUser;

        /// <summary>
        /// Gets or sets the command used to load user data.
        /// </summary>
        public ICommand LoadUserDataCommand { get; private set; }
        /// <summary>
        /// Gets the command to show the home view.
        /// </summary>
        public ICommand ShowHomeViewCommand { get; private set; }
        /// <summary>
        /// The command that shows the profile view.
        /// </summary>
        public ICommand ShowProfileViewCommand { get; private set; }
        /// <summary>
        /// Gets the command used to show the game view.
        /// </summary>
        public ICommand ShowGameViewCommand { get; private set; }
        /// <summary>
        /// Gets or sets the command used to show the users view.
        /// </summary>
        public ICommand ShowUsersViewCommand { get; private set; }
        /// <summary>
        /// Gets or sets the command to show the topic view.
        /// </summary>
        public ICommand ShowTopicViewCommand { get; private set; }
        /// <summary>
        /// Gets or sets the command to show the question view.
        /// </summary>
        public ICommand ShowQuestionViewCommand { get; private set; }
        /// <summary>
        /// Gets or sets the command to show the rank view. 
        /// </summary>
        public ICommand ShowRankViewCommand { get; private set; }
        /// <summary>
        /// Gets or sets the command for logging out the current user.
        /// </summary>
        public ICommand LogOutCurrentUserCommand { get; private set; }

        /// <summary>
        /// Gets or sets the current child view.
        /// </summary>
        public Page CurrentChildView
        {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        /// <summary>
        /// Gets or sets the caption for the current page.
        /// </summary>
        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        /// <summary>
        /// Gets or sets the IconChar of the object for the caption.
        /// </summary>
        public IconChar Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            CurrentUser = SessionManager.Instance.CurrentUser;
            InitializeCommands();
            ExecuteShowHomeViewCommand();
        }

        /// <summary>
        /// Initializes the commands.
        /// </summary>
        private void InitializeCommands()
        {
            _userRepository = new UserRepository();
            ShowHomeViewCommand = new RelayCommand(_ => ExecuteShowHomeViewCommand());
            ShowUsersViewCommand = new RelayCommand(_ => ExecuteShowUsersViewCommand());
            ShowTopicViewCommand = new RelayCommand(_ => ExecuteShowTopicViewCommand());
            ShowQuestionViewCommand = new RelayCommand(_ => ExecuteShowQuestionViewCommand());
            ShowRankViewCommand = new RelayCommand(_ => ExecuteShowRankViewCommand());
            LogOutCurrentUserCommand = new AsyncRelayCommand(async _ => await LogOutCurrentUser());
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
        private async Task LogOutCurrentUser()
        {
            ApiResponseWithNoData response = await _userRepository.LogOut(CurrentUser.AuthToken);
            if (response.Success)
            {
                SessionManager.Instance.ClearCurrentUser();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            else
            {
                StringBuilder result = new();
                result.AppendLine("Kijelentkezés sikertelen:");
                result.AppendLine(response.Message);
                result.AppendLine("Bezárja az alkalmazást?");

                var answer = MessageBox.Show(result.ToString(), "Hiba", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }
        }

        /// <summary>
        /// Executes the command to show the topic view.
        /// </summary>
        private void ExecuteShowTopicViewCommand()
        {
            CurrentChildView = new TopicPage();
            Caption = "Témák";
            Icon = IconChar.Earth;
        }

        /// <summary>
        /// Executes the show rank view command. Sets the current child view to a new RankPage,
        /// sets the caption to "Ranglista", and sets the icon to IconChar.RankingStar.
        /// The other ExecuteShow command doe the same with different parameters.
        /// </summary>
        private void ExecuteShowRankViewCommand()
        {
            CurrentChildView = new RankPage();
            Caption = "Ranglista";
            Icon = IconChar.RankingStar;
        }

        /// <summary>
        /// Executes the command to show the question page.
        /// </summary>
        private void ExecuteShowQuestionViewCommand()
        {
            CurrentChildView = new QuestionsPage();
            Caption = "Profil";
            Icon = IconChar.UserAlt;
        }

        /// <summary>
        /// Executes the command to show the home page.
        /// </summary>
        private void ExecuteShowHomeViewCommand()
        {
            CurrentChildView = new HomePage();
            Caption = "Kezdőlap";
            Icon = IconChar.Home;
        }

        /// <summary>
        /// Executes the command to show the users page.
        /// </summary>
        private void ExecuteShowUsersViewCommand()
        {
            CurrentChildView = new UsersPage();
            Caption = "Felhasználók";
            Icon = IconChar.UserGroup;
        }
    }
}