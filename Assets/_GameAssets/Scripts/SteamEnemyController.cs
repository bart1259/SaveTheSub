using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamEnemyController : MonoBehaviour
{

    public float offset = 0.0f;
    public float offTime = 3.0f;
    public float onTime = 0.5f;

    public new ParticleSystem particleSystem;
    public Collider2D damageCollider;
    private AudioSource bubbleSound;

    private float timer;
    public bool on = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = offset;
        bubbleSound = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        if (on && timer < 0.0f) {
            TurnOff();
        }
        if (!on && timer < 0.0f) {
            TurnOn();
        }
    }

    public void TurnOff() {
        timer = offTime;
        on = false;
        particleSystem.Stop();
        damageCollider.gameObject.SetActive(false);
        bubbleSound.Stop();
    }

    public void TurnOn() {
        timer = onTime;
        on = true;
        particleSystem.Play();
        damageCollider.gameObject.SetActive(true);
        bubbleSound.Play();
    }
}
