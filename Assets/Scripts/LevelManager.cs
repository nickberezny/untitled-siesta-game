using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void restartLevel()
    {
        StaticClass.loaderName = "Restart";
        SceneManager.LoadScene("Level1");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void nextLevel(string levelName)
    {
        StaticClass.loaderName = "Next";
        SceneManager.LoadScene(levelName);
    }
}
