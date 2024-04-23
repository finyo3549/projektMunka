using System;
using MagicQuizDesktop.Models;
using MagicQuizDesktop.Repositories;
using MagicQuizDesktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MagicQuizDesktop;

/// <summary>
///     This class functions as the application's main entry point. It is responsible for configuring services,
///     creating and managing instances of the Service Provider, as well as handling the application's start event.
/// </summary>
public partial class App
{
    /// <summary>
    ///     Initializes and configures services for the application.
    /// </summary>
    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
    }

    /// <summary>
    ///     Gets the instance of the service provider that is available globally throughout the application.
    /// </summary>
    public static IServiceProvider ServiceProvider { get; private set; }

    /// <summary>
    ///     Configures services for the application. Registers services in the dependency injection container.
    ///     This includes singleton services for the TopicRepository and QuestionRepository, and transient services for the
    ///     TopicViewModel and QuestionViewModel.
    /// </summary>
    public static void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<ITopicRepository, TopicRepository>();
        services.AddSingleton<IQuestionRepository, QuestionRepository>();
        services.AddTransient<TopicViewModel>();
        services.AddTransient<QuestionViewModel>();
    }


    /////// <summary>
    /////// Handles the start of the application. It initializes and displays a LoginView.
    /////// If LoginView is no longer visible and has been loaded, a MainWindow is displayed.
    /////// When the MainWindow gets closed or becomes invisible, the LoginView is displayed again.
    /////// </summary>
    ////protected void ApplicationStart(object sender, StartupEventArgs e)
    ////{
    ////    var loginView = new LoginView();
    ////    loginView.Show();

    ////    loginView.IsVisibleChanged += (s, ev) =>
    ////    {
    ////        var mainWindow = new MainWindow();
    ////        mainWindow.Show();


    ////        if (loginView.IsVisible || !loginView.IsLoaded) return;

    ////        var mainWindow = new MainWindow();
    ////        var isMainWindowClosed = false;

    ////        mainWindow.Closed += (mainSender, mainEv) =>
    ////        {
    ////            isMainWindowClosed = true;
    ////        };

    ////        mainWindow.Show();

    ////        if (this.ShutdownMode != ShutdownMode.OnLastWindowClose) return;
    ////        mainWindow.IsVisibleChanged += (mainSender, mainEv) =>
    ////        {
    ////            if (mainWindow.IsVisible || isMainWindowClosed) return;

    ////            loginView.Show();
    ////        };

    ////    };
    ////}
}