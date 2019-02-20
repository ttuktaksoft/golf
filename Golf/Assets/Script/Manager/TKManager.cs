using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

    private CommonData.GENDER Gender;
    public string Name { get; private set; }
    private CommonData.GRADE_TYPE Grade = CommonData.GRADE_TYPE.BRONZE;
    public string PhoneNumber { get; private set; }
    public CommonData.TRAINING_TYPE TrainingType;
    public CommonData.TRAINING_POSE PoseType;
    public Dictionary<CommonData.TRAINING_ANGLE, int> AngleTypeList = new Dictionary<CommonData.TRAINING_ANGLE, int>();
    public Gyroscope gyro;
    public int TempoTrainingLevel = 3;
    public Sprite ThumbnailSprite = null;
    public string ThumbnailSpritePath = "";


    private SaveData MySaveData = new SaveData();
    public bool MyLoadData = false;
    private int TrainingTimer;
    // Start is called before the first frame update
    void Start()
    {
        Gender = CommonData.GENDER.GENDER_MAN;
        TrainingType = CommonData.TRAINING_TYPE.TRAINING_POSE;
        PoseType = CommonData.TRAINING_POSE.TRAINING_ADDRESS;
        gyro = Input.gyro;
        gyro.enabled = false;
        DontDestroyOnLoad(this);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 50;
#if UNITY_ANDROID || UNITY_EDITOR
        Screen.SetResolution((int)Screen.safeArea.width, ((int)Screen.safeArea.width * 16) / 9, false);
#endif
        //Screen.SetResolution((int)Screen.safeArea.width, ((int)Screen.safeArea.width * 16) / 9, false);
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

    public void SetTrainingTimer(int Timer)
    {
        TrainingTimer = Timer;
    }

    public int GetTrainingTimer()
    {
        return TrainingTimer;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetPhoneNumber(string num)
    {
        PhoneNumber = num;
    }

    public CommonData.GRADE_TYPE GetGrade()
    {
        return Grade;
    }

    public void SetGrade(CommonData.GRADE_TYPE type)
    {
        Grade = type;
    }

    public string GetGradeStr()
    {
        return CommonFunc.ConvertGradeStr(Grade);
    }














    [System.Serializable]
    public class SaveData
    {
        public CommonData.GENDER Gender = CommonData.GENDER.GENDER_MAN;
        public string Name = "";
        public CommonData.GRADE_TYPE Grade = CommonData.GRADE_TYPE.BRONZE;
        public string PhoneNumber = "";
        public List<EvaluationData> EvaluationDataList = new List<EvaluationData>();
        public string ThumbnailSpritePath = "";

        public void Save()
        {
            Gender = TKManager.Instance.GetGender();
            Name = TKManager.Instance.Name;
            Grade = TKManager.Instance.GetGrade();
            PhoneNumber = TKManager.Instance.PhoneNumber;
            EvaluationDataList = DataManager.Instance.EvaluationDataList;
            ThumbnailSpritePath = TKManager.Instance.ThumbnailSpritePath;
        }

        public void Load()
        {
            TKManager.Instance.SetGender(Gender);
            TKManager.Instance.Name = Name;
            TKManager.Instance.SetGrade(Grade);
            TKManager.Instance.PhoneNumber = PhoneNumber;

            if (EvaluationDataList == null)
                DataManager.Instance.EvaluationDataList = new List<EvaluationData>();
            else
                DataManager.Instance.EvaluationDataList = EvaluationDataList;

            if (ThumbnailSpritePath != "")
            {
                Texture2D texture = NativeGallery.LoadImageAtPath(ThumbnailSpritePath);
                if (texture == null)
                {
                    Debug.Log("!!!!!!Couldn't load texture from " + ThumbnailSpritePath);
                    return;
                }
                TKManager.Instance.ThumbnailSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f,0.5f));
            }
            else
                TKManager.Instance.ThumbnailSprite = null;
        }
    }



    public void SaveFile()
    {
        MySaveData.Save();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = pathForDocumentsFile("PlayerData.ini");
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, MySaveData);
        stream.Close();
    }

    public void LoadFile()
    {
        string path = pathForDocumentsFile("PlayerData.ini");
        FileInfo fileInfo = new FileInfo(path);
        if (fileInfo.Exists)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            MySaveData = (SaveData)formatter.Deserialize(stream);
            stream.Close();
            MySaveData.Load();
            MyLoadData = true;
        }
        else
        {
            SaveFile();
        }
    }

    public string pathForDocumentsFile(string filename)
    {
#if UNITY_EDITOR
        string path_pc = Application.dataPath;
        path_pc = path_pc.Substring(0, path_pc.LastIndexOf('/'));
        return Path.Combine(path_pc, filename);
#elif UNITY_ANDROID
        string path = Application.persistentDataPath;
        path = path.Substring(0, path.LastIndexOf('/'));
        return Path.Combine(path, filename);
#elif UNITY_IOS
        return Application.persistentDataPath + "/" + filename;
        //string path = Application.dataPath.Substring(0, Application.dataPath.Length);
        //path = path.Substring(0, path.LastIndexOf('/'));
        //return Path.Combine(Path.Combine(path, "Documents"), filename);
#endif
    }
}
