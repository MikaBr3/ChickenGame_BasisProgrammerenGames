using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game_Screen"); // Replace with your game scene name
    }

    public void OpenSettings()
    {
        // Add settings logic (e.g., open a settings panel)
        Debug.Log("Settings opened");
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // For testing in the editor
        #endif
    }
}