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

    public void SetData(FriendData data)
    {
        CommonFunc.SetImageFile(TextureCacheManager.Instance.GetTexture(data.ThumbnailUrl), ref Thumbnail);
        Name.text = data.Nickname;

        if (data.Gender == CommonData.GENDER.GENDER_MAN)
            CommonFunc.SetImageFile("icon_man", ref Gender);
        else
            CommonFunc.SetImageFile("icon_woman", ref Gender);

        CommonFunc.SetGradeImg(ref GradeImg, data.Grade);
        Point.text = string.Format("{0:n0} P", data.SeasonPoint);
    }

}
