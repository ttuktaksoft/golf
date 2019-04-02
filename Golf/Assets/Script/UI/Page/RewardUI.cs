using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    public UIMyInfo MyInfo;

    public GameObject RewardSelectObj;
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

        RefreshGiftconList();

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

    public void OnClickRewardSelect()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.REWARD_SELECT);
    }
}
