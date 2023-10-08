using Core.CrashlyticsHelpers;
using Core.TimerHelpers;
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
    /// Initializes a new instance of the <see cref="MainPage" /> class.
    /// </summary>
    /// <param name="isResume">if set to <c>true</c> [is resume].</param>
    public SudokuGame(bool isResume = false)
    {
        try
        {
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.page);
            BaseViewmodel.Instance.IsSudokuHistorySelected = isResume;
            _sudukoGeneratorViewmodel.IsInGeneration = true;

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
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.page);
            await _sudukoGeneratorViewmodel.FirstTimeSuduko();
            if (BaseViewmodel.Instance.IsSudokuHistorySelected)
            {
                TimerHelpers.resumeTimer();
            }
            else
            {
                TimerHelpers.startTimer();
            }
            BaseViewmodel.Instance.IsSudokuHistorySelected = false;
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
    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        try
        {
            await _sudukoGeneratorViewmodel.savesuduko();
            parentGrid
                .Children.Clear();
            BindingContext=null;
        }
        catch (Exception e)
        {
            CrashLogger.LogException(e);
        }
    }
}