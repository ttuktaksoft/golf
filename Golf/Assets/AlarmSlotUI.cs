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

    public void SetData(int index)
    {
        MiniSlotTitle.text = CommonData.TEMP_ALARM_TITLE[index];
        MiniSlotDate.text = "01-30";

        BigSlotMsg.text = CommonData.TEMP_ALARM_MSG[index];

        MiniSlotObj.gameObject.SetActive(MiniMode);
        BigSlotObj.gameObject.SetActive(!MiniMode);
        StartCoroutine(Co_Test());
    }

    public void ResetSlot()
    {
        MiniMode = true;

        MiniSlotObj.gameObject.SetActive(MiniMode);
        BigSlotObj.gameObject.SetActive(!MiniMode);
        StartCoroutine(Co_Test());
    }

    public void OnClickModeChange()
    {
        MiniMode = !MiniMode;

        MiniSlotObj.gameObject.SetActive(MiniMode);
        BigSlotObj.gameObject.SetActive(!MiniMode);
        StartCoroutine(Co_Test());
    }

    public IEnumerator Co_Test()
    {

        yield return null;
        //yield return null;
        //yield return null;
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
