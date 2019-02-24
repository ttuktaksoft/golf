using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmData
{
    public string Title;
    public long Date;
    public string BodyImgStr;

    public string DateStr;

    public AlarmData(string title, long date, string bodyimg)
    {
        Title = title;
        Date = date;
        BodyImgStr = bodyimg;

        DateTime time = new DateTime(date);
        DateStr = string.Format("{0:D2}-{1:D2}", time.Month, time.Day);

    }
}

