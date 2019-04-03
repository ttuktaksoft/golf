using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase;
using Firebase.Database;
using Firebase.Storage;
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

    public bool NickNameExist = false;
    public bool RegisterUserProgress = false;

    private bool FirebaseProgress = false;
    private Action FirebaseProgressEndAction = null;

    public void SetFirebaseProgressEndAction(Action action)
    {
        FirebaseProgress = true;
        FirebaseProgressEndAction = action;
        StartCoroutine(Co_FirebaseProgressEndAction());
    }

    private IEnumerator Co_FirebaseProgressEndAction()
    {
        TKManager.Instance.ShowHUD();
        while (true)
        {
            if (FirebaseProgress == false)
                break;

            yield return null;
        }

        TKManager.Instance.HideHUD();

        if (FirebaseProgressEndAction != null)
            FirebaseProgressEndAction();
    }

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
        GetTutorialData();
        GetAcademyData();
        GetMarketData();
        GetAlarmData();
    }

    private void AddFirstLoadingComplete()
    {
        if (FirstLoadingComplete == false)
            LoadingCount++;

        if (LoadingCount == 5)
            FirstLoadingComplete = true;
    }

    // 유져 인덱스 받아오기
    public void RegisterUserByFirebase()
    {
        RegisterUserProgress = true;
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

                RegisterUserProgress = false;
            }

            return TransactionResult.Success(mutableData);
        });
    }


    // 사용자 정보 파이어베이스에서 로드
    public void GetUserData()
    {
        string userIdx = TKManager.Instance.Mydata.Index;

        Debug.Log("GetUserData Start");
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
                    //string tempThumbNail = tempData["ThumbNail"].ToString();

                    TKManager.Instance.Mydata.Init((CommonData.GENDER)tempGender, userIdx, tempName, null, tempSeasonPoint, tempAccumPoint, "");

                    if (tempData.ContainsKey("UserCode") == false)
                        SetRecommenderCode();
                    else
                    {
                        string tempUserCode = tempData["UserCode"].ToString();
                        TKManager.Instance.Mydata.UserCode = tempUserCode;
                    }
                }

                Debug.Log("GetUserData End");
                AddFirstLoadingComplete();
                //  Debug.LogFormat("UserInfo: Index : {0} NickName {1} Point {2}", TKManager.Instance.MyData.Index, TKManager.Instance.MyData.NickName, TKManager.Instance.MyData.Point);
                }
        });
    }

    // 튜토리얼 정보 파이어베이스에서 로드
    public void GetTutorialData()
    {
        Debug.Log("GetTutorialData Start");
        mDatabaseRef.Child("Tutorial").OrderByKey().LimitToLast(4)
       .GetValueAsync().ContinueWith(task =>
       {
           if (task.IsFaulted)
           {
               // Handle the error...
           }
           else if (task.IsCompleted)
           {
               DataSnapshot snapshot = task.Result;
               if (snapshot != null && snapshot.Exists)
               {
                   foreach (var tempChild in snapshot.Children)
                   {
                       int tempIndex = Convert.ToInt32(tempChild.Key);                       
                       var tempData = tempChild.Value as Dictionary<string, object>;
                       var tempTitle = tempData["Title"].ToString();
                       var tempType = tempData["Type"].ToString();
                       var tempContent = tempData["Content"].ToString();

                       TutorialData tutorialData = new TutorialData(tempTitle, tempContent);
                       DataManager.Instance.AddTutorialData(tempType, tutorialData);
                   }
               }
               else
               {
                    
               }
               Debug.Log("GetTutorialData End");
               AddFirstLoadingComplete();
           }
       }
      );
    }

    // 아카데미 정보 파이어베이스에서 로드
    public void GetAcademyData()
    {
        Debug.Log("GetAcademyData Start");
        mDatabaseRef.Child("Academy").GetValueAsync().ContinueWith(task =>
       {
           if (task.IsFaulted)
           {
               // Handle the error...
           }
           else if (task.IsCompleted)
           {
               DataSnapshot snapshot = task.Result;
               if (snapshot != null && snapshot.Exists)
               {
                   DataManager.Instance.AcademyURL = snapshot.Value.ToString();                   
               }
               else
               {

               }
               Debug.Log("GetAcademyData End");
               AddFirstLoadingComplete();
           }
       }
      );
    }

    // 마켓 정보 파이어베이스에서 로드
    public void GetMarketData()
    {
        Debug.Log("GetMarketData Start");
#if (UNITY_ANDROID || UNITY_EDITOR)
        string dataKey = "MarketAOS";
#elif UNITY_IOS
        string dataKey = "MarketIOS";
#endif
        mDatabaseRef.Child(dataKey).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot != null && snapshot.Exists)
                {
                    DataManager.Instance.MarketURL = snapshot.Value.ToString();
                }
                else
                {

                }
                Debug.Log("GetMarketData End");
                AddFirstLoadingComplete();
            }
        }
      );
    }

    // 커피 기프티콘 정보 파이어베이스에서 로드
    public void GetGiftData()
    {
        mDatabaseRef.Child("Gifticon").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot != null && snapshot.Exists)
                {
                    foreach (var tempChild in snapshot.Children)
                    {
                        int tempIndex = Convert.ToInt32(tempChild.Key);
                        var tempData = tempChild.Value as Dictionary<string, object>;
                        var tempThumb = tempData["Thumb"].ToString();
                        var tempContent = tempData["Content"].ToString();
                    }                    
                }
                else
                {

                }

                AddFirstLoadingComplete();
            }
        }
      );
    }

    // 리워드 정보 파이어베이스에서 로드
    public void GetRewardData()
    {
        mDatabaseRef.Child("Reward").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot != null && snapshot.Exists)
                {
                    foreach (var tempChild in snapshot.Children)
                    {
                        int tempIndex = Convert.ToInt32(tempChild.Key);
                        var tempData = tempChild.Value as Dictionary<string, object>;
                        var tempThumb = tempData["Thumb"].ToString();
                        var tempTitle = tempData["Title"].ToString();
                        var tempType = tempData["Type"].ToString();
                        var tempContent = tempData["Content"].ToString();
                    }
                }
                else
                {

                }

                AddFirstLoadingComplete();
            }
        }
      );
    }


    // 기타알림 정보 파이어베이스에서 로드
    public void GetAlarmData()
    {
        Debug.Log("GetAlarmData Start");
        mDatabaseRef.Child("Alarm").OrderByKey().LimitToLast(4)
       .GetValueAsync().ContinueWith(task =>
       {
           if (task.IsFaulted)
           {
               // Handle the error...
           }
           else if (task.IsCompleted)
           {
               DataSnapshot snapshot = task.Result;
               if (snapshot != null && snapshot.Exists)
               {
                   foreach (var tempChild in snapshot.Children)
                   {
                       int tempIndex = Convert.ToInt32(tempChild.Key);
                       var tempData = tempChild.Value as Dictionary<string, object>;
                       var tempTitle = tempData["Title"].ToString();
                       var tempContent = tempData["Content"].ToString();
                       var tempDate = tempData["Date"].ToString();
                       DateTime date = new DateTime();

                       if (DateTime.TryParseExact(tempDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out date))
                       {
                           AlarmData alarmData = new AlarmData(tempTitle, date, tempContent);
                           DataManager.Instance.AddAlarmData(alarmData);
                           TextureCacheManager.Instance.AddLoadImageURL(tempContent);
                       }
                       else
                       {
                           Debug.LogFormat("GetAlarmData 날짜 형식이 맞지 않습니다 {0}", tempDate);
                       }
                   }
               }
               else
               {

               }
               Debug.Log("GetAlarmData End");
               AddFirstLoadingComplete();
           }
       }
      );
    }

    // 닉네임 중복 체크
    public void IsExistNickName(string NickName, Action endAction)
    {
        SetFirebaseProgressEndAction(endAction);

        mDatabaseRef.Child("UserNameList").Child(NickName).GetValueAsync().ContinueWith(task =>
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
                    NickNameExist = true;
                }
                else
                {
                    NickNameExist = false;
                }
                //  Debug.LogFormat("UserInfo: Index : {0} NickName {1} Point {2}", TKManager.Instance.MyData.Index, TKManager.Instance.MyData.NickName, TKManager.Instance.MyData.Point);
            }
        });
    }

    // 사용자 정보 파이어베이스에 세팅
    public void SetUserData()
    {
        mDatabaseRef.Child("UserNameList").Child(TKManager.Instance.Mydata.Name).SetValueAsync(TKManager.Instance.Mydata.Index);

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

    // 사용자 썸네일 세팅
    public void SetThumbNail(Texture2D ThumbTexture)
    {
 
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
 
    public void SetRecommenderCode()
    {
        char[] stringChars = new char[4];
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        var random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        String tempUserCode = new String(stringChars);
        tempUserCode += TKManager.Instance.Mydata.Index;
        TKManager.Instance.Mydata.UserCode = tempUserCode;

        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("UserCode").SetValueAsync(tempUserCode);
        mDatabaseRef.Child("UserCode").Child(tempUserCode).SetValueAsync(TKManager.Instance.Mydata.Index);
    }

    public void AddFreind(string userCode, Action<bool> endAction)
    {
        TKManager.Instance.ShowHUD();
        mDatabaseRef.Child("UserCode").Child(userCode).GetValueAsync().ContinueWith(task =>
        {

            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                TKManager.Instance.HideHUD();
                if (snapshot.Exists || snapshot.Value != null)
                {
                    var tempIndex = snapshot.Value.ToString();                    
                    mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("FriendList").Push().SetValueAsync(tempIndex);
                    endAction(true);
                }
                else
                {
                    // 코드 잘못 쳤을 경우
                    endAction(false);
                }
            }
        });
    }

    public void GetFreindList()
    {        
        mDatabaseRef.Child("Users").Child(TKManager.Instance.Mydata.Index).Child("FriendList").GetValueAsync().ContinueWith(task =>
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
                    foreach (var tempChild in snapshot.Children)
                    {
                        Debug.Log(tempChild.Value.ToString());
                        GetFreindInfo(tempChild.Value.ToString());
                    }

                    
                    

                }
                else
                {

                }
            }
        });
    }

    public void GetFreindInfo(string freindIndex)
    {
        mDatabaseRef.Child("Users").Child(freindIndex).GetValueAsync().ContinueWith(task =>
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

                    var tempData = snapshot.Value as Dictionary<string, object>;

                    string tempName = tempData["Name"].ToString();
                    var tempGender = Convert.ToInt32(tempData["Gender"]);                    
                    int tempAccumPoint = Convert.ToInt32(tempData["AccumPoint"]);
                    int tempSeasonPoint = Convert.ToInt32(tempData["SeasonPoint"]);
                    int tempThumbNail = Convert.ToInt32(tempData["ThumbNail"]);
                    
                }
                else
                {
                    
                }
            }
        });
    }


}
