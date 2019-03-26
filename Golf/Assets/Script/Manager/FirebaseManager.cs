using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Auth;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager _instance = null;
    public static FirebaseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FirebaseManager>() as FirebaseManager;
            }
            return _instance;
        }
    }

    public bool FirstLoadingComplete = false;
    public int LoadingCount = 0;
    public Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    private string googleIdToken;
    private string googleAccessToken;
    public DatabaseReference mDatabaseRef;


    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void init()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://golfhometraining.firebaseio.com/");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
    }

    public void GetData()
    {
        GetUserData();
    }

    private void AddFirstLoadingComplete()
    {
        if (FirstLoadingComplete == false)
            LoadingCount++;

        if (LoadingCount == 1)
            FirstLoadingComplete = true;
    }

    // 사용자 정보 파이어베이스에서 로드
    public void GetUserData()
    {
        string userIdx = TKManager.Instance.Mydata.Index;

        mDatabaseRef.Child("Users").Child(userIdx).GetValueAsync().ContinueWith(task =>
        {

        if (task.IsFaulted)
        {
            // Handle the error...
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;

            if (snapshot.Exists)
            {
                var tempData = snapshot.Value as Dictionary<string, object>;

                var tempGender = Convert.ToInt32(tempData["Gender"]);

                string tempName = tempData["Name"].ToString();
                int tempAccumPoint = Convert.ToInt32(tempData["AccumPoint"]);
                int tempSeasonPoint = Convert.ToInt32(tempData["SeasonPoint"]);

                TKManager.Instance.Mydata.Init((CommonData.GENDER)tempGender, userIdx, tempName, null, tempSeasonPoint, tempAccumPoint);            
            }

                AddFirstLoadingComplete();

                //  Debug.LogFormat("UserInfo: Index : {0} NickName {1} Point {2}", TKManager.Instance.MyData.Index, TKManager.Instance.MyData.NickName, TKManager.Instance.MyData.Point);
            }
        });
    }

    // 닉네임 중복 체크
    public bool IsExistNickName(string NickName)
    {
        bool rtValue = false;

        mDatabaseRef.Child("UserNameList").GetValueAsync().ContinueWith(task =>
        {

            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {

                    foreach (var tempChild in snapshot.Children)
                    {
                        var tempData = tempChild.Value.ToString();
                        Debug.LogFormat("UserInfo: Index : {0}", tempData);

                        if(tempData.Equals(NickName))
                        {
                            rtValue = true;
                            break;                           
                        }
                    }
                }
                //  Debug.LogFormat("UserInfo: Index : {0} NickName {1} Point {2}", TKManager.Instance.MyData.Index, TKManager.Instance.MyData.NickName, TKManager.Instance.MyData.Point);
            }
        });

        return rtValue;
    }

    // 사용자 정보 파이어베이스에 세팅
    public void SetUserData()
    {
        mDatabaseRef.Child("UserNameList").Child(TKManager.Instance.Mydata.Index).SetValueAsync(TKManager.Instance.Mydata.Name);

        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Index").SetValueAsync(TKManager.Instance.Mydata.Index);
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Name").SetValueAsync(TKManager.Instance.Mydata.Name);
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Gender").SetValueAsync(TKManager.Instance.Mydata.Gender);
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("AccumPoint").SetValueAsync(TKManager.Instance.Mydata.AccumulatePoint);
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("SeasonPoint").SetValueAsync(TKManager.Instance.Mydata.SeasonPoint);
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Grade").SetValueAsync(TKManager.Instance.Mydata.Grade);
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Percent").SetValueAsync(TKManager.Instance.Mydata.Percent);
        
    }

    public void SetSeasonPoint()
    {

    }
}