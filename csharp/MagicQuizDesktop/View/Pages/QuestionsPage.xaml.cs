using System.Windows.Controls;
using MagicQuizDesktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MagicQuizDesktop.View.Pages;

/// <summary>
///     Represents a QuestionsPage. This is a partial class that inherits from the Page class.
///     It initialises components and sets the data context to an instance of the QuestionViewModel retrieved from the
///     service provider.
/// </summary>
public partial class QuestionsPage : Page
{
    /// <summary>
    ///     Initializes a new instance of the QuestionsPage class.
    /// </summary>
    public QuestionsPage()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetService<QuestionViewModel>();
    }
}