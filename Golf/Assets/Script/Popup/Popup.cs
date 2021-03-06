﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Popup : MonoBehaviour
{
    public PopupMgr.POPUP_TYPE PopupType { get; private set; }

    public class PopupBaseData
    {
    }

    public Popup(PopupMgr.POPUP_TYPE type)
    {
        PopupType = type;
    }

    public abstract void SetData(PopupBaseData data);
}
