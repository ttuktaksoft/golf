using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMyInfo : MonoBehaviour
{
    public Button ThumbnailEdit;
    public Image Thumbnail;
    public GameObject ThumbnailCameraIcon;
    public Text Name;
    public Image GradeImg;
    public Text Percent;
    public Text Point;
    //public Text Grade;
    public Image Gender;
    public Button Edit;

    public void Awake()
    {
        Edit.onClick.AddListener(OnClickEdit);
        ThumbnailEdit.onClick.AddListener(OnClickThumbnailEdit);
    }

    public void Init(bool editEnable = false)
    {
        Edit.gameObject.SetActive(editEnable);
        if (editEnable == false)
            ThumbnailEdit.onClick.RemoveAllListeners();
        ThumbnailCameraIcon.gameObject.SetActive(editEnable);

        Percent.text = string.Format("상위 {0}%", TKManager.Instance.Mydata.Percent);
        Point.text = string.Format("{0:n0} P / {1:n0} P", TKManager.Instance.Mydata.SeasonPoint, TKManager.Instance.Mydata.AccumulatePoint);

        RefreshUI();
    }

    public void RefreshUI()
    {
        Name.text = TKManager.Instance.Mydata.Name;
        //Grade.text = CommonFunc.GetGradeStr();
        CommonFunc.SetGradeImg(ref GradeImg, TKManager.Instance.Mydata.Grade);

        if (TKManager.Instance.Mydata.Gender == CommonData.GENDER.GENDER_MAN)
            CommonFunc.SetImageFile("icon_man", ref Gender);
        else
            CommonFunc.SetImageFile("icon_woman", ref Gender);

        CommonFunc.RefreshThumbnail(ref Thumbnail);
    }

    public void OnClickEdit()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.USER_SETTING, new PopupUserSetting.PopupData(RefreshUI));
    }

    public void OnClickThumbnailEdit()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.USER_SETTING, new PopupUserSetting.PopupData(RefreshUI));
    }
}
