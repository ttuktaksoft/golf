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
    }

    public void OnClickEdit()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.USER_SETTING, new PopupUserSetting.PopupData(RefreshUI));
    }

    public void OnClickThumbnailEdit()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("!!!!!!Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path);
                if (texture == null)
                {
                    Debug.Log("!!!!!!Couldn't load texture from " + path);
                    return;
                }
                Sprite tempSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Thumbnail.rectTransform.pivot);
                Thumbnail.sprite = tempSprite;

            }
        }, "Select a PNG image", "image/png");
    }
}
