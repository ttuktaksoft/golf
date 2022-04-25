using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class PopupUserCreate : Popup
{
    public Button OK;
    public Button Cancel;

    private Action OkAction = null;

    public InputField ID_Input;
    public InputField PW_Input;
    public InputField Name_Input;

    public Button Man_Button;
    public Button Woman_Button;
    private CommonData.GENDER Gender = CommonData.GENDER.GENDER_MAN;

    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
        Cancel.onClick.AddListener(OnClickCancel);
        Man_Button.onClick.AddListener(OnClickMan);
        Woman_Button.onClick.AddListener(OnClickWoman);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }


    public PopupUserCreate()
        : base(PopupMgr.POPUP_TYPE.CRAETE)
    {

    }

    public class PopupData : PopupBaseData
    {
        public Action OkAction = null;

        public PopupData(Action okAction = null)
        {
            OkAction = okAction;
        }
    }

    public override void SetData(PopupBaseData data)
    {
        var popupData = data as PopupData;
        if (popupData == null)
            return;

        OkAction = popupData.OkAction;
        ID_Input.text = "";
        PW_Input.text = "";
        RefreahUI();
    }

    public void RefreahUI()
    {
        if (Gender == CommonData.GENDER.GENDER_MAN)
        {
            Man_Button.interactable = false;
            Woman_Button.interactable = true;
        }
        else
        {
            Man_Button.interactable = true;
            Woman_Button.interactable = false;
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

    public void OnClickOK()
    {
        CreateUser();
    }

    public void OnClickCancel()
    {
        PopupMgr.Instance.DismissPopup();
    }

    public void CreateUser()
    {
        string id = ID_Input.text.ToString();
        string pw = PW_Input.text.ToString();

        bool result = Regex.IsMatch(id, @"^[a-zA-Z0-9]+$");

        if (id.Contains(" "))
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("아이디에 빈칸을 넣을수 없습니다", null, null, PopupMsg.BUTTON_TYPE.ONE));
            return;
        }

        if (result == false)
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("아이디는 영문과 숫자만 가능합니다", null, null, PopupMsg.BUTTON_TYPE.ONE));
            return;
        }

        if (id.Contains(".") ||
            id.Contains("#") ||
            id.Contains("$") ||
            id.Contains("[") ||
            id.Contains("]"))
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData(". # $ [ ] 문자는 사용 할 수 없습니다", null, null, PopupMsg.BUTTON_TYPE.ONE));
            return;
        }

        if (string.IsNullOrWhiteSpace(id))
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("아이디를 입력해주세요", null, null, PopupMsg.BUTTON_TYPE.ONE));
            return;
        }

        if (string.IsNullOrWhiteSpace(pw))
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("비밀번호를 입력해주세요", null, null, PopupMsg.BUTTON_TYPE.ONE));
            return;
        }

        FirebaseManager.Instance.IsExistID(ID_Input.text.ToString(), () =>
        {
            if (FirebaseManager.Instance.UserIDExist)
            {
                PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("아이디가 중복입니다", null, null, PopupMsg.BUTTON_TYPE.ONE));
            }
            else
            {
                TKManager.Instance.Mydata.SetData(Gender, Name_Input.text.ToString(), "", 0, 0, "");

                FirebaseManager.Instance.RegisterUserByFirebase((index) =>
                {
                    TKManager.Instance.Mydata.Init(index.ToString());

                    FirebaseManager.Instance.SetUserCreateData(ID_Input.text.ToString(), PW_Input.text.ToString(), index);
                    FirebaseManager.Instance.SetUserData();

                    TKManager.Instance.SaveFile();

                    if (OkAction != null)
                        OkAction();

                    PopupMgr.Instance.DismissPopup(this.PopupType);
                });
            }
        });
    }
}
