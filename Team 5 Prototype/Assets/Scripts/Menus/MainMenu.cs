using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool team;
    public Text currentTeam;

    
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

    private void Update()
    {
        DisplayTeam();
    }


    public void DisplayTeam()
    {
        if (currentTeam.IsActive())
        {
            if (team)
            {
                currentTeam.text = "Red";
            }
            else if (!team)
            {
                currentTeam.text = "Blue";
            }
        }
        
    }

    public void SelectHeavy()
    {
        if (team )
        {
            PlayerPrefs.SetString("RedPlayerClass", "Heavy");
        }
        else if (!team)
        {
            PlayerPrefs.SetString("BluePlayerClass", "Heavy");
        }
        team = !team;


    }
    public void SelectScout()
    {
        if (team)
        {
            PlayerPrefs.SetString("RedPlayerClass", "Scout");
        }
        else if (!team)
        {
            PlayerPrefs.SetString("BluePlayerClass", "Scout");
        }
        team = !team;
    }
    public void SelectGrenedier()
    {
        if (team)
        {
            PlayerPrefs.SetString("RedPlayerClass", "Gren");
        }
        else if (!team)
        {
            PlayerPrefs.SetString("BluePlayerClass", "Gren");
        }
        team = !team;
    }


    public void SwapTeams()
    {
        print("ran");
        team = !team;
    }
    public void LoadPractice()
    {
        SceneManager.LoadScene(0);
    }



}
