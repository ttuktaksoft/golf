using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFriendSlot : MonoBehaviour
{
    public Image Thumbnail;
    public Text Name;
    public Image Gender;
    public Image GradeImg;
    public Text Point;

    public void Init(int index = 0)
    {
        
        if(index == 0)
        {
            CommonFunc.SetImageFile("Temp_1/test_6", ref Thumbnail);
            Name.text = "이민영";
            CommonFunc.SetImageFile("icon_man", ref Gender);
            CommonFunc.SetGradeImg(ref GradeImg, 6);
            Point.text = string.Format("{0:n0} Point", 10000);
        }

        if (index == 1)
        {
            CommonFunc.SetImageFile("logo_2", ref Thumbnail);
            Name.text = "이민영골프아카데미";
            CommonFunc.SetImageFile("icon_man", ref Gender);
            CommonFunc.SetGradeImg(ref GradeImg, 5);
            Point.text = string.Format("{0:n0} Point", 5000);
        }
    }

}
