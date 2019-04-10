using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingHUD : MonoBehaviour
{
    public Canvas HUDCanvas;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        HUDCanvas.sortingOrder = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
