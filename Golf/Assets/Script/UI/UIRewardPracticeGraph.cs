using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardPracticeGraph : MonoBehaviour
{
    public Text Title;
    public Text Desc;
    public Text Today;
    public Slider Slide;
    
    public void SetData(PracticeData data)
    {
        if(data.TrainingType == CommonData.TRAINING_TYPE.TRAINING_TEMPO)
            Title.text = CommonFunc.ConvertTrainingTypeEngStr(data.TrainingType);
        else
            Title.text = CommonFunc.ConvertPoseTypeEngStr(data.TrainingPoseType);

        Desc.text = string.Format("{0} / {1}", data.PracticeCount, CommonData.MAX_PRACTICE_COUNT);
        Today.text = string.Format("Today : {0}", data.TodayPracticeCount);
        Slide.value = (float)data.PracticeCount / CommonData.MAX_PRACTICE_COUNT;
    }
}
