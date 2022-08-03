using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoFunction : MonoBehaviour
{
    private void OnEnable()
    {
        InputManager.Instance.StopMonitorInput();
    }

    private void OnDisable()
    {
        InputManager.Instance.StartMonitorInput();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameEventManager.TriggerUpdateGameInfo("", false);
        }
    }
}