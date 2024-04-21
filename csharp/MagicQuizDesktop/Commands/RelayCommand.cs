using System;
using System.Windows.Input;

namespace MagicQuizDesktop.Commands;

/// <summary>
///     Represents the RelayCommand class which implements the ICommand interface.
///     A Relay Command is a type of command that allows to bind methods to delegate commands where the Execute and
///     CanExecute methods can be defined dynamically.
/// </summary>
internal class RelayCommand : ICommand
{
    private readonly Predicate<object> _canExecuteAction;
    private readonly Action<object> _executeAction;


    //Constructors
    /// <summary>
    ///     Initializes a new instance of the RelayCommand class that can always execute.
    /// </summary>
    /// <param name="executeAction">The execution logic.</param>
    public RelayCommand(Action<object> executeAction)
    {
        _executeAction = executeAction;
        _canExecuteAction = null;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="RelayCommand" /> class.
    /// </summary>
    /// <param name="executeAction">The action to be executed.</param>
    /// <param name="canExecuteAction">The predicate to determine whether the command can execute or not.</param>
    public RelayCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
    {
        _executeAction = executeAction;
        _canExecuteAction = canExecuteAction;
    }


    //Events
    /// <summary>
    ///     Event triggered when the execution status of the command changes.
    ///     It does this by subscribing or unsubscribing the value to the `CommandManager.RequerySuggested` event.
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    //Methods
    /// <summary>
    ///     Determines whether the specified command can execute in its current state.
    /// </summary>
    /// <param name="parameter">
    ///     Data used by the command. If the command does not require data to be passed, this object can be
    ///     set to null.
    /// </param>
    /// <returns>true if this command can be executed; otherwise, false.</returns>
    public bool CanExecute(object? parameter)
    {
        return _canExecuteAction == null ? true : _canExecuteAction(parameter);
    }

    /// <summary>
    ///     Executes the action with the provided parameter.
    /// </summary>
    /// <param name="parameter">The parameter to pass to the action.</param>
    public void Execute(object? parameter)
    {
        _executeAction(parameter);
    }
}