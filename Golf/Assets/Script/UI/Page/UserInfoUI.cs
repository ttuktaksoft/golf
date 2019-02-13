using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoUI : MonoBehaviour {

    public string Name;
    public string Grade;
    public GameObject ListObj;
    private List<EvaluationSlotUI> EvaluationSlotList = new List<EvaluationSlotUI>();

    public void Init()
    {
        if (EvaluationSlotList.Count <= 0)
        {
            for (int i = 0; i < DataManager.Instance.EvaluationDataList.Count; i++)
            {
                var data = DataManager.Instance.EvaluationDataList[i];
                var slotObj = Instantiate(Resources.Load("Prefab/UIEvaluationSlot"), ListObj.transform) as GameObject;
                var slot = slotObj.GetComponent<EvaluationSlotUI>();
                slot.SetData(data);
                EvaluationSlotList.Add(slot);
            }
        }
        else
        {
        }
    }
}
