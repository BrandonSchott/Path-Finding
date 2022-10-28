using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngineInternal;

public class CatControl : MonoBehaviour
{
    [SerializeField]
    GameObject mouse, catGhost;

    RaycastHit hit;
    RaycastHit rightHit;

    CatGhostScript ghost;

    float movementSpeed;

    public bool wallDetected, detected;

    [SerializeField]
    bool wallRight, wallLeft;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 3.0f;
        ghost = catGhost.GetComponent<CatGhostScript>();
        wallRight = false;
        wallLeft = false;  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        if(Physics.Linecast(transform.position, mouse.transform.position, out hit))
        {
            if(hit.transform.tag == "Wall")
            {
                wallDetected = true;
            }
            else
            {
                wallDetected = false;
            }
        }

        if(Physics.Raycast(transform.position, transform.right, out rightHit, 5))
        {
            if(rightHit.transform.tag == "Wall")
            {
                
                wallRight = true;
            }
            else
            {
                wallRight = false;
            }
        }
        if (Physics.BoxCast(transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, out hit, new Quaternion(0, 0, 0, 0), 0.5f))
        {
            Debug.Log("Wall");
            Turn();
        }

        if (!wallDetected && detected)
        {
            ghost.mouseFound = true;
        }

        if(ghost.goodToTurn)
        {
            transform.LookAt(mouse.transform);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Mouse")
        {
            detected = true;
        }
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, mouse.transform.position);
    }

    void Turn()
    {
        transform.Rotate(0,-90,0);
    }
}
