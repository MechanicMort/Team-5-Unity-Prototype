using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

        public ClassHolder[] classHolder = new ClassHolder[6];
        public WeaponStats[] weapons = new WeaponStats[6];
        public GameObject[] players = new GameObject[2];
        public GameObject[] playersUI = new GameObject[2];



    void Start()
    {

        AssignPlayers();
    }


    private void AssignPlayers()
    {
        print(PlayerPrefs.GetString("RedPlayerClass"));
        print(PlayerPrefs.GetString("BluePlayerClass"));
        PlayerPrefs.SetString("BluePlayerClass", "Gren");
        if (PlayerPrefs.GetString("RedPlayerClass") == "Heavy")
        {
            players[0].GetComponent<PlayerInputController>().currentClass = classHolder[0];
        }
        if (PlayerPrefs.GetString("RedPlayerClass") == "Scout")
        {
            players[0].GetComponent<PlayerInputController>().currentClass = classHolder[1];
        }
        if (PlayerPrefs.GetString("RedPlayerClass") == "Gren")
        {
            players[0].GetComponent<PlayerInputController>().currentClass = classHolder[2];
        }

        if (PlayerPrefs.GetString("BluePlayerClass") == "Heavy")
        {
            players[1].GetComponent<PlayerInputController>().currentClass = classHolder[3];
        }
        if (PlayerPrefs.GetString("BluePlayerClass") == "Gren")
        {
            players[1].GetComponent<PlayerInputController>().currentClass = classHolder[4];
        }
        if (PlayerPrefs.GetString("BluePlayerClass") == "Scout")
        {
            players[1].GetComponent<PlayerInputController>().currentClass = classHolder[5];
        }

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerInputController>().ApplyClass();
          //  players[i].GetComponent<PlayerInputController>().ApplyWeaponStats();

        }
        for (int i = 0; i < playersUI.Length; i++)
        {
            playersUI[i].GetComponent<PlayerUIManagement>().canStart = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
