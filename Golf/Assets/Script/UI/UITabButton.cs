using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITabButton : MonoBehaviour
{
    public Button TabButton;
    //public Image TabIcon;
    //public Text TabText;

    public void SetSelect(bool select)
    {
        TabButton.interactable = !select;
        //if (select)
        //{
        //    TabButton.interactable = false;
        //    Color selectColor = new Color(0, 0, 0, 1f);
        //    TabIcon.color = selectColor;
        //    TabText.color = selectColor;
        //}
        //else
        //{
        //    TabButton.interactable = true;
        //    Color selectColor = new Color(0, 0, 0, 0.5f);
        //    TabIcon.color = selectColor;
        //    TabText.color = selectColor;
        //}
    }

    public void SetButtonAction(UnityAction click)
    {
        TabButton.onClick.AddListener(click);
    }
}
