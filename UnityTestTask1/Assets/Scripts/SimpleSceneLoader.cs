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

public class NavigationControls : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape");
        }

        if (Input.GetKeyDown(KeyCode.Menu))
        {
            Debug.Log("Menu");
        }
    }
}
