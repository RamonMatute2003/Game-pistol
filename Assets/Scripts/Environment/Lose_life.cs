using Game.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose_life : MonoBehaviour{

    private void OnTriggerEnter(Collider other) {
        
        if(other.tag=="Player") {
            print(other.tag);
            other.GetComponent<Player_controller>().subtract_life();
        }
    }
}
