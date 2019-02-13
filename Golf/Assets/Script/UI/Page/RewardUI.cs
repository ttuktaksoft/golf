using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    public List<UIRewardPracticeGraph> PracticeList = new List<UIRewardPracticeGraph>();
    public GameObject ListObj;
    public List<UIGiftconSlot> GiftconSlotList = new List<UIGiftconSlot>();

    public void Init()
    {
        if (GiftconSlotList.Count <= 0)
        {
            for (int i = 0; i < DataManager.Instance.GiftconDataList.Count; i++)
            {
                var data = DataManager.Instance.GiftconDataList[i];
                var slotObj = Instantiate(Resources.Load("Prefab/UIGiftcon"), ListObj.transform) as GameObject;
                var slot = slotObj.GetComponent<UIGiftconSlot>();
                slot.SetData(data);
                GiftconSlotList.Add(slot);
            }
        }
        else
        {
            //for (int i = 0; i < AlarmSlotList.Count; i++)
            //{
            //    AlarmSlotList[i].ResetSlot();
            //}
        }
    }
}
