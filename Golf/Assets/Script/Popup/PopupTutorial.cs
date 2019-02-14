using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTutorial : Popup
{
    public Button OK;
    public GameObject ListObj;
    private List<UITutorialSlot> TutorialSlotList = new List<UITutorialSlot>();


    public PopupTutorial()
        : base(PopupMgr.POPUP_TYPE.TUTORIAL)
    {

    }

    public class PopupData : PopupBaseData
    {
        public PopupData()
        {
        }
    }

    public override void SetData(PopupBaseData data)
    {
        if (TutorialSlotList.Count <= 0)
        {
            for (int i = 0; i < DataManager.Instance.TutorialDataList.Count; i++)
            {
                var temp_data = DataManager.Instance.TutorialDataList[i];
                var slotObj = Instantiate(Resources.Load("Prefab/UITutorialSlot"), ListObj.transform) as GameObject;
                var slot = slotObj.GetComponent<UITutorialSlot>();
                slot.SetData(temp_data);
                TutorialSlotList.Add(slot);
            }
        }
    }


    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public void OnClickOK()
    {
        PopupMgr.Instance.DismissPopup();
    }
}
