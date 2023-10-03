using SudokuGame.Viewmodel;

namespace SudokuGame;

/// <summary>
/// suduko game class
/// </summary>
/// <seealso cref="Microsoft.Maui.Controls.ContentPage" />
public partial class SudokuGame : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage" /> class.
    /// </summary>
    public SudokuGame()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = SudukoGeneratorViewmodel.Instance;            
            configurePageGrid();
        }
        catch (Exception e)
        {
            _ = e;
        }

    }

    /// <summary>
    /// Configures the page grid.
    /// </summary>
    private void configurePageGrid()
    {
       // BindableLayout.SetItemsSource(sudukoGrid, SudukoGeneratorViewmodel.Instance.SudukoBoardModel);
    }

    /// <summary>
    /// Called when [appearing].
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //SudukoGeneratorViewmodel.Instance.FirstTimeSuduko();

        //errorSwitch.Toggled += markErrorToggled;
        ///BindableLayout.SetItemsSource(sudukoGrid, SudukoGeneratorViewmodel.Instance.SudukoBoardModel);
    }

    /// <summary>
    /// Marks the error toggled.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ToggledEventArgs" /> instance containing the event data.</param>
    private void markErrorToggled(object sender, ToggledEventArgs e)
    {
        SudukoGeneratorViewmodel.Instance.markErrors(e.Value);
    }

    /// <summary>
    /// Called when [disappearing].
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
       // sudukoGrid?.Clear();
    }
}