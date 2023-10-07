using SudokuGame.Viewmodel;

namespace SudokuGame.Models
{
    /// <summary>
    /// game items
    /// </summary>
    /// <seealso cref="SudokuGame.Viewmodel.BaseViewmodel" />
    public class GameStartModel : BaseViewmodel
    {
        /// <summary>
        /// The game name
        /// </summary>
        private string _gameName;
        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        /// <value>
        /// The name of the game.
        /// </value>
        public string GameName
        {
            get => _gameName;
            set
            {
                _gameName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The is visible
        /// </summary>
        private bool _isVisible;

        /// <summary>
        /// Gets or sets the is visible.
        /// </summary>
        /// <value>
        /// The is visible.
        /// </value>
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The command
        /// </summary>
        private Command _command;
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public Command Command
        {
            get => _command;
            set
            {
                _command = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The command parameter
        /// </summary>
        private Page _commandParameter;

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        /// <value>
        /// The command parameter.
        /// </value>
        public Page CommandParameter
        {
            get => _commandParameter;
            set
            {
                _commandParameter = value;
                OnPropertyChanged();
            }
        }
    }
}
