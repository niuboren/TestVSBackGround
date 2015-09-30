using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace NBR.VSBackGroundPackage
{
    public class ImageCatchManager
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

        private ImageCatchManager()
        {
            mCatchDic = new Dictionary<string, BitmapImage>();
            LoadDefaultPath();
        }

        /// <summary>
        /// 加载本地存储的路径
        /// </summary>
        private void LoadDefaultPath()
        {
        }

        public BitmapImage GetImage(string key)
        {
            if (mCatchDic.ContainsKey(key))
                return mCatchDic[key];

            var source = new BitmapImage();
            // have to be done in this way to get rid of file sharing problem
            source.BeginInit();
            source.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            source.CacheOption = BitmapCacheOption.OnLoad;

            source.UriSource = new Uri(GetPath());
            source.EndInit();

            return source;
        }

        public string GetPath()
        {
            string dirpath = GetDirPath();
            DirectoryInfo di = new DirectoryInfo(dirpath);
            FileInfo[] arr = di.GetFiles();
            List<string> pathList = new List<string>();
            foreach (FileInfo fi in arr)
            {
                if (fi.Extension == ".png")
                {
                    pathList.Add(fi.FullName);
                }
            }
            if (pathList.Count < 1) return "";

            Random r = new Random((int)DateTime.Now.Ticks);
            int index = r.Next(pathList.Count - 1);
            return pathList[index];
        }

        private string GetDirPath()
        {
            return VSBackGroundSetting.Default.BackGroundPath;
        }

        public void SetDirPath(string p)
        {
            VSBackGroundSetting.Default.BackGroundPath = p;
        }
    }
}