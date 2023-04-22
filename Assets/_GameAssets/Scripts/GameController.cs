using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject playerGO;

    private bool dying = false;

    public void Start() {
        playerGO = GameObject.FindGameObjectsWithTag("Player")[0];
        playerGO.GetComponent<PlayerController>().OnPlayerDie += DieRestartLevel;
    }

    public void DieRestartLevel() {

        if(dying) {
            return;
        }

        dying = true;
        // Play death animation
        Camera.main.transform.GetChild(0).gameObject.SetActive(true);
        Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;

        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        // Disable player movment
        playerGO.GetComponent<PlayerController>().enabled = false;

        float ANIMATION_TIME = 1.5f;
        int ANIMATION_STEPS = 50;

        for (float l = 0.0f; l < 1.0f; l += 1.0f / ANIMATION_STEPS)
        {
            Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.Lerp(Color.clear, Color.black, l);
            yield return new WaitForSeconds(ANIMATION_TIME / ANIMATION_STEPS);
        }

        playerGO.transform.position = respawnPoint.transform.position;

        for (float l = 1.0f; l >= 0.0f; l -= 1.0f / ANIMATION_STEPS)
        {
            Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.Lerp(Color.clear, Color.black, l);
            yield return new WaitForSeconds(ANIMATION_TIME / ANIMATION_STEPS / 2.0f);
        }

        Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
        Camera.main.transform.GetChild(0).gameObject.SetActive(false);

        // Enable player movment
        playerGO.GetComponent<PlayerController>().enabled = true;

        dying = false;
    }
}
