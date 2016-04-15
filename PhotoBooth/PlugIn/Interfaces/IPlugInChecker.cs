using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoBoothPlugInSDK;

namespace PhotoBooth.PlugIn.Interfaces
{
    public interface IPlugInChecker
    {
        bool ImagePlugInCheck(IProcessingImagePlugIn plugIn);
    }
}
