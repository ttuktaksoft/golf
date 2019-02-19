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

    private bool MiniMode = true;

    private void Awake()
    {
        SlotButton.onClick.AddListener(OnClickModeChange);
    }

    public void SetData(AlarmData data)
    {
        MiniSlotTitle.text = data.Title;
        MiniSlotDate.text = data.DateStr;

        BigSlotMsg.text = data.BodyStr;

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
