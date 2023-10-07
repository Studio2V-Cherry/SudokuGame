using Core.LocalStorageHelper;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using SudokuGame.CommonControls;
using SudokuGame.CoreLogics;
using SudokuGame.Pages;
using SudokuGame.Platforms.Android.ControlRendererrers;
using SudokuGame.Viewmodel;

namespace SudokuGame;

/// <summary>
/// 
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Creates the maui application.
    /// </summary>
    /// <returns></returns>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCompatibility()
            .ConfigureMauiHandlers(handlers =>
            {
#if __ANDROID__
                handlers.AddCompatibilityRenderer(typeof(FrameControl), typeof(FrameControlRenderrer));
                handlers.AddCompatibilityRenderer(typeof(LabelControl), typeof(LabelControlRenderer));
#endif
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        //models
        builder.Services.AddSingleton<StorageHelper>();
        //builder.Services.AddSingleton<TodoItemDatabase>();
        builder.Services.AddSingleton<BaseViewmodel>();
        builder.Services.AddSingleton<SudukoGeneratorViewmodel>();
        builder.Services.AddSingleton<SudukoSolverViewmodel>();
        builder.Services.AddSingleton<PuzzleGenerator>();

        //pages
        builder.Services.AddSingleton<SudokuGame>();
        builder.Services.AddSingleton<StarterPage>();

        return builder.Build();
    }
}
