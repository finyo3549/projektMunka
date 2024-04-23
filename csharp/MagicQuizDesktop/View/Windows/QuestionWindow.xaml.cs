using MagicQuizDesktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Input;

namespace MagicQuizDesktop.View.Windows
{
    /// <summary>
    /// Represents a window for displaying questions. This window provides functionalities for dragging,
    /// minimizing, and closing the window.
    /// </summary>
    public partial class QuestionWindow : Window
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionWindow"/> class and sets the data context by fetching an instance of <see cref="QuestionViewModel"/> from the app's service provider. 
        /// </summary>
        public QuestionWindow()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetService<QuestionViewModel>();

        }

        /// <summary>
        /// Handles the MouseDown event of the Window control.
        /// Allows the user to drag the window when the left mouse button is pressed.
        /// </summary>
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Event handler for the click event of the BtnMinimize button. Minimizes the window.
        /// </summary>
        /// <param name="sender">The button click event sender.</param>
        /// <param name="e">The event arguments.</param>
        public void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Handles the Click event of the BtnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
