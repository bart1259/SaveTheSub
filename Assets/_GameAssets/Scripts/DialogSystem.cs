using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{

    public GameObject graphicsGO;
    public TMP_Text speechText;
    public float secondsPerChar = 0.05f;
    public float secondsPerPunctuation = 0.2f;

    private float timer;
    private string fullText;
    private string words;
    private int wordIndex = 0;
    private bool active = false;
    private bool animating = false;

    private GameObject playerGO;

    // Singleton
    private static DialogSystem instance;

    public static DialogSystem GetInstance() {
        return instance;
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        playerGO = GameObject.FindWithTag("Player");
    }

    void Update() {
        if(active) {
            if(animating) {
                timer -= Time.deltaTime;
                if (timer < 0) {
                    // Add next word
                    speechText.text += words[wordIndex] ;

                    // reset timer
                    if (words[wordIndex] == '.' || words[wordIndex] == '?' || words[wordIndex] == '!')
                    {
                        timer = secondsPerPunctuation;
                    } else {
                        timer = secondsPerChar;
                    }

                    wordIndex += 1;
                    if (wordIndex >= words.Length) {
                        animating = false;
                    }
                }
            }

            if (Input.anyKeyDown && animating) {
                animating = false;
                speechText.text = fullText;
            } else if (Input.anyKeyDown)
            {
                StopDialog();
            }
        }
    }

    public void StopDialog() {
        active = false;
        graphicsGO.SetActive(false);

        // Enable player movment
        playerGO.GetComponent<PlayerController>().enabled = true;
    }

    public void StartDialog(string text) {
        speechText.text = "";
         
        words = text;
        fullText = text;
        wordIndex = 0;

        timer = secondsPerChar;

        active = true;
        animating = true;
        graphicsGO.SetActive(true);

        // Disable player movment
        playerGO.GetComponent<PlayerController>().enabled = false;
    }
}
