using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWebView : Popup
{
    private WebViewObject webViewObject;

    public PopupWebView()
        : base(PopupMgr.POPUP_TYPE.WEB_VIEW)
    {

    }

    void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }
    // Update is called once per frame
    void Update()
    {

        if (Application.platform == RuntimePlatform.Android)
        {

            if (Input.GetKey(KeyCode.Escape))
            {
                //Destroy(webViewObject);
                return;
            }
        }

    }

    public override void SetData(PopupBaseData data)
    {
        StartWebView();
    }

    public void StartWebView()
    {
        string strUrl = "http://www.naver.com";

        webViewObject =
            (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
        webViewObject.Init((msg) =>
        {
            Debug.Log(string.Format("CallFromJS[{0}]", msg));
        });

        webViewObject.LoadURL(strUrl);
        webViewObject.SetVisibility(true);
        webViewObject.SetMargins(50, 50, 50, 50);
    }
}
