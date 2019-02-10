using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmUI : MonoBehaviour {

    public GameObject ListObj;
    private List<AlarmSlotUI> AlarmSlotList = new List<AlarmSlotUI>();

    public void Init()
    {
        if(AlarmSlotList.Count <= 0)
        {
            for (int i = 0; i < CommonData.TEMP_ALARM_TITLE.Length; i++)
            {
                var slotObj = Instantiate(Resources.Load("Prefab/UIAlarmSlot"), ListObj.transform) as GameObject;
                var slot = slotObj.GetComponent<AlarmSlotUI>();
                slot.SetData(i);
                AlarmSlotList.Add(slot);
            }
        }
        else
        {
            for (int i = 0; i < AlarmSlotList.Count; i++)
            {
                AlarmSlotList[i].ResetSlot();
            }
        }
    }
}
