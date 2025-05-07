using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Game_Screen"); // Replace with your game scene name
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("Start_Screen"); // Replace with your start screen scene name
    }
}