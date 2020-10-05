using System;
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

    private Action FriendListFreshAction = null;

    public PopupFreindPlus()
        : base(PopupMgr.POPUP_TYPE.FRIEND_PLUS)
    {

    }

    public class PopupData : PopupBaseData
    {
        public Action FriendListFreshAction = null;

        public PopupData(Action friendListFreshAction = null)
        {
            FriendListFreshAction = friendListFreshAction;
        }
    }

    public override void SetData(PopupBaseData data)
    {
        var popupData = data as PopupData;
        if (popupData != null)
            FriendListFreshAction = popupData.FriendListFreshAction;
        else
            FriendListFreshAction = null;

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
        else if (friendCode == TKManager.Instance.Mydata.UserCode)
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("유효하지 않은 코드 입니다", null, null, PopupMsg.BUTTON_TYPE.ONE));
        else
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("친구를 추가하시겠습니까?", () =>
            {
                FirebaseManager.Instance.AddFriend(friendCode, () =>
                {
                    if (FirebaseManager.Instance.AddFriendEnable)
                    {
                        var list = TKManager.Instance.Mydata.FriendDataList;
                        string index = "";

                        for (int i = 0; i < list.Count; i++)
                        {
                            if(list[i].Index != "" && list[i].DataLoad == false)
                            {
                                index = list[i].Index;
                                break;
                            }
                        }
                        if (index != "")
                        {
                            FirebaseManager.Instance.GetFriendData_ONE(index, () =>
                           {
                               StartCoroutine(Co_GetFriendPlusEnd());
                           });
                        } 
                    }
                    else
                    {
                        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("유효하지 않은 코드 입니다", null, null, PopupMsg.BUTTON_TYPE.ONE));
                    }
                });
            }));
        }   
    }

    public IEnumerator Co_GetFriendPlusEnd()
    {

        TextureCacheManager.Instance.LoadImage();

        while (true)
        {
            if (TextureCacheManager.Instance.ImageLoadProgress == false)
                break;

            yield return null;
        }

        if (FriendListFreshAction != null)
            FriendListFreshAction();

        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("친구를 추가 하였습니다", null, null, PopupMsg.BUTTON_TYPE.ONE));
    }
}
