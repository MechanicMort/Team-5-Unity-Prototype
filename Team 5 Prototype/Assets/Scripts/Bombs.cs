using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBomb", menuName = "Bombs")]
public class Bombs : ScriptableObject
{
    public string bombName;

    public float damage;
    public float force;
    public float delay;
    public float countDown;
    public float radius;

    public bool hasExploded;

    public GameObject expParticles;
}