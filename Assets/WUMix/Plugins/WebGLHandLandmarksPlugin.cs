using System.Runtime.InteropServices;
using AOT;

public class WebGLHandLandmarksPlugin
{
    [DllImport("__Internal")]
    static extern void CreateHandLandmarker(int width, int height);

    public static void Init(int width, int height)
    {
        CreateHandLandmarker(width, height);
    }
}
