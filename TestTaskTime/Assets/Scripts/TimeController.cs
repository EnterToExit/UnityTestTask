using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;


public class TimeController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void AlertMoscowTime(string message);

    private string _time;

    private void GetMoscowTime()
    {
        StartCoroutine(GetTime());
        Invoke("DisplayTime", 1f);
    }

    private void DisplayTime()
    {
        // Debug.Log(_time);
        AlertMoscowTime(_time);
    }


    private IEnumerator GetTime()
    {
        const string url = "https://time100.ru/api.php";
        var request = UnityWebRequest.Get(url);
        // request.SetRequestHeader("Access-Control-Allow-Credentials", "true");
        // request.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
        // request.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS"); 
        // request.SetRequestHeader("Access-Control-Allow-Origin", "*");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            _time = ConvertSeconds(int.Parse(request.downloadHandler.text));
        }
    }

    private string ConvertSeconds(int secs)
    {
        var t = TimeSpan.FromSeconds(secs);

        var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s:{t.Milliseconds:D3}ms";
        return answer;
    }
}