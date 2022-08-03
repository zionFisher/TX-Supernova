using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GameFlowTrigger : MonoBehaviour
{
    public bool TriggerGameFlow = true;

    [TextArea(2, 10)]
    public string Information;

    public GameObject[] EnableObjects;

    public GameObject[] DisableObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (TriggerGameFlow && Information != null)
        {
            GameFlowManager.Instance.SetGameInfo(Information, true);

            if (DisableObjects != null)
            {
                foreach(var go in EnableObjects)
                {
                    go.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (DisableObjects != null)
        {
            foreach (var go in DisableObjects)
            {
                go.SetActive(false);
            }
        }

        TriggerGameFlow = false;
    }
}