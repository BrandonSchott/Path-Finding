using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedBoostControl : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        other.BroadcastMessage("SpeedBoost");
        this.gameObject.SetActive(false);
    }
}
