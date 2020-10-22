using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [SerializeField] string NextLevel;
    [SerializeField] LevelManager levelManager;
    [SerializeField] Player player;
    [SerializeField] RawImage fade;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision w/ " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            //win
            //animation
            //next level
            //levelManager.nextLevel(NextLevel);

            player.Win();
            StartCoroutine(loadNextScene());
        }
    }

    IEnumerator loadNextScene()
    {
        float dt = 0;

        while(dt < 7f)
        {
            dt += Time.deltaTime;
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, dt/5);
            yield return new WaitForSeconds(0.01f);
        }
        
        levelManager.loadNext();
    }
}
