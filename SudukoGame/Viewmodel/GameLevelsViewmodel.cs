using Core.CrashlyticsHelpers;
using SudokuGame.CoreLogics;
using SudokuGame.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SudokuGame.Viewmodel
{
    /// <summary>
    /// game levels viewmodel manage
    /// </summary>
    /// <seealso cref="SudokuGame.Viewmodel.BaseViewmodel" />
    public class GameLevelsViewmodel : BaseViewmodel
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static GameLevelsViewmodel _instance;

        /// <summary>
        /// The levels models
        /// </summary>
        private ObservableCollection<LevelsModel> _levelsModels;

        /// <summary>
        /// Gets or sets the levels models.
        /// </summary>
        /// <value>
        /// The levels models.
        /// </value>
        public ObservableCollection<LevelsModel> LevelsModels
        {
            get => _levelsModels;
            set
            {
                _levelsModels = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static GameLevelsViewmodel Instance
        {
            protected set => _instance = value;
            get => _instance = _instance ?? (_instance = new GameLevelsViewmodel());
        }

        /// <summary>
        /// Gets the level select command.
        /// </summary>
        /// <value>
        /// The level select command.
        /// </value>
        public ICommand LevelSelectCommand => new Command<LevelsModel>(async(x)=>await levelSelect(x));

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLevelsViewmodel" /> class.
        /// </summary>
        private GameLevelsViewmodel()
        {
            try
            {
                CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
                GenerateLevels();
            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
            }
        }

        /// <summary>
        /// Levels the select.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private async Task levelSelect(LevelsModel model)
        {
            try
            {
                CrashLogger.TrackEvent(Core.Constants.loggerEnum.page);
                PuzzleGenerator.Instance._levelsModel = model;
                await SudukoGeneratorViewmodel.instance.PopulateSuduko();
                await PuzzleGenerator.Instance.PushPage(new SudokuGame());
            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
            }
        }

        /// <summary>
        /// Generates the levels.
        /// </summary>
        private void GenerateLevels()
        {
            Dictionary<int, string> levels = new Dictionary<int, string>()
            {
                {0,"Easy"},
                {1,"Moderate" },
                {2,"Challenging" },
                {3,"Random"},
                {4,"Generate Myself" }
            };
            LevelsModels = new ObservableCollection<LevelsModel>();
            foreach (var level in levels)
            {
                LevelsModels.Add(new LevelsModel()
                {
                    LevelName = level.Value,
                    LevelDescription = level.Value,
                    Gamelevels = level.Key
                });
            }
        }
    }
}
