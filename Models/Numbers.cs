using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SudokuGame.Models
{
    public class Numbers : INotifyPropertyChanged
    {
        public int rrow { get; set; }
        public int ccol { get; set; }
        private string _number;
        public string number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(number));
            }
        }

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged();
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
