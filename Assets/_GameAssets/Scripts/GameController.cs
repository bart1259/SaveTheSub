using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject playerGO;

    public void Start() {
        playerGO = GameObject.FindGameObjectsWithTag("Player")[0];
        playerGO.GetComponent<PlayerController>().OnPlayerDie += RestartLevel;
    }

    public void RestartLevel() {
        playerGO.transform.position = respawnPoint.transform.position;
    }
}
