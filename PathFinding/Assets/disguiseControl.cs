using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disguiseControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Mouse")
        {
            other.BroadcastMessage("Disguise");
            this.gameObject.SetActive(false);
        }
    }
}
