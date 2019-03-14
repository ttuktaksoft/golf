using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTerms : Popup
{
    public Button AgreeButton_1;
    public GameObject AgreeCheckObj_1;
    private bool AgreeEnable_1 = false;
    public Button AgreeButton_2;
    public GameObject AgreeCheckObj_2;
    private bool AgreeEnable_2 = false;
    public Button OK;

    private Action OkAction = null;

    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
        AgreeButton_1.onClick.AddListener(OnClickAgree_1);
        AgreeButton_2.onClick.AddListener(OnClickAgree_2);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public PopupTerms()
        : base(PopupMgr.POPUP_TYPE.TERMS)
    {

    }

    public class PopupData : PopupBaseData
    {
        public Action OkAction = null;

        public PopupData(Action okAction = null)
        {
            OkAction = okAction;
        }
    }

    public override void SetData(PopupBaseData data)
    {
        var popupData = data as PopupData;
        if (popupData == null)
            return;

        OkAction = popupData.OkAction;

        AgreeEnable_1 = false;
        AgreeEnable_2 = false;

        RefreshUI();
    }

    public void OnClickOK()
    {
        if(AgreeEnable_1 == false)
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("트레이닝 포인트에 따른 각종 혜택을 받기 위해서 개인정보 취급동의가 필요 합니다.", null, null, PopupMsg.BUTTON_TYPE.ONE));
            return;
        }

        if (AgreeEnable_2 == false)
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("트레이닝 포인트에 따른 각종 혜택을 받기 위해서 마케팅동의가 필요 합니다.", null, null, PopupMsg.BUTTON_TYPE.ONE));
            return;
        }



        if (OkAction != null)
            OkAction();

        PopupMgr.Instance.DismissPopup();
    }

    public void OnClickAgree_1()
    {
        AgreeEnable_1 = !AgreeEnable_1;
        RefreshUI();
    }
    public void OnClickAgree_2()
    {
        AgreeEnable_2 = !AgreeEnable_2;
        RefreshUI();
    }

    public void RefreshUI()
    {
        AgreeCheckObj_1.gameObject.SetActive(AgreeEnable_1);
        AgreeCheckObj_2.gameObject.SetActive(AgreeEnable_2);
    }
}
