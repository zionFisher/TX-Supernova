using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrequencyInfo : MonoBehaviour
{
    public TMP_Text Frequency;

    public LaserBeamController LBC;

    private void Update()
    {
        Frequency.text = LBC.curFrequency.ToString();
    }
}