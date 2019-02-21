using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {

    public enum MAIN_MENU_TYPE
    {
        NONE,
        TRANING,
        ALARM,
        REWARD,
        USER_INFO,
    }
    public Button TopWebButton;
    public MainTraningUI TraningUIPage;
    public AlarmUI AlarmUIPage;
    public RewardUI RewardUIPage;
    public UserInfoUI UserInfoUIPage;

    public UITabButton TraningTab;
    public UITabButton AlarmTab;
    public UITabButton RewardTab;
    public UITabButton UserInfoTab;
    private MAIN_MENU_TYPE CurrType = MAIN_MENU_TYPE.NONE;

    private void Awake()
    {
        TopWebButton.onClick.AddListener(OnClickWeb);
        TraningTab.SetButtonAction(OnClickTraning);
        AlarmTab.SetButtonAction(OnClickAlarm);
        RewardTab.SetButtonAction(OnClickReward);
        UserInfoTab.SetButtonAction(OnClickUserInfo);
    }

    // Use this for initialization
    void Start () {
        OnClickTraning();
    }
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR || UNITY_ANDROID
        if (PopupMgr.Instance.IsShowPopup(PopupMgr.POPUP_TYPE.MSG) == false && Input.GetKeyUp(KeyCode.Escape))
        {
            switch (CurrType)
            {
                case MAIN_MENU_TYPE.TRANING:
                    if(TraningUIPage.MenuAction == false && TraningUIPage.IsQuitApp())
                    {
                        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("종료 하시겠습니까?", () =>
                        {
                            Application.Quit();
                        }));
                    }
                    break;
                case MAIN_MENU_TYPE.ALARM:
                    OnClickTraning();
                    break;
                case MAIN_MENU_TYPE.REWARD:
                    OnClickAlarm();
                    break;
                case MAIN_MENU_TYPE.USER_INFO:
                    OnClickReward();
                    break;
                default:
                    break;
            }
            
        }
#endif
    }

    public void OnClickTraning()
    {
        ChangeTab(MAIN_MENU_TYPE.TRANING);
    }
    public void OnClickAlarm()
    {
        //PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.WEB_VIEW);
//        Application.OpenURL("http://unity3d.com/");
        ChangeTab(MAIN_MENU_TYPE.ALARM);
    }
    public void OnClickReward()
    {
        ChangeTab(MAIN_MENU_TYPE.REWARD);
    }
    public void OnClickUserInfo()
    {
        ChangeTab(MAIN_MENU_TYPE.USER_INFO);
    }

    private void ChangeTab(MAIN_MENU_TYPE type)
    {
        if (TraningUIPage.MenuAction || CurrType == type)
            return;

        CurrType = type;
        TraningTab.SetSelect(false);
        AlarmTab.SetSelect(false);
        RewardTab.SetSelect(false);
        UserInfoTab.SetSelect(false);

        TraningUIPage.gameObject.SetActive(false);
        AlarmUIPage.gameObject.SetActive(false);
        RewardUIPage.gameObject.SetActive(false);
        UserInfoUIPage.gameObject.SetActive(false);

        switch (CurrType)
        {
            case MAIN_MENU_TYPE.TRANING:
                TraningTab.SetSelect(true);
                TraningUIPage.gameObject.SetActive(true);
                TraningUIPage.Init(this);
                break;
            case MAIN_MENU_TYPE.ALARM:
                AlarmTab.SetSelect(true);
                AlarmUIPage.gameObject.SetActive(true);
                AlarmUIPage.Init();
                break;
            case MAIN_MENU_TYPE.REWARD:
                RewardTab.SetSelect(true);
                RewardUIPage.gameObject.SetActive(true);
                RewardUIPage.Init();
                break;
            case MAIN_MENU_TYPE.USER_INFO:
                UserInfoTab.SetSelect(true);
                UserInfoUIPage.gameObject.SetActive(true);
                UserInfoUIPage.Init();
                break;
            default:
                break;
        }


    }

    public void OnClickWeb()
    {
        Application.OpenURL("http://mygolfbiz.kr/main");
    }
}
