using SudokuGame.Viewmodel;

namespace SudokuGame;

public partial class MainPage : ContentPage
{
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

    private void markErrorToggled(object sender, ToggledEventArgs e)
    {
       // SudukoGeneratorViewmodel.Instance.MarkError = e.Value;
        SudukoGeneratorViewmodel.Instance.markErrors(e.Value);
    }
}