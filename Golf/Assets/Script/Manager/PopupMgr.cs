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
        MSG,
        WEB_VIEW,
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
        popup.SetData(data);

        CurrentPopup = type;
        QueuePopupType.Add(type);

        // sortingOrder

        SortCanvasDepth();
    }

    public void DismissPopup()
    {
        if (QueuePopupType.Count <= 0)
            return;

        PopupList[CurrentPopup].gameObject.SetActive(false);

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
}
