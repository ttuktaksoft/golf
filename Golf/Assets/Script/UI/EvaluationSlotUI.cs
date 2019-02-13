using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvaluationSlotUI : MonoBehaviour
{
    public Text Date;
    public Text Title;
    public Text SubTitle;
    public List<Image> Starlist = new List<Image>();
    public Text Msg;

    public void SetData(EvaluationData data)
    {
        Date.text = data.GetDate();
        Title.text = data.GetTraining();
        SubTitle.text = data.GetAngle();

        for (int i = 0; i < Starlist.Count; i++)
        {
            if(data.StarCount > i)
                Starlist[i].sprite = (Sprite)Resources.Load("star", typeof(Sprite));
            else
                Starlist[i].sprite = (Sprite)Resources.Load("star_empty", typeof(Sprite));
        }

        Msg.text = data.Msg;
    }
}
