using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void init()
    {

    }

    public void GetData()
    {
        GetUserData();
    }

    private void AddFirstLoadingComplete()
    {
        if (FirstLoadingComplete == false)
            LoadingCount++;

        if (LoadingCount == 14)
            FirstLoadingComplete = true;
    }

    public void GetUserData()
    {
        AddFirstLoadingComplete();
    }

    public void SetUserData()
    {
        // TODO 파베에 UserData 세팅
    }

    public void SetSeasonPoint()
    {

    }
}