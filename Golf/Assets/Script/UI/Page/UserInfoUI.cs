using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour {

    public Button ThumbnailEdit;
    public Image Thumbnail;
    public Text Name;
    public Text Grade;
    public Image Gender;
    public Button Edit;
    public GameObject EmptyText;
    public GameObject EvaluationList;
    public GameObject ListObj;
    private List<EvaluationSlotUI> EvaluationSlotList = new List<EvaluationSlotUI>();

    public void Awake()
    {
        Edit.onClick.AddListener(OnClickEdit);
        ThumbnailEdit.onClick.AddListener(OnClickThumbnailEdit);
    }

    public void Init()
    {
        for (int i = 0; i < EvaluationSlotList.Count; i++)
        {
            DestroyImmediate(EvaluationSlotList[i].gameObject);
        }
        EvaluationSlotList.Clear();

        if (DataManager.Instance.EvaluationDataList.Count > 0)
        {
            EvaluationList.SetActive(true);
            EmptyText.SetActive(false);
        }
        else
        {
            EvaluationList.SetActive(false);
            EmptyText.SetActive(true);
        }

        for (int i = 0; i < DataManager.Instance.EvaluationDataList.Count; i++)
        {
            var data = DataManager.Instance.EvaluationDataList[i];
            var slotObj = Instantiate(Resources.Load("Prefab/UIEvaluationSlot"), ListObj.transform) as GameObject;
            var slot = slotObj.GetComponent<EvaluationSlotUI>();
            slot.SetData(data);
            EvaluationSlotList.Add(slot);
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        Name.text = TKManager.Instance.Name;
        Grade.text = CommonFunc.ConvertGradeStr(TKManager.Instance.GetGrade());
        Grade.color = CommonFunc.HexToColor(CommonFunc.GetGradeColor(TKManager.Instance.GetGrade()));
        if (TKManager.Instance.GetGender() == CommonData.GENDER.GENDER_MAN)
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
