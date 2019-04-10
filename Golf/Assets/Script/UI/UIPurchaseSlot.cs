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

    public void SetData(PurchaseData data)
    {
        PurchaseIndex = data.Index;
        CommonFunc.SetImageFile(TextureCacheManager.Instance.GetTexture(data.ThumbnailURL), ref PurchaseIconImg);
        RefreshUIAction = null;
    }

    public void SetRefreshUIAction(Action action)
    {
        RefreshUIAction = action;
    }

    public void OnClickSlot()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.PURCHASE, new PopupPurchase.PopupData(PurchaseIndex));
    }
}
