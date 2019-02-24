using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour {

    public UIMyInfo MyInfo;
    
    public GameObject EmptyText;
    public GameObject EvaluationList;
    private List<EvaluationSlotUI> EvaluationSlotList = new List<EvaluationSlotUI>();

    public List<UIFriendSlot> FriendSlotList = new List<UIFriendSlot>();

    public void Init()
    {
        for (int i = 0; i < EvaluationSlotList.Count; i++)
        {
            DestroyImmediate(EvaluationSlotList[i].gameObject);
        }
        EvaluationSlotList.Clear();

        if (DataManager.Instance.EvaluationDataList.Count > 0)
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
        for (int i = DataManager.Instance.EvaluationDataList.Count - 1; i >= 0; i--)
        {
            maxSlot--;
            var data = DataManager.Instance.EvaluationDataList[i];
            var slotObj = Instantiate(Resources.Load("Prefab/UIEvaluationSlotMini"), EvaluationList.transform) as GameObject;
            var slot = slotObj.GetComponent<EvaluationSlotUI>();
            slot.SetData(data);
            EvaluationSlotList.Add(slot);

            if (maxSlot == 0)
                break;
        }

        for (int i = 0; i < FriendSlotList.Count; i++)
        {
            FriendSlotList[i].Init(i);
        }

        MyInfo.Init(true);

    }
}
