﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroScopeManager : MonoBehaviour
{
    private int initialOrientationX;
    private int initialOrientationY;
    private int initialOrientationZ;

    private Gyroscope gyro;
    public Quaternion rotation = Quaternion.identity;
    private Quaternion initialGyroRotation;
    private Quaternion initialRotation;
    private Quaternion baseRotation;
    private Quaternion gyroInitialRotation;
    public GameObject Model;


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

        //   if (Input.gyro.enabled)
     
            initialRotation = transform.rotation;
             Debug.Log("!!! gyro initialRotation : " + initialRotation);

            gyroInitialRotation = Input.gyro.attitude;
               Debug.Log("!!! gyro gyroInitialRotation : " + gyroInitialRotation);

            Screen.orientation = ScreenOrientation.Portrait;

            /*
            initialOrientationX = (int)Input.gyro.rotationRateUnbiased.x;
            initialOrientationY = (int)Input.gyro.rotationRateUnbiased.y;
            initialOrientationZ = (int)-Input.gyro.rotationRateUnbiased.z;
            */

            /*
            Input.gyro.enabled = true;
            Input.gyro.updateInterval = 0.01f;


            initialOrientationX = (int)Input.gyro.rotationRate.x;
            initialOrientationY = (int)Input.gyro.rotationRate.y;
            initialOrientationZ = (int)-Input.gyro.rotationRate.z;
            */
        }



             public void Init()
             {
      

            /*
initialOrientationX = (int)Input.gyro.rotationRateUnbiased.x;
initialOrientationY = (int)Input.gyro.rotationRateUnbiased.y;
initialOrientationZ = (int)-Input.gyro.rotationRateUnbiased.z;
*/
            /*
            Input.gyro.updateInterval = 0.01f;



            initialOrientationX = (int)Input.gyro.rotationRate.x;
            initialOrientationY = (int)Input.gyro.rotationRate.y;
            initialOrientationZ = (int)-Input.gyro.rotationRate.z;
            */
                 }


    void Update()
    {        
      Invoke("gyroupdate", 0.1f);
    }
    
    private static void GetAngle(Quaternion q)
    {

        /*
          float sqw = q.w * q.w;
          float sqx = q.x * q.x;
          float sqy = q.y * q.y;
          float sqz = q.z * q.z;

          float pitch = Mathf.Asin(2.0f * (q.w * q.x - q.y * q.z));
          float yaw = Mathf.Atan2(2.0f * (q.x * q.z + q.w * q.y), (-sqx - sqy + sqz + sqw));
          float roll = Mathf.Atan2(2.0f * (q.x * q.y + q.w * q.z), (-sqx + sqy - sqz +sqw));

         */

            float x = q.x;
          float y = q.y;
          float z = q.z;
          float w = q.w;

          

        float roll = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
        float pitch = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
        float yaw = Mathf.Asin(2 * x * y + 2 * z * w);


        /*
         float roll = Mathf.Atan2(2.0f * (q.z * q.y + q.w * q.x), 1.0f - 2.0f * (q.x * q.x + q.y * q.y));
         float pitch = Mathf.Asin(2.0f * (q.y * q.w - q.z * q.x));
         float yaw = Mathf.Atan2(2.0f * (q.z * q.w + q.x * q.y), -1.0f + 2.0f * (q.w * q.w + q.x * q.x));    
         */

        PracticeManager.Instance.SetGyroStatus(yaw, pitch, roll);
    }


    private static void GetAngle(Vector3 q)
    {
        float roll = q.x;
        float pitch = q.y;
        float yaw = q.z;        

        PracticeManager.Instance.SetGyroStatus(roll, pitch, yaw);
    }
    private static Quaternion GyroToUnity(Quaternion q)
    {        
        //Quaternion rtQt = new Quaternion(q.x, q.y, -q.z, -q.w);
        GetAngle(q);
        //GetAngle(q.eulerAngles);

        return q;
    }

    float _BaseAngle = -90f;

    void gyroupdate()
    {
        if (Input.gyro.enabled)
        {      

            Quaternion offsetRotation = Quaternion.Inverse(gyroInitialRotation) * Input.gyro.attitude;
        
            transform.rotation = initialRotation * offsetRotation;
            GyroToUnity(initialRotation * offsetRotation);

            float AngleX = transform.rotation.eulerAngles.x;
            float AngleY = transform.rotation.eulerAngles.y;
            float AngleZ = transform.rotation.eulerAngles.z;

            Debug.Log("!@@@@@ Address x" + AngleX);
            Debug.Log("!@@@@@ Address y" + AngleY);
            Debug.Log("!@@@@@ Address z" + AngleZ);

            // transform.rotation = Input.gyro.attitude;
            /*


             Debug.Log("!@@@@@ Address x" + (initialOrientationX - Input.gyro.rotationRate.x));
             Debug.Log("!@@@@@ Address y" + (initialOrientationY - Input.gyro.rotationRate.y));
             Debug.Log("!@@@@@ Address z" + (initialOrientationZ - Input.gyro.rotationRate.z));



             transform.Rotate(initialOrientationX - Input.gyro.rotationRate.x,
                     initialOrientationY - Input.gyro.rotationRate.y,
                     initialOrientationZ + Input.gyro.rotationRate.z);

         */
        }
        else
        {
            Input.gyro.enabled = true;
        }

    }

}
