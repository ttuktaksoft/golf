using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmSlotUI : MonoBehaviour
{
    public Button SlotButton;
    public RectTransform SlotRect;

    public GameObject MiniSlotObj;
    public RectTransform MiniSlotRect;
    public Text MiniSlotTitle;
    public Text MiniSlotDate;

    public GameObject BigSlotObj;
    public RectTransform BigSlotRect;
    public Text BigSlotMsg;
    public Image BigSlotImg;

    public bool MiniMode = true;

    private int SlotIndex = 0;
    private Action<int> ClickAction;

    private void Awake()
    {
        SlotButton.onClick.AddListener(OnClickModeChange);
    }

    public void SetData(AlarmData data, int index, Action<int> clickAction)
    {
        MiniSlotTitle.text = data.Title;
        MiniSlotDate.text = data.DateStr;
        SlotIndex = index;
        ClickAction = clickAction;

        CommonFunc.SetImageFile(data.BodyImgStr, ref BigSlotImg);        

        RefreshSlot();
    }

    public void ResetSlot()
    {
        MiniMode = true;

        RefreshSlot();
    }

    public void OnClickModeChange()
    {
        MiniMode = !MiniMode;

        RefreshSlot();

        ClickAction(SlotIndex);
    }

    public void RefreshSlot()
    {
        BigSlotObj.gameObject.SetActive(!MiniMode);
        LayoutRebuilder.ForceRebuildLayoutImmediate(BigSlotRect);
        if (MiniMode)
        {
            SlotRect.sizeDelta = new Vector2(SlotRect.sizeDelta.x, MiniSlotRect.sizeDelta.y);
        }
        else
        {
            SlotRect.sizeDelta = new Vector2(SlotRect.sizeDelta.x, BigSlotRect.sizeDelta.y);
        }
    }
}
