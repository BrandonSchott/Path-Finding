using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 3.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * movementSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f))
            {
                float angle = Vector3.SignedAngle(transform.forward, -hit.normal, Vector3.forward);

                transform.Rotate(0, angle, 0);
            }
            //transform.localPosition -= transform.forward;
            if (Random.Range(0, 2) == 0)
            {
                transform.Rotate(Vector3.up, -90);
            }
            else
            {
                transform.Rotate(Vector3.up, 90);
            }
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == 3)
    //    {
    //        transform.localPosition -= transform.forward;
    //        transform.Rotate(Vector3.up, 90);
    //    }        
    //}
}
