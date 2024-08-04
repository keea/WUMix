using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blitter : MonoBehaviour
{
    public Material TransitionMaterial;
    private Vector4 posShader = Vector4.zero;

    public void SetPosShader(Vector2 pos, float radius)
    {
        // 화면 좌표를 셰이더 좌표로 변환
        posShader.x = pos.x / Screen.width;
        posShader.y = pos.y / Screen.height;
        posShader.z = radius;
        posShader.w = 0.0f;
    }

    void Update()
    {
        // 마우스 위치를 가져오기
        Vector3 mousePos = Input.mousePosition;
        Debug.Log("mousePos : " + mousePos.ToString());

        // 셰이더에 전달
        TransitionMaterial.SetVector("_MousePos", posShader);
    }

    //모든 랜더링이 완료된 후에 랜더 이미지로 호출
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (TransitionMaterial != null)
            //소스 텍스쳐를 쉐이더를 이용하여 랜더 텍스쳐에 복사.
            Graphics.Blit(src, dst, TransitionMaterial); 
    }
}
