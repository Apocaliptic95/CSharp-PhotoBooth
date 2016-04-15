using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPhotoEffects
{
    public class Sepia : PhotoBoothPlugInSDK.IProcessingImagePlugIn
    {
        public Bitmap processImage(Bitmap bitmap)
        {
            CvInvoke.UseOpenCL = true;
            Image<Bgr, Byte> img = new Image<Bgr, Byte>(bitmap);
            byte[,,] data = img.Data;

            for (int i = img.Rows - 1; i >= 0; i--)
            {
                for (int j = img.Cols - 1; j >= 0; j--)
                {
                    int inputRed = data[i, j, 2];
                    int inputGreen = data[i, j, 1];
                    int inputBlue = data[i, j, 0];

                    data[i, j, 2] = (byte)Math.Min(255, (int)((.393 * inputRed) + (.769 * inputGreen) + (.189 * inputBlue))); //red
                    data[i, j, 1] = (byte)Math.Min(255, (int)((.349 * inputRed) + (.686 * inputGreen) + (.168 * inputBlue))); //green
                    data[i, j, 0] = (byte)Math.Min(255, (int)((.272 * inputRed) + (.534 * inputGreen) + (.131 * inputBlue))); //blue
                }
            }
            return img.ToBitmap();
        }

        public string getName()
        {
            return "Sepia";
        }

        public List<PhotoBoothPlugInSDK.Parameter> getParameters()
        {
            return new List<PhotoBoothPlugInSDK.Parameter>();
        }
    }
}
