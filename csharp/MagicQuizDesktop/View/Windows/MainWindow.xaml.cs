using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace MagicQuizDesktop.View.Windows
{
    /// <summary>
    /// Represents the MainWindow class, which includes methods that interact with window controls (minimize, maximize, mouse interactions)
    /// and user32.dll library for sending commands to the operating system.
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sends a message to a window or windows. Calls the window procedure for the specified window and does not return until the window procedure has processed the message.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window procedure receives the message.</param>
        /// <param name="wMsg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        /// <summary>
        /// Handles the MouseLeftButtonDown event of the pnlControlBar control. 
        /// Allows the window to be moved by dragging the control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The MouseButtonEventArgs instance containing the event data.</param>
        public void PnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        /// <summary>
        /// Handles the Click event of the btnMinimize button, changing the window state to Minimized.
        /// </summary>
        public void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// Handles the Click event of the Maximize button. Toggles the window state between Normal and Maximized.
        /// </summary>
        public void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        ///// <summary>
        ///// Handles the Click event of the BtnClose control. Shuts down the application when the button is clicked.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        //private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
    }
}

