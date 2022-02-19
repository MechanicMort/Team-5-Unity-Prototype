using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "defaultClass", menuName = "Classes")]
public class ClassHolder : ScriptableObject
{
    public float ShieldRegen;
    public float HealthRegen;
    public float HealthMax;
    public float ShieldMax;
    public float moveSpeed;
    public float jumpForce;


    public Ability ability1;
    public Ability ability2;
    public Ability Ultimate;
}

