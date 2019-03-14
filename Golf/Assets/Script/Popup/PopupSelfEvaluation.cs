using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PopupSelfEvaluation : Popup
{
    public Text Date;
    public Text Title;
    public Button OK;
    public List<Button> StarList = new List<Button>();
    public List<Image> StarImgList = new List<Image>();
    public InputField Msg;

    private int StarCount = 1;
    private CommonData.TRAINING_TYPE TrainingType = CommonData.TRAINING_TYPE.TRAINING_POSE;
    private int TrainingCount = 0;
    private int TrainingPoint = 0;
    private CommonData.TRAINING_POSE PoseType = CommonData.TRAINING_POSE.TRAINING_ADDRESS;
    private Dictionary<CommonData.TRAINING_ANGLE, int> AngleTypeList = new Dictionary<CommonData.TRAINING_ANGLE, int>();
    private Action EndAction = null;

    public PopupSelfEvaluation()
        : base(PopupMgr.POPUP_TYPE.SELF_EVALUATION)
    {

    }

    public class PopupData : PopupBaseData
    {
        public int Count = 0;
        public Action EndAction = null;
        public PopupData(int count, Action endAction = null)
        {
            Count = count;
            EndAction = endAction;
        }
    }

    public override void SetData(PopupBaseData data)
    {
        var popupData = data as PopupData;
        if (popupData == null)
            return;

        TrainingCount = popupData.Count;
        EndAction = popupData.EndAction;

        TrainingType = TKManager.Instance.GetTrainingType();
        PoseType = TKManager.Instance.GetPoseType();
        AngleTypeList = TKManager.Instance.GetAngleType();

        DateTime time = new DateTime(DateTime.Now.Ticks);
        Date.text = string.Format("{0:D2}월 {1:D2}일", time.Month, time.Day);

        if (TrainingType == CommonData.TRAINING_TYPE.TRAINING_POSE)
        {
            TrainingPoint = TrainingCount * 3;
            Title.text = string.Format("{0} {1}Point", CommonFunc.ConvertPoseTypeEngStr(PoseType), TrainingPoint);
        }
        else
        {
            TrainingPoint = 0;
            Title.text = string.Format("{0} {1}회", CommonFunc.ConvertTrainingTypeEngStr(TrainingType, false), TrainingCount);
        }
            
        Msg.text = "";
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
            TKManager.Instance.Mydata.AddEvaluationData(
            new EvaluationData(
                DateTime.Now.Ticks,
                CommonData.TRAINING_TYPE.TRAINING_POSE,
                TrainingCount,
                PoseType,
                new Dictionary<CommonData.TRAINING_ANGLE, int>(AngleTypeList),
                StarCount,
                Msg.text.ToString())
            );
        }
        else
        {
            TKManager.Instance.Mydata.AddEvaluationData(
            new EvaluationData(
                DateTime.Now.Ticks,
                CommonData.TRAINING_TYPE.TRAINING_TEMPO,
                TrainingCount,
                StarCount,
                Msg.text.ToString())
            );
        }

        TKManager.Instance.Mydata.AddSeasonPoint(TrainingPoint);
        TKManager.Instance.SaveFile();

        PopupMgr.Instance.DismissPopup();

        if (EndAction != null)
            EndAction();
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
