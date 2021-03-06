﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 public class HealthBar : MonoBehaviour
{
    public float barDisplay = 1; //current progress
    //[SerializeField] Vector2 pos = new Vector2(20, 40);
    //[SerializeField] Vector2 size = new Vector2(100, 30);
    [SerializeField] Texture2D emptyTex;
    [SerializeField] Texture2D fullTex;
    [SerializeField] AudioSource gameMusic;

    private float w;
    float h;


    void OnGUI()
    {
        w = Screen.width;
        h = Screen.height;
        Vector2 size = new Vector2(w*4/5, 20);
        Vector2 pos = new Vector2(w/10, h/20);
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();

        //if (barDisplay > 0.15) gameMusic.pitch = 0.7f;
        //else gameMusic.pitch = 0.6f;
      
    }

}

