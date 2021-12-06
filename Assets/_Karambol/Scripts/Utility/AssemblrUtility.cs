using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;

namespace InternshipUnity3D
{
    public static class ProjectUtility
    {
        public static Sprite ConvertTexture2DToSprite(Texture2D tex)
        {
            if (tex != null)
            {
                Rect rec = new Rect(0, 0, tex.width, tex.height);
                return Sprite.Create(tex, rec, Vector2.zero, 1);
            }
            else
            {
                return null;
            }
        }
    }
}