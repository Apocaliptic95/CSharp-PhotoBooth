using System;
using System.Collections.Generic;
using System.IO;

namespace PhotoBooth.Other
{
    public class LibraryManager
    {
        private List<string> required;
            
        public  LibraryManager()
        {
            required = new List<string>();
            required.Add(@"DirectShowLib-2005.dll");
            required.Add(@"EDSDKLib.dll");
            required.Add(@"Emgu.CV.UI.dll");
            required.Add(@"Emgu.CV.UI.GL.dll");
            required.Add(@"Emgu.CV.World.dll");
            required.Add(@"EntityFramework.dll");
            required.Add(@"EntityFramework.SqlServer.dll");
            required.Add(@"PhotoBooth.dll");
            required.Add(@"System.Data.SQLite.dll");
            required.Add(@"System.Data.SQLite.EF6.dll");
            required.Add(@"System.Data.SQLite.Linq.dll");
            required.Add(@"zh-CN/ZedGraph.resources.dll");
            required.Add(@"x86/cvextern.dll");
            required.Add(@"x86/msvcp120.dll");
            required.Add(@"x86/msvcr120.dll");
            required.Add(@"x86/opencv_ffmpeg310.dll");
            required.Add(@"x86/SQLite.Interop.dll");
            required.Add(@"x64/cvextern.dll");
            required.Add(@"x64/msvcp120.dll");
            required.Add(@"x64/msvcr120.dll");
            required.Add(@"x64/opencv_ffmpeg310_64.dll");
            required.Add(@"x64/SQLite.Interop.dll");
        }

        public List<string> LibraryCheck()
        {
            List<string> temp = new List<string>();
            foreach (string s in required)
            {
                try
                {
                    if(!File.Exists(s))
                    {
                        temp.Add(s);
                    }
                }
                catch(Exception e)
                {
                    temp.Add(s);
                }
            }
            return temp;
        }
    }
}