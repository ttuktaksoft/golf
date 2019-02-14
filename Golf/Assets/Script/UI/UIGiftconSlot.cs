using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGiftconSlot : MonoBehaviour
{
    public Button SlotButton;
    public Image GiftconImg;
    public Text Title;
    public Text Desc;

    private int GiftconIndex = -1;
    private Action RefreshUIAction = null;

    private void Awake()
    {
        SlotButton.onClick.AddListener(OnClickSlot);
    }

    public void SetData(GiftconData data)
    {
        CommonFunc.SetImageFile("Temp/test_1", ref GiftconImg);
        Title.text = data.Title;
        Desc.text = data.ExpirationTime;
        GiftconIndex = data.Index;
        RefreshUIAction = null;
    }

    public void SetRefreshUIAction(Action action)
    {
        RefreshUIAction = action;
    }

    public void OnClickSlot()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.GIFT_CON, new PopupGiftcon.PopupData(GiftconIndex, RefreshUIAction));
    }
}
