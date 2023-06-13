using Assets.PostEffectes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using static PostEffectPaint.OutLineSettings;

public class PostEffectPaint : MonoBehaviour
	{
	public Material restEllipsMat;

	[SerializeField]
	public List<OutLineSettings> lineSettings;

	public RestSelect paramsRest;
	RenderTexture buff;

	public static PostEffectPaint inst;

	public enum ColorOutline
		{
		Roug,
		Verr,
		Jeun,
		Bleu
		}

    private void Awake()
        {
        if (inst == null)
            {
			inst = this;
            }
        }

    // Start is called before the first frame update
    void Start()
		{
		foreach (var item in lineSettings)
			{
			item.Initialiser();
			}

		paramsRest = new RestSelect { estDraw = false };
		}

	public void SetSelectRest(RestSelect _rest)
		{
		paramsRest = _rest;
		}


	public int RecoirIndexFromColor(Color _color)
		{
		for (int i = 0; i < lineSettings.Count; i++)
			{
			if (DistCOlor(lineSettings[i].color, _color) < 0.1f)
				{
				return i;
				}
			}

		return -1;
		}

	private float DistCOlor(Color _a, Color _b)
		{
		float temp = Mathf.Pow((_a.r - _b.r), 2) + Mathf.Pow((_a.g - _b.g), 2) + Mathf.Pow((_a.b - _b.b), 2);
		return Mathf.Sqrt(temp);
		}

	public void RegisterOutLine(Renderer _rend, int _id, ColorOutline _colorIndex)
		{
		RendIndex res = new RendIndex(_id, _rend);
		lineSettings[(int)_colorIndex].rend.Add(res);
		}

	public void RegisterOutLine(Renderer[] _rend, int _id, ColorOutline _colorIndex)
		{
		foreach (var item in _rend)
			{
			RendIndex res = new RendIndex(_id, item);
			lineSettings[(int)_colorIndex].rend.Add(res);
			}

		}

	public void RegisterOutLine(Renderer _rend, int _id, Color _colorIndex)
		{
		int index = RecoirIndexFromColor(_colorIndex);
		if (index != -1)
			{
			RegisterOutLine(_rend, _id, (ColorOutline)index);
			}
		}

	public void RegisterOutLine(Renderer[] _rend, int _id, Color _colorIndex)
		{
		int index = RecoirIndexFromColor(_colorIndex);
		if (index != -1)
			{
			RegisterOutLine(_rend, _id, (ColorOutline)index);
			}
		}

	public void DeleteOutline(int _id, ColorOutline _index)
		{
		if (lineSettings[(int)_index].rend == null)
			{
			return;
			}

		lineSettings[(int)_index].rend.RemoveAll(x => x.id == _id);
		}

	public void DeleteOutline(int _id, Color _index)
		{
		int index = RecoirIndexFromColor(_index);
		lineSettings[index].rend.RemoveAll(x => x.id == _id);
		}

	public void DeleteOutline(Renderer[] _rend, int _id, ColorOutline _index)
		{
		foreach (var iter in _rend)
			{
			lineSettings[(int)_index].rend.RemoveAll(x => x.id == _id);
			}
		}

	public void DeleteOutline(Renderer[] _rend, int _id, Color _index)
		{
		int index = RecoirIndexFromColor(_index);

		foreach (var iter in _rend)
			{
			lineSettings[index].rend.RemoveAll(x => x.id == _id);
			}
		}

	public void ClearOutLine(ColorOutline _colorIndex)
		{
		if (lineSettings == null)
			{
			Debug.Log("нулевые настройки");
			}
		else
			{
			if (lineSettings[(int)_colorIndex].rend == null)
				{
				Debug.Log("нулевые список");
				}
			}

		lineSettings[(int)_colorIndex].rend.Clear();
		}
	
	public class RestSelect
		{
        public Vector4 pointA;
        public Vector4 pointB;
        public bool estDraw { get; set; }
		}

	/// <summary>
	/// Настройки для отрисовки обводки
	/// </summary>
	[System.Serializable]
	public class OutLineSettings
		{
		/// <summary>
		/// ширина
		/// </summary>
		public float largeur;

		/// <summary>
		/// рендерер для отрисовки
		/// </summary>
		[SerializeField]
		public List<RendIndex> rend;

		/// <summary>
		/// Цвет для выделения
		/// </summary>
		public Color color;

		public class RendIndex
			{
			public int id;
			public Renderer renderer;

			public RendIndex(int _id, Renderer _rend)
				{
				id = _id;
				renderer = _rend;
				}

			}

		public void Initialiser()
			{
			rend = new List<RendIndex>();
			}

		public OutLineSettings(Color _col)
			{
			rend = new List<RendIndex>();
			color = _col;
			}
		}
	}
