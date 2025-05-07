using UnityEngine;
using UnityEngine.SceneManagement;

public class DeatbScreenManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your game scene name
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("StartScreen"); // Replace with your start screen scene name
    }
}