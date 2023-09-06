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
    }
    

    private IEnumerator GetTime()
    {
        var url = "https://time100.ru/api.php";
        var request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        
        if (request.result != UnityWebRequest.Result.Success) {
            Debug.Log(request.error);
        }
        else {
            _time = ConvertSeconds(int.Parse(request.downloadHandler.text));
        }
    }

    private string ConvertSeconds(int secs)
    {
        TimeSpan t = TimeSpan.FromSeconds( secs );

        string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", 
            t.Hours, 
            t.Minutes, 
            t.Seconds, 
            t.Milliseconds);
        return answer;
    }
}