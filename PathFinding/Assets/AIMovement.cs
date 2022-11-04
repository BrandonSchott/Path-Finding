using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    RaycastHit hit;

    RaycastHit wallHit;

    float speedBoostStamp;

    bool wallNotDetected;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 3.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * movementSpeed);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 4))
        {
            if (hit.transform.tag == "Wall")
            {
                if (Physics.Raycast(transform.position, transform.forward, out hit, 5.0f))
                {
                    float angle = Vector3.SignedAngle(transform.forward, -hit.normal, Vector3.up);

                    transform.Rotate(0, angle, 0);
                }
                //transform.localPosition -= transform.forward;
                if (Physics.Raycast(transform.position, transform.right, out hit, 5.0f))
                {
                    transform.Rotate(Vector3.up, -90);
                }
                else if (Physics.Raycast(transform.position, -transform.right, out hit, 5.0f))
                {
                    transform.Rotate(Vector3.up, 90);
                }
                else if (Random.Range(0, 2) == 0)
                {
                    transform.Rotate(Vector3.up, -90);
                }
                else
                {
                    transform.Rotate(Vector3.up, 90);
                }
            }
        }

        Collider[] detector = Physics.OverlapSphere(transform.position, 7);

        foreach (Collider col in detector)
        {
            if (col.transform.tag == "Mouse")
            {
                MouseControl mouseCon = col.GetComponent<MouseControl>();
                if (!mouseCon.disguised)
                {
                    wallNotDetected = true;
                    if (Physics.Linecast(transform.position, col.transform.position, out wallHit))
                    {
                        if (wallHit.transform.tag == "Wall")
                        {
                            wallNotDetected = false;
                        }
                    }

                    if (wallNotDetected)
                    {
                        transform.LookAt(col.transform.position);
                    }
                }

            }

            if (col.transform.tag == "Speed")
            {
                if (Physics.Linecast(transform.position, col.transform.position, out wallHit))
                {
                    if (wallHit.transform.tag == "Wall")
                    {
                        wallNotDetected = false;
                    }
                    else
                    {
                        wallNotDetected = true;
                    }
                }
                if (wallNotDetected)
                {
                    transform.LookAt(col.transform.position);
                }
            }

        }
        if (movementSpeed > 3 && Time.time > speedBoostStamp + 10)
        {
            movementSpeed = 3;
        }
    }

    public void SpeedBoost()
    {
        movementSpeed = 5;
        speedBoostStamp = Time.time;
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
