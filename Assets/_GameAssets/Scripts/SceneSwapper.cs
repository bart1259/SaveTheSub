using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.tag == "Player")
        {
            StartCoroutine(Fade());
        }
    }

    public void SwapScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Fade()
    {
        float ANIMATION_TIME = 1.5f;
        int ANIMATION_STEPS = 50;

        for (float l = 0.0f; l < 1.0f; l += 1.0f / ANIMATION_STEPS)
        {
            Camera.main.GetComponent<AudioSource>().volume -= ANIMATION_TIME/ANIMATION_STEPS;
            Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.Lerp(Color.clear, Color.black, l);
            yield return new WaitForSeconds(ANIMATION_TIME / ANIMATION_STEPS);
        }

        SceneManager.LoadScene(sceneName);
    }
}
