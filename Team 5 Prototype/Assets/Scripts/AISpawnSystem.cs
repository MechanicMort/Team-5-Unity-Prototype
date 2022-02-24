using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnSystem : MonoBehaviour
{
    public GameObject AI;
    private int count;

    private void Awake()
    {
        count = 0;
        StartCoroutine("AISpawner");

    }

    IEnumerator AISpawner()
    {
        if(count <= 3)
        {
            yield return new WaitForSeconds(30f);
            Instantiate(AI, this.transform.position, Quaternion.identity);
            count++;
        }     
    }
}
