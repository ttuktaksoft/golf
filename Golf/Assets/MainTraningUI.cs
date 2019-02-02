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
    
    private List<string> HexagonMenuColor = new List<string>();
    private List<Vector3> HexagonMenuPos = new List<Vector3>();
    private List<string> TrainingModeLevelStr = new List<string>();

    private TRAINING_SET_STEP TrainingSetStep = TRAINING_SET_STEP.MAIN;
    //private TRANING_TYPE TraningType = TRANING_TYPE.NONE;
    private CommonData.TRAINING_MODE TrainingMode = CommonData.TRAINING_MODE.TRAINING_ADDRESS;

    private Dictionary<CommonData.TRAINING_ANGLE, int> TrainingModeLevel = new Dictionary<CommonData.TRAINING_ANGLE, int>();
    private Dictionary<CommonData.TRAINING_ANGLE, int> TrainingModeLevelBtnIndex = new Dictionary<CommonData.TRAINING_ANGLE, int>();

    private int TrainingTimeIndex = 0;
    private int TrainingTimeBtnIndex = 0;

    public bool MenuAction = false;

    private void Awake()
    {
        HexagonMenuColor.Clear();
        HexagonMenuColor.Add("#e93232");
        HexagonMenuColor.Add("#731818");
        HexagonMenuColor.Add("#8c1e1e");
        HexagonMenuColor.Add("#b02525");
        HexagonMenuColor.Add("#c32a2a");
        HexagonMenuColor.Add("#d92d2d");

        HexagonMenuPos.Clear();
        HexagonMenuPos.Add(new Vector3(-350, 0, 0));
        HexagonMenuPos.Add(new Vector3(-175, 300, 0));
        HexagonMenuPos.Add(new Vector3(175, 300, 0));
        HexagonMenuPos.Add(new Vector3(350, 0, 0));
        HexagonMenuPos.Add(new Vector3(175, -300, 0));
        HexagonMenuPos.Add(new Vector3(-175, -300, 0));

        TrainingModeLevelStr.Clear();
        TrainingModeLevelStr.Add("");
        TrainingModeLevelStr.Add("\n비기너");
        TrainingModeLevelStr.Add("\n중급");
        TrainingModeLevelStr.Add("\n프로");

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
        if (TrainingSetStep == TRAINING_SET_STEP.MAIN)
        {
            TraningTypeText.text = "";
            CenterButton.Init("", "logo", new Color(1, 1, 1, 1f), null);
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
            CenterButton.Init("", "logo", new Color(1, 1, 1, 1f), null);
            CenterButton.SetImageSize(new Vector2(227.3f, 262.3f));

            for (int i = 0; i < HexagonMenu.Count; i++)
            {
                if (i == 0)
                {
                    HexagonMenu[i].Init("", "icon_back", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickBack);
                    HexagonMenu[i].SetImageSize(new Vector2(227.3f, 168.1f));
                }
                else if (i == 1)
                    HexagonMenu[i].Init("어드레스", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickAddress);
                else if (i == 2)
                    HexagonMenu[i].Init("백스윙 탑", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickBackSwingTop);
                else if (i == 3)
                    HexagonMenu[i].Init("임팩트", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickImpact);
                else if (i == 4)
                    HexagonMenu[i].Init("사운드\n트레이닝", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickSoundTraning);
                else if (i == 5)
                    HexagonMenu[i].Init("튜토리얼\n영상", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTutorial);
            }
        }
        else if (TrainingSetStep == TRAINING_SET_STEP.ANGLE)
        {
            CenterButton.Init("트레이닝\n시작", "", CommonFunc.HexToColor("#393939", 1f), OnClickTraningStart);

            TrainingModeLevel.Clear();
            TrainingModeLevelBtnIndex.Clear();

            for (int i = 0; i < HexagonMenu.Count; i++)
            {
                if (i == 0)
                {
                    HexagonMenu[i].Init("", "icon_back", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickBack);
                    HexagonMenu[i].SetImageSize(new Vector2(227.3f, 168.1f));
                }
                else if (i == 1)
                {
                    HexagonMenu[i].Init("BEND", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickLevelBend);
                    TrainingModeLevel.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND, 0);
                    TrainingModeLevelBtnIndex.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND, i);
                }
                else if (i == 2)
                {
                    HexagonMenu[i].Init("ROTATION", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickLevelTurn);
                    TrainingModeLevel.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN, 0);
                    TrainingModeLevelBtnIndex.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN, i);
                }
                else if (i == 3)
                {
                    HexagonMenu[i].Init("SIDE BEND", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickLevelSide);
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

        TKManager.Instance.SetMode(CommonData.TRAINING_MODE.TRAINING_TEMPO);
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }

    public void OnClickAcademyInfo()
    {
        if (MenuAction)
            return;
    }

    public void OnClickTutorial()
    {
        if (MenuAction)
            return;
    }

    public void OnClickImpact()
    {
        if (MenuAction)
            return;

        TraningTypeText.text = "임팩트";
        TrainingSetStep = TRAINING_SET_STEP.ANGLE;
        TrainingMode = CommonData.TRAINING_MODE.TRAINING_IMPACT;

        StartMenuAction();
    }

    public void OnClickBackSwingTop()
    {
        if (MenuAction)
            return;

        TraningTypeText.text = "백스윙 탑";
        TrainingSetStep = TRAINING_SET_STEP.ANGLE;
        TrainingMode = CommonData.TRAINING_MODE.TRAINING_BACKSWING;

        StartMenuAction();
    }

    public void OnClickAddress()
    {
        if (MenuAction)
            return;

        TraningTypeText.text = "어드레스";
        TrainingSetStep = TRAINING_SET_STEP.ANGLE;
        TrainingMode = CommonData.TRAINING_MODE.TRAINING_ADDRESS;

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
  
            switch (type)
            {
                case CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE:
                    HexagonMenu[btnIndex].SetText(string.Format("SIDE BEND{0}", TrainingModeLevelStr[level]));
                    break;
                case CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN:
                    HexagonMenu[btnIndex].SetText(string.Format("ROTATION{0}", TrainingModeLevelStr[level]));
                    break;
                case CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND:
                    HexagonMenu[btnIndex].SetText(string.Format("BEND{0}", TrainingModeLevelStr[level]));
                    break;
                default:
                    break;
            }
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

        if (TrainingModeLevelStr.Count <= TrainingModeLevel[type])
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
        if (TrainingModeLevel.Count <= 0)
        {
            // TODO 팝업
            return;
        }
            

        TKManager.Instance.SetAngleType(TrainingModeLevel);
        TKManager.Instance.SetMode(TrainingMode);
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }
}
