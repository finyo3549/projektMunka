using System.ComponentModel;

namespace MagicQuizDesktop.ViewModels;

/// <summary>
///     Represents a base class for view models that implement the <see cref="INotifyPropertyChanged" /> interface.
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    ///     Method used to raise the PropertyChanged event with the specified property name.
    ///     To refresh the view
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}