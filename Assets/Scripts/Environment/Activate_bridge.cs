using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_bridge : MonoBehaviour{
    public GameObject bridge;
    private void OnTriggerEnter(Collider other) {
        bridge.GetComponent<Rigidbody>().isKinematic=false;
    }
}