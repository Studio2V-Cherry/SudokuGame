using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SudokuGame.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SudukoBoardModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the cell region.
        /// </summary>
        /// <value>
        /// The cell region.
        /// </value>
        public int cellRegion { get; set; }
        /// <summary>
        /// The rrow
        /// </summary>
        private int _rrow;
        /// <summary>
        /// Gets or sets the rrow.
        /// </summary>
        /// <value>
        /// The rrow.
        /// </value>
        public int rrow
        {
            get => _rrow;
            set
            {
                _rrow = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The col
        /// </summary>
        private int _col;
        /// <summary>
        /// Gets or sets the ccol.
        /// </summary>
        /// <value>
        /// The ccol.
        /// </value>
        public int ccol
        {
            get => _col;
            set
            {
                _col = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the cell.
        /// </summary>
        /// <value>
        /// The cell.
        /// </value>
        public int Cell { get; set; }

        /// <summary>
        /// The cell value
        /// </summary>
        private string _cellVal;
        /// <summary>
        /// Gets or sets the cell value.
        /// </summary>
        /// <value>
        /// The cell value.
        /// </value>
        public string CellVal
        {
            get => _cellVal;
            set
            {
                _cellVal = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The background color
        /// </summary>
        private Color _backgroundColor;
        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The selected color
        /// </summary>
        private Color _selectedColor;

        /// <summary>
        /// Gets or sets the color of the selected.
        /// </summary>
        /// <value>
        /// The color of the selected.
        /// </value>
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The text color
        /// </summary>
        private Color _textColor;

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>
        /// The color of the text.
        /// </value>
        public Color TextColor
        {
            get => _textColor;
            set
            {
                if (!isLocked)
                {
                    _textColor = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is locked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is locked; otherwise, <c>false</c>.
        /// </value>
        public bool isLocked { get; set; }

        /// <summary>
        /// Sets the original cell value.
        /// </summary>
        /// <value>
        /// The original cell value.
        /// </value>
        public string OriginalCellVal { private get; set; }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Checks the original value.
        /// </summary>
        /// <param name="check">if set to <c>true</c> [check].</param>
        public void CheckOriginalValue(bool check)
        {
            if (!string.IsNullOrEmpty(CellVal) && check)
            {
                if (!CellVal.Equals(OriginalCellVal))
                {
                    TextColor = Colors.Red;
                }
                else
                {
                    TextColor = Colors.Black;
                }
            }
            else
            {
                TextColor = Colors.Black;
            }
        }
    }
}
