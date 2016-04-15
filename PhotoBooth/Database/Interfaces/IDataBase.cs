using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoBooth.Database.Interfaces
{
    public interface IDataBase
    {
        void CreateBase();
        void Insert(string name, PhotoBoothPlugInSDK.PlugInType type);
        List<string> SelectAll();
        List<string> SelectType(PhotoBoothPlugInSDK.PlugInType type);
        void Delete(string name);
    }
}
