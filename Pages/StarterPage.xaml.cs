using SudokuGame.Viewmodel;

namespace SudokuGame.Pages;

/// <summary>
/// starter page 
/// </summary>
/// <seealso cref="Microsoft.Maui.Controls.ContentPage" />
public partial class StarterPage : ContentPage
{
    
    /// <summary>
    /// Initializes a new instance of the <see cref="StarterPage"/> class.
    /// </summary>
    public StarterPage()
    {
        InitializeComponent();
        this.BindingContext = BaseViewmodel.Instance;
    }

    /// <summary>
    /// Playsudukoes the specified sender.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private async void playsuduko(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SudokuGame(),true);
    }
}