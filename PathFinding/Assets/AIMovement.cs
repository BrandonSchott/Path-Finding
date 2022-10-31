using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    RaycastHit hit;

    RaycastHit wallHit;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 3.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * movementSpeed);

        Collider[] detector = Physics.OverlapSphere(transform.position, 7);

        Transform closestMouse;

        foreach (Collider col in detector)
        {
            if (col.transform.tag == "Mouse")
            {
                bool wallDetected = true;
                if (Physics.Linecast(transform.position, col.transform.position, out wallHit))
                {
                    if (wallHit.transform.tag == "Wall")
                    {
                        wallDetected = false;
                    }
                }

                if (wallDetected)
                {
                    transform.LookAt(col.transform.position);
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5.0f))
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
