using System.ComponentModel;

namespace MagicQuizDesktop.Models;

/// <summary>
///     Represents a Message object with text and color properties, and notifies when the properties change.
/// </summary>
public class Message : INotifyPropertyChanged
{
    private string _messageColor;
    private string _messageText;

    /// <summary>
    ///     Initializes a new instance of the Message class,
    ///     setting its text and color properties to an empty string.
    /// </summary>
    public Message()
    {
        MessageText = string.Empty;
        MessageColor = "Green";
    }

    /// <summary>
    ///     Initializes a new instance of the Message class with the specified text and color.
    /// </summary>
    /// <param name="messageText">The text of the message.</param>
    /// <param name="messageColor">The color of the message.</param>
    public Message(string messageText, string messageColor)
    {
        MessageText = messageText;
        MessageColor = messageColor;
    }

    public string MessageText
    {
        get => _messageText;
        set
        {
            if (_messageText == value) return;
            _messageText = value;
            OnPropertyChanged(nameof(MessageText));
        }
    }

    public string MessageColor
    {
        get => _messageColor;
        set
        {
            if (_messageColor == value) return;
            _messageColor = value;
            OnPropertyChanged(nameof(MessageColor));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    ///     Invokes the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}