using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportTrigger : MonoBehaviour
{
    private AudioSource door;
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other) //When You Collide With Object.
    {
        if (other.gameObject.CompareTag("Player"))//compare tag TeleportTrigger
        {
            StartCoroutine(Fade());
            //if collide with object that has tag "TeleportTrigger"
            //then load the next scene//
        }
    }

    IEnumerator Fade()
    {
        door.Play();
        // yield return new WaitForSeconds(1);
        float ANIMATION_TIME = 1.5f;
        int ANIMATION_STEPS = 50;

        for (float l = 0.0f; l < 1.0f; l += 1.0f / ANIMATION_STEPS)
        {
            Camera.main.GetComponent<AudioSource>().volume -= ANIMATION_TIME/ANIMATION_STEPS;
            Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.Lerp(Color.clear, Color.black, l);
            yield return new WaitForSeconds(ANIMATION_TIME / ANIMATION_STEPS);
        }
        SceneManager.LoadScene("Level2");
    }
}
