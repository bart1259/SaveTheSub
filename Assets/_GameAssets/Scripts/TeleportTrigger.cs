using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other) //When You Collide With Object.
    {
        if (other.gameObject.CompareTag("Player"))//compare tag TeleportTrigger
        {
            SceneManager.LoadScene("Level2");
            Debug.Log("Loaded Next Scene");
            //if collide with object that has tag "TeleportTrigger"
            //then load the next scene//
        }
    }
}
