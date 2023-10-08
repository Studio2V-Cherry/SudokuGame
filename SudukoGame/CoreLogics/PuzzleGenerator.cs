using Core.CrashlyticsHelpers;
using Core.LocalStorageHelper;
using Core.Models;
using Core.TimerHelpers;
using SudokuGame.Viewmodel;
using SudokuSharp;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace SudokuGame.CoreLogics
{
    /// <summary>
    /// puzzle generator for resume and create
    /// </summary>
    /// <seealso cref="SudokuGame.Viewmodel.BaseViewmodel" />
    public class PuzzleGenerator : BaseViewmodel
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static PuzzleGenerator _instance;

        /// <summary>
        /// The solved sudoku board
        /// </summary>
        Board SolvedSudokuBoard = new Board();

        /// <summary>
        /// The sudoku board
        /// </summary>
        Board SudokuBoard = new Board();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static PuzzleGenerator Instance
        {
            get => _instance = _instance ?? new PuzzleGenerator();
        }
        /// <summary>
        /// Gets or sets the storage helper.
        /// </summary>
        /// <value>
        /// The storage helper.
        /// </value>
        private StorageHelper _storageHelper { get; set; } = StorageHelper.Instance;

        /// <summary>
        /// The random
        /// </summary>
        Random random;
        /// <summary>
        /// Initializes a new instance of the <see cref="PuzzleGenerator" /> class.
        /// </summary>
        public PuzzleGenerator()
        {
            random = new Random();
        }

        /// <summary>
        /// Gets if suduko history present.
        /// </summary>
        public async Task getIfSudukoHistoryPresent()
        {
            IsSudokuHistory = await _storageHelper.isDataPresent();
        }

        /// <summary>
        /// Boards the configure.
        /// </summary>
        /// <param name="board">The board.</param>
        public void BoardConfigure()
        {
            try
            {
                CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
                int solvedSudukoSeed = random.Next(6, 25);
                int quadsTocut = random.Next(1, 20);
                int pairsTocut = random.Next(1, 20);
                int singlesTocut = random.Next(1, 20);
                var _levelsModel = BaseViewmodel.Instance._levelsModel;
                //Easy or Default
                if (_levelsModel == null || ((_levelsModel?.Gamelevels ?? 0) == 0))
                {
                    solvedSudukoSeed = 20;
                    quadsTocut = 3;
                    pairsTocut = 3;
                    singlesTocut = 3;
                }
                //Moderate
                else if (_levelsModel.Gamelevels == 1)
                {
                    solvedSudukoSeed = 20;
                    quadsTocut = 5;
                    pairsTocut = 5;
                    singlesTocut = 5;
                }
                //challenging
                else if (_levelsModel.Gamelevels == 2)
                {
                    solvedSudukoSeed = 20;
                    quadsTocut = 20;
                    pairsTocut = 20;
                    singlesTocut = 20;
                }
                //Random Generated
                else if (_levelsModel.Gamelevels == 3)
                {
                    //Nothing to do here  
                }
                else
                {
                    //[todo] Let me generate suduko myself
                }
                SolvedSudokuBoard = Factory.Solution(solvedSudukoSeed);
                SudokuBoard = Factory.Puzzle(SolvedSudokuBoard, random, QuadsToCut: quadsTocut, pairsTocut, singlesTocut);

            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
            }  
        }

        /// <summary>
        /// Saves the puzzle.
        /// </summary>
        /// <param name="solvedSuduko">The solved suduko.</param>
        /// <param name="CurrentGeneratedSuduko">The current generated suduko.</param>
        /// <param name="instance">The instance.</param>
        public async Task savePuzzle(Board solvedSuduko, Board CurrentGeneratedSuduko, ObservableCollection<SudukoBoardModel> instance)
        {
            try
            {
                CrashLogger.TrackEvent(Core.Constants.loggerEnum.method);
                List<int> solvedSudukoList = new List<int>();
                List<int> CurrentGeneratedSudukoList = new List<int>();

                for (int i = 0; i < 81; i++)
                {
                    solvedSudukoList.Add(solvedSuduko[i]);
                    CurrentGeneratedSudukoList.Add(CurrentGeneratedSuduko[i]);
                }

                SudukoLocalDataHelper sudukoLocalDataHelper = new SudukoLocalDataHelper()
                {
                    SolvedBoard = JsonSerializer.Serialize(solvedSudukoList),
                    GeneratedBoard = JsonSerializer.Serialize(CurrentGeneratedSudukoList),
                    elapsed = TimerHelpers.getElapsed().Ticks,
                    CurrentBoardInstance = JsonSerializer.Serialize(instance)
                };
                var z = await _storageHelper.SaveInstanceAsync(sudukoLocalDataHelper);
                if (z != 0)
                {
                    IsSudokuHistory = true;
                    OnPropertyChanged(nameof(IsSudokuHistory));
                }
                else if (z == -1)
                {
                    //[todo] exception logged breadcrumb add MCToolkit
                }
            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
            }
        }


        /// <summary>
        /// Generates the puzzle asynchronous.
        /// </summary>
        /// <param name="FastSudukoBoardModel">The fast suduko board model.</param>
        /// <returns></returns>
        public async Task<Tuple<Board, Board, List<SudukoBoardModel>>> GeneratePuzzleAsync(List<SudukoBoardModel> FastSudukoBoardModel)
        {
            try
            {

                if (BaseViewmodel.Instance.IsSudokuHistorySelected)
                {
                    var storageHelper = await _storageHelper.GetSavedInstanceAsync();
                    var SolvedSudokuBoardList = JsonSerializer.Deserialize<List<int>>(storageHelper.FirstOrDefault().SolvedBoard);
                    var SudokuBoardList = JsonSerializer.Deserialize<List<int>>(storageHelper.FirstOrDefault().GeneratedBoard);
                    for (int i = 0; i < 81; i++)
                    {
                        SolvedSudokuBoard.PutCell(i, SolvedSudokuBoardList[i]);
                        SudokuBoard.PutCell(i, SudokuBoardList[i]);
                    }

                    var FastSudukoBoardModelInstance = JsonSerializer.Deserialize<List<SudukoBoardModel>>(storageHelper.FirstOrDefault().CurrentBoardInstance);

                    TimerHelpers.setTimer(storageHelper.FirstOrDefault().elapsed);

                    return new Tuple<Board, Board, List<SudukoBoardModel>>(SolvedSudokuBoard, SudokuBoard, FastSudukoBoardModelInstance);
                }
                else
                {
                    BoardConfigure();
                    int places = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++, places++)
                        {
                            FastSudukoBoardModel[places].rrow = j;
                            FastSudukoBoardModel[places].isLocked = (SudokuBoard[places] != 0);
                            FastSudukoBoardModel[places].ccol = i;
                            FastSudukoBoardModel[places].Cell = places;
                            FastSudukoBoardModel[places].OriginalCellVal = SolvedSudokuBoard[places].ToString();
                            FastSudukoBoardModel[places].CellVal = (SudokuBoard[places] == 0) ? "" : SudokuBoard[places].ToString();
                        }
                    }
                    TimerHelpers.resetTimer();
                    return new Tuple<Board, Board, List<SudukoBoardModel>>(SolvedSudokuBoard, SudokuBoard, FastSudukoBoardModel);
                }
            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
                return new Tuple<Board, Board, List<SudukoBoardModel>>(new Board(), new Board(), new List<SudukoBoardModel>());
            }
        }
    }
}
