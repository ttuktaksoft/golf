using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTerms : Popup
{
    public Button OK;

    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public PopupTerms()
        : base(PopupMgr.POPUP_TYPE.TERMS)
    {

    }

    public override void SetData(PopupBaseData data)
    {
    }

    public void OnClickOK()
    {
        PopupMgr.Instance.DismissPopup();
    }
}
