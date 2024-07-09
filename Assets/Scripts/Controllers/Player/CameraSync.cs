using System;
using UnityEngine;

public class CameraSync : MonoBehaviour
{
    [SerializeField]
    GameObject firstCam;
    [SerializeField]
    GameObject thirdCam;
    [SerializeField]
    GameObject currentCam;
    bool camMode = true;

    public static event Action<GameObject> OnCameraSwitched;

    GameObject GetCurrentCam()
    {
        return currentCam;
    }

    void Start()
    {
        firstCam.SetActive(true);
        thirdCam.SetActive(false);
        currentCam = firstCam;
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("SwitchCamera");
            SwitchMode();
        }
    }
    void SwitchMode()
    {
        if(camMode)
        {
            firstCam.SetActive(false);
            thirdCam.SetActive(true);
            currentCam = thirdCam;
        }
        else
        {
            thirdCam.SetActive(false);
            firstCam.SetActive(true);
            currentCam = firstCam;
        }
        OnCameraSwitched?.Invoke(currentCam);
        camMode = !camMode;
    }
}
