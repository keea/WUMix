using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class TestWeb : MonoBehaviour
{
    public Blitter blitter;

    // Start is called before the first frame update
    void Start()
    {
        WebGLHandLandmarksPlugin.Init(960, 600, OnLandmarksUpdate);
    }

    public void OnLandmarksUpdate(string multiHandLandmarks, int width, int height)
    {
        var pointArrays = JsonConvert.DeserializeObject<List<List<Landmark>>>(multiHandLandmarks);

        if (pointArrays.Count <= 0) return;

        Landmark landmark = pointArrays[0][(int)HAND_LANDMARKS.MIDDLE_FINGER_MCP];

        Vector2 middle_finger_mcp = new Vector2(pointArrays[0][(int)HAND_LANDMARKS.MIDDLE_FINGER_MCP].x, pointArrays[0][(int)HAND_LANDMARKS.MIDDLE_FINGER_MCP].y);
        Vector2 middle_finger_tip = new Vector2(pointArrays[0][(int)HAND_LANDMARKS.MIDDLE_FINGER_TIP].x, pointArrays[0][(int)HAND_LANDMARKS.MIDDLE_FINGER_TIP].y);
        float distance = Vector2.Distance(middle_finger_mcp, middle_finger_tip);

        Vector2 landmarkCoord = new Vector2(landmark.x, 1f - landmark.y);

        Vector2 screenRatio = new Vector2(Screen.width, Screen.height);
        Vector2 position = landmarkCoord * screenRatio;
        blitter.SetPosShader(position, distance);
    }
}
