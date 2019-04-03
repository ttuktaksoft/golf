using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmData
{
    public string Title;
    public DateTime Date;
    public string ContentURL;

    public string DateStr;

    public AlarmData(string title, DateTime date, string contentURL)
    {
        Title = title;
        Date = date;
        ContentURL = contentURL;

        DateStr = string.Format("{0:D2}-{1:D2}", date.Month, date.Day);

    }
}

