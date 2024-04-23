using MagicQuizDesktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Input;

namespace MagicQuizDesktop.View.Windows
{
    /// <summary>
    /// Represents a window for displaying topic information.
    /// It is constructed using the TopicViewModel and provides support for window interactions 
    /// such as moving the window on mouse down event and minimizing/closing the window on button click events.
    /// </summary>
    public partial class TopicWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TopicWindow"/> class.
        /// </summary>
        public TopicWindow()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetService<TopicViewModel>();
        }

        /// <summary>
        /// Handles the MouseDown event of the Window control. If the left mouse button is pressed, the window will move following the cursor.
        /// </summary>
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Handles the event when the Minimize button is clicked by minimizing the window's state.
        /// </summary>
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
