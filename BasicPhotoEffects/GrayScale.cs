using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;

namespace BasicPhotoEffects
{
    public  class GrayScale : PhotoBoothPlugInSDK.IProcessingImagePlugIn
    {
        public Bitmap processImage(Bitmap bitmap)
        {
            CvInvoke.UseOpenCL = true;
            Image<Bgr, Byte> img = new Image<Bgr, Byte>(bitmap);
            Image<Gray, Byte> img2 = img.Convert<Gray, Byte>();
            return img2.ToBitmap();
        }

        public string getName()
        {
            return "Gray Scale";
        }

        public List<PhotoBoothPlugInSDK.Parameter> getParameters()
        {
            return new List<PhotoBoothPlugInSDK.Parameter>();
        }
    }
}
