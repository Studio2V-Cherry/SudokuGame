using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SudokuGame.Models
{
    /// <summary>
    /// numbers models
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class Numbers : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the rrow.
        /// </summary>
        /// <value>
        /// The rrow.
        /// </value>
        public int rrow { get; set; }
        /// <summary>
        /// Gets or sets the ccol.
        /// </summary>
        /// <value>
        /// The ccol.
        /// </value>
        public int ccol { get; set; }
        /// <summary>
        /// The number
        /// </summary>
        private string _number;
        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public string number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(number));
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
    }
}
