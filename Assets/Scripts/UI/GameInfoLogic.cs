using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoLogic : LogicBase
{
    private GameInfoCtrl ctrl;

    public GameInfoLogic() : base(ZUIName.GameInfoName)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        ctrl = Ctrl as GameInfoCtrl;

        DisableGameInfo();
        GameEventManager.Instance.EventUpdateGameInfo += SetGameInfo;
    }

    protected override void Dispose()
    {
        // GameEventManager.Instance.EventUpdateGameInfo -= SetGameInfo;
        base.Dispose();
    }

    public void SetGameInfo(string information, bool enable)
    {
        ctrl.Information.text = information;
        if (enable == true)
            EnableGameInfo();
        else
            DisableGameInfo();
    }

    private void DisableGameInfo()
    {
        ctrl.gameObject.SetActive(false);
        GameEventManager.Instance.TriggerEnableGameInfo(false);
    }

    private void EnableGameInfo()
    {
        ctrl.gameObject.SetActive(true);
        GameEventManager.Instance.TriggerEnableGameInfo(true);
    }
}