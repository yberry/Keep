using UnityEngine;

public abstract class Instance<T> : MonoBehaviour where T : Instance<T>
{
    private static T _instance = null;

    public static T instance => _instance;

    public static bool Initialized => _instance != null;

    [SerializeField]
    private bool dontDestroyOnLoad = true;

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
