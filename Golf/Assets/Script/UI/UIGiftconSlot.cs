using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGiftconSlot : MonoBehaviour
{
    public Button SlotButton;
    public Image GiftconImg;

    private int GiftconIndex = -1;
    private Action RefreshUIAction = null;

    private void Awake()
    {
        SlotButton.onClick.AddListener(OnClickSlot);
    }

    public void SetData(GiftconData data)
    {
        GiftconIndex = data.Index;
        CommonFunc.SetImageFile(TextureCacheManager.Instance.GetTexture(data.ThumbnailURL), ref GiftconImg);
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
