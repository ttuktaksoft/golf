using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUserLogin : Popup
{
    public Button OK;
    public Button Cancel;

    private Action OkAction = null;

    public InputField ID_Input;
    public InputField PW_Input;

    private CommonData.LOGIN_TYPE LoginType = CommonData.LOGIN_TYPE.ERROR;

    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
        Cancel.onClick.AddListener(OnClickCancel);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }


    public PopupUserLogin()
        : base(PopupMgr.POPUP_TYPE.LOGIN)
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
        ID_Input.text = "";
        PW_Input.text = "";

    }

    public void OnClickOK()
    {
        LoginUser();
    }

    public void OnClickCancel()
    {
        PopupMgr.Instance.DismissPopup();
    }

    public void LoginUser()
    {
        string id = ID_Input.text.ToString();
        string pw = PW_Input.text.ToString();

        if (string.IsNullOrWhiteSpace(id))
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("아이디를 입력해주세요", null, null, PopupMsg.BUTTON_TYPE.ONE));
            return;
        }

        if (string.IsNullOrWhiteSpace(pw))
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("비밀번호를 입력해주세요", null, null, PopupMsg.BUTTON_TYPE.ONE));
            return;
        }

        FirebaseManager.Instance.UserLogin(ID_Input.text.ToString(), PW_Input.text.ToString(), (login) =>
        {
            LoginType = login;
        }, LoginConplete);
    }

    public void LoginConplete()
    {
        if (LoginType == CommonData.LOGIN_TYPE.ERROR)
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("로그인 에러", null, null, PopupMsg.BUTTON_TYPE.ONE));
        }
        else if (LoginType == CommonData.LOGIN_TYPE.ID_FAIL)
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("없는 아이디 입니다", null, null, PopupMsg.BUTTON_TYPE.ONE));
        }
        else if (LoginType == CommonData.LOGIN_TYPE.PW_FAIL)
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("비밀번호가 맞지 않습니다", null, null, PopupMsg.BUTTON_TYPE.ONE));
        }
        else
        {
            TKManager.Instance.SaveFile();

            if (OkAction != null)
                OkAction();

            PopupMgr.Instance.DismissPopup(this.PopupType);
        }
    }
}
