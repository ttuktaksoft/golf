using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMsg : Popup
{
    public Text Msg;
    public Button OK;
    public Button Cancel;

    private Action OkAction = null;
    private Action CancelAction = null;

    private BUTTON_TYPE ButtonType = BUTTON_TYPE.TWO;

    public enum BUTTON_TYPE
    {
        ONE,
        TWO,
    }


    public PopupMsg()
        : base(PopupMgr.POPUP_TYPE.MSG)
    {

    }

    public class PopupData : PopupBaseData
    {
        public string Msg;
        public Action OkAction = null;
        public Action CancelAction = null;
        public BUTTON_TYPE ButtonType = BUTTON_TYPE.TWO;

        public PopupData(string msg, Action okAction = null, Action cancelAction = null, BUTTON_TYPE type = BUTTON_TYPE.TWO)
        {
            Msg = msg;
            OkAction = okAction;
            CancelAction = cancelAction;
            ButtonType = type;
        }
    }

    public override void SetData(PopupBaseData data)
    {
        var popupData = data as PopupData;
        if (popupData == null)
            return;

        Msg.text = popupData.Msg;
        OkAction = popupData.OkAction;
        CancelAction = popupData.CancelAction;
        ButtonType = popupData.ButtonType;

        switch (ButtonType)
        {
            case BUTTON_TYPE.ONE:
                OK.gameObject.SetActive(true);
                Cancel.gameObject.SetActive(false);
                break;
            case BUTTON_TYPE.TWO:
                OK.gameObject.SetActive(true);
                Cancel.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }


    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
        Cancel.onClick.AddListener(OnClickCancel);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public void OnClickOK()
    {
        PopupMgr.Instance.DismissPopup();

        if (OkAction != null)
            OkAction();
    }

    public void OnClickCancel()
    {
        PopupMgr.Instance.DismissPopup();

        if (CancelAction != null)
            CancelAction();
    }
}
