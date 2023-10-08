using SudokuGame.Pages;
using SudokuGame.Viewmodel;

namespace SudokuGame;

/// <summary>
/// main app class
/// </summary>
/// <seealso cref="Microsoft.Maui.Controls.Application" />
public partial class App : Application
{
    /// <summary>
    /// Initializes a new instance of the <see cref="App" /> class.
    /// </summary>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public App()
    {
        InitializeComponent();
        Current.UserAppTheme = AppTheme.Light;
        var navBar = new NavigationPage(new StarterPage());
        navBar.BarBackgroundColor = Colors.White;
        navBar.BackgroundColor = Colors.Transparent;
        MainPage = navBar;
        MainPage.BackgroundColor = Colors.Black;
        BaseViewmodel.Instance.setNavigation(navBar.Navigation);
    }
}
