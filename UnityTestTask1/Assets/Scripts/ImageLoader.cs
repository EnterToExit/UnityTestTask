using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    [SerializeField] private Transform _scrollViewContent;
    [SerializeField] private GameObject _imagePrefab;
    private int _picNum;
    private bool _hasError;

    private void Start()
    {
        StartCoroutine(TryLoadImages());
    }

    private string GenerateLink()
    {
        _picNum++;
        return $"https://data.ikppbb.com/test-task-unity-data/pics/{_picNum}.jpg";
    }

    private IEnumerator TryLoadImages()
    {
        while (!_hasError)
        {
            yield return LoadImage(GenerateLink());
        }
    }

    private IEnumerator LoadImage(string link)
    {
        var request = UnityWebRequestTexture.GetTexture(link);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            _hasError = true;
        }
        else
        {
            var gameObjectInstantiated = Instantiate(_imagePrefab, _scrollViewContent);
            var myTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            gameObjectInstantiated.GetComponent<Image>().sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height),
                new Vector3(.5f, 5f));
        }
    }
}