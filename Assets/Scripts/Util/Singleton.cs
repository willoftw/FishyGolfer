using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : class
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                    Debug.LogError("SingletoneBase<T>: Could not found GameObject of type " + typeof(T).Name);
            }
            return instance;
        }
    }
}