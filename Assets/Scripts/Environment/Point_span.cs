using Game.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_span : MonoBehaviour{
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag=="Player") {
            other.GetComponent<Player_controller>().startPosition=transform.position;
            other.GetComponent<Player_controller>().startPosition.y=transform.position.y+10;
        }
    }
}
