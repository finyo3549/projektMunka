using System.Windows;
using System.Windows.Input;

namespace MagicQuizDesktop.View.Windows
{

    /// <summary>
    /// Represents a window which provides user profile functionality.
    /// </summary>
    public partial class ProfileWindow
    {
        /// <summary>
        /// Initializes a new instance of the ProfileWindow class.
        /// </summary>
        public ProfileWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the MouseDown event of the Window. If the left button was pressed, it initiates the window drag operation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Handles the Click event of the Minimize button.
        /// Changes the WindowState to minimized when the button is clicked.
        /// </summary>
        public void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Handles the Click event of the Close Button control. 
        /// Closes the current window.
        /// </summary>
        public void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
