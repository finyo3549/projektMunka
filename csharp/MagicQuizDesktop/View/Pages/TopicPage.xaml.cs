using MagicQuizDesktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace MagicQuizDesktop.View.Pages
{
    /// <summary>
    /// Represents a Topic page in the application and initializes its components and data context.
    /// </summary>
    public partial class TopicPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TopicPage"/> class.
        /// </summary>
        public TopicPage()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetService<TopicViewModel>();
        }
    }
}
