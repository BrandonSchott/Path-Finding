using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    RaycastHit hit;

    RaycastHit wallHit;

    [SerializeField]
    float movementSpeed;
    
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

        if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
        {
            if (hit.transform.tag == "Wall")
            {

                float angle = Vector3.SignedAngle(transform.forward, -hit.normal, Vector3.up);

                transform.Rotate(0, angle, 0);

                //transform.localPosition -= transform.forward;
                if (Physics.Raycast(transform.position, transform.right, out hit, 4.0f))
                {
                    transform.Rotate(Vector3.up, -90);
                }
                else
                {
                    transform.Rotate(Vector3.up, 90);
                }
            }
        }
        else
        {
            Collider[] detector = Physics.OverlapSphere(transform.position, 7);

            foreach (Collider col in detector)
            {
                if (col.transform.tag == "Cat")
                {
                    wallNotDetected = true;
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
                        if(!Physics.Raycast(transform.position, col.transform.position - transform.position,2))
                        {
                            transform.LookAt(col.transform.position);
                            transform.Rotate(new Vector3(0, 180, 0));
                        }
                    }
                    if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
                    {
                        if (hit.transform.tag == "Wall")
                        {

                            float angle = Vector3.SignedAngle(transform.forward, -hit.normal, Vector3.up);

                            transform.Rotate(0, angle, 0);

                            //transform.localPosition -= transform.forward;
                            if (Physics.Raycast(transform.position, transform.right, out hit, 4.0f))
                            {
                                transform.Rotate(Vector3.up, -90);
                            }
                            else
                            {
                                transform.Rotate(Vector3.up, 90);
                            }

                        }
                    }

                }

                if(col.transform.tag == "Speed")
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
                    if(wallNotDetected)
                    {
                        transform.LookAt(col.transform.position);
                    }
                }
            }
        }

        if(movementSpeed > 3 && Time.time > speedBoostStamp + 10)
        {
            movementSpeed = 3;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Cat")
        {
            this.gameObject.SetActive(false);
        }
    }
    
    public void SpeedBoost()
    {
        movementSpeed = 5;
        speedBoostStamp = Time.time;
    }
}
