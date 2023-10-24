using Core.CrashlyticsHelpers;
using SudokuGame.CoreLogics;
using SudokuGame.Viewmodel;
using static Microsoft.Maui.LifecycleEvents.AndroidLifecycle;

namespace SudokuGame.Pages;

/// <summary>
/// starter page
/// </summary>
/// <seealso cref="Microsoft.Maui.Controls.ContentPage" />
public partial class StarterPage : ContentPage
{
    /// <summary>
    /// Gets the sudukogenerator viewmodel.
    /// </summary>
    /// <value>
    /// The sudukogenerator viewmodel.
    /// </value>
    SudukoGeneratorViewmodel _sudukogeneratorViewmodel => SudukoGeneratorViewmodel.instance;
    /// <summary>
    /// Gets the puzzle generator.
    /// </summary>
    /// <value>
    /// The puzzle generator.
    /// </value>
    PuzzleGenerator _puzzleGenerator => PuzzleGenerator.Instance;
    /// <summary>
    /// Initializes a new instance of the <see cref="StarterPage" /> class.
    /// </summary>
    public StarterPage()
    {
        try
        {            
            InitializeComponent();
        }
        catch (Exception e)
        {
            CrashLogger.LogException(e);
        }
    }

    /// <summary>
    /// Called when [appearing].
    /// </summary>
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _puzzleGenerator.getIfSudukoHistoryPresent();
        historyOption.IsVisible = _puzzleGenerator.IsSudokuHistory;
    }

    /// <summary>
    /// Plays the suduko game.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private async void playSudukoGame(object sender, EventArgs e)
    {
        try
        {
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.page);
            await Navigation.PushAsync(new SudokuGame());
        }
        catch (Exception ex)
        {
            CrashLogger.LogException(ex);
        }
    }

    /// <summary>
    /// Resumes the suduko game.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private async void resumeSudukoGame(object sender, EventArgs e)
    {
        try
        {
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.page);
            PuzzleGenerator.Instance.IsSudokuHistorySelected = true;
            SudukoGeneratorViewmodel.instance.IsInGeneration = true;
            await SudukoGeneratorViewmodel.instance.FirstTimeSuduko();
            await Navigation.PushAsync(new SudokuGame());
        }
        catch (Exception ex)
        {
            CrashLogger.LogException(ex);
        }
    }

    /// <summary>
    /// Levelses the suduko game.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private async void levelsSudukoGame(object sender, EventArgs e)
    {
        try
        {
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.page);
            await Navigation.PushAsync(new LevelsPage());
        }
        catch (Exception ex)
        {
            CrashLogger.LogException(ex);
        }
    }
}