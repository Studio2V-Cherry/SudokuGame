using SudokuGame.Models;
using SudokuSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SudokuGame.Viewmodel
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SudukoGeneratorViewmodel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the sudoku board.
        /// </summary>
        /// <value>
        /// The sudoku board.
        /// </value>
        public Board SudokuBoard { get; set; }

        private Board SolvedSudokuBoard { get; set; }
        /// <summary>
        /// Gets or sets the regions.
        /// </summary>
        /// <value>
        /// The regions.
        /// </value>
        private Dictionary<int, List<int>> Regions { get; set; }

        /// <summary>
        /// The instance
        /// </summary>
        private static SudukoGeneratorViewmodel _instance;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// The random
        /// </summary>
        Random random;

        private bool isNumberSelected;
        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static SudukoGeneratorViewmodel Instance
        {
            set { _instance = value; }
            get
            {
                return _instance = _instance ?? new SudukoGeneratorViewmodel();
            }
        }

        /// <summary>
        /// The is loading
        /// </summary>
        private bool _isLoading;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private bool _markError;

        public bool MarkError
        {
            get => _markError;
            set
            {
                _markError = value;
                OnPropertyChanged(nameof(MarkError));
               // markErrors(value);
            }
        }

        /// <summary>
        /// The suduko board model
        /// </summary>
        private ObservableCollection<SudukoBoardModel> _sudukoBoardModel;
        /// <summary>
        /// Gets or sets the suduko board model.
        /// </summary>
        /// <value>
        /// The suduko board model.
        /// </value>
        public ObservableCollection<SudukoBoardModel> SudukoBoardModel
        {
            get => _sudukoBoardModel;
            set
            {
                _sudukoBoardModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The numbers
        /// </summary>
        private ObservableCollection<Numbers> _numbers;

        /// <summary>
        /// Gets or sets the numbers.
        /// </summary>
        /// <value>
        /// The numbers.
        /// </value>
        public ObservableCollection<Numbers> Numbers
        {
            get => _numbers;
            set
            {
                _numbers = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected suduko frame.
        /// </summary>
        /// <value>
        /// The selected suduko frame.
        /// </value>
        private SudukoBoardModel selectedSudukoFrame { get; set; }

        /// <summary>
        /// Gets or sets the selected numbers.
        /// </summary>
        /// <value>
        /// The selected numbers.
        /// </value>
        private Numbers selectedNumbers { get; set; }

        /// <summary>
        /// Gets the frame selected command.
        /// </summary>
        /// <value>
        /// The frame selected command.
        /// </value>
        public ICommand frameSelectedCommand => new Command<SudukoBoardModel>(frameSelectedModel);
        /// <summary>
        /// Gets the number selected command.
        /// </summary>
        /// <value>
        /// The number selected command.
        /// </value>
        public ICommand numberSelectedCommand => new Command<Numbers>(numberSelectedModel);

        public ICommand numberLongSelectedCommand => new Command<Numbers>(numberLongSelected);

        /// <summary>
        /// Gets the validate sudoku command.
        /// </summary>
        /// <value>
        /// The validate sudoku command.
        /// </value>
        public ICommand validateSudokuCommand => new Command(validateSudoku);

        public ICommand regenerateSudokuCommand => new Command(PopulateSuduko);

        /// <summary>
        /// Resets all cells.
        /// </summary>
        private void resetAllCells()
        {
            SudukoBoardModel.All(x =>
            {
                x.BackgroundColor = Colors.Transparent;
                return true;
            });
        }

        /// <summary>
        /// Highlights the regions.
        /// </summary>
        /// <param name="sudukoBoardModel">The suduko board model.</param>
        private void highlightRegions(SudukoBoardModel sudukoBoardModel)
        {
            var num = sudukoBoardModel.CellVal;
            var cellRegion = sudukoBoardModel.cellRegion;
            int i = 0;
            int j = 0;
            SudukoBoardModel.All(x =>
            {
                if (x.rrow == sudukoBoardModel.rrow && x.ccol == i)
                {
                    x.BackgroundColor = Color.FromArgb("#C5C5C5");
                    i++;
                }
                if (x.rrow == j && x.ccol == sudukoBoardModel.ccol)
                {
                    x.BackgroundColor = Color.FromArgb("#C5C5C5");
                    j++;
                }
                if (x.CellVal== sudukoBoardModel.CellVal && !string.IsNullOrEmpty(x.CellVal))
                {
                    x.BackgroundColor = Color.FromArgb("#C5C5C5");
                }
                return true;
            });
            SudukoBoardModel.All(x =>
            {
                if (x.cellRegion == cellRegion)
                    x.BackgroundColor = Color.FromArgb("#629BFB");
                return true;
            });
        }

        /// <summary>
        /// Frames the selected model.
        /// </summary>
        /// <param name="sudukoBoardModel">The suduko board model.</param>
        private void frameSelectedModel(SudukoBoardModel sudukoBoardModel)
        {
            try
            {
                resetAllCells();
                // sudukoBoardModel.BackgroundColor = Colors.Green;
                highlightRegions(sudukoBoardModel);
                if (!sudukoBoardModel.isLocked)
                {
                    sudukoBoardModel.CellVal = selectedNumbers.number;
                    SudokuBoard[sudukoBoardModel.Cell] = int.Parse(selectedNumbers.number);
                    //validateSudoku();
                    sudukoBoardModel.CheckOriginalValue(MarkError);
                    isSudukoSolved();
                }
               // selectedSudukoFrame = sudukoBoardModel;
            }
            catch (Exception e)
            {
                _ = e;
            }
        }

        /// <summary>
        /// Highlights the frames.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        private void highlightFrames(Numbers numbers)
        {
            resetAllCells();
            SudukoBoardModel.All(x =>
            {
                if (x.CellVal == numbers.number && !string.IsNullOrEmpty(x.CellVal))
                    x.BackgroundColor = Color.FromArgb("#629BFB");
                return true;
            });
        }

        /// <summary>
        /// Numbers the selected model.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        private void numberSelectedModel(Numbers numbers)
        {
            isNumberSelected = false;
            Numbers.All(x =>
            {
                if (x.number == numbers.number)
                {
                    x.BackgroundColor = Color.FromArgb("#DCE5F4");
                }
                else
                {
                    x.BackgroundColor = Colors.White;
                }
                return true;
            });
            selectedNumbers = numbers;
            highlightFrames(numbers);
            // selectedSudukoFrame.CellVal =(string) numbers.number;
            //SudokuBoard[selectedSudukoFrame.Cell] = int.Parse(numbers.number);
        }

        private void numberLongSelected(Numbers numbers)
        {
            isNumberSelected =!isNumberSelected;
            if (isNumberSelected)
            {
                Numbers.All(x =>
                {
                    if (x.number == numbers.number)
                    {
                        x.BackgroundColor = Color.FromArgb("#60f78e");
                        selectedNumbers=numbers;
                    }
                    else
                    {
                        x.BackgroundColor = Colors.White;
                    }
                    return true;
                });
            }
            else
            {
                Numbers.All(x =>
                {
                    x.BackgroundColor = Color.FromArgb("#DCE5F4");
                    return true;
                });
                selectedNumbers = numbers;
            }
        }

        /// <summary>
        /// Validates the sudoku.
        /// </summary>
        private async void validateSudoku()
        {
            //if (SudokuBoard.IsValid)
            //{
            //   // await Application.Current.MainPage.DisplayAlert("Sudoku Validation", "Still few fields are empty", "OK");
            //    return;
            //}

            
        }

        public void markErrors(bool check)
        {
            SudukoBoardModel.All(x =>
            {
                x.CheckOriginalValue(check);
                return true;
            });
        }

        private async void isSudukoSolved()
        {
            if (SudokuBoard.IsSolved)
            {
                await Application.Current.MainPage.DisplayAlert("Sudoku", "Sudoku Solved Successfully", "Ok");
                IsLoading = true;
                PopulateSuduko();
                IsLoading = false;
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="SudukoGeneratorViewmodel"/> class from being created.
        /// </summary>
        private SudukoGeneratorViewmodel()
        {
            random = new Random();
            Regions = new Dictionary<int, List<int>>();
        }

        /// <summary>
        /// Resets the populate numbers.
        /// </summary>
        private void ResetPopulateNumbers()
        {
            Numbers = new ObservableCollection<Numbers>();
            for (int i = 1; i <= 9; i++)
                Numbers.Add(new Models.Numbers()
                {
                    number = i.ToString(),
                    rrow = 0,
                    ccol = i - 1,
                    BackgroundColor = Colors.White,
                });
        }

        /// <summary>
        /// Populates the regions.
        /// </summary>
        private void PopulateRegions()
        {
            int region = 1;
            int places = 0;
            for (int m = 0; m < 3; m++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        //Regions.TryAdd(region, new List<int>(places));                        
                        // Regions[region].Add(places++);
                        SudukoBoardModel[places++].cellRegion = region;
                    }
                    for (int k = 0; k < 3; k++)
                    {
                        //Regions.TryAdd(region+1, new List<int>(places));
                        //Regions[region+1].Add(places++);
                        SudukoBoardModel[places++].cellRegion = region + 1;
                    }
                    for (int l = 0; l < 3; l++)
                    {
                        //Regions.TryAdd(region+2, new List<int>(places));
                        //Regions[region+2].Add(places++);
                        SudukoBoardModel[places++].cellRegion = region + 2;
                    }
                }
                region += 3;
            }
        }

        /// <summary>
        /// Firsts the time suduko.
        /// </summary>
        public void FirstTimeSuduko()
        {
            ResetPopulateNumbers();
            PopulateSuduko();
            PopulateRegions();
        }

        /// <summary>
        /// Populates the suduko.
        /// </summary>
        public void PopulateSuduko()
        {
            SolvedSudokuBoard = SudokuSharp.Factory.Solution(random.Next(6, 25));
            SudokuBoard = SudokuSharp.Factory.Puzzle(SolvedSudokuBoard, random, QuadsToCut: random.Next(1, 8), random.Next(1, 8), random.Next(1, 8));
            ReturnPopulatedGrid(SudokuBoard);
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Populates the grid.
        /// </summary>
        private void PopulateGrid()
        {
            if (SudukoBoardModel == null)
            {
                SudukoBoardModel = new ObservableCollection<SudukoBoardModel>();
                for (int i = 0; i < 81; i++)
                {
                    SudukoBoardModel.Add(new Models.SudukoBoardModel());
                }
            }
        }

        /// <summary>
        /// Returns the populated grid.
        /// </summary>
        /// <param name="suduko">The suduko.</param>
        private void ReturnPopulatedGrid(Board suduko)
        {
            //Grid sudukogrid = new Grid();
            //RowDefinitionCollection rowDefinitions = new RowDefinitionCollection();
            //ColumnDefinitionCollection columnDefinitions = new ColumnDefinitionCollection();

            PopulateGrid();
            //SudukoBoardModel = new ObservableCollection<SudukoBoardModel>();
            // sudukogrid.BindingContext = SudukoBoardModel;
            // BindableLayout.SetItemsSource(sudukogrid, SudukoBoardModel);
            int places = 0;
            for (int i = 0; i < 9; i++)
            {
                //rowDefinitions.Add(new RowDefinition());
                //columnDefinitions.Add(new ColumnDefinition());

                for (int j = 0; j < 9; j++, places++)
                {
                    SudukoBoardModel[places].isLocked = (suduko[places] != 0);
                    SudukoBoardModel[places].rrow = j;
                    SudukoBoardModel[places].TextColor = Colors.Black;
                    SudukoBoardModel[places].ccol = i;
                    SudukoBoardModel[places].Cell = places;
                    SudukoBoardModel[places].OriginalCellVal = SolvedSudokuBoard[places].ToString();
                    SudukoBoardModel[places].CellVal = (suduko[places] == 0) ? "" : suduko[places].ToString();

                    //SudukoBoardModel.Add(new Models.SudukoBoardModel()
                    //{
                    //    isLocked = (suduko[places] != 0),
                    //    rrow = j,
                    //    ccol = i,
                    //    Cell = places,
                    //    CellVal = (suduko[places] == 0) ? "" : suduko[places].ToString()
                    //});
                }
            }

            //sudukogrid.RowDefinitions = rowDefinitions;
            //sudukogrid.ColumnDefinitions = columnDefinitions;
            //BindableLayout.SetItemsSource(sudukogrid, SudukoBoardModel);

            //return sudukogrid;
        }

        /// <summary>
        /// Determines whether [is sudoku board locked] [the specified suduko board model].
        /// </summary>
        /// <param name="SudukoBoardModel">The suduko board model.</param>
        private void isSudokuBoardLocked(ObservableCollection<SudukoBoardModel> SudukoBoardModel)
        {

        }
    }
}
