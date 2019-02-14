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

    public PopupMsg()
        : base(PopupMgr.POPUP_TYPE.MSG)
    {

    }

    public class PopupData : PopupBaseData
    {
        public string Msg;
        public Action OkAction = null;
        public Action CancelAction = null;

        public PopupData(string msg, Action okAction = null, Action cancelAction = null)
        {
            Msg = msg;
            OkAction = okAction;
            CancelAction = cancelAction;
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
        if (OkAction != null)
            OkAction();

        PopupMgr.Instance.DismissPopup();
    }

    public void OnClickCancel()
    {
        if (CancelAction != null)
            CancelAction();

        PopupMgr.Instance.DismissPopup();
    }
}
