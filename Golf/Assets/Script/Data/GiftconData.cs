﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftconData
{
    public int Index = -1;
    public string ThumbnailURL = "";
    public string GiftconURL = "";

    public GiftconData(int index, string thumbnailURL, string giftconURL)
    {
        Index = index;
        ThumbnailURL = thumbnailURL;
        GiftconURL = giftconURL;
    }
}
