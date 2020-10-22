using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] Camera camera;
    [SerializeField] GameObject player;
    [SerializeField] Health health;

    private bool gameStart = false;

    private void Start()
    {
        //move camera to player

        if (StaticClass.loaderName == "Restart")
        {
            camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20);
            StartCoroutine(moveToPlayer());
        }
        else StartCoroutine(moveToPlayer());
    }

    void Update()
    {
        if(player)
        {
            if (gameStart && player.transform.position.y > -8) camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);

        }
    }

    IEnumerator moveToPlayer()
    {
        yield return new WaitForSeconds(1f);
        Vector3 p = new Vector3(player.transform.position.x, player.transform.position.y, -20);
        Vector3 dir = (p - camera.transform.position);
        dir = dir.normalized;
       

        while((p - camera.transform.position).magnitude > 0.1)
        {
            //Debug.Log((player.transform.position - camera.transform.position).magnitude);
            camera.transform.position = camera.transform.position + dir/7; ///20;
            yield return new WaitForSeconds(0.01f);
        }

        //start
        gameStart = true;
        player.GetComponent<Player>().input = true;
        health.StartGame();
    }
}
