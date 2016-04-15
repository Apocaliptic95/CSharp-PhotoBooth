using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoBoothPlugInSDK;
using PhotoBooth.PlugIn.Interfaces;

namespace PhotoBooth.PlugIn.Controllers
{
    public class PlugInChecker : IPlugInChecker
    {
        public bool ImagePlugInCheck(IProcessingImagePlugIn plugIn)
        {
            Bitmap test = new Bitmap(1024, 1024);
            try
            {
                List<Parameter> list = plugIn.getParameters();
                foreach(Parameter p in list)
                {
                    string paramname = p.getName();
                    if (paramname == null || paramname == string.Empty)
                        return false;
                    if (p.getDefault() == null)
                        return false;
                    if (p.getMax() == null)
                        return false;
                    if (p.getMin() == null)
                        return false;
                    if (p.getValue() == null)
                        return false;
                    try
                    {
                        p.setValue(p.getDefault());
                    }
                    catch(Exception e)
                    {
                        return false;
                    }
                }
                string name = plugIn.getName();
                if (name == null || name == string.Empty)
                    return false;
                try
                {
                    Bitmap output = plugIn.processImage(test);
                }
                catch(Exception f)
                {
                    return false;
                }
            }
            catch(Exception g)
            {
                return false;
            }
            return true;
        }
    }
}
