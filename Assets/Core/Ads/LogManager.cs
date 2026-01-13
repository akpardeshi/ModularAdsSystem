using UnityEngine;

[DefaultExecutionOrder(-1000)] 
public class LogManager : MonoBehaviour
{
    void Awake()
    {
#if !(DEVELOPMENT_BUILD || UNITY_EDITOR)
            Debug.unityLogger.logEnabled = false;
#endif
    }
}