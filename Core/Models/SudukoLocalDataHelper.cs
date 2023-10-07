using SQLite;

namespace Core.Models
{
    /// <summary>
    /// suduko database helper
    /// </summary>
    [Table("sudukoInstance")]
    public class SudukoLocalDataHelper
    {
        /// <summary>
        /// Gets or sets the name of the board saved.
        /// </summary>
        /// <value>
        /// The name of the board saved.
        /// </value>
        [PrimaryKey, Unique, Column("id")]
        public string BoardSavedName { get; set; }
        /// <summary>
        /// Gets or sets the elapsed.
        /// </summary>
        /// <value>
        /// The elapsed.
        /// </value>
        [Column("elapsed")]
        public long elapsed { get; set; }
        /// <summary>
        /// Gets or sets the current board instance.
        /// </summary>
        /// <value>
        /// The current board instance.
        /// </value>
        [Column("currentboardInstance")]
        public string CurrentBoardInstance { get; set; }
        /// <summary>
        /// Gets or sets the solved board.
        /// </summary>
        /// <value>
        /// The solved board.
        /// </value>
        [Column("solvedBoard")]
        public string SolvedBoard { get; set; }
        /// <summary>
        /// Gets or sets the generated board.
        /// </summary>
        /// <value>
        /// The generated board.
        /// </value>
        [Column("generatedBoard")]
        public string GeneratedBoard { get; set; }
    }
}
