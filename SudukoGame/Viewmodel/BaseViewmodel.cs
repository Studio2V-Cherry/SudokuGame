using Core.CrashlyticsHelpers;
using SudokuGame.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SudokuGame.Viewmodel
{
    /// <summary>
    /// base viewmodel for common properties and functionalities
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class BaseViewmodel : INotifyPropertyChanged
    {
        #region fields
        /// <summary>
        /// The game start model
        /// </summary>
        private ObservableCollection<GameStartModel> _gameStartModel;

        /// <summary>
        /// The instance
        /// </summary>
        private static BaseViewmodel _instance;

        /// <summary>
        /// The is sudoku history
        /// </summary>
        private bool _isSudokuHistory;

        /// <summary>
        /// The is sudoku history selected
        /// </summary>
        private bool _isSudokuHistorySelected;
        #endregion

        #region public
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static BaseViewmodel Instance
        {
            get
            {
                _instance = _instance ?? new BaseViewmodel();
                return _instance;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sudoku history.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sudoku history; otherwise, <c>false</c>.
        /// </value>
        public bool IsSudokuHistory
        {
            get => _isSudokuHistory;
            set
            {
                _isSudokuHistory = value;
                OnPropertyChanged(nameof(IsSudokuHistory));
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is sudoku history selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sudoku history selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSudokuHistorySelected
        {
            get => _isSudokuHistorySelected;
            set
            {
                _isSudokuHistorySelected = value;
                OnPropertyChanged(nameof(IsSudokuHistorySelected));
            }
        }

        /// <summary>
        /// Gets or sets the game start models.
        /// </summary>
        /// <value>
        /// The game start models.
        /// </value>
        public ObservableCollection<GameStartModel> GameStartModels
        {
            get => _gameStartModel;
            set
            {
                _gameStartModel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region commands
        /// <summary>
        /// Gets the navigation command.
        /// </summary>
        /// <value>
        /// The navigation command.
        /// </value>
        public Command NavigationCommand => new Command<Page>(PushPage);

        /// <summary>
        /// Gets or sets the navigation.
        /// </summary>
        /// <value>
        /// The navigation.
        /// </value>
        private INavigation navigation { get; set; } 
        #endregion

        /// <summary>
        /// Prevents a default instance of the <see cref="BaseViewmodel" /> class from being created.
        /// </summary>
        public BaseViewmodel()
        {

        }

        #region methods
        /// <summary>
        /// Sets the navigation.
        /// </summary>
        /// <param name="navigation">The navigation.</param>
        public void setNavigation(INavigation navigation)
        {
            this.navigation = navigation;
        }

        /// <summary>
        /// Pushes the page.
        /// </summary>
        /// <param name="page">The page.</param>
        public async void PushPage(Page page)
        {
            await navigation.PushAsync(page, true);
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
