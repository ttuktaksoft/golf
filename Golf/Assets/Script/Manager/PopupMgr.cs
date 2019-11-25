using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMgr : MonoBehaviour
{
    public static PopupMgr _instance = null;
    public static PopupMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PopupMgr>() as PopupMgr;
            }
            return _instance;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public enum POPUP_TYPE
    {
        NONE,
        LOADING,
        TEST,
        MSG,
        GIFT_CON,
        TUTORIAL,
        SELF_EVALUATION,
        USER_SETTING,
        WEB_VIEW,
        TERMS,
        HELP,
        FRIEND_PLUS,
        REWARD_SELECT,
        PURCHASE,
        PURCHASE_SHIPPING,
    }
    
    private Dictionary<POPUP_TYPE, Popup> PopupList = new Dictionary<POPUP_TYPE, Popup>();
    private POPUP_TYPE CurrentPopup = POPUP_TYPE.NONE;
    private List<POPUP_TYPE> QueuePopupType = new List<POPUP_TYPE>();

    public void RegisterPopup(Popup popup)
    {
        if (PopupList.ContainsKey(popup.PopupType) == false)
            PopupList.Add(popup.PopupType, popup);

        DontDestroyOnLoad(popup);
        popup.gameObject.SetActive(false);
    }

    public void ShowPopup(POPUP_TYPE type, Popup.PopupBaseData data = null)
    {
        if (CurrentPopup == type)
            DismissPopup();

        if (PopupList.ContainsKey(type) == false)
            return;

        var popup = PopupList[type];
        popup.gameObject.SetActive(true);

        CurrentPopup = type;
        QueuePopupType.Add(type);

        // sortingOrder

        SortCanvasDepth();

        popup.SetData(data);
    }

    public void DismissPopup()
    {
        if (QueuePopupType.Count <= 0)
            return;

        DismissPopup(CurrentPopup);
    }

    public void DismissPopup(POPUP_TYPE type)
    {
        if (QueuePopupType.Count <= 0)
            return;

        if (QueuePopupType.Contains(type) == false)
            return;

        PopupList[type].gameObject.SetActive(false);

        QueuePopupType.RemoveAt(QueuePopupType.Count - 1);
        if (QueuePopupType.Count <= 0)
            CurrentPopup = POPUP_TYPE.NONE;
        else
            CurrentPopup = QueuePopupType[QueuePopupType.Count - 1];
    }

    public void SortCanvasDepth()
    {
        for (int i = 0; i < QueuePopupType.Count; i++)
        {
            var popup = PopupList[QueuePopupType[i]];
            popup.GetComponent<Canvas>().sortingOrder = 100 + i;
        }
    }

    public bool IsShowPopup(POPUP_TYPE type)
    {
        return CurrentPopup == type;
    }

}
