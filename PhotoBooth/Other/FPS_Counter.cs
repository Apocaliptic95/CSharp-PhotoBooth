using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoBooth.Other
{
    public class FPS_Counter
    {
        List<DateTime> FrameCounter = new List<DateTime>();

        public void countFrame()
        {
            FrameCounter.Add(DateTime.Now);
        }

        private void clearOld()
        {
            bool continueLoop;
            DateTime decayLimit = DateTime.Now.AddSeconds(-1);

            do
            {
                continueLoop = false;
                if (FrameCounter.Count > 0 && FrameCounter[0] < decayLimit)
                {
                    FrameCounter.RemoveAt(0);
                    //If you removed one, the one after might be too old too.
                    continueLoop = true;
                }
            } while (continueLoop);
        }

        public int FPS
        {
            get
            {
                clearOld();
                return FrameCounter.Count;
            }
        }
    }
}
