using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonFunc : MonoBehaviour
{
    static public void SetImageFile(string fileName, ref Image img)
    {
        var imgSprite = (Sprite)Resources.Load(fileName, typeof(Sprite));
        if (imgSprite == null)
            return;

        //if (sizeAuto)
        //{
        //    RectTransform rt = img.GetComponent<RectTransform>();
        //    rt.sizeDelta = imgSprite.rect.size;
        //}
        img.sprite = imgSprite;
    }

    static public void SetImageSize(Vector2 size, ref Image img)
    {
        RectTransform rt = img.GetComponent<RectTransform>();
        rt.sizeDelta = size;
    }

    static public Color HexToColor(string HexVal, float a = 1f)
    {
        Color returnValue = new Color(0, 0, 0, a);
        ColorUtility.TryParseHtmlString(HexVal, out returnValue);
        returnValue.a = a;
        return returnValue;
    }
}