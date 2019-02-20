using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmUI : MonoBehaviour {

    public GameObject EmptyText;
    public GameObject AlarmList;
    public GameObject ListObj;
    private List<AlarmSlotUI> AlarmSlotList = new List<AlarmSlotUI>();

    public void Init()
    {
        if(DataManager.Instance.AlarmDataList.Count > 0)
        {
            AlarmList.SetActive(true);
            EmptyText.SetActive(false);
        }
        else
        {
            AlarmList.SetActive(false);
            EmptyText.SetActive(true);
        }
        if (AlarmSlotList.Count <= 0)
        {
            for (int i = 0; i < DataManager.Instance.AlarmDataList.Count; i++)
            {
                var data = DataManager.Instance.AlarmDataList[i];
                var slotObj = Instantiate(Resources.Load("Prefab/UIAlarmSlot"), ListObj.transform) as GameObject;
                var slot = slotObj.GetComponent<AlarmSlotUI>();
                slot.SetData(data);
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
