using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] Camera camera;
    [SerializeField] GameObject player;

    void Update()
    {
        camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
    }
}
