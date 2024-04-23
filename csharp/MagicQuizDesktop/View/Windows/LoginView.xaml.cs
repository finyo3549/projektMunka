using System.Windows;
using System.Windows.Input;
using MagicQuizDesktop.ViewModels;

namespace MagicQuizDesktop.View.Windows;

/// <summary>
///     Represents a window for user login. Allows user to input credentials and contains the functionality for minimizing,
///     closing and interacting with the window.
/// </summary>
public partial class LoginView
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LoginView" /> class.
    /// </summary>
    public LoginView()
    {
        InitializeComponent();
    }

    /// <summary>
    ///     Handles the MouseDown event of the Window control. Initiates the drag move operation if the left button is pressed.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
    public void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed) DragMove();
    }

    /// <summary>
    ///     Handles the Click event of the btnMinimize control. Minimizes the window.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    public void BtnMinimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    /// <summary>
    ///     Handles the Click event of the "Close" button control. Shuts down the current application.
    /// </summary>
    public void BtnClose_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    /// <summary>
    ///     Handles the PasswordChanged event of the PasswordBox control.
    ///     Transfers the new password to the bounded LoginViewModel.
    /// </summary>
    public void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel viewModel) viewModel.Password = PasswordData.Password;
    }
}