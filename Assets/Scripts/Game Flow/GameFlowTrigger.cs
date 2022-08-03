using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GameFlowTrigger : MonoBehaviour
{
    public bool TriggerGameFlow = true;

    [TextArea(2, 10)]
    public string Information;

    private void OnTriggerEnter(Collider other)
    {
        if (TriggerGameFlow && Information != null)
            GameFlowManager.Instance.SetGameInfo(Information, true);
    }
}