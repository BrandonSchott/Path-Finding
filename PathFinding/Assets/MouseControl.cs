using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    RaycastHit hit;

    RaycastHit wallHit;

    Color originalColor;


    Renderer ren;

    [SerializeField]
    Vector3 escapeRoute;

    float movementSpeed;

    float speedBoostStamp, disguisedStamp;

    public bool wallNotDetected, disguised;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 3.0f;
        ren = gameObject.GetComponentInChildren<Renderer>();
        originalColor = ren.material.color;
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

            
            escapeRoute = Vector3.zero;

            foreach (Collider col in detector)
            {
                if (col.transform.tag == "Disguise")
                {
                    transform.LookAt(col.transform.position);
                }
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
                        escapeRoute += (transform.position - col.transform.position);
                        
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

            if(escapeRoute != Vector3.zero)
            {
                if (!Physics.Raycast(transform.position, transform.position + escapeRoute, 3))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, escapeRoute, 1, 0));
                    Debug.Log($"forward = " + transform.forward + " Escape = " + escapeRoute);
                }
                
            }
            
            
        }

        if (movementSpeed > 3 && Time.time > speedBoostStamp + 10)
        {
            movementSpeed = 3;
        }

        if (disguised && Time.time > disguisedStamp + 10)
        {
            disguised = false;
            ren.material.color = originalColor;
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

    public void Disguise()
    {
        disguised = true;
        disguisedStamp = Time.time;
        ren.material.color = Color.blue;
    }
}
