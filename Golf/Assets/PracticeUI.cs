using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticeUI : MonoBehaviour
{
    public Button InitButton;

    public Text txtTurn;
    public Text txtBend;
    public Text txtSide;

    public GameObject Model;

    float[] tempStatus = new float[3];
    bool bStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        InitButton.onClick.AddListener(OnClickInit);
    }

    public void OnClickInit()
    {
        bStatus = !bStatus;
        GyroScopeManager.Instance.Init(bStatus);


        PracticeManager.Instance.SetCalibration();
    }

    // Update is called once per frame
    void Update()
    {
        tempStatus = PracticeManager.Instance.GetGyroStatus();

        txtTurn.text = "TURN : " + (int)tempStatus[2];
        txtBend.text = "BEND : " + (int)tempStatus[1];
        txtSide.text = "SIDE : " + (int)tempStatus[0];
    }

}
