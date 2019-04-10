using System;
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

    private void Awake()
    {
        RewardSelectButton.onClick.AddListener(OnRewardSelect);
        LaterButton.onClick.AddListener(OnClickLater);
    }

    public override void SetData(PopupBaseData data)
    {
        var currTime = DateTime.Now;

        if (currTime.Month != TKManager.Instance.CurrSeasonTime.Month ||
            currTime.Day != TKManager.Instance.CurrSeasonTime.Day ||
            currTime.Year != TKManager.Instance.CurrSeasonTime.Year)
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("시즌 보상을 받을 수 없습니다."));
            PopupMgr.Instance.DismissPopup();
        }

        if (RewardSelectSlotList.Count <= 0)
        {
            for (int i = 0; i < DataManager.Instance.SeasonRewardDataList.Count; i++)
            {
                var temp_data = DataManager.Instance.SeasonRewardDataList[i];
                var slotObj = Instantiate(Resources.Load("Prefab/UIRewardSelectSlot"), ListObj.transform) as GameObject;
                var slot = slotObj.GetComponent<UIRewardSelectSlot>();
                slot.SetData(temp_data, RefreshSlotUI);
                RewardSelectSlotList.Add(slot);
            }
        }
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public void OnClickLater()
    {
        PopupMgr.Instance.DismissPopup();
    }

    public void OnRewardSelect()
    {
        // 리워드 보상 ㄱㄱ
        PopupMgr.Instance.DismissPopup();
    }

    public void RefreshSlotUI(int index, CommonData.SEASON_REWARD_TYPE type)
    {
        for (int i = 0; i < RewardSelectSlotList.Count; i++)
        {
            if(RewardSelectSlotList[i].Data.Index == index &&
                RewardSelectSlotList[i].Data.SeasonRewardType == type)
            {
                RewardSelectSlotList[i].SlotSelect(true);
            }
            else
                RewardSelectSlotList[i].SlotSelect(false);
        }
    }
}
