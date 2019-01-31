using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SampleUI : MonoBehaviour
{
    public Button Btn_Address;
    public Button Btn_Backswing;
    public Button Btn_Impact;
    public Button Btn_Temp;
    

    // Start is called before the first frame update
    void Start()
    {
        Btn_Address.onClick.AddListener(OnClickAddress);
        Btn_Backswing.onClick.AddListener(OnClickBackSwing);
        Btn_Impact.onClick.AddListener(OnClickImpact);
        Btn_Temp.onClick.AddListener(OnClickTempo);
    }

    public void OnClickAddress()
    {
        TKManager.Instance.gyro.enabled = false;
        TKManager.Instance.SetMode(CommonData.TRAINING_MODE.TRAINING_ADDRESS);
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }

    public void OnClickBackSwing()
    {
        TKManager.Instance.gyro.enabled = false;
        TKManager.Instance.SetMode(CommonData.TRAINING_MODE.TRAINING_BACKSWING);
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }
    public void OnClickImpact()
    {
        TKManager.Instance.gyro.enabled = false;
        TKManager.Instance.SetMode(CommonData.TRAINING_MODE.TRAINING_IMPACT);
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }

    public void OnClickTempo()
    {
        TKManager.Instance.gyro.enabled = false;
        TKManager.Instance.SetMode(CommonData.TRAINING_MODE.TRAINING_TEMPO);
        SceneManager.LoadScene("PracticeScene", LoadSceneMode.Single);
    }


    // Update is called once per frame
    void Update()
    {

    }


}
