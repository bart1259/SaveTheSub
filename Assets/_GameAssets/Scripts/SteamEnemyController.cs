using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamEnemyController : MonoBehaviour
{
    public float offTime = 3.0f;
    public float onTime = 0.5f;

    public new ParticleSystem particleSystem;
    public Collider2D damageCollider;

    private float timer;
    private bool on = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = offTime / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        if (on && timer < 0.0f) {
            timer = offTime;
            TurnOff();
        }
        if (!on && timer < 0.0f) {
            timer = onTime;
            TurnOn();
        }
    }

    void TurnOff() {
        on = false;
        particleSystem.Stop();
        damageCollider.gameObject.SetActive(false);
    }

    void TurnOn() {
        on = true;
        particleSystem.Play();
        damageCollider.gameObject.SetActive(true);
    }
}
