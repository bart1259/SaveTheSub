using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    public void OnStartClicked()
    {
        GetComponent<Animator>().SetBool("startGame", true);
        // SceneManager.LoadScene("Level1");
    }

    public void OnQuitClicked() {
        Application.Quit();
    }

}