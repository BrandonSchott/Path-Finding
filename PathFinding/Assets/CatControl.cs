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

    CatGhostScript ghost;

    float movementSpeed;

    public bool wallDetected, detected;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 3.0f;
        ghost = catGhost.GetComponent<CatGhostScript>();
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
        

        if(!wallDetected && detected)
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
}
