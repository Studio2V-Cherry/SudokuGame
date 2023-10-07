using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SudukoBoardModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The cell region
        /// </summary>
        private int _cellRegion;
        /// <summary>
        /// Gets or sets the cell region.
        /// </summary>
        /// <value>
        /// The cell region.
        /// </value>
        public int cellRegion
        {
            get => _cellRegion;
            set
            {
                _cellRegion = value;
                OnPropertyChanged();
            }
        }
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
        /// The is selected
        /// </summary>
        private bool _isSelected;


        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// The is cell wrong
        /// </summary>
        private bool _isCellWrong;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cell wrong.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is cell wrong; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool IsCellWrong
        {
            get => _isCellWrong;
            set
            {
                _isCellWrong = value;
                OnPropertyChanged(nameof(IsCellWrong));
            }
        }

        /// <summary>
        /// The is locked
        /// </summary>
        private bool _isLocked;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is locked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is locked; otherwise, <c>false</c>.
        /// </value>
        public bool isLocked
        {
            get => _isLocked;
            set
            {
                _isLocked = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The original cell value
        /// </summary>
        private string _originalCellVal;
        /// <summary>
        /// Sets the original cell value.
        /// </summary>
        /// <value>
        /// The original cell value.
        /// </value>
        public string OriginalCellVal
        {
            get => _originalCellVal;
            set
            {
                _originalCellVal = value;
                OnPropertyChanged();
            }
        }

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
            if (check)
            {
                IsCellWrong = !CellVal.Equals(OriginalCellVal);
            }
            else
            {
                IsCellWrong = false;
            }
        }
    }
}
