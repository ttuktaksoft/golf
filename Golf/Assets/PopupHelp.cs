using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupHelp : Popup
{
    public Button Next;
    public Image HelpImg;

    private List<string> HelpImgList = new List<string>();

    public PopupHelp()
        : base(PopupMgr.POPUP_TYPE.HELP)
    {

    }

    public class PopupData : PopupBaseData
    {
        public List<string> HelpImgList = new List<string>();

        public PopupData(List<string> list)
        {
            HelpImgList = list;
        }
    }

    public override void SetData(PopupBaseData data)
    {
        var popupData = data as PopupData;
        HelpImgList = popupData.HelpImgList;

        CommonFunc.SetImageFile(HelpImgList[0], ref HelpImg);
        HelpImgList.RemoveAt(0);
    }

    public void Awake()
    {
        Next.onClick.AddListener(OnClickNext);
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

    public void OnClickNext()
    {
        if (HelpImgList.Count <= 0)
        {
            PopupMgr.Instance.DismissPopup();
            return;
        }
            
        CommonFunc.SetImageFile(HelpImgList[0], ref HelpImg);
        HelpImgList.RemoveAt(0);
    }
}
