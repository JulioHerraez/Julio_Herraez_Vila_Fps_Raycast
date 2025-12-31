using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void RestartLevel()
    {
        Time.timeScale = 1f;

        
        RestartData.forceLevelRestart = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();


        
    }
}