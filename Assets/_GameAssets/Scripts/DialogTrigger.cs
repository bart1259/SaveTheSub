using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public string dialog;
    public bool onlyTriggerOnce;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            DialogSystem.GetInstance().StartDialog(dialog);
            if (onlyTriggerOnce) {
                gameObject.SetActive(false);
            }
        }
    }
}
