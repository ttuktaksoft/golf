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

    public List<EvaluationData> EvaluationDataList = new List<EvaluationData>();
    public List<GiftconData> GiftconDataList = new List<GiftconData>();

    // TODO 파베가 붙으면 변경해야함
    public string ThumbnailSpritePath = "";

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

    public void Init(CommonData.GENDER gender, string index, string name, string phoneNumber, int seasonPoint, int accumulatePoint)
    {
        Index = index;
        Gender = gender;
        Name = name;
        PhoneNumber = phoneNumber;
        SeasonPoint = seasonPoint;
        AccumulatePoint = accumulatePoint;

        RefreshGrade();
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
        RefreshGrade();
        FirebaseManager.Instance.SetSeasonPoint();
    }

    public void RefreshGrade()
    {
        var arr = CommonData.GRADE_POINT;
        Grade = arr.Length - 1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (AccumulatePoint < arr[i])
            {
                Grade = i;
                break;
            }
                
        }
    }
    

    public void AddEvaluationData(EvaluationData data)
    {
        EvaluationDataList.Insert(0, data);
        TKManager.Instance.SaveFile();
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

}

