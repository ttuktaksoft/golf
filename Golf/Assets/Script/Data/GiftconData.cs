using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftconData
{
    public int Index = -1;
    public string Title;
    public string ExpirationTime;

    public GiftconData(int index, string title, string expiration)
    {
        Index = index;
        Title = title;
        ExpirationTime = expiration;
        ExpirationTime = "유효기간 : 2019.03.02";
    }
}
