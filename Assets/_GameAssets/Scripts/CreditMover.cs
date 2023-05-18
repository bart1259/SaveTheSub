using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMover : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.up * speed * Time.deltaTime);
        if(transform.position.y > 1800) {
            GetComponent<SceneSwapper>().SwapScenes("MainMenu");
        }
    }
}
