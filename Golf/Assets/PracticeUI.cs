using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;

public class PracticeUI : MonoBehaviour
{
    public enum PRACTICE_ORDER
    { 
        READY,
        START,
        END
    }
    

    public GameObject Model;

    public GameObject Model_RotateLeft;
    public GameObject Model_BendUp;
    public GameObject Model_SideLeft;

    public GameObject Model_RotateRight;
    public GameObject Model_BendDown;
    public GameObject Model_SideRight;

    public UITrainingAngle BendTrainingAngle;
    public UITrainingAngle RotationTrainingAngle;
    public UITrainingAngle SideBendTrainingAngle;

    public GameObject ReadyUI;
    public Text ReadyCount;

    public Text TrainingType;
    public Text TrainingCount;
    public Text TrainingAngleType;

    public Button PauseButton;

    public Slider Timer;

    private float TrainingTime = 0f;
    private int TrainingSuccessCount = 0;

    private float ArrowDegreePerSecond = 100.0f;
    private float ArrowSpeed;

    private PRACTICE_ORDER PracticeOrderType = PRACTICE_ORDER.READY;
    private int[] RefData = { };
    private bool TrainingSuccess = false;
    private bool TrainingSuccess_Rotate = false;
    private bool TrainingSuccess_Bend = false;
    private bool TrainingSuccess_Side = false;

    private float[] GyroStatus = new float[3];

    private void Awake()
    {
        PauseButton.onClick.AddListener(OnClickBack);
    }

    // Start is called before the first frame update
    void Start()
    {
        TrainingSuccessCount = 0;
        PracticeOrderType = PRACTICE_ORDER.READY;

        TrainingCount.text = "0회";
        TrainingAngleType.text = "";

        if (TKManager.Instance.GetTrainingType() == CommonData.TRAINING_TYPE.TRAINING_TEMPO)
            TrainingType.text = CommonFunc.ConvertTrainingTypeStr(TKManager.Instance.GetTrainingType());
        else
        {
            TrainingType.text = CommonFunc.ConvertPoseTypeStr(TKManager.Instance.GetPoseType());

            StringBuilder builder = new StringBuilder();
            var angleTypeList = TKManager.Instance.GetAngleType();
            var enumerator = angleTypeList.GetEnumerator();
            int count = angleTypeList.Count;

            while (enumerator.MoveNext())
            {
                count--;
                if (enumerator.Current.Value == 0)
                    continue;
                builder.Append(string.Format("{0} {1}", CommonFunc.ConvertPoseAngleTypeStr(enumerator.Current.Key), CommonFunc.ConvertPoseLevelStr(enumerator.Current.Value)));
                if (count > 0)
                    builder.Append(",");
            }

            TrainingAngleType.text = builder.ToString();
        }

        if (TKManager.Instance.GetGender() == CommonData.GENDER.GENDER_MAN)
            RefData = CommonData.REF_MAN;
        else
            RefData = CommonData.REF_WOMAN;

        TrainingReady();

        if (TKManager.Instance.GetTrainingType() == CommonData.TRAINING_TYPE.TRAINING_TEMPO)                  
        {
            Model_BendDown.SetActive(false);
            Model_BendUp.SetActive(false);

            Model_RotateLeft.SetActive(false);
            Model_RotateRight.SetActive(false);

            Model_SideLeft.SetActive(false);
            Model_SideRight.SetActive(false);


            BendTrainingAngle.gameObject.SetActive(false);
            RotationTrainingAngle.gameObject.SetActive(false);
            SideBendTrainingAngle.gameObject.SetActive(false);
        }
        else
        {
            Model_BendDown.SetActive(true);
            Model_BendUp.SetActive(true);

            Model_RotateLeft.SetActive(true);
            Model_RotateRight.SetActive(true);

            Model_SideLeft.SetActive(true);
            Model_SideRight.SetActive(true);
        }

        

        int nTrainMode = Convert.ToInt32(TKManager.Instance.GetPoseType());

        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN);
            if (angleLevel > 0)
            {
                RotationTrainingAngle.gameObject.SetActive(true);
                float LevelCover = CommonData.LEVEL_COVER[angleLevel];

                float minValue = RefData[nTrainMode] - 30;
                float maxValue = RefData[nTrainMode + 1] + 30;
                float successMinValue = RefData[nTrainMode] - LevelCover;
                float successMaxValue = RefData[nTrainMode + 1] + LevelCover;
                RotationTrainingAngle.Init("ROTATION", minValue, maxValue, successMinValue, successMaxValue);

            }
            else
            {
                RotationTrainingAngle.gameObject.SetActive(false);
            }
        }


        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND);
            if (angleLevel > 0)
            {
                BendTrainingAngle.gameObject.SetActive(true);
                float LevelCover = CommonData.LEVEL_COVER[angleLevel];

                float minValue = RefData[nTrainMode + 2] - 30;
                float maxValue = RefData[nTrainMode + 3] + 30;
                float successMinValue = RefData[nTrainMode + 2] - LevelCover;
                float successMaxValue = RefData[nTrainMode + 3] + LevelCover;
                BendTrainingAngle.Init("BEND", minValue, maxValue, successMinValue, successMaxValue);
            }
            else
            {
                BendTrainingAngle.gameObject.SetActive(false);
            }
        }


        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE);

            if (angleLevel > 0)
            {
                SideBendTrainingAngle.gameObject.SetActive(true);
                float LevelCover = CommonData.LEVEL_COVER[angleLevel];

                float minValue = RefData[nTrainMode + 4] - 30;
                float maxValue = RefData[nTrainMode + 5] + 30;
                float successMinValue = RefData[nTrainMode + 4] - LevelCover;
                float successMaxValue = RefData[nTrainMode + 5] + LevelCover;
                SideBendTrainingAngle.Init("SIDE BEND", minValue, maxValue, successMinValue, successMaxValue);
            }
            else
            {
                SideBendTrainingAngle.gameObject.SetActive(false);
            }
        }    
    }

    public void TrainingReady()
    {
        if (TKManager.Instance.GetTrainingType() == CommonData.TRAINING_TYPE.TRAINING_POSE)
            StartCoroutine(Co_PoseTrainingReady());
        if (TKManager.Instance.GetTrainingType() == CommonData.TRAINING_TYPE.TRAINING_TEMPO)
            StartCoroutine(Co_TempoTrainingReady());
    }
    IEnumerator Co_PoseTrainingReady()
    {
        GyroScopeManager.Instance.TrainingReadyStart();

        yield return Co_TrainingReady();

        SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_START);
        GyroScopeManager.Instance.TrainingReadyEnd();
        PracticeOrderType = PRACTICE_ORDER.START;
        TrainingStart();
    }

    IEnumerator Co_TempoTrainingReady()
    {
        yield return Co_TrainingReady();
        PracticeOrderType = PRACTICE_ORDER.START;
        TrainingStart();
    }

    IEnumerator Co_TrainingReady()
    {
        ReadyUI.gameObject.SetActive(true);
        int readyTime = CommonData.TRAINING_READY_TIME;
        while (true)
        {
            ReadyCount.text = string.Format("{0}", readyTime);
            yield return new WaitForSeconds(1.0f);
            readyTime -= 1;

            if (readyTime <= 0)
                break;
        }
        ReadyUI.gameObject.SetActive(false);
    }

    public void TrainingStart()
    {
        if (TKManager.Instance.GetTrainingType() == CommonData.TRAINING_TYPE.TRAINING_POSE)
            StartCoroutine(Co_PoseTrainingStart());
        else if (TKManager.Instance.GetTrainingType() == CommonData.TRAINING_TYPE.TRAINING_TEMPO)
            StartCoroutine(Co_TempoTrainingStart());
    }

    IEnumerator Co_PoseTrainingStart()
    {
        TrainingTime = TKManager.Instance.GetTrainingTimer();
        float soundTime = 1f;
        float waitTime = CommonData.TRAINING_READY_TIME;

        while (true)
        {
            if(waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                if (TrainingSuccess)
                {
                    TrainingTime -= Time.deltaTime;
                    soundTime -= Time.deltaTime;
                    if (soundTime < 0)
                    {
                        SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_START);
                        soundTime = 1f;
                    }


                    if (TrainingTime < 0)
                    {
                        TrainingSuccessCount++;
                        TrainingTime = TKManager.Instance.GetTrainingTimer();
                        SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_SUCCESS);

                        TrainingCount.text = string.Format("{0}회", TrainingSuccessCount);

                        waitTime = CommonData.TRAINING_READY_TIME;
                    }
                }
                else
                    TrainingTime = TKManager.Instance.GetTrainingTimer();
            }
            

            yield return null;
        }
    }

    IEnumerator Co_TempoTrainingStart()
    {
        TrainingTime = CommonData.TEMPO_TRAINING_WAIT_TIME;

        while (true)
        {
            TrainingTime -= Time.deltaTime;
            yield return null;

            if(TrainingTime < 0)
            {
                SoundManager.Instance.PlayFXSound(CommonData.SOUND_TYPE.TRAINING_TEMPO);
                TrainingSuccessCount++;
                TrainingTime = CommonData.TEMPO_TRAINING_WAIT_TIME;
            }
        }
    }

    void Update()
    {
        if (PracticeOrderType != PRACTICE_ORDER.START)
            return;

        if (TKManager.Instance.GetTrainingType() == CommonData.TRAINING_TYPE.TRAINING_POSE)
        {
            GyroStatus = PracticeManager.Instance.GetGyroStatus();
            ArrowSpeed = ArrowDegreePerSecond * Time.deltaTime;

            UpdateGyroStatus();
        }

        if (TKManager.Instance.GetTrainingType() == CommonData.TRAINING_TYPE.TRAINING_POSE)
            Timer.value = TrainingTime / TKManager.Instance.GetTrainingTimer();
        else if (TKManager.Instance.GetTrainingType() == CommonData.TRAINING_TYPE.TRAINING_TEMPO)
            Timer.value = CommonData.TEMPO_TRAINING_WAIT_TIME;
    }

    public void UpdateGyroStatus()
    {
        int nTrainMode = Convert.ToInt32(TKManager.Instance.GetPoseType());

        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN);
            if (angleLevel > 0)
            {
                float LevelCover = CommonData.LEVEL_COVER[angleLevel];

                if (RefData[nTrainMode] - LevelCover <= GyroStatus[2] && GyroStatus[2] <= RefData[nTrainMode + 1] + LevelCover)
                {
                    Model_RotateLeft.SetActive(false);
                    Model_RotateRight.SetActive(false);

                    TrainingSuccess_Rotate = true;
                }
                else if (GyroStatus[2] < RefData[nTrainMode] - LevelCover)
                {
                    Model_RotateRight.SetActive(true);
                    Model_RotateLeft.SetActive(false);

                    Model_RotateRight.transform.Rotate(0, 0, -1 * ArrowSpeed);
                    TrainingSuccess_Rotate = false;
                }

                else if (RefData[nTrainMode] - LevelCover < GyroStatus[2])
                {
                    Model_RotateLeft.SetActive(true);
                    Model_RotateRight.SetActive(false);
                    Model_RotateLeft.transform.Rotate(0, 0, ArrowSpeed);
                    TrainingSuccess_Rotate = false;
                }

                RotationTrainingAngle.SetAngle(GyroStatus[2]);
            }
            else
            {
                Model_RotateLeft.SetActive(false);
                Model_RotateRight.SetActive(false);

                TrainingSuccess_Rotate = true;
            }
        }


        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND);
            if (angleLevel > 0)
            {
                float LevelCover = CommonData.LEVEL_COVER[angleLevel];
                if (RefData[nTrainMode + 2] - LevelCover <= GyroStatus[1] && GyroStatus[1] <= RefData[nTrainMode + 3] + LevelCover)
                {
                    Model_BendDown.SetActive(false);
                    Model_BendUp.SetActive(false);

                    TrainingSuccess_Bend = true;
                }
                else if (GyroStatus[1] < RefData[nTrainMode + 2] - LevelCover)
                {
                    Model_BendDown.SetActive(true);
                    Model_BendUp.SetActive(false);

                    Model_BendDown.transform.Rotate(ArrowSpeed, 0, 0);
                    TrainingSuccess_Bend = false;
                }

                else if (RefData[nTrainMode + 3] - LevelCover < GyroStatus[1])
                {
                    Model_BendUp.SetActive(true);
                    Model_BendDown.SetActive(false);
                    Model_BendUp.transform.Rotate(-1 * ArrowSpeed, 0, 0);
                    TrainingSuccess_Bend = false;
                }

                BendTrainingAngle.SetAngle(GyroStatus[1]);
            }
            else
            {
                TrainingSuccess_Bend = true;

                Model_BendDown.SetActive(false);
                Model_BendUp.SetActive(false);
            }
        }


        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE);

            if (angleLevel > 0)
            {
                float LevelCover = CommonData.LEVEL_COVER[angleLevel];
                if (RefData[nTrainMode + 4] - LevelCover <= GyroStatus[0] && GyroStatus[0] <= RefData[nTrainMode + 5] + LevelCover)
                {

                    Model_SideLeft.SetActive(false);
                    Model_SideRight.SetActive(false);
                    TrainingSuccess_Side = true;
                }
                else if (GyroStatus[0] < RefData[nTrainMode + 4] - LevelCover)
                {
                    Model_SideRight.SetActive(true);
                    Model_SideLeft.SetActive(false);

                    Model_SideRight.transform.Rotate(0, 0, -1 * ArrowSpeed);
                    TrainingSuccess_Side = false;
                }

                else if (RefData[nTrainMode + 5] - LevelCover < GyroStatus[0])
                {
                    Model_SideLeft.SetActive(true);
                    Model_SideRight.SetActive(false);
                    Model_SideLeft.transform.Rotate(0, 0, ArrowSpeed);
                    TrainingSuccess_Side = false;
                }

                SideBendTrainingAngle.SetAngle(GyroStatus[0]);
            }
            else
            {
                TrainingSuccess_Side = true;
                Model_SideLeft.SetActive(false);
                Model_SideRight.SetActive(false);
            }
        }

        TrainingSuccess = TrainingSuccess_Side && TrainingSuccess_Bend && TrainingSuccess_Rotate;
    }

    public void OnClickBack()
    {
        PracticeOrderType = PRACTICE_ORDER.END;
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("훈련을 종료 하시겠습니까?", () =>
         {
             PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.SELF_EVALUATION, new PopupSelfEvaluation.PopupData(TrainingSuccessCount, () =>
             {
                 GyroScopeManager.Instance.DestroyGyro();
                 StopAllCoroutines();
                 SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
             }));
         }));
    }


//    // Update is called once per frame
    

//    public void SetTrainingCount()
//    {
//        StartCoroutine(Co_TrainingCount());
//    }

//    IEnumerator Co_TrainingCount()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(7.5f);
//            nTrainingCount++;
//       //     txtCount.text = nTrainingCount.ToString() + "회";
//        }
//        yield return null;
//    }

//    public void SetTrainingTimer()
//    {
//        StartCoroutine(Co_TrainingTimer());
//    }

//    IEnumerator Co_TrainingTimer()
//    {
//        int tempTimer = 0;
//        if (TKManager.Instance.bTempoTraining)
//        {
         
//            while (true)
//            {
//                yield return new WaitForSeconds(1.0f);

//                tempTimer++;

//                if (tempTimer > 7)
//                {
//                    tempTimer = 0;
//            //        txtTempoTimer.text = tempTimer.ToString();
//                }

//            }
//        }
//        else
//        {
//            yield return new WaitForSeconds(3.0f);
//            tempTimer = TKManager.Instance.GetTrainingTimer();

//            while (true)
//            {
//                yield return new WaitForSeconds(1.0f);

//                tempTimer--;

//                if (tempTimer < 0)
//                {
//                    nTrainingCount++;
//               //     txtCount.text = nTrainingCount.ToString() + "회";
//                    tempTimer = TKManager.Instance.GetTrainingTimer();
//                }

////
//     //           txtTimer.text = tempTimer.ToString() + "초";
//            }
//        }

  
//        yield return null;
//    }

//    public void CheckGyroStatus()
//    {
//        if(TKManager.Instance.GetGender() == CommonData.GENDER.GENDER_MAN)
//        {
//            CheckGyroStatus_Man();
//            txtMode.text = "(" + Model.transform.rotation.x.ToString() + " , " + Model.transform.rotation.y.ToString() + " , " + Model.transform.rotation.z.ToString() + " ) ";
//        }
//        else
//        {
//            CheckGyroStatus_Woman();
//        }
        
//    }

    


//    public void SuccessTraining()
//    {        
//        if(TKManager.Instance.bTrainingSuccess == false)
//        {
//            if (TrainingSuccess_Rotate && TrainingSuccess_Bend && TrainingSuccess_Side)
//            {
//                TKManager.Instance.bTrainingSuccess = true;
//                SoundManager.Instance.PlaySuccessSound();             
//            }       
//        }
//    }
}
