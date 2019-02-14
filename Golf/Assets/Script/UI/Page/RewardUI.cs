using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    public Text Name;
    public Text Grade;
    public Image Gender;
    public List<UIRewardPracticeGraph> PracticeList = new List<UIRewardPracticeGraph>();

    public GameObject GiftconListObj;
    public GameObject GiftconEmpty;
    public GameObject ListObj;
    private List<UIGiftconSlot> GiftconSlotList = new List<UIGiftconSlot>();



    public void Init()
    {
        Name.text = TKManager.Instance.Name;
        Grade.text = CommonFunc.ConvertGradeStr(TKManager.Instance.GetGrade());
        Grade.color = CommonFunc.HexToColor(CommonFunc.GetGradeColor(TKManager.Instance.GetGrade()));
        if (TKManager.Instance.GetGender() == CommonData.GENDER.GENDER_MAN)
            CommonFunc.SetImageFile("icon_man", ref Gender);
        else
            CommonFunc.SetImageFile("icon_woman", ref Gender);

        RefreshGiftconList();

        for (int i = 0; i < DataManager.Instance.PracticeDataList.Count; i++)
        {
            if (PracticeList.Count <= i)
                break;
            var data = DataManager.Instance.PracticeDataList[i];
            PracticeList[i].SetData(data);
        }
    }

    public void RefreshGiftconList()
    {
        for (int i = 0; i < GiftconSlotList.Count; i++)
        {
            DestroyImmediate(GiftconSlotList[i].gameObject);
        }
        GiftconSlotList.Clear();

        GiftconListObj.SetActive(DataManager.Instance.GiftconDataList.Count > 0);
        GiftconEmpty.SetActive(DataManager.Instance.GiftconDataList.Count <= 0);

        for (int i = 0; i < DataManager.Instance.GiftconDataList.Count; i++)
        {
            var data = DataManager.Instance.GiftconDataList[i];
            var slotObj = Instantiate(Resources.Load("Prefab/UIGiftcon"), ListObj.transform) as GameObject;
            var slot = slotObj.GetComponent<UIGiftconSlot>();
            slot.SetData(data);
            slot.SetRefreshUIAction(RefreshGiftconList);
            GiftconSlotList.Add(slot);
        }
    }
}
