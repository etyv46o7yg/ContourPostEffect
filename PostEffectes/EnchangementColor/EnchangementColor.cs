using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.PostEffectes.EnchangementColor
    {
    class EnchangementColor : PostEffector
        {
        Material mat;
        string nomVarSize = "_size";
        RenderTexture tex;

        public EnchangementColor()
            {
            mat = new Material(Shader.Find("Hidden/EncharcherColor"));
            }

        public override void ProccesImage(Texture _sourse, RenderTexture _dest)
            {
            if (tex == null)
                {
                tex = new RenderTexture(_sourse.width, _sourse.height, 32);
                }

            mat.SetVector("_dir", new Vector4(1, 0, 0, 0));
            Graphics.Blit(_sourse, tex, mat);

            mat.SetVector("_dir", new Vector4(0, 1, 0, 0));
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
