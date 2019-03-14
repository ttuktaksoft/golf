using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingUI : MonoBehaviour
{
    // TODO 로딩씬으로 변경 예정
    // Start is called before the first frame update

    public bool Permissions = false;
    public bool Login = false;
    public bool Terms = false;

    static int getSDKInt()
    {
        using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
        {
            return version.GetStatic<int>("SDK_INT");
        }
    }

    void Start()
    {

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

        StartCoroutine(LoadingData());
    }

    IEnumerator LoadingData()
    {
        while(true)
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
        SceneManager.LoadScene("PopupScene", LoadSceneMode.Additive);

        TKManager.Instance.gyro.enabled = false;
        TKManager.Instance.LoadFile();
        DataManager.Instance.init();

        yield return new WaitForSeconds(2f);
        
        if (TKManager.Instance.MyLoadData == false)
        {
            Terms = false;
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.TERMS, new PopupTerms.PopupData(() =>
            {
                Terms = true;
            }));
        }
        else
            Terms = true;

        while (true)
        {
            if (Terms)
                break;

            yield return null;
        }


        if (TKManager.Instance.MyLoadData == false)
        {
            PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.USER_SETTING, new PopupUserSetting.PopupData(() =>
            {
                Login = true;
            }, true));
        }
        else
            Login = true;

        while (true)
        {
            if (Login)
                break;

            yield return null;
        }


        
        yield return null;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void OnClickAddress()
    {
        TKManager.Instance.gyro.enabled = false;
        TKManager.Instance.SetMode(CommonData.TRAINING_POSE.TRAINING_ADDRESS);
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }

    public void OnClickBackSwing()
    {
        TKManager.Instance.gyro.enabled = false;
        TKManager.Instance.SetMode(CommonData.TRAINING_POSE.TRAINING_BACKSWING);
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }
    public void OnClickImpact()
    {
        TKManager.Instance.gyro.enabled = false;
        TKManager.Instance.SetMode(CommonData.TRAINING_POSE.TRAINING_IMPACT);
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }

    public void OnClickTempo()
    {
        TKManager.Instance.gyro.enabled = false;
        TKManager.Instance.SetTrainingType(CommonData.TRAINING_TYPE.TRAINING_TEMPO);
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }


    // Update is called once per frame
    void Update()
    {

    }


}
