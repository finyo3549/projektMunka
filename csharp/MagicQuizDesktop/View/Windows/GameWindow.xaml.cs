using System;
using System.Windows;
using System.Windows.Input;
using MagicQuizDesktop.ViewModels;

namespace MagicQuizDesktop.View.Windows;

/// <summary>
///     Represents a game window. Provides methods to handle events such as mouse clicks, minimization, closing and window
///     activation/deactivation.
/// </summary>
public partial class GameWindow
{
    /// <summary>
    ///     Initializes a new instance of the GameWindow class, subscribes to the Activated and Deactivated events.
    /// </summary>
    public GameWindow()
    {
        InitializeComponent();
        Activated += GameWindow_Activated;
        Deactivated += GameWindow_Deactivated;
    }

    /// <summary>
    ///     Handles the MouseDown event of the window. If the left mouse button is pressed, it initiates the window move
    ///     operation.
    /// </summary>
    public void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed) DragMove();
    }

    /// <summary>
    ///     Handles the click event for the minimize button, minimizing the current window.
    /// </summary>
    public void BtnMinimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    /// <summary>
    ///     Handles the Click event of the BtnClose control.
    /// </summary>
    public void BtnClose_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    /// <summary>
    ///     Handles the Activated event of the GameWindow control.
    ///     If the DataContext is a GameViewModel, it resumes the timer.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains no event data.</param>
    public void GameWindow_Activated(object? sender, EventArgs e)
    {
        if (DataContext is GameViewModel viewModel) viewModel.ResumeTimer();
    }

    /// <summary>
    ///     Handles when the GameWindow becomes deactivated. If the current DataContext is a GameViewModel, it pauses the
    ///     timer.
    /// </summary>
    public void GameWindow_Deactivated(object? sender, EventArgs e)
    {
        if (DataContext is GameViewModel viewModel) viewModel.PauseTimer();
    }
}