using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.PostEffectes.Blur
    {
    class BlurPostEffect : PostEffector
        {
        RenderTexture tex;

        string nomVarSize = "_BlurSize";

        public BlurPostEffect()
            {
            mat = new Material(Shader.Find("Tutorial/023_Postprocessing_Blur"));
            //mat.Set
            }

        public override void ProccesImage(Texture _sourse, RenderTexture _dest)
            {
            if (tex == null)
                {
                tex = new RenderTexture(_sourse.width, _sourse.height, 32);
                }

            //mat.SetFloat(nomVarSize, size);

            mat.SetVector("DirSelect", new Vector4(0, 1, 0, 0));
            Graphics.Blit(_sourse, tex, mat);

            mat.SetVector("DirSelect", new Vector4(1, 0, 0, 0));
            Graphics.Blit(tex, _dest, mat);
            }

        public override Texture2D RecoirPNGFile(RenderTexture _sourse)
            {
            throw new NotImplementedException();
            }

        public override void SetPrincIntensivite(float _intens)
            {
            mat.SetFloat(nomVarSize, _intens);
            }
        }
    }
