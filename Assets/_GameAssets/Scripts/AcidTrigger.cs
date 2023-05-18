using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidTrigger : MonoBehaviour
{
    public GameObject acid;

    public bool onlyTriggerOnce;

    void Start()
    {
        acid.GetComponent<AcidExpander>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D Enemy)
    {
     if (Enemy.tag == "Player")
     {
        if (onlyTriggerOnce) {
            gameObject.SetActive(false);
            acid.GetComponent<AcidExpander>().enabled = true;
        }
     }
    }
}
