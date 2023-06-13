using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.PostEffectes.FogBlurPostEffact
    {
    public class FogBlurPostEffact : PostEffector
        {
        public FogBlurPostEffact()
            {
            mat = new Material(Shader.Find("Hidden/BlurGorVarShader"));
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
            throw new System.NotImplementedException();
            }

        public void SetTextures(RenderTexture _prince, RenderTexture _blured, RenderTexture _masque)
            {
            mat.SetTexture("_MainTex", _prince);
            mat.SetTexture("_Blured", _blured);
            mat.SetTexture("_Masque", _masque);
            }
        }
    }