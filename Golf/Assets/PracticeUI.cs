using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PracticeUI : MonoBehaviour
{
    public Text txtTurn;
    public Text txtBend;
    public Text txtSide;

    public Text txtMode;

    public Button btnInit;
    public Button btnBack;

    float[] tempStatus = new float[3];
    bool bStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        string tempString = TKManager.Instance.GetPoseType().ToString();
        tempString = tempString.Substring(9);

        txtMode.text = tempString;

        btnInit.onClick.AddListener(OnClickInit);
        btnBack.onClick.AddListener(OnClickBack);

        //GyroScopeManager.Instance.Init();
        //  PracticeManager.Instance.SetCalibration();
    }

    public void OnClickInit()
    {
        //isModelActive = !isModelActive;
        if (TKManager.Instance.GetTrainingType() != CommonData.TRAINING_TYPE.TRAINING_TEMPO)
            GyroScopeManager.Instance.Init(true);
        else
            SoundManager.Instance.PlayTempoTraining(5.0f);


        //PracticeManager.Instance.SetCalibration();
    }

    public void OnClickBack()
    {
        GyroScopeManager.Instance.DestroyGyro();
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

    }


    // Update is called once per frame
    void Update()
    {        
        if (TKManager.Instance.GetTrainingType() != CommonData.TRAINING_TYPE.TRAINING_TEMPO)
        {
            tempStatus = PracticeManager.Instance.GetGyroStatus();
            //txtTurn.text = "tempStatus : " + tempStatus[0];
            Debug.Log("tempStatus : " + tempStatus[0]);
            CheckGyroStatus();
            SuccessTraining();
        }
        

    }


    public void CheckGyroStatus()
    {
        if(TKManager.Instance.GetGender() == CommonData.GENDER.GENDER_MAN)
        {
            CheckGyroStatus_Man();
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
                    txtTurn.text = "ROTATION OK";
                    bTrainingSuccess_Rotate = true;
                }
                else
                {
                    bTrainingSuccess_Rotate = false;
                    txtTurn.text = "TURN : " + (int)tempStatus[2] + "(" + (CommonData.REF_MAN[nTrainMode] - LevelCover) + " to " + (CommonData.REF_MAN[nTrainMode + 1] + LevelCover) + ")";
                }
            }
            else
            {
                bTrainingSuccess_Rotate = true;
                txtTurn.text = "ROTATION : 측정안함";
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
                    txtBend.text = "BEND OK";
                    bTrainingSuccess_Bend = true;
                }
                else
                {
                    bTrainingSuccess_Bend = false;
                    txtBend.text = "BEND : " + (int)tempStatus[1] + "(" + (CommonData.REF_MAN[nTrainMode + 2] - LevelCover) + " to " + (CommonData.REF_MAN[nTrainMode + 3] + LevelCover) + ")";
                }
            }
            else
            {
                bTrainingSuccess_Bend = true;
                txtBend.text = "BEND : 측정안함";
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
                    bTrainingSuccess_Side = true;
                    txtSide.text = "SIDE OK";
                }
                else
                {
                    bTrainingSuccess_Side = false;
                    txtSide.text = "SIDE : " + (int)tempStatus[0] + "(" + (CommonData.REF_MAN[nTrainMode + 4] - LevelCover) + " to " + (CommonData.REF_MAN[nTrainMode + 5] + LevelCover) + ")";
                }
            }
            else
            {
                bTrainingSuccess_Side = true;
                txtSide.text = "SIDE : 측정안함";
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
                txtTurn.text = "TURN OK";
            }
            else
            {
                txtTurn.text = "TURN : " + (int)tempStatus[2];
            }
        }
        else
            txtTurn.text = "TURN 측정안함";


        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_BEND);

            if (CommonData.REF_WOMAN[nTrainMode + 2] <= tempStatus[1] && tempStatus[1] <= CommonData.REF_WOMAN[nTrainMode + 3])
            {
                txtBend.text = "BEND OK";
            }
            else
            {
                txtBend.text = "BEND : " + (int)tempStatus[1];
            }
        }
        else
            txtBend.text = "TURN 측정안함";

        if (TKManager.Instance.IsAngleType(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE))
        {
            int angleLevel = TKManager.Instance.GetAngleLevel(CommonData.TRAINING_ANGLE.TRAINING_ANGLE_SIDE);

            if (CommonData.REF_WOMAN[nTrainMode + 4] <= tempStatus[0] && tempStatus[0] <= CommonData.REF_WOMAN[nTrainMode + 5])
            {
                txtSide.text = "SIDE OK";
            }
            else
            {
                txtSide.text = "SIDE : " + (int)tempStatus[0];
            }
        }
        else
            txtSide.text = "SIDE 측정안함";
    }
}
