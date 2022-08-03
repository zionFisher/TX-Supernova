using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventManager
{
    public static Action<string, bool> EventUpdateGameInfo;
    public static void TriggerUpdateGameInfo(string information, bool enable) { if (EventUpdateGameInfo == null) return; EventUpdateGameInfo(information, enable); }

    public static Action<bool> EventEnableGameInfo;
    public static void TriggerEnableGameInfo(bool enable) { if (EventEnableGameInfo == null) return; EventEnableGameInfo(enable); }
}