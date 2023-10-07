using Android.Content;
using Microsoft.Maui.Controls.Compatibility.Platform.Android.FastRenderers;
using Microsoft.Maui.Controls.Platform;
using SudokuGame.CommonControls;
using System.ComponentModel;
using Color = Android.Graphics.Color;

namespace SudokuGame.Platforms.Android.ControlRendererrers
{
    /// <summary>
    /// frame renderer for platform
    /// </summary>
    /// <seealso cref="Microsoft.Maui.Controls.Compatibility.Platform.Android.FastRenderers.FrameRenderer" />
    public class FrameControlRenderrer : FrameRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameControlRenderrer"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public FrameControlRenderrer(Context context) : base(context)
        {

        }

        /// <summary>
        /// Raises the <see cref="E:ElementChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ElementChangedEventArgs{Frame}"/> instance containing the event data.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var frame = e.NewElement as FrameControl;
                if (frame != null)
                {
                    frameBackgroundChange(frame);
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
                if (e.PropertyName == nameof(FrameControl.cellRegion) || 
                    e.PropertyName == nameof(FrameControl.SelectedBackground))
                {
                    _ = e.PropertyName;
                    frameBackgroundChange(sender as FrameControl);
                }
            }
        }

        /// <summary>
        /// Frames the background change.
        /// </summary>
        /// <param name="frame">The frame.</param>
        private void frameBackgroundChange(FrameControl frame)
        {
            if (frame.SelectedBackground)
            {
                Control.SetBackgroundColor(Color.ParseColor("#E9DDD4"));
            }
            else if (frame.cellRegion % 2 == 1)
            {
                Control.SetBackgroundColor(Color.ParseColor("#F2F1F0"));
            }
            else
            {
                Control.SetBackgroundColor(Color.Transparent);
            }
        }
    }
}