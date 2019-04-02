using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorialSlot : MonoBehaviour
{
    public Text Title;
    public Button SlotButton;
    private TutorialData Data;

    public void Awake()
    {
        SlotButton.onClick.AddListener(OnClickButton);
    }

    public void SetData(TutorialData data)
    {
        Title.text = data.Title;
        Data = data;
    }

    public void OnClickButton()
    {
        Application.OpenURL(Data.Url);
    }
}
