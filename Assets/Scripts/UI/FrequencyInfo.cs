using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrequencyInfo : MonoBehaviour
{
    public TMP_Text Frequency;

    public LaserBeamController LBC;

    private void Start()
    {
        LBC = GameObject.Find("Light").GetComponent<LaserBeamController>();
    }

    private void Update()
    {
        Frequency.text = LBC.curFrequency.ToString();
    }
}