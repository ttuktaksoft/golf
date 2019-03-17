using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGradeInfoSlot : MonoBehaviour
{
    public Image Icon;
    public Text GradeName;
    public Image Line;
    public Text Point;
    public GameObject ThumbnailObj;
    public Image Thumbnail;

    public void SetGrade(int grade)
    {
        CommonFunc.SetGradeImg(ref Icon, grade);
        GradeName.text = CommonFunc.GetGradeStr(grade);
        Point.text = string.Format("{0:n0}P", CommonData.GRADE_POINT[grade]);

        var myGrade = TKManager.Instance.Mydata.Grade;
        if(myGrade == grade)
        {
            CommonFunc.RefreshThumbnail(ref Thumbnail);
            ThumbnailObj.gameObject.SetActive(true);
            //if(myGrade == 0 || myGrade == CommonData.GRADE_POINT.Length - 1)
            //{
            //    var pos = ThumbnailObj.transform.localPosition;
            //    pos.y = 0f;
            //    ThumbnailObj.transform.localPosition = pos;
            //}
            //else
            //{

            //}

        }
        else
        {
            ThumbnailObj.gameObject.SetActive(false);
        }


        
    }
}
