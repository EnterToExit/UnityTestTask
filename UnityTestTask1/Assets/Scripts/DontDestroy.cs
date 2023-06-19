using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static bool Instantiated;
    [SerializeField] private bool _dontDestroyOnLoad;

    private void Awake()
    {
        if (!_dontDestroyOnLoad) return;
        if (!Instantiated) 
        {
        DontDestroyOnLoad(this.gameObject);
        Instantiated = true;
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }
}
