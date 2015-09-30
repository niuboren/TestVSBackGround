using EnvDTE;
using Microsoft.VisualStudio.Text.Editor;
using System.Windows.Controls;
using System.Windows.Media;

namespace NBR.VSBackGroundPackage
{
    /// <summary>
    /// Adornment class that draws a square box in the top right hand corner of the viewport
    /// </summary>
    internal class VSBackGround
    {
        private ImageBrush smImageBrush = new ImageBrush();

        private Canvas mImageSurface;
        private IWpfTextView _view;
        private IAdornmentLayer _adornmentLayer;
        private _DTE _dte;

        //private VSBackGroundTool mToolWnd;

        private void RefreshBackgroundBrush()
        {
            smImageBrush.ImageSource = null;

            try
            {
                smImageBrush.ImageSource = ImageCatchManager.Instance.GetImage(_dte.ActiveDocument.ActiveWindow.Document.Name);
                smImageBrush.Stretch = Stretch.Uniform;
                smImageBrush.Opacity = 0.5f;
                smImageBrush.AlignmentX = AlignmentX.Right;
                smImageBrush.AlignmentY = AlignmentY.Bottom;
            }
            catch { }
        }

        /// <summary>
        /// Creates a square image and attaches an event handler to the layout changed event that
        /// adds the the square in the upper right-hand corner of the TextView via the adornment layer
        /// </summary>
        /// <param name="view">The <see cref="IWpfTextView"/> upon which the adornment will be drawn</param>
        public VSBackGround(IWpfTextView view, DTE dte)
        {
            _view = view;
            _dte = dte;
            //mToolWnd = new VSBackGroundTool();
            //mToolWnd.InitDTE(dte);

            //Grab a reference to the adornment layer that this adornment should be added to
            _adornmentLayer = view.GetAdornmentLayer("VSBackGround");

            InitializeImageAdornment();

            _view.ViewportHeightChanged += delegate { this.onSizeChange(); };
            _view.ViewportWidthChanged += delegate { this.onSizeChange(); };
            _view.GotAggregateFocus += delegate { this.onSizeChange(); };
        }

        private void InitializeImageAdornment()
        {
            mImageSurface = new Canvas();
            RenderOptions.SetBitmapScalingMode(mImageSurface, BitmapScalingMode.Linear); // better look

            RefreshBackgroundBrush();

            mImageSurface.Background = smImageBrush;
        }

        public void onSizeChange()
        {
            //clear the adornment layer of previous adornments
            _adornmentLayer.RemoveAllAdornments();

            //mToolWnd.OnSizeChange(_view);

            //add the image to the adornment layer and make it relative to the viewport
            //_adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, mToolWnd, null);
            _adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, mImageSurface, null);

            mImageSurface.Width = _view.ViewportWidth;
            mImageSurface.Height = _view.ViewportHeight;
        }
    }
}