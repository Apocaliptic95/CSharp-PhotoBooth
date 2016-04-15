using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BasicPhotoEffects
{
    public class Blur : PhotoBoothPlugInSDK.IProcessingImagePlugIn
    {
        private List<PhotoBoothPlugInSDK.Parameter> paramList = new List<PhotoBoothPlugInSDK.Parameter>();
        private PhotoBoothPlugInSDK.Parameter a;
        private PhotoBoothPlugInSDK.Parameter b;

        public Blur()
        {
            a = new PhotoBoothPlugInSDK.Parameter("Width", 1, (int)1, (int)50, (int)5, PhotoBoothPlugInSDK.parameterDisplayType.range);
            b = new PhotoBoothPlugInSDK.Parameter("Height", 1, (int)1, (int)50, (int)5, PhotoBoothPlugInSDK.parameterDisplayType.range);
            paramList.Add(a);
            paramList.Add(b);
        }

        public Bitmap processImage(Bitmap bitmap)
        {
            CvInvoke.UseOpenCL = true;
            Image<Bgr, Byte> img = new Image<Bgr, Byte>(bitmap);
            Image<Bgr, Byte> img2 = img.SmoothBlur((int)a.getValue(), (int)b.getValue(), true);
            return img2.ToBitmap();
        }

        public string getName()
        {
            return "Blur";
        }

        public List<PhotoBoothPlugInSDK.Parameter> getParameters()
        {
            return paramList;
        }
    }
}
