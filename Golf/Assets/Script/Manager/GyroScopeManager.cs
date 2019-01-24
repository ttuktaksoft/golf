using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroScopeManager : MonoBehaviour
{
    private int initialOrientationX;
    private int initialOrientationY;
    private int initialOrientationZ;


    private Quaternion cameraBase = Quaternion.Euler(new Vector3(3f, 180f, 0f));
    private Quaternion referanceRotation = Quaternion.identity;
    private const float lowPassFilterFactor = 0.2f;


    public static GyroScopeManager _instance = null;
    public static GyroScopeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GyroScopeManager>() as GyroScopeManager;
            }
            return _instance;
        }
    }

    void Start()
    {

        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.01f;

        initialOrientationX = (int)Input.gyro.rotationRateUnbiased.x;
        initialOrientationY = (int)Input.gyro.rotationRateUnbiased.y;
        initialOrientationZ = (int)-Input.gyro.rotationRateUnbiased.z;

    }

    public void Init()
    {
       
    }
    

    void Update()
    {
        //transform.rotation = Quaternion.Slerp(transform.rotation, cameraBase * (ConvertRotation(referanceRotation * Input.gyro.attitude) * GetRotFix()), lowPassFilterFactor);

        Invoke("gyroupdate", 0.1f);

        //Debug.Log("!@@@@@ initialOrientationY - Input.gyro.rotationRateUnbiased.y :" +  (Input.gyro.rotationRateUnbiased.y));
        //Debug.Log("!@@@@@ initialOrientationZ + Input.gyro.rotationRateUnbiased.z :" + ( Input.gyro.rotationRateUnbiased.z));
    }

    private static Quaternion ConvertRotation(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    private Quaternion GetRotFix()
    {
        if (Screen.orientation == ScreenOrientation.Portrait) return Quaternion.identity;
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.Landscape) return Quaternion.Euler(0, 0, -90);
        if (Screen.orientation == ScreenOrientation.LandscapeRight) return Quaternion.Euler(0, 0, 90);
        if (Screen.orientation == ScreenOrientation.PortraitUpsideDown) return Quaternion.Euler(0, 0, 180);
        return Quaternion.identity;
    }


    private static void GetAngle(Quaternion q)
    {

        float sqw = q.w * q.w;
        float sqx = q.x * q.x;
        float sqy = q.y * q.y;
        float sqz = q.z * q.z;

        float pitch = Mathf.Asin(2.0f * (q.w * q.x - q.y * q.z));
        float yaw = Mathf.Atan2(2.0f * (q.x * q.z + q.w * q.y), (-sqx - sqy + sqz + sqw));
        float roll = Mathf.Atan2(2.0f * (q.x * q.y + q.w * q.z), (-sqx + sqy - sqz +sqw));

        /*
         float roll = Mathf.Atan2(2.0f * (q.z * q.y + q.w * q.x), 1.0f - 2.0f * (q.x * q.x + q.y * q.y));
         float pitch = Mathf.Asin(2.0f * (q.y * q.w - q.z * q.x));
         float yaw = Mathf.Atan2(2.0f * (q.z * q.w + q.x * q.y), -1.0f + 2.0f * (q.w * q.w + q.x * q.x));    
         */

        PracticeUI.Instance.SetGyroStatus(pitch, yaw, roll);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        Quaternion rtQt = new Quaternion(q.x, q.y, -q.z, -q.w);
        GetAngle(Quaternion.identity * rtQt);
        return rtQt;
    }

    float _BaseAngle = -90f;

    void gyroupdate()
    {
       // transform.rotation = Input.gyro.attitude;

        GyroToUnity(Input.gyro.attitude);

        
            transform.Rotate(initialOrientationX - Input.gyro.rotationRateUnbiased.x,
                initialOrientationY - Input.gyro.rotationRateUnbiased.y,
                initialOrientationZ + Input.gyro.rotationRateUnbiased.z);

    }

}
