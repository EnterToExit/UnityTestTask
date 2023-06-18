using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageSpriteController : MonoBehaviour
{
    private Camera _camera;
    private RectTransform _rectTransform;
    private Canvas _overlayCanvas;
    private bool _active;

    private void Start()
    {
        _camera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
        _overlayCanvas = FindObjectOfType<Canvas>();
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

        // Convert the corners to viewport coordinates
        var minViewportPoint = _camera.WorldToViewportPoint(corners[0]);
        var maxViewportPoint = _camera.WorldToViewportPoint(corners[2]);

        // Check if the RectTransform is visible on the overlay canvas
        var isVisible = IsRectVisibleOnCanvas(minViewportPoint, maxViewportPoint, _overlayCanvas);

        if (isVisible)
        {
            _active = true;
            StartCoroutine(LoadImageTexture(GenerateLink()));
        }
    }
    
    private bool IsRectVisibleOnCanvas(Vector3 minViewportPoint, Vector3 maxViewportPoint, Canvas canvas)
    {
        var canvasRect = canvas.pixelRect;
        var rectBounds = new Rect(minViewportPoint, maxViewportPoint - minViewportPoint);

        // Check if the RectTransform bounds intersect with the canvas bounds
        return canvasRect.Overlaps(rectBounds, true);
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
            // transform.GetComponent<Image>().color = Color.white;
            var serverTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            transform.GetComponent<Image>().sprite = Sprite.Create(serverTexture,
                new Rect(0, 0, serverTexture.width, serverTexture.height),
                new Vector3(.5f, 5f));
            transform.GetComponent<Image>().color = Color.white;
        }
    }
}