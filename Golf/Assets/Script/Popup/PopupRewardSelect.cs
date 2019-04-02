using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupRewardSelect : Popup
{
    public Button LaterButton;
    public Button RewardSelectButton;
    public GameObject ListObj;
    private List<UIRewardSelectSlot> RewardSelectSlotList = new List<UIRewardSelectSlot>();


    public PopupRewardSelect()
        : base(PopupMgr.POPUP_TYPE.REWARD_SELECT)
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
        //if (TutorialSlotList.Count <= 0)
        //{
        //    for (int i = 0; i < DataManager.Instance.TutorialDataList.Count; i++)
        //    {
        //        var temp_data = DataManager.Instance.TutorialDataList[i];
        //        var slotObj = Instantiate(Resources.Load("Prefab/UITutorialSlot"), ListObj.transform) as GameObject;
        //        var slot = slotObj.GetComponent<UITutorialSlot>();
        //        slot.SetData(temp_data);
        //        TutorialSlotList.Add(slot);
        //    }
        //}
    }


    private void Awake()
    {
        LaterButton.onClick.AddListener(OnClickLater);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public void OnClickLater()
    {
        PopupMgr.Instance.DismissPopup();
    }
}
