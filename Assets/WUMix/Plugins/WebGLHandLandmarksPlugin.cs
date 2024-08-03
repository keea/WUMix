using System.Runtime.InteropServices;
using AOT;

public class WebGLHandLandmarksPlugin
{
    public delegate void LandmarksUpdateHandler(string multiHandLandmarks, int width, int height);
    
    [DllImport("__Internal")]
    static extern void CreateHandLandmarker(int width, int height, LandmarksUpdateHandler callback);

    public static event LandmarksUpdateHandler OnLandmarksUpdate;
    
    [MonoPInvokeCallback(typeof(LandmarksUpdateHandler))]
    public static void onReceiveHands(string landmarks, int width, int height)
    {
        if (OnLandmarksUpdate != null)
        {
            OnLandmarksUpdate(landmarks, width, height);
        }
    }

    public static void Init(int width, int height, LandmarksUpdateHandler callback)
    {
        OnLandmarksUpdate += callback;
        CreateHandLandmarker(width, height, onReceiveHands);
    }
}
