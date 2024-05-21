using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement{
    public class Pikable_object:MonoBehaviour{
        public float push_power = 2.0f;
        private float target_mass;

        private void OnControllerColliderHit(ControllerColliderHit hit) {
            Rigidbody body=hit.collider.attachedRigidbody;

            if(body==null || body.isKinematic){
                return;
            }

            if(hit.moveDirection.y < -0.3){
                return;
            }

            Vector3 push_direction=new Vector3(hit.moveDirection.x,0,hit.moveDirection.z);

            target_mass=body.mass;

            body.velocity=push_direction*push_power/target_mass;
        }
    }
}

