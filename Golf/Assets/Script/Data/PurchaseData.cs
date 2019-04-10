using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseData
{
    public int Index = -1;
    public string ThumbnailURL = "";
    public string InfoURL = "";

    public PurchaseData(int index, string thumbnailURL)
    {
        Index = index;
        ThumbnailURL = thumbnailURL;
    }
}

