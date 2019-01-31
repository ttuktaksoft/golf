using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHexagonButton : MonoBehaviour {

    public Button HexagonButton;
    public Image HexagonButtonImg;
    public Image HexagonIcon;
    public Text HexagonText;

    public void Init(string str, string fileName, Color btnColor, UnityEngine.Events.UnityAction click)
    {
        HexagonIcon.gameObject.SetActive(false);
        HexagonText.gameObject.SetActive(false);
        HexagonButtonImg.color = new Color(0,0,0,1);

        SetText(str);
        SetImage(fileName);
        SetButtonColor(btnColor);
        SetButtonAction(click);
    }

    public void SetText(string str)
    {
        if (str == string.Empty)
            return;

        HexagonIcon.gameObject.SetActive(false);
        HexagonText.gameObject.SetActive(true);
        HexagonText.text = str;
    }

    private void SetImage(string fileName)
    {
        if (fileName == string.Empty)
            return;

        HexagonIcon.gameObject.SetActive(true);
        HexagonText.gameObject.SetActive(false);

        CommonFunc.SetImageFile(fileName, ref HexagonIcon);
    }

    public void SetImageSize(Vector2 size)
    {
        CommonFunc.SetImageSize(size, ref HexagonIcon);
    }

    public void SetButtonColor(Color color)
    {
        HexagonButtonImg.color = color;
    }

    public void SetButtonAction(UnityEngine.Events.UnityAction click)
    {
        HexagonButton.onClick.RemoveAllListeners();
        if(click != null)
            HexagonButton.onClick.AddListener(click);
    }
}
