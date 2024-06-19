using System.Collections;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    private static CoroutineHandler _instance;

    public static CoroutineHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("CoroutineHandler");
                _instance = go.AddComponent<CoroutineHandler>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
}
