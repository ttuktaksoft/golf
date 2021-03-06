﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrainingAngle : MonoBehaviour
{
    public GameObject GaugeOnObj;
    public GameObject GaugeOffObj;
    public GameObject SuccessObj;
    public Image SuccessBg;
    public Image MaxLine;
    public GameObject Circle;
    public Text Angle;
    public Text Title;

    private float MinValue = 0;
    private float MaxValue = 0;

    public void Init(string title)
    {
        GaugeOnObj.gameObject.SetActive(false);
        GaugeOffObj.gameObject.SetActive(true);
        Title.text = title;
    }

    public void Init(string title, float min, float max, float successMin, float successMax)
    {
        GaugeOnObj.gameObject.SetActive(true);
        GaugeOffObj.gameObject.SetActive(false);
        Title.text = title;
        MinValue = min;
        MaxValue = max;

        float value = max - min;
        float successPower = (successMax - successMin) / value;
        SuccessBg.fillAmount = successPower;

        SuccessObj.gameObject.transform.localRotation = Quaternion.Euler(SuccessBg.gameObject.transform.localRotation.x, SuccessBg.gameObject.transform.localRotation.y, -90 + (successPower * 100) * 0.9f);

        float value_2 = (successMax - MinValue) * (180 / value);
        MaxLine.transform.localRotation = Quaternion.Euler(MaxLine.transform.localRotation.x, MaxLine.transform.localRotation.y, (successPower * -180f));

        Circle.transform.localRotation = Quaternion.Euler(Circle.transform.localRotation.x, Circle.transform.localRotation.y, -90f);
        Angle.text = "0";
        //SetAngle(value / 2);
    }

    public void SetAngle(float angle)
    {
        if (MinValue > angle)
            angle = MinValue;
        if(MaxValue < angle)
            angle = MaxValue;

        float value_1 = MaxValue - MinValue;
        float value_2 = (angle - MinValue) * (180 / value_1);
        // TODO 테스트용
        //Angle.text = string.Format("{0} ({1} ~ {2})", (int)angle, MinValue + CommonData.ANGLE_OFFSET, MaxValue - CommonData.ANGLE_OFFSET);
        Angle.text = string.Format("{0}", (int)angle);

        // Circle.transform.localRotation = Quaternion.Euler(Circle.transform.localRotation.x, Circle.transform.localRotation.y, angle * 3.6f);
        Circle.transform.localRotation = Quaternion.Euler(Circle.transform.localRotation.x, Circle.transform.localRotation.y,-value_2);
    }
}
