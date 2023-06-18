using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneLoader : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        Debug.Log($"Loaded {sceneIndex}");
        SceneManager.LoadScene(sceneIndex);
    }
}
