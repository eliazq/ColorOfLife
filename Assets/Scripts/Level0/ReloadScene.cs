using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    // Time to wait before reloading the scene
    public float reloadTime = 35f;

    void Start()
    {
        // Start the coroutine to reload the scene after a delay
        StartCoroutine(ReloadSceneAfterDelay());
    }

    IEnumerator ReloadSceneAfterDelay()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(reloadTime);

        // Get the name of the current scene
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Reload the current scene by its name
        SceneManager.LoadScene(currentSceneName);
    }
}