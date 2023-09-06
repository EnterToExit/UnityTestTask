using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class TimeController : MonoBehaviour
{
    [DllImport("__Internal")]

    private static extern void AlertMoscowTime(string message);
    
    private void GetMoscowTime()
    {
        var url = "https://time100.ru/api.php";
        var request = UnityWebRequest.Get(url);
        var time = request.downloadHandler.text;
        
        AlertMoscowTime(time);
    }
}