using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingHUD : Popup
{
    public RectTransform LoadingRect;

    public LoadingHUD()
        : base(PopupMgr.POPUP_TYPE.LOADING)
    {

    }

    public override void SetData(PopupBaseData data)
    {
    }

    public void Awake()
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
        LoadingRect.Rotate(0f, 0f, 300f * Time.deltaTime);
    }
    
}
