using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TimeController : MonoBehaviour
{
    private void GetMoscowTime()
    {
        var url = "https://time100.ru/api.php";
        var request = UnityWebRequest.Get(url);
        var time = request.downloadHandler.text;
        Debug.Log(time);
    }
}