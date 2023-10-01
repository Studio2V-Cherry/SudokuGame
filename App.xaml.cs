namespace SudokuGame;

/// <summary>
/// main app class
/// </summary>
/// <seealso cref="Microsoft.Maui.Controls.Application" />
public partial class App : Application
{
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public App()
    {
        InitializeComponent();
        MainPage = new MainPage();
    }
}
