using MagicQuizDesktop.Models;
using MagicQuizDesktop.View.Pages;
using MagicQuizDesktop.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MagicQuizDesktop.ViewModels
{
    internal class HomeViewModel
    {
        public ICommand NavigateToProfileCommand { get; }
        public ICommand NavigateToHomeCommand { get; }
        public ICommand OpenGameWindowCommand { get; }

        public HomeViewModel(Frame navigationFrame, User user)
        {
            NavigateToProfileCommand = new RelayCommand(_ => NavigateToProfile(navigationFrame, user));
            NavigateToHomeCommand = new RelayCommand(_ => NavigateToHome(navigationFrame, user));   
        }

        public HomeViewModel(User user)
        {
            OpenGameWindowCommand = new RelayCommand(_ => OpenGameWindow(user));
        }


        private void NavigateToProfile(Frame navigationFrame, User user)
        { 
            navigationFrame.Navigate(new ProfilePage(user));
        }

        private void NavigateToHome(Frame navigationFrame, User user)
        {
            navigationFrame.Navigate(new HomePage(user));
        }

        private void OpenGameWindow(User user)
        {
            GameWindow gameWindow = new GameWindow(user);
            gameWindow.ShowDialog();
        }

    }
}
