using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsManager : MonoBehaviour
{
    [SerializeField] Canvas controls;
    [SerializeField] Canvas Instuctions;
    [SerializeField] GameObject guy;

    private int page = 1;
    public void Update()
    {
        if(Input.GetKeyDown("space") || Input.GetKeyDown("enter"))
        {
            next();
        }
    }
    public void next()
    {
        if(page == 1)
        {
            page = 2;
            Destroy(guy);
            Instuctions.enabled = false;
            controls.enabled = true;


        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
