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
    private UnityWebRequest _request;

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
            yield return LoadImagePrefabs(GenerateLink());
        }

        _hasError = false;
    }

    private IEnumerator LoadImagePrefabs(string link)
    {
        var request = UnityWebRequestTexture.GetTexture(link);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            _hasError = true;
        }
        else
        {
            var imagePrefab = Instantiate(_imagePrefab, _scrollViewContent);
            imagePrefab.name = _picNum.ToString();
            imagePrefab.GetComponent<Image>().color = Color.clear;
        }
    }
}