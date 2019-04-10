using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public CommonData.GENDER Gender;
    public string Index { get; private set; }
    public string Name;    
    public int Grade { get; private set; }
    public string PhoneNumber { get; private set; }
    public int SeasonPoint { get; private set; }
    public int AccumulatePoint { get; private set; }
    public int Percent { get; private set; }
    public LocationInfo currentGPSPosition;
    public string UserCode = "";

    public List<EvaluationData> EvaluationDataList = new List<EvaluationData>();
    public List<GiftconData> GiftconDataList = new List<GiftconData>();
    public List<PurchaseData> PurchaseDataList = new List<PurchaseData>();

    public List<FriendData> FriendDataList = new List<FriendData>();

    public string ThumbnailSpriteURL = "";

    public UserData()
    {
        Index = "";
        Gender = CommonData.GENDER.GENDER_MAN;
        Name = "";
        PhoneNumber = "";
        SeasonPoint = 0;
        AccumulatePoint = 0;
        Grade = 0;
        Percent = 100;
    }
    public void Init(string index)
    {
        Index = index;
    }
    public void SetData(CommonData.GENDER gender, string name, string phoneNumber, int seasonPoint, int accumulatePoint, string ThumbNail)
    {
        Gender = gender;
        Name = name;
        PhoneNumber = phoneNumber;
        SeasonPoint = seasonPoint;
        AccumulatePoint = accumulatePoint;
        ThumbnailSpriteURL = ThumbNail;
        Grade = CommonFunc.RefreshGrade(AccumulatePoint);

        AddGiftcon(1, "http://cfs11.tistory.com/upload_control/download.blog?fhandle=YmxvZzM0NTUzOEBmczExLnRpc3RvcnkuY29tOi9hdHRhY2gvMS8zMzAwMDAwMDAxODcuanBn", "https://t1.daumcdn.net/cfile/tistory/995A4F395B9BD7C20E");
        TextureCacheManager.Instance.AddLoadImageURL("http://cfs11.tistory.com/upload_control/download.blog?fhandle=YmxvZzM0NTUzOEBmczExLnRpc3RvcnkuY29tOi9hdHRhY2gvMS8zMzAwMDAwMDAxODcuanBn");
        TextureCacheManager.Instance.AddLoadImageURL("https://t1.daumcdn.net/cfile/tistory/995A4F395B9BD7C20E");
    }

    public void SetIndex(string index)
    {
        Index = index;
    }

    public void SetGender(CommonData.GENDER gender)
    {
        Gender = gender;
      //  FirebaseManager.Instance.SetUserGender();
    }

    public void SetName(string name)
    {
        Name = name;
      //  FirebaseManager.Instance.SetUserName();
    }

    public void SetPhoneNumber(string num)
    {
        PhoneNumber = num;
      //  FirebaseManager.Instance.SetUserData();
    }

    public void AddSeasonPoint(int point)
    {
        SeasonPoint += point;
        AccumulatePoint += point;
        Grade = CommonFunc.RefreshGrade(AccumulatePoint);
        FirebaseManager.Instance.SetSeasonPoint();
        FirebaseManager.Instance.SetAccumPoint();
    }
    

    public void AddEvaluationData(EvaluationData data)
    {
        EvaluationDataList.Insert(0, data);
        TKManager.Instance.SaveFile();
    }

    public void AddGiftcon(int index, string thumbnailURL, string giftconURL)
    {
        for (int i = 0; i < GiftconDataList.Count; i++)
        {
            if (GiftconDataList[i].Index == index)
                return;
        }

        GiftconData data = new GiftconData(index, thumbnailURL, giftconURL);
        GiftconDataList.Add(data);
    }

    public void RemoveGiftcon(int index)
    {
        for (int i = 0; i < GiftconDataList.Count; i++)
        {
            if (GiftconDataList[i].Index == index)
            {
                GiftconDataList.RemoveAt(i);
                break;
            }
        }
    }

    public GiftconData GetGiftconData(int index)
    {
        for (int i = 0; i < GiftconDataList.Count; i++)
        {
            if (GiftconDataList[i].Index == index)
                return GiftconDataList[i];
        }

        return null;
    }

    public void AddFriend(string index)
    {
        for (int i = 0; i < FriendDataList.Count; i++)
        {
            if (FriendDataList[i].Index == index)
                return;
        }

        FriendDataList.Add(new FriendData(index));
    }

    public void AddFriend(string index, string nickname, CommonData.GENDER gender, int accumulatePoint, int seasonPoint, string thumbnailUrl)
    {
        for (int i = 0; i < FriendDataList.Count; i++)
        {
            if (FriendDataList[i].Index == index)
            {
                FriendDataList[i].SetData(nickname, gender, accumulatePoint, seasonPoint, thumbnailUrl);
                return;
            }
        }
    }

    public PurchaseData GetPurchaseData(int index)
    {
        for (int i = 0; i < PurchaseDataList.Count; i++)
        {
            if (PurchaseDataList[i].Index == index)
                return PurchaseDataList[i];
        }

        return null;
    }
}

