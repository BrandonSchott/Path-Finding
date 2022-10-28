using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CatGhostScript : MonoBehaviour
{
    [SerializeField]
    GameObject mouse;

    RaycastHit hit;

    [SerializeField]
    public bool mouseFound, clear, goodToTurn;
    // Start is called before the first frame update
    void Start()
    {
        mouseFound = false;
        clear = false;
        goodToTurn = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (mouseFound)
        {
            transform.LookAt(mouse.transform);
            
            if (Physics.BoxCast(transform.position, new Vector3(1, 1, 1), transform.forward, out hit, new Quaternion(0, 0, 0, 0), 1))
            {
                //Debug.Log("Mouse Not Found");
                clear = false;
            }
            else
            {
                //Debug.Log("Mouse Found");
                clear = true;
            }

            if (clear)
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
