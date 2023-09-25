using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr : MonoBehaviour
{

    private void OnEnable()
    {
        // Register the sceneLoaded event callback
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unregister the sceneLoaded event callback to avoid potential memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the "SampleScene"
        if (scene.name == "SampleScene")
        {
            // Get the component reference from the GameObject in "SampleScene" and execute its method
            ColorSorting sampleScript = FindObjectOfType<ColorSorting>();
            if (sampleScript != null)
            {
                sampleScript.Start();
            }
        }
    }
}
