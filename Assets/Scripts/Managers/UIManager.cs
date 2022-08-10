using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<Type, LogicBase> uiPages = new Dictionary<Type, LogicBase>();

    public GameObject UIRoot { get => _uiRoot; private set { _uiRoot = value; } }

    public Canvas UICanvas { get; private set; }

    [SerializeField] private GameObject _uiRoot;

    private void Awake()
    {
        Utility.CheckUnassignedVar<GameObject>(_uiRoot);
        UICanvas = UIRoot.GetComponent<Canvas>();
        UICanvas.worldCamera = Camera.main;
        UICanvas.planeDistance = 100.0f;
    }

    public LogicBase GetUI(Type type)
    {
        if (!typeof(LogicBase).IsAssignableFrom(type))
            return null;

        LogicBase logicBase = null;

        if (uiPages.TryGetValue(type, out logicBase))
            return logicBase;

        return null;
    }

	public LogicBase LoadUI(Type type)
    {
        LogicBase logicBase = GetUI(type);

        if (logicBase != null)
        {
            return logicBase;
        }

        logicBase = (LogicBase)Activator.CreateInstance(type);
        AddUI(logicBase);

        return logicBase;
    }

    public bool CloseUI(Type type)
    {
        LogicBase logicBase = GetUI(type);

        if (logicBase != null)
        {
            RemoveUI(type);
            logicBase.OnClose();
            return true;
        }

        return false;
    }

    public void CloseAllUI()
    {
        List<Type> keys = new List<Type>(uiPages.Keys);
        foreach (var key in keys)
        {
            if (uiPages.ContainsKey(key))
            {
                uiPages[key].OnClose();
            }
        }
        uiPages.Clear();
    }

    private void AddUI(LogicBase logicBase)
    {
        uiPages.Add(logicBase.GetType(), logicBase);
        logicBase.currentUIRoot.transform.SetParent(UIRoot.transform);

        logicBase.currentUIRoot.transform.localScale = Vector3.one;
        logicBase.currentUIRoot.transform.localRotation = Quaternion.identity;
    }

    private void RemoveUI(Type logicBase)
    {
        if (uiPages.ContainsKey(logicBase))
            uiPages.Remove(logicBase);
    }
}