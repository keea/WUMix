using UnityEngine;

public class TestWeb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WebGLHandLandmarksPlugin.Init(960, 600, OnLandmarksUpdate);
    }

    public void OnLandmarksUpdate(string multiHandLandmarks, int width, int height)
    {
        Debug.Log("OnLandmarksUpdate : " + multiHandLandmarks);
    }
}
