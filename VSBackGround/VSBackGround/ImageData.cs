using System;
using System.Configuration;
using System.IO;

namespace VSBackGround
{
    internal class ImageData
    {
        public static string DefaultPath
        {
            get
            {
                return System.Environment.CurrentDirectory + "/Config";
            }
        }

        private string[] FilePathArray;

        public ImageData()
        {
            DirectoryInfo dir = new DirectoryInfo(DefaultPath);
            if (!dir.Exists) dir.Create();

          

        }

        public void UpdateConfig()
        {
        }




    }
}