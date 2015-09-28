using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace VSBackGround
{
    internal class ImageCatchManager
    {
        private static ImageCatchManager _instance;
        public static ImageCatchManager Instance
        {
            get
            {
                if (_instance == null) _instance = new ImageCatchManager();
                return _instance;
            }
        }

        private Dictionary<string, BitmapImage> mCatchDic;

        private ImageData mImageData;


        private ImageCatchManager()
        {
            mCatchDic = new Dictionary<string, BitmapImage>();
            mImageData = new ImageData();
        }

        public BitmapImage GetImage(string key)
        {
            if (mCatchDic.ContainsKey(key))
                return mCatchDic[key];

            BitmapImage source = new BitmapImage();
            source.BeginInit();
            source.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            source.CacheOption = BitmapCacheOption.OnLoad;
            source.UriSource = new Uri(ImageData.DefaultPath);
            source.EndInit();

            return null;
        }



    }
}
