using DirectShowLib;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace PhotoBooth
{
    public class WebCameraController : ICameraController
    {

        #region Variables
        private Capture _capture = null;
        private bool _captureInProgress = false;
        private int CameraDevice;
        private Video_Device[] WebCams = null;
        private int Timer = 0;
        private int Bulb = 0;
        private bool BulbOn = false;
        #endregion

        #region Events
        public event DFrameGrabbed FrameGrabbedEvent;
        public event DTime TimeRemained;
        #endregion

        #region Methods
        public WebCameraController()
        {
            CvInvoke.UseOpenCL = true;
            DsDevice[] _SystemCameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            WebCams = new Video_Device[_SystemCameras.Length];
            List<string> temp = new List<string>();
            for (int i = 0; i < _SystemCameras.Length; i++)
            {
                WebCams[i] = new Video_Device(i, _SystemCameras[i].Name, _SystemCameras[i].ClassID);
            }
        }

        public void setBulbTime(int value)
        {
            Bulb = value;
        }

        public void setBulb(bool value)
        {
            BulbOn = value;
        }

        public List<string> getAvList()
        {
            List<string> temp = new List<string>();
            temp.Add("Normal");
            return temp;
        }

        public List<string> getTvList()
        {
            List<string> temp = new List<string>();
            temp.Add("Normal");
            return temp;
        }

        public List<string> getISOList()
        {
            List<string> temp = new List<string>();
            temp.Add("Normal");
            return temp;
        }

        public List<string> getCameraList()
        {
            List<string> temp = new List<string>();
            for(int i=0; i<WebCams.Length; i++)
            {
                temp.Add(WebCams[i].ToString());
            }
            return temp;
        }

        public void setAV(string value)
        {

        }

        public void setTV(string value)
        {

        }

        public void setISO(string value)
        {

        }

        public void setFocus(Focus focus)
        {

        }

        public void setTimer(int secunds)
        {
            Timer = secunds;
        }

        public void startLiveView()
        {
            if(_capture != null)
            {
                if(!_captureInProgress)
                {
                    _capture.Start();
                    _captureInProgress = true;
                }
            }
        }

        public void stopLiveView()
        {
            if (_capture != null)
            {
                if (_captureInProgress)
                {
                    _capture.Pause();
                    _captureInProgress = false;
                }
            }
        }

        public Bitmap TakePhoto()
        {           
            while (Timer != 0)
            {
                if (TimeRemained != null)
                {
                    TimeRemained(Timer);
                }
                System.Threading.Thread.Sleep(1000);
                Timer--;
            }

            Mat frame = new Mat();
            _capture.Retrieve(frame);
            Bitmap Image = frame.Bitmap;
            return Image;
        }

        public void setCamera(string value)
        {
            int Selected = -1;
            for(int i=0; i<WebCams.Length; i++)
            {
                if(WebCams[i].ToString() == value)
                {
                    Selected = i;
                    break;
                }
            }
            if(Selected != -1)
            {
                CameraDevice = Selected;
                if (_capture != null) _capture.Dispose();
                try
                {
                    _capture = new Capture(CameraDevice);
                    _capture.ImageGrabbed += ProcessFrame;
                }
                catch (NullReferenceException e)
                {
                    //TODO
                }
            }
        }
        public void Dispose()
        {
            if (_capture != null)
            {
                if (_captureInProgress)
                {
                    _capture.Pause();
                    _captureInProgress = false;
                }
                _capture.Dispose();
            }
        }

        public Directory getAvailableSaveTo()
        {
            return Directory.Computer;
        }

        public void setSaveTo(Directory dirType)
        {

        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            Mat frame = new Mat();
            _capture.Retrieve(frame);
            Bitmap Image = frame.Bitmap;
            if(FrameGrabbedEvent != null)
            {
                FrameGrabbedEvent(Image);
            }
            Image.Dispose();
            GC.Collect();
        }

        #endregion
    }

    public struct Video_Device
    {
        public string Device_Name;
        public int Device_ID;
        public Guid Identifier;

        public Video_Device(int ID, string Name, Guid Identity = new Guid())
        {
            Device_ID = ID;
            Device_Name = Name;
            Identifier = Identity;
        }

        public override string ToString()
        {
            return String.Format("[{0}] {1}: {2}", Device_ID, Device_Name, Identifier);
        }
    }
}
