using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    public UIMyInfo MyInfo;

    public Image GradeImg;
    public Text Grade;
    public Text Point;

    //public List<UIRewardPracticeGraph> PracticeList = new List<UIRewardPracticeGraph>();


    public GameObject GiftconListObj;
    public GameObject GiftconEmpty;
    public GameObject ListObj;
    private List<UIGiftconSlot> GiftconSlotList = new List<UIGiftconSlot>();



    public void Init()
    {
        MyInfo.Init();

        RefreshGiftconList();

        CommonFunc.SetGradeImg(ref GradeImg, TKManager.Instance.GetGrade());
        Grade.text = CommonFunc.GetGradeStr(TKManager.Instance.GetGrade());
        Point.text = string.Format("현재 포인트 : {0:n0} Point", TKManager.Instance.Point + 1000);

        //for (int i = 0; i < DataManager.Instance.PracticeDataList.Count; i++)
        //{
        //    if (PracticeList.Count <= i)
        //        break;
        //    var data = DataManager.Instance.PracticeDataList[i];
        //    PracticeList[i].SetData(data);
        //}
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
