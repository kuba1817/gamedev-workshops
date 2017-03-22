using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

    private static T _instance;
    public static T Instance { get { return _instance; } }

    protected void Awake()
    {
        _instance = (T) FindObjectOfType(typeof(T));
    }
}
