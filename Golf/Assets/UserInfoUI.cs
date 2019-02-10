using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoUI : MonoBehaviour {

    public GameObject ListObj;
    private List<EvaluationSlotUI> EvaluationSlotList = new List<EvaluationSlotUI>();

    public void Init()
    {
        if (EvaluationSlotList.Count <= 0)
        {
            for (int i = 0; i < CommonData.TEMP_ALARM_TITLE.Length; i++)
            {
                var slotObj = Instantiate(Resources.Load("Prefab/UIEvaluationSlot"), ListObj.transform) as GameObject;
                var slot = slotObj.GetComponent<EvaluationSlotUI>();
                //slot.SetData(i);
                EvaluationSlotList.Add(slot);
            }
        }
        else
        {
            //for (int i = 0; i < EvaluationSlotList.Count; i++)
            //{
            //    EvaluationSlotList[i].ResetSlot();
            //}
        }
    }
}
