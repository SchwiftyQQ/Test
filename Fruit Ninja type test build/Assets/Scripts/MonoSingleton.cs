using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log(typeof(T).ToString() + " is null!");
            return _instance;
        }
    }
    private void Awake()
    {
        //DontDestroyOnLoad();

        _instance = (T)this;
    }


    private void DontDestroyOnLoad()
    {
        if (_instance != null && _instance != (T)this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = (T)this;
        }

        DontDestroyOnLoad((T)this);
    }

    public virtual void Init()
    {

    }

}

