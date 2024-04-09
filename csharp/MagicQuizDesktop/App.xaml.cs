using MagicQuizDesktop.View.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MagicQuizDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Dictionary<string, Uri> PageUris { get; } = new Dictionary<string, Uri>
    {
        { "Welcome", new Uri("View/Pages/WelcomePage.xaml", UriKind.Relative) },
        { "Login", new Uri("View/Pages/LoginPage.xaml", UriKind.Relative) },
        // További oldalak hozzáadása
    };

        public static void NavigateToPage(string pageName)
        {
            if (Application.Current.MainWindow != null && PageUris.TryGetValue(pageName, out Uri uri))
            {
                ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(uri);
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainWindow();
                    mainView.Show();
                    loginView.Close();
                }
            };
        }
    }
}
