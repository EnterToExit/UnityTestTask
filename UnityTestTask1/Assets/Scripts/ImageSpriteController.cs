using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageSpriteController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadImageTexture(GenerateLink()));
    }
    
    private string GenerateLink()
    {
        var gameObjectName = transform.name;
        return $"https://data.ikppbb.com/test-task-unity-data/pics/{gameObjectName}.jpg";
    }

    private IEnumerator LoadImageTexture(string link)
    {
        var request = UnityWebRequestTexture.GetTexture(link);
        yield return request.SendWebRequest();
    
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            var serverTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            transform.GetComponent<Image>().sprite = Sprite.Create(serverTexture, new Rect(0, 0, serverTexture.width, serverTexture.height),
                new Vector3(.5f, 5f));
        }
    }
}
