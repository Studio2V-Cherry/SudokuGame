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
        BaseViewmodel.Instance.IsSudokuHistorySelected = isResume;
        _sudukoGeneratorViewmodel.IsInGeneration = true;

        InitializeComponent();
        this.BindingContext = _sudukoGeneratorViewmodel;
    }

    /// <summary>
    /// Called when [appearing].
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
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

    /// <summary>
    /// Marks the error toggled.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ToggledEventArgs" /> instance containing the event data.</param>
    private void markErrorToggled(object sender, ToggledEventArgs e)
    {
        _sudukoGeneratorViewmodel.markErrors(e.Value);
    }

    /// <summary>
    /// Called when [disappearing].
    /// </summary>
    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        await _sudukoGeneratorViewmodel.savesuduko();
        parentGrid
            .Children.Clear();
    }
}