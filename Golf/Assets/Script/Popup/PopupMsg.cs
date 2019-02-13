using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMsg : Popup
{
    public Text Msg;
    public Button OK;
    public Button Cancel;

    public PopupMsg()
        : base(PopupMgr.POPUP_TYPE.MSG)
    {

    }

    public class PopupData : PopupBaseData
    {
        public string Msg;

        public PopupData(string msg)
        {
            Msg = msg;
        }
    }

    public override void SetData(PopupBaseData data)
    {
        var popupData = data as PopupData;

        Msg.text = popupData.Msg;
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
    }

    public void OnClickCancel()
    {
        PopupMgr.Instance.DismissPopup();
    }
}
