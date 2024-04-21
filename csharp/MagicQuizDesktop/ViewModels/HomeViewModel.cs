using System.Collections.Generic;
using System.Windows.Input;
using MagicQuizDesktop.Commands;
using MagicQuizDesktop.Models;
using MagicQuizDesktop.Services;
using MagicQuizDesktop.View.Windows;

namespace MagicQuizDesktop.ViewModels;

/// <summary>
///     Represents a view model for the Home view.
/// </summary>
/// <remarks>
///     This view model includes the list of articles to be displayed on the Home page,
///     the current user of the application, and commands to handle user interactions such as starting a game or adding an
///     article.
/// </remarks>
public class HomeViewModel : ViewModelBase
{
    private List<string> _articles;

    private User _currentUser;


    /// <summary>
    ///     Initializes a new instance of the <see cref="HomeViewModel" /> class.
    ///     It initializes commands for starting the game.
    /// </summary>
    public HomeViewModel()
    {
        Initialize();
        StartGameClickCommand = new RelayCommand(_ => OpenGameWindow());
    }


    /// <summary>
    ///     Gets or sets the current user.
    /// </summary>
    public User CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            OnPropertyChanged(nameof(CurrentUser));
        }
    }

    /// <summary>
    ///     Represents a list of articles.
    /// </summary>
    public List<string> Articles
    {
        get => _articles;
        set
        {
            _articles = value;
            OnPropertyChanged(nameof(Articles));
        }
    }

    /// <summary>
    ///     Gets the command for starting the game.
    /// </summary>
    public ICommand StartGameClickCommand { get; }

    /// <summary>
    ///     Opens a new game window and displays it as a modal dialog box.
    /// </summary>
    private static void OpenGameWindow()
    {
        GameWindow window = new();
        window.ShowDialog();
    }

    /// <summary>
    ///     Initializes necessary components, by assigning the current user from the session manager and setting up Articles.
    /// </summary>
    private void Initialize()
    {
        CurrentUser = SessionManager.Instance.CurrentUser;
        SetArticles();
    }


    /// <summary>
    ///     Sets the article constants, which are split into three strings, possibly due to length or thematic division.
    ///     These articles seem to be the preamble and instructions to a quiz game, as they mention welcome messages, scoring,
    ///     a leaderboard, and lifelines (including 50/50, Ask The Audience, and Phone A Friend options).
    ///     After setting the constants, they are added to the Articles list.
    /// </summary>
    private void SetArticles()
    {
        Articles = [];
        const string article1 = "Köszöntünk a Magic Quiz-ben, ahol a tudásod varázslatos próbára teszed! " +
                                "Készülj fel egy izgalmas kalandra, ahol minden kérdés egy újabb lépés a tudás birodalmában." +
                                "A játék egyszerű: tíz különböző témájú kérdés, mindegyikre csak egy helyes válasz létezik.";

        const string article2 =
            "Figyelj, mert az idő szorít! Minden kérdésre csupán 20 másodperced van a válaszadásra," +
            "így gyorsaságod és tudásod egyaránt próbára kerül. Minden helyes válaszért 100 pontot kapsz," +
            "így a maximális pontszám elérése felé törhetsz. Ha elégséges pontot gyűjtesz," +
            "bekerülhetsz a ranglistára, ahol összemérheted tudásodat más kvízvarázslókkal.";

        const string article3 =
            "Tipp: Elakadatál? Használd a segítségeket! Minden új játék kezdetekor kapsz 3 rendkívüli szolgáltatást:" +
            "Felező/Közönség/Telefonhívás";

        Articles.Add(article1);
        Articles.Add(article2);
        Articles.Add(article3);
    }
}