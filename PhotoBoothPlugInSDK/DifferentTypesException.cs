using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoBoothPlugInSDK
{
    public class DifferentTypesException : Exception
    {
        public DifferentTypesException()
            :base("Parameters have different types.")
        {            
        }
    }
}
