using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    public UIMyInfo MyInfo;

    public GameObject RewardSelectObj;
    public Text RewardSelectDesc;
    public Button RewardSelectButton;

    public GameObject GiftconListObj;
    public GameObject GiftconEmpty;
    public GameObject ListObj;
    private List<UIGiftconSlot> GiftconSlotList = new List<UIGiftconSlot>();

    public GameObject PurchaseListObj;
    public GameObject PurchaseListEmpty;
    public GameObject PurchaseList;
    private List<UIPurchaseSlot> PurchaseSlotList = new List<UIPurchaseSlot>();

    public List<UIGradeInfoSlot> GradeInfoSlotList = new List<UIGradeInfoSlot>();

    public void Awake()
    {
        RewardSelectButton.onClick.AddListener(OnClickRewardSelect);
    }

    public void Init()
    {
        MyInfo.Init();

        var currTime = DateTime.Now;

        if(currTime.Month == TKManager.Instance.CurrSeasonTime.Month &&
            currTime.Day == TKManager.Instance.CurrSeasonTime.Day &&
            currTime.Year == TKManager.Instance.CurrSeasonTime.Year)
        {
            // 리워드를 받을 수있다.
            RewardSelectButton.gameObject.SetActive(true);
            RewardSelectDesc.gameObject.SetActive(false);
        }
        else
        {
            // 다음 시즌을 기달려야 한다.
            RewardSelectButton.gameObject.SetActive(false);
            RewardSelectDesc.gameObject.SetActive(true);
            RewardSelectDesc.text = string.Format("다음 시즌 {0}", TKManager.Instance.NextSeasonTime.ToString("MM월 dd일"));
        }

        RefreshGiftconList();
        RefreshPurchaseList();

        for (int i = 0; i < GradeInfoSlotList.Count; i++)
        {
            GradeInfoSlotList[i].SetGrade(i);
        }
    }

    public void RefreshGiftconList()
    {
        for (int i = 0; i < GiftconSlotList.Count; i++)
        {
            DestroyImmediate(GiftconSlotList[i].gameObject);
        }
        GiftconSlotList.Clear();

        GiftconListObj.SetActive(TKManager.Instance.Mydata.GiftconDataList.Count > 0);
        GiftconEmpty.SetActive(TKManager.Instance.Mydata.GiftconDataList.Count <= 0);

        for (int i = 0; i < TKManager.Instance.Mydata.GiftconDataList.Count; i++)
        {
            var data = TKManager.Instance.Mydata.GiftconDataList[i];
            var slotObj = Instantiate(Resources.Load("Prefab/UIGiftcon"), ListObj.transform) as GameObject;
            var slot = slotObj.GetComponent<UIGiftconSlot>();
            slot.SetData(data);
            slot.SetRefreshUIAction(RefreshGiftconList);
            GiftconSlotList.Add(slot);
        }
    }

    public void RefreshPurchaseList()
    {
        for (int i = 0; i < PurchaseSlotList.Count; i++)
        {
            DestroyImmediate(PurchaseSlotList[i].gameObject);
        }
        PurchaseSlotList.Clear();

        PurchaseListObj.SetActive(TKManager.Instance.Mydata.PurchaseDataList.Count > 0);
        PurchaseListEmpty.SetActive(TKManager.Instance.Mydata.PurchaseDataList.Count <= 0);

        for (int i = 0; i < TKManager.Instance.Mydata.PurchaseDataList.Count; i++)
        {
            var data = TKManager.Instance.Mydata.PurchaseDataList[i];
            var slotObj = Instantiate(Resources.Load("Prefab/UIPurchaseSlot"), ListObj.transform) as GameObject;
            var slot = slotObj.GetComponent<UIPurchaseSlot>();
            slot.SetData(data);
            slot.SetRefreshUIAction(RefreshPurchaseList);
            PurchaseSlotList.Add(slot);
        }
    }

    public void OnClickRewardSelect()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.REWARD_SELECT);
    }
}
