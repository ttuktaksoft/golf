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
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                // Assign texture to a temporary quad and destroy it after 5 seconds
                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                quad.transform.forward = Camera.main.transform.forward;
                quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                Thumbnail.sprite = Sprite.Create(texture, Thumbnail.rectTransform.rect, Thumbnail.rectTransform.pivot);

                //Material material = quad.GetComponent<Renderer>().material;
                //if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                //    material.shader = Shader.Find("Legacy Shaders/Diffuse");

                //material.mainTexture = texture;

                Destroy(quad, 5f);

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                Destroy(texture, 5f);
            }
        }, "Select a PNG image", "image/png");
    }
}
