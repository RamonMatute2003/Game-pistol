using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_with_floor : MonoBehaviour{
    public CharacterController player;
    public Vector3 ground_position, last_ground_position;
    public string ground_name, last_ground_name;
    public Quaternion current_rotation, last_rotation;

    void Start(){
        player=this.GetComponent<CharacterController>();
    }

    void FixedUpdate(){
        if(player.isGrounded){
            RaycastHit hit;

            if(Physics.SphereCast(transform.position, player.height/4.2f,-transform.up, out hit)){
                GameObject grounded_in=hit.collider.gameObject;
               
                ground_name=grounded_in.name;
                ground_position=grounded_in.transform.position;
                current_rotation=grounded_in.transform.rotation;

                if(ground_position!=last_ground_position && ground_name==last_ground_name){
                    this.transform.position+=ground_position-last_ground_position;
                }

                if(current_rotation!=last_rotation && ground_name==last_ground_name){
                    var new_rotation=this.transform.rotation*(current_rotation.eulerAngles-last_rotation.eulerAngles);
                    this.transform.RotateAround(grounded_in.transform.position, Vector3.up, new_rotation.y);
                }

                last_ground_name=ground_name;
                last_ground_position=ground_position;
                last_rotation=current_rotation;
            }

        }else{
            if(!player.isGrounded){
                last_ground_name=null;
                last_ground_position=Vector3.zero;
                last_rotation=Quaternion.Euler(0,0,0);
            }
        }
    }

    private void OnDrawGizmos(){
        player=this.GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position, player.height/4.2f);
    }
}
