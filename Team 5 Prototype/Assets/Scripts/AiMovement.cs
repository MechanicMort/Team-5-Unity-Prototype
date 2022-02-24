using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    
    public GameObject currentNode;
    GameObject previousNode;
    GameObject[] allNodes;
    Vector3 target;

    bool travelling;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        allNodes = GameObject.FindGameObjectsWithTag("Node");
        currentNode = GetRandomNode();
        SetDest();

    }

    private void Update()
    {
        if(agent.remainingDistance <= 0.5f)
        {
            SetDest();
        }
    }


    public GameObject GetRandomNode()
    {
        if(allNodes.Length == 0)
        {
            return null;
        }
        else
        {
            int index = Random.Range(0, allNodes.Length);
            return allNodes[index];
        }
    }

    public void SetDest()
    {
        previousNode = currentNode;
        currentNode = GetRandomNode();
        target = currentNode.transform.position;
        agent.destination = target;



    }

    
}
