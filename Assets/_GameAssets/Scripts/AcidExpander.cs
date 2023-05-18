using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidExpander : MonoBehaviour
{
    public GameObject acid;

    private float scale = 0.25f;
    float timePassed = 0f;

    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > 0.1f)
        {
            acid.transform.localScale += new Vector3(0, scale, 0);
            timePassed = 0f;
        } 
    }

}