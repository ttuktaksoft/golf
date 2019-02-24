using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupHelp : Popup
{
    public Button Next;
    public Image HelpImg;

    public PopupHelp()
        : base(PopupMgr.POPUP_TYPE.HELP)
    {

    }

    public class PopupData : PopupBaseData
    {
        public PopupData()
        {
        }
    }

    public override void SetData(PopupBaseData data)
    {
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
}
