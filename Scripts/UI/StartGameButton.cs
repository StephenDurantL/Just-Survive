using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    // Specify the target scene name
    [SerializeField] private string targetSceneName = "Game";

    public void StartGame()
    {
        // Load the specified game scene
        Debug.Log($"Loading scene: {targetSceneName}");
        SceneManager.LoadScene(targetSceneName);
    }
}
