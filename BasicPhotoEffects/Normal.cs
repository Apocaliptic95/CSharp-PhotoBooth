using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPhotoEffects
{
    class Normal : PhotoBoothPlugInSDK.IProcessingImagePlugIn
    {
        public string getName()
        {
            return "Normal";
        }

        public Bitmap processImage(Bitmap bitmap)
        {
            return bitmap;
        }

        public List<PhotoBoothPlugInSDK.Parameter> getParameters()
        {
            return new List<PhotoBoothPlugInSDK.Parameter>();
        }
    }
}
