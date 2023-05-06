using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public bool hasWrench;
    public GameObject wrenchIcon;

    private static PickupSystem instance = null;

    public static PickupSystem GetInstance() {
        return instance;
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start()
    {
        if (hasWrench)
        {
            PickupWrench();
        }
    }

    public void PickupWrench()
    {
        hasWrench = true;
        wrenchIcon.SetActive(true);
    }
}
