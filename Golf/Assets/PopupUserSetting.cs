using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUserSetting : Popup
{
    public InputField Name;
    public InputField Number;
    public Button ManButton;
    public Button WomanButton;
    public Button OK;
    public Button Cancel;

    private Action OkAction = null;
    private CommonData.GENDER Gender = CommonData.GENDER.GENDER_MAN;

    public PopupUserSetting()
        : base(PopupMgr.POPUP_TYPE.USER_SETTING)
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

        Name.text = TKManager.Instance.Name;
        Number.text = TKManager.Instance.PhoneNumber;
        Gender = TKManager.Instance.GetGender();

        RefreahUI();
    }


    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
        Cancel.onClick.AddListener(OnClickCancel);

        ManButton.onClick.AddListener(OnClickMan);
        WomanButton.onClick.AddListener(OnClickWoman);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public void OnClickOK()
    {
        TKManager.Instance.SetName(Name.text.ToString());
        TKManager.Instance.SetPhoneNumber(Number.text.ToString());
        TKManager.Instance.SetGender(Gender);

        if (OkAction != null)
            OkAction();

        PopupMgr.Instance.DismissPopup();
    }

    public void OnClickCancel()
    {
        PopupMgr.Instance.DismissPopup();
    }

    public void RefreahUI()
    {
        if (Gender == CommonData.GENDER.GENDER_MAN)
        {
            ManButton.interactable = false;
            WomanButton.interactable = true;
        }
        else
        {
            ManButton.interactable = true;
            WomanButton.interactable = false;
        }
    }

    public void OnClickMan()
    {
        Gender = CommonData.GENDER.GENDER_MAN;
        RefreahUI();
    }

    public void OnClickWoman()
    {
        Gender = CommonData.GENDER.GENDER_WOMAN;
        RefreahUI();
    }

    
}
