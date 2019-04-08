using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour {

    public UIMyInfo MyInfo;
    
    public GameObject EmptyText;
    public GameObject EvaluationList;
    public Button FriendPlus;
    private List<EvaluationSlotUI> EvaluationSlotList = new List<EvaluationSlotUI>();

    public GameObject FriendEmptyText;
    public GameObject FriendList;
    private List<UIFriendSlot> FriendSlotList = new List<UIFriendSlot>();

    public void Awake()
    {
        FriendPlus.onClick.AddListener(OnClickFriendPlus);
    }

    public void Init()
    {
        for (int i = 0; i < EvaluationSlotList.Count; i++)
        {
            DestroyImmediate(EvaluationSlotList[i].gameObject);
        }
        EvaluationSlotList.Clear();

        if (TKManager.Instance.Mydata.EvaluationDataList.Count > 0)
        {
            EvaluationList.SetActive(true);
            EmptyText.SetActive(false);
        }
        else
        {
            EvaluationList.SetActive(false);
            EmptyText.SetActive(true);
        }

        int maxSlot = 4;
        for (int i = 0; i < TKManager.Instance.Mydata.EvaluationDataList.Count; i++)
        {
            maxSlot--;
            var data = TKManager.Instance.Mydata.EvaluationDataList[i];
            var slotObj = Instantiate(Resources.Load("Prefab/UIEvaluationSlotMini"), EvaluationList.transform) as GameObject;
            var slot = slotObj.GetComponent<EvaluationSlotUI>();
            slot.SetData(data);
            EvaluationSlotList.Add(slot);

            if (maxSlot == 0)
                break;
        }

        RefreshFriendList();

        MyInfo.Init(true);

    }

    public void OnClickFriendPlus()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.FRIEND_PLUS, new PopupFreindPlus.PopupData(RefreshFriendList));
    }

    public void RefreshFriendList()
    {
        var friendList = TKManager.Instance.Mydata.FriendDataList;
        for (int i = 0; i < FriendSlotList.Count; i++)
        {
            DestroyImmediate(FriendSlotList[i].gameObject);
        }
        FriendSlotList.Clear();

        if (friendList.Count > 0)
        {
            FriendList.SetActive(true);
            FriendEmptyText.SetActive(false);
        }
        else
        {
            FriendList.SetActive(false);
            FriendEmptyText.SetActive(true);
        }

        for (int i = 0; i < friendList.Count; i++)
        {
            var data = friendList[i];
            var slotObj = Instantiate(Resources.Load("Prefab/UIFriendSlot"), FriendList.transform) as GameObject;
            var slot = slotObj.GetComponent<UIFriendSlot>();
            slot.SetData(data);
            FriendSlotList.Add(slot);
        }
    }
}
