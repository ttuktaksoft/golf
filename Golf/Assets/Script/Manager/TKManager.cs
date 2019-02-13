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
    public CommonData.TRAINING_TYPE TrainingType;
    public CommonData.TRAINING_POSE PoseType;
    public Dictionary<CommonData.TRAINING_ANGLE, int> AngleTypeList = new Dictionary<CommonData.TRAINING_ANGLE, int>();
    public Gyroscope gyro;

    public bool bTrainingSuccess = false;
    // Start is called before the first frame update
    void Start()
    {
        Gender = CommonData.GENDER.GENDER_MAN;
        TrainingType = CommonData.TRAINING_TYPE.TRAINING_POSE;
        PoseType = CommonData.TRAINING_POSE.TRAINING_ADDRESS;
        AngleTypeList.Add(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE, 1);
        gyro = Input.gyro;
        gyro.enabled = false;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown("space"))
        {
            UnityEngine.ScreenCapture.CaptureScreenshot("shot.png");
        }
#endif
    }

    public void SetGender(CommonData.GENDER Gender)
    {
        this.Gender = Gender;
    }
    public CommonData.GENDER GetGender()
    {
        return this.Gender;
    }

    public void SetTrainingType(CommonData.TRAINING_TYPE type)
    {
        this.TrainingType = type;
    }

    public CommonData.TRAINING_TYPE GetTrainingType()
    {
        return this.TrainingType;
    }

    public void SetMode(CommonData.TRAINING_POSE type)
    {
        SetTrainingType(CommonData.TRAINING_TYPE.TRAINING_POSE);
        this.PoseType = type;
    }
    public CommonData.TRAINING_POSE GetPoseType()
    {
        return this.PoseType;
    }

    public void SetAngleType(Dictionary<CommonData.TRAINING_ANGLE, int> dic)
    {
        AngleTypeList.Clear();
        AngleTypeList = dic;
    }

    public Dictionary<CommonData.TRAINING_ANGLE, int> GetAngleType()
    {
        return AngleTypeList;
    }

    public bool IsAngleType(CommonData.TRAINING_ANGLE type)
    {
        return AngleTypeList.ContainsKey(type);
    }

    public int GetAngleLevel(CommonData.TRAINING_ANGLE type)
    {
        if (IsAngleType(type) == false)
            return 0;

        return AngleTypeList[type];
    }
}
