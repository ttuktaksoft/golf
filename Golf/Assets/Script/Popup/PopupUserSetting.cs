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
    public Button BigOK;
    public Button OK;
    public Button Cancel;

    public GameObject TermsObj;
    public Button TermsEnableButton;
    public GameObject TermsCheckObj;
    public Button TermsViewButton;

    private bool TermsEnable = true;
    private Action OkAction = null;
    private CommonData.GENDER Gender = CommonData.GENDER.GENDER_MAN;
    private bool Firstuser = false;

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

        Firstuser = popupData.FirstUser;
        if (Firstuser)
        {
            Title.text = "회원가입";
            OK.gameObject.SetActive(false);
            Cancel.gameObject.SetActive(false);
            BigOK.gameObject.SetActive(true);
            TermsObj.gameObject.SetActive(false);
        }
        else
        {
            Title.text = "정보변경";
            OK.gameObject.SetActive(true);
            Cancel.gameObject.SetActive(true);
            BigOK.gameObject.SetActive(false);
            TermsObj.gameObject.SetActive(false);
        } 

        OkAction = popupData.OkAction;

        Name.text = TKManager.Instance.Mydata.Name;
        Number.text = TKManager.Instance.Mydata.PhoneNumber;
        Gender = TKManager.Instance.Mydata.Gender;

        CommonFunc.RefreshThumbnail(ref Thumbnail);

        RefreahUI();
        RefreshTermsEnable();
    }


    private void Awake()
    {
        BigOK.onClick.AddListener(OnClickOK);
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
        if (Firstuser)
        {
            FirebaseManager.Instance.IsExistNickName(Name.text.ToString(), () =>
            {
                if (FirebaseManager.Instance.NickNameExist)
                {
                    PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("닉네임이 중복입니다", null, null, PopupMsg.BUTTON_TYPE.ONE));
                }
                else
                {
                    if (TermsEnable == false)
                    {
                        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("트레이닝 포인트에 따른 각종 혜택을 받기 위해서 개인정보 취급동의가 필요 합니다", () =>
                        {
                            TKManager.Instance.Mydata.SetData(Gender, Name.text.ToString(), Number.text.ToString(), 0, 0, "");

                            FirebaseManager.Instance.RegisterUserByFirebase();

                            if (OkAction != null)
                                OkAction();

                            TKManager.Instance.SaveFile();
                            PopupMgr.Instance.DismissPopup();
                        }));
                    }
                    else
                    {
                        TKManager.Instance.Mydata.SetData(Gender, Name.text.ToString(), Number.text.ToString(), 0, 0, "");

                        FirebaseManager.Instance.RegisterUserByFirebase();

                        if (OkAction != null)
                            OkAction();

                        TKManager.Instance.SaveFile();
                        PopupMgr.Instance.DismissPopup();
                    }
                }
            });
        }
        else
        {
            // TODO 도형 이미지를 올리고 url 받아서 mydata에 세팅
            TKManager.Instance.Mydata.SetData(Gender, Name.text.ToString(), Number.text.ToString(), 0, 0, "");

            FirebaseManager.Instance.SetUserData();

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
