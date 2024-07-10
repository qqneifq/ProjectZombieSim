using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static System.Math;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    private void Start()
    {
        ZombiesWait.OnTimerTick += OnTimerTickHandler;
    }

    void OnTimerTickHandler(float f)
    {
        if(f > 0)
        {
            text.text = Floor(f).ToString();
        }

    }
}
