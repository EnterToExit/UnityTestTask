using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageSpriteController : MonoBehaviour
{
    private Camera _camera;
    private RectTransform _rectTransform;
    private bool _active;

    private void Start()
    {
        _camera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
        // StartCoroutine(LoadImageTexture(GenerateLink()));
    }

    private void Update()
    {
        if (_active) return;
        ActivateIfInView();
    }

    private void ActivateIfInView()
    {
        // Get the screen coordinates of the four corners of the RectTransform
        var corners = new Vector3[4];
        _rectTransform.GetWorldCorners(corners);

        var minScreenPoint = RectTransformUtility.WorldToScreenPoint(_camera, corners[0]);
        var maxScreenPoint = RectTransformUtility.WorldToScreenPoint(_camera, corners[2]);

        // Check if the RectTransform is fully or partially visible on the screen
        var rectBounds = new Rect(minScreenPoint, maxScreenPoint - minScreenPoint);
        var screenBounds = new Rect(0, 0, Screen.width, Screen.height);
        var isVisible = screenBounds.Overlaps(rectBounds, true);

        if (isVisible)
        {
            Debug.Log("RectTransform is visible on the screen.");
            _active = true;
        }
        else
        {
            Debug.Log("RectTransform is not visible on the screen.");
        }
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
            transform.GetComponent<Image>().sprite = Sprite.Create(serverTexture,
                new Rect(0, 0, serverTexture.width, serverTexture.height),
                new Vector3(.5f, 5f));
        }
    }
}