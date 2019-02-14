﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PopupSelfEvaluation : Popup
{
    public Text Date;
    public Text Title;
    public Text SubTitle;
    public Button OK;
    public List<Button> StarList = new List<Button>();
    public List<Image> StarImgList = new List<Image>();
    public InputField Msg;

    private int StarCount = 1;
    private CommonData.TRAINING_TYPE TrainingType = CommonData.TRAINING_TYPE.TRAINING_POSE;
    private int TrainingCount = 0;
    private CommonData.TRAINING_POSE PoseType = CommonData.TRAINING_POSE.TRAINING_ADDRESS;
    private Dictionary<CommonData.TRAINING_ANGLE, int> AngleTypeList = new Dictionary<CommonData.TRAINING_ANGLE, int>();


    public PopupSelfEvaluation()
        : base(PopupMgr.POPUP_TYPE.SELF_EVALUATION)
    {

    }

    public class PopupData : PopupBaseData
    {
        public PopupData()
        {
        }
    }

    public override void SetData(PopupBaseData data)
    {
        TrainingType = TKManager.Instance.GetTrainingType();
        TrainingCount = 12;
        PoseType = TKManager.Instance.GetPoseType();
        AngleTypeList = TKManager.Instance.GetAngleType();

        DateTime time = new DateTime(DateTime.Now.Ticks);
        Date.text = string.Format("{0:D2}월 {1:D2}일", time.Month, time.Day);

        if (TrainingType == CommonData.TRAINING_TYPE.TRAINING_POSE)
        {
            Title.text = string.Format("{0} {1}회", CommonFunc.ConvertPoseTypeStr(PoseType), TrainingCount);
        }
        else
            Title.text = string.Format("{0} {1}회", CommonFunc.ConvertTrainingTypeStr(TrainingType, false), TrainingCount);

        StringBuilder builder = new StringBuilder();
        var enumerator = AngleTypeList.GetEnumerator();
        int count = AngleTypeList.Count;

        while (enumerator.MoveNext())
        {
            count--;
            if (enumerator.Current.Value == 0)
                continue;
            builder.Append(string.Format("{0} {1}", CommonFunc.ConvertPoseAngleTypeStr(enumerator.Current.Key), CommonFunc.ConvertPoseLevelStr(enumerator.Current.Value)));
            if (count > 0)
                builder.Append(",");
        }

        SubTitle.text = builder.ToString();

        RefreshStar();
    }


    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
        StarList[0].onClick.AddListener(OnClickStar_1);
        StarList[1].onClick.AddListener(OnClickStar_2);
        StarList[2].onClick.AddListener(OnClickStar_3);
        StarList[3].onClick.AddListener(OnClickStar_4);
        StarList[4].onClick.AddListener(OnClickStar_5);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public void OnClickOK()
    {
        if(TrainingType == CommonData.TRAINING_TYPE.TRAINING_POSE)
        {
            DataManager.Instance.AddEvaluationData(
            new EvaluationData(
                DateTime.Now.Ticks,
                CommonData.TRAINING_TYPE.TRAINING_POSE,
                TrainingCount,
                PoseType,
                AngleTypeList,
                StarCount,
                Msg.text.ToString())
            );
        }
        else
        {
            DataManager.Instance.AddEvaluationData(
            new EvaluationData(
                DateTime.Now.Ticks,
                CommonData.TRAINING_TYPE.TRAINING_TEMPO,
                TrainingCount,
                StarCount,
                Msg.text.ToString())
            );
        }
        
        PopupMgr.Instance.DismissPopup();
    }

    public void RefreshStar()
    {
        for (int i = 0; i < StarImgList.Count; i++)
        {
            var img = StarImgList[i];
            if (StarCount - 1 >= i)
                CommonFunc.SetImageFile("star", ref img);
            else
                CommonFunc.SetImageFile("star_empty", ref img);
        }
    }

    public void OnClickStar_1()
    {
        StarCount = 1;
        RefreshStar();
    }
    public void OnClickStar_2()
    {
        StarCount = 2;
        RefreshStar();
    }
    public void OnClickStar_3()
    {
        StarCount = 3;
        RefreshStar();
    }
    public void OnClickStar_4()
    {
        StarCount = 4;
        RefreshStar();
    }
    public void OnClickStar_5()
    {
        StarCount = 5;
        RefreshStar();
    }
}