using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.PostEffectes
    {
    class TooniColor : PostEffector
        {
        Material mat;

        public TooniColor(int _x, int _y)
            {
            mat = new Material( Shader.Find("Hidden/TooniColor") );
            }

        public override void ProccesImage(Texture _sourse, RenderTexture _dest)
            {
            Graphics.Blit(_sourse, _dest, mat);
            }


        public override Texture2D RecoirPNGFile(RenderTexture _sourse)
            {
            throw new NotImplementedException();
            }

        public override void SetPrincIntensivite(float _intens)
            {
            mat.SetFloat("_koeff", _intens);
            }
        }
    }
