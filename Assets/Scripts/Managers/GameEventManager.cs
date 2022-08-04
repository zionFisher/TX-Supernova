using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : Singleton<GameEventManager>
{
    public Action<string, bool> EventUpdateGameInfo;
    public void TriggerUpdateGameInfo(string information, bool enable) { if (EventUpdateGameInfo == null) return; EventUpdateGameInfo(information, enable); }

    public Action<bool> EventEnableGameInfo;
    public void TriggerEnableGameInfo(bool enable) { if (EventEnableGameInfo == null) return; EventEnableGameInfo(enable); }
}