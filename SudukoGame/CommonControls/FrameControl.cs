namespace SudokuGame.CommonControls
{
    /// <summary>
    /// frame control for grid
    /// </summary>
    /// <seealso cref="Microsoft.Maui.Controls.Frame" />
    public class FrameControl : Frame
    {
        /// <summary>
        /// The cell region property
        /// </summary>
        public static readonly BindableProperty cellRegionProperty =
            BindableProperty.Create(nameof(cellRegion), typeof(int), typeof(int), null);

        /// <summary>
        /// The selected background property
        /// </summary>
        public static readonly BindableProperty SelectedBackgroundProperty =
            BindableProperty.Create(nameof(SelectedBackground), typeof(bool), typeof(bool), null);

        /// <summary>
        /// Gets or sets the cell region.
        /// </summary>
        /// <value>
        /// The cell region.
        /// </value>
        public int cellRegion
        {
            get { return (int)GetValue(cellRegionProperty); }
            set { SetValue(cellRegionProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [selected background].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [selected background]; otherwise, <c>false</c>.
        /// </value>
        public bool SelectedBackground
        {
            get { return (bool)GetValue(SelectedBackgroundProperty); }
            set { SetValue(SelectedBackgroundProperty, value); }
        }
    }
}
