using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoBoothPlugInSDK;
using System.Drawing;

namespace PhotoBooth.Camera.Controllers
{
    public class CameraChecker
    {
        private bool grabbed;
        //private bool timerWork;
        private ICameraController camera;

        public bool checkCamera(ICameraController cam)
        {
            camera = cam;
            if (camera == null)
                return false;
            try
            {
                if (camera.getCameraList() == null)
                    return false;
                if (camera.getAvList() == null)
                    return false;
                if (camera.getTvList() == null)
                    return false;
                if (camera.getISOList() == null)
                    return false;
                if (camera.TakePhoto() == null)
                    return false;
                camera.setBulb(true);
                camera.setBulbTime(1);
                foreach(string s in camera.getAvList())
                {
                    camera.setAV(s);
                }
                foreach (string s in camera.getTvList())
                {
                    camera.setTV(s);
                }
                foreach (string s in camera.getISOList())
                {
                    camera.setISO(s);
                }
                foreach (string s in camera.getCameraList())
                {
                    camera.setCamera(s);
                }
                camera.setFocus(Focus.Far1);
                camera.setFocus(Focus.Far2);
                camera.setFocus(Focus.Far3);
                camera.setFocus(Focus.Near1);
                camera.setFocus(Focus.Near2);
                camera.setFocus(Focus.Near3);
                camera.setTimer(1);
                if (camera.getAvailableSaveTo() == Directory.Computer)
                    camera.setSaveTo(Directory.Computer);
                if (camera.getAvailableSaveTo() == Directory.Camera)
                    camera.setSaveTo(Directory.Camera);
                if (camera.getAvailableSaveTo() == Directory.Both)
                    camera.setSaveTo(Directory.Both);
                grabbed = false;
                camera.startLiveView();
                if (grabbed == false)
                    return grabbed;
            }
            catch(Exception e)
            {          
                return false;
            }
            return true;
        }

        private void imagecc(Bitmap bitmap)
        {
            camera.stopLiveView();
            grabbed = true;
        }
    }
}
