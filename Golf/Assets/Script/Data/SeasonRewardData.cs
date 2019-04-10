using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonRewardData
{
    public int Index = 0;
    public string Name;
    public string Desc;
    public string ThumbnailURL;
    public CommonData.SEASON_REWARD_TYPE SeasonRewardType = CommonData.SEASON_REWARD_TYPE.GIFTCON;


    public SeasonRewardData(int index, string name, string desc, string thumbnailURL, CommonData.SEASON_REWARD_TYPE type)
    {
        Index = index;
        Name = name;
        Desc = desc;
        ThumbnailURL = thumbnailURL;
        SeasonRewardType = type;
    }
}


