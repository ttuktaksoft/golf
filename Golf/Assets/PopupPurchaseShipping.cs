using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPurchaseShipping : Popup
{
    public Button OK;
    public Button Cencel;
    public InputField Name;
    public InputField PhoneNumber;
    public InputField Address;

    public PopupPurchaseShipping()
        : base(PopupMgr.POPUP_TYPE.PURCHASE_SHIPPING)
    {

    }

    public class PopupData : PopupBaseData
    {
        public PopupData()
        {
        }
    }

    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
        Cencel.onClick.AddListener(OnClickCencel);
    }

    public override void SetData(PopupBaseData data)
    {
        
    }

    

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public void OnClickOK()
    {
        PopupMgr.Instance.DismissPopup();
    }

    public void OnClickCencel()
    {
        PopupMgr.Instance.DismissPopup();
    }
}
