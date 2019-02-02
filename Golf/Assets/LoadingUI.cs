using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingUI : MonoBehaviour
{
    // TODO 로딩씬으로 변경 예정
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingData());
    }

    IEnumerator LoadingData()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        TKManager.Instance.gyro.enabled = false;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
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
