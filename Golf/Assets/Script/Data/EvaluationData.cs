using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class EvaluationData
{
    public long Date;

    private CommonData.TRAINING_TYPE TrainingType = CommonData.TRAINING_TYPE.TRAINING_POSE;
    private int TrainingCount = 0;
    private CommonData.TRAINING_POSE PoseType = CommonData.TRAINING_POSE.TRAINING_ADDRESS;
    private Dictionary<CommonData.TRAINING_ANGLE, int> AngleTypeList = new Dictionary<CommonData.TRAINING_ANGLE, int>();

    public int StarCount { get; private set; }
    public string Msg { get; private set; }

    public EvaluationData(long date,
        CommonData.TRAINING_TYPE trainingType, 
        int trainingCount, 
        CommonData.TRAINING_POSE poseType, 
        Dictionary<CommonData.TRAINING_ANGLE, int> angleTypeList,
        int starCount,
        string msg)
    {
        Date = date;
        TrainingType = trainingType;
        TrainingCount = trainingCount;
        PoseType = poseType;
        AngleTypeList = angleTypeList;
        StarCount = starCount;
        Msg = msg;
    }

    public EvaluationData(long date,
        CommonData.TRAINING_TYPE trainingType,
        int trainingCount,
        int starCount,
        string msg)
    {
        Date = date;
        TrainingType = trainingType;
        TrainingCount = trainingCount;
        StarCount = starCount;
        Msg = msg;
    }

    public string GetDate()
    {
        DateTime time = new DateTime(Date);
        return string.Format("{0:D2}월 {1:D2}일", time.Month, time.Day);
    }

    public string GetTraining()
    {
        if (TrainingType == CommonData.TRAINING_TYPE.TRAINING_POSE)
        {
            return string.Format("{0} {1}Point", CommonFunc.ConvertPoseTypeStr(PoseType), TrainingCount * 3);
        }
        else
            return string.Format("{0} {1}회", CommonFunc.ConvertTrainingTypeStr(TrainingType, false), TrainingCount);
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


