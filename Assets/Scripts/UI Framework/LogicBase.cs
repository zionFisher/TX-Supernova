using UnityEngine;

public class LogicBase
{
    public GameObject currentUIRoot = null;

    public string currentUIName = "";

    public CtrlBase Ctrl { get; private set; }

    public LogicBase(string UIName)
    {
        currentUIName = UIName;
        GameObject uiPrefab = ResourcesManager.Instance.LoadUIPrefab(currentUIName);
        currentUIRoot = GameObject.Instantiate(uiPrefab, UIManager.Instance.UIRoot.transform);
        Ctrl = currentUIRoot.GetComponent<CtrlBase>();
        OnLoad();
    }

    protected virtual void OnLoad()
    {

    }

    public virtual void OnClose()
    {
        Dispose();
    }

    protected virtual void Dispose()
    {
        GameObject.Destroy(currentUIRoot);
    }
}