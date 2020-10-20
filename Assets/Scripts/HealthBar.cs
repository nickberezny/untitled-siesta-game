using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 public class HealthBar : MonoBehaviour
{
    public float barDisplay; //current progress
    [SerializeField] Vector2 pos = new Vector2(20, 40);
    [SerializeField] Vector2 size = new Vector2(60, 20);
    [SerializeField] Texture2D emptyTex;
    [SerializeField] Texture2D fullTex;
    [SerializeField] AudioSource gameMusic;

    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();

        if (barDisplay > 0.5) gameMusic.pitch = 1;
        else if (barDisplay > 0.2) gameMusic.pitch = 0.95f;
        else gameMusic.pitch = 0.75f;
      
    }

}

