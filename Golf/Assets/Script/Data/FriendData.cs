using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendData
{
    public bool DataLoad = false;
    public string Index = "";
    public string Nickname { get; private set; }
    public CommonData.GENDER Gender { get; private set; }
    public int Grade { get; private set; }
    public int AccumulatePoint { get; private set; }
    public int SeasonPoint { get; private set; }
    public string ThumbnailUrl { get; private set; }

    public FriendData(string index)
    {
        Index = index;
        DataLoad = false;
    }

    public void SetData(string nickname, CommonData.GENDER gender, int accumulatePoint, int seasonPoint, string thumbnailUrl)
    {
        Nickname = nickname;
        Gender = gender;
        AccumulatePoint = accumulatePoint;
        SeasonPoint = seasonPoint;
        ThumbnailUrl = thumbnailUrl;
        DataLoad = true;

        Grade = CommonFunc.RefreshGrade(AccumulatePoint);
    }
}
