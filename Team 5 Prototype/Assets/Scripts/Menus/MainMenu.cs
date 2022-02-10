using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    
    public void Quit()
    {
        Application.Quit();
    }
    public void OptionsMenu()
    {
        //open options Menu
    }
    public void LoadMain()
    {
       // SceneManager.LoadScene(MainLevel.name);
    }

    public void LoadPractice()
    {
        SceneManager.LoadScene(1);
    }



}
