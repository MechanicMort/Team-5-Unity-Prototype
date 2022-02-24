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
        Vector3 mask = new Vector3(0, 0, 0); //: new Vector3(0, 1, 0);
        // Does the ray intersect any objects excluding the player layer
        var colliders = Physics.OverlapSphere(transform.position, 1.5f);
        if (colliders.Length > 0)
        {
            foreach (var cld in colliders)
            {
                Vector3 dir = GetComponent<Rigidbody>().velocity;
                var paintController = cld.transform.GetComponent<UVPaintController>();
                if (paintController != null)
                {
                    paintController.PaintOnGO(transform.position, dir, mask, 1);
                    paintController.PaintOnGO(transform.position + new Vector3(2.5f,0, 0), dir, mask, 1);
                    paintController.PaintOnGO(transform.position + new Vector3(0,0, 2.5f), dir, mask, 1);
                    paintController.PaintOnGO(transform.position + new Vector3(0, 0, -2.5f), dir, mask, 1);
                    paintController.PaintOnGO(transform.position + new Vector3(-2.5f, 0, 0), dir, mask, 1);
                }
            }
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
