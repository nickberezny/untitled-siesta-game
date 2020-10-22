using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] string nextLevel;

    public void restartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        StaticClass.loaderName = "Restart";
        SceneManager.LoadScene(scene.name);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadNext()
    {
        StaticClass.loaderName = "Next";
        SceneManager.LoadScene(nextLevel);
    }
}
