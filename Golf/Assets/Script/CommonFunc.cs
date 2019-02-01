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

    static public CommonData.TRAINING_MODE ConvertTrainingMode(TraningUI.TRANING_POSE_TYPE type)
    {
        switch (type)
        {
            case TraningUI.TRANING_POSE_TYPE.IMPACT:
                return CommonData.TRAINING_MODE.TRAINING_IMPACT;
            case TraningUI.TRANING_POSE_TYPE.BACK_SWING_TOP:
                return CommonData.TRAINING_MODE.TRAINING_BACKSWING;
            case TraningUI.TRANING_POSE_TYPE.ADDRESS:
                return CommonData.TRAINING_MODE.TRAINING_ADDRESS;
            default:
                break;
        }

        return CommonData.TRAINING_MODE.TRAINING_IMPACT;
    }

    static public CommonData.TRAINING_ANGLE_MODE ConvertTrainingMode(TraningUI.TRANING_POSE_ANGLE_TYPE type)
    {
        switch (type)
        {
            case TraningUI.TRANING_POSE_ANGLE_TYPE.SIDE:
                return CommonData.TRAINING_ANGLE_MODE.TRAINING_ANGLE_SIDE;
            case TraningUI.TRANING_POSE_ANGLE_TYPE.TURN:
                return CommonData.TRAINING_ANGLE_MODE.TRAINING_ANGLE_TURN;
            case TraningUI.TRANING_POSE_ANGLE_TYPE.BEND:
                return CommonData.TRAINING_ANGLE_MODE.TRAINING_ANGLE_BEND;
            default:
                break;
        }

        return CommonData.TRAINING_ANGLE_MODE.TRAINING_ANGLE_SIDE;
    }
}