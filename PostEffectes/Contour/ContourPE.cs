using Assets.PostEffectes.Blur;
using Assets.PostEffectes.EnchangementColor;
using Assets.PostEffectes.FogBlurPostEffact;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static PostEffectPaint;

public class ContourPE : MonoBehaviour
    {
    //public RawImage ri;
    CommandBuffer buff;
    RenderTexture courr;
    public RenderTexture riggTex, bluredTex, accumulator, temp;
    public Material solidMat;

    //public Mesh drawRend;
    //public MeshRenderer remd;
    //public Transform drawTr;
    PostEffector blur, enchegement;
    FogBlurPostEffact mixTex;
    MixTexture cutTexture;

    [Range(0, 0.1f)]
    public float blurKoeff;
    [Range(0, 10)]
    public float epasseur;

    // Start is called before the first frame update
    void Start()
        {
        riggTex = new RenderTexture(Screen.width, Screen.height, 24);
        riggTex.Create();

        bluredTex = new RenderTexture(Screen.width, Screen.height, 24);
        bluredTex.Create();

        accumulator = new RenderTexture(Screen.width, Screen.height, 24);
        accumulator.Create();

        temp = new RenderTexture(Screen.width, Screen.height, 24);
        temp.Create();

        //ri.texture = rt_2;

        blur = new BlurPostEffect();
        blur.SetPrincIntensivite(5.1f);

        enchegement = new EnchangementColor();

        mixTex = new FogBlurPostEffact();
        cutTexture = new MixTexture();
        }

    private void Update()
        {

        //RegisterContour_2(drawRend, drawTr);
        }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
        ClearOutRenderTexture(accumulator);

        var settings = PostEffectPaint.inst.lineSettings;

        foreach (var item in settings)
            {
            DrawOutLineColor(riggTex, item);

            enchegement.SetPrincIntensivite(epasseur * item.largeur);
            enchegement.ProccesImage(riggTex, bluredTex);

            blur.SetPrincIntensivite(blurKoeff);
            blur.ProccesImage(bluredTex, bluredTex);

            Graphics.Blit(accumulator, temp);

            cutTexture.SetMask(riggTex);
            cutTexture.SetAccum(temp);
            cutTexture.SetMainColor(item.color);
            cutTexture.ProccesImage(bluredTex, accumulator);
            }
        //

        mixTex.SetTextures(source, accumulator, accumulator);
        mixTex.ProccesImage(source, destination);

        var paramsRest = PostEffectPaint.inst.paramsRest;
        var restEllipsMat = PostEffectPaint.inst.restEllipsMat;
        //Graphics.Blit(accumulator, destination);

        //return;
        if (paramsRest.estDraw)
            {
            Graphics.Blit(destination, temp);
            restEllipsMat.SetVector("pointA", paramsRest.pointA);
            restEllipsMat.SetVector("pointB", paramsRest.pointB);
            Graphics.Blit(temp, destination, restEllipsMat);
            }

        else
            {
            Graphics.Blit(destination, destination);
            }
        }

    public void DrawOutLineColor(RenderTexture source, OutLineSettings settings)
        {
        var commands = new CommandBuffer();
        //render selection buffer
        commands.SetRenderTarget(source);
        commands.ClearRenderTarget(true, true, Color.clear);

        foreach (var item in settings.rend)
            {
            if (item.renderer != null)
                {
                commands.DrawRenderer(item.renderer, solidMat);
                }
            }

        //execute and clean up commandbuffer itself
        Graphics.ExecuteCommandBuffer(commands);
        commands.Dispose();
        }

    public void ClearOutRenderTexture(RenderTexture renderTexture)
        {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
        }

    }
