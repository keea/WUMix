using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class TestWeb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WebGLHandLandmarksPlugin.Init(960, 600, OnLandmarksUpdate);
    }

    public void OnLandmarksUpdate(string multiHandLandmarks, int width, int height)
    {
        var pointArrays = JsonConvert.DeserializeObject<List<List<Landmark>>>(multiHandLandmarks);
        
        foreach (var pointList in pointArrays)
        {
            foreach (var point in pointList)
            {
                Debug.Log($"Landmark: x={point.x}, y={point.y}, z={point.z}");
            }
        }
    }
}
