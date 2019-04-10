using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGiftcon : Popup
{
    public Button OK;
    public Button Del;
    public Image Img;

    private int GiftconIndex = -1;
    private Action RefreshUIAction = null;
    private GiftconData Giftcondata = null;

    public PopupGiftcon()
        : base(PopupMgr.POPUP_TYPE.GIFT_CON)
    {

    }

    public class PopupData : PopupBaseData
    {
        public int GiftconIndex = -1;
        public Action RefreshUIAction = null;

        public PopupData(int index, Action refreshUIAction)
        {
            GiftconIndex = index;
            RefreshUIAction = refreshUIAction;
        }
    }

    public override void SetData(PopupBaseData data)
    {
        var popupData = data as PopupData;
        if (popupData == null)
            return;

        GiftconIndex = popupData.GiftconIndex;
        Giftcondata = TKManager.Instance.Mydata.GetGiftconData(GiftconIndex);
        RefreshUIAction = popupData.RefreshUIAction;

        if (Giftcondata == null)
            OnClickOK();

        CommonFunc.SetImageFile(TextureCacheManager.Instance.GetTexture(Giftcondata.GiftconURL), ref Img);
    }

    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
        Del.onClick.AddListener(OnClickDel);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public void OnClickOK()
    {
        PopupMgr.Instance.DismissPopup();
    }

    public void OnClickDel()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("기프티콘을 삭제 하시겠습니까?", () =>
        {
            TKManager.Instance.Mydata.RemoveGiftcon(GiftconIndex);

            if (RefreshUIAction != null)
                RefreshUIAction();

            PopupMgr.Instance.DismissPopup();
        }));
    }
}
