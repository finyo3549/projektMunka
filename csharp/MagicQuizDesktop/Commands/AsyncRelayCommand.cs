using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MagicQuizDesktop.Commands;

/// <summary>
///     Represents an asynchronous command for the Command pattern, which is a behavioral design pattern that turns a
///     request into a stand-alone object that contains all information about the request.
///     This model allows you to pass requests as method arguments, delay or queue a request's execution, and support
///     undoable operations.
/// </summary>
internal class AsyncRelayCommand : ICommand
{
    private readonly Predicate<object> _canExecute;

    // Fields
    private readonly Func<object, Task> _executeActionAsync;

    // Constructors
    /// <summary>
    ///     Initializes a new instance of the <see cref="AsyncRelayCommand" /> class with a specific function to be executed
    ///     asynchronously.
    /// </summary>
    /// <param name="executeActionAsync">The asynchronous action to be executed by the command.</param>
    public AsyncRelayCommand(Func<object, Task> executeActionAsync)
        : this(executeActionAsync, null)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AsyncRelayCommand" /> class.
    /// </summary>
    /// <param name="executeActionAsync">The asynchronous action to execute.</param>
    /// <param name="canExecute">The predicate that determines if the command can execute.</param>
    /// <exception cref="ArgumentNullException">Thrown when executeActionAsync is null.</exception>
    public AsyncRelayCommand(Func<object, Task> executeActionAsync, Predicate<object> canExecute)
    {
        _executeActionAsync = executeActionAsync ?? throw new ArgumentNullException(nameof(executeActionAsync));
        _canExecute = canExecute;
    }

    // Events
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    // Methods
    /// <summary>
    ///     Determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">
    ///     Data used by the command. If the command does not require data to be passed, this object can be
    ///     set to null.
    /// </param>
    /// <returns>true if this command can be executed; otherwise, false.</returns>
    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    /// <summary>
    ///     Executes the async action associated with the command, if it's available and can be executed with the given
    ///     parameter.
    /// </summary>
    /// <param name="parameter">The parameter to be used in the command's execution.</param>
    public async void Execute(object parameter)
    {
        if (_executeActionAsync != null && CanExecute(parameter)) await _executeActionAsync(parameter);
    }

    // Method to trigger the CanExecuteChanged event to re-evaluate the can execute logic
    /// <summary>
    ///     Triggers the CanExecuteChanged event to re-evaluate the can execute logic.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}