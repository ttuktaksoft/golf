using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

[System.Serializable]
public class EvaluationData
{
    public string TKDate;
    public DateTime Time;

    public CommonData.TRAINING_TYPE TrainingType = CommonData.TRAINING_TYPE.TRAINING_POSE;
    public int TrainingCount = 0;
    public CommonData.TRAINING_POSE PoseType = CommonData.TRAINING_POSE.TRAINING_ADDRESS;
    public Dictionary<CommonData.TRAINING_ANGLE, int> AngleTypeList = new Dictionary<CommonData.TRAINING_ANGLE, int>();
    public Dictionary<string, int> AngleTypeList_Str = new Dictionary<string, int>();

    public int StarCount { get; private set; }
    public string Msg { get; private set; }

    public EvaluationData(Dictionary<string, object> dic)
    {
        TrainingType = CommonFunc.ToEnum<CommonData.TRAINING_TYPE>(dic["TrainingType"].ToString());

        long tkTime = Convert.ToInt64(dic["TKDate"].ToString());
        Time = DateTime.MinValue;
        if (tkTime <= 0)
            Time = DateTime.MinValue;
        else
        {
            try
            {
                Time = DateTime.ParseExact(tkTime.ToString(), "yyyyMMddHHmmssffff", null);
                CultureInfo cultures = CultureInfo.CreateSpecificCulture("ko-KR");
                TKDate = Time.ToString("yyyyMMddHHmmssffff", cultures);
            }
            catch
            {
                Time = DateTime.MinValue;
                CultureInfo cultures = CultureInfo.CreateSpecificCulture("ko-KR");
                TKDate = Time.ToString("yyyyMMddHHmmssffff", cultures);
            }
        }

        TrainingCount = Convert.ToInt32(dic["TrainingCount"].ToString());
        StarCount = Convert.ToInt32(dic["StarCount"].ToString());
        if(dic.ContainsKey("Msg"))
            Msg = dic["Msg"].ToString();

        if (TrainingType == CommonData.TRAINING_TYPE.TRAINING_POSE)
        {
            PoseType = CommonFunc.ToEnum<CommonData.TRAINING_POSE>(dic["PoseType"].ToString());
            var dic_angle = dic["AngleTypeList"] as Dictionary<string, object>;
            var e = dic_angle.GetEnumerator();
            while (e.MoveNext())
            {
                var k = CommonFunc.ToEnum<CommonData.TRAINING_ANGLE>(e.Current.Key);
                var v = Convert.ToInt32(e.Current.Value);
                AngleTypeList.Add(k, v);
            }

            var e_1 = AngleTypeList.GetEnumerator();
            while (e_1.MoveNext())
            {
                AngleTypeList_Str.Add(e_1.Current.Key.ToString(), e_1.Current.Value);
            }
        }
    }

    public EvaluationData(long date,
        CommonData.TRAINING_TYPE trainingType, 
        int trainingCount, 
        CommonData.TRAINING_POSE poseType, 
        Dictionary<CommonData.TRAINING_ANGLE, int> angleTypeList,
        int starCount,
        string msg)
    {
        Time = new DateTime(date);
        CultureInfo cultures = CultureInfo.CreateSpecificCulture("ko-KR");
        TKDate = Time.ToString("yyyyMMddHHmmssffff", cultures);
        TrainingType = trainingType;
        TrainingCount = trainingCount;
        PoseType = poseType;
        AngleTypeList = angleTypeList;
        var e = AngleTypeList.GetEnumerator();
        while (e.MoveNext())
        {
            AngleTypeList_Str.Add(e.Current.Key.ToString(), e.Current.Value);
        }
        StarCount = starCount;
        Msg = msg;
    }

    public EvaluationData(long date,
        CommonData.TRAINING_TYPE trainingType,
        int trainingCount,
        int starCount,
        string msg)
    {
        Time = new DateTime(date);
        CultureInfo cultures = CultureInfo.CreateSpecificCulture("ko-KR");
        TKDate = Time.ToString("yyyyMMddHHmmssffff", cultures);
        TrainingType = trainingType;
        TrainingCount = trainingCount;
        StarCount = starCount;
        Msg = msg;
    }

    public string GetDate()
    {
        return string.Format("{0:D2}월 {1:D2}일", Time.Month, Time.Day);
    }

    public string GetTraining()
    {
        if (TrainingType == CommonData.TRAINING_TYPE.TRAINING_POSE)
        {
            return string.Format("{0} {1}Point", CommonFunc.ConvertPoseTypeEngStr(PoseType), TrainingCount * 3);
        }
        else
            return string.Format("{0} {1}회", CommonFunc.ConvertTrainingTypeEngStr(TrainingType, false), TrainingCount);
    }

    public string GetAngle()
    {
        StringBuilder builder = new StringBuilder();
        var enumerator = AngleTypeList.GetEnumerator();
        int count = AngleTypeList.Count;

        while (enumerator.MoveNext())
        {
            count--;
            if (enumerator.Current.Value == 0)
                continue;
            builder.Append(string.Format("{0} {1}", CommonFunc.ConvertPoseAngleTypeStr(enumerator.Current.Key), CommonFunc.ConvertPoseLevelStr(enumerator.Current.Value)));
            if (count > 0)
                builder.Append(",");
        }

        return builder.ToString();
    }
}


