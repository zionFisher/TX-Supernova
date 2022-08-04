using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager>
{
    public void SetGameInfo(int inforIndex, bool enable)
    {
        // TODO:
    }

    public void SetGameInfo(string information, bool enable)
    {
        GameEventManager.Instance.TriggerUpdateGameInfo(information, enable);
    }

    public void DisableGameInfo()
    {
        GameEventManager.Instance.TriggerUpdateGameInfo("", false);
    }
}