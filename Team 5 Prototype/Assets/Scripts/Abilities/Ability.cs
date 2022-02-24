using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "DefaultAbility" , menuName = "Ability")]
public class Ability : ScriptableObject
{
    public float coolDown;
    public string abilityName;
    public GameObject abilityOwner;
    public Sprite abilitySprite;
    
    



   public  void DoAbility()
    {
        Debug.Log(abilityOwner.name);
        switch (abilityName)
        {
            case "Yes":
                Debug.Log("Yes");
                abilityOwner.GetComponent<PlayerInputController>().Heal(50);
                break;
            case "No":
                Debug.Log("NO");
                abilityOwner.GetComponent<PlayerInputController>().TakeDamage(30);
                break;
            case "UltimateTest":
                Debug.Log("UltimateTest");
                abilityOwner.GetComponent<PlayerInputController>().Heal(700);
                break;
            case "Dash":
                Debug.Log("Dash");
                abilityOwner.GetComponent<PlayerInputController>().abilitySpeed = 10f;
                abilityOwner.GetComponent<PlayerInputController>().RestoreValue(0.1f);
                break;
            case "DoubleJump":
                Debug.Log("Double Jump");
                break;
            case "OverDrive":
                Debug.Log("OverDrive");
                abilityOwner.GetComponent<PlayerInputController>().TakeDamage(30);
                abilityOwner.GetComponent<PlayerInputController>().fireRateMulti = 1.6f;
                abilityOwner.GetComponent<PlayerInputController>().abilitySpeed = 0.7f;
                abilityOwner.GetComponent<PlayerInputController>().RestoreValue(3f);
                break;
            case "OverShield":
                Debug.Log("OverShield");
                abilityOwner.GetComponent<PlayerInputController>().ShieldRestore(100);
                break;
            case "ShieldCore":
                Debug.Log("ShieldCore");
                abilityOwner.GetComponent<PlayerInputController>().ShieldRestore(200);
                abilityOwner.GetComponent<PlayerInputController>().fireRateMulti = 3f;
                abilityOwner.GetComponent<PlayerInputController>().abilitySpeed = 0.0f;
                abilityOwner.GetComponent<PlayerInputController>().RestoreValue(2f);
                break;
            case "PaintNade":
                Debug.Log("PaintNade");
                abilityOwner.GetComponent<GunScript>().ThrowNade("");
                break;
            case "WaterNade":
                Debug.Log("waterNade");
                abilityOwner.GetComponent<GunScript>().ThrowNade("Water");
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }


}


