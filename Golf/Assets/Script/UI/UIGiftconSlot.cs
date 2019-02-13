using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGiftconSlot : MonoBehaviour
{
    public Button SlotButton;
    public Image GiftconImg;
    public Text Title;
    public Text Desc;

    private void Awake()
    {
        SlotButton.onClick.AddListener(OnClickSlot);
    }

    public void SetData(GiftconData data)
    {
        CommonFunc.SetImageFile("Temp/test_1", ref GiftconImg);
        Title.text = data.Title;
        Desc.text = data.ExpirationTime;
    }

    public void OnClickSlot()
    {

    }
}
