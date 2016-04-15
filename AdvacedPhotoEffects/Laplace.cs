using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace AdvacedPhotoEffects
{
    public class Laplace : PhotoBoothPlugInSDK.IProcessingImagePlugIn
    {
        private List<PhotoBoothPlugInSDK.Parameter> param = new List<PhotoBoothPlugInSDK.Parameter>();
        private PhotoBoothPlugInSDK.Parameter a;
        private int[] prime = new int[] { 1, 3, 5, 7, 11, 13, 17, 19, 21, 23, 29, 31 };

        public Laplace()
        {
            a = new PhotoBoothPlugInSDK.Parameter("Aperture Size", 1, 0, 11, 2, PhotoBoothPlugInSDK.parameterDisplayType.range);
            param.Add(a);
        }

        public List<PhotoBoothPlugInSDK.Parameter> getParameters()
        {
            return param;
        }

        public string getName()
        {
            return "Laplace";
        }

        public Bitmap processImage(Bitmap bitmap)
        {
            CvInvoke.UseOpenCL = false;
            Image<Bgr, Byte> img = new Image<Bgr, Byte>(bitmap);
            Image<Gray, Byte> img2 = img.Convert<Gray, Byte>();
            int aperture = Convert.ToInt32(Math.Floor((double)a.getValue()));
            return img2.Laplace(prime[aperture]).ToBitmap();
        }

    }
}
