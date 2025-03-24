using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void SinglePlayer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SinglePlayer");
    }
    public void MultiPlayer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Multiscene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
