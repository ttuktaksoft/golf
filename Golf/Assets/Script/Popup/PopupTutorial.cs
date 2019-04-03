using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTutorial : Popup
{
    public Button OK;
    public GameObject ListObj;
    private List<UITutorialSlot> TutorialSlotList = new List<UITutorialSlot>();
    private TutorialData.TUTORIAL_TYPE TutorialType = TutorialData.TUTORIAL_TYPE.MAIN;

    public PopupTutorial()
        : base(PopupMgr.POPUP_TYPE.TUTORIAL)
    {

    }

    public class PopupData : PopupBaseData
    {
        public TutorialData.TUTORIAL_TYPE TutorialType = TutorialData.TUTORIAL_TYPE.MAIN;
        public PopupData(TutorialData.TUTORIAL_TYPE type)
        {
            TutorialType = type;
        }
    }

    public override void SetData(PopupBaseData data)
    {
        PopupData popupData = data as PopupData;
        TutorialType = popupData.TutorialType;

        for (int i = 0; i < TutorialSlotList.Count; i++)
        {
            DestroyImmediate(TutorialSlotList[i].gameObject);
        }
        TutorialSlotList.Clear();

        if(DataManager.Instance.TutorialDataList.ContainsKey(TutorialType))
        {
            var list = DataManager.Instance.TutorialDataList[TutorialType];
            for (int i = 0; i < list.Count; i++)
            {
                var temp_data = list[i];
                var slotObj = Instantiate(Resources.Load("Prefab/UITutorialSlot"), ListObj.transform) as GameObject;
                var slot = slotObj.GetComponent<UITutorialSlot>();
                slot.SetData(temp_data);
                TutorialSlotList.Add(slot);
            }
        }
    }


    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public void OnClickOK()
    {
        PopupMgr.Instance.DismissPopup();
    }
}
