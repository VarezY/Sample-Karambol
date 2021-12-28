using UnityEngine.Serialization;
using System;


namespace UnityEngine.UI
{
	public class GradientUI : MaskableGraphic {

		[SerializeField] private Color m_Color1 = Color.white;
		public Color Color1 { get { return m_Color1;} set {m_Color1 = value;}}

		public Color Color2 { get { return color;} set {color = value;}}

		private Vector2[] m_UVs = new Vector2[4];

		[SerializeField] private bool m_Horizontal = true;
		public bool Horizontal { get { return m_Horizontal;} set {m_Horizontal = value;}}

		[FormerlySerializedAs("m_Frame")]
		[SerializeField] private Sprite m_Sprite;
		public Sprite sprite { get { return m_Sprite; } set { if (SetClass(ref m_Sprite, value)) SetAllDirty(); } }

		[NonSerialized]
		private Sprite m_OverrideSprite;
		public Sprite overrideSprite { get { return m_OverrideSprite == null ? sprite : m_OverrideSprite; } set { if (SetClass(ref m_OverrideSprite, value)) SetAllDirty(); } }

		public override Texture mainTexture { 
			get { 
				if (overrideSprite == null)
				{
					if (material != null && material.mainTexture != null)
					{
						return material.mainTexture;
					}
					return s_WhiteTexture;
				}
				return overrideSprite.texture;
			}
		}

		private bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
				return false;

			currentValue = newValue;
			return true;
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			Rect r = GetPixelAdjustedRect();

			if (m_Horizontal == true){
				vh.AddVert(new Vector2(r.x, r.y), m_Color1, m_UVs[0]);
				vh.AddVert(new Vector2(r.x, r.y+r.height), m_Color1, m_UVs[1]);
				vh.AddVert(new Vector2(r.width+r.x, r.y+r.height), color, m_UVs[2]);
				vh.AddVert(new Vector2(r.width+r.x, r.y), color, m_UVs[3]);
			}else {
				vh.AddVert(new Vector2(r.x, r.y), color, m_UVs[0]);
				vh.AddVert(new Vector2(r.x, r.y+r.height), m_Color1, m_UVs[1]);
				vh.AddVert(new Vector2(r.width+r.x, r.y+r.height), m_Color1, m_UVs[2]);
				vh.AddVert(new Vector2(r.width+r.x, r.y), color, m_UVs[3]);
			}

			m_UVs[0] = new Vector2(0,0);
			m_UVs[1] = new Vector2(0,1);
			m_UVs[2] = new Vector2(1,1);
			m_UVs[3] = new Vector2(1,0);

			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}
	}
}
