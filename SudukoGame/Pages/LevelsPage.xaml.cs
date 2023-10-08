using SudokuGame.Viewmodel;

namespace SudokuGame.Pages;

/// <summary>
/// levels page to display levels
/// </summary>
/// <seealso cref="Microsoft.Maui.Controls.ContentPage" />
public partial class LevelsPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LevelsPage"/> class.
    /// </summary>
    public LevelsPage()
    {
        InitializeComponent();
        BindingContext = GameLevelsViewmodel.Instance;
    }
}