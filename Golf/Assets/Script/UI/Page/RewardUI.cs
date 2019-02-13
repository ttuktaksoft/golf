using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    public List<UIRewardPracticeGraph> PracticeList = new List<UIRewardPracticeGraph>();

    public GameObject GiftconListObj;
    public GameObject GiftconEmpty;
    public GameObject ListObj;
    private List<UIGiftconSlot> GiftconSlotList = new List<UIGiftconSlot>();



    public void Init()
    {
        for (int i = 0; i < GiftconSlotList.Count; i++)
        {
            DestroyImmediate(GiftconSlotList[i].gameObject);
        }
        GiftconSlotList.Clear();

        GiftconListObj.SetActive(DataManager.Instance.GiftconDataList.Count > 0);
        GiftconEmpty.SetActive(DataManager.Instance.GiftconDataList.Count <= 0);

        for (int i = 0; i < DataManager.Instance.GiftconDataList.Count; i++)
        {
            var data = DataManager.Instance.GiftconDataList[i];
            var slotObj = Instantiate(Resources.Load("Prefab/UIGiftcon"), ListObj.transform) as GameObject;
            var slot = slotObj.GetComponent<UIGiftconSlot>();
            slot.SetData(data);
            GiftconSlotList.Add(slot);
        }

        for (int i = 0; i < DataManager.Instance.PracticeDataList.Count; i++)
        {
            if (PracticeList.Count <= i)
                break;
            var data = DataManager.Instance.PracticeDataList[i];
            PracticeList[i].SetData(data);
        }
    }
}
