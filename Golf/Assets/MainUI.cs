﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour {

    public enum MAIN_MENU_TYPE
    {
        NONE,
        TRANING,
        ALARM,
        USER_INFO,
    }

    public TraningUI TraningUIPage;

    public UITabButton TraningTab;
    public UITabButton AlarmTab;
    public UITabButton UserInfoTab;
    private MAIN_MENU_TYPE CurrType = MAIN_MENU_TYPE.NONE;

    private void Awake()
    {
        TraningTab.SetButtonAction(OnClickTraning);
        AlarmTab.SetButtonAction(OnClickAlarm);
        UserInfoTab.SetButtonAction(OnClickUserInfo);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickTraning()
    {
        ChangeTab(MAIN_MENU_TYPE.TRANING);
    }
    public void OnClickAlarm()
    {
        ChangeTab(MAIN_MENU_TYPE.ALARM);
    }
    public void OnClickUserInfo()
    {
        ChangeTab(MAIN_MENU_TYPE.USER_INFO);
    }

    private void ChangeTab(MAIN_MENU_TYPE type)
    {
        if (CurrType == type)
            return;

        CurrType = type;
        TraningTab.SetSelect(false);
        AlarmTab.SetSelect(false);
        UserInfoTab.SetSelect(false);

        TraningUIPage.gameObject.SetActive(false);

        switch (CurrType)
        {
            case MAIN_MENU_TYPE.TRANING:
                TraningTab.SetSelect(true);
                TraningUIPage.gameObject.SetActive(true);
                TraningUIPage.Init();
                break;
            case MAIN_MENU_TYPE.ALARM:
                AlarmTab.SetSelect(true);
                break;
            case MAIN_MENU_TYPE.USER_INFO:
                UserInfoTab.SetSelect(true);
                break;
            default:
                break;
        }


    }
}