using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_drop_object : MonoBehaviour
{
    public bool is_pickable = true;

    private void OnTriggerEnter(Collider other) {
        if(other.tag=="Player_interaction_zone") {
            other.GetComponentInParent<Pick_up_object>().object_to_pick_up=this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag=="Player_interaction_zone") {
            other.GetComponentInParent<Pick_up_object>().object_to_pick_up=null;
        }
    }
}
