using SudokuGame.Pages;

namespace SudokuGame;

/// <summary>
/// main app class
/// </summary>
/// <seealso cref="Microsoft.Maui.Controls.Application" />
public partial class App : Application
{
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public App()
    {
        InitializeComponent();
        Application.Current.UserAppTheme = AppTheme.Light;
        var navBar= new NavigationPage(new StarterPage());
        navBar.BarBackgroundColor = Colors.Transparent;
        MainPage = navBar;
    }
}
