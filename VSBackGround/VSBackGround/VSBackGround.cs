using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Editor;
using EnvDTE;
using System.Windows.Media.Imaging;
using System;
using System.IO;
using System.Collections.Generic;

namespace VSBackGround
{
    /// <summary>
    /// Adornment class that draws a square box in the top right hand corner of the viewport
    /// </summary>
    class VSBackGround
    {
        private static string BasePath = "D:/001";

        private ImageBrush smImageBrush = new ImageBrush();

        private Canvas mImageSurface;
        private IWpfTextView _view;
        private IAdornmentLayer _adornmentLayer;

        //private VSBackGroundTool mToolWnd;

        private void RefreshBackgroundBrush()
        {
            smImageBrush.ImageSource = null;

            try
            {
                var source = new BitmapImage();
                // have to be done in this way to get rid of file sharing problem
                source.BeginInit();
                source.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                source.CacheOption = BitmapCacheOption.OnLoad;

                DirectoryInfo di = new DirectoryInfo(BasePath);
                FileInfo[] arr = di.GetFiles();
                List<string> pathList = new List<string>();
                foreach (FileInfo fi in arr)
                {
                    if (fi.Extension == ".png")
                    {
                        pathList.Add(fi.FullName);
                    }
                }

                Random r = new Random((int)DateTime.Now.Ticks);
                int index = r.Next(pathList.Count - 1);
                string truepath = pathList[index];

                source.UriSource = new Uri(truepath);
                //source.UriSource = new Uri("D:/001/58.png");
                source.EndInit();

                smImageBrush.ImageSource = source;

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
