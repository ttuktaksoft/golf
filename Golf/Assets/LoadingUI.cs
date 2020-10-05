using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using LitJson;

public class LoadingUI : MonoBehaviour
{
    // TODO 로딩씬으로 변경 예정
    // Start is called before the first frame update

    public GameObject LoadingPage;
    public GameObject LoginPage;

    public bool Permissions = false;
    public bool Terms = false;

    public Button GuestLogin;

    public Button Kakao;
    public GameObject WebviewObj;
    public RectTransform WebviewObjRect;
    public WebViewObject WebView;
    public Button WebViewClose;

    private bool KakaoDataSaveProgress = false;
    private string KakaoAccessToken = "";

    static int getSDKInt()
    {
        using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
        {
            return version.GetStatic<int>("SDK_INT");
        }
    }

    private void Awake()
    {
        ApplicationChrome.statusBarState = ApplicationChrome.States.Visible;

        GuestLogin.onClick.AddListener(OnClickGuestLogin);
        Kakao.onClick.AddListener(OnClickKakaoLogin);
        WebViewClose.onClick.AddListener(OnClickWebViewClose);
        WebviewObj.gameObject.SetActive(false);
    }

    void Start()
    {
        WebViewClose.gameObject.SetActive(false);
        LoadingPage.gameObject.SetActive(true);
        LoginPage.gameObject.SetActive(false);
        Kakao.gameObject.SetActive(false);
#if (UNITY_ANDROID && !UNITY_EDITOR)

      int version = getSDKInt();
        
        //Log.d("!!@@@@@ version");
        Debug.Log("!!@@@@@ version : " + version);

        if (getSDKInt() < 23)
        {

            Permissions = true;
            // Android OS Version 4.1 이상에서 동작할 내용
        }
        else
        {
            AndroidPermissionsManager.RequestPermission(new[] { "android.permission.READ_EXTERNAL_STORAGE" }, new AndroidPermissionCallback(
         grantedPermission =>
         {
             Permissions = true;
                // 권한이 승인 되었다.
                //CallPermission();
            },
         deniedPermission =>
         {
            Permissions = true;
                //canvas_denied.SetActive(true);
                // 권한이 거절되었다.
            },
         deniedPermissionAndDontAskAgain =>
         {
            Permissions = true;
                // 권한이 거절된데다가 다시 묻지마시오를 눌러버렸다.
                // 안드로이드 설정창 권한에서 직접 변경 할 수 있다는 팝업을 띄우는 방식을 취해야함. 
                //canvas_denied.SetActive(true);
            }));
        }


    
#elif (UNITY_ANDROID && UNITY_EDITOR) || UNITY_IOS
        Permissions = true;
#endif
        var widthoffeset = Screen.width - WebviewObjRect.rect.width;
        var heightffeset = Screen.height - WebviewObjRect.rect.height;
        StartCoroutine(LoadingData());
    }

    IEnumerator LoadingData()
    {
        var loadpopup = SceneManager.LoadSceneAsync("PopupScene", LoadSceneMode.Additive);
        while (!loadpopup.isDone)
        {
            print("Loading the Scene");
            yield return null;
        }

        TKManager.Instance.ShowLoading();
        while (true)
        {
            if (Permissions)
                break;

            yield return null;
        }

        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        print("Loading the Scene 1");
        TKManager.Instance.gyro.enabled = false;
        print("Loading the Scene 2");
        TKManager.Instance.LoadFile();
        print("Loading the Scene 3");
        DataManager.Instance.init();
        print("Loading the Scene 4");
        TextureCacheManager.Instance.init();
        print("Loading the Scene 5");
        yield return new WaitForSeconds(2f);
        TKManager.Instance.HideLoading();
        
        if(TKManager.Instance.MyLoadData)
        {
            // 파베 데이터 땡겨오기
            yield return null;
            yield return null;
            yield return null;
            GetUserData();
        }
        else
        {
            // 로그인이 안되었다.
            CreateUser();
            LoadingPage.gameObject.SetActive(false);
            LoginPage.gameObject.SetActive(true);
        }

        //if (TKManager.Instance.MyLoadData == false)
        //{
        //    Terms = false;
        //    PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.TERMS, new PopupTerms.PopupData(() =>
        //    {
        //        Terms = true;
        //    }));
        //}
        //else
        //    Terms = true;

        //while (true)
        //{
        //    if (Terms)
        //        break;

        //    yield return null;
        //}

    }

    public void CreateUser()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.USER_SETTING, new PopupUserSetting.PopupData(() =>
        {
            StartCoroutine(Co_RegisterUserProgress());
        }, true));
    }

    public IEnumerator Co_RegisterUserProgress()
    {
        TKManager.Instance.ShowLoading();
        while(true)
        {
            if (FirebaseManager.Instance.RegisterUserProgress == false)
                break;

            yield return null;
        }

        TKManager.Instance.SaveFile();

        if (KakaoDataSaveProgress)
        {
            StartCoroutine(Co_SetKakaoFBKey());

            while (true)
            {
                if (KakaoDataSaveProgress == false)
                    break;

                yield return null;
            }
        }

        TKManager.Instance.HideLoading();

        GetUserData();
    }

    public void GetUserData()
    {
        StartCoroutine(Co_GetUserData());
    }

    public IEnumerator Co_GetUserData()
    {
        TKManager.Instance.ShowLoading();

        FirebaseManager.Instance.GetData();

        while (true)
        {
            if (FirebaseManager.Instance.FirstLoadingComplete)
                break;

            yield return null;
        }

        var friendList = TKManager.Instance.Mydata.FriendDataList;
        var friendIndexLIst = new List<string>();
        for (int i = 0; i < friendList.Count; i++)
        {
            if (friendList[i].Index != "" && friendList[i].DataLoad == false)
                friendIndexLIst.Add(friendList[i].Index);
        }

        yield return FirebaseManager.Instance.Co_GetFriendData(friendIndexLIst);

        TextureCacheManager.Instance.LoadImage();

        while (true)
        {
            if (TextureCacheManager.Instance.ImageLoadProgress == false)
                break;

            yield return null;
        }

        TKManager.Instance.SetUserLocation();

        TKManager.Instance.HideLoading();

        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void OnClickGuestLogin()
    {
        CreateUser();
    }


    public void OnClickWebViewClose()
    {
        WebViewClose.gameObject.SetActive(false);
        WebView.SetVisibility(false);
    }
    public void OnClickKakaoLogin()
    {
        StartCoroutine(Co_KakaoLoginStart(CreateUser));
    }

    public IEnumerator Co_KakaoLoginStart(Action createUser)
    {
        TKManager.Instance.ShowLoading();
        WebViewClose.gameObject.SetActive(true);

        string query = "https://kauth.kakao.com/oauth/authorize";
        string clientId = "?client_id=" + CommonData.KAKAO_REST_API_KEY;
        string redirectUri = "&redirect_uri=" + CommonData.KAKAO_REDIRECT_URI;
        string etc = "&response_type=code";

        query += clientId;
        query += redirectUri;
        query += etc;

        UnityWebRequest www = UnityWebRequest.Get(query);

        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);

        }
        else
        {
            Debug.Log(www.ToString());
        }

        yield return null;

        WebviewObj.gameObject.SetActive(true);

        string strUrl = www.uri.AbsoluteUri;
        bool codeLoad = false;
        WebView.Init((msg) =>
        {
            Debug.Log(string.Format("CallFromJS_onJS[{0}]", msg));
        },
        false,
        "",
        (msg) =>
        {
            Debug.Log(string.Format("CallFromJS_onError[{0}]", msg));
        },
        (msg) =>
        {
            Debug.Log(string.Format("CallFromJS_onHttpError[{0}]", msg));
        },
        (msg) =>
        {
            Debug.Log(string.Format("CallFromJS_onLoaded[{0}]", msg));
        },
        false,
        (msg) =>
        {
            Debug.Log(string.Format("CallFromJS_onStarted[{0}]", msg));

            if (msg.Contains(CommonData.KAKAO_REDIRECT_URI + "?code="))
            {
                int index = msg.IndexOf("=");
                if (index > 0)
                {
                    TKManager.Instance.HideLoading();
                    string code = msg.Substring(index + 1);
                    Debug.Log(string.Format("CallFromJS_onStarted code {0}", code));
                    StartCoroutine(Co_GetAccessToken(code, createUser));
                    codeLoad = true;
                    //Destroy(WebView);
                }
            }
            else if(msg.Contains("error_description"))
            {
                TKManager.Instance.HideLoading();
                WebViewClose.gameObject.SetActive(false);
                WebView.SetVisibility(false);
            }

        });

        var widthoffeset = Screen.width - WebviewObjRect.rect.width;
        var heightffeset = Screen.height - WebviewObjRect.rect.height;

        WebView.SetMargins(0, (int)heightffeset, 0, 0);
        WebView.LoadURL(strUrl);
        WebView.SetVisibility(true);

        while(true)
        {
            yield return null;

            if (codeLoad)
            {
                Destroy(WebView);
                break;
            }

            yield return null;
        }
    }

    [System.Serializable]
    class KakaoTokenData
    {
        public string access_token;
        public string token_type;
        public string refresh_token;
        public int expires_in;
        public string scope;
        public int refresh_token_expires_in;
    }

    public IEnumerator Co_GetAccessToken(string kakaoCode, Action createUser)
    {
        TKManager.Instance.ShowLoading();
        string query = "?grant_type=authorization_code";
        string clientId = "&client_id=" + CommonData.KAKAO_REST_API_KEY;
        string redirectUri = "&redirect_uri=" + CommonData.KAKAO_REDIRECT_URI;
        string code = "&code=" + kakaoCode;

        query += clientId;
        query += redirectUri;
        query += code;

        UnityWebRequest www = UnityWebRequest.Post("https://kauth.kakao.com/oauth/token" + query, "");
        www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log("www.error Form upload complete!");
        }
        else
        {
            TKManager.Instance.HideLoading();
            var data = JsonUtility.FromJson<KakaoTokenData>(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
            Debug.Log("Form upload complete!");
            KakaoAccessToken = data.access_token;

            // 테스트용 앱 연결 해제
            //UnityWebRequest www2 = UnityWebRequest.Post("https://kapi.kakao.com/v1/user/unlink", "");
            //string token = "Bearer " + KakaoAccessToken;
            //www2.SetRequestHeader("Authorization", token);
            //yield return www2.SendWebRequest();

            //if (www2.isNetworkError || www2.isHttpError)
            //{
            //    Debug.Log(www2.error);
            //    Debug.Log("www2.error Form upload complete!");
            //}
            //else
            //{
            //    Debug.Log(www2.downloadHandler.text);
            //    Debug.Log("Form upload complete!");
            //}

            StartCoroutine(Co_GetKakaoFBKey(KakaoAccessToken, createUser));
        }
    }

    public IEnumerator Co_GetKakaoFBKey(string accessToken, Action createUser)
    {
        TKManager.Instance.ShowLoading();
        string propertiesFB = UnityWebRequest.EscapeURL(CommonData.KAKAO_PROPERTIES_FB);
        string query = "?property_keys=" + "[" + "\"" + propertiesFB + "\"" + "]";
        UnityWebRequest www = UnityWebRequest.Post("https://kapi.kakao.com/v2/user/me" + query, "");
        string token = "Bearer " + accessToken;
        www.SetRequestHeader("Authorization", token);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log("www.error");
        }
        else
        {
            string str = www.downloadHandler.text;
            if (str.IndexOf("firebasekey") >= 0)
            {
                TKManager.Instance.HideLoading();
                JsonData jsonData = JsonMapper.ToObject(www.downloadHandler.text);
                string fbKey = jsonData["properties"]["firebasekey"].ToString();
                // 확인
                TKManager.Instance.Mydata.SetIndex(fbKey);
                GetUserData();
            }
            else
            {
                TKManager.Instance.HideLoading();
                KakaoDataSaveProgress = true;
                createUser();
            }

            Debug.Log(www.downloadHandler.text);
            Debug.Log("Form upload complete!");
        }
    }

    [System.Serializable]
    class KakaoFBKeyData
    {
        public string firebasekey;
    }
    public IEnumerator Co_SetKakaoFBKey()
    {
        TKManager.Instance.ShowLoading();
        KakaoFBKeyData jsonData = new KakaoFBKeyData();
        jsonData.firebasekey = TKManager.Instance.Mydata.Index;

        string query = "?properties=" + UnityWebRequest.EscapeURL(JsonUtility.ToJson(jsonData, false));

        UnityWebRequest www = UnityWebRequest.Post("https://kapi.kakao.com/v1/user/update_profile" + query, "");
        string token = "Bearer " + KakaoAccessToken;
        www.SetRequestHeader("Accept", "application/json");
        www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");
        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log("www.error!");
        }
        else
        {
            TKManager.Instance.HideLoading();
            KakaoDataSaveProgress = false;
            Debug.Log(www.downloadHandler.text);
            Debug.Log("Form upload complete!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
