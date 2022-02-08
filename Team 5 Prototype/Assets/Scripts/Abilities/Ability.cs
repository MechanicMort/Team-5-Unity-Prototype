using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "AbilityTestOne" , menuName = "Abilities")]
public class Ability : ScriptableObject
{
    public float coolDown;
    public string abilityName;
    public GameObject abilityOwner;
    public Sprite abilitySprite;
    
    



   public  void DoAbility()
    {
        switch (abilityName)
        {
            case "Yes":
                Debug.Log("Yes");
                abilityOwner.GetComponent<PlayerController>().Heal(50);
                break;
            case "No":
                Debug.Log("NO");
                abilityOwner.GetComponent<PlayerController>().TakeDamage(30);
                break;
            case "UltimateTest":
                Debug.Log("UltimateTest");
                abilityOwner.GetComponent<PlayerController>().Heal(700);
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
}

