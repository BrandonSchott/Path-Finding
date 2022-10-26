using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CatGhostScript : MonoBehaviour
{
    [SerializeField]
    GameObject mouse;

    RaycastHit hit;

    public bool mouseFound, leftSideGood, rightSideGood, goodToTurn;
    // Start is called before the first frame update
    void Start()
    {
        mouseFound = false;
        leftSideGood = false;
        rightSideGood = false;
        goodToTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseFound)
        {
            transform.LookAt(mouse.transform);
            //leftRay
            if (Physics.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), transform.forward, out hit, 10.0f, 6))
            {
                leftSideGood = false; 
            }
            else
            {
                leftSideGood = true;
            }
            //rightRay
            if (Physics.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.forward, out hit, 10.0f, 6))
            {
                rightSideGood = false;
            }
            else
            {
                rightSideGood = true;
            }

            if (rightSideGood && leftSideGood)
            {
                goodToTurn = true;
            }
            else
            {
                goodToTurn = false;
            }
        }

    }

}
