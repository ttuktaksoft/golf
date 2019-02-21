using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PracticeData
{
    public CommonData.TRAINING_TYPE TrainingType;
    public CommonData.TRAINING_POSE TrainingPoseType;
    public int PracticeCount;
    public int TodayWeek;
    public int TodayPracticeCount = 0;

    public PracticeData(CommonData.TRAINING_TYPE trainingType, CommonData.TRAINING_POSE poseType, int practiceCount)
    {
        TrainingType = trainingType;
        TrainingPoseType = poseType;
        PracticeCount = practiceCount;
    }

    public PracticeData(CommonData.TRAINING_TYPE trainingType, int practiceCount)
    {
        TrainingType = trainingType;
        PracticeCount = practiceCount;
    }
    // 임시
    public void AddPracticeCount()
    {
        TodayWeek = DateTime.Now.Day;
        TodayPracticeCount++;
        if(PracticeCount < CommonData.MAX_PRACTICE_COUNT)
            PracticeCount++;
    }
    public void RefreshPracticeCount()
    {
        if (TodayWeek != DateTime.Now.Day)
            TodayPracticeCount = 0;
    }
    public void ResetCount()
    {
        PracticeCount = 0;
    }
}