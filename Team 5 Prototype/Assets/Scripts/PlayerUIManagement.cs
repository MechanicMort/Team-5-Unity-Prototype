using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManagement : MonoBehaviour
{


    public Image healthDisplay;
    public Image shieldDisplay;

    public Image overHealthDisplay;
    public Image overShieldDisplay;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Display();

    }

    private void Display()
    {
        healthDisplay.fillAmount = (player.playerHealth / player.playerHealthMax);
        shieldDisplay.fillAmount = (player.playerShield / player.playerShieldMax);
        overShieldDisplay.fillAmount = (player.playerOverShield / 100);
        overHealthDisplay.fillAmount = (player.playerOverHealth / 100);
    }
}
