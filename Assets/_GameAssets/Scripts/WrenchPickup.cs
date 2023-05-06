using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchPickup : MonoBehaviour
{
    public GameObject wrenchGraphic;
    public float bobAmplitude;
    public float bobPeriod;

    private Vector3 originalPosition;
    private float time;

    void Start() {
        originalPosition = wrenchGraphic.transform.position;
    }

    void Update() {
        time += Time.deltaTime;
        wrenchGraphic.transform.position = originalPosition + (Vector3.up * Mathf.Sin(time * bobPeriod) * bobAmplitude);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PickupSystem.GetInstance().PickupWrench();
            Destroy(gameObject);
        }
    }

}
