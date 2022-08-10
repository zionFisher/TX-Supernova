using UnityEngine;

public class USingleton<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = new T();

            return _instance;
        }
        private set => _instance = value;
    }

    protected virtual void OnDestroy()
    {
        if (_instance != null)
            _instance = null;
    }
}