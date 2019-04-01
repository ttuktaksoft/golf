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

        SignUpByAnonymouse();
    }

    public void SignUpByGoogle()
    {
        Firebase.Auth.Credential credential =
        Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    public void SignUpByAnonymouse()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    public void SignUpByFaceBook()
    {
        /*
        Firebase.Auth.Credential credential =
    Firebase.Auth.FacebookAuthProvider.GetCredential(accessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
        */
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

    // 유져 인덱스 받아오기
    public void RegisterUserByFirebase()
    {
        mDatabaseRef.Child("UsersCount").RunTransaction(mutableData =>
        {
            int tempCount = Convert.ToInt32(mutableData.Value);

            if (tempCount == 0)
            {
                tempCount = 0;
            }
            else
            {
                TKManager.Instance.Mydata.SetIndex(tempCount.ToString());

                SetUserData();
                mutableData.Value = tempCount + 1;
            }

            return TransactionResult.Success(mutableData);
        });
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
                TKManager.Instance.SetUserLocation();

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
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Gender").SetValueAsync((int)TKManager.Instance.Mydata.Gender);
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("AccumPoint").SetValueAsync(TKManager.Instance.Mydata.AccumulatePoint);
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("SeasonPoint").SetValueAsync(TKManager.Instance.Mydata.SeasonPoint);
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Grade").SetValueAsync(TKManager.Instance.Mydata.Grade);
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Percent").SetValueAsync(TKManager.Instance.Mydata.Percent);
        
    }


    // 사용자 성별 파이어베이스에 세팅
    public void SetUserGender()
    {
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Gender").SetValueAsync((int)TKManager.Instance.Mydata.Gender);
    }

    // 사용자 네임 파이어베이스에 세팅
    public void SetUserName()
    {
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Name").SetValueAsync(TKManager.Instance.Mydata.Name);
    }

    // 사용자 시즌포인트 세팅
    public void SetSeasonPoint()
    {
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("SeasonPoint").SetValueAsync(TKManager.Instance.Mydata.SeasonPoint);
    }

    // 사용자 누적포인트 세팅
    public void SetAccumPoint()
    {
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("AccumPoint").SetValueAsync(TKManager.Instance.Mydata.AccumulatePoint);
    }

    // 사용자 퍼센트 세팅
    public void SetPercent()
    {
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Percent").SetValueAsync(TKManager.Instance.Mydata.Percent);
    }

    // 사용자 등급 세팅
    public void SetGrade()
    {
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Grade").SetValueAsync(TKManager.Instance.Mydata.Grade);
    }


    // 사용자 트레이닝 세팅    
    public void SetTraingReport(int trainingType, int trainingCount)
    {
        switch(trainingType)
        {
            case 0:
                GetPoseTrainingCount("Address", trainingCount);
                break;
            case 1:
                GetTempoTrainingCount(trainingCount);
                break;
            case 6:
                GetPoseTrainingCount("BackSwingTop", trainingCount);
                break;
            case 12:
                GetPoseTrainingCount("Impact", trainingCount);
                break;
        }
    }

    public void GetPoseTrainingCount(string trainingType, int trainingCount)
    {
        
        mDatabaseRef.Child("TraingReport").Child("PoseTraining").Child(trainingType).Child(TKManager.Instance.Mydata.Index).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists || snapshot.Value != null)
                {
                    int tempCount = Convert.ToInt32(snapshot.Value);

                    tempCount += trainingCount;
                    mDatabaseRef.Child("TraingReport").Child("PoseTraining").Child(trainingType).Child(TKManager.Instance.Mydata.Index).SetValueAsync(tempCount);

                    mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("PoseTraining").Child(trainingType).SetValueAsync(tempCount);
                }
            }
            else
            {
                mDatabaseRef.Child("TraingReport").Child("PoseTraining").Child(trainingType).Child(TKManager.Instance.Mydata.Index).SetValueAsync(trainingCount);
                mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("PoseTraining").Child(trainingType).SetValueAsync(trainingCount);
            }

            //  Debug.LogFormat("UserInfo: Index : {0} NickName {1} Point {2}", TKManager.Instance.MyData.Index, TKManager.Instance.MyData.NickName, TKManager.Instance.MyData.Point);
        
        });
    }

    public void GetTempoTrainingCount(int trainingCount)
    {
        mDatabaseRef.Child("TraingReport").Child("TempoTraining").Child(TKManager.Instance.Mydata.Index).GetValueAsync().ContinueWith(task =>
        {

            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists || snapshot.Value != null)
                {
                    int tempCount = Convert.ToInt32(snapshot.Value);

                    tempCount += trainingCount;
                    mDatabaseRef.Child("TraingReport").Child("TempoTraining").Child(TKManager.Instance.Mydata.Index).SetValueAsync(tempCount);
                    mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("TempoTraining").SetValueAsync(tempCount);
                }
                else
                {
                    mDatabaseRef.Child("TraingReport").Child("TempoTraining").Child(TKManager.Instance.Mydata.Index).SetValueAsync(trainingCount);
                    mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("TempoTraining").SetValueAsync(trainingCount);
                }
            }
        });
    }

    public void SetUserLocation(LocationInfo Position)
    {
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Lat").SetValueAsync(Position.latitude);
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("Lon").SetValueAsync(Position.longitude);
    }

    public void SetPurchaseItem(CommonData.PURCHASE_ITEM itemType, string Name, string Address, string Number, string Price)
    {
        CommonData._sPurchaseInfo purchaseInfo;

        purchaseInfo.PurchaseType = itemType;
        purchaseInfo.PurchaseDate = DateTime.Now.ToString("yyyy-MM-dd");
        purchaseInfo.PurchaseName = Name;
        purchaseInfo.PurchaseAddress = Address;
        purchaseInfo.PurchaseNumber = Number;
        purchaseInfo.PurchasePrice = Price;

        mDatabaseRef.Child("Purchase").Child(itemType.ToString()).Push().SetValueAsync(purchaseInfo);

        /*
        switch (itemType)
        {
            case CommonData.PURCHASE_ITEM.PURCHASE_MFS:
                mDatabaseRef.Child("Purchase").Child("MFS").Child("Lat").SetValueAsync(purchaseInfo);
                break;
            case CommonData.PURCHASE_ITEM.PURCHASE_COCONUT:
                break;
            case CommonData.PURCHASE_ITEM.PURCHASE_COFFEE:
                break;
        }
        */
    }

}
