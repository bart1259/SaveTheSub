using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    void SwapScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
