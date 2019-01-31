using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticeUI : MonoBehaviour
{
    public Text txtTurn;
    public Text txtBend;
    public Text txtSide;

    public Text txtMode;

  

    float[] tempStatus = new float[3];
    bool bStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        string tempString = TKManager.Instance.GetMode().ToString();
        tempString = tempString.Substring(9);

        txtMode.text = tempString;

        GyroScopeManager.Instance.Init();
        PracticeManager.Instance.SetCalibration();
    }
    

    // Update is called once per frame
    void Update()
    {
        tempStatus = PracticeManager.Instance.GetGyroStatus();
        CheckGyroStatus();
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


    public void CheckGyroStatus_Man()
    {
        int nTrainMode = Convert.ToInt32(TKManager.Instance.GetMode());

        if (CommonData.REF_MAN[nTrainMode] <= tempStatus[2] && tempStatus[2] <= CommonData.REF_MAN[nTrainMode + 1])
        {
            txtTurn.text = "TURN OK";
        }
        else
        {
            txtTurn.text = "TURN : " + (int)tempStatus[2];
        }
            

        if (CommonData.REF_MAN[nTrainMode + 2] <= tempStatus[1] && tempStatus[1] <= CommonData.REF_MAN[nTrainMode + 3]
            )
        {
            txtBend.text = "BEND OK";
        }
        else
        {
            txtBend.text = "BEND : " + (int)tempStatus[1];
        }

        if (CommonData.REF_MAN[nTrainMode + 4] <= tempStatus[0] && tempStatus[0] <= CommonData.REF_MAN[nTrainMode + 5] )
        {
            txtSide.text = "SIDE OK";
        }
        else
        {
            txtSide.text = "SIDE : " + (int)tempStatus[0];
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
