using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICat : MonoBehaviour
{
    float movementSpeed;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

        if (Physics.BoxCast(transform.position, new Vector3(0.4f, 0.4f, 0.4f), transform.forward, out hit, new Quaternion(0,0,0,0), 2))
        {
            Debug.Log("Hit Wall");
                float angle = Vector3.SignedAngle(transform.forward, -hit.normal, Vector3.forward);
                transform.Rotate(0, angle, 0);

        }

    }
}
