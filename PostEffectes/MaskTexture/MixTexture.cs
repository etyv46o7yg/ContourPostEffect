using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixTexture : PostEffector
    {
    public MixTexture()
        {
        mat = new Material(Shader.Find("Hidden/MixTexture"));
        }

    public override void ProccesImage(Texture _sourse, RenderTexture _dest)
        {
        Graphics.Blit(_sourse, _dest, mat);
        }

    public override Texture2D RecoirPNGFile(RenderTexture _sourse)
        {
        throw new System.NotImplementedException();
        }

    public override void SetPrincIntensivite(float _intens)
        {
        
        }

    public void SetMask(RenderTexture _tex)
        {
        mat.SetTexture("_Mask", _tex);
        }

    public void SetAccum(RenderTexture _tex)
        {
        mat.SetTexture("_Accum", _tex);
        }
    // Start is called before the first frame update

    }
