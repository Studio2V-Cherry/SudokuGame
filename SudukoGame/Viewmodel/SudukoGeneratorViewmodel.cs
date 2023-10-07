using Core.CrashlyticsHelpers;
using Core.Models;
using Core.TimerHelpers;
using Microsoft.AppCenter.Crashes;
using SudokuGame.CoreLogics;
using SudokuGame.Models;
using SudokuSharp;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SudokuGame.Viewmodel
{
    /// <summary>
    /// suduko generator viewmodel
    /// </summary>
    /// <seealso cref="SudokuGame.Viewmodel.BaseViewmodel" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SudukoGeneratorViewmodel : BaseViewmodel
    {

        #region fields
        /// <summary>
        /// The instance
        /// </summary>
        private static SudukoGeneratorViewmodel _instance;

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

        /// <summary>
        /// The timing
        /// </summary>
        private string _timing;

        /// <summary>
        /// The is number selected
        /// </summary>
        private bool isNumberSelected;

        /// <summary>
        /// The is loading
        /// </summary>
        private bool _isLoading;

        /// <summary>
        /// The mark error
        /// </summary>
        private bool _markError;

        /// <summary>
        /// The suduko board model
        /// </summary>
        private ObservableCollection<SudukoBoardModel> _sudukoBoardModel;

        /// <summary>
        /// The fastsuduko board model
        /// </summary>
        private List<SudukoBoardModel> _fastsudukoBoardModel;

        /// <summary>
        /// The numbers
        /// </summary>
        private ObservableCollection<Numbers> _numbers;
        /// <summary>
        /// Gets the puzzle generator.
        /// </summary>
        /// <value>
        /// The puzzle generator.
        /// </value>
        private PuzzleGenerator _puzzleGenerator => PuzzleGenerator.Instance;

        /// <summary>
        /// Gets or sets the selected numbers.
        /// </summary>
        /// <value>
        /// The selected numbers.
        /// </value>
        private Numbers selectedNumbers { get; set; }
        #endregion

        #region property
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static SudukoGeneratorViewmodel instance
        {
            get
            {
                return _instance = _instance ?? new SudukoGeneratorViewmodel();
            }
        }

        /// <summary>
        /// Gets or sets the sudoku board.
        /// </summary>
        /// <value>
        /// The sudoku board.
        /// </value>
        public Board SudokuBoard { get; set; }

        /// <summary>
        /// Gets or sets the suduko board generated.
        /// </summary>
        /// <value>
        /// The suduko board generated.
        /// </value>
        public Board SudukoBoardGenerated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in generation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in generation; otherwise, <c>false</c>.
        /// </value>
        public bool IsInGeneration
        {
            get => _isInGeneration;
            set
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
        /// Gets or sets the fast suduko board model.
        /// </summary>
        /// <value>
        /// The fast suduko board model.
        /// </value>
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

        #endregion

        #region commands
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
        public ICommand regenerateSudokuCommand => new Command(async () => await RegenerateSudoku());

        /// <summary>
        /// Gets the undo suduko command.
        /// </summary>
        /// <value>
        /// The undo suduko command.
        /// </value>
        public ICommand undoSudukoCommand => new Command(SudukoUndoOperation);
        #endregion

        #region privateMethods
        /// <summary>
        /// Resets all cells.
        /// </summary>
        private void resetAllCells()
        {
            SudukoBoardModel.All(x =>
            {
                x.IsSelected = false;
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
                CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
                if (selectedNumbers == null)
                    return;
                if (!sudukoBoardModel.isLocked)
                {
                    sudukoBoardModel.CellVal = selectedNumbers.number;
                    SudokuBoard[sudukoBoardModel.Cell] = int.Parse(selectedNumbers.number);
                    SudukoAddOperation(sudukoBoardModel.Cell, selectedNumbers.number);

                    sudukoBoardModel.CheckOriginalValue(MarkError);
                    isSudukoSolved();
                }
                highlightNumberFrames(selectedNumbers);
                highlightFrames(selectedNumbers);
            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
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
                    x.IsSelected = true;
                return true;
            });
        }

        /// <summary>
        /// Highlights the number frames.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        private void highlightNumberFrames(Numbers numbers)
        {
            SudukoBoardModel.All(x =>
            {
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
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
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
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
            Numbers.All(x =>
            {
                x.BackgroundColor = Colors.White;
                return true;
            });
            selectedNumbers = null;
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
        /// Determines whether [is suduko solved].
        /// </summary>
        private async void isSudukoSolved()
        {
            if (SudokuBoard.IsSolved)
            {
                CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
                TimerHelpers.stopTimer();
                await Application.Current.MainPage.DisplayAlert("Sudoku", $"Sudoku Solved Successfully in {Timing}", "Ok");
                IsLoading = true;
                await PopulateSuduko();
                IsLoading = false;
            }
        }

        /// <summary>
        /// Resets the populate numbers.
        /// </summary>
        private void ResetPopulateNumbers()
        {
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
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
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
            int region = 1;
            int places = 0;
            for (int m = 0; m < 3; m++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        FastSudukoBoardModel[places++].cellRegion = region;
                    }
                    for (int k = 0; k < 3; k++)
                    {
                        FastSudukoBoardModel[places++].cellRegion = region + 1;
                    }
                    for (int l = 0; l < 3; l++)
                    {
                        FastSudukoBoardModel[places++].cellRegion = region + 2;
                    }
                }
                region += 3;
            }
        }
        /// <summary>
        /// Regenerates the sudoku.
        /// </summary>
        private async Task RegenerateSudoku()
        {
            try
            {
                CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
                await PopulateSuduko();
                if (Instance.IsSudokuHistorySelected)
                {
                    TimerHelpers.resumeTimer();
                }
                else
                {
                    TimerHelpers.startTimer();
                }
                Instance.IsSudokuHistorySelected = false;
                Crashes.GenerateTestCrash();
            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
            }
        }



        /// <summary>
        /// Sudukoes the add operation.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="value">The value.</param>
        private void SudukoAddOperation(int cell, string value)
        {
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
            try
            {
                SudukoBoardModelsStack.Push(new Tuple<int, string>(cell, value));
                IsUndoEnabled = SudukoBoardModelsStack?.Any() ?? false;
            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
            }
        }

        /// <summary>
        /// Sudukoes the undo operation.
        /// </summary>
        private void SudukoUndoOperation()
        {
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
            try
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
            catch (Exception e)
            {
                CrashLogger.LogException(e);
            }
        }

        /// <summary>
        /// Populates the grid.
        /// </summary>
        private void PopulateGrid()
        {
            SudukoBoardModel = new ObservableCollection<SudukoBoardModel>();
            FastSudukoBoardModel = new List<SudukoBoardModel>();
            for (int i = 0; i < 81; i++)
            {
                FastSudukoBoardModel.Add(new SudukoBoardModel());
            }
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

        #endregion

        /// <summary>
        /// Prevents a default instance of the <see cref="SudukoGeneratorViewmodel" /> class from being created.
        /// </summary>
        public SudukoGeneratorViewmodel()
        {
            SudukoBoardModelsStack = new Stack<Tuple<int, string>>();
            FastSudukoBoardModel = new List<SudukoBoardModel>();
        }

        #region publicProperties

        /// <summary>
        /// Marks the errors.
        /// </summary>
        /// <param name="check">if set to <c>true</c> [check].</param>
        public void markErrors(bool check)
        {
            MarkError = check;
            SudukoBoardModel.All(x =>
            {
                x.CheckOriginalValue(check);
                return true;
            });
        }

        /// <summary>
        /// Configures the page.
        /// </summary>
        public void configurePage()
        {
            populateGrids();
        }

        /// <summary>
        /// Populates the grids.
        /// </summary>
        public void populateGrids()
        {
            ResetPopulateNumbers();
            PopulateGrid();
        }

        /// <summary>
        /// Firsts the time suduko.
        /// </summary>
        public async Task FirstTimeSuduko()
        {
            IsInGeneration = true;
            await PopulateSuduko();
            PopulateRegions();
        }

        /// <summary>
        /// Populates the suduko.
        /// </summary>
        public async Task PopulateSuduko()
        {
            CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
            if (IsInGeneration)
            {
                IsInGeneration = false;

                var puzzle = await _puzzleGenerator.GeneratePuzzleAsync(FastSudukoBoardModel);


                SolvedSudokuBoard = puzzle.Item1;
                SudokuBoard = new Board(puzzle.Item2);
                SudukoBoardGenerated = new Board(puzzle.Item2);
                FastSudukoBoardModel = puzzle.Item3;
                SudukoBoardModelsStack = new Stack<Tuple<int, string>>();
                SudukoUndoOperation();
                TimerHelpers.UpdateTimer += TimerHelpers_UpdateTimer;
                SudukoBoardModel = new ObservableCollection<SudukoBoardModel>(FastSudukoBoardModel);

                resetNumberSelections();
                IsInGeneration = true;
            }
        }

        /// <summary>
        /// Savesudukoes this instance.
        /// </summary>
        public async Task savesuduko()
        {
            await _puzzleGenerator.savePuzzle(SolvedSudokuBoard, SudukoBoardGenerated, SudukoBoardModel);
        }

        #endregion
    }
}
