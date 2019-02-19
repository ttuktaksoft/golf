using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PracticeUI : MonoBehaviour
{

    private float degreePerSecond = 100.0f;
    private float speed;

    //public GameObject PoseContent;
    //public GameObject PoseModel;

    public GameObject Model;

    public GameObject Model_RotateLeft;
    public GameObject Model_BendUp;
    public GameObject Model_SideLeft;

    public GameObject Model_RotateRight;
    public GameObject Model_BendDown;
    public GameObject Model_SideRight;

    //public GameObject TempoContent;

    //   public Text txtTempoTimer;
    //public Text txtRotateLevel;
    //public Text txtRotateMin;
    //public Text txtRotateMax;
    //public Transform InnerSliderRotate;
    //public Slider SliderRotate;

    //public Text txtBendLevel;
    //public Text txtBendMin;
    //public Text txtBendMax;
    //public Transform InnerSliderBend;
    //public Slider SliderBend;

    //public Text txtSideLevel;
    //public Text txtSideMin;
    //public Text txtSideMax;
    //public Transform InnerSliderSide;
    //public Slider SliderSide;

    public UITrainingAngle BendTrainingAngle;
    public UITrainingAngle RotationTrainingAngle;
    public UITrainingAngle SideBendTrainingAngle;


    public Text txtMode;
    public Text txtCount;
    public Text txtLevel;

    //  public Text txtTimer;
    //   public Text txtCount;
    public int nTrainingCount = 0;
    public int nTrainingTimer = 0;

    //public Button btnInit;
    public Button btnBack;

    float[] tempStatus = new float[3];
    bool bStatus = false;

    public bool bTraining = false;

    // Start is called before the first frame update
    void Start()
    {
        string tempString = TKManager.Instance.GetPoseType().ToString();
        tempString = tempString.Substring(9);
        txtMode.enabled = true;

    


        SetTrainingTimer();

        if (TKManager.Instance.GetTrainingType() == CommonData.TRAINING_TYPE.TRAINING_TEMPO)                  
        {
            Model_BendDown.SetActive(false);
            Model_BendUp.SetActive(false);

            Model_RotateLeft.SetActive(false);
            Model_RotateRight.SetActive(false);

            Model_SideLeft.SetActive(false);
            Model_SideRight.SetActive(false);


         //   PoseContent.SetActive(false);
          //  PoseModel.SetActive(false);

          //  TempoContent.SetActive(true);

            txtMode.enabled = false;
          //  txtTimer.enabled = false;
            TKManager.Instance.bTempoTraining = true;
            SoundManager.Instance.PlayTempoTraining();
            SetTrainingCount();
         
        }
        else
        {
            Model_BendDown.SetActive(true);
            Model_BendUp.SetActive(true);

            Model_RotateLeft.SetActive(true);
            Model_RotateRight.SetActive(true);

            Model_SideLeft.SetActive(true);
            Model_SideRight.SetActive(true);


            //    PoseContent.SetActive(true);
            //     PoseModel.SetActive(true);

            //    TempoContent.SetActive(false);

            //     txtTimer.enabled = true;
            //txtMode.text = tempString;
        }

        btnBack.onClick.AddListener(OnClickBack);

        int nTrainMode = Convert.ToInt32(TKManager.Instance.GetPoseType());

        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN);
            if (angleLevel > 0)
            {
                RotationTrainingAngle.gameObject.SetActive(true);
                float LevelCover = CommonData.LEVEL_COVER[angleLevel];

                float minValue = CommonData.REF_MAN[nTrainMode] - 30;
                float maxValue = CommonData.REF_MAN[nTrainMode + 1] + 30;
                float successMinValue = CommonData.REF_MAN[nTrainMode] - LevelCover;
                float successMaxValue = CommonData.REF_MAN[nTrainMode + 1] + LevelCover;
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

                float minValue = CommonData.REF_MAN[nTrainMode + 2] - 30;
                float maxValue = CommonData.REF_MAN[nTrainMode + 3] + 30;
                float successMinValue = CommonData.REF_MAN[nTrainMode + 2] - LevelCover;
                float successMaxValue = CommonData.REF_MAN[nTrainMode + 3] + LevelCover;
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

                float minValue = CommonData.REF_MAN[nTrainMode + 4] - 30;
                float maxValue = CommonData.REF_MAN[nTrainMode + 5] + 30;
                float successMinValue = CommonData.REF_MAN[nTrainMode + 4] - LevelCover;
                float successMaxValue = CommonData.REF_MAN[nTrainMode + 5] + LevelCover;
                SideBendTrainingAngle.Init("SIDE BEND", minValue, maxValue, successMinValue, successMaxValue);
            }
            else
            {
                SideBendTrainingAngle.gameObject.SetActive(false);
            }
        }


        
   //     txtTimer.text = TKManager.Instance.GetTrainingTimer().ToString() + "초";


        //     
    }

    public void OnClickInit()
    {
        //isModelActive = !isModelActive;
        if (TKManager.Instance.GetTrainingType() != CommonData.TRAINING_TYPE.TRAINING_TEMPO)
            GyroScopeManager.Instance.Init(true);
        else
        {
            TKManager.Instance.bTempoTraining = true;
            SoundManager.Instance.PlayTempoTraining();
        }



        //PracticeManager.Instance.SetCalibration();
    }

    public void OnClickBack()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.MSG, new PopupMsg.PopupData("훈련을 종료 하시겠습니까?", () =>
         {
             TKManager.Instance.bPoseTraining = false;
             TKManager.Instance.bTempoTraining = false;

             GyroScopeManager.Instance.DestroyGyro();
             StopCoroutine(Co_TrainingCount());
             StopCoroutine(Co_TrainingTimer());
             SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
         }));
    }


    // Update is called once per frame
    void Update()
    {        
        if (TKManager.Instance.GetTrainingType() != CommonData.TRAINING_TYPE.TRAINING_TEMPO)
        {
            tempStatus = PracticeManager.Instance.GetGyroStatus();
            //txtTurn.text = "tempStatus : " + tempStatus[0];
         //   Debug.Log("tempStatus : " + tempStatus[0]);
            CheckGyroStatus();
            SuccessTraining();

            speed = degreePerSecond * Time.deltaTime;
            
        }
    }

    public void SetTrainingCount()
    {
        StartCoroutine(Co_TrainingCount());
    }

    IEnumerator Co_TrainingCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(7.5f);
            nTrainingCount++;
       //     txtCount.text = nTrainingCount.ToString() + "회";
        }
        yield return null;
    }

    public void SetTrainingTimer()
    {
        StartCoroutine(Co_TrainingTimer());
    }

    IEnumerator Co_TrainingTimer()
    {
        int tempTimer = 0;
        if (TKManager.Instance.bTempoTraining)
        {
         
            while (true)
            {
                yield return new WaitForSeconds(1.0f);

                tempTimer++;

                if (tempTimer > 7)
                {
                    tempTimer = 0;
            //        txtTempoTimer.text = tempTimer.ToString();
                }

            }
        }
        else
        {
            yield return new WaitForSeconds(3.0f);
            tempTimer = TKManager.Instance.GetTrainingTimer();

            while (true)
            {
                yield return new WaitForSeconds(1.0f);

                tempTimer--;

                if (tempTimer < 0)
                {
                    nTrainingCount++;
               //     txtCount.text = nTrainingCount.ToString() + "회";
                    tempTimer = TKManager.Instance.GetTrainingTimer();
                }

//
     //           txtTimer.text = tempTimer.ToString() + "초";
            }
        }

  
        yield return null;
    }

    public void CheckGyroStatus()
    {
        if(TKManager.Instance.GetGender() == CommonData.GENDER.GENDER_MAN)
        {
            CheckGyroStatus_Man();
            txtMode.text = "(" + Model.transform.rotation.x.ToString() + " , " + Model.transform.rotation.y.ToString() + " , " + Model.transform.rotation.z.ToString() + " ) ";
        }
        else
        {
            CheckGyroStatus_Woman();
        }
        
    }

    int nTrainingSuccess = 0;
    bool bTrainingSuccess_Rotate = false;
    bool bTrainingSuccess_Bend = false;
    bool bTrainingSuccess_Side = false;


    public void SuccessTraining()
    {        
        if(TKManager.Instance.bTrainingSuccess == false)
        {
            if (bTrainingSuccess_Rotate && bTrainingSuccess_Bend && bTrainingSuccess_Side)
            {
                TKManager.Instance.bTrainingSuccess = true;
                SoundManager.Instance.PlaySuccessSound();             
            }       
        }
    }

    public void CheckGyroStatus_Man()
    {
        int nTrainMode = Convert.ToInt32(TKManager.Instance.GetPoseType());

        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN))
        {
            
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN);
            if(angleLevel > 0)
            {
                float LevelCover = CommonData.LEVEL_COVER[angleLevel];

                if (CommonData.REF_MAN[nTrainMode] - LevelCover <= tempStatus[2] && tempStatus[2] <= CommonData.REF_MAN[nTrainMode + 1] + LevelCover)
                {
                    Model_RotateLeft.SetActive(false);
                    Model_RotateRight.SetActive(false);

                    bTrainingSuccess_Rotate = true;
                }
                else if(CommonData.REF_MAN[nTrainMode] - LevelCover < tempStatus[2] )
                {
                    Model_RotateRight.SetActive(true);
                    Model_RotateLeft.SetActive(false);

                    Model_RotateRight.transform.Rotate(0, 0, -1 * speed);
                    bTrainingSuccess_Rotate = false;
                    //txtTurn.text = "TURN : " + (int)tempStatus[2] + "(" + (CommonData.REF_MAN[nTrainMode] - LevelCover) + " to " + (CommonData.REF_MAN[nTrainMode + 1] + LevelCover) + ")";
                }

                else if (CommonData.REF_MAN[nTrainMode] - LevelCover > tempStatus[2])
                {
                    Model_RotateLeft.SetActive(true);
                    Model_RotateRight.SetActive(false);
                    Model_RotateLeft.transform.Rotate(0, 0, speed);
                    bTrainingSuccess_Rotate = false;
                    //txtTurn.text = "TURN : " + (int)tempStatus[2] + "(" + (CommonData.REF_MAN[nTrainMode] - LevelCover) + " to " + (CommonData.REF_MAN[nTrainMode + 1] + LevelCover) + ")";
                }

                RotationTrainingAngle.SetAngle(tempStatus[2]);
            }
            else
            {
                //  SliderRotate.value = 12;
                Model_RotateLeft.SetActive(false);
                Model_RotateRight.SetActive(false);

                bTrainingSuccess_Rotate = true;
            }      
        }
            

        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND);
            if (angleLevel > 0)
            {
                float LevelCover = CommonData.LEVEL_COVER[angleLevel];
                if (CommonData.REF_MAN[nTrainMode + 2] - LevelCover <= tempStatus[1] && tempStatus[1] <= CommonData.REF_MAN[nTrainMode + 3] + LevelCover)
                {
                    Model_BendDown.SetActive(false);
                    Model_BendUp.SetActive(false);

                    // txtBend.text = "BEND OK";
                    bTrainingSuccess_Bend = true;
                }
                else if (CommonData.REF_MAN[nTrainMode + 2] - LevelCover < tempStatus[1])
                {
                    Model_BendDown.SetActive(true);
                    Model_BendUp.SetActive(false);

                    Model_BendDown.transform.Rotate(speed, 0, 0);
                    bTrainingSuccess_Bend = false;
                    //txtTurn.text = "TURN : " + (int)tempStatus[2] + "(" + (CommonData.REF_MAN[nTrainMode] - LevelCover) + " to " + (CommonData.REF_MAN[nTrainMode + 1] + LevelCover) + ")";
                }

                else if (CommonData.REF_MAN[nTrainMode + 3] - LevelCover > tempStatus[1])
                {
                    Model_BendUp.SetActive(true);
                    Model_BendDown.SetActive(false);
                    Model_BendUp.transform.Rotate(speed,  0, 0);
                    bTrainingSuccess_Bend = false;
                    //txtTurn.text = "TURN : " + (int)tempStatus[2] + "(" + (CommonData.REF_MAN[nTrainMode] - LevelCover) + " to " + (CommonData.REF_MAN[nTrainMode + 1] + LevelCover) + ")";
                }

                BendTrainingAngle.SetAngle(tempStatus[1]);
            }
            else
            {
                bTrainingSuccess_Bend = true;

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
                if (CommonData.REF_MAN[nTrainMode + 4] - LevelCover <= tempStatus[0] && tempStatus[0] <= CommonData.REF_MAN[nTrainMode + 5] + LevelCover)
                {

                    Model_SideLeft.SetActive(false);
                    Model_SideRight.SetActive(false);
                    bTrainingSuccess_Side = true;
                  //  txtSide.text = "SIDE OK";
                }
                else if (CommonData.REF_MAN[nTrainMode + 4] - LevelCover < tempStatus[0])
                {
                    Model_SideRight.SetActive(true);
                    Model_SideLeft.SetActive(false);

                    Model_SideRight.transform.Rotate(0, 0, -1 * speed);
                    bTrainingSuccess_Side = false;
                    //txtTurn.text = "TURN : " + (int)tempStatus[2] + "(" + (CommonData.REF_MAN[nTrainMode] - LevelCover) + " to " + (CommonData.REF_MAN[nTrainMode + 1] + LevelCover) + ")";
                }

                else if (CommonData.REF_MAN[nTrainMode + 5] - LevelCover > tempStatus[0])
                {
                    Model_SideLeft.SetActive(true);
                    Model_SideRight.SetActive(false);
                    Model_SideLeft.transform.Rotate(0, 0, speed);
                    bTrainingSuccess_Side = false;
                    //txtTurn.text = "TURN : " + (int)tempStatus[2] + "(" + (CommonData.REF_MAN[nTrainMode] - LevelCover) + " to " + (CommonData.REF_MAN[nTrainMode + 1] + LevelCover) + ")";
                }

                SideBendTrainingAngle.SetAngle(tempStatus[0]);
            }
            else
            {                
                bTrainingSuccess_Side = true;
                Model_SideLeft.SetActive(false);
                Model_SideRight.SetActive(false);
            }
        }        
    }

    public void CheckGyroStatus_Woman()
    {
        int nTrainMode = Convert.ToInt32(TKManager.Instance.GetPoseType());

        //Debug.Log("!@@@@@ UserStatus" + UserStatus);
        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_TURN);

            if (CommonData.REF_WOMAN[nTrainMode] <= tempStatus[2] && tempStatus[2] <= CommonData.REF_WOMAN[nTrainMode + 1])
            {
               // txtTurn.text = "TURN OK";
            }
            else
            {
               // txtTurn.text = "TURN : " + (int)tempStatus[2];
            }
        }
       // else
       //     txtTurn.text = "TURN 측정안함";


        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND);

            if (CommonData.REF_WOMAN[nTrainMode + 2] <= tempStatus[1] && tempStatus[1] <= CommonData.REF_WOMAN[nTrainMode + 3])
            {
           //     txtBend.text = "BEND OK";
            }
            else
            {
         //       txtBend.text = "BEND : " + (int)tempStatus[1];
            }
        }
      //  else
      //      txtBend.text = "TURN 측정안함";

        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE);

            if (CommonData.REF_WOMAN[nTrainMode + 4] <= tempStatus[0] && tempStatus[0] <= CommonData.REF_WOMAN[nTrainMode + 5])
            {
        //        txtSide.text = "SIDE OK";
            }
            else
            {
        //        txtSide.text = "SIDE : " + (int)tempStatus[0];
            }
        }
       // else
      //      txtSide.text = "SIDE 측정안함";
    }

    public void EndTraining()
    {
        PopupMgr.Instance.ShowPopup(PopupMgr.POPUP_TYPE.SELF_EVALUATION, new PopupSelfEvaluation.PopupData());
    }
}
