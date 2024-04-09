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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MagicQuizDesktop.Models;
using MagicQuizDesktop.View.Pages;
using MagicQuizDesktop.ViewModels;

namespace MagicQuizDesktop.View.Windows
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow(User user)
        {
            InitializeComponent();
            var viewModel = new HomeViewModel(HomeFrame, user);
            DataContext = viewModel;            
            HomeFrame.Content = new HomePage(user);
        }


    }
}
