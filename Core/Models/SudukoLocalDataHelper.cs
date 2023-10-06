using SQLite;
using SudokuSharp;
using System.Collections.ObjectModel;

namespace Core.Models
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
    }

    public class sample
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string cap { get; set; }
    }

    [Table("sudukoInstance")]
    public class SudukoLocalDataHelper
    {
        [PrimaryKey,Unique,Column("id")]
        public string BoardSavedName { get; set; }
        [Column("elapsed")]
        public long elapsed { get; set; }
        [Column("currentboardInstance")]
        public string CurrentBoardInstance { get; set; }
        [Column("solvedBoard")]
        public string SolvedBoard {  get; set; }
        [Column("generatedBoard")]
        public string GeneratedBoard { get; set; }
    }
}
