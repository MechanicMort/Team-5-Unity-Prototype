using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{

   public GameObject topCross;
   private RectTransform topCrossRect;
    private Vector3 topStartPos;

    public GameObject botCross;
    private RectTransform botCrossRect;
    private Vector3 botStartPos;


    public Light spotLight;

    // Start is called before the first frame update
    void Start()
    {
        botCrossRect = botCross.GetComponent<RectTransform>();
        topCrossRect = topCross.GetComponent<RectTransform>();
        botStartPos = botCrossRect.localPosition;
        topStartPos = topCrossRect.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        botCrossRect.position = new Vector3(botStartPos.x,botStartPos.y - spotLight.spotAngle,botStartPos.z);
        topCrossRect.position = new Vector3(topStartPos.x, topStartPos.y + spotLight.spotAngle, topStartPos.z);
    }
}
