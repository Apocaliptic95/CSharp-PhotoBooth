using EDSDKLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using PhotoBoothPlugInSDK;

namespace PhotoBooth
{
    public class CanonController : ICameraController
    {
        #region Variables
        private SDKHandler CameraHandler;
        private List<EDSDKLib.Camera> CanonList;
        private List<string> CameraList;
        private int Timer = 0;
        private int Bulb = 0;
        private bool BulbOn = false;
        private Bitmap Photo = null;
        #endregion

        #region Events
        public event DTime TimeRemained;
        public event DFrameGrabbed FrameGrabbedEvent;
        #endregion

        #region Methods

        public CanonController()
        {
            CameraHandler = new SDKHandler();
            CanonList = CameraHandler.GetCameraList();
            CameraList = new List<string>();
            foreach(EDSDKLib.Camera cam in CanonList)
            {
                CameraList.Add(cam.getName());
            }
            CameraHandler.CameraHasShutdown += SDK_CameraHasShutdown;
            CameraHandler.LiveViewUpdated += SDK_LiveViewUpdated;
            CameraHandler.CameraAdded += SDK_CameraAdded;
            CameraHandler.PhotoTakenEvent += PhotoTaken;
        }

        public void setBulbTime(int value)
        {
            Bulb = value;
        }

        public void setBulb(bool value)
        {
            BulbOn = value;
        }

        public void startLiveView()
        {
            if(CameraHandler.MainCamera != null)
                CameraHandler.StartLiveView();
        }

        public void stopLiveView()
        {
            if (CameraHandler.MainCamera != null)
                CameraHandler.StopLiveView();
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

            if (BulbOn)
                CameraHandler.TakePhoto((uint) Bulb);
            else CameraHandler.TakePhoto();

            while(Photo == null)
            {              
            }

            Bitmap bitmap = (Bitmap)Photo.Clone();
            Photo = null;
            GC.Collect();

            return bitmap;
        }

        public void setFocus(Focus focus)
        {
            switch(focus)
            {
                case Focus.Far1:
                    CameraHandler.SetFocus(EDSDK.EvfDriveLens_Far1);
                    break;
                case Focus.Far2:
                    CameraHandler.SetFocus(EDSDK.EvfDriveLens_Far2);
                    break;
                case Focus.Far3:
                    CameraHandler.SetFocus(EDSDK.EvfDriveLens_Far3);
                    break;
                case Focus.Near1:
                    CameraHandler.SetFocus(EDSDK.EvfDriveLens_Near1);
                    break;
                case Focus.Near2:
                    CameraHandler.SetFocus(EDSDK.EvfDriveLens_Near2);
                    break;
                case Focus.Near3:
                    CameraHandler.SetFocus(EDSDK.EvfDriveLens_Near3);
                    break;
            }
        }

        public void setTimer(int secunds)
        {
            Timer = secunds;
        }

        public void setSaveTo(Directory dirType)
        {
            switch(dirType)
            {
                case Directory.Both:
                    CameraHandler.SetSetting(EDSDK.PropID_SaveTo, (uint)EDSDK.EdsSaveTo.Both);
                    break;
                case Directory.Camera:
                    CameraHandler.SetSetting(EDSDK.PropID_SaveTo, (uint)EDSDK.EdsSaveTo.Camera);
                    break;
                case Directory.Computer:
                    CameraHandler.SetSetting(EDSDK.PropID_SaveTo, (uint)EDSDK.EdsSaveTo.Host);
                    break;
            }
        }

        public Directory getAvailableSaveTo()
        {
            return Directory.Both;
        }

        public void setCamera(string value)
        {
            foreach(EDSDKLib.Camera cam in CanonList)
            {
                if(cam.Info.szDeviceDescription == value)
                {
                    CameraHandler.OpenSession(cam);
                }
            }
        }

        public List<string> getCameraList()
        {
            return CameraList;
        }

        public List<string> getAvList()
        {
            List<int> AvIdList = CameraHandler.GetSettingsList(EDSDK.PropID_Av);
            List<string> AvList = new List<string>();
            foreach (var Av in AvIdList) AvList.Add(CameraValues.AV((uint) Av));
            return AvList;
        }

        public List<string> getTvList()
        {
            List<int> TvIdList = CameraHandler.GetSettingsList(EDSDK.PropID_Tv);
            List<string> TvList = new List<string>();
            foreach (var Tv in TvIdList) TvList.Add(CameraValues.TV((uint)Tv));
            return TvList;
        }

        public List<string> getISOList()
        {
            List<int> ISOIdList = CameraHandler.GetSettingsList(EDSDK.PropID_ISOSpeed);
            List<string> ISOList = new List<string>();
            foreach (var ISO in ISOIdList) ISOList.Add(CameraValues.ISO((uint)ISO));
            return ISOList;
        }

        public void setAV(string value)
        {
            CameraHandler.SetSetting(EDSDK.PropID_Av, CameraValues.AV(value));
        }

        public void setTV(string value)
        {
            CameraHandler.SetSetting(EDSDK.PropID_Tv, CameraValues.TV(value));
        }

        public void setISO(string value)
        {
            CameraHandler.SetSetting(EDSDK.PropID_ISOSpeed, CameraValues.ISO(value));
        }

        public void Dispose()
        {
            CameraHandler.Dispose();
        }

        private void SDK_CameraHasShutdown(object sender, EventArgs e)
        {
            CameraHandler.CloseSession();
        }

        private void SDK_LiveViewUpdated(Stream img)
        {
            Bitmap Image = new Bitmap(img);
            if (FrameGrabbedEvent != null)
            {
                FrameGrabbedEvent(Image);
            }
            Image.Dispose();
            GC.Collect();
        }

        private void SDK_CameraAdded()
        {
            CanonList = CameraHandler.GetCameraList();
            foreach (EDSDKLib.Camera cam in CanonList)
            {
                CameraList.Add(cam.getName());
            }
        }

        private void PhotoTaken(IntPtr inRef)
        {
            Bitmap bitmap = CameraHandler.DownloadImage(inRef);
            Photo = bitmap;
        }

        #endregion
    }
}
