namespace SudokuGame.CommonControls
{
    /// <summary>
    /// label control for sudoku cell label
    /// </summary>
    /// <seealso cref="Microsoft.Maui.Controls.Label" />
    public class LabelControl : Label
    {
        /// <summary>
        /// The is cell wrong property
        /// </summary>
        public static readonly BindableProperty isCellWrongProperty =
            BindableProperty.Create(nameof(isCellWrong), typeof(bool), typeof(bool), null);

        /// <summary>
        /// The is locked property
        /// </summary>
        public static readonly BindableProperty isLockedProperty =
            BindableProperty.Create(nameof(isLocked), typeof(bool), typeof(bool), null);

        /// <summary>
        /// The cell original value
        /// </summary>
        public static readonly BindableProperty cellOriginalValueProperty =
            BindableProperty.Create(nameof(cellOriginalValue), typeof(string), typeof(string), null);

        /// <summary>
        /// Gets or sets the is cell wrong.
        /// </summary>
        /// <value>
        /// The is cell wrong.
        /// </value>
        public bool isCellWrong
        {
            get { return (bool)GetValue(isCellWrongProperty); }
            set { SetValue(isCellWrongProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is locked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is locked; otherwise, <c>false</c>.
        /// </value>
        public bool isLocked
        {
            get { return (bool)GetValue(isLockedProperty); }
            set { SetValue(isLockedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the cell original value.
        /// </summary>
        /// <value>
        /// The cell original value.
        /// </value>
        public string cellOriginalValue
        {
            get { return (string)GetValue(cellOriginalValueProperty); }
            set { SetValue(cellOriginalValueProperty, value); }
        }
    }
}
