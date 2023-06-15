using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GallerySceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TextMeshProUGUI _progressValueText;

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        //Fake load <90%
        var timer = 2f;
        _loadingScreen.SetActive(true);
        while (timer > 0f)
        {
            var progress = (2f - timer) / 20f * 9f;
            var progressValue = progress * 100f;
            _progressSlider.value = progress;
            _progressValueText.text = Mathf.RoundToInt(progressValue) + "%";
            timer -= Time.deltaTime;
            yield return null;
        }

        //Normal load >90%
        var operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            var progress = 0.9f + Mathf.Clamp01(operation.progress / 0.9f) / 10f;
            var progressValue = progress * 100f;
            _progressSlider.value = progress;
            _progressValueText.text = Mathf.RoundToInt(progressValue) + "%";
            yield return null;
        }
    }
}