using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_up_object : MonoBehaviour{
    public GameObject object_to_pick_up, picked_object;
    public Transform interaction_zone;

    private void Update() {
        if(object_to_pick_up!=null && object_to_pick_up.GetComponent<Grab_drop_object>().is_pickable==true && picked_object==null){
            if(Input.GetKeyDown(KeyCode.F)){
                picked_object=object_to_pick_up;
                picked_object.GetComponent<Grab_drop_object>().is_pickable=false;
                picked_object.transform.SetParent(interaction_zone);
                picked_object.transform.position=interaction_zone.position;
                picked_object.GetComponent<Rigidbody>().useGravity=false;
                picked_object.GetComponent<Rigidbody>().isKinematic=true;
            }
        }else{
            if(picked_object!=null){
                if(Input.GetKeyDown(KeyCode.F)) {
                    picked_object.GetComponent<Grab_drop_object>().is_pickable=true;
                    picked_object.transform.SetParent(null);
                    picked_object.GetComponent<Rigidbody>().useGravity=true;
                    picked_object.GetComponent<Rigidbody>().isKinematic=false;
                    picked_object=null;
                }
            }
        }
    }
}
