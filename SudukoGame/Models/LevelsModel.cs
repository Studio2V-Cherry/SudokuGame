using SudokuGame.Viewmodel;
using System.Windows.Input;

namespace SudokuGame.Models
{
    /// <summary>
    /// levels models class
    /// </summary>
    /// <seealso cref="SudokuGame.Viewmodel.BaseViewmodel" />
    public class LevelsModel : BaseViewmodel
    {
        /// <summary>
        /// The level name
        /// </summary>
        private string _levelName;

        /// <summary>
        /// The level description
        /// </summary>
        private string _levelDescription;

        /// <summary>
        /// Gets or sets the name of the level.
        /// </summary>
        /// <value>
        /// The name of the level.
        /// </value>
        public string LevelName
        {
            get => _levelName;
            set
            {
                _levelName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The page push command
        /// </summary>
        public Command pagePushCommand;

        /// <summary>
        /// Gets or sets the level description.
        /// </summary>
        /// <value>
        /// The level description.
        /// </value>
        public string LevelDescription
        {
            get => _levelDescription;
            set
            {
                _levelDescription = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The gamelevels
        /// </summary>
        public int Gamelevels;
    }
}
