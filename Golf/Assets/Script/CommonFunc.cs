﻿using System.Collections;
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

    static public void SetImageFile(Texture2D texture, ref Image img)
    {
        img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), img.rectTransform.pivot);
    }

    static public Sprite GetImageFile(Texture2D texture, ref Image img)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), img.rectTransform.pivot);
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

    static public string ConvertPoseTypeStr(CommonData.TRAINING_POSE type)
    {
        switch (type)
        {
            case CommonData.TRAINING_POSE.TRAINING_ADDRESS:
                return "어드레스";
            case CommonData.TRAINING_POSE.TRAINING_BACKSWING:
                return "백스윙 탑";
            case CommonData.TRAINING_POSE.TRAINING_IMPACT:
                return "임팩트";
            default:
                break;
        }

        return "";
    }

    static public string ConvertPoseTypeEngStr(CommonData.TRAINING_POSE type)
    {
        switch (type)
        {
            case CommonData.TRAINING_POSE.TRAINING_ADDRESS:
                return "Address";
            case CommonData.TRAINING_POSE.TRAINING_BACKSWING:
                return "Back Swing Top";
            case CommonData.TRAINING_POSE.TRAINING_IMPACT:
                return "Impact";
            default:
                break;
        }

        return "";
    }

    static public string ConvertPoseAngleTypeStr(CommonData.TRAINING_ANGLE type)
    {
        switch (type)
        {
            case CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE:
                return "SIDE BEND";
            case CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN:
                return "ROTATION";
            case CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND:
                return "BEND";
            default:
                break;
        }

        return "";
    }

    static public string ConvertPoseLevelStr(int level)
    {
        if (level == 0)
            return "";
        else if (level == 1)
            return "비기너";
        else if (level == 2)
            return "중급";
        else 
            return "프로";
    }

    static public string ConvertTrainingTypeStr(CommonData.TRAINING_TYPE type, bool enter = false)
    {
        switch (type)
        {
            case CommonData.TRAINING_TYPE.TRAINING_POSE:
                return "포즈";
            case CommonData.TRAINING_TYPE.TRAINING_TEMPO:
                {
                    if(enter)
                        return "사운드\n트레이닝";
                    else
                        return "사운드 트레이닝";
                }
            default:
                break;
        }

        return "";
    }

    static public string ConvertTrainingTypeEngStr(CommonData.TRAINING_TYPE type, bool enter = false)
    {
        switch (type)
        {
            case CommonData.TRAINING_TYPE.TRAINING_POSE:
                return "포즈";
            case CommonData.TRAINING_TYPE.TRAINING_TEMPO:
                {
                    if (enter)
                        return "Sound\nTraining";
                    else
                        return "Sound Training";
                }
            default:
                break;
        }

        return "";
    }

    static public string GetGradeStr(int grade)
    {
        if (CommonData.GRADE_STR.Length <= grade)
            return "";
        return CommonData.GRADE_STR[grade];
    }

    static public void SetGradeImg(ref Image img, int grade)
    {
        if (CommonData.GRADE_IMG_STR.Length <= grade)
            return;

        SetImageFile(CommonData.GRADE_IMG_STR[grade], ref img);
    }



    static public void RefreshThumbnail(ref Image img)
    {
        if (TKManager.Instance.ThumbnailSprite == null)
        {
            TKManager.Instance.ThumbnailSprite = CommonFunc.GetImageFile(TextureCacheManager.Instance.GetTexture(TKManager.Instance.Mydata.ThumbnailSpriteURL), ref img);
            img.sprite = TKManager.Instance.ThumbnailSprite;
        }
        else
            img.sprite = TKManager.Instance.ThumbnailSprite;
    }

    static public int RefreshGrade(int accumulatePoint)
    {
        int returnGrade = 0;
        var arr = CommonData.GRADE_POINT;
        returnGrade = arr.Length - 1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (accumulatePoint < arr[i])
            {
                returnGrade = i;
                break;
            }

        }

        return returnGrade;
    }

    public static T ToEnum<T>(string value)
    {
        if (!System.Enum.IsDefined(typeof(T), value))
            return default(T);

        return (T)System.Enum.Parse(typeof(T), value, true);
    }
}