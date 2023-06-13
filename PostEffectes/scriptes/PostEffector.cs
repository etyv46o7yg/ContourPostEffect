using UnityEngine;

public abstract class PostEffector
    {
    public Material mat;
    public abstract void ProccesImage(Texture _sourse, RenderTexture _dest);

    public abstract Texture2D RecoirPNGFile(RenderTexture _sourse);
    public abstract void SetPrincIntensivite(float _intens);
    
    public virtual void SetMainColor(Color _col)
        {
        mat.SetColor("_Color", _col);
        }
    }