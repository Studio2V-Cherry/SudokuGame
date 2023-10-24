using Core.CrashlyticsHelpers;
using SudokuGame.CoreLogics;
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
        coldStart().Wait();
        Current.UserAppTheme = AppTheme.Light;
        MainPage = new NavigationPage(new StarterPage())
        {
            BarBackground = Colors.White,
            BackgroundColor = Colors.Transparent
        };
        MainPage.BackgroundColor = Colors.Black;
        PuzzleGenerator.Instance.setNavigation(MainPage.Navigation);
    }

    /// <summary>
    /// Colds the start.
    /// </summary>
    private async Task coldStart()
    {
        try
        {
            SudukoGeneratorViewmodel.instance.configurePage();
            await SudukoGeneratorViewmodel.instance.FirstTimeSuduko();
        }
        catch (Exception e)
        {
            CrashLogger.LogException(e);
        }
    }
}
