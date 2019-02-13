using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftconData
{
    public string Title;
    public string ExpirationTime;

    public GiftconData(string title, string expiration)
    {
        Title = title;
        ExpirationTime = expiration;
        ExpirationTime = "유효기간 : 2019.03.02";
    }
}
