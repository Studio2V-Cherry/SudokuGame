using SudokuGame.Viewmodel;

namespace SudokuGame;

/// <summary>
/// 
/// </summary>
/// <seealso cref="Microsoft.Maui.Controls.ContentPage" />
public partial class MainPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    public MainPage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = SudukoGeneratorViewmodel.Instance;
            SudukoGeneratorViewmodel.Instance.FirstTimeSuduko();
            errorSwitch.Toggled += markErrorToggled;
        }
        catch (Exception e)
        {
            _ = e;
        }

    }

    /// <summary>
    /// Marks the error toggled.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ToggledEventArgs"/> instance containing the event data.</param>
    private void markErrorToggled(object sender, ToggledEventArgs e)
    {
        // SudukoGeneratorViewmodel.Instance.MarkError = e.Value;
        SudukoGeneratorViewmodel.Instance.markErrors(e.Value);
    }
}