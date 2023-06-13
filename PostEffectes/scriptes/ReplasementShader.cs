using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplasementShader : MonoBehaviour
    {
    public Camera cam;
    public Shader solidColor;
    public Material mat_01, mat_02;

    /// <summary>
    /// обычное изображение с камеры
    /// </summary>
    public RenderTexture mainCamTex;

    RenderTexture tempTex;

    [Range(0, 1)]
    public float _pousser;
    // Start is called before the first frame update
    void Start()
        {
        Application.targetFrameRate = 60;

        cam = GetComponent<Camera>();
        cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.DepthNormals;
        cam.SetReplacementShader(solidColor, "RenderType");
        mat_01.SetTexture("_SubTex", mainCamTex);
        }

    // Update is called once per frame
    void Update()
        {
        //cam.SetReplacementShader(solidColor, "RenderType");
        }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
        var temp_01 = RenderTexture.GetTemporary(src.width, src.height);
        var temp_02 = RenderTexture.GetTemporary(src.width, src.height);

        Graphics.Blit(src, temp_01, mat_02, 0);
        Graphics.Blit(temp_01, temp_02, mat_02, 1);       
        Shader.SetGlobalFloat("_pousser", _pousser);
        Graphics.Blit(temp_02, dest, mat_01);

        RenderTexture.ReleaseTemporary(temp_01);
        RenderTexture.ReleaseTemporary(temp_02);
        }
    }
