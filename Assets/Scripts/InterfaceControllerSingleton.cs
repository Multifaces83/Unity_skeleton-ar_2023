using UnityEngine;

public class InterfaceControllerSingleton : MonoBehaviour
{
    private static InterfaceControllerSingleton _instance;

    public static InterfaceControllerSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InterfaceControllerSingleton>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject); // Destroy duplicate instances
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject); // Keep this instance across scenes
        }
    }
}
