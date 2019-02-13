using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeData
{
    public CommonData.TRAINING_TYPE TrainingType;
    public CommonData.TRAINING_POSE TrainingPoseType;
    public int PracticeCount;
    public int TodayPracticeCount;

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

    public void SetTodayPracticeCount(int count)
    {
        TodayPracticeCount = count;
    }
}