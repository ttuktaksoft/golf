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

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void init()
    {

    }
}