using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MagicQuizDesktop.View.UserControls;
using MagicQuizDesktop.View.Windows;
using MagicQuizDesktop.ViewModels;

namespace MagicQuizDesktop.View.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {        
            InitializeComponent();
            DataContext = new LoginViewModel ();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            App.NavigateToPage("Welcome");
        }
    }
}
