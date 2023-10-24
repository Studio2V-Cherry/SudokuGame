using Android.Util;
using Core.CrashlyticsHelpers;
using Core.TimerHelpers;
using SudokuGame.CoreLogics;
using SudokuGame.Viewmodel;

namespace SudokuGame;

/// <summary>
/// suduko game class
/// </summary>
/// <seealso cref="Microsoft.Maui.Controls.ContentPage" />
public partial class SudokuGame : ContentPage
{
    /// <summary>
    /// The suduko generator viewmodel
    /// </summary>
    SudukoGeneratorViewmodel _sudukoGeneratorViewmodel = SudukoGeneratorViewmodel.instance;

    /// <summary>
    /// Initializes a new instance of the <see cref="SudokuGame"/> class.
    /// </summary>
    public SudokuGame()
    {
        try
        {
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.page);
            InitializeComponent();
            this.BindingContext = _sudukoGeneratorViewmodel;
        }
        catch (Exception e)
        {
            CrashLogger.LogException(e);
        }
    }

    /// <summary>
    /// Called when [appearing].
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.page);
            if (PuzzleGenerator.Instance.IsSudokuHistorySelected)
            {
                TimerHelpers.resumeTimer();
            }
            else
            {
                TimerHelpers.startTimer();
            }
            PuzzleGenerator.Instance.IsSudokuHistorySelected = false;
        }
        catch (Exception e)
        {
            CrashLogger.LogException(e);
        }
    }

    /// <summary>
    /// Marks the error toggled.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ToggledEventArgs" /> instance containing the event data.</param>
    private void markErrorToggled(object sender, ToggledEventArgs e)
    {
        try
        {
            _sudukoGeneratorViewmodel.markErrors(e.Value);
        }
        catch (Exception ex)
        {
            CrashLogger.LogException(ex);
        }
    }

    /// <summary>
    /// Called when [disappearing].
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        try
        {
            _sudukoGeneratorViewmodel.savesuduko();
            parentGrid
                .Children.Clear();
            BindingContext=null;
            //[TODO] [clear page memory]
        }
        catch (Exception e)
        {
            CrashLogger.LogException(e);
        }
    }
}