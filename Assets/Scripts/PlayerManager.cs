using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject player;

    public static PlayerManager Instance { private set; get; }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void spawnPlayer()
    {
        player.transform.position = spawnPoint.transform.position;
    }
}
