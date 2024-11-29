using OpenCvSharp;

namespace Ts_vid
{
    public class TargetDetector
    {
        public OpenCvSharp.Point DetectTarget(Mat frame)
        {
            
            return new OpenCvSharp.Point(frame.Width / 2, frame.Height / 2);
        }
    }
}
