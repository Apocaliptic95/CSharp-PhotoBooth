using System.Collections.Generic;
using System.Drawing;

namespace PhotoBoothPlugInSDK
{
    public interface IProcessingImagePlugIn
    {
        Bitmap processImage(Bitmap bitmap);
        string getName();
        List<Parameter> getParameters();
    }
}
