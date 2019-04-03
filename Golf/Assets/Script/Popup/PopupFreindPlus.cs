using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupFreindPlus : Popup
{
    public Text RecommenderCode;
    public Button CopyButton;
    public InputField FriendCodeInput;
    public Button PlusButton;
    public Button OK;

    public PopupFreindPlus()
        : base(PopupMgr.POPUP_TYPE.FRIEND_PLUS)
    {

    }

    public override void SetData(PopupBaseData data)
    {
        RecommenderCode.text = TKManager.Instance.Mydata.UserCode;
    }

    public void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
        CopyButton.onClick.AddListener(OnClcikCodeCopy);
        PlusButton.onClick.AddListener(OnClcikFriendPlus);
    }

    // Start is called before the first frame update
    void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickOK()
    {
        PopupMgr.Instance.DismissPopup();
    }

    public void OnClcikCodeCopy()
    { 
        GUIUtility.systemCopyBuffer = RecommenderCode.text;
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("복사하였습니다", null, null, PopupMsg.BUTTON_TYPE.ONE));
    }

    public void OnClcikFriendPlus()
    {
        string friendCode = FriendCodeInput.text.ToString();
        if(friendCode == "")
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("친구코드를 입력해주세요", null, null, PopupMsg.BUTTON_TYPE.ONE));
        else
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("친구를 추가하시겠습니까?"));
    }
}
