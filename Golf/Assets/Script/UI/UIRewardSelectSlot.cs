using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardSelectSlot : MonoBehaviour
{
    public Image Icon;
    public Text Title;
    public Text Desc;
    public Button SlotButton;
    public GameObject CheckObj;

    public SeasonRewardData Data;
    private Action<int, CommonData.SEASON_REWARD_TYPE> RefreshUI;
    private bool Select = false;

    public void Awake()
    {
        SlotButton.onClick.AddListener(OnClickSelect);
    }

    public void SetData(SeasonRewardData data, Action<int, CommonData.SEASON_REWARD_TYPE> refreshUI)
    {
        Data = data;
        RefreshUI = refreshUI;
        Select = false;
        CommonFunc.SetImageFile(TextureCacheManager.Instance.GetTexture(data.ThumbnailURL), ref Icon);
        Title.text = data.Name;
        Desc.text = data.Desc;

        SlotSelect(false);
    }

    public void OnClickSelect()
    {
        SlotSelect(true);
        RefreshUI(Data.Index, Data.SeasonRewardType);
    }

    public void SlotSelect(bool enable)
    {
        Select = enable;
        CheckObj.gameObject.SetActive(Select);
    }
}
