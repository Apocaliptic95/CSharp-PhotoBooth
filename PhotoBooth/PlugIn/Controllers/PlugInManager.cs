using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using PhotoBooth.PlugIn.Interfaces;
using PhotoBooth.Database.Interfaces;

namespace PhotoBooth.PlugIn.Controllers
{
    public class PlugInManager
    {
        private IPlugInChecker _checker;
        private IDataBase _database;
        private List<string> badPlugIns;

        public PlugInManager(IPlugInChecker checker, IDataBase database)
        {
            _checker = checker;
            _database = database;
            if (_checker == null | _database == null)
                throw new NullReferenceException();
        }

        private Object getInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public List<PhotoBoothPlugInSDK.IProcessingImagePlugIn> getImagePlugInsList()
        {
            List<string> plugInList = _database.SelectType(PhotoBoothPlugInSDK.PlugInType.Image);
            List<PhotoBoothPlugInSDK.IProcessingImagePlugIn> types = new List<PhotoBoothPlugInSDK.IProcessingImagePlugIn>();
            foreach (string name in plugInList)
            {
                if(!badPlugIns.Any(e => e.Equals(name)))
                {
                    string path = @"PlugIns\" + name;
                    Assembly asm = Assembly.LoadFrom(path);
                    foreach (Type t in asm.GetTypes())
                    {
                        foreach (Type iface in t.GetInterfaces())
                        {
                            if (iface.Equals(typeof(PhotoBoothPlugInSDK.IProcessingImagePlugIn)))
                            {
                                types.Add((PhotoBoothPlugInSDK.IProcessingImagePlugIn) getInstance(t));
                            }
                        }
                    }
                }
            }
            return types;
        }

        public List<string> CheckPlugIns()
        {
            List<string> plugInList = _database.SelectAll();
            badPlugIns = new List<string>();
            foreach (string name in plugInList)
            {
                if (!LibraryExists(name))
                {
                    badPlugIns.Add(name);
                }
                else if (!TryLoadingPlugin(name))
                {
                    badPlugIns.Add(name);
                }
                else
                {
                    string path = @"PlugIns\" + name;
                    Assembly asm = Assembly.LoadFrom(path);
                    foreach (Type t in asm.GetTypes())
                    {
                        foreach (Type iface in t.GetInterfaces())
                        {
                            if (iface.Equals(typeof(PhotoBoothPlugInSDK.IProcessingImagePlugIn)))
                            {
                                if (!_checker.ImagePlugInCheck((PhotoBoothPlugInSDK.IProcessingImagePlugIn)getInstance(t)))
                                    badPlugIns.Add(name);
                            }
                        }
                    }
                }
            }
            return badPlugIns;
        }

        private Boolean LibraryExists(string name)
        {
            if (name != null &&  name != string.Empty && File.Exists(("PlugIns\\" + name)))
                return true;
            else
                return false;
        }

        private Boolean TryLoadingPlugin(string name)
        {
            string path = @"PlugIns\" + name;
            Assembly asm;

            try
            {
                asm = Assembly.LoadFrom(path);
            }
            catch(BadImageFormatException e)
            {
                return false;
            }    
            catch(AppDomainUnloadedException e)
            {
                return false;
            }      
            catch(FileLoadException e)
            {
                return false;
            }

            foreach (Type t in asm.GetTypes())
            {
                foreach (Type iface in t.GetInterfaces())
                {
                    if (iface.Equals(typeof(PhotoBoothPlugInSDK.IProcessingImagePlugIn)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}
