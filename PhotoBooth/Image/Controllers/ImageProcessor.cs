using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoBoothPlugInSDK;
using System.Drawing;

namespace PhotoBooth.Image.Controllers
{
    public class ImageProcessor
    {
        private Queue<IProcessingImagePlugIn> frameProcessing = new Queue<IProcessingImagePlugIn>();

        public void setFrameProcessing(Queue<IProcessingImagePlugIn> effects)
        {
            if (effects == null)
                throw new NullReferenceException();
            frameProcessing = effects;
        }

        public Queue<IProcessingImagePlugIn> getFrameProcessing()
        {
            return frameProcessing;
        }

        public void cleanFrameProcessing()
        {
            frameProcessing.Clear();
        }

        public Bitmap processImage(Bitmap input)
        {
            foreach(IProcessingImagePlugIn effect in frameProcessing)
            {
                input = effect.processImage(input);
            }
            return input;
        }
    }
}
