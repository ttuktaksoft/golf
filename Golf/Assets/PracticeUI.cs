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
    private bool isModelActive = false;

    float UserStatus_x;
    float UserStatus_y;
    float UserStatus_z;

    float InitUserStatus_x;
    float InitUserStatus_y;
    float InitUserStatus_z;


    public static PracticeUI _instance = null;
    public static PracticeUI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PracticeUI>() as PracticeUI;
            }
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitUserStatus_x = 0.0f;
        InitUserStatus_y = 0.0f;
        InitUserStatus_z = 0.0f;

        InitButton.onClick.AddListener(OnClickInit);
    }

    public void OnClickInit()
    {
        isModelActive = !isModelActive;

        InitUserStatus_x = UserStatus_x;
        InitUserStatus_y = UserStatus_y;
        InitUserStatus_z = UserStatus_z;
    }

    // Update is called once per frame
    void Update()
    {
       if(isModelActive)
        {
    

            CheckGyroStatus();
        }
    }



    public void SetGyroStatus(float x, float y, float z)
    {
        UserStatus_x = Mathf.Rad2Deg * x - InitUserStatus_x;
        UserStatus_y = Mathf.Rad2Deg * y - InitUserStatus_y;
        UserStatus_z = Mathf.Rad2Deg * z - InitUserStatus_z;

        //txtTurn.text = "TURN : " + (int)UserStatus_x;
        txtBend.text = "BEND : " + (int)UserStatus_y;
        //txtSide.text = "SIDE : " + (int)UserStatus_z;

        Debug.Log("!@@@@@ UserStatus_x :" + UserStatus_x);
        Debug.Log("!@@@@@ UserStatus_y :" + UserStatus_y);
        Debug.Log("!@@@@@ UserStatus_z :" + UserStatus_z);
    }


    public void CheckGyroStatus()
    {

        if(7 <= UserStatus_x &&  UserStatus_x <= 17)
            Debug.Log("!@@@@@ Address Turn OK");

    }

}
