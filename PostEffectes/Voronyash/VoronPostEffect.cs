using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.PostEffectes
    {
    class VoronPostEffect : PostEffector
        {
        Material voron;
        Material edge;
        private RenderTexture voronTex, mainImage, rend;

        public VoronPostEffect(int _x, int _y)
            {           
            voron = new Material( Shader.Find("Hidden/Voronyash") );
            edge  = new Material( Shader.Find("Hidden/EdgeSelect") );

            voronTex  = new RenderTexture(_x, _y, 32);
            mainImage = new RenderTexture(_x, _y, 32);
            rend = new RenderTexture(_x, _y, 32);
            rend.Create();
            }

        public override void ProccesImage(Texture _sourse, RenderTexture _dest)
            {
            Graphics.Blit(_sourse, mainImage);
            Graphics.Blit(_sourse, voronTex, voron);
            edge.SetTexture("_Carde", voronTex);
            Graphics.Blit(mainImage, _dest, edge);
            }

        public Texture2D RecoirPNGFile(Texture2D _sourse)
            {           
            Graphics.Blit(_sourse, mainImage);
            Graphics.Blit(_sourse, voronTex, voron);
            edge.SetTexture("_Carde", voronTex);
            return toTexture2D(rend);
            }

        static Texture2D toTexture2D(RenderTexture rTex)
            {
            Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
            // ReadPixels looks at the active RenderTexture.
            RenderTexture.active = rTex;
            tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            tex.Apply();
            return tex;
            }

        public override void SetPrincIntensivite(float _intens)
            {
            voron.SetFloat("SIZE", _intens);
            }

        public void SetEngeEpassaur(float _intens)
            {
            edge.SetFloat("_SizeBorder", _intens);
            }

        public override Texture2D RecoirPNGFile(RenderTexture _sourse)
            {
            throw new NotImplementedException();
            }
        }
    }
