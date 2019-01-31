using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeManager : MonoBehaviour
{

    public static PracticeManager _instance = null;
    public static PracticeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PracticeManager>() as PracticeManager;
            }
            return _instance;
        }
    }

    private bool isModelActive = false;

    float[] UserStatus = new float[3];

    float InitUserStatus_x;
    float InitUserStatus_y;
    float InitUserStatus_z;

    // Start is called before the first frame update
    void Start()
    {
        InitUserStatus_x = 0.0f;
        InitUserStatus_y = 0.0f;
        InitUserStatus_z = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckGyroStatus();
    }

    public void SetCalibration()
    {
        InitUserStatus_x = UserStatus[0];
        InitUserStatus_y = UserStatus[1];
        InitUserStatus_z = UserStatus[2];
    }

    public void SetGyroStatus(float yaw, float pitch, float roll)
    {
        // yaw :  turn
        UserStatus[0] = Mathf.Rad2Deg * yaw;// - InitUserStatus_x;

        // pitch :band
        UserStatus[1] = Mathf.Rad2Deg * pitch;// - InitUserStatus_y;

        //roll : side
        UserStatus[2] = Mathf.Rad2Deg * roll;// - InitUserStatus_z;

        /*
        Debug.Log("!@@@@@ UserStatus_x :" + UserStatus[0]);
        Debug.Log("!@@@@@ UserStatus_y :" + UserStatus[1]);
        Debug.Log("!@@@@@ UserStatus_z :" + UserStatus[2]);
        */

    }

    public float[] GetGyroStatus()
    {
        return UserStatus;
    }


}
