using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    static GameObject instance;

    void Awake()
    {
        Debug.Log(instance);
        // Avoid duplicates of this gameobject when loading scenes.
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (gameObject == instance)
        {
            instance = null;
        }
    }
}
