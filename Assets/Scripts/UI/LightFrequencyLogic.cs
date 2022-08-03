using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFrequencyLogic : LogicBase
{
    private LightFrequencyCtrl ctrl;

    public LightFrequencyLogic() : base(ZUIName.LightFrequencyName)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        ctrl = Ctrl as LightFrequencyCtrl;
        ctrl.Frequency.text = "386";
        GameEventManager.EventEnableGameInfo += UpdateLightFrequency;
    }

    protected override void Dispose()
    {
        GameEventManager.EventEnableGameInfo -= UpdateLightFrequency;
        base.Dispose();
    }

    private void UpdateLightFrequency(bool enable)
    {
        ctrl.gameObject.SetActive(!enable);
    }
}