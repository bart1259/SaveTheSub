using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelMover : MonoBehaviour
{
    public float speed;
    public GameObject eelGO;
    public GameObject[] eelCheckpoints;

    private int targetIndex;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if(eelCheckpoints.Length < 1) {
            Debug.Log("No eel checkpoints found!");
            return;
        }

        // Set initial location
        eelGO.transform.position = eelCheckpoints[0].transform.position;

        // Get sprite renderer
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = eelGO.transform.position;
        Vector3 targetPosition = eelCheckpoints[targetIndex].transform.position;

        spriteRenderer.flipX = (targetPosition.x - currentPosition.x > 0);

        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        eelGO.transform.position = newPosition;

        if((newPosition - targetPosition).sqrMagnitude < (0.01f * 0.01f)) {
            // Get next target
            targetIndex = (targetIndex + 1) % eelCheckpoints.Length;
        }
    }
}
