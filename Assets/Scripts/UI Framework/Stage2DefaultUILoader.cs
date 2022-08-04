using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2DefaultUILoader : MonoBehaviour
{
    private void OnEnable()
    {
        UIManager.Instance.LoadUI(typeof(CameraStateInfoLogic));
        UIManager.Instance.LoadUI(typeof(PlayerOperationLogic));
        UIManager.Instance.LoadUI(typeof(GameInfoLogic));
        UIManager.Instance.LoadUI(typeof(LightFrequencyLogic));
    }
}