using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{



    public void Play1Level()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Play2Level()
    {
    SceneManager.LoadScene("SecondLevel");
    }

    public void ChooseLevel()
    {
        
    }
}
