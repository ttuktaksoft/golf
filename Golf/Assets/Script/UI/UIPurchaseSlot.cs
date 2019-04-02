using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPurchaseSlot : MonoBehaviour
{
    public Button SlotButton;
    public Image PurchaseIconImg;

    private int PurchaseIndex = -1;
    private Action RefreshUIAction = null;

    private void Awake()
    {
        SlotButton.onClick.AddListener(OnClickSlot);
    }

    public void SetData()
    {
        //CommonFunc.SetImageFile("Temp_1/test_1", ref GiftconImg);
        //Title.text = data.Title;
        //Desc.text = data.ExpirationTime;
        
        RefreshUIAction = null;
    }

    public void SetRefreshUIAction(Action action)
    {
        RefreshUIAction = action;
    }

    public void OnClickSlot()
    {
        //PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.GIFT_CON, new PopupGiftcon.PopupData(GiftconIndex, RefreshUIAction));
    }
}
