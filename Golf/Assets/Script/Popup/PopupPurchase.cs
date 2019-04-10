using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPurchase : Popup
{
    public Button Buy;
    public Button Cancel;
    public Image Img;

    private int PurchaseIndex = -1;
    private PurchaseData Purchasedata = null;

    public PopupPurchase()
        : base(PopupMgr.POPUP_TYPE.PURCHASE)
    {

    }

    public class PopupData : PopupBaseData
    {
        public int PurchaseIndex = -1;

        public PopupData(int index)
        {
            PurchaseIndex = index;
        }
    }

    public override void SetData(PopupBaseData data)
    {
        var popupData = data as PopupData;
        if (popupData == null)
            return;

        PurchaseIndex = popupData.PurchaseIndex;
        Purchasedata = TKManager.Instance.Mydata.GetPurchaseData(PurchaseIndex);

        if (Purchasedata == null)
            OnClickCancel();

        CommonFunc.SetImageFile(TextureCacheManager.Instance.GetTexture(Purchasedata.InfoURL), ref Img);
    }

    private void Awake()
    {
        Buy.onClick.AddListener(OnClickBuy);
        Cancel.onClick.AddListener(OnClickCancel);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public void OnClickBuy()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("구매하시겠습니까?", () =>
        {
            // 카카오 페이 연동
            // 구매 성공 -> 배송지 입력 -> 배송지 파베로 전달
        }));
        PopupMgr.Instance.DismissPopup();
    }

    public void OnClickCancel()
    {
        PopupMgr.Instance.DismissPopup();
    }
}
