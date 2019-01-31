using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKManager : MonoBehaviour
{

    public static TKManager _instance = null;
    public static TKManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TKManager>() as TKManager;
            }
            return _instance;
        }
    }

    public CommonData.GENDER Gender;
    public CommonData.TRAINING_MODE Mode;

    // Start is called before the first frame update
    void Start()
    {
        Gender = CommonData.GENDER.GENDER_MAN;
        Mode = CommonData.TRAINING_MODE.TRAINING_ADDRESS;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetGender(CommonData.GENDER Gender)
    {
        this.Gender = Gender;
    }
    public CommonData.GENDER GetGender()
    {
        return this.Gender;
    }

    public void SetMode(CommonData.TRAINING_MODE Mode)
    {
        this.Mode = Mode;
    }
    public CommonData.TRAINING_MODE GetMode()
    {
        return this.Mode;
    }
}
