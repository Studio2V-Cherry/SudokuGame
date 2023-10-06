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
    /// 
    /// </summary>
    /// <seealso cref="SudokuGame.Viewmodel.BaseViewmodel" />
    public class PuzzleGenerator : BaseViewmodel
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static PuzzleGenerator _instance;
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
        /// Initializes a new instance of the <see cref="PuzzleGenerator"/> class.
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
        /// Saves the puzzle.
        /// </summary>
        /// <param name="solvedSuduko">The solved suduko.</param>
        /// <param name="CurrentGeneratedSuduko">The current generated suduko.</param>
        /// <param name="instance">The instance.</param>
        public async Task savePuzzle(Board solvedSuduko, Board CurrentGeneratedSuduko, ObservableCollection<SudukoBoardModel> instance)
        {
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
        }


        /// <summary>
        /// Generates the puzzle asynchronous.
        /// </summary>
        /// <param name="FastSudukoBoardModel">The fast suduko board model.</param>
        /// <returns></returns>
        public async Task<Tuple<Board, Board, List<SudukoBoardModel>>> GeneratePuzzleAsync(List<SudukoBoardModel> FastSudukoBoardModel)
        {
            Board SolvedSudokuBoard = new Board();
            Board SudokuBoard = new Board();

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
                for (int i = 0; i < 81; i++)
                {
                    FastSudukoBoardModelInstance[i].BackgroundColor = FastSudukoBoardModel[i].BackgroundColor;
                    if (SudokuBoardList[i] != 0 && $"{SudokuBoardList[i]}".Equals(FastSudukoBoardModelInstance[i].CellVal))
                    {
                        FastSudukoBoardModelInstance[i].isLocked = false;
                        FastSudukoBoardModelInstance[i].TextColor = Colors.DimGray;
                        FastSudukoBoardModelInstance[i].isLocked = true;
                    }
                    else
                    {

                    }
                }
                TimerHelpers.setTimer(storageHelper.FirstOrDefault().elapsed);

                return new Tuple<Board, Board, List<SudukoBoardModel>>(SolvedSudokuBoard, SudokuBoard, FastSudukoBoardModelInstance);
            }
            else
            {
                SolvedSudokuBoard = Factory.Solution(random.Next(6, 25));
                SudokuBoard = Factory.Puzzle(SolvedSudokuBoard, random, QuadsToCut: random.Next(1, 8), random.Next(1, 8), random.Next(1, 8));

                int places = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++, places++)
                    {
                        FastSudukoBoardModel[places].rrow = j;
                        FastSudukoBoardModel[places].TextColor = Colors.DimGray;
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
    }
}
