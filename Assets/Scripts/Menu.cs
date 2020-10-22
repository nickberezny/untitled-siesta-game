using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class StaticClass
{   
    public static string loaderName { get; set; }
}


public class Menu : MonoBehaviour
{

    public void startGame()
    {
        StaticClass.loaderName = "Main";
        SceneManager.LoadScene("Level1");
    }

}
