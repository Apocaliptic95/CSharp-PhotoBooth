using System.Collections.Generic;
using System.Drawing;
using PhotoBoothPlugInSDK;

namespace PhotoBooth
{
    public delegate void DFrameGrabbed(Bitmap bitmap);
    public delegate void DTime(int time);
    public interface ICameraController
    {
        event DFrameGrabbed FrameGrabbedEvent;
        event DTime TimeRemained;

        List<string> getAvList();
        List<string> getTvList();
        List<string> getISOList();
        List<string> getCameraList();
        Directory getAvailableSaveTo();

        Bitmap TakePhoto();

        void setBulbTime(int value);
        void setBulb(bool value);
        void setAV(string value);
        void setTV(string value);
        void setISO(string value);
        void setCamera(string value);
        void setFocus(Focus focus);
        void setTimer(int secunds);
        void setSaveTo(Directory dirType);
        void Dispose();

        void startLiveView();
        void stopLiveView();

    }

    public enum Focus
    {
        Near1,
        Near2,
        Near3,
        Far1,
        Far2,
        Far3
    }

    public enum Directory
    {
        Camera,
        Computer,
        Both
    }
}
