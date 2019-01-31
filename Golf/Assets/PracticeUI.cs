﻿using System;
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
        string tempString = TKManager.Instance.GetMode().ToString();
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
        GyroScopeManager.Instance.Init(true);


        //PracticeManager.Instance.SetCalibration();
    }

    public void OnClickBack()
    {
        GyroScopeManager.Instance.DestroyGyro();
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

    }



    // Update is called once per frame
    void Update()
    {
        tempStatus = PracticeManager.Instance.GetGyroStatus();
        //txtTurn.text = "tempStatus : " + tempStatus[0];
        Debug.Log("tempStatus : " + tempStatus[0]);
        CheckGyroStatus();
        SuccessTraining();
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
    bool bTrainingSuccess = false;

    public void SuccessTraining()
    {        
        if(bTrainingSuccess == false)
        {
            if (nTrainingSuccess == 2)
            {
                SoundManager.Instance.PlaySuccessSound();
                bTrainingSuccess = true;
            }
       
        }
    }

    public void CheckGyroStatus_Man()
    {
        int nTrainMode = Convert.ToInt32(TKManager.Instance.GetMode());

        if (CommonData.REF_MAN[nTrainMode] <= tempStatus[2] && tempStatus[2] <= CommonData.REF_MAN[nTrainMode + 1])
        {
            txtTurn.text = "TURN OK";
            nTrainingSuccess = 1;
        }
        else
        {
            txtTurn.text = "TURN : " + (int)tempStatus[2] + "(" + CommonData.REF_MAN[nTrainMode] + " to " + CommonData.REF_MAN[nTrainMode +1] + ")";
        }
            

        if (CommonData.REF_MAN[nTrainMode + 2] <= tempStatus[1] && tempStatus[1] <= CommonData.REF_MAN[nTrainMode + 3])
        {
            txtBend.text = "BEND OK";
            if (nTrainingSuccess == 1)
                nTrainingSuccess = 2;
        }
        else
        {
            txtBend.text = "BEND : " + (int)tempStatus[1] + "(" + CommonData.REF_MAN[nTrainMode +2 ] + " to " + CommonData.REF_MAN[nTrainMode + 3] + ")";
        }

        if (CommonData.REF_MAN[nTrainMode + 4] <= tempStatus[0] && tempStatus[0] <= CommonData.REF_MAN[nTrainMode + 5] )
        {
            txtSide.text = "SIDE OK";
        }
        else
        {
            txtSide.text = "SIDE : " + (int)tempStatus[0] + "(" + CommonData.REF_MAN[nTrainMode + 4] + " to " + CommonData.REF_MAN[nTrainMode + 5] + ")";
        }    

    }

    public void CheckGyroStatus_Woman()
    {
        int nTrainMode = Convert.ToInt32(TKManager.Instance.GetMode());

        //Debug.Log("!@@@@@ UserStatus" + UserStatus);
        if (CommonData.REF_WOMAN[nTrainMode] <= tempStatus[2] && tempStatus[2] <= CommonData.REF_WOMAN[nTrainMode + 1])
        {
            txtTurn.text = "TURN OK";
        }
        else
        {
            txtTurn.text = "TURN : " + (int)tempStatus[2];
        }


        if (CommonData.REF_WOMAN[nTrainMode + 2] <= tempStatus[1] && tempStatus[1] <= CommonData.REF_WOMAN[nTrainMode + 3]
            )
        {
            txtBend.text = "BEND OK";
        }
        else
        {
            txtBend.text = "BEND : " + (int)tempStatus[1];
        }

        if (CommonData.REF_WOMAN[nTrainMode + 4] <= tempStatus[0] && tempStatus[0] <= CommonData.REF_WOMAN[nTrainMode + 5])
        {
            txtSide.text = "SIDE OK";
        }
        else
        {
            txtSide.text = "SIDE : " + (int)tempStatus[0];
        }

    }
}
