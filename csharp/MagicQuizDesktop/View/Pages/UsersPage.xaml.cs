using MagicQuizDesktop.ViewModels;
using System.Windows.Controls;

namespace MagicQuizDesktop.View.Pages
{
    /// <summary>
    /// A partial class 'UsersPage' that inherits from the class 'Page'. It primarily
    /// initializes the UsersViewModel and sets its DataContext during the loading phase.
    /// </summary>
    public partial class UsersPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersPage"/> class.
        /// Also, it sets the Load event to initialize the DataContext as an instance of UsersViewModel.
        /// </summary>
        public UsersPage()
        {
            InitializeComponent();
            Loaded += async (s, e) => await ((UsersViewModel)DataContext).InitializeAsync();
        }

    }
}
