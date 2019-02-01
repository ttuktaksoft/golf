using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TraningUI : MonoBehaviour {
    
    public enum TRANING_POSE_STEP
    {
        NONE,
        SELECT,
        LEVEL,
    }

    public enum TRANING_POSE_TYPE
    {
        NONE,
        IMPACT,
        BACK_SWING_TOP,
        ADDRESS
    }

    public enum TRANING_POSE_LEVEL_TYPE
    {
        NONE,
        SIDE,
        TURN,
        BEND,
    }

    public enum TRANING_TYPE
    {
        NONE,
        POSE,
        RHYTHM,
    }

    private MainUI MainUIObj;

    public UIHexagonButton CenterButton;
    public List<UIHexagonButton> HexagonMenu;
    public Text TraningTypeText;

    private TRANING_TYPE TraningType = TRANING_TYPE.NONE;
    private TRANING_POSE_STEP TraningStep = TRANING_POSE_STEP.NONE;
    private TRANING_POSE_TYPE TraningPoseType = TRANING_POSE_TYPE.NONE;

    private List<string> HexagonMenuColor = new List<string>();
    private List<Vector3> HexagonMenuPos = new List<Vector3>();
    private List<string> TraningPoseLevelStr = new List<string>();
    private Dictionary<TRANING_POSE_LEVEL_TYPE, int> TraningPoseLevel = new Dictionary<TRANING_POSE_LEVEL_TYPE, int>();
    private Dictionary<TRANING_POSE_LEVEL_TYPE, int> TraningPoseLevelBtnIndex = new Dictionary<TRANING_POSE_LEVEL_TYPE, int>();

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

        TraningPoseLevelStr.Clear();
        TraningPoseLevelStr.Add("");
        TraningPoseLevelStr.Add("\n비기너");
        TraningPoseLevelStr.Add("\n중급");
        TraningPoseLevelStr.Add("\n프로");

    }

    public void Start()
    {
    }

    public void Init(MainUI obj)
    {
        MainUIObj = obj;

        TraningType = TRANING_TYPE.NONE;
        TraningStep = TRANING_POSE_STEP.NONE;

        for (int i = 0; i < HexagonMenu.Count; i++)
        {
            HexagonMenu[i].gameObject.transform.localPosition = HexagonMenuPos[i];
        }

        SetMenu();
    }

    private void SetMenu()
    {
        if (TraningType == TRANING_TYPE.NONE && TraningStep == TRANING_POSE_STEP.NONE)
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
        else if(TraningType == TRANING_TYPE.POSE && TraningStep == TRANING_POSE_STEP.SELECT)
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
                    HexagonMenu[i].Init("임팩트", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickImpact);
                else if (i == 2)
                    HexagonMenu[i].Init("백스윙 탑", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickBackSwingTop);
                else if (i == 3)
                    HexagonMenu[i].Init("어드레스", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickAddress);
                else if (i == 4)
                    HexagonMenu[i].Init("", "", CommonFunc.HexToColor(HexagonMenuColor[i], 0.5f), null);
                else if (i == 5)
                    HexagonMenu[i].Init("튜토리얼\n영상", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickTutorial);
            }
        }
        else if (TraningType == TRANING_TYPE.POSE && TraningStep == TRANING_POSE_STEP.LEVEL)
        {
            CenterButton.Init("트레이닝\n시작", "", CommonFunc.HexToColor("#393939", 1f), OnClickTraningStart);

            TraningPoseLevel.Clear();
            TraningPoseLevelBtnIndex.Clear();

            for (int i = 0; i < HexagonMenu.Count; i++)
            {
                if (i == 0)
                {
                    HexagonMenu[i].Init("", "icon_back", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickBack);
                    HexagonMenu[i].SetImageSize(new Vector2(227.3f, 168.1f));
                }
                else if (i == 1)
                {
                    HexagonMenu[i].Init("SIDE", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickLevelSide);
                    TraningPoseLevel.Add(TRANING_POSE_LEVEL_TYPE.SIDE, 0);
                    TraningPoseLevelBtnIndex.Add(TRANING_POSE_LEVEL_TYPE.SIDE, i);
                }
                else if (i == 2)
                {
                    HexagonMenu[i].Init("TURN", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickLevelTurn);
                    TraningPoseLevel.Add(TRANING_POSE_LEVEL_TYPE.TURN, 0);
                    TraningPoseLevelBtnIndex.Add(TRANING_POSE_LEVEL_TYPE.TURN, i);
                }
                else if (i == 3)
                {
                    HexagonMenu[i].Init("BEND", "", CommonFunc.HexToColor(HexagonMenuColor[i], 1f), OnClickLevelBend);
                    TraningPoseLevel.Add(TRANING_POSE_LEVEL_TYPE.BEND, 0);
                    TraningPoseLevelBtnIndex.Add(TRANING_POSE_LEVEL_TYPE.BEND, i);
                }
                else if (i == 4)
                    HexagonMenu[i].Init("", "", CommonFunc.HexToColor(HexagonMenuColor[i], 0.5f), null);
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

        TraningType = TRANING_TYPE.POSE;
        TraningStep = TRANING_POSE_STEP.SELECT;

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
        TraningType = TRANING_TYPE.POSE;
        TraningStep = TRANING_POSE_STEP.LEVEL;
        TraningPoseType = TRANING_POSE_TYPE.IMPACT;

        StartMenuAction();
    }

    public void OnClickBackSwingTop()
    {
        if (MenuAction)
            return;

        TraningTypeText.text = "백스윙 탑";
        TraningType = TRANING_TYPE.POSE;
        TraningStep = TRANING_POSE_STEP.LEVEL;
        TraningPoseType = TRANING_POSE_TYPE.BACK_SWING_TOP;

        StartMenuAction();
    }

    public void OnClickAddress()
    {
        if (MenuAction)
            return;

        TraningTypeText.text = "어드레스";
        TraningType = TRANING_TYPE.POSE;
        TraningStep = TRANING_POSE_STEP.LEVEL;
        TraningPoseType = TRANING_POSE_TYPE.ADDRESS;

        StartMenuAction();
    }

    public void ChangeTraningPosLevel()
    {
        var enumerator = TraningPoseLevelBtnIndex.GetEnumerator();

        while(enumerator.MoveNext())
        {
            var type = enumerator.Current.Key;
            var btnIndex = enumerator.Current.Value;
            var level = TraningPoseLevel[type];
            if(level == 0)
                HexagonMenu[btnIndex].SetButtonColor(CommonFunc.HexToColor(HexagonMenuColor[btnIndex], 0.5f));
            else
                HexagonMenu[btnIndex].SetButtonColor(CommonFunc.HexToColor(HexagonMenuColor[btnIndex], 1f));
  
            switch (type)
            {
                case TRANING_POSE_LEVEL_TYPE.SIDE:
                    HexagonMenu[btnIndex].SetText(string.Format("SIDE{0}", TraningPoseLevelStr[level]));
                    break;
                case TRANING_POSE_LEVEL_TYPE.TURN:
                    HexagonMenu[btnIndex].SetText(string.Format("TURN{0}", TraningPoseLevelStr[level]));
                    break;
                case TRANING_POSE_LEVEL_TYPE.BEND:
                    HexagonMenu[btnIndex].SetText(string.Format("BEND{0}", TraningPoseLevelStr[level]));
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

        TraningPoseLevel[TRANING_POSE_LEVEL_TYPE.SIDE]++;

        if (TraningPoseLevelStr.Count <= TraningPoseLevel[TRANING_POSE_LEVEL_TYPE.SIDE])
            TraningPoseLevel[TRANING_POSE_LEVEL_TYPE.SIDE] = 0;

        ChangeTraningPosLevel();
    }

    public void OnClickLevelTurn()
    {
        if (MenuAction)
            return;

        TraningPoseLevel[TRANING_POSE_LEVEL_TYPE.TURN]++;

        if (TraningPoseLevelStr.Count <= TraningPoseLevel[TRANING_POSE_LEVEL_TYPE.TURN])
            TraningPoseLevel[TRANING_POSE_LEVEL_TYPE.TURN] = 0;

        ChangeTraningPosLevel();
    }

    public void OnClickLevelBend()
    {
        if (MenuAction)
            return;

        TraningPoseLevel[TRANING_POSE_LEVEL_TYPE.BEND]++;

        if (TraningPoseLevelStr.Count <= TraningPoseLevel[TRANING_POSE_LEVEL_TYPE.BEND])
            TraningPoseLevel[TRANING_POSE_LEVEL_TYPE.BEND] = 0;

        ChangeTraningPosLevel();
    }

    public void OnClickBack()
    {
        if (MenuAction)
            return;

        if (TraningType == TRANING_TYPE.POSE && TraningStep == TRANING_POSE_STEP.SELECT)
        {
            TraningType = TRANING_TYPE.NONE;
            TraningStep = TRANING_POSE_STEP.NONE;
            TraningPoseType = TRANING_POSE_TYPE.NONE;
        }
        else if(TraningType == TRANING_TYPE.POSE && TraningStep == TRANING_POSE_STEP.LEVEL)
        {
            TraningType = TRANING_TYPE.POSE;
            TraningStep = TRANING_POSE_STEP.SELECT;
            TraningPoseType = TRANING_POSE_TYPE.NONE;
        }

        StartMenuAction();
    }

    public void OnClickTraningStart()
    {
        if (MenuAction)
            return;

        TKManager.Instance.SetMode(CommonFunc.ConvertTrainingMode(TraningPoseType));
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }
}
