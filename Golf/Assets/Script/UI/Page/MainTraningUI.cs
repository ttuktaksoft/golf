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
        ANGLE,
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

    private CommonData.TRAINING_TYPE TrainingType = CommonData.TRAINING_TYPE.TRAINING_POSE;
    private TRAINING_SET_STEP TrainingSetStep = TRAINING_SET_STEP.MAIN;
    //private TRANING_TYPE TraningType = TRANING_TYPE.NONE;
    private CommonData.TRAINING_POSE TrainingMode = CommonData.TRAINING_POSE.TRAINING_ADDRESS;

    private Dictionary<CommonData.TRAINING_ANGLE, int> TrainingModeLevel = new Dictionary<CommonData.TRAINING_ANGLE, int>();
    private Dictionary<CommonData.TRAINING_ANGLE, int> TrainingModeLevelBtnIndex = new Dictionary<CommonData.TRAINING_ANGLE, int>();
    private Dictionary<int, int> TempoTrainingModeLevelBtnIndex = new Dictionary<int, int>();
    private int TrainingTimeIndex = 0;
    private int TrainingTimeBtnIndex = 0;

    public bool MenuAction = false;

    private void Awake()
    {
        HexagonMenuColor.Clear();
        HexagonMenuColor.Add("#4E4E4E");
        HexagonMenuColor.Add("#464646");
        HexagonMenuColor.Add("#383838");
        HexagonMenuColor.Add("#333333");
        HexagonMenuColor.Add("#2C2C2C");
        HexagonMenuColor.Add("#252525");

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

        if(TrainingType == CommonData.TRAINING_TYPE.TRAINING_POSE)
        {
            if(TrainingSetStep == TRAINING_SET_STEP.MAIN)
            {
                TraningTypeText.text = "";
                CenterButton.Init("", "logo", CommonFunc.HexToColor(HexagonCenterColor, 0f), null);
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
                        HexagonMenu[i].Init("사용방법", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTutorial);
                }
            }
            else if (TrainingSetStep == TRAINING_SET_STEP.POSE)
            {
                TraningTypeText.text = "";
                CenterButton.Init("", "logo", CommonFunc.HexToColor(HexagonCenterColor, 0f), null);
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
                        HexagonMenu[i].Init("사용방법", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTutorial);
                }
            }
            else if (TrainingSetStep == TRAINING_SET_STEP.ANGLE)
            {
                CenterButton.Init("트레이닝\n시작", "", CommonFunc.HexToColor(HexagonCenterColor, 0f), OnClickTraningStart);

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
                        TrainingModeLevel.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND, 1);
                        TrainingModeLevelBtnIndex.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND, i);
                    }
                    else if (i == 2)
                    {
                        HexagonMenu[i].Init(CommonFunc.ConvertPoseAngleTypeStr(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN), "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickLevelTurn);
                        TrainingModeLevel.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN, 1);
                        TrainingModeLevelBtnIndex.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN, i);
                    }
                    else if (i == 3)
                    {
                        HexagonMenu[i].Init(CommonFunc.ConvertPoseAngleTypeStr(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE), "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickLevelSide);
                        TrainingModeLevel.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE, 1);
                        TrainingModeLevelBtnIndex.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE, i);
                    }
                    else if (i == 4)
                    {
                        HexagonMenu[i].Init("사용방법", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTutorial);
                        TrainingTimeBtnIndex = i;
                    }
                    else if (i == 5)
                    {
                        TKManager.Instance.MirrorMode = false;
                        HexagonMenu[i].Init("거울보기\n모드", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickMirrorMode);
                    }
                }

                ChangeTraningPosLevel();
            }
        }
        else
        {
            TraningTypeText.text = "Sound Training";
            CenterButton.Init("트레이닝\n시작", "", CommonFunc.HexToColor(HexagonCenterColor, 0f), OnClickTraningStart);

            TempoTrainingModeLevelBtnIndex.Clear();

            for (int i = 0; i < HexagonMenu.Count; i++)
            {
                if (i == 0)
                {
                    HexagonMenu[i].Init("", "icon_back", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickBack);
                    HexagonMenu[i].SetImageSize(new Vector2(227.3f, 168.1f));
                }
                else if (i == 1)
                {
                    HexagonMenu[i].Init("비기너", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTempoLevel_1);
                    TempoTrainingModeLevelBtnIndex.Add(1, i);
                }
                else if (i == 2)
                {
                    HexagonMenu[i].Init("중급", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTempoLevel_2);
                    TempoTrainingModeLevelBtnIndex.Add(2, i);
                }
                else if (i == 3)
                {
                    HexagonMenu[i].Init("프로", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTempoLevel_3);
                    TempoTrainingModeLevelBtnIndex.Add(3, i);
                }
                else if (i == 4)
                    HexagonMenu[i].Init("아카데미\n소개", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickAcademyInfo);
                else if (i == 5)
                    HexagonMenu[i].Init("사용방법", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTutorial);
            }

            ChangeTempoTraningPosLevel();
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

        TrainingType = CommonData.TRAINING_TYPE.TRAINING_POSE;
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

        TrainingType = CommonData.TRAINING_TYPE.TRAINING_TEMPO;
        StartMenuAction();
    }

    public void OnClickAcademyInfo()
    {
        if (MenuAction)
            return;

        Application.OpenURL(DataManager.Instance.AcademyURL);
    }

    public void OnClickTutorial()
    {
        if (MenuAction)
            return;

        if (TrainingType == CommonData.TRAINING_TYPE.TRAINING_POSE)
        {
            if (TrainingSetStep == TRAINING_SET_STEP.MAIN)
            {
                PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.TUTORIAL, new PopupTutorial.PopupData(TutorialData.TUTORIAL_TYPE.MAIN));
            }
            else if (TrainingSetStep == TRAINING_SET_STEP.ANGLE)
            {
                PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.TUTORIAL, new PopupTutorial.PopupData(TutorialData.TUTORIAL_TYPE.ANGLE));
            }
            else
            {
                PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.TUTORIAL, new PopupTutorial.PopupData(TutorialData.TUTORIAL_TYPE.POSE));
            }
        }
        else
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.TUTORIAL, new PopupTutorial.PopupData(TutorialData.TUTORIAL_TYPE.TEMPO));
        }

        return;
        List<string> list = new List<string>();
        
        if(TrainingType == CommonData.TRAINING_TYPE.TRAINING_POSE)
        {
            if (TrainingSetStep == TRAINING_SET_STEP.MAIN)
            {
                list.Add("t_1");
                list.Add("t_2");
                PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.HELP, new PopupHelp.PopupData(list));
            }
            else if(TrainingSetStep == TRAINING_SET_STEP.ANGLE)
            {
                Application.OpenURL("https://www.youtube.com/watch?v=X-e_V2SYguQ");
            }
            else
            {
                list.Add("t_1_1");
                list.Add("t_1_2");
                list.Add("t_1_3");
                list.Add("t_1_4");
                list.Add("t_1_5");
                PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.HELP, new PopupHelp.PopupData(list));
            }
        }
        else
        {
            Application.OpenURL("https://www.youtube.com/watch?v=rjwRbsmFO1g");
            /*
            list.Add("t_2_1");
            list.Add("t_2_2");
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.HELP, new PopupHelp.PopupData(list));
            */
        }
        
    }

    public void OnClickMirrorMode()
    {
        if (MenuAction)
            return;

        TKManager.Instance.MirrorMode = !TKManager.Instance.MirrorMode;

        if(TKManager.Instance.MirrorMode)
        {
            HexagonMenu[5].SetText("미러링\n모드");
            HexagonMenu[5].SetButtonColor(CommonFunc.HexToColor(HexagonMenuColor[5], 1f));
        }
        else
        {
            HexagonMenu[5].SetText("거울보기\n모드");
            HexagonMenu[5].SetButtonColor(CommonFunc.HexToColor(HexagonMenuColor[5], 1f));
        }
    }

    public void OnClickImpact()
    {
        if (MenuAction)
            return;
        
        TrainingSetStep = TRAINING_SET_STEP.ANGLE;
        TrainingMode = CommonData.TRAINING_POSE.TRAINING_IMPACT;
        TraningTypeText.text = "Impact";// CommonFunc.ConvertPoseTypeStr(TrainingMode);

        StartMenuAction();
    }

    public void OnClickBackSwingTop()
    {
        if (MenuAction)
            return;

        TrainingSetStep = TRAINING_SET_STEP.ANGLE;
        TrainingMode = CommonData.TRAINING_POSE.TRAINING_BACKSWING;
        TraningTypeText.text = "Back Swing Top";// CommonFunc.ConvertPoseTypeStr(TrainingMode);

        StartMenuAction();
    }

    public void OnClickAddress()
    {
        if (MenuAction)
            return;

        TrainingSetStep = TRAINING_SET_STEP.ANGLE;
        TrainingMode = CommonData.TRAINING_POSE.TRAINING_ADDRESS;
        TraningTypeText.text = "Address"; //CommonFunc.ConvertPoseTypeStr(TrainingMode);

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
                HexagonMenu[btnIndex].SetText(string.Format("{0}\nOFF",CommonFunc.ConvertPoseAngleTypeStr(type)));
            else
                HexagonMenu[btnIndex].SetText(string.Format("{0}\n{1}", CommonFunc.ConvertPoseAngleTypeStr(type), CommonFunc.ConvertPoseLevelStr(level)));
        }
    }

    public void ChangeTempoTraningPosLevel()
    {
        var enumerator = TempoTrainingModeLevelBtnIndex.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var type = enumerator.Current.Key;
            var btnIndex = enumerator.Current.Value;
            var level = TKManager.Instance.TempoTrainingLevel;

            if (level == enumerator.Current.Key)
                HexagonMenu[btnIndex].SetButtonColor(CommonFunc.HexToColor(HexagonMenuColor[btnIndex], 1f));
            else
                HexagonMenu[btnIndex].SetButtonColor(CommonFunc.HexToColor(HexagonMenuColor[btnIndex], 0.5f));
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

    public void OnClickTempoLevel_1()
    {
        TKManager.Instance.TempoTrainingLevel = 1;
        ChangeTempoTraningPosLevel();
    }

    public void OnClickTempoLevel_2()
    {
        TKManager.Instance.TempoTrainingLevel = 2;
        ChangeTempoTraningPosLevel();
    }

    public void OnClickTempoLevel_3()
    {
        TKManager.Instance.TempoTrainingLevel = 3;
        ChangeTempoTraningPosLevel();
    }

    public void OnClickBack()
    {
        if (MenuAction)
            return;

        if (TrainingType == CommonData.TRAINING_TYPE.TRAINING_POSE)
        {
            TrainingSetStep--;
            if (TrainingSetStep < 0)
                TrainingSetStep = TRAINING_SET_STEP.MAIN;
        }
        else
        {
            TrainingType = CommonData.TRAINING_TYPE.TRAINING_POSE;
            TrainingSetStep = TRAINING_SET_STEP.MAIN;
        }

        StartMenuAction();
    }

    public void OnClickTraningStart()
    {
        if (MenuAction)
            return;

        if(TrainingType == CommonData.TRAINING_TYPE.TRAINING_POSE)
        {
            bool startEnable = false;
            var enumerator = TrainingModeLevel.GetEnumerator();
            while (enumerator.MoveNext())
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
        else
        {
            TKManager.Instance.SetTrainingType(CommonData.TRAINING_TYPE.TRAINING_TEMPO);
            SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
        }
    }

    public bool IsQuitApp()
    {
        if (TrainingType == CommonData.TRAINING_TYPE.TRAINING_POSE && 
            TrainingSetStep == TRAINING_SET_STEP.MAIN)
            return true;

        return false;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_ANDROID
        if (PopupMgr.Instance.IsShowPopup(PopupMgr.POPUP_TYPE.MSG) == false && Input.GetKeyUp(KeyCode.Escape))
        {
            if(MenuAction == false && IsQuitApp() == false)
                OnClickBack();
        }
#endif
    }
}
