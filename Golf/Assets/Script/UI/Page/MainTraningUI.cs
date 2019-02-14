using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainTraningUI : MonoBehaviour {
    
    public enum TRAINING_SET_STEP
    {
        MAIN,
        POSE,
        ANGLE
    }

    public enum TRANING_TYPE
    {
        NONE,
        POSE,
        TEMPO,
    }

    //public enum TRANING_POSE_STEP
    //{
    //    NONE,
    //    SELECT,
    //    LEVEL,
    //}

    //public enum TRANING_POSE_TYPE
    //{
    //    NONE,
    //    IMPACT,
    //    BACK_SWING_TOP,
    //    ADDRESS
    //}

    //public enum TRANING_ANGLE_TYPE
    //{
    //    NONE,
    //    SIDE,
    //    TURN,
    //    BEND,
    //}

    

    private MainUI MainUIObj;

    public UIHexagonButton CenterButton;
    public List<UIHexagonButton> HexagonMenu;
    public Text TraningTypeText;
    public Text AngleSelectInfoText;

    private string HexagonCenterColor = "#686666";
    private List<string> HexagonMenuColor = new List<string>();
    private List<Vector3> HexagonMenuPos = new List<Vector3>();

    private TRAINING_SET_STEP TrainingSetStep = TRAINING_SET_STEP.MAIN;
    //private TRANING_TYPE TraningType = TRANING_TYPE.NONE;
    private CommonData.TRAINING_POSE TrainingMode = CommonData.TRAINING_POSE.TRAINING_ADDRESS;

    private Dictionary<CommonData.TRAINING_ANGLE, int> TrainingModeLevel = new Dictionary<CommonData.TRAINING_ANGLE, int>();
    private Dictionary<CommonData.TRAINING_ANGLE, int> TrainingModeLevelBtnIndex = new Dictionary<CommonData.TRAINING_ANGLE, int>();

    private int TrainingTimeIndex = 0;
    private int TrainingTimeBtnIndex = 0;

    public bool MenuAction = false;

    private void Awake()
    {
        HexagonMenuColor.Clear();
        HexagonMenuColor.Add("#c8000b");
        HexagonMenuColor.Add("#e70012");
        HexagonMenuColor.Add("#c8000b");
        HexagonMenuColor.Add("#a40001");
        HexagonMenuColor.Add("#7d0000");
        HexagonMenuColor.Add("#a40001");

        HexagonMenuPos.Clear();
        HexagonMenuPos.Add(new Vector3(-350, 0, 0));
        HexagonMenuPos.Add(new Vector3(-175, 300, 0));
        HexagonMenuPos.Add(new Vector3(175, 300, 0));
        HexagonMenuPos.Add(new Vector3(350, 0, 0));
        HexagonMenuPos.Add(new Vector3(175, -300, 0));
        HexagonMenuPos.Add(new Vector3(-175, -300, 0));
    }

    public void Start()
    {
    }

    public void Init(MainUI obj)
    {
        MainUIObj = obj;

        TrainingSetStep = TRAINING_SET_STEP.MAIN;
        TrainingTimeIndex = 0;

        for (int i = 0; i < HexagonMenu.Count; i++)
        {
            HexagonMenu[i].gameObject.transform.localPosition = HexagonMenuPos[i];
        }

        SetMenu();
    }

    private void SetMenu()
    {
        AngleSelectInfoText.gameObject.SetActive(false);

        if (TrainingSetStep == TRAINING_SET_STEP.MAIN)
        {
            TraningTypeText.text = "";
            CenterButton.Init("", "logo", CommonFunc.HexToColor(HexagonCenterColor, 0.5f), null);
            CenterButton.SetImageSize(new Vector2(227.3f, 262.3f));

            for (int i = 0; i < HexagonMenu.Count; i++)
            {
                if (i == 0)
                    HexagonMenu[i].Init("자세\n트레이닝", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickPoseTraning);
                else if (i == 1)
                    HexagonMenu[i].Init("트레이닝\n리워드", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTraningReward);
                else if (i == 2)
                    HexagonMenu[i].Init("기타 알림", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickAlarm);
                else if (i == 3)
                    HexagonMenu[i].Init("사운드\n트레이닝", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickSoundTraning);
                else if (i == 4)
                    HexagonMenu[i].Init("아카데미\n소개", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickAcademyInfo);
                else if (i == 5)
                    HexagonMenu[i].Init("튜토리얼\n영상", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTutorial);
            }
        }
        else if(TrainingSetStep == TRAINING_SET_STEP.POSE)
        {
            TraningTypeText.text = "";
            CenterButton.Init("", "logo", CommonFunc.HexToColor(HexagonCenterColor, 0.5f), null);
            CenterButton.SetImageSize(new Vector2(227.3f, 262.3f));

            for (int i = 0; i < HexagonMenu.Count; i++)
            {
                if (i == 0)
                {
                    HexagonMenu[i].Init("", "icon_back", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickBack);
                    HexagonMenu[i].SetImageSize(new Vector2(227.3f, 168.1f));
                }
                else if (i == 1)
                    HexagonMenu[i].Init(CommonFunc.ConvertPoseTypeStr(CommonData.TRAINING_POSE.TRAINING_ADDRESS), "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickAddress);
                else if (i == 2)
                    HexagonMenu[i].Init(CommonFunc.ConvertPoseTypeStr(CommonData.TRAINING_POSE.TRAINING_BACKSWING), "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickBackSwingTop);
                else if (i == 3)
                    HexagonMenu[i].Init(CommonFunc.ConvertPoseTypeStr(CommonData.TRAINING_POSE.TRAINING_IMPACT), "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickImpact);
                else if (i == 4)
                    HexagonMenu[i].Init(CommonFunc.ConvertTrainingTypeStr(CommonData.TRAINING_TYPE.TRAINING_TEMPO, true), "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickSoundTraning);
                else if (i == 5)
                    HexagonMenu[i].Init("튜토리얼\n영상", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTutorial);
            }
        }
        else if (TrainingSetStep == TRAINING_SET_STEP.ANGLE)
        {
            CenterButton.Init("트레이닝\n시작", "", CommonFunc.HexToColor(HexagonCenterColor, 0.5f), OnClickTraningStart);

            TrainingModeLevel.Clear();
            TrainingModeLevelBtnIndex.Clear();

            AngleSelectInfoText.gameObject.SetActive(true);

            for (int i = 0; i < HexagonMenu.Count; i++)
            {
                if (i == 0)
                {
                    HexagonMenu[i].Init("", "icon_back", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickBack);
                    HexagonMenu[i].SetImageSize(new Vector2(227.3f, 168.1f));
                }
                else if (i == 1)
                {
                    HexagonMenu[i].Init(CommonFunc.ConvertPoseAngleTypeStr(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND), "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickLevelBend);
                    TrainingModeLevel.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND, 0);
                    TrainingModeLevelBtnIndex.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND, i);
                }
                else if (i == 2)
                {
                    HexagonMenu[i].Init(CommonFunc.ConvertPoseAngleTypeStr(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN), "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickLevelTurn);
                    TrainingModeLevel.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN, 0);
                    TrainingModeLevelBtnIndex.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN, i);
                }
                else if (i == 3)
                {
                    HexagonMenu[i].Init(CommonFunc.ConvertPoseAngleTypeStr(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE), "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickLevelSide);
                    TrainingModeLevel.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE, 0);
                    TrainingModeLevelBtnIndex.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE, i);
                }
                else if (i == 4)
                {
                    HexagonMenu[i].Init("트레이닝 시간\n5초", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTrainingTime);
                    TrainingTimeBtnIndex = i;
                }
                else if (i == 5)
                    HexagonMenu[i].Init("튜토리얼\n영상", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTutorial);
            }

            ChangeTraningPosLevel();
        }
    }

    public void StartMenuAction()
    {
        MenuAction = true;
        StartCoroutine(Co_MenuAction());
    }

    IEnumerator Co_MenuAction()
    {
        for (int i = 0; i < HexagonMenu.Count; i++)
        {
            HexagonMenu[i].gameObject.transform.localPosition = HexagonMenuPos[i];
        }

        float waitTime = 0f;
        for (int i = 0; i < HexagonMenu.Count; i++)
        {
            float delayTime = i * 0.05f;
            float time = 0.3f;
            iTween.MoveTo(HexagonMenu[i].gameObject, iTween.Hash("position", new Vector3(0, 0, 0), "delay", delayTime, "time", time, "islocal", true, "movetopath", false, "easetype", iTween.EaseType.easeInBack));

            if (waitTime < delayTime + time)
                waitTime = delayTime + time;
        }

        yield return new WaitForSeconds(waitTime);

        SetMenu();

        yield return new WaitForSeconds(0.005f);

        waitTime = 0;

        for (int i = 0; i < HexagonMenu.Count; i++)
        {
            float delayTime = i * 0.05f;
            float time = 0.3f;
            iTween.MoveTo(HexagonMenu[i].gameObject, iTween.Hash("position", HexagonMenuPos[i], "delay", delayTime, "time", time, "islocal", true, "movetopath", false, "easetype", iTween.EaseType.easeOutBack));

            if (waitTime < delayTime + time)
                waitTime = delayTime + time;
        }

        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < HexagonMenu.Count; i++)
        {
            HexagonMenu[i].gameObject.transform.localPosition = HexagonMenuPos[i];
        }

        yield return null;
        MenuAction = false;
    }


    public void OnClickPoseTraning()
    {
        if (MenuAction)
            return;

        TrainingSetStep = TRAINING_SET_STEP.POSE;

        StartMenuAction();
    }

    public void OnClickTraningReward()
    {
        if (MenuAction)
            return;
        MainUIObj.OnClickReward();
    }

    public void OnClickAlarm()
    {
        if (MenuAction)
            return;

        MainUIObj.OnClickAlarm();
    }

    public void OnClickSoundTraning()
    {
        if (MenuAction)
            return;

        TKManager.Instance.SetTrainingType(CommonData.TRAINING_TYPE.TRAINING_TEMPO);
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }

    public void OnClickAcademyInfo()
    {
        if (MenuAction)
            return;

        Application.OpenURL("http://mygolfbiz.kr/about-us");
    }

    public void OnClickTutorial()
    {
        if (MenuAction)
            return;
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.TUTORIAL, new PopupTutorial.PopupData());
    }

    public void OnClickImpact()
    {
        if (MenuAction)
            return;

        TraningTypeText.text = CommonFunc.ConvertPoseTypeStr(CommonData.TRAINING_POSE.TRAINING_IMPACT);
        TrainingSetStep = TRAINING_SET_STEP.ANGLE;
        TrainingMode = CommonData.TRAINING_POSE.TRAINING_IMPACT;

        StartMenuAction();
    }

    public void OnClickBackSwingTop()
    {
        if (MenuAction)
            return;

        TraningTypeText.text = CommonFunc.ConvertPoseTypeStr(CommonData.TRAINING_POSE.TRAINING_BACKSWING);
        TrainingSetStep = TRAINING_SET_STEP.ANGLE;
        TrainingMode = CommonData.TRAINING_POSE.TRAINING_BACKSWING;

        StartMenuAction();
    }

    public void OnClickAddress()
    {
        if (MenuAction)
            return;

        TraningTypeText.text = CommonFunc.ConvertPoseTypeStr(CommonData.TRAINING_POSE.TRAINING_ADDRESS);
        TrainingSetStep = TRAINING_SET_STEP.ANGLE;
        TrainingMode = CommonData.TRAINING_POSE.TRAINING_ADDRESS;

        StartMenuAction();
    }

    public void ChangeTraningPosLevel()
    {
        var enumerator = TrainingModeLevelBtnIndex.GetEnumerator();

        while(enumerator.MoveNext())
        {
            var type = enumerator.Current.Key;
            var btnIndex = enumerator.Current.Value;
            var level = TrainingModeLevel[type];
            if(level == 0)
                HexagonMenu[btnIndex].SetButtonColor(CommonFunc.HexToColor(HexagonMenuColor[btnIndex], 0.5f));
            else
                HexagonMenu[btnIndex].SetButtonColor(CommonFunc.HexToColor(HexagonMenuColor[btnIndex], 1f));

            if(level == 0)
                HexagonMenu[btnIndex].SetText(string.Format("{0}",CommonFunc.ConvertPoseAngleTypeStr(type)));
            else
                HexagonMenu[btnIndex].SetText(string.Format("{0}\n{1}", CommonFunc.ConvertPoseAngleTypeStr(type), CommonFunc.ConvertPoseLevelStr(level)));
        }
    }

    public void OnClickLevelSide()
    {
        if (MenuAction)
            return;

        AngleLevelUp(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE);
        ChangeTraningPosLevel();
    }

    public void OnClickLevelTurn()
    {
        if (MenuAction)
            return;

        AngleLevelUp(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN);
        ChangeTraningPosLevel();
    }

    public void OnClickLevelBend()
    {
        if (MenuAction)
            return;


        AngleLevelUp(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND);
        ChangeTraningPosLevel();
    }

    public void AngleLevelUp(CommonData.TRAINING_ANGLE type)
    {
        TrainingModeLevel[type]++;

        if (CommonData.ANGLE_MAX_LEVEL <= TrainingModeLevel[type])
            TrainingModeLevel[type] = 0;
    }

    public void OnClickTrainingTime()
    {
        TrainingTimeIndex++;

        if (CommonData.TRAINING_TIME.Length <= TrainingTimeIndex)
            TrainingTimeIndex = 0;
        HexagonMenu[TrainingTimeBtnIndex].SetText(string.Format("트레이닝 시간\n{0}초", CommonData.TRAINING_TIME[TrainingTimeIndex]));
    }

    public void OnClickBack()
    {
        if (MenuAction)
            return;

        TrainingSetStep--;
        if (TrainingSetStep < 0)
            TrainingSetStep = TRAINING_SET_STEP.MAIN;

        StartMenuAction();
    }

    public void OnClickTraningStart()
    {
        if (MenuAction)
            return;

        bool startEnable = false;
        var enumerator = TrainingModeLevel.GetEnumerator();
        while(enumerator.MoveNext())
        {
            if (enumerator.Current.Value > 0)
            {
                startEnable = true;
                break;
            }
        }

        if (startEnable == false)
        {
            // TODO 팝업
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("트레이닝을 시작 할 수 없습니다."));
            return;
        }

        TKManager.Instance.SetTrainingTimer(CommonData.TRAINING_TIME[TrainingTimeIndex]);
        TKManager.Instance.SetAngleType(TrainingModeLevel);
        TKManager.Instance.SetMode(TrainingMode);
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }
}
