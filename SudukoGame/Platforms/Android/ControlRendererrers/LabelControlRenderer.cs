using Android.Content;
using Core.CrashlyticsHelpers;
using Microsoft.Maui.Controls.Compatibility.Platform.Android.FastRenderers;
using Microsoft.Maui.Controls.Platform;
using SudokuGame.CommonControls;
using System.ComponentModel;

namespace SudokuGame.Platforms.Android.ControlRendererrers
{
    /// <summary>
    /// label renderer for platform specific
    /// </summary>
    /// <seealso cref="Microsoft.Maui.Controls.Compatibility.Platform.Android.FastRenderers.LabelRenderer" />
    public class LabelControlRenderer : LabelRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabelControlRenderer"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LabelControlRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// Raises the <see cref="E:ElementChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ElementChangedEventArgs{Label}"/> instance containing the event data.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var label = e.NewElement as LabelControl;
                if (label != null)
                {
                    LabelConfigChange(label);
                }
            }
        }

        /// <summary>
        /// Called when [element property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                if (e.PropertyName == nameof(LabelControl.Text) ||
                    e.PropertyName == nameof(LabelControl.isCellWrong))
                {
                    LabelConfigChange(sender as LabelControl);
                }
                //{
                //    _ = e.PropertyName;
                //    //frameBackgroundChange(label);
                //}
            }
        }


        /// <summary>
        /// Labels the configuration change.
        /// </summary>
        /// <param name="label">The label.</param>
        private void LabelConfigChange(LabelControl label)
        {
            try
            {
                if (!string.IsNullOrEmpty(label.Text))
                {
                    if (label.isLocked)
                    {
                        label.TextColor = Colors.DimGray;
                    }
                    else if (label.isCellWrong && !label.Text.Equals(label.cellOriginalValue))
                    {
                        label.TextColor = Colors.Red;
                    }
                    else
                    {
                        label.TextColor = Colors.Black;
                    }
                }
            }
            catch (Exception e)
            {
                CrashLogger.LogException(e);
            }
        }
    }
}
