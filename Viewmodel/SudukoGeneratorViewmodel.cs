using SudokuGame.Helpers;
using SudokuGame.Models;
using SudokuSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SudokuGame.Viewmodel
{
    /// <summary>
    /// suduko generator viewmodel
    /// </summary>
    /// <seealso cref="SudokuGame.Viewmodel.BaseViewmodel" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SudukoGeneratorViewmodel : BaseViewmodel, INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the sudoku board.
        /// </summary>
        /// <value>
        /// The sudoku board.
        /// </value>
        public Board SudokuBoard { get; set; }

        /// <summary>
        /// Gets or sets the solved sudoku board.
        /// </summary>
        /// <value>
        /// The solved sudoku board.
        /// </value>
        private Board SolvedSudokuBoard { get; set; }

        /// <summary>
        /// Gets or sets the suduko board models stack.
        /// </summary>
        /// <value>
        /// The suduko board models stack.
        /// </value>
        private Stack<Tuple<int, string>> SudukoBoardModelsStack { get; set; }

        /// <summary>
        /// The is undo enabled
        /// </summary>
        private bool _isUndoEnabled;

        /// <summary>
        /// The is in generation
        /// </summary>
        private bool _isInGeneration;

        public bool IsInGeneration
        {
            get=>_isInGeneration; 
            private set
            {
                _isInGeneration = value;
                OnPropertyChanged(nameof(IsInGeneration));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is undo enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is undo enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsUndoEnabled
        {
            get => _isUndoEnabled;
            set
            {
                _isUndoEnabled = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The timing
        /// </summary>
        private string _timing;

        /// <summary>
        /// Gets or sets the timing.
        /// </summary>
        /// <value>
        /// The timing.
        /// </value>
        public string Timing
        {
            get => _timing;
            set
            {
                _timing = value;
                OnPropertyChanged(nameof(Timing));
            }
        }

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

        /// <summary>
        /// The is number selected
        /// </summary>
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

        /// <summary>
        /// The mark error
        /// </summary>
        private bool _markError;

        /// <summary>
        /// Gets or sets a value indicating whether [mark error].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [mark error]; otherwise, <c>false</c>.
        /// </value>
        public bool MarkError
        {
            get => _markError;
            set
            {
                _markError = value;
                OnPropertyChanged(nameof(MarkError));
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

        private List<SudukoBoardModel> _fastsudukoBoardModel;

        public List<SudukoBoardModel> FastSudukoBoardModel
        {
            get => _fastsudukoBoardModel;
            set
            {
                _fastsudukoBoardModel = value;
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

        /// <summary>
        /// Gets the number long selected command.
        /// </summary>
        /// <value>
        /// The number long selected command.
        /// </value>
        public ICommand numberLongSelectedCommand => new Command<Numbers>(numberLongSelected);

        /// <summary>
        /// Gets the validate sudoku command.
        /// </summary>
        /// <value>
        /// The validate sudoku command.
        /// </value>
        public ICommand validateSudokuCommand => new Command(validateSudoku);

        /// <summary>
        /// Gets the regenerate sudoku command.
        /// </summary>
        /// <value>
        /// The regenerate sudoku command.
        /// </value>
        public ICommand regenerateSudokuCommand => new Command(PopulateSuduko);

        /// <summary>
        /// Gets the undo suduko command.
        /// </summary>
        /// <value>
        /// The undo suduko command.
        /// </value>
        public ICommand undoSudukoCommand => new Command(SudukoUndoOperation);
        /// <summary>
        /// Resets all cells.
        /// </summary>
        private void resetAllCells()
        {
            SudukoBoardModel.All(x =>
            {
                x.SelectedColor = Colors.Transparent;
                return true;
            });
        }

        /// <summary>
        /// Resets all number cells.
        /// </summary>
        private void resetAllNumberCells()
        {
            SudukoBoardModel.All(x =>
            {
                if (x.isLocked)
                    x.TextColor = Colors.DarkGray;
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
                if (x.CellVal == sudukoBoardModel.CellVal && !string.IsNullOrEmpty(x.CellVal))
                {
                    x.BackgroundColor = Color.FromArgb("#C5C5C5");
                }
                return true;
            });
            //SudukoBoardModel.All(x =>
            //{
            //    if (x.cellRegion == cellRegion)
            //        x.BackgroundColor = Color.FromArgb("#629BFB");
            //    return true;
            //});
        }

        /// <summary>
        /// Frames the selected model.
        /// </summary>
        /// <param name="sudukoBoardModel">The suduko board model.</param>
        private void frameSelectedModel(SudukoBoardModel sudukoBoardModel)
        {
            try
            {
                //resetAllCells();
                // sudukoBoardModel.BackgroundColor = Colors.Green;
                if (!sudukoBoardModel.isLocked)
                {
                    sudukoBoardModel.CellVal = selectedNumbers.number;
                    SudokuBoard[sudukoBoardModel.Cell] = int.Parse(selectedNumbers.number);
                    SudukoAddOperation(sudukoBoardModel.Cell, selectedNumbers.number);
                    //validateSudoku();
                    sudukoBoardModel.CheckOriginalValue(MarkError);
                    isSudukoSolved();
                }
                highlightNumberFrames(selectedNumbers);
                highlightFrames(selectedNumbers);
                //highlightRegions(sudukoBoardModel);
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
                    x.SelectedColor = Color.FromArgb("#E9DDD4");
                return true;
            });
        }

        /// <summary>
        /// Highlights the number frames.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        private void highlightNumberFrames(Numbers numbers)
        {
            resetAllNumberCells();
            SudukoBoardModel.All(x =>
            {
                if (x.CellVal == numbers.number && !string.IsNullOrEmpty(x.CellVal))
                {
                    x.TextColor = Colors.Black;
                }
                if (!x.isLocked)
                {
                    x.CheckOriginalValue(MarkError);
                }
                return true;
            });
        }

        /// <summary>
        /// Numbers the selected model.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        private void numberSelectedModel(Numbers numbers)
        {
            isNumberSelected = !isNumberSelected;
            if (numbers.number != (selectedNumbers?.number ?? ""))
            {
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
                highlightNumberFrames(selectedNumbers);
                highlightFrames(numbers);
                isNumberSelected = true;
            }
            else
            {
                resetNumberSelections();
            }
        }

        /// <summary>
        /// Resets the number selections.
        /// </summary>
        private void resetNumberSelections()
        {
            Numbers.All(x =>
            {
                x.BackgroundColor = Colors.White;
                return true;
            });
            selectedNumbers = null;
            resetAllNumberCells();
            resetAllCells();
        }

        /// <summary>
        /// Numbers the long selected.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        private void numberLongSelected(Numbers numbers)
        {
            isNumberSelected = !isNumberSelected;
            if (numbers.number != selectedNumbers.number)
            {
                Numbers.All(x =>
                {
                    if (x.number == numbers.number)
                    {
                        x.BackgroundColor = Color.FromArgb("#60f78e");
                        selectedNumbers = numbers;
                    }
                    else
                    {
                        x.BackgroundColor = Colors.White;
                    }
                    return true;
                });
                isNumberSelected = true;
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
            //[todo] charan
        }

        /// <summary>
        /// Marks the errors.
        /// </summary>
        /// <param name="check">if set to <c>true</c> [check].</param>
        public void markErrors(bool check)
        {
            SudukoBoardModel.All(x =>
            {
                x.CheckOriginalValue(check);
                return true;
            });
        }

        /// <summary>
        /// Determines whether [is suduko solved].
        /// </summary>
        private async void isSudukoSolved()
        {
            if (SudokuBoard.IsSolved)
            {
                TimerHelpers.stopTimer();
                await Application.Current.MainPage.DisplayAlert("Sudoku", $"Sudoku Solved Successfully in {Timing}", "Ok");
                IsLoading = true;
                PopulateSuduko();
                IsLoading = false;
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="SudukoGeneratorViewmodel" /> class from being created.
        /// </summary>
        private SudukoGeneratorViewmodel()
        {
            random = new Random();
            SudukoBoardModelsStack = new Stack<Tuple<int, string>>();
            FastSudukoBoardModel = new List<SudukoBoardModel>();
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
                        if (region % 2 == 1)
                        {
                            FastSudukoBoardModel[places].BackgroundColor = Color.FromArgb("#F2F1F0");
                        }
                        FastSudukoBoardModel[places++].cellRegion = region;
                    }
                    for (int k = 0; k < 3; k++)
                    {
                        if ((region + 1) % 2 == 1)
                        {
                            FastSudukoBoardModel[places].BackgroundColor = Color.FromArgb("#F2F1F0");
                        }
                        FastSudukoBoardModel[places++].cellRegion = region + 1;
                    }
                    for (int l = 0; l < 3; l++)
                    {
                        if ((region + 2) % 2 == 1)
                        {
                            FastSudukoBoardModel[places].BackgroundColor = Color.FromArgb("#F2F1F0");
                        }
                        FastSudukoBoardModel[places++].cellRegion = region + 2;
                    }
                }
                region += 3;
            }
        }

        public void populateGrids()
        {
            ResetPopulateNumbers();
            PopulateGrid();
        }

        /// <summary>
        /// Firsts the time suduko.
        /// </summary>
        public void FirstTimeSuduko()
        {
            //async Task runPopulateGrid() 
            //{
            //    await Task.Run(PopulateSuduko);
            //    await Task.Run(PopulateRegions);
            //}
            //Device.BeginInvokeOnMainThread(async() =>
            //{
            //    await runPopulateGrid();
            //});
            IsInGeneration =true;
            PopulateSuduko();
            PopulateRegions();
        }

        /// <summary>
        /// Populates the suduko.
        /// </summary>
        public void PopulateSuduko()
        {
            if (IsInGeneration)
            {
                IsInGeneration = false;
                SolvedSudokuBoard = Factory.Solution(random.Next(6, 25));
                SudokuBoard = Factory.Puzzle(SolvedSudokuBoard, random, QuadsToCut: random.Next(1, 8), random.Next(1, 8), random.Next(1, 8));
                ReturnPopulatedGrid(SudokuBoard);
                IsInGeneration = true;
            }
        }

        /// <summary>
        /// Sudukoes the add operation.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="value">The value.</param>
        private void SudukoAddOperation(int cell, string value)
        {
            SudukoBoardModelsStack.Push(new Tuple<int, string>(cell, value));
            IsUndoEnabled = SudukoBoardModelsStack?.Any() ?? false;
        }

        /// <summary>
        /// Sudukoes the undo operation.
        /// </summary>
        private void SudukoUndoOperation()
        {
            if (SudukoBoardModelsStack.Count > 0)
            {
                var isPopped = SudukoBoardModelsStack.TryPop(out Tuple<int, string> sudukoPop);
                if (isPopped)
                {
                    var Poppedcell = sudukoPop.Item1;
                    var Poppedvalue = sudukoPop.Item2;

                    SudukoBoardModel[Poppedcell].CellVal = string.Empty;
                    var parellelStack = new Stack<Tuple<int, string>>();
                    while (SudukoBoardModelsStack.TryPop(out Tuple<int, string> sudukoPopped))
                    {
                        parellelStack.Push(sudukoPopped);
                        if (sudukoPopped.Item1 == Poppedcell)
                        {
                            SudukoBoardModel[Poppedcell].CellVal = sudukoPopped.Item2;
                            break;
                        }
                    }
                    while (parellelStack.TryPop(out Tuple<int, string> sudukoPopped))
                    {
                        SudukoBoardModelsStack.Push(sudukoPopped);
                    }
                    resetNumberSelections();
                }
            }
            IsUndoEnabled = SudukoBoardModelsStack?.Any() ?? false;
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
           // if (FastSudukoBoardModel == null)
            {
                SudukoBoardModel = new ObservableCollection<SudukoBoardModel>();
                FastSudukoBoardModel = new List<SudukoBoardModel>();
                for (int i = 0; i < 81; i++)
                {
                    FastSudukoBoardModel.Add(new Models.SudukoBoardModel());
                }
            }
        }

        /// <summary>
        /// Returns the populated grid.
        /// </summary>
        /// <param name="suduko">The suduko.</param>
        private void ReturnPopulatedGrid(Board suduko)
        {
            int places = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++, places++)
                {
                    FastSudukoBoardModel[places].rrow = j;
                    FastSudukoBoardModel[places].TextColor = Colors.DimGray;
                    FastSudukoBoardModel[places].isLocked = (suduko[places] != 0);
                    FastSudukoBoardModel[places].ccol = i;
                    FastSudukoBoardModel[places].Cell = places;
                    FastSudukoBoardModel[places].OriginalCellVal = SolvedSudokuBoard[places].ToString();
                    FastSudukoBoardModel[places].CellVal = (suduko[places] == 0) ? "" : suduko[places].ToString();
                }
            }

            //Start _stopwatch
            //OnPropertyChanged(nameof(SudukoBoardModel));
            resetNumberSelections();
            TimerHelpers.startTimer();
            SudukoBoardModelsStack = new Stack<Tuple<int, string>>();
            SudukoUndoOperation();
            TimerHelpers.UpdateTimer += TimerHelpers_UpdateTimer;
            SudukoBoardModel = new ObservableCollection<SudukoBoardModel>(FastSudukoBoardModel);
        }

        /// <summary>
        /// Handles the UpdateTimer event of the TimerHelpers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TimerHelperEventArgs" /> instance containing the event data.</param>
        private void TimerHelpers_UpdateTimer(object sender, TimerHelperEventArgs e)
        {
            Timing = e.Timer;
        }
    }
}
