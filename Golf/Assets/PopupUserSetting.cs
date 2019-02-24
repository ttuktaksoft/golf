using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUserSetting : Popup
{
    public Text Title;
    public Button ThumbnailEdit;
    public Image Thumbnail;
    public InputField Name;
    public InputField Number;
    public Button ManButton;
    public Button WomanButton;
    public Button OK;
    public Button Cancel;

    public GameObject TermsObj;
    public Button TermsEnableButton;
    public GameObject TermsCheckObj;
    public Button TermsViewButton;

    private bool TermsEnable = false;
    private Action OkAction = null;
    private CommonData.GENDER Gender = CommonData.GENDER.GENDER_MAN;

    public PopupUserSetting()
        : base(PopupMgr.POPUP_TYPE.USER_SETTING)
    {

    }

    public class PopupData : PopupBaseData
    {
        public Action OkAction = null;
        public bool FirstUser = false;

        public PopupData(Action okAction = null, bool firstuser = false)
        {
            OkAction = okAction;
            FirstUser = firstuser;
    }
    }

    public override void SetData(PopupBaseData data)
    {
        var popupData = data as PopupData;
        if (popupData == null)
            return;

        if (popupData.FirstUser)
        {
            Title.text = "회원가입";
            Cancel.gameObject.SetActive(false);
            TermsObj.gameObject.SetActive(true);
        }
        else
        {
            Title.text = "정보변경";
            Cancel.gameObject.SetActive(true);
            TermsObj.gameObject.SetActive(false);
        } 

        OkAction = popupData.OkAction;

        Name.text = TKManager.Instance.Name;
        Number.text = TKManager.Instance.PhoneNumber;
        Gender = TKManager.Instance.GetGender();

        CommonFunc.RefreshThumbnail(ref Thumbnail);

        RefreahUI();
        RefreshTermsEnable();
    }


    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
        Cancel.onClick.AddListener(OnClickCancel);

        ManButton.onClick.AddListener(OnClickMan);
        WomanButton.onClick.AddListener(OnClickWoman);

        ThumbnailEdit.onClick.AddListener(OnClickThumbnailEdit);

        TermsEnableButton.onClick.AddListener(OnClickTermsEnable);
        TermsViewButton.onClick.AddListener(OnClickTermsView);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public void OnClickOK()
    {
        /*
        bool emptyString = true;
        string name = Name.text.ToString();
        for (int i = 0; i < name.Length; i++)
        {
            if (name[i] != ' ')
                emptyString = false;
        }


        if (emptyString)
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("이름을 입력해주세요"));
            return;
        }
        */
        if(TermsEnable == false)
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("트레이닝 포인트에 따른 각종 혜택을 받기 위해서 개인정보 취급동의가 필요 합니다.", () =>
            {
                TKManager.Instance.SetName(Name.text.ToString());
                TKManager.Instance.SetPhoneNumber(Number.text.ToString());
                TKManager.Instance.SetGender(Gender);

                if (OkAction != null)
                    OkAction();

                TKManager.Instance.SaveFile();
                PopupMgr.Instance.DismissPopup();
            }));
        }
        else
        {
            TKManager.Instance.SetName(Name.text.ToString());
            TKManager.Instance.SetPhoneNumber(Number.text.ToString());
            TKManager.Instance.SetGender(Gender);

            if (OkAction != null)
                OkAction();

            TKManager.Instance.SaveFile();
            PopupMgr.Instance.DismissPopup();
        }
    }

    public void OnClickCancel()
    {
        PopupMgr.Instance.DismissPopup();
    }

    public void RefreahUI()
    {
        if (Gender == CommonData.GENDER.GENDER_MAN)
        {
            ManButton.interactable = false;
            WomanButton.interactable = true;
        }
        else
        {
            ManButton.interactable = true;
            WomanButton.interactable = false;
        }
    }

    public void OnClickMan()
    {
        Gender = CommonData.GENDER.GENDER_MAN;
        RefreahUI();
    }

    public void OnClickWoman()
    {
        Gender = CommonData.GENDER.GENDER_WOMAN;
        RefreahUI();
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
                TKManager.Instance.ThumbnailSpritePath = path;
                TKManager.Instance.ThumbnailSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Thumbnail.rectTransform.pivot);
                Thumbnail.sprite = TKManager.Instance.ThumbnailSprite;

            }
        }, "Select a PNG image", "image/png");
    }

    public void OnClickTermsEnable()
    {
        TermsEnable = !TermsEnable;
        RefreshTermsEnable();
    }

    public void OnClickTermsView()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.TERMS);
    }

    public void RefreshTermsEnable()
    {
        TermsCheckObj.gameObject.SetActive(TermsEnable);
    }

}
